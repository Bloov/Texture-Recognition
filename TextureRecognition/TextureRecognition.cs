using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageRecognition;

namespace TextureRecognition
{
    public class TextureRecognition
    {
        private static TextureRecognition instance;
        private RecognitionCore core;
        
        static TextureRecognition()
        {
            instance = new TextureRecognition();
        }

        private TextureRecognition()
        {
            core = new RecognitionCore();
            CurrentClass = null;
        }

        public static TextureRecognition Instance
        {
            get
            {
                return instance;
            }
        }

        public RecognitionCore Core
        {
            get
            {
                return core;
            }
        }

        public TextureClass CurrentClass
        {
            get;
            set;
        }
    }
}
