using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Xml;
using ImageProcessing;

namespace ImageRecognition
{
    internal class TeachThreadParameter
    {
        public TeachThreadParameter(int from, int to, Barrier barrier)
        {
            From = from;
            To = to;
            CommonBarrier = barrier;
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

        public Barrier CommonBarrier
        {
            get;
            set;
        }
    }

    public class TextureClass
    {
        private double teachProgress;
        private bool isTeaching;
        private bool isTeachingAborted;
        private List<string> filesToTeach;
        private List<Bitmap> imagesToTeach;

        private int featuresCount;
        private List<string> knownFiles;
        private List<GLCMFeature> glcmFeatures;
        private List<LBPFeature> lbpFeatures;

        public TextureClass(string name, Color regionColor)
        {
            Name = name;
            RegionColor = regionColor;

            knownFiles = new List<string>();
            glcmFeatures = new List<GLCMFeature>();
            lbpFeatures = new List<LBPFeature>();
        }

        internal void SaveKnowledges(XmlElement element, XmlDocument parent)
        {
            var data = parent.CreateElement("class");
            var idAttribute = parent.CreateAttribute("id");
            idAttribute.Value = element.ChildNodes.Count.ToString();
            var nameAttribute = parent.CreateAttribute("name");
            nameAttribute.Value = Name;
            var rAttribute = parent.CreateAttribute("R");
            rAttribute.Value = RegionColor.R.ToString();
            var gAttribute = parent.CreateAttribute("G");
            gAttribute.Value = RegionColor.G.ToString();
            var bAttribute = parent.CreateAttribute("B");
            bAttribute.Value = RegionColor.B.ToString();
            data.Attributes.Append(idAttribute);
            data.Attributes.Append(nameAttribute);
            data.Attributes.Append(rAttribute);
            data.Attributes.Append(gAttribute);
            data.Attributes.Append(bAttribute);
            for (int i = 0; i < featuresCount; ++i)
            {
                var point = parent.CreateElement("sample");
                var file = parent.CreateElement("file");
                var path = parent.CreateAttribute("path");
                path.Value = knownFiles[i];
                file.Attributes.Append(path);
                point.AppendChild(file);
                glcmFeatures[i].SaveKnowledges(point, parent);
                lbpFeatures[i].SaveKnowledges(point, parent);
                data.AppendChild(point);
            }
            element.AppendChild(data);
        }

        internal void LoadKnowledges(XmlNode node)
        {
            Name = node.Attributes["name"].Value;
            int r, g, b;
            int.TryParse(node.Attributes["R"].Value, out r);
            int.TryParse(node.Attributes["G"].Value, out g);
            int.TryParse(node.Attributes["B"].Value, out b);
            RegionColor = Color.FromArgb(r, g, b);
            var samples = node.ChildNodes;
            foreach (XmlNode item in samples)
            {
                var file = item.ChildNodes[0].Attributes["path"].Value;
                var glcm = new GLCMFeature();
                glcm.LoadKnowledges(item.ChildNodes[1]);
                var lbp = new LBPFeature();
                lbp.LoadKnowledges(item.ChildNodes[2]);
                AddFeatures(glcm, lbp, file);
            }
        }

        public void Teach(List<string> files, List<Bitmap> images)
        {
            if ((files == null) || (images == null) || (files.Count == 0) || (files.Count != images.Count))
            {
                MessageBox.Show("Неправельный набор данных для обучения.");
                return;
            }

            isTeachingAborted = false;
            isTeaching = true;
            teachProgress = 0;
            filesToTeach = files;
            imagesToTeach = images;
            StartTeachingThreads();            
        }

        public void TeachCompact(List<string> files, List<Bitmap> images)
        {
            if ((files == null) || (images == null) || (files.Count == 0) || (files.Count != images.Count))
            {
                MessageBox.Show("Неправельный набор данных для обучения.");
                return;
            }

            isTeachingAborted = false;
            isTeaching = true;
            teachProgress = 0;
            filesToTeach = files;
            imagesToTeach = images;
            StartTeachingThreads();

            while (isTeaching)
            {
                Thread.Sleep(200);
            }

            FormsCompact();
        }

        public void TeachDischarged(List<string> files, List<Bitmap> images)
        {
            if ((files == null) || (images == null) || (files.Count == 0) || (files.Count != images.Count))
            {
                MessageBox.Show("Неправельный набор данных для обучения.");
                return;
            }

            isTeachingAborted = false;
            isTeaching = true;
            teachProgress = 0;
            filesToTeach = files;
            imagesToTeach = images;
            StartTeachingThreads(); 
            
            while (isTeaching)
            {
                Thread.Sleep(200);
            }

            FormsDischarged();
        }

        private void StartTeachingThreads()
        {
            int threadCount = Math.Min(imagesToTeach.Count, 2 * Environment.ProcessorCount);
            int threadPart = imagesToTeach.Count / threadCount;
            var barrier = new Barrier(threadCount, item => isTeaching = false);
            var threadParameters = new TeachThreadParameter[threadCount];
            for (int i = 0; i < threadCount; ++i)
            {
                threadParameters[i] = new TeachThreadParameter(
                    threadPart * i, ((i + 1) < threadCount) ? (threadPart * (i + 1)) : (imagesToTeach.Count),
                    barrier);
                ThreadPool.QueueUserWorkItem(new WaitCallback(TeachThread), threadParameters[i]);
            }
        }

