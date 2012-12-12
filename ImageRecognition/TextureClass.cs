using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using ImageProcessing;

namespace ImageRecognition
{
    public enum TextureFeatures
    {
        GLCM = 0x01, 
        LBP = 0x02
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
            if ((files == null) || (files.Count == 0) || (files.Count != images.Count))
            {
                MessageBox.Show("Неправельный набор данных для обучения.");
                return;
            }

            isTeachingAborted = false;
            isTeaching = true;
            teachProgress = 0;
            filesToTeach = files;
            imagesToTeach = images;
            ThreadPool.QueueUserWorkItem(new WaitCallback(TeachThread));
        }

        public void AborTeaching()
        {
            isTeachingAborted = true;
        }

        private void TeachThread(object parameter)
        {
            double delta = 1 / (5.0 * filesToTeach.Count);
            for (int i = 0; i < filesToTeach.Count; ++i)
            {
                SimpleImage image;
                ImageGrayData imageData;
                try
                {
                    image = new SimpleImage(imagesToTeach[i]);
                    imageData = image.GetGrayData();
                    teachProgress += delta;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    teachProgress += 5 * delta;
                    continue;
                }

                if (isTeachingAborted)
                {
                    break;
                }

                try
                {
                    LBPCreator lbp = new LBPCreator(imageData);
                    GLCMCreator glcm = new GLCMCreator(imageData);
                    AddFeatures(glcm.Feature, lbp.Feature, filesToTeach[i]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    continue;
                }
                finally
                {
                    teachProgress += 4 * delta;
                }

                if (isTeachingAborted)
                {
                    break;
                }
            }

            isTeaching = false;
        }

        private void AddFeatures(GLCMFeature glcm, LBPFeature lbp, string path)
        {
            ++featuresCount;
            glcmFeatures.Add(glcm);
            lbpFeatures.Add(lbp);
            knownFiles.Add(path);
        }

        internal List<double> GetSortedDistances(SubImageSample sample, TextureFeatures feature)
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
                default:
                    return 0;
            }
        }

        public void RemoveAllSamples()
        {
            knownFiles.Clear();
            glcmFeatures.Clear();
            lbpFeatures.Clear();
            featuresCount = 0;
        }

        public void RemoveSample(string file)
        {
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
