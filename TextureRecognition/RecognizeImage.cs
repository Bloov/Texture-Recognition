using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ImageProcessing;
using ImageRecognition;

namespace TextureRecognition
{
    public partial class RecognizeImage : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private static RecognizeImage instance;
        private TextureRecognition recognition;
        
        private string imageUrl;
        private Image sourceImage;
        private Image recognizedImage;
        private Image selectingImage;
        private bool isSelecting, isSelected;
        private Point firstPoint, secondPoint;
        private Rectangle selectedRegion;
        private string answer;

        public RecognizeImage()
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

        internal static RecognizeImage Instance
        {
            get
            {
                return instance;
            }
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти из приложения?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MainForm.Instance.Close();
            }
        }

        private void tsmiBack_Click(object sender, EventArgs e)
        {
            Hide();
            MainForm.Instance.Show();
        }

        private void cbFeature1_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbFeature1.Checked && !cbFeature2.Checked)
            {
                cbFeature2.Checked = true;
            }
        }

        private void cbFeature2_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbFeature1.Checked && !cbFeature2.Checked)
            {
                cbFeature1.Checked = true;
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imageUrl = openImageDialog.FileName;
                sourceImage = new Bitmap(openImageDialog.FileName);
                selectingImage = new Bitmap(sourceImage.Width, sourceImage.Height);
                pbImage.Image = sourceImage;
                isSelected = false;
            }            
        }

        private TextureFeatures GetFeatures()
        {
            return ((cbFeature1.Checked) ? (TextureFeatures.GLCM) : (0)) |
                ((cbFeature2.Checked) ? (TextureFeatures.LBP) : (0));
        }

        private void RecognizeProcess(object parameter)
        {
            if (rbWay1.Checked)
            {
                recognizedImage = recognition.Core.RecognizeImage(imageUrl, selectedRegion, GetFeatures(), out answer);
            }
            else
            {
                recognizedImage = recognition.Core.RecognizeImage(imageUrl, GetFeatures());
            }
            this.BeginInvoke(
                new Action(delegate()
                    {
                        ShowImage();
                    }));
        }

        private void RecognizeControl(object parameter)
        {
            while (true)
            {
                this.BeginInvoke(
                    new Action(delegate()
                        {
                            var progress = recognition.Core.RecognitionProgress;
                            if (progress > 1)
                            {
                                progress = 1;
                            }
                            pbProgress.Value = (int)(pbProgress.Maximum * progress);
                        }));
                Thread.Sleep(200);
                if (!recognition.Core.IsRecognizing)
                {
                    break;
                }
            }
            this.BeginInvoke(
                new Action(delegate()
                    {
                        btnRecognize.Enabled = true;
                        btnSelectImage.Enabled = true;
                        panelParameters.Enabled = true;
                        tsmiBack.Enabled = true;
                        tsmiClose.Enabled = true;

                        pbProgress.Value = pbProgress.Maximum;

                        if (rbWay1.Checked)
                        {
                            MessageBox.Show("На этой областе изображено: " + answer);
                        }
                    }));                        
        }

        private void btnRecognize_Click(object sender, EventArgs e)
        {
            if (sourceImage == null)
            {
                return;
            }

            if (recognition.Core.TextureClassCount < 1)
            {
                MessageBox.Show("Неизвестно ни одного класса. Сначала проведите обучение.");
                return;
            }

            if (rbWay1.Checked && !isSelected)
            {
                MessageBox.Show("Не выделена область на изображении. Сначала выделите область.");
                return;
            }

            btnRecognize.Enabled = false;
            btnSelectImage.Enabled = false;
            panelParameters.Enabled = false;
            tsmiBack.Enabled = false;
            tsmiClose.Enabled = false;
            pbProgress.Value = 0;

            ThreadPool.QueueUserWorkItem(new WaitCallback(RecognizeProcess));
            ThreadPool.QueueUserWorkItem(new WaitCallback(RecognizeControl));
        }

        private void cbShowImage_CheckedChanged(object sender, EventArgs e)
        {
            ShowImage();
        }

        private void ShowImage()
        {
            if (cbShowImage.Checked)
            {
                pbImage.Image = sourceImage;
            }
            else
            {
                pbImage.Image = recognizedImage;
            }
        }

        private Point GetImageStartEdge(out double scale)
        {
            var iwidth = pbImage.Image.Width;
            var iheight = pbImage.Image.Height;
            var pwidth = pbImage.Width;
            var pheight = pbImage.Height;
            var shorizontal = pwidth / (double)iwidth;
            var svertical = pheight / (double)iheight;
            var min = Math.Min(shorizontal, svertical);
            var swidth = iwidth * min;
            var sheight = iheight * min;
            int dx = (int)((shorizontal != min) ? ((pwidth - swidth) / 2) : (0));
            int dy = (int)((svertical != min) ? ((pheight - sheight) / 2) : (0));
            scale = min;
            return new Point(dx, dy);
        }

        private Point GetImageEndEdge()
        {
            var iwidth = pbImage.Image.Width;
            var iheight = pbImage.Image.Height;
            var pwidth = pbImage.Width;
            var pheight = pbImage.Height;
            var shorizontal = pwidth / (double)iwidth;
            var svertical = pheight / (double)iheight;
            var min = Math.Min(shorizontal, svertical);
            var swidth = iwidth * min;
            var sheight = iheight * min;
            int dx = (int)((shorizontal != min) ? (pwidth - (pwidth - swidth) / 2) : (pwidth));
            int dy = (int)((svertical != min) ? (pheight - (pheight - sheight) / 2) : (pheight));
            return new Point(dx, dy);
        }

        private void UpdateSelectingImage()
        {
            var render = Graphics.FromImage(selectingImage);
            render.DrawImage(sourceImage, 0, 0, sourceImage.Width, sourceImage.Height);
            var x = Math.Min(firstPoint.X, secondPoint.X);
            var y = Math.Min(firstPoint.Y, secondPoint.Y);
            var width = Math.Abs(firstPoint.X - secondPoint.X);
            var height = Math.Abs(firstPoint.Y - secondPoint.Y);
            var region = new Rectangle(x, y, width, height);
            Color color1, color2;
            if ((width >= RecognitionParameters.FragmentsSize) &&
                (height >= RecognitionParameters.FragmentsSize))
            {
                color1 = Color.FromArgb(200, 30, 50, 250);
                color2 = Color.FromArgb(100, 30, 50, 250);
            }
            else
            {
                color1 = Color.FromArgb(200, 250, 30, 30);
                color2 = Color.FromArgb(100, 250, 30, 30);
            }
            var pen = new Pen(color1, 1);
            var brush = new SolidBrush(color2);
            render.FillRectangle(brush, region);
            render.DrawRectangle(pen, region);
            pbImage.Image = selectingImage;
        }

        private void pbImage_MouseDown(object sender, MouseEventArgs e)
        {
            if ((pbImage.Image != null) && (rbWay1.Checked))
            {
                isSelected = false;
                isSelecting = true;
                var scale = 1.0;
                var sedge = GetImageStartEdge(out scale);
                if (e.X < sedge.X)
                {
                    firstPoint.X = sedge.X;
                }
                else
                {
                    firstPoint.X = e.X;
                }
                if (e.Y < sedge.Y)
                {
                    firstPoint.Y = sedge.Y;
                }
                else
                {
                    firstPoint.Y = e.Y;
                }

                var eedge = GetImageEndEdge();
                if (e.X > eedge.X)
                {
                    firstPoint.X = eedge.X;
                }
                else
                {
                    firstPoint.X = e.X;
                }
                if (e.Y > eedge.Y)
                {
                    firstPoint.Y = eedge.Y;
                }
                else
                {
                    firstPoint.Y = e.Y;
                }
                firstPoint.X -= sedge.X;
                firstPoint.X = (int)(firstPoint.X / scale);
                firstPoint.Y -= sedge.Y;
                firstPoint.Y = (int)(firstPoint.Y / scale);
                secondPoint = firstPoint;
                UpdateSelectingImage();
            }
        }

        private void pbImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {
                var scale = 1.0;
                var sedge = GetImageStartEdge(out scale);
                if (e.X < sedge.X)
                {
                    secondPoint.X = sedge.X;
                }
                else
                {
                    secondPoint.X = e.X;
                }
                if (e.Y < sedge.Y)
                {
                    secondPoint.Y = sedge.Y;
                }
                else
                {
                    secondPoint.Y = e.Y;
                }

                var eedge = GetImageEndEdge();
                if (e.X > eedge.X)
                {
                    secondPoint.X = eedge.X;
                }
                else
                {
                    secondPoint.X = e.X;
                }
                if (e.Y > eedge.Y)
                {
                    secondPoint.Y = eedge.Y;
                }
                else
                {
                    secondPoint.Y = e.Y;
                }
                secondPoint.X -= sedge.X;
                secondPoint.X = (int)(secondPoint.X / scale);
                secondPoint.Y -= sedge.Y;
                secondPoint.Y = (int)(secondPoint.Y / scale);
                UpdateSelectingImage();
            }
        }

        private void pbImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {
                isSelecting = false;
                var x = Math.Min(firstPoint.X, secondPoint.X);
                var y = Math.Min(firstPoint.Y, secondPoint.Y);
                var width = Math.Abs(firstPoint.X - secondPoint.X);
                var height = Math.Abs(firstPoint.Y - secondPoint.Y);
                if ((width >= RecognitionParameters.FragmentsSize) &&
                    (height >= RecognitionParameters.FragmentsSize))
                {
                    selectedRegion = new Rectangle(x, y, width, height);
                    isSelected = true;
                }
                else
                {
                    isSelected = false;
                    pbImage.Image = sourceImage;
                }
            }
        }

        private void tsmiSaveResult_Click(object sender, EventArgs e)
        {
            if (recognizedImage == null)
            {
                MessageBox.Show("Изображение ещё не распознано");
                return;
            }

            if (saveImageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var ext = System.IO.Path.GetExtension(saveImageDialog.FileName);
                switch (ext)
                {
                    case ".png":
                        recognizedImage.Save(saveImageDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        return;

                    case ".bmp":
                        recognizedImage.Save(saveImageDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        return;

                    case ".jpg":
                    case ".jpeg":
                        recognizedImage.Save(saveImageDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        return;

                    default:
                        recognizedImage.Save(saveImageDialog.FileName);
                        return;
                }
            }
        }

        private void UpdateClassesLegend()
        {
            tsmiLegend.DropDownItems.Clear();
            for (int i = 0; i < recognition.Core.TextureClassCount; ++i)
            {
                var textureClass = recognition.Core.GetTextureClass(i);
                var classImage = new Bitmap(16, 16);
                var render = Graphics.FromImage(classImage);
                render.FillRectangle(new SolidBrush(textureClass.RegionColor), 
                    0, 0, classImage.Width, classImage.Height);
                tsmiLegend.DropDownItems.Add(textureClass.Name, classImage);
            }
        }

        private void RecognizeImage_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                UpdateClassesLegend();
            }
        }

        private void tsmiOptions_Click(object sender, EventArgs e)
        {
            RecognitionOptions.Instance.ShowDialog();
        }
    }
}
