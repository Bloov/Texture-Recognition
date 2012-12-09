using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRecognition
{
    static public class RecognitionParameters
    {
        static RecognitionParameters()
        {
            NeededNeighborsNumber = 8;
            FragmentProcessThreadCount = (byte)Environment.ProcessorCount;
            FragmentsSize = 36;
            RejectSignificanceLevel = 0.4;
            GLCMMaxDisplacementDistance = 12;
            GLCMSize = 16;
        }

        public static int NeededNeighborsNumber
        {
            get;
            set;
        }

        public static byte FragmentProcessThreadCount
        {
            get;
            set;
        }

        public static byte FragmentsSize
        {
            get;
            set;
        }

        public static double RejectSignificanceLevel
        {
            get;
            set;
        }

        public static byte GLCMMaxDisplacementDistance
        {
            get;
            set;
        }

        public static byte GLCMSize
        {
            get;
            set;
        }
    }
}
