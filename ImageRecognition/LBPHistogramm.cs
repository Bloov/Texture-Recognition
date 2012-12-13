using System;
using System.Collections.Generic;
using System.Threading;
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
                result += MathHelpers.Sqr(data[i] - other[i]) / (data[i] + other[i]);
            }
            return result;
        }

        public static LBPFeature BuildStandart(List<LBPFeature> list)
        {
            if (list.Count == 0)
            {
                return null;
            }

            if (list.Count == 1)
            {
                return list[0];
            }

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

    internal class LBPHistogrammThreadParameter
    {
        public LBPHistogrammThreadParameter(int from, int to, List<Fragment> list, ManualResetEvent resetEvent)
        {
            From = from;
            To = to;
            List = list;
            Result = new List<LBPFeature>();
            ResetEvent = resetEvent;
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

        public List<Fragment> List
        {
            get;
            set;
        }

        public List<LBPFeature> Result
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

    public class LBPCreator
    {
        private byte[,] data;
        private int width, height;
        private int fragmentSize;
        private LBPFeature feature;
        private bool isTeaching;
        private bool isDivideToFragments;
        private bool isMultithread;

        public LBPCreator(ImageGrayData imageData, bool isTeaching, bool isDivideToFragments, bool isMultithread = true)
        {
            var needSize = RecognitionParameters.RecognitionFragmentSize;
            if (isTeaching)
            {
                needSize = RecognitionParameters.FragmentsSize;
            }

            if ((imageData.Width < needSize) ||
                (imageData.Height < needSize))
            {
                throw new ArgumentException("Размер изображения меньше размера фрагмента для разбиения. \n" +
                    "Невозможно создать LBP-гистограмму");
            }

            fragmentSize = needSize;
            this.isTeaching = isTeaching;
            this.isDivideToFragments = isDivideToFragments;
            this.isMultithread = isMultithread;
            data = imageData.Data;
            width = imageData.Width;
            height = imageData.Height;            
            feature = null;
            ConstructFeature();
        }

        private void ConstructFeature()
        {
            if (isDivideToFragments)
            {
                var list = GetSubfeatures();
                feature = LBPFeature.BuildStandart(list);
            }
            else
            {
                feature = GetLBPFeature(new Fragment(0, 0, width, height));
            }
        }

        private List<LBPFeature> GetSubfeatures()
        {
            var fragments = GetFragments();
            int threadCount = Math.Min(fragments.Count, RecognitionParameters.FragmentProcessThreadCount);
            threadCount = (isMultithread) ? (threadCount) : (1);
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
                if (threadCount > 1)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(CalculateLBP), threadParameters[i]);
                }
                else
                {
                    CalculateLBP(threadParameters[i]);
                }
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
            int XCount = width / fragmentSize;
            int YCount = height / fragmentSize;

            for (int x = 0; x < XCount; ++x)
            {
                for (int y = 0; y < YCount; ++y)
                {
                    Fragment fragment = new Fragment(x * fragmentSize, y * fragmentSize,
                        fragmentSize, fragmentSize);
                    list.Add(fragment);
                }
            }

            if (XCount * fragmentSize < width)
            {
                for (int y = 0; y < YCount; ++y)
                {
                    Fragment fragment = new Fragment(width - fragmentSize, y * fragmentSize,
                        fragmentSize, fragmentSize);
                    list.Add(fragment);
                }
            }

            if (YCount * fragmentSize < height)
            {
                for (int x = 0; x < XCount; ++x)
                {
                    Fragment fragment = new Fragment(x * fragmentSize, height - fragmentSize,
                        fragmentSize, fragmentSize);
                    list.Add(fragment);
                }
            }

            if ((XCount * fragmentSize < width) || (YCount * fragmentSize < height))
            {
                Fragment fragment = new Fragment(width - fragmentSize, height - fragmentSize,
                    fragmentSize, fragmentSize);
                list.Add(fragment);
            }

            return list;
        }

        private void CalculateLBP(object parameter)
        {
            LBPHistogrammThreadParameter input = parameter as LBPHistogrammThreadParameter;
            if (parameter == null)
            {
                throw new ArgumentException("Неправильный формат параметра потока.");
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
            int toX = MathHelpers.Clamp(fromX + fragment.width, 2, width - 2);
            int fromY = MathHelpers.Clamp(fragment.fromY, 2, height - 2);
            int toY = MathHelpers.Clamp(fromY + fragment.height, 2, height - 2);

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
            var min = UInt16.MaxValue;
            for (byte i = 0; i < 16; i++)
            {
                var rotated = MathHelpers.RightCyclicRotate(lbpValue, i);
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

        public LBPFeature Feature
        {
            get
            {
                return feature;
            }
        }
    }
}
