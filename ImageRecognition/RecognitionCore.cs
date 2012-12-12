using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using ImageProcessing;

namespace ImageRecognition
{
    public class TextureClassDuplicateException : Exception
    {
        public TextureClassDuplicateException(string message) :
            base(message)
        {
        }
    }

    internal class SplitImageThreadParameter
    {
        public SplitImageThreadParameter(SimpleImage image, int from, int to, List<Rectangle> regions,
            List<SubImageSample> samples, ManualResetEvent resetEvent)
        {
            Image = image;
            From = from;
            To = to;
            ResultRegions = regions;
            ResultSamples = samples;
            ResetEvent = resetEvent;
        }

        public SimpleImage Image
        {
            get;
            set;
        }

        public int From
        {
            get;
            set;
        }

        public int To
        {
            get;
            set;
        }

        public List<Rectangle> ResultRegions
        {
            get;
            set;
        }

        public List<SubImageSample> ResultSamples
        {
            get;
            set;
        }

        public ManualResetEvent ResetEvent
        {
            get;
            set;
        }
    }

    public class RecognitionCore
    {
        private Random random;
        private double recognitionProgress;
        private bool isRecognizing;
        List<TextureClass> knownClasses;

        public RecognitionCore()
        {
            random = new Random();
            knownClasses = new List<TextureClass>();
        }

        public void LoadKnowledges(string url)
        {

        }

        public void SaveKnowledges(string url)
        {

        }

        public TextureClass AddTextureClass(string name, Color color)
        {
            if (knownClasses.Count((TextureClass item) => item.Name == name) > 0)
            {
                throw new TextureClassDuplicateException("Класс с таким именем уже существует");
            }
            var result = new TextureClass(name, color);
            knownClasses.Add(result);
            return result;
        }

        public void RemoveTextureClass(string name)
        {
            knownClasses.RemoveAll((TextureClass item) => item.Name == name);
        }

        public void RemoveAllTextureClasses()
        {
            knownClasses.Clear();
        }

        public Image RecognizeImage(string imageUrl, TextureFeatures usedFeatures)
        {
            isRecognizing = true;
            recognitionProgress = 0;
            
            var image = new SimpleImage(imageUrl);
            recognitionProgress = 0.05;
            
            var result = image.Image;
            var regions = new List<Rectangle>();
            var samples = new List<SubImageSample>();
            var matches = new List<TextureClass[]>();
            
            SplitImage(image, samples, regions);
            recognitionProgress = 0.95;
            
            var render = Graphics.FromImage(result);
            for (int i = 0; i < samples.Count; ++i)
            {
                recognitionProgress = 0.95 + 0.05 * i / (double)samples.Count;
                var match = GetBestMatch(samples[i], usedFeatures);
                if (match != null)
                {
                    var color = Color.FromArgb(100, match.RegionColor.R, match.RegionColor.G, match.RegionColor.B);
                    render.FillRectangle(new SolidBrush(color), regions[i]); 
                }
            }
            
            recognitionProgress = 1;
            isRecognizing = false;
            return result;
        }

        public Image RecognizeImage(string imageUrl, Rectangle region, TextureFeatures usedFeatures, out string answer)
        {
            isRecognizing = true;
            recognitionProgress = 0;
            var image = new SimpleImage(imageUrl);
            recognitionProgress = 0.33;
            answer = "";
            var result = new Bitmap(image.Image);
            var imageData = image.GetGrayData(
                region.X, region.Y, 
                region.X + region.Width, region.Y + region.Height);
            var sample = new SubImageSample(imageData);
            var render = Graphics.FromImage(result);
            recognitionProgress = 0.66;
            var match = GetBestMatch(sample, usedFeatures);
            if (match != null)
            {
                var color = Color.FromArgb(100, match.RegionColor.R, match.RegionColor.G, match.RegionColor.B);
                render.FillRectangle(new SolidBrush(color), region);
                answer = match.Name;
            }
            recognitionProgress = 1;
            isRecognizing = false;
            return result;
        }

        private void SplitImage(SimpleImage image, List<SubImageSample> samples, List<Rectangle> regions)
        {
            int YCount = image.Height / RecognitionParameters.FragmentsSize;

            int threadCount = Environment.ProcessorCount * 3 / 2;
            int threadPart = YCount / threadCount;
            var resetEvents = new ManualResetEvent[threadCount + 1];
            var threadParameters = new SplitImageThreadParameter[threadCount + 1];
            for (int i = 0; i < threadCount; ++i)
            {
                resetEvents[i] = new ManualResetEvent(false);
                threadParameters[i] = new SplitImageThreadParameter(
                    image, threadPart * i, ((i + 1) < threadCount) ? (threadPart * (i + 1)) : (YCount),
                    regions, samples, resetEvents[i]);
                ThreadPool.QueueUserWorkItem(new WaitCallback(SplitImageThread), threadParameters[i]);
            }
            resetEvents[threadCount] = new ManualResetEvent(false);
            threadParameters[threadCount] = new SplitImageThreadParameter(
                image, 0, 0, regions, samples, resetEvents[threadCount]);
            ThreadPool.QueueUserWorkItem(new WaitCallback(SplitImageAdditionalThread), threadParameters[threadCount]);
            WaitHandle.WaitAll(resetEvents);
        }

