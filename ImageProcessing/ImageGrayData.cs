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

        internal ImageGrayData(int width, int height, byte[,] data)
        {
            this.width = width;
            this.height = height;
            this.data = data;
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
