using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ImageProcessing;

namespace ImageRecognition
{
    public sealed class GLCMFeature
    {
        internal double[] sourceData;
        private double[] feature;

        internal GLCMFeature(double[] data)
        {
            sourceData = data;
            feature = new double[8];
            for (int i = 0; i < 4; ++i)
            {
                feature[i * 2] = MathHelpers.GetAverage(sourceData, i * 4, 4);
                feature[i * 2 + 1] = Math.Sqrt(MathHelpers.GetVariance(sourceData, feature[i * 2], i * 4, 4));
            }
        }

        public double this[int index]
        {
            get
            {
                if ((index < 0) || (index >= 8))
                {
                    return 0;
                }
                return feature[index];
            }
        }

        public double GetDistance(GLCMFeature other)
        {
            double result = 0;
            for (int i = 0; i < 8; ++i)
            {
                result += MathHelpers.Sqr(feature[i] - other[i]);
            }
            return Math.Sqrt(result);
        }

        public static GLCMFeature BuildStandart(List<GLCMFeature> list)
        {
            var data = new double[16];
            int count = list.Count;
            for (int i = 0; i < count; ++i)
            {
                for (int j = 0; j < 16; ++j)
                {
                    data[j] += list[i].sourceData[j];
                }
            }
            for (int j = 0; j < 16; ++j)
            {
                data[j] /= count;
            }
            return new GLCMFeature(data);
        }
    }

    internal class GLCMThreadParameter
    {
        private ManualResetEvent resetEvent;
        private int from;
        private int to;
        private List<byte[]> fragments;
        private List<GLCMFeature> result;

        public GLCMThreadParameter(int from, int to, List<byte[]> fragments, ManualResetEvent resetEvent)
        {
            this.resetEvent = resetEvent;
            this.from = from;
            this.to = to;
            this.fragments = fragments;
            result = new List<GLCMFeature>();
        }

        public int From
        {
            get
            {
                return from;
            }
        }

        public int To
        {
            get
            {
                return to;
            }
        }

        public List<byte[]> Data
        {
            get
            {
                return fragments;
            }

        }

        public List<GLCMFeature> Result
        {
            get
            {
                return result;
            }
        }

        public ManualResetEvent ResetEvent
        {
            get
            {
                return resetEvent;
            }
        }
    }

    public class GLCMCreator
    {
        private byte[] data;
        private GLCMFeature feature;
        private int width, height;
        private bool oneFragment;

        public GLCMCreator(SimpleImage image, bool oneFragment = false)
        {
            if ((image.Width < RecognitionParameters.FragmentsSize) || (image.Height < RecognitionParameters.FragmentsSize))
            {
                throw new ArgumentException("Изображение слишком мало");
            }

            this.oneFragment = oneFragment;
            feature = null;
            PrepareData(image);
            ConstructFeature();
        }

        private void PrepareData(SimpleImage image)
        {
            width = image.Width;
            height = image.Height;
            data = new byte[width * height];
            var grayData = image.GetGrayData();
            int quantizers = 256 / RecognitionParameters.GLCMSize;
            for (int y = 0; y < height; ++y)
            {
                int offset = y * width;
                for (int x = 0; x < width; ++x)
                {
                    data[offset + x] = (byte)(grayData[x, y] / quantizers);
                }
            }
        }

        private void ConstructFeature()
        {
            var list = GetSubfeatures();
            if (oneFragment)
            {
                feature = GLCMFeature.BuildStandart(list);
            }
            else
            {
                var filtered = FilterReject(list);
                feature = GLCMFeature.BuildStandart(filtered);
            }
        }

        private List<GLCMFeature> GetSubfeatures()
        {
            var fragments = GetFragments();

            int threadCount = Math.Min(fragments.Count, RecognitionParameters.FragmentProcessThreadCount);
            int threadPart = fragments.Count / threadCount;
            var resetEvents = new ManualResetEvent[threadCount];
            var threadParameters = new GLCMThreadParameter[threadCount];
            for (int i = 0; i < threadCount; ++i)
            {
                resetEvents[i] = new ManualResetEvent(false);
                threadParameters[i] = new GLCMThreadParameter(
                    threadPart * i, 
                    ((i + 1) < threadCount) ? (threadPart * (i + 1)) : (fragments.Count), 
                    fragments, resetEvents[i]);
                ThreadPool.QueueUserWorkItem(new WaitCallback(CalculateGLCM), threadParameters[i]);
            }
            WaitHandle.WaitAll(resetEvents);

            var list = new List<GLCMFeature>();
            for (int i = 0; i < threadCount; ++i)
            {
                list.AddRange(threadParameters[i].Result);
            }
            return list;
        }

