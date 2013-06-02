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
        private List<RecognitionResult> sampleResults;

        public RecognitionSample(RecognitionCore core)
        {
            this.core = core;
            imageFile = "";
            sampleResults = new List<RecognitionResult>();
        }
        
        public RecognitionSample(RecognitionCore core, string file)
        {
            this.core = core;
            imageFile = file;
            sampleResults = new List<RecognitionResult>();
        }

        public void SaveSample(string path)
        {
            
        }

        public void LoadSample(string path)
        {
            
        }

        public void SetAnswer(Point position, string answer)
        {
            
        }

        public void Recognize()
        {
            
        }

        public float Compare(RecognitionSample other)
        {
            var result = 1.0f;

            return result;
        }
    }
}
