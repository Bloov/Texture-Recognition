using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageRecognition
{
    public class CompareVector
    {
        double[] vector;
        int size;

        public CompareVector(SubImageSample sample, TextureFeatures useFeatures)
        {
            size = GetVectorSize(useFeatures);
            vector = new double[size];
        }

        public CompareVector(GLCMFeature glcmFeature, LBPFeature lbpFeature, TextureFeatures useFeatures)
        {
            size = GetVectorSize(useFeatures);
            vector = new double[size];
        }

        private bool IsUseGLCMFeature(TextureFeatures useFeatures)
        {
            return (useFeatures & TextureFeatures.GLCM) == TextureFeatures.GLCM;
        }

        private bool IsUseLBPFeature(TextureFeatures useFeatures)
        {
            return (useFeatures & TextureFeatures.LBP) == TextureFeatures.LBP;
        }

        private int GetVectorSize(TextureFeatures useFeatures)
        {
            int result = 0;
            if (IsUseGLCMFeature(useFeatures))
            {
                result += 8;
            }
            if (IsUseLBPFeature(useFeatures))
            {
                result += 1;
            }
            return result;
        }

        public double this[int index]
        {
            get
            {
                return 0;
            }
        }
    }
}
