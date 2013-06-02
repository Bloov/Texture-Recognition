using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextureRecognition
{
    public partial class OptimizeTeaching : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private static OptimizeTeaching instance;
        private TextureRecognition recognition;

        public OptimizeTeaching()
        {
            instance = this;

            InitializeComponent();

            recognition = TextureRecognition.Instance;           
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        internal static OptimizeTeaching Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
