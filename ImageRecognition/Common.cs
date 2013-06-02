using System;
using System.Drawing;
using System.Collections.Generic;
using System.Xml;

namespace ImageRecognition
{
    public enum TextureFeatures
    {
        GLCM = 0x01,
        LBP = 0x02
    }

    internal struct Fragment
    {
        public int fromX;
        public int fromY;
        public int width;
        public int height;

        public Fragment(int fromX, int fromY, int width, int height)
        {
            this.fromX = fromX;
            this.fromY = fromY;
            this.width = width;
            this.height = height;
        }
    }

    public class RecognitionResult
    {
        private TextureSample sample;
        private Rectangle region;
        private Dictionary<TextureFeatures, TextureClass> answers;

        public RecognitionResult()
        {
            region = new Rectangle(0, 0, 0, 0);
            sample = null;
            answers = new Dictionary<TextureFeatures, TextureClass>();
        }

        public RecognitionResult(Rectangle region, TextureSample sample)
        {
            this.region = region;
            this.sample = sample;
            answers = new Dictionary<TextureFeatures, TextureClass>();
        }

        internal void Save(XmlElement element, XmlDocument parent)
        {
            var data = parent.CreateElement("RecognitionResult");
            var attribute = parent.CreateAttribute("answer");
            attribute.Value = answers[TextureFeatures.GLCM].Name;
            data.Attributes.Append(attribute);
            var regionElement = parent.CreateElement("region");

            attribute = parent.CreateAttribute("X");
            attribute.Value = region.X.ToString();
            regionElement.Attributes.Append(attribute);

            attribute = parent.CreateAttribute("Y");
            attribute.Value = region.Y.ToString();
            regionElement.Attributes.Append(attribute);

            attribute = parent.CreateAttribute("Width");
            attribute.Value = region.Width.ToString();
            regionElement.Attributes.Append(attribute);

            attribute = parent.CreateAttribute("Height");
            attribute.Value = region.Height.ToString();
            regionElement.Attributes.Append(attribute);

            element.AppendChild(regionElement);
        }

        internal delegate TextureClass GetTextureClassByName(string name);

        internal void Load(XmlNode node, GetTextureClassByName getter)
        {
            int x, y, width, height;
            int.TryParse(node.ChildNodes[0].Attributes["X"].Value, out x);
            int.TryParse(node.ChildNodes[0].Attributes["Y"].Value, out y);
            int.TryParse(node.ChildNodes[0].Attributes["Width"].Value, out width);
            int.TryParse(node.ChildNodes[0].Attributes["Height"].Value, out height);
            region.X = x;
            region.Y = y;
            region.Width = width;
            region.Height = height;

            var answer = getter(node.Attributes["answer"].Value);
            answers[TextureFeatures.GLCM] = answer;
            answers[TextureFeatures.LBP] = answer;
        }

        public void SetAnswer(TextureFeatures feature, TextureClass answer)
        {
            answers[feature] = answer;            
        }

        public Rectangle Region
        {
            get
            {
                return region;
            }
            set
            {
                region = value;                
            }

        public TextureSample Sample
        {
            get
            {
                return sample;
            }
        }

        public TextureClass this[TextureFeatures feature]
        {
            get
            {
                TextureClass result;
                answers.TryGetValue(feature, out result);
                return result;
            }
        }
    
        public bool CompareAnswers(RecognitionResult other, TextureFeatures feature)
        {
            if (other != null)
            {
                var otherAnswer = other[feature].Name;
                if (otherAnswer != null)
                {
                    return otherAnswer == this[feature].Name;
                }
            }
            return false;
        }       
    }
}
