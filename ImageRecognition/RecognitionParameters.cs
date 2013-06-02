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
        private static byte glcmMaxDisplacementDistance;
        private static int glcmSize;
        private static double glcmDeviationWeight;
        private static double compactFactor;
        private static double dischargeFactor;

        static RecognitionParameters()
        {
            NeededNeighborsNumber = 6;
            FragmentProcessThreadCount = Environment.ProcessorCount;
            FragmentsSize = 40;
            RecognitionFragmentSize = FragmentsSize;
            GLCMMaxDisplacementDistance = 12;
            GLCMSize = 16;
            GLCMDeviationWeight = 0.75;
            CompactFactor = 0.35;
            DischargeFactor = 0.35;
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

        public static double GLCMDeviationWeight
        {
            get
            {
                return glcmDeviationWeight;
            }
            set
            {
                glcmDeviationWeight = MathHelpers.Clamp(value, 0, 1);
            }
        }

        public static double CompactFactor
        {
            get
            {
                return compactFactor;
            }
            set
            {
                compactFactor = value;
                compactFactor = MathHelpers.Clamp(compactFactor, 0.01, 1.0);
            }
        }

        public static double DischargeFactor
        {
            get
            {
                return dischargeFactor;
            }
            set
            {
                dischargeFactor = value;
                dischargeFactor = MathHelpers.Clamp(dischargeFactor, 0.01, 1.0);
            }
        }
    }
}
