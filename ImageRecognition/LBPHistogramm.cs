using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ImageProcessing;

namespace ImageRecognition
{
    public class LBPFeature
    {
        double[] data;

        internal LBPFeature(double[] vector)
        {
            data = new double[18];
            for (int i = 0; i < 18; ++i)
            {
                data[i] = vector[i];
            }
        }

        public double this[int index]
        {
            get
            {
                if ((index < 0) || (index > 17))
                {
                    return 0;
                }
                return data[index];
            }
        }

        public double GetDistance(LBPFeature other)
        {
            double result = 0;
            for (int i = 0; i < 18; ++i)
            {
                result += Math.Abs(data[i] - other[i]);
            }
            return result;
        }

        public static LBPFeature BuildStandart(List<LBPFeature> list)
        {
            double[] standartData = new double[18];
            int count = list.Count;
            for (int i = 0; i < count; ++i)
            {
                for (int x = 0; x < 18; ++x)
                {
                    standartData[x] += list[i][x];
                }
            }
            for (int x = 0; x < 18; ++x)
            {
                standartData[x] /= count;
            }
            return new LBPFeature(standartData);
        }
    }

    internal struct Fragment
    {
        public int fromX;
        public int fromY;
        public byte size;
    }

    internal class LBPHistogrammThreadParameter
    {
        public LBPHistogrammThreadParameter(int from, int to, List<Fragment> list, ManualResetEvent resetEvent)
        {
            Result = new List<LBPFeature>();
            List = list;
            From = from;
            To = to;
            ResetEvent = resetEvent;
        }

        public List<Fragment> List
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

        public ManualResetEvent ResetEvent
        {
            get;
            set;
        }

        public List<LBPFeature> Result
        {
            get;
            set;
        }
    }

    public class LBPCreator
    {
        byte[,] data;
        int width, height;
        LBPFeature feature;
        bool filterRejects;
        
        public LBPCreator(ImageGrayData imageData, bool filterRejects = false)
        {
            if ((imageData.Width < RecognitionParameters.FragmentsSize) || 
                (imageData.Height < RecognitionParameters.FragmentsSize))
            {
                throw new ArgumentException("Изображение слишком мало");
            }

            data = imageData.Data;
            width = imageData.Width;
            height = imageData.Height;
            
            this.filterRejects = filterRejects;
            feature = null;
            ConstructFeature();
        }

        private void ConstructFeature()
        {
            var list = GetSubfeatures();
            if (!filterRejects)
            {
                feature = LBPFeature.BuildStandart(list);
            }
            else
            {
                var filtered = FilterReject(list);
                feature = LBPFeature.BuildStandart(filtered);
            }
        }

        private List<LBPFeature> GetSubfeatures()
        {
            var fragments = GetFragments();
            int threadCount = Math.Min(fragments.Count, RecognitionParameters.FragmentProcessThreadCount);
            int threadPart = fragments.Count / threadCount;
            var resetEvents = new ManualResetEvent[threadCount];
            var threadParameters = new LBPHistogrammThreadParameter[threadCount];
            for (int i = 0; i < threadCount; ++i)
            {
                resetEvents[i] = new ManualResetEvent(false);
                threadParameters[i] = new LBPHistogrammThreadParameter(
                    threadPart * i,
                    ((i + 1) < threadCount) ? (threadPart * (i + 1)) : (fragments.Count),
                    fragments, resetEvents[i]);
                ThreadPool.QueueUserWorkItem(new WaitCallback(CalculateLBP), threadParameters[i]);
            }
            WaitHandle.WaitAll(resetEvents);

            var list = new List<LBPFeature>();
            for (int i = 0; i < threadCount; ++i)
            {
                list.AddRange(threadParameters[i].Result);
            }
            return list;
        }