        private List<byte[]> GetFragments()
        {
            var list = new List<byte[]>();
            int fragmentSize = RecognitionParameters.FragmentsSize;
            int XCount = width / fragmentSize;
            int YCount = height / fragmentSize;
            
            for (int x = 0; x < XCount; ++x)
            {
                for (int y = 0; y < YCount; ++y)
                {
                    int fromX = x * fragmentSize;
                    int fromY = y * fragmentSize;
                    int toX = fromX + fragmentSize;
                    int toY = fromY + fragmentSize;
                    list.Add(GetFragment(fromX, fromY, toX, toY));
                }
            }

            if (XCount * fragmentSize < width)
            {
                for (int x = 0; x < XCount; ++x)
                {
                    int fromX = x * fragmentSize;
                    int fromY = height - fragmentSize;
                    int toX = fromX + fragmentSize;
                    int toY = height;
                    list.Add(GetFragment(fromX, fromY, toX, toY));
                }
            }

            if (YCount * fragmentSize < height)
            {
                for (int y = 0; y < YCount; ++y)
                {
                    int fromX = width - fragmentSize;
                    int fromY = y * fragmentSize;
                    int toX = width;
                    int toY = fromY + fragmentSize;
                    list.Add(GetFragment(fromX, fromY, toX, toY));
                }
            }

            if ((XCount * fragmentSize < width) || (YCount * fragmentSize < height))
            {
                int fromX = width - fragmentSize;
                int fromY = height - fragmentSize;
                int toX = width;
                int toY = height;
                list.Add(GetFragment(fromX, fromY, toX, toY));
            }

            return list;
        }

        private byte[] GetFragment(int fromX, int fromY, int toX, int toY)
        {
            byte[] result = new byte[(toX - fromX) * (toY - fromY) + 2];
            result[0] = (byte)(toX - fromX);
            result[1] = (byte)(toY - fromY);
            int index = 2;
            for (int y = fromY; y < toY; ++y)
            {
                int offset = y * width;
                for (int x = fromX; x < toX; ++x)
                {
                    result[index] = data[offset + x];
                    ++index;
                }
            }
            return result;
        }

        private void CalculateGLCM(object parameter)
        {
            GLCMThreadParameter data = parameter as GLCMThreadParameter;
            if (data == null)
            {
                throw new ArgumentNullException("Неправильный параметр потока.");
            }

            for (int i = data.From; i < data.To; ++i)
            {
                var feature = GetGLCMFeature(data.Data[i]);
                data.Result.Add(feature);
            }
            data.ResetEvent.Set();
        }

        private GLCMFeature GetGLCMFeature(byte[] fragment)
        {
            var glcm0list = GetGLCMList(1, 0, fragment);
            var glcm45list = GetGLCMList(1, 1, fragment);
            var glcm90list = GetGLCMList(0, 1, fragment);
            var glcm135list = GetGLCMList(-1, 1, fragment);
            var glcm0feature = GetBestScalarPropertyFeature(glcm0list);
            var glcm45feature = GetBestScalarPropertyFeature(glcm45list);
            var glcm90feature = GetBestScalarPropertyFeature(glcm90list);
            var glcm135feature = GetBestScalarPropertyFeature(glcm135list);
            var bigfeature = new double[16];
            for (int i = 0; i < 4; ++i)
            {
                bigfeature[i * 4] = glcm0feature[i];
                bigfeature[i * 4 + 1] = glcm45feature[i];
                bigfeature[i * 4 + 2] = glcm90feature[i];
                bigfeature[i * 4 + 3] = glcm135feature[i];
            }
            return new GLCMFeature(bigfeature);
        }