        private void SplitImageThread(object parameter)
        {
            SplitImageThreadParameter input = parameter as SplitImageThreadParameter;
            if (input == null)
            {
                throw new ArgumentException("Неправильный вормат потока");
            }

            int fragmentSize = RecognitionParameters.FragmentsSize;
            var image = input.Image;
            var regions = input.ResultRegions;
            var samples = input.ResultSamples;
            int XCount = image.Width / fragmentSize;
            int YCount = image.Height / fragmentSize;

            for (int y = input.From; y < input.To; ++y)
            {
                for (int x = 0; x < XCount; ++x)
                {
                    int fromX = x * fragmentSize;
                    int fromY = y * fragmentSize;
                    int toX = fromX + fragmentSize;
                    int toY = fromY + fragmentSize;
                    var imageData = image.GetGrayData(fromX, fromY, toX, toY);
                    var sample = new SubImageSample(imageData);
                    lock ((regions as ICollection).SyncRoot)
                    {
                        regions.Add(new Rectangle(fromX, fromY, fragmentSize, fragmentSize));
                        samples.Add(sample);
                        recognitionProgress += 0.9 / (XCount * YCount);
                    }
                }
            }

            input.ResetEvent.Set();
        }

        private void SplitImageAdditionalThread(object parameter)
        {
            SplitImageThreadParameter input = parameter as SplitImageThreadParameter;
            if (input == null)
            {
                throw new ArgumentException("Неправильный вормат потока");
            }

            int fragmentSize = RecognitionParameters.FragmentsSize;
            var image = input.Image;
            var regions = input.ResultRegions;
            var samples = input.ResultSamples;
            int XCount = image.Width / fragmentSize;
            int YCount = image.Height / fragmentSize;

            if (XCount * fragmentSize < image.Width)
            {
                for (int y = 0; y < YCount; ++y)
                {
                    int fromX = image.Width - fragmentSize;
                    int fromY = y * fragmentSize;
                    int toX = image.Width;
                    int toY = fromY + fragmentSize;
                    var imageData = image.GetGrayData(fromX, fromY, toX, toY);
                    var sample = new SubImageSample(imageData);
                    lock ((regions as ICollection).SyncRoot)
                    {
                        regions.Add(new Rectangle(fromX, fromY, fragmentSize, fragmentSize));
                        samples.Add(sample);
                    }
                }
            }

            if (YCount * fragmentSize < image.Height)
            {
                for (int x = 0; x < XCount; ++x)
                {
                    int fromX = x * fragmentSize;
                    int fromY = image.Height - fragmentSize;
                    int toX = fromX + fragmentSize;
                    int toY = image.Height;
                    var imageData = image.GetGrayData(fromX, fromY, toX, toY);
                    var sample = new SubImageSample(imageData);
                    lock ((regions as ICollection).SyncRoot)
                    {
                        regions.Add(new Rectangle(fromX, fromY, fragmentSize, fragmentSize));
                        samples.Add(sample);
                    }
                }
            }

            if ((XCount * fragmentSize < image.Width) || (YCount * fragmentSize < image.Height))
            {
                int fromX = image.Width - fragmentSize;
                int fromY = image.Height - fragmentSize;
                int toX = image.Width;
                int toY = image.Height;
                var imageData = image.GetGrayData(fromX, fromY, toX, toY);
                var sample = new SubImageSample(imageData);
                lock ((regions as ICollection).SyncRoot)
                {
                    regions.Add(new Rectangle(fromX, fromY, fragmentSize, fragmentSize));
                    samples.Add(sample);
                }
            }

            input.ResetEvent.Set();
        }