        private List<Fragment> GetFragments()
        {
            var list = new List<Fragment>();
            byte fragmentSize = RecognitionParameters.FragmentsSize;
            int XCount = width / fragmentSize;
            int YCount = height / fragmentSize;

            for (int x = 0; x < XCount; ++x)
            {
                for (int y = 0; y < YCount; ++y)
                {
                    Fragment fragment;
                    fragment.fromX = x * fragmentSize;
                    fragment.fromY = y * fragmentSize;
                    fragment.size = fragmentSize;
                    list.Add(fragment);
                }
            }

            if (XCount * fragmentSize < width)
            {
                for (int x = 0; x < XCount; ++x)
                {
                    Fragment fragment;
                    fragment.fromX = x * fragmentSize;
                    fragment.fromY = height - fragmentSize;
                    fragment.size = fragmentSize;
                    list.Add(fragment);
                }
            }

            if (YCount * fragmentSize < height)
            {
                for (int y = 0; y < YCount; ++y)
                {
                    Fragment fragment;
                    fragment.fromX = width - fragmentSize;
                    fragment.fromY = y * fragmentSize;
                    fragment.size = fragmentSize;
                    list.Add(fragment);
                }
            }

            if ((XCount * fragmentSize < width) || (YCount * fragmentSize < height))
            {
                Fragment fragment;
                fragment.fromX = width - fragmentSize;
                fragment.fromY = height - fragmentSize;
                fragment.size = fragmentSize;
                list.Add(fragment);
            }

            return list;
        }

        private void CalculateLBP(object parameter)
        {
            LBPHistogrammThreadParameter input = parameter as LBPHistogrammThreadParameter;
            if (parameter == null)
            {
                throw new ArgumentException("Неправильный формат параметра потока");
            }

            for (int i = input.From; i < input.To; ++i)
            {
                var feature = GetLBPFeature(input.List[i]);
                input.Result.Add(feature);
            }
            input.ResetEvent.Set();
        }

        private LBPFeature GetLBPFeature(Fragment fragment)
        {
            int fromX = MathHelpers.Clamp(fragment.fromX, 2, width - 2);
            int toX = MathHelpers.Clamp(fromX + fragment.size, 2, width - 2);
            int fromY = MathHelpers.Clamp(fragment.fromY, 2, height - 2);
            int toY = MathHelpers.Clamp(fromY + fragment.size, 2, height - 2);

            double[] histogramm = new double[18];
            int count = 0;
            for (int y = fromY; y < toY; ++y)
            {
                for (int x = fromX; x < toX; ++x)
                {
                    var lbpValue = GetLBPValue(x, y);
                    histogramm[GetHistogrammValue(lbpValue)] += 1;
                    ++count;
                }
            }
            for (int i = 0; i < 18; ++i)
            {
                histogramm[i] /= count;
            }

            return new LBPFeature(histogramm);
        }

