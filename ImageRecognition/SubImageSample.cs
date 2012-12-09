using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageProcessing;

namespace ImageRecognition
{
    public class SubImageSample
    {
        public SubImageSample(ImageGrayData imageData)
        {
            Sample = imageData;

            GLCMCreator glcm = new GLCMCreator(imageData, false);
            GLCM = glcm.Feature;

            LBPCreator lbp = new LBPCreator(imageData, false);
            LBP = lbp.Feature;
        }

        public ImageGrayData Sample
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
