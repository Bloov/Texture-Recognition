using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageProcessing;

namespace ImageRecognition
{
    public class TextureSample
    {
        public TextureSample(ImageGrayData imageData, bool isDivideToFragments)
        {
            Sample = imageData;

            GLCMCreator glcm = new GLCMCreator(imageData, false, isDivideToFragments);
            GLCM = glcm.Feature;

            LBPCreator lbp = new LBPCreator(imageData, false, isDivideToFragments);
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
