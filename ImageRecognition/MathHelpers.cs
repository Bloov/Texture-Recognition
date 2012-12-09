using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRecognition
{
    internal static class MathHelpers
    {
        public static double GetAverage(double[] data, int from, int count)
        {
            if (from + count > data.Length)
            {
                throw new ArgumentOutOfRangeException("Выход за пределы массива");
            }
            double sum = 0;
            for (int i = from; i < from + count; ++i)
            {
                sum += data[i];
            }
            return sum / count; 
        }

        public static double GetVariance(double[] data, double average, int from, int count)
        {
            if (from + count > data.Length)
            {
                throw new ArgumentOutOfRangeException("Выход за пределы массива");
            }
            double sum = 0;
            for (int i = from; i < from + count; ++i)
            {
                sum += Sqr(data[i] - average);
            }
            return sum / count;
        }

        public static double GetVariance(double[] data, int from, int count)
        {
            if (from + count > data.Length)
            {
                throw new ArgumentOutOfRangeException("Выход за пределы массива");
            }
            double average = GetAverage(data, from, count);
            double sum = 0;
            for (int i = from; i < from + count; ++i)
            {
                sum += Sqr(data[i] - average);
            }
            return sum / count;
        }

        public static double Sqr(double a)
        {
            return a * a;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }

        public static double Clamp(double value, double min, double max)
        {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }

        public static byte BilinearInterpolate(double x, double y,
            byte x0y0, byte x1y0, byte x0y1, byte x1y1)
        {
            var value = x0y0 * (1 - x) * (1 - y) + x1y0 * x * (1 - y) +
                x0y1 * (1 - x) * y + x1y1 * x * y;
            byte result = (byte)Math.Round(value);
            return result;
        }

        public static UInt16 RightCyclicRotate(UInt16 value, byte bits)
        {
            return (UInt16)(((UInt16)(value >> bits)) | ((UInt16)(value << (16 - bits))));
        }
    }
}