        private TextureClass GetBestMatch(SubImageSample sample, TextureFeatures usedFeatures)
        {
            var answers = new List<TextureClass>();
            if (IsGLCMFeature(usedFeatures))
            {
                answers.Add(GetBestMatchForFeature(sample, TextureFeatures.GLCM));
            }
            if (IsLBPFeature(usedFeatures))
            {
                answers.Add(GetBestMatchForFeature(sample, TextureFeatures.LBP));
            }
            /*
            if (IsTravellingWavesFeature(usedFeatures))
            {
                answers.Add(GetBestMatchForFeature(sample, TextureFeatures.TravellingWaves));
            }
            */
            var rindex = 0;
            switch (answers.Count)
            {
                case 1:
                    return answers[0];

                case 2:
                    if (answers[0] != answers[1])
                    {
                        rindex = (random.Next(11) > 5) ? (0) : (1);
                        return answers[rindex];
                    }
                    else
                    {
                        return answers[0];
                    }

                case 3:
                    var counts = new int[3] { 
                        answers.Count((TextureClass item) => item == answers[0]),
                        answers.Count((TextureClass item) => item == answers[1]),
                        answers.Count((TextureClass item) => item == answers[2])};
                    int max = 0, index = -1;
                    for (int i = 0; i < 3; ++i)
                    {
                        if (counts[i] > max)
                        {
                            max = counts[i];
                            index = i;
                        }
                    }
                    if (max > 1)
                    {
                        return answers[index];
                    }
                    else
                    {
                        rindex = random.Next(0, 2);
                        return answers[rindex];
                    }
            }
            return null;
        }

        private TextureClass[] GetMatches(SubImageSample sample)
        {
            var result = new TextureClass[] {
                GetBestMatchForFeature(sample, TextureFeatures.GLCM),
                GetBestMatchForFeature(sample, TextureFeatures.LBP)};
            return result;
        }

        private TextureClass GetBestMatchForFeature(SubImageSample sample, TextureFeatures feature)
        {
            var distancesList = GetDistancesListForFeature(sample, feature);
            var indexesList = new int[TextureClassCount];
            var classesList = new List<int>();
            var classesFlags = new List<int>();
            
            while (true)
            {
                int bestClass = GetBest(distancesList, indexesList, classesList, classesFlags);
                indexesList[bestClass] += 1;

                if (indexesList[bestClass] == knownClasses[bestClass].KnownSamplesNumber)
                {
                    classesFlags.Add(bestClass);
                    if (classesFlags.Count == knownClasses.Count)
                    {
                        return knownClasses[classesFlags[0]];
                    }
                }
                if (indexesList[bestClass] == RecognitionParameters.NeededNeighborsNumber)
                //if ((classesList.Count == RecognitionParameters.NeededNeighborsNumber) ||
                //    (classesFlags.Count == TextureClassCount))
                {
                    //return knownClasses[GetBestClass(classesList)];
                    return knownClasses[bestClass];
                }
            }
        }

        private int GetBestClass(List<int> classes)
        {
            var matches = new int[TextureClassCount];
            foreach (var item in classes)
            {
                matches[item] += 1;
            }
            int max = matches[0], index = 0;
            for (int i = 1; i < TextureClassCount; ++i)
            {
                if (matches[i] > max)
                {
                    max = matches[i];
                    index = i;
                }
            }
            return index;
        }

        private int GetBest(List<double>[] distances, int[] indexes, List<int> classes, List<int> flags)
        {
            double best = double.MaxValue;
            int bestClass = -1;
            for (int i = 0; i < TextureClassCount; ++i)
            {
                if (flags.Contains(i))
                {
                    continue;
                }
                if (distances[i][indexes[i]] < best)
                {
                    best = distances[i][indexes[i]];
                    bestClass = i;
                }
            }
            classes.Add(bestClass);
            return bestClass;
        }

        private List<double>[] GetDistancesListForFeature(SubImageSample sample, TextureFeatures feature)
        {
            var distancesList = new List<double>[TextureClassCount];
            for (int i = 0; i < TextureClassCount; ++i)
            {
                distancesList[i] = knownClasses[i].GetSortedDistances(sample, feature);
            }
            return distancesList;
        }

        private bool IsGLCMFeature(TextureFeatures usedFeatures)
        {
            return (usedFeatures & TextureFeatures.GLCM) == TextureFeatures.GLCM;
        }

        private bool IsLBPFeature(TextureFeatures usedFeatures)
        {
            return (usedFeatures & TextureFeatures.LBP) == TextureFeatures.LBP;
        }

        public TextureClass GetTextureClass(string name)
        {
            return knownClasses.Find((TextureClass item) => item.Name == name);
        }

        public TextureClass GetTextureClass(int index)
        {
            if ((index < 0) || (index >= knownClasses.Count))
            {
                return null;
            }
            return knownClasses[index];
        }

        public int GetTextureClassIndex(TextureClass textureClass)
        {
            return knownClasses.IndexOf(textureClass);
        }

        public int TextureClassCount
        {
            get
            {
                return knownClasses.Count;
            }
        }

        public double RecognitionProgress
        {
            get
            {
                return recognitionProgress;
            }
        }

        public bool IsRecognizing
        {
            get
            {
                return isRecognizing;
            }
        }
    }
}
