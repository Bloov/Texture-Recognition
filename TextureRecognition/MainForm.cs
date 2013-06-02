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
        private ChangeName changeNameWindow;
        private RecognitionOptions recognitionOptionsWindow;
        private BatchProcessing batchProcessingWindow;
        private CreateSample createSample;
        private OptimizeTeaching optimizeTeaching;
        private TextureRecognition recognition;

        public MainForm()
        {
            instance = this;

            InitializeComponent();

            manageTexturesWindow = new ManageTextureClasses();
            teachTextureClassWindow = new TeachTextureClass();
            recognizeImageWindow = new RecognizeImage();
            changeNameWindow = new ChangeName();
            recognitionOptionsWindow = new RecognitionOptions();
            batchProcessingWindow = new BatchProcessing();
            createSample = new CreateSample();
            optimizeTeaching = new OptimizeTeaching();

            recognition = TextureRecognition.Instance;
        }

        internal static MainForm Instance
        {
            get
            {
                return instance;
            }
        }

        private void MainForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                Focus();
            }
        }

        private void tsmiResetKnowledges_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить все знания системы?", "Внимание", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                recognition.Core.RemoveAllTextureClasses();
            }
        }

        private void tsmiSaveKnoledges_Click(object sender, EventArgs e)
        {
            if (saveKnowledgesDialog.ShowDialog() == DialogResult.OK)
            {
                recognition.Core.SaveKnowledges(saveKnowledgesDialog.FileName);
            }
        }

        private void tsmiLoadKnowledges_Click(object sender, EventArgs e)
        {
            if (openKnowledgesDialog.ShowDialog() == DialogResult.OK)
            {
                recognition.Core.LoadKnowledges(openKnowledgesDialog.FileName);
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsmiTeaching_Click(object sender, EventArgs e)
        {
            manageTexturesWindow.ShowDialog();
            Focus();
        }

        private void tsmiRecognition_Click(object sender, EventArgs e)
        {
            recognizeImageWindow.ShowDialog();
            Focus();
        }

        private void tsmiCreateSample_Click(object sender, EventArgs e)
        {
            createSample.ShowDialog();
            Focus();
        }

        private void tsmiOptimizeTeachig_Click(object sender, EventArgs e)
        {
            optimizeTeaching.ShowDialog();
            Focus();
        }
    }
}
