using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageProcessing
{
    internal class GrayFilterThreadParameter
    {
        public GrayFilterThreadParameter(byte[] data, byte[,] result, int stride, int from, int to, ManualResetEvent resetEvent)
        {
            Data = data;
            Result = result;
            Stride = stride;
            From = from;
            To = to;
            ResetEvent = resetEvent;
        }

        public byte[] Data
        {
            get;
            set;
        }

        public byte[,] Result
        {
            get;
            set;
        }

        public int Stride
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
    }

    public class SimpleImage
    {
        private Bitmap image;
        int width, height;
        private byte[,] grayData;

        public SimpleImage(Bitmap source)
        {
            image = source;
            width = image.Width;
            height = image.Height;
            int imageStride = 0;
            var sourceData = GetImageData(image, out imageStride);
            grayData = GrayFilter(sourceData, imageStride);
        }

        public SimpleImage(string url)
        {
            image = new Bitmap(url);
            width = image.Width;
            height = image.Height;
            int imageStride = 0;
            var sourceData = GetImageData(image, out imageStride);
            grayData = GrayFilter(sourceData, imageStride);
        }

        private byte[] GetImageData(Bitmap image, out int stride)
        {
            BitmapData bitmapData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height), 
                ImageLockMode.ReadOnly, image.PixelFormat);
            stride = bitmapData.Stride;
            byte[] data = new byte[stride * bitmapData.Height];
            Marshal.Copy(bitmapData.Scan0, data, 0, data.Length);
            image.UnlockBits(bitmapData);
            return data;
        }

        private void GrayFilterThread(object parameter)
        {
            GrayFilterThreadParameter input = parameter as GrayFilterThreadParameter;
            if (input == null)
            {
                throw new ArgumentException("Неправильный формат параметра потока");
            }

            var grayResult = input.Result;
            var data = input.Data;
            int delta = input.Stride / Width;
            for (int y = input.From; y < input.To; ++y)
            {
                int offset = y * input.Stride;
                for (int x = 0; x < Width; ++x)
                {
                    int index = offset + x * delta;
                    byte color = (byte)((data[index] + data[index + 1] + data[index + 2]) / 3);
                    grayResult[x, y] = color;
                }
            }
            input.ResetEvent.Set();
        }

        private byte[,] GrayFilter(byte[] data, int stride)
        {
            var grayResult = new byte[width, height];
            int threadCount = Environment.ProcessorCount;
            int threadPart = height / threadCount;
            var resetEvents = new ManualResetEvent[threadCount];
            var threadParameters = new GrayFilterThreadParameter[threadCount];
            for (int i = 0; i < threadCount; ++i)
            {
                resetEvents[i] = new ManualResetEvent(false);
                threadParameters[i] = new GrayFilterThreadParameter(
                    data, grayResult, stride,
                    threadPart * i,
                    ((i + 1) < threadCount) ? (threadPart * (i + 1)) : (height),
                    resetEvents[i]);
                ThreadPool.QueueUserWorkItem(new WaitCallback(GrayFilterThread), threadParameters[i]);
            }
            WaitHandle.WaitAll(resetEvents);
            return grayResult;
        }

        private byte[,] ExtractGrayData(int fromX, int fromY, int toX, int toY)
        {
            var result = new byte[toX - fromX, toY - fromY];
            for (int y = fromY; y < toY; ++y)
            {
                for (int x = fromX; x < toX; ++x)
                {
                    result[x - fromX, y - fromY] = grayData[x, y];
                }
            }
            return result;
        }

        public ImageGrayData GetGrayData(int fromX, int fromY, int toX, int toY)
        {
            fromX = (fromX < 0) ? (0) : (fromX);
            fromY = (fromY < 0) ? (0) : (fromY);
            toX = (toX > Width) ? (Width) : (toX);
            toY = (toY > Height) ? (Height) : (toY);
            if ((fromX >= toX) || (fromY >= toY))
            {
                throw new ArgumentOutOfRangeException("Неправильный размер фрагмента");
            }

            var data = ExtractGrayData(fromX, fromY, toX, toY);
            return new ImageGrayData(data);
        }

        public ImageGrayData GetGrayData()
        {
            var data = ExtractGrayData(0, 0, width, height);
            return new ImageGrayData(data);
        }

        public Bitmap Image
        {
            get
            {
                return image;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
        }
    }
}
