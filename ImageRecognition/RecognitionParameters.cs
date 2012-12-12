using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRecognition
{
    static public class RecognitionParameters
    {
        private static int needNeighborsNumber;
        private static int fragmentProcessThreadCount;
        private static int fragmentSize;
        private static int recognitionFragmentSize;
        private static double rejectSignificanceLevel;
        private static byte glcmMaxDisplacementDistance;
        private static int glcmSize;

        static RecognitionParameters()
        {
            NeededNeighborsNumber = 5;
            FragmentProcessThreadCount = Environment.ProcessorCount;
            FragmentsSize = 36;
            RecognitionFragmentSize = FragmentsSize;
            RejectSignificanceLevel = 0.65;
            GLCMMaxDisplacementDistance = 12;
            GLCMSize = 16;
        }

        public static int NeededNeighborsNumber
        {
            get
            {
                return needNeighborsNumber;
            }
            set
            {
                if (value < 1)
                {
                    needNeighborsNumber = 1;
                }
                else
                {
                    needNeighborsNumber = value;
                }
            }
        }

        public static int FragmentProcessThreadCount
        {
            get
            {
                return fragmentProcessThreadCount;
            }
            set
            {
                fragmentProcessThreadCount = MathHelpers.Clamp(value, 1, 4 * Environment.ProcessorCount);
            }
        }

        public static int FragmentsSize
        {
            get
            {
                return fragmentSize;
            }
            set
            {
                if (value < 36)
                {
                    fragmentSize = 36;
                }
                else
                {
                    fragmentSize = value;
                }
                recognitionFragmentSize = MathHelpers.Clamp(recognitionFragmentSize, fragmentSize, int.MaxValue);
            }
        }

        public static int RecognitionFragmentSize
        {
            get
            {
                return recognitionFragmentSize;
            }
            set
            {
                if (value < fragmentSize)
                {
                    recognitionFragmentSize = fragmentSize;
                }
                else
                {
                    recognitionFragmentSize = value;
                }
            }
        }

        public static double RejectSignificanceLevel
        {
            get
            {
                return rejectSignificanceLevel;
            }
            set
            {
                rejectSignificanceLevel = MathHelpers.Clamp(value, 0.01, 1.0);
            }
        }

        public static byte GLCMMaxDisplacementDistance
        {
            get
            {
                return glcmMaxDisplacementDistance;
            }
            set
            {
                glcmMaxDisplacementDistance = (byte)MathHelpers.Clamp(value, 8, 20);
            }
        }

        public static int GLCMSize
        {
            get
            {
                return glcmSize;
            }
            set
            {
                glcmSize = MathHelpers.Clamp(value, 4, 256);
            }
        }
    }
}