        private List<double[,]> GetGLCMList(int dx, int dy, byte[] fragment)
        {
            var result = new List<double[,]>();
            int fwidth = fragment[0];
            int fheight = fragment[1];
            for (int i = 1; i < RecognitionParameters.GLCMMaxDisplacementDistance; ++i)
            {
                var glcm = new double[RecognitionParameters.GLCMSize, RecognitionParameters.GLCMSize];
                for (int y = 0; y < fheight; ++y)
                {
                    for (int x = 0; x < fwidth; ++x)
                    {
                        int nx = x + dx * i, ny = y + dy * i;
                        if (IsInFragment(nx, ny, fwidth, fheight))
                        {
                            int from = fragment[GetIndexInFragment(x, y, fwidth)];
                            int to = fragment[GetIndexInFragment(nx, ny, fwidth)];
                            glcm[from, to] += 1;
                        }
                    }
                }
                NormalizeGLCM(glcm, dx * i, dy * i);
                result.Add(glcm);
            }
            return result;
        }

        private bool IsInFragment(int x, int y, int fwidth, int fheight)
        {
            return (x >= 0) && (x < fwidth) && (y >= 0) && (y < fheight);
        }

        private int GetIndexInFragment(int x, int y, int fwidth)
        {
            return y * fwidth + x + 2;
        }

        private void NormalizeGLCM(double[,] glcm, int dx, int dy)
        {
            double factor = (RecognitionParameters.FragmentsSize - dx) * 
                (RecognitionParameters.FragmentsSize - dy);
            factor = 1 / factor;
            for (int y = 0; y < 16; ++y)
            {
                for (int x = 0; x < 16; ++x)
                {
                    glcm[x, y] *= factor;
                }
            }
        }

        private double[] GetScalarPropertyFeature(double[,] glcm)
        {
            double homogeneity = 0;
            double contrast = 0;
            double energy = 0;
            double entropy = 0;
            for (int y = 0; y < 16; ++y)
            {
                for (int x = 0; x < 16; ++x)
                {
                    homogeneity += glcm[x, y] / (1 + Math.Abs(x - y));
                    contrast += MathHelpers.Sqr(x - y) * glcm[x, y];
                    energy += MathHelpers.Sqr(glcm[x, y]);
                    if (glcm[x, y] > 0)
                    {
                        entropy -= glcm[x, y] * Math.Log(glcm[x, y], 2);
                    }
                }
            }
            return new double[] { homogeneity, contrast, energy, entropy };
        }

        private double[] GetBestScalarPropertyFeature(List<double[,]> glcmList)
        {
            var result = new double[4];
            for (int i = 0; i < glcmList.Count; ++i)
            {
                var scalar = GetScalarPropertyFeature(glcmList[i]);
                for (int j = 0; j < 4; ++j)
                {
                    if (scalar[j] > result[j])
                    {
                        result[j] = scalar[j];
                    }
                }
            }
            return result;
        }

        private List<GLCMFeature> FilterReject(List<GLCMFeature> list)
        {
            var standart = GLCMFeature.BuildStandart(list);
            var average = GetAverageDistance(list, standart);
            var deviation = GetDeviationDistance(list, standart, average);
            var goodfeatures = new List<GLCMFeature>();
            var studentTTest = alglib.studenttdistr.studenttdistribution(list.Count - 1, 0.16);
            for (int i = 0; i < list.Count; ++i)
            {
                var distance = list[i].GetDistance(standart);
                var value = (distance - average) / deviation;
                if (value < studentTTest)
                {
                    goodfeatures.Add(list[i]);
                }
            }
            return goodfeatures;
        }

        private double GetAverageDistance(List<GLCMFeature> list, GLCMFeature standart)
        {
            double sum = 0;
            int count = list.Count;
            for (int i = 0; i < count; ++i)
            {
                sum += list[i].GetDistance(standart);
            }
            return sum / count;
        }

        private double GetDeviationDistance(List<GLCMFeature> list, GLCMFeature standart, double average)
        {
            double sum = 0;
            int count = list.Count;
            for (int i = 0; i < count; ++i)
            {
                sum += MathHelpers.Sqr(list[i].GetDistance(standart) - average);
            }
            return Math.Sqrt(sum / (count - 1));
        }

        public GLCMFeature Feature
        {
            get
            {
                return feature;
            }
        }    
    }
}
