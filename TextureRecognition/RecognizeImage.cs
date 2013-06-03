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
        private Bitmap sourceImage;
        private Bitmap recognizedImage;
        private Bitmap selectingImage;
        private bool isSelecting, isSelected;
        private Point firstPoint, secondPoint;
        private Rectangle selectedRegion;
        private List<RecognitionResult> answers;
        private RecognitionResult answer;
        private bool isRecognized;

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

        private void RecognizeImage_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                Focus();
                UpdateClassesLegend();
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
        }

        private void tsmiBatchProcessing_Click(object sender, EventArgs e)
        {
            BatchProcessing.Instance.ShowDialog();
            Focus();
        }

        private void tsmiOptions_Click(object sender, EventArgs e)
        {
            var result = RecognitionOptions.Instance.ShowDialog();
            Focus();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                isRecognized = false;
                btnRecognize.Image = resources.search;
                btnRecognize.Text = "Распознать\nтекстуры";
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

        private void cbShowImage_CheckedChanged(object sender, EventArgs e)
        {
            UpdateImages();
        }

        private void UpdateImages()
        {
            if (cbShowImage.Checked)
            {
                pbImage.Image = sourceImage;
            }
            else
            {
                if (recognizedImage == null)
                {
                    pbImage.Image = sourceImage;
                }
                else
                {
                    pbImage.Image = recognizedImage;
                }
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imageUrl = openImageDialog.FileName;
                sourceImage = new Bitmap(openImageDialog.FileName);
                if ((sourceImage.Width < RecognitionParameters.RecognitionFragmentSize) ||
                    (sourceImage.Height < RecognitionParameters.RecognitionFragmentSize))
                {
                    MessageBox.Show("Изображение меньше указанного размера фрагмента для разбиения.\n" +
                        "Укажите иной размер фрагмента или выберите другое изображение.");
                    return;
                }

                isRecognized = false;
                btnRecognize.Image = resources.search;
                btnRecognize.Text = "Распознать\nтекстуры";

                recognizedImage = null;
                selectingImage = new Bitmap(sourceImage.Width, sourceImage.Height);
                pbImage.Image = sourceImage;
                isSelected = false;
            }            
        }

        private void btnRecognize_Click(object sender, EventArgs e)
        {
            if (sourceImage == null)
            {
                MessageBox.Show("Выберете сначала изображение.");
                return;
            }

            if (isRecognized)
            {
                UpdateRecognizedImage();
                UpdateImages();
                return;
            }

            if ((sourceImage.Width < RecognitionParameters.RecognitionFragmentSize) ||
                (sourceImage.Height < RecognitionParameters.RecognitionFragmentSize))
            {
                MessageBox.Show("Изображние слишком мало - невозможно разбить на фрагменты.\n" +
                    "Выберете другое изображение или размер фрагмента.");
                return;
            }

            if (recognition.Core.TextureClassCount < 1)
            {
                MessageBox.Show("Неизвестно ни одного класса. Сначала проведите обучение.");
                return;
            }

            if (rbRegion.Checked && !isSelected)
            {
                MessageBox.Show("Не выделена область на изображении. Сначала выделите область.");
                return;
            }

            btnRecognize.Enabled = false;
            btnSelectImage.Enabled = false;
            panelParameters.Enabled = false;
            tsmiBatchProcessing.Enabled = false;
            tsmiOptions.Enabled = false;
            tsmiSaveResult.Enabled = false;
            tsmiBack.Enabled = false;
            tsmiClose.Enabled = false;
            pbProgress.Value = 0;

            answer = null;
            answers = null;
            ThreadPool.QueueUserWorkItem(new WaitCallback(RecognizeProcess));
            ThreadPool.QueueUserWorkItem(new WaitCallback(RecognizeControl));
        }

        private void RecognizeProcess(object parameter)
        {
            if (rbRegion.Checked || rbAllImage.Checked)
            {
                Rectangle region = selectedRegion;
                if (rbAllImage.Checked)
                {
                    region = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
                }
                answer = recognition.Core.RecognizeImage(sourceImage, region);
            }
            else
            {
                answers = recognition.Core.RecognizeImage(sourceImage);
            }
            this.BeginInvoke(
                new Action(delegate()
                    {
                        UpdateRecognizedImage();
                        UpdateImages();
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
                Thread.Sleep(150);
                if (!recognition.Core.IsRecognizing)
                {
                    break;
                }
            }
            this.BeginInvoke(
                new Action(delegate()
                    {
                        btnRecognize.Enabled = true;
                        if (rbSegmentation.Checked)
                        {
                            isRecognized = true;
                            btnRecognize.Image = resources.refresh;
                            btnRecognize.Text = "Обновить\nизображение";
                        }

                        btnSelectImage.Enabled = true;
                        panelParameters.Enabled = true;
                        tsmiBatchProcessing.Enabled = true;
                        tsmiOptions.Enabled = true;
                        tsmiSaveResult.Enabled = true;
                        tsmiBack.Enabled = true;
                        tsmiClose.Enabled = true;
                        pbProgress.Value = pbProgress.Maximum;

                        if (rbRegion.Checked || rbAllImage.Checked)
                        {
                            MessageBox.Show("В выделеной области изображено\n" +
                                "\tсогласно LBP-гистограммам: " + answer[TextureFeatures.LBP].Name + "\n" +
                                "\tсогласно ПМВВ: " + answer[TextureFeatures.GLCM].Name);
                        }
                    }));                        
        }

        private void UpdateRecognizedImage()
        {
            List<RecognitionResult> list = new List<RecognitionResult>();
            if (answers == null)
            {
                list.Add(answer);
            }
            else
            {
                list.AddRange(answers);
            }

            recognizedImage = new Bitmap(sourceImage);
            var render = Graphics.FromImage(recognizedImage);
            int alpha = 50;
            alpha = (cbFeature1.Checked && !cbFeature2.Checked) ? (100) : (alpha);
            alpha = (cbFeature2.Checked && !cbFeature1.Checked) ? (100) : (alpha);
            foreach (var item in list)
            {
                var variant = item[TextureFeatures.GLCM];
                if ((variant != null) && cbFeature1.Checked)
                {
                    render.FillRectangle(
                        new SolidBrush(Color.FromArgb(alpha, variant.RegionColor.R, variant.RegionColor.G, variant.RegionColor.B)),
                        item.Region);
                }

                variant = item[TextureFeatures.LBP];
                if ((variant != null) && cbFeature2.Checked)
                {
                    render.FillRectangle(
                        new SolidBrush(Color.FromArgb(alpha, variant.RegionColor.R, variant.RegionColor.G, variant.RegionColor.B)),
                        item.Region);
                }
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
            if ((pbImage.Image != null) && (rbRegion.Checked))
            {
                isSelected = false;
                isSelecting = true;
                var scale = 1.0;
                var sedge = GetImageStartEdge(out scale);
                var eedge = GetImageEndEdge();
                firstPoint.X = (e.X < sedge.X) ? (sedge.X) : (e.X);
                firstPoint.Y = (e.Y < sedge.Y) ? (sedge.Y) : (e.Y);
                firstPoint.X = (e.X > eedge.X) ? (eedge.X) : (e.X);
                firstPoint.Y = (e.Y > eedge.Y) ? (eedge.Y) : (e.Y);
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
                var eedge = GetImageEndEdge();
                secondPoint.X = (e.X < sedge.X) ? (sedge.X) : (e.X);
                secondPoint.Y = (e.Y < sedge.Y) ? (sedge.Y) : (e.Y);
                secondPoint.X = (e.X > eedge.X) ? (eedge.X) : (e.X);
                secondPoint.Y = (e.Y > eedge.Y) ? (eedge.Y) : (e.Y);
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

        private void rbSegmentation_CheckedChanged(object sender, EventArgs e)
        {
            isRecognized = false;
            btnRecognize.Image = resources.search;
            btnRecognize.Text = "Распознать\nтекстуры";
        }

        private void rbRegion_CheckedChanged(object sender, EventArgs e)
        {
            isRecognized = false;
            btnRecognize.Image = resources.search;
            btnRecognize.Text = "Распознать\nтекстуры";
        }

        private void rbAllImage_CheckedChanged(object sender, EventArgs e)
        {
            isRecognized = false;
            btnRecognize.Image = resources.search;
            btnRecognize.Text = "Распознать\nтекстуры";
        }
    }
}
