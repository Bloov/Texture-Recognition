using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading;
using ImageRecognition;

namespace TextureRecognition
{
    public partial class TeachTextureClass : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private static TeachTextureClass instance;
        private TextureRecognition recognition;
        private Image classColor;

        private List<string> filesToTeach;
        private Dictionary<string, Bitmap> images;
        private ImageList imageList;

        public TeachTextureClass()
        {
            instance = this;

            InitializeComponent();

            recognition = TextureRecognition.Instance;
            
            filesToTeach = new List<string>();
            images = new Dictionary<string, Bitmap>();
            imageList = new ImageList();
            imageList.ImageSize = new System.Drawing.Size(40, 40);
            imageList.ColorDepth = ColorDepth.Depth16Bit;
            lwImages.LargeImageList = imageList;
            classColor = new Bitmap(16, 16);
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

        internal static TeachTextureClass Instance
        {
            get
            {
                return instance;
            }
        }

        private void TeachTextureClass_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                UpdateTextureClass();
                this.Focus();
                pbTeach.Value = 0;
            }
        }

        private void UpdateTextureClass()
        {
            if (recognition.CurrentClass == null)
            {
                MessageBox.Show("Указанный текстурный класс не найден.");
                ManageTextureClasses.Instance.Show();
                Hide();
                return;
            }

            filesToTeach.Clear();
            tsmiClass.Text = "Класс: " + recognition.CurrentClass.Name;
            pbImage.Image = null;
            UpdateClassData();
            UpdateClassColor();
        }

        private void UpdateClassData()
        {
            lwImages.Items.Clear();
            imageList.Images.Clear();
            images.Clear();
            
            for (int i = 0; i < recognition.CurrentClass.KnownSamplesNumber(TextureFeatures.GLCM); ++i)
            {
                var file = recognition.CurrentClass.GetKnownSample(i);
                var image = new Bitmap(file);
                images.Add(file, image);
                imageList.Images.Add(file, image);
                var item = lwImages.Items.Add(Path.GetFileNameWithoutExtension(file), file);
                item.Group = lwImages.Groups["lvgKnownSamples"];
            }
        }

        private void UpdateClassColor()
        {
            var render = Graphics.FromImage(classColor);
            render.FillRectangle(
                new SolidBrush(recognition.CurrentClass.RegionColor),
                0, 0, classColor.Width, classColor.Height);
            tsmiClass.Image = classColor;
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            //ManageTextureClasses.Instance.Show();
            Hide();
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти из приложения?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MainForm.Instance.Close();
            }
        }

        private void tsmiPrev_Click(object sender, EventArgs e)
        {
            var index = recognition.Core.GetTextureClassIndex(recognition.CurrentClass);
            var newClass = recognition.Core.GetTextureClass(index - 1);
            if (newClass == null)
            {
                newClass = recognition.Core.GetTextureClass(recognition.Core.TextureClassCount - 1);
            }
            recognition.CurrentClass = newClass;
            UpdateTextureClass();
        }

        private void tsmiNext_Click(object sender, EventArgs e)
        {
            var index = recognition.Core.GetTextureClassIndex(recognition.CurrentClass);
            var newClass = recognition.Core.GetTextureClass(index + 1);
            if (newClass == null)
            {
                newClass = recognition.Core.GetTextureClass(0);
            }
            recognition.CurrentClass = newClass;
            UpdateTextureClass();
        }

        private void tsmiChangeName_Click(object sender, EventArgs e)
        {
            ChangeName.Instance.ShowDialog();
            tsmiClass.Text = "Класс: " + recognition.CurrentClass.Name;
        }

        private void tsmiChandeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                recognition.CurrentClass.RegionColor = colorDialog.Color;
                tsmiClass.Image = null;
                UpdateClassColor();
            }
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            if (openImagesDialog.ShowDialog() == DialogResult.OK)
            {
                pbTeach.Value = 0;
                //filesToTeach.Clear();
                foreach (var file in openImagesDialog.FileNames)
                {
                    if (recognition.CurrentClass.IsKnowSample(file))
                    {
                        continue;
                    }

                    filesToTeach.Add(file);
                    var image = new Bitmap(file);
                    images.Add(file, image);
                    imageList.Images.Add(file, image);
                    var item = lwImages.Items.Add(Path.GetFileNameWithoutExtension(file), file);
                    item.Group = lwImages.Groups["lvgUnknownSamples"];
                    item.ForeColor = Color.Red;
                }
            }
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить выбраные образцы?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (ListViewItem item in lwImages.SelectedItems)
                {
                    var file = item.ImageKey;
                    if (recognition.CurrentClass.IsKnowSample(file))
                    {
                        recognition.CurrentClass.RemoveSample(file);
                    }
                    imageList.Images.RemoveByKey(file);
                    var image = images[file];
                    if (image == pbImage.Image)
                    {
                        pbImage.Image = null;
                    }
                    images.Remove(file);
                    filesToTeach.Remove(file);
                    lwImages.Items.Remove(item);
                }
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить все образцы?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                recognition.CurrentClass.RemoveAllSamples();
                filesToTeach.Clear();
                lwImages.Items.Clear();
                images.Clear();                
                imageList.Images.Clear();
                pbImage.Image = null;
            }
        }

        private void lwImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lwImages.SelectedItems.Count > 0)
            {
                var file = lwImages.SelectedItems[0].ImageKey;
                pbImage.Image = images[file];
            }
            else
            {
                pbImage.Image = null;
            }
        }

        private void btnTeachingControl_Click(object sender, EventArgs e)
        {
            if (recognition.CurrentClass.IsTeaching)
            {
                recognition.CurrentClass.AborTeaching();
                return;
            }

            pbTeach.Value = 0;
            btnDeleteAll.Enabled = false;
            btnDeleteSelected.Enabled = false;
            btnSelectFiles.Enabled = false;
            btnTeachingControl.Text = "Прервать обучение";
            btnTeachingControl.Image = resources.pause;
            tsmiClose.Enabled = false;
            tsmiExit.Enabled = false;
            tsmiNext.Enabled = false;
            tsmiPrev.Enabled = false;

            var sourceImages = GetImagesToTeach(filesToTeach);
            recognition.CurrentClass.Teach(filesToTeach, sourceImages);
            ThreadPool.QueueUserWorkItem(new WaitCallback(MonitorTeaching));
        }

        private List<Bitmap> GetImagesToTeach(List<string> files)
        {
            var result = new List<Bitmap>();
            for (int i = 0; i < filesToTeach.Count; ++i)
            {
                result.Add(images[filesToTeach[i]]);
            }
            return result;
        }

        private void MonitorTeaching(object parameter)
        {
            while (recognition.CurrentClass.IsTeaching)
            {
                this.BeginInvoke(
                    new Action(delegate()
                    {
                        var value = (int)(pbTeach.Maximum * recognition.CurrentClass.TeachingProgress);
                        if (value > pbTeach.Maximum)
                        {
                            value = pbTeach.Maximum;
                        }
                        pbTeach.Value = value;
                    }));
                Thread.Sleep(150);
            }
            this.BeginInvoke(
                    new Action(delegate()
                    {
                        pbTeach.Value = pbTeach.Maximum;
                        btnDeleteAll.Enabled = true;
                        btnDeleteSelected.Enabled = true;
                        btnSelectFiles.Enabled = true;
                        btnTeachingControl.Text = "Начать обучение";
                        btnTeachingControl.Image = resources.refresh;
                        tsmiClose.Enabled = true;
                        tsmiExit.Enabled = true;
                        tsmiNext.Enabled = true;
                        tsmiPrev.Enabled = true;

                        UpdateListViewItems();
                    }));
        }

        private void UpdateListViewItems()
        {
            foreach (ListViewItem item in lwImages.Items)
            {
                if (item.ForeColor == Color.Black)
                {
                    continue;
                }

                var file = item.ImageKey;
                if (recognition.CurrentClass.IsKnowSample(file))
                {
                    filesToTeach.Remove(file);
                    item.ForeColor = Color.Black;
                    item.Group = lwImages.Groups["lvgKnownSamples"];
                }
            }
        }
    }
}
