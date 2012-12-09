using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageProcessing;
using ImageRecognition;

namespace TextureRecognition
{
    public partial class MainForm : Form
    {
        private static MainForm instance;
        private ManageTextureClasses manageTexturesWindow;
        private TeachTextureClass teachTextureClassWindow;
        private RecognizeImage recognizeImageWindow;

        public MainForm()
        {
            instance = this;

            InitializeComponent();

            manageTexturesWindow = new ManageTextureClasses();
            teachTextureClassWindow = new TeachTextureClass();
            recognizeImageWindow = new RecognizeImage();
        }

        internal static MainForm Instance
        {
            get
            {
                return instance;
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsmiTeaching_Click(object sender, EventArgs e)
        {
            manageTexturesWindow.Show();
            Hide();
        }

        private void tsmiRecognition_Click(object sender, EventArgs e)
        {
            recognizeImageWindow.Show();
            Hide();
        }
    }
}
