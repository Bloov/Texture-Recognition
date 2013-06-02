using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
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
            List<TextureSample> samples, ManualResetEvent resetEvent)
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

        public List<TextureSample> ResultSamples
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
        private double recognitionProgress;
        private bool isRecognizing;
        private bool isRecognizingAborted;
        List<TextureClass> knownClasses;

        public RecognitionCore()
        {
            knownClasses = new List<TextureClass>();
        }

        public void LoadKnowledges(string url)
        {
            RemoveAllTextureClasses();
            var xml = new XmlDocument();
            xml.Load(url);
            var classes = xml.GetElementsByTagName("class");
            foreach (XmlNode item in classes)
            {
                var textureClass = new TextureClass("Texture", Color.Red);
                textureClass.LoadKnowledges(item);
                knownClasses.Add(textureClass);
            }
        }

        public void SaveKnowledges(string url)
        {
            var xml = new XmlDocument();
            xml.CreateProcessingInstruction("xml", @"version=""1.0"" encoding=""WINDOWS-1251""");
            var classes = xml.CreateElement("classes");
            foreach (var item in knownClasses)
            {
                item.SaveKnowledges(classes, xml); 
            }
            xml.AppendChild(classes);
            xml.Save(url);
        }

        public TextureClass AddTextureClass(string name, Color color)
        {
            if (knownClasses.Count(item => item.Name == name) > 0)
            {
                throw new TextureClassDuplicateException("Класс с таким именем уже существует");
            }

            var result = new TextureClass(name, color);
            knownClasses.Add(result);
            return result;
        }

        public void RemoveTextureClass(string name)
        {
            knownClasses.RemoveAll(item => item.Name == name);
        }

        public void RemoveAllTextureClasses()
        {
            knownClasses.Clear();
        }

        public List<RecognitionResult> RecognizeImage(Bitmap image)
        {
            recognitionProgress = 0;
            isRecognizing = true;

            var imageToRecognize = new SimpleImage(image);
            recognitionProgress = 0.05;

            var regions = new List<Rectangle>();
            var samples = new List<TextureSample>();
            SplitImage(imageToRecognize, samples, regions);

            var matches = new List<RecognitionResult>();
            for (int i = 0; i < samples.Count; ++i)
            {
                var answer = GetBestMatch(samples[i], regions[i]);
                matches.Add(answer);
                recognitionProgress = 0.95 + (0.05 * i) / samples.Count;
            }
            
            recognitionProgress = 1;
            isRecognizing = false;
            return matches;
        }

        public RecognitionResult RecognizeImage(Bitmap image, Rectangle region)
        {
            recognitionProgress = 0;
            isRecognizing = true;
            
            var imageToRecognize = new SimpleImage(image);
            recognitionProgress = 0.2;
            
            var imageData = imageToRecognize.GetGrayData(region.X, region.Y, 
                region.X + region.Width, region.Y + region.Height);
            var sample = new TextureSample(imageData, false);
            recognitionProgress = 0.8;

            var answer = GetBestMatch(sample, region);

            recognitionProgress = 1;
            isRecognizing = false;
            return answer;
        }

        public List<RecognitionResult> RecognizeImages(List<Bitmap> images)
        {
            recognitionProgress = 0;
            isRecognizing = true;

            var answers = new List<RecognitionResult>();
            for (int i = 0; i < images.Count; ++i)
            {
                var imageToRecognize = new SimpleImage(images[i]);
                var imageData = imageToRecognize.GetGrayData(0, 0, imageToRecognize.Width, imageToRecognize.Height);
                var sample = new TextureSample(imageData, false);
                answers.Add(GetBestMatch(sample, new Rectangle(0, 0, imageData.Width, imageData.Height)));
                recognitionProgress += 1.0 * i / images.Count;
            }

            recognitionProgress = 1;
            isRecognizing = false;
            return answers;
        }

        private void SplitImage(SimpleImage image, List<TextureSample> samples, List<Rectangle> regions)
        {
            int YCount = image.Height / RecognitionParameters.RecognitionFragmentSize;
            int threadCount = Math.Min(YCount, Environment.ProcessorCount * 2);
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
                throw new ArgumentException("Неправильный формат потока");
            }

            int fragmentSize = RecognitionParameters.RecognitionFragmentSize;
            var image = input.Image;
            var regions = input.ResultRegions;
            var samples = input.ResultSamples;
            int XCount = image.Width / fragmentSize;
            int YCount = image.Height / fragmentSize;

            for (int y = input.From; (y < input.To) && (!isRecognizingAborted); ++y)
            {
                for (int x = 0; (x < XCount) && (!isRecognizingAborted); ++x)
                {
                    int fromX = x * fragmentSize;
                    int fromY = y * fragmentSize;
                    int toX = fromX + fragmentSize;
                    int toY = fromY + fragmentSize;
                    TextureSample sample = null;
                    try
                    {
                        var imageData = image.GetGrayData(fromX, fromY, toX, toY);
                        sample = new TextureSample(imageData, false);
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("Возникла ошибка. Продолжать?\n" + ex.Message, "Внимание", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            isRecognizingAborted = true;
                        }
                        else
                        {
                            continue;
                        }
                    }
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
                throw new ArgumentException("Неправильный формат потока");
            }

            int fragmentSize = RecognitionParameters.RecognitionFragmentSize;
            var image = input.Image;
            var regions = input.ResultRegions;
            var samples = input.ResultSamples;
            int XCount = image.Width / fragmentSize;
            int YCount = image.Height / fragmentSize;

            if (XCount * fragmentSize < image.Width)
            {
                for (int y = 0; (y < YCount) && (!isRecognizingAborted); ++y)
                {
                    int fromX = image.Width - fragmentSize;
                    int fromY = y * fragmentSize;
                    int toX = image.Width;
                    int toY = fromY + fragmentSize;
                    TextureSample sample = null;
                    try
                    {
                        var imageData = image.GetGrayData(fromX, fromY, toX, toY);
                        sample = new TextureSample(imageData, false);
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("Возникла ошибка. Продолжать?\n" + ex.Message, "Внимание", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            isRecognizingAborted = true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    lock ((regions as ICollection).SyncRoot)
                    {
                        regions.Add(new Rectangle(fromX, fromY, fragmentSize, fragmentSize));
                        samples.Add(sample);
                    }
                }
            }

            if (YCount * fragmentSize < image.Height)
            {
                for (int x = 0; (x < XCount) && (!isRecognizingAborted); ++x)
                {
                    int fromX = x * fragmentSize;
                    int fromY = image.Height - fragmentSize;
                    int toX = fromX + fragmentSize;
                    int toY = image.Height;
                    TextureSample sample = null;
                    try
                    {
                        var imageData = image.GetGrayData(fromX, fromY, toX, toY);
                        sample = new TextureSample(imageData, false);
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("Возникла ошибка. Продолжать?\n" + ex.Message, "Внимание", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            isRecognizingAborted = true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    lock ((regions as ICollection).SyncRoot)
                    {
                        regions.Add(new Rectangle(fromX, fromY, fragmentSize, fragmentSize));
                        samples.Add(sample);
                    }
                }
            }

            if ((XCount * fragmentSize < image.Width) || (YCount * fragmentSize < image.Height) && (!isRecognizingAborted))
            {
                int fromX = image.Width - fragmentSize;
                int fromY = image.Height - fragmentSize;
                int toX = image.Width;
                int toY = image.Height;
                TextureSample sample = null;
                try
                {
                    var imageData = image.GetGrayData(fromX, fromY, toX, toY);
                    sample = new TextureSample(imageData, false);
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("Возникла ошибка. Продолжать?\n" + ex.Message, "Внимание", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        isRecognizingAborted = true;
                    }
                }
                lock ((regions as ICollection).SyncRoot)
                {
                    regions.Add(new Rectangle(fromX, fromY, fragmentSize, fragmentSize));
                    samples.Add(sample);
                }
            }

            input.ResetEvent.Set();
        }

        private RecognitionResult GetBestMatch(TextureSample sample, Rectangle region)
        {
            var answer = new RecognitionResult(region, sample);
            answer.SetAnswer(TextureFeatures.GLCM, GetBestMatchForFeature(sample, TextureFeatures.GLCM));
            answer.SetAnswer(TextureFeatures.LBP, GetBestMatchForFeature(sample, TextureFeatures.LBP));
            return answer;
        }

        private TextureClass GetBestMatchForFeature(TextureSample sample, TextureFeatures feature)
        {
            var distancesList = GetDistancesListForFeature(sample, feature);
            var indexesList = new int[TextureClassCount];
            var classesList = new List<int>();
            var classesFlags = new List<int>();
            
            while (true)
            {
                int bestClass = GetBestDistance(distancesList, indexesList, classesList, classesFlags);
                indexesList[bestClass] += 1;

                if (indexesList[bestClass] == knownClasses[bestClass].KnownSamplesNumber(feature))
                {
                    classesFlags.Add(bestClass);
                    if (classesFlags.Count == knownClasses.Count)
                    {
                        return knownClasses[classesFlags[0]];
                    }
                }
                if (indexesList[bestClass] == RecognitionParameters.NeededNeighborsNumber)
                {
                    return knownClasses[bestClass];
                }
            }
        }

        private List<double>[] GetDistancesListForFeature(TextureSample sample, TextureFeatures feature)
        {
            var distancesList = new List<double>[TextureClassCount];
            for (int i = 0; i < TextureClassCount; ++i)
            {
                distancesList[i] = knownClasses[i].GetSortedDistances(sample, feature);
            }
            return distancesList;
        }

        private int GetBestDistance(List<double>[] distances, int[] indexes, List<int> classes, List<int> flags)
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
