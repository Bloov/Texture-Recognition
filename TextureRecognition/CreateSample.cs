using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ImageRecognition;

namespace TextureRecognition
{
    public partial class CreateSample : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private static CreateSample instance;
        private TextureRecognition recognition;

        private RecognitionSample sample;
        private bool mousePressed;
        private bool mouseRightPressed;
        private Point from, to;

        public CreateSample()
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

        internal static CreateSample Instance
        {
            get
            {
                return instance;
            }
        }

        private void tsmiBack_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти из приложения?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MainForm.Instance.Close();
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            if (openImage.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sample = new RecognitionSample(TextureRecognition.Instance.Core, openImage.FileName);
                pbSample.Image = sample.GetSampleImage();
            }

            lbTextureClasses.Items.Clear();
            var core = TextureRecognition.Instance.Core;
            for (int i = 0; i < core.TextureClassCount; ++i)
            {
                lbTextureClasses.Items.Add(core.GetTextureClass(i).Name);
            }
            lbTextureClasses.SelectedIndex = -1;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            pbSample.Image = sample.GetSampleImage();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openSample.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sample = new RecognitionSample(TextureRecognition.Instance.Core);
                sample.LoadSample(openSample.FileName);
                pbSample.Image = sample.GetSampleImage();
            }

            lbTextureClasses.Items.Clear();
            var core = TextureRecognition.Instance.Core;
            for (int i = 0; i < core.TextureClassCount; ++i)
            {
                lbTextureClasses.Items.Add(core.GetTextureClass(i).Name);
            }
            lbTextureClasses.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveSample.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sample.SaveSample(saveSample.FileName);            
            }
        }

        private void lbTextureClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            var textureClass = TextureRecognition.Instance.Core.GetTextureClass(lbTextureClasses.SelectedIndex);
            if (textureClass != null)
            {
                var image = new Bitmap(16, 16);
                var render = Graphics.FromImage(image);
                render.FillRectangle(new SolidBrush(textureClass.RegionColor),
                    0, 0, 16, 16);
                pbTextureClass.Image = image;
            }
        }

        private Point TranslatePosition(Point point)
        {
            var x = point.X / (float)pbSample.Width;
            var y = point.Y / (float)pbSample.Height;
            var imgX = (int)(x * sample.Width);
            var imgY = (int)(y * sample.Height);
            return new Point(imgX, imgY);
        }

        private void pbSample_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mousePressed = true;
                var textureClass = TextureRecognition.Instance.Core.GetTextureClass(lbTextureClasses.SelectedIndex);
                if (textureClass != null)
                {
                    sample.SetAnswer(TranslatePosition(new Point(e.X, e.Y)), textureClass.Name);
                }
                pbSample.Image = sample.GetSampleImage();
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                from = TranslatePosition(new Point(e.X, e.Y));                
            }
        }

        private void pbSample_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                var point = new Point(e.X, e.Y);
                var textureClass = TextureRecognition.Instance.Core.GetTextureClass(lbTextureClasses.SelectedIndex);
                if (textureClass != null)
                {
                    sample.SetAnswer(TranslatePosition(point), textureClass.Name);
                }
                pbSample.Image = sample.GetSampleImage();
            }
        }

        private void pbSample_MouseUp(object sender, MouseEventArgs e)
        {
            mousePressed = false;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                to = TranslatePosition(new Point(e.X, e.Y));
                var textureClass = TextureRecognition.Instance.Core.GetTextureClass(lbTextureClasses.SelectedIndex);
                if (textureClass != null)
                {
                    for (int i = Math.Min(from.X, to.X); i <= Math.Max(from.X, to.X); ++i)
                    {
                        for (int j = Math.Min(from.Y, to.Y); j <= Math.Max(from.Y, to.Y); ++j)
                        {
                            sample.SetAnswer(new Point(i, j), textureClass.Name);
                        }
                    }
                    pbSample.Image = sample.GetSampleImage();
                }
            }
        }

        private void btnRecognize_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = false;
            btnRecognize.Enabled = false;
            btnSave.Enabled = false;
            btnSelectImage.Enabled = false;
            msMenu.Enabled = false;

            ThreadPool.QueueUserWorkItem(new WaitCallback(RecognizeProcess));                        
        } 
       
        private void RecognizeProcess(object param)
        {
            sample.RecognizeFake();
            pbSample.Image = sample.GetSampleImage();

            this.BeginInvoke(
                new Action(delegate()
                {
                    btnOpen.Enabled = true;
                    btnRecognize.Enabled = true;
                    btnSave.Enabled = true;
                    btnSelectImage.Enabled = true;
                    msMenu.Enabled = true;
                }));            
        }
    }
}