        private UInt16 GetLBPValue(int x, int y)
        {
            var threshold = data[x, y];
            UInt16 result = 0;

            result += (UInt16)(GetBinary(data[x + 2, y], threshold));
            result += (UInt16)(GetBinary(
                MathHelpers.BilinearInterpolate(
                    0.84776, 0.76537,
                    data[x + 1, y], data[x + 2, y],
                    data[x + 1, y + 1], data[x + 2, y + 1]),
                threshold) * 2);
            result += (UInt16)(GetBinary(
                MathHelpers.BilinearInterpolate(
                    0.41421, 0.41421,
                    data[x + 1, y + 1], data[x + 2, y + 1],
                    data[x + 1, y + 2], data[x + 2, y + 2]),
                threshold) * 4);
            result += (UInt16)(GetBinary(
                MathHelpers.BilinearInterpolate(
                    0.76537, 0.84776,
                    data[x, y + 1], data[x + 1, y + 1],
                    data[x, y + 2], data[x + 1, y + 2]),
                threshold) * 8);
            result += (UInt16)(GetBinary(data[x, y + 2], threshold) * 16);
            result += (UInt16)(GetBinary(
                MathHelpers.BilinearInterpolate(
                    0.23463, 0.84776,
                    data[x - 1, y + 1], data[x, y + 1],
                    data[x - 1, y + 2], data[x, y + 2]),
                threshold) * 32);
            result += (UInt16)(GetBinary(
                MathHelpers.BilinearInterpolate(
                    0.58579, 0.41421,
                    data[x - 2, y + 1], data[x - 1, y + 1],
                    data[x - 2, y + 2], data[x - 1, y + 2]),
                threshold) * 64);
            result += (UInt16)(GetBinary(
                MathHelpers.BilinearInterpolate(
                    0.15224, 0.76537,
                    data[x - 2, y], data[x - 1, y],
                    data[x - 2, y + 1], data[x - 1, y + 1]),
                threshold) * 128);
            result += (UInt16)(GetBinary(data[x - 2, y], threshold) * 256);
            result += (UInt16)(GetBinary(
                MathHelpers.BilinearInterpolate(
                    0.15224, 0.23463,
                    data[x - 2, y - 1], data[x - 1, y - 1],
                    data[x - 2, y], data[x - 1, y]),
                threshold) * 512);
            result += (UInt16)(GetBinary(
                MathHelpers.BilinearInterpolate(
                    0.58579, 0.58579,
                    data[x - 2, y - 2], data[x - 1, y - 2],
                    data[x - 2, y - 1], data[x - 1, y - 1]),
                threshold) * 1024);
            result += (UInt16)(GetBinary(
                MathHelpers.BilinearInterpolate(
                    0.23463, 0.15224,
                    data[x - 1, y - 2], data[x, y - 2],
                    data[x - 1, y - 1], data[x, y - 1]),
                threshold) * 2048);
            result += (UInt16)(GetBinary(data[x, y - 2], threshold) * 4096);
            result += (UInt16)(GetBinary(
                MathHelpers.BilinearInterpolate(
                    0.76537, 0.15224,
                    data[x, y - 2], data[x + 1, y - 2],
                    data[x, y - 1], data[x + 1, y - 1]),
                threshold) * 8192);
            result += (UInt16)(GetBinary(
                MathHelpers.BilinearInterpolate(
                    0.41421, 0.58579,
                    data[x + 1, y - 2], data[x + 1, y - 2],
                    data[x + 2, y - 1], data[x + 2, y - 1]),
                threshold) * 16384);
            result += (UInt16)(GetBinary(
                MathHelpers.BilinearInterpolate(
                    0.84776, 0.23463,
                    data[x + 1, y - 1], data[x + 1, y - 1],
                    data[x + 2, y], data[x + 2, y]),
                threshold) * 32768);

            return result;
        }

        private byte GetBinary(byte value, byte threshold)
        {
            if (value >= threshold)
            {
                return 1;
            }
            return 0;
        }

        private byte GetHistogrammValue(UInt16 lbpValue)
        {
            UInt16 min = UInt16.MaxValue;
            for (byte i = 0; i < 16; i++)
            {
                UInt16 rotated = MathHelpers.RightCyclicRotate(lbpValue, i);
                if (rotated < min)
                {
                    min = rotated;
                }
            }

            if (min == 0)
            {
                return 0;
            }

            if (min == 1)
            {
                return 1;
            }

            UInt16 part = 1;
            for (byte i = 1; i < 16; ++i)
            {
                part = (UInt16)(part | (part << 1));
                if (part == min)
                {
                    return (byte)(i + 1);
                }
            }            

            return 17;
        }

        private List<LBPFeature> FilterReject(List<LBPFeature> list)
        {
            var standart = LBPFeature.BuildStandart(list);
            var average = GetAverageDistance(list, standart);
            var deviation = GetDeviationDistance(list, standart, average);
            var goodfeatures = new List<LBPFeature>();
            var studentTTest = alglib.studenttdistr.studenttdistribution(
                list.Count - 1, RecognitionParameters.RejectSignificanceLevel);
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

        private double GetAverageDistance(List<LBPFeature> list, LBPFeature standart)
        {
            double sum = 0;
            int count = list.Count;
            for (int i = 0; i < count; ++i)
            {
                sum += list[i].GetDistance(standart);
            }
            return sum / count;
        }

        private double GetDeviationDistance(List<LBPFeature> list, LBPFeature standart, double average)
        {
            double sum = 0;
            int count = list.Count;
            for (int i = 0; i < count; ++i)
            {
                sum += MathHelpers.Sqr(list[i].GetDistance(standart) - average);
            }
            return Math.Sqrt(sum / (count - 1));
        }

        public LBPFeature Feature
        {
            get
            {
                return feature;
            }
        }
    }
}
