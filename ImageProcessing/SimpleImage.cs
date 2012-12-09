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
        private Bitmap sourceImage;
        private PixelFormat sourcePixelFormat;
        int sourceWidth, sourceHeight;
        private int imageStride;
        private byte[,] grayData;

        public SimpleImage(Image image)
        {
            sourceImage = new Bitmap(image);
            sourcePixelFormat = sourceImage.PixelFormat;
            sourceWidth = sourceImage.Width;
            sourceHeight = sourceImage.Height;
            var sourceData = GetImageData(sourceImage, out imageStride);
            grayData = GrayFilter(sourceData, imageStride);
        }

        public SimpleImage(string url)
        {
            sourceImage = new Bitmap(url);
            sourcePixelFormat = sourceImage.PixelFormat;
            sourceWidth = sourceImage.Width;
            sourceHeight = sourceImage.Height;
            var sourceData = GetImageData(sourceImage, out imageStride);
            grayData = GrayFilter(sourceData, imageStride);
        }

        private SimpleImage(PixelFormat format, byte[] data, int width, int height)
        {
            sourceWidth = width;
            sourceHeight = height;
            sourceImage = new Bitmap(width, height, format);
            sourcePixelFormat = format;
            var sourceData = GetImageData(sourceImage, out imageStride);
            CopyGrayDataToData(data, width, height, sourceData, imageStride);
            CopyDataToImage(sourceData, sourceImage);
            grayData = ExtractGrayData(data, width, height);
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

        private void CopyDataToImage(byte[] data, Bitmap image)
        {
            BitmapData bitmapData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.WriteOnly, image.PixelFormat);
            Marshal.Copy(data, 0, bitmapData.Scan0, bitmapData.Stride * bitmapData.Height);
            image.UnlockBits(bitmapData);
        }

        private void GrayFilterThread(object parameter)
        {
            GrayFilterThreadParameter input = parameter as GrayFilterThreadParameter;
            if (parameter == null)
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
            var grayResult = new byte[Width, Height];

            int threadCount = Environment.ProcessorCount;
            int threadPart = sourceHeight / threadCount;
            var resetEvents = new ManualResetEvent[threadCount];
            var threadParameters = new GrayFilterThreadParameter[threadCount];
            for (int i = 0; i < threadCount; ++i)
            {
                resetEvents[i] = new ManualResetEvent(false);
                threadParameters[i] = new GrayFilterThreadParameter(
                    data, grayResult, stride,
                    threadPart * i,
                    ((i + 1) < threadCount) ? (threadPart * (i + 1)) : (sourceHeight),
                    resetEvents[i]);
                ThreadPool.QueueUserWorkItem(new WaitCallback(GrayFilterThread), threadParameters[i]);
            }
            WaitHandle.WaitAll(resetEvents);
            return grayResult;
        }

        private void CopyGrayDataToData(byte[] gdata, int gdataWidth, int gdataHeigth, byte[] data, int dataStride)
        {
            int delta = dataStride / gdataWidth;
            for (int y = 0; y < gdataHeigth; ++y)
            {
                int offset = y * dataStride;
                for (int x = 0; x < gdataWidth; ++x)
                {
                    int index = offset + x * delta;
                    int gindex = y * gdataWidth + x;
                    data[index] = gdata[gindex];
                    data[index + 1] = gdata[gindex];
                    data[index + 2] = gdata[gindex];
                }
            }
        }

        private byte[,] ExtractGrayData(byte[] data, int dataWidth, int dataHeigth)
        {
            var result = new byte[dataWidth, dataHeigth];
            for (int y = 0; y < dataHeigth; ++y)
            {
                for (int x = 0; x < dataWidth; ++x)
                {
                    int index = y * dataWidth + x;
                    result[x, y] = data[index];
                }
            }
            return result;
        }

        private byte[,] ExtractGrayData(int fromX, int fromY, int toX, int toY)
        {
            var result = new byte[toY - fromY, toX - fromX];
            for (int y = fromY; y < toY; ++y)
            {
                for (int x = fromX; x < toX; ++x)
                {
                    result[x - fromX, y - fromY] = grayData[x, y];
                }
            }
            return result;
        }

        private byte[] ExctractSubImage(int fromX, int fromY, int toX, int toY)
        {
            var result = new byte[(toX - fromX) * (toY - fromY)];
            int index = 0;
            for (int y = fromY; y < toY; ++y)
            {
                for (int x = fromX; x < toX; ++x)
                {
                    result[index] = grayData[x, y];
                    ++index;
                }
            }
            return result;
        }

        public SimpleImage GetSubImage(int fromX, int fromY, int toX, int toY)
        {
            fromX = (fromX < 0) ? (0) : (fromX);
            fromY = (fromY < 0) ? (0) : (fromY);
            int width = ((toX > Width) ? (Width) : (toX)) - fromX;
            int height = ((toY > Height) ? (Height) : (toY)) - fromY;
            if ((width <= 0) || (height <= 0))
            {
                throw new ArgumentOutOfRangeException("Неправельный размер врагмента");
            }

            var data = ExctractSubImage(fromX, fromY, fromX + width, fromY + height);
            return new SimpleImage(sourcePixelFormat, data, width, height);
        }

        public byte[,] GetGrayData()
        {
            var result = new byte[Width, Height];
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    result[x, y] = grayData[x, y];
                }
            }
            return result;
        }

        public ImageGrayData GetGrayData(int fromX, int fromY, int toX, int toY)
        {
            fromX = (fromX < 0) ? (0) : (fromX);
            fromY = (fromY < 0) ? (0) : (fromY);
            int width = ((toX > Width) ? (Width) : (toX)) - fromX;
            int height = ((toY > Height) ? (Height) : (toY)) - fromY;
            if ((width <= 0) || (height <= 0))
            {
                throw new ArgumentOutOfRangeException("Неправельный размер фрагмента");
            }

            var data = ExtractGrayData(fromX, fromY, fromX + width, fromY + height);
            return new ImageGrayData(width, height, data);
        }

        public Bitmap SourceImage
        {
            get
            {
                return sourceImage;
            }
        }

        public int Width
        {
            get
            {
                return sourceWidth;
            }
        }

        public int Height
        {
            get
            {
                return sourceHeight;
            }
        }
    }
}
