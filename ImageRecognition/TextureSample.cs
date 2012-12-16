using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageProcessing;

namespace ImageRecognition
{
    public class TextureSample
    {
        private ImageGrayData sample;
        private GLCMFeature glcm;
        private LBPFeature lbp;

        public TextureSample(ImageGrayData imageData, bool isDivideToFragments)
        {
            sample = imageData;
            var glcmCreator = new GLCMCreator(sample, false, isDivideToFragments);
            var lbpCreator = new LBPCreator(sample, false, isDivideToFragments);
            glcm = glcmCreator.Feature;            
            lbp = lbpCreator.Feature;
        }

        public ImageGrayData Sample
        {
            get
            {
                return sample;
            }
        }

        public GLCMFeature GLCM
        {
            get
            {
                return glcm;
            }
        }

        public LBPFeature LBP
        {
            get
            {
                return lbp;
            }
        }
    }
}
