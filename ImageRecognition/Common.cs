using System;
using System.Drawing;
using System.Collections.Generic;

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

        public RecognitionResult(Rectangle region, TextureSample sample)
        {
            this.region = region;
            this.sample = sample;
            answers = new Dictionary<TextureFeatures, TextureClass>();
        }

        public void AddAnswer(TextureFeatures feature, TextureClass answer)
        {
            if (!answers.ContainsKey(feature))
            {
                answers.Add(feature, answer);
            }
            else
            {
                answers[feature] = answer;
            }
        }

        public Rectangle Region
        {
            get
            {
                return region;
            }
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
    }
}
