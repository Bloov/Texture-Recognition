using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageRecognition
{
    class RecognitionSample
    {
        private RecognitionCore core;
        private string imageFile;
        private Bitmap image;
        private List<RecognitionResult> sampleResults;

        public RecognitionSample(RecognitionCore core)
        {
            this.core = core;
            imageFile = "";
            image = null;
            sampleResults = new List<RecognitionResult>();
        }
        
        public RecognitionSample(RecognitionCore core, string file)
        {
            this.core = core;
            imageFile = file;
            image = new Bitmap(file);
            sampleResults = new List<RecognitionResult>();
        }

        public void SaveSample(string path)
        {
            
        }

        public void LoadSample(string path)
        {
            
        }        

        private RecognitionResult GetAnswer(Rectangle fragment)
        {
            foreach (var answer in sampleResults)
            {
                if (fragment.Equals(answer.Region))
                {
                    return answer;                   
                }
            }
            return null;
        }

        private List<Rectangle> GetPossibleFragments(Point position)
        {
            var result = new List<Rectangle>();
            var fragmentSize = RecognitionParameters.RecognitionFragmentSize;

            int XCount = image.Width / fragmentSize;
            int YCount = image.Height / fragmentSize;

            int XInd = position.X / fragmentSize;
            int YInd = position.Y / fragmentSize;
            
            if ((XInd < XCount) && (YInd < YCount))
            {
                result.Add(new Rectangle(XInd * fragmentSize, YInd * fragmentSize,
                                         fragmentSize, fragmentSize));
            }                
            if ((XInd == XCount) && (YInd == YCount))
            {
                result.Add(new Rectangle(image.Width - fragmentSize, image.Height - fragmentSize,
                                         fragmentSize, fragmentSize));
            }
            if (((image.Width - fragmentSize) < position.X) && (YInd < YCount))
            {
                result.Add(new Rectangle(image.Width - fragmentSize, YInd * fragmentSize,
                                         fragmentSize, fragmentSize));
            }
            if ((XInd < XCount) && ((image.Height - fragmentSize) < position.Y))
            {
                result.Add(new Rectangle(XInd * fragmentSize, image.Height - fragmentSize,
                                         fragmentSize, fragmentSize));
            }
            if (((image.Width - fragmentSize) < position.X) && ((image.Height - fragmentSize) < position.Y))
            {
                result.Add(new Rectangle(image.Width - fragmentSize, image.Height - fragmentSize,
                                         fragmentSize, fragmentSize));
            }

            return result;
        }

        public void SetAnswer(Point position, string answer)
        {
            var fragments = GetPossibleFragments(position);
            foreach (var fragment in fragments)
            {
                var sampleAnswer = GetAnswer(fragment);
                if (sampleAnswer == null)
                {
                    sampleAnswer = new RecognitionResult();
                    sampleAnswer.Region = fragment;
                }
                var textureClass = core.GetTextureClass(answer);
                sampleAnswer.SetAnswer(TextureFeatures.GLCM, textureClass);
                sampleAnswer.SetAnswer(TextureFeatures.LBP, textureClass);
            }
        }

        public void Recognize()
        {
            
        }

        public Bitmap GetSampleImage()
        {
            var result = new Bitmap(image);
            var render = Graphics.FromImage(result);
            int alpha = 50;
            foreach (var item in sampleResults)
            {
                var variant = item[TextureFeatures.GLCM];
                render.FillRectangle(
                    new SolidBrush(Color.FromArgb(alpha, variant.RegionColor.R, variant.RegionColor.G, variant.RegionColor.B)),
                    item.Region);

                variant = item[TextureFeatures.LBP];
                render.FillRectangle(
                    new SolidBrush(Color.FromArgb(alpha, variant.RegionColor.R, variant.RegionColor.G, variant.RegionColor.B)),
                    item.Region);
            }
            return result;
        }

        public float Compare(RecognitionSample other)
        {
            var result = 1.0f;

            return result;
        }
    }
}
