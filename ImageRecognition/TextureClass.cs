using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using ImageProcessing;

namespace ImageRecognition
{
    public enum TextureFeatures
    {
        GLCM = 0x01, 
        LBP = 0x02,
        TravellingWaves = 0x04
    }

    public class TextureClass
    {
        private double teachProgress;
        private bool isTeaching;
        private List<string> urls;

        private int featuresCount;
        private List<string> knownFiles;
        private List<GLCMFeature> glcmFeatures;
        private List<LBPFeature> lbpFeatures;

        public TextureClass(string name, Color regionColor)
        {
            Name = name;
            RegionColor = regionColor;
            urls = null;
            isTeaching = false;

            knownFiles = new List<string>();
            glcmFeatures = new List<GLCMFeature>();
            lbpFeatures = new List<LBPFeature>();
        }

        public void Teach(List<string> urls)
        {
            isTeaching = true;
            teachProgress = 0;
            this.urls = new List<string>(urls);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TeachThread));
        }

        private void TeachThread(object parameter)
        {
            if (urls == null)
            {
                return;
            }

            double delta = 1 / (5.0 * urls.Count);
            foreach (var item in urls)
            {
                SimpleImage image;
                try
                {
                    if (knownFiles.Exists((string name) => name == item))
                    {
                        teachProgress += 5 * delta;
                        continue;
                    }

                    image = new SimpleImage(item);
                    teachProgress += delta;
                }
                catch (Exception ex)
                {
                    teachProgress += 5 * delta;
                    continue;
                }

                try
                {
                    LBPCreator lbp = new LBPCreator(image);
                    GLCMCreator glcm = new GLCMCreator(image);
                    AddFeatures(glcm.Feature, lbp.Feature, item);
                }
                catch (Exception ex)
                {
                    continue;
                }
                finally
                {
                    teachProgress += 4 * delta;
                }
            }

            isTeaching = false;
        }

        public void RemoveSample(int index)
        {
            if ((index < 0) || (index >= featuresCount))
            {
                return;
            }

            knownFiles.RemoveAt(index);
            glcmFeatures.RemoveAt(index);
            lbpFeatures.RemoveAt(index);
            --featuresCount;
        }

        private void AddFeatures(GLCMFeature glcm, LBPFeature lbp, string path)
        {
            ++featuresCount;
            glcmFeatures.Add(glcm);
            lbpFeatures.Add(lbp);
            knownFiles.Add(path);
        }

        public List<double> GetSortedDistances(SubImageSample sample, TextureFeatures feature)
        {
            var list = new List<double>();
            for (int i = 0; i < featuresCount; ++i)
            {
                list.Add(GetDistance(sample, i, feature));
            }
            list.Sort();
            return list;
        }

        private double GetDistance(SubImageSample sample, int featureIndex, TextureFeatures feature)
        {
            switch (feature)
            {
                case TextureFeatures.GLCM:
                    return sample.GLCM.GetDistance(glcmFeatures[featureIndex]);
                case TextureFeatures.LBP:
                    return sample.LBP.GetDistance(lbpFeatures[featureIndex]);
                case TextureFeatures.TravellingWaves:
                    return 0;
            }
            return 0;
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

        public string GetKnownSample(int index)
        {
            if ((index < 0) || (index >= knownFiles.Count))
            {
                return "";
            }
            return knownFiles[index];
        }
    }
}
