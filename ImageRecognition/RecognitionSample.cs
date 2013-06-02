using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;

namespace ImageRecognition
{
    public class RecognitionSample
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
            var xml = new XmlDocument();
            xml.CreateProcessingInstruction("xml", @"version=""1.0"" encoding=""WINDOWS-1251""");
            var sample = xml.CreateElement("sample");
            var attribute = xml.CreateAttribute("imageFile");
            attribute.Value = imageFile;
            sample.Attributes.Append(attribute);
            foreach (var result in sampleResults)
            {
                result.Save(sample, xml);
            }
            xml.AppendChild(sample);
            xml.Save(path);
        }

        public void LoadSample(string path)
        {
            sampleResults.Clear();

            var xml = new XmlDocument();
            xml.Load(path);
            var results = xml.GetElementsByTagName("sample");
            imageFile = results[0].Attributes["imageFile"].Value;                
            results = xml.GetElementsByTagName("RecognitionResult");
            foreach (XmlNode item in results)
            {
                var result = new RecognitionResult();
                result.Load(item, core.GetTextureClass);
                sampleResults.Add(result);                
            }     
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
                    sampleResults.Add(sampleAnswer);
                }
                var textureClass = core.GetTextureClass(answer);
                sampleAnswer.SetAnswer(TextureFeatures.GLCM, textureClass);
                sampleAnswer.SetAnswer(TextureFeatures.LBP, textureClass);
            }
        }

        public void Recognize()
        {
            var results = core.RecognizeImage(image);
            sampleResults.Clear();
            sampleResults.AddRange(results);
        }

        public Bitmap GetSampleImage()
        {
            if (image == null)
            {
                image = new Bitmap(imageFile);
            }
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

        public float CompareFeature(RecognitionSample other, TextureFeatures feature)
        {
            if (other.imageFile != imageFile)
            {
                return 0;
            }

            int fragmentsCount = 0;
            int fragmentsMathches = 0;
            foreach (var result in other.sampleResults)
            {
                ++fragmentsCount;
                var mineResult = GetAnswer(result.Region);
                if (mineResult != null)
                {
                    if (mineResult.CompareAnswers(result, feature))
                    {
                        ++fragmentsMathches;
                    }
                }
            }

            return fragmentsMathches / (float)fragmentsCount;
        }

        public int Width
        {
            get
            {
                return image.Width;
            }
        }

        public int Height
        {
            get
            {
                return image.Height;
            }
        }
    }
}
