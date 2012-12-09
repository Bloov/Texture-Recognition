using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageProcessing;

namespace ImageRecognition
{
    public class SubImageSample
    {
        public SubImageSample(SimpleImage sample)
        {
            Sample = sample;

            GLCMCreator glcm = new GLCMCreator(sample, true);
            GLCM = glcm.Feature;

            LBPCreator lbp = new LBPCreator(sample, true);
            LBP = lbp.Feature;
        }

        public SimpleImage Sample
        {
            get;
            set;
        }

        public GLCMFeature GLCM
        {
            get;
            set;
        }

        public LBPFeature LBP
        {
            get;
            set;
        }
    }
}
