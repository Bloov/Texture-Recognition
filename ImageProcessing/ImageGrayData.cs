using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
    public struct ImageGrayData
    {
        int width, height;
        byte[,] data;

        internal ImageGrayData(byte[,] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("Не указаны данные о яркости");
            }

            this.data = data;
            width = data.GetLength(0);
            height = data.GetLength(1);
            if ((width == 0) || (height == 0))
            {
                throw new ArgumentException("Недопустимый размер фрагмента");
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

        public byte[,] Data
        {
            get
            {
                return data;
            }
        }
    }
}