        public void AborTeaching()
        {
            isTeachingAborted = true;
        }

        private void TeachThread(object parameter)
        {
            TeachThreadParameter input = parameter as TeachThreadParameter;
            if (input == null)
            {
                throw new ArgumentNullException("Неправильный параметр потока.");
            }

            var delta = 1.0 / filesToTeach.Count;
            for (int i = input.From; (i < input.To) && (!isTeachingAborted); ++i)
            {
                try
                {
                    var image = new SimpleImage(imagesToTeach[i]);
                    var imageData = image.GetGrayData();
                    var lbp = new LBPCreator(imageData, true, true, false);
                    var glcm = new GLCMCreator(imageData, true, true, false);
                    AddFeatures(glcm.Feature, lbp.Feature, filesToTeach[i]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    continue;
                }
                finally
                {
                    lock ((knownFiles as ICollection).SyncRoot)
                    {
                        teachProgress += delta;
                    }
                }
            }
            input.CommonBarrier.SignalAndWait();
        }

        private void AddFeatures(GLCMFeature glcm, LBPFeature lbp, string path)
        {
            lock ((knownFiles as ICollection).SyncRoot)
            {
                ++featuresCount;
                glcmFeatures.Add(glcm);
                lbpFeatures.Add(lbp);
                knownFiles.Add(path);
            }
        }

        internal List<double> GetSortedDistances(TextureSample sample, TextureFeatures feature)
        {
            var list = new List<double>();
            var count = KnownSamplesNumber(feature);
            for (int i = 0; i < count; ++i)
            {
                list.Add(GetDistance(sample, i, feature));
            }
            list.Sort();
            return list;
        }

        private double GetDistance(TextureSample sample, int featureIndex, TextureFeatures feature)
        {
            switch (feature)
            {
                case TextureFeatures.GLCM:
                    return sample.GLCM.GetDistance(glcmFeatures[featureIndex]);
                case TextureFeatures.LBP:
                    return sample.LBP.GetDistance(lbpFeatures[featureIndex]);
                default:
                    return 0;
            }
        }

        public void RemoveAllSamples()
        {
            if (isTeaching)
            {
                return;
            }
            
            knownFiles.Clear();
            glcmFeatures.Clear();
            lbpFeatures.Clear();
            featuresCount = 0;
        }

        public void RemoveSample(string file)
        {
            if (isTeaching)
            {
                return;
            }

            var index = knownFiles.IndexOf(file);
            if (index >= 0)
            {
                knownFiles.RemoveAt(index);
                glcmFeatures.RemoveAt(index);
                lbpFeatures.RemoveAt(index);
                --featuresCount;
            }
        }

        public string GetKnownSample(int index)
        {
            if ((index < 0) || (index >= knownFiles.Count))
            {
                return "";
            }
            return knownFiles[index];
        }

        public bool IsKnowSample(string sample)
        {
            return knownFiles.Exists(item => item == sample);
        }

        public bool IsTeaching
        {
            get
            {
                return isTeaching;
            }
        }

        public double TeachingProgress
        {
            get
            {
                return teachProgress;
            }
        }

        public string Name
        {
            get;
            set;
        }

        public Color RegionColor
        {
            get;
            set;
        }

        public int KnownSamplesNumber(TextureFeatures feature)
        {
            switch (feature)
            {
                case TextureFeatures.GLCM:
                    return glcmFeatures.Count();
                case TextureFeatures.LBP:
                    return lbpFeatures.Count;
                default:
                    return 0;
            }            
        }

        public GLCMFeature PrepareGLCM(out double average, out double variance)
        {
            var gStandart = GLCMFeature.BuildStandart(glcmFeatures);
            var gDistances = new double[glcmFeatures.Count];
            for (int i = 0; i < glcmFeatures.Count; ++i)
            {
                gDistances[i] = gStandart.GetDistance(glcmFeatures[i]);
            }
            
            average = MathHelpers.GetAverage(gDistances, 0, glcmFeatures.Count);
            variance = MathHelpers.GetVariance(gDistances, average, 0, glcmFeatures.Count);

            return gStandart;
        }

        public LBPFeature PrepareLBP(out double average, out double variance)
        {
            var lStandart = LBPFeature.BuildStandart(lbpFeatures);
            var lDistances = new double[lbpFeatures.Count];

            for (int i = 0; i < lbpFeatures.Count; ++i)
            {
                lDistances[i] = lStandart.GetDistance(lbpFeatures[i]);
            }

            average = MathHelpers.GetAverage(lDistances, 0, lbpFeatures.Count);
            variance = MathHelpers.GetVariance(lDistances, average, 0, lbpFeatures.Count);

            return lStandart;
        }

        public void FormsCompact()
        {
            double gAverage, gVariance, lAverage, lVariance;
            var glcmStandart = PrepareGLCM(out gAverage, out gVariance);
            var lbpStandart = PrepareLBP(out lAverage, out lVariance);
        }

        public void FormsDischarged()
        {
            double gAverage, gVariance, lAverage, lVariance;
            var glcmStandart = PrepareGLCM(out gAverage, out gVariance);
            var lbpStandart = PrepareLBP(out lAverage, out lVariance);
        }
    }
}
