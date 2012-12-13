using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
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
            for (int i = 0; i < featuresCount; ++i)
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

        public int KnownSamplesNumber
        {
            get
            {
                return featuresCount;
            }
        }
    }
}
