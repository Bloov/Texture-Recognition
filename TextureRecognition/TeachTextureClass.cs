using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ImageRecognition;

namespace TextureRecognition
{
    public partial class TeachTextureClass : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private static TeachTextureClass instance;
        private TextureRecognition recognition;
        private TextureClass workClass;
        private Image classColor;

        private List<string> imageUrls;
        private Dictionary<string, Image> images;
        private ImageList imageList;

        public TeachTextureClass()
        {
            instance = this;

            InitializeComponent();

            recognition = TextureRecognition.Instance;
            imageUrls = new List<string>();
            images = new Dictionary<string, Image>();
            imageList = new ImageList();
            imageList.ImageSize = new System.Drawing.Size(48, 48);
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

        private void UpdateData()
        {
            for (int i = 0; i < workClass.KnownSamplesNumber; ++i)
            {
                var file = workClass.GetKnownSample(i);
                var image = new Bitmap(file);
                var key = System.IO.Path.GetFileName(file);
                images.Add(key, image);
                imageUrls.Add(file);
                imageList.Images.Add(key, image);
                lwImages.Items.Add(key, key).ToolTipText = file;
            }
        }

        private void UpdateClassColor()
        {
            var render = Graphics.FromImage(classColor);
            render.FillRectangle(
                new SolidBrush(workClass.RegionColor),
                0, 0, 16, 16);
            tsmiClass.Image = classColor;
        }

        internal bool SetTextureClass(string name)
        {
            workClass = recognition.Core.GetTextureClass(name);
            if (workClass == null)
            {
                MessageBox.Show("Указанный текстурный класс не найден.");
                ManageTextureClasses.Instance.Show();
                return false;
            }
            Show();

            tsmiClass.Text = "Класс: " + workClass.Name;
            UpdateClassColor();

            lwImages.Items.Clear();
            images.Clear();
            imageUrls.Clear();
            imageList.Images.Clear();
            pbImage.Image = null;
            UpdateData();

            return true;
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            Hide();
            ManageTextureClasses.Instance.Show();
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            if (openImagesDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var file in openImagesDialog.FileNames)
                {
                    for (int i = 0; i < workClass.KnownSamplesNumber; ++i)
                    {
                        if (file == workClass.GetKnownSample(i))
                        {
                            continue;
                        }
                    }
                    if (imageUrls.Exists((string name) => name == file))
                    {
                        continue;
                    }

                    var image = new Bitmap(file);
                    var key = System.IO.Path.GetFileName(file);
                    images.Add(key, image);
                    imageUrls.Add(file);
                    imageList.Images.Add(key, image);
                    var item = lwImages.Items.Add(key, key);
                    item.ToolTipText = file;
                    item.ForeColor = Color.Red;
                }
            }
        }

        private void lwImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lwImages.SelectedItems.Count > 0)
            {
                var key = lwImages.SelectedItems[0].ImageKey;
                pbImage.Image = images[key];
            }
            else
            {
                pbImage.Image = null;
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < workClass.KnownSamplesNumber; ++i)
            {
                workClass.RemoveSample(i);
            }
            lwImages.Items.Clear();
            images.Clear();
            imageUrls.Clear();
            imageList.Images.Clear();
            pbImage.Image = null;
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lwImages.SelectedItems)
            {
                var key = item.ImageKey;
                for (int i = 0; i < workClass.KnownSamplesNumber; ++i)
                {
                    if (item.ToolTipText == workClass.GetKnownSample(i))
                    {
                        workClass.RemoveSample(i);
                    }
                }
                imageList.Images.RemoveByKey(key);
                var image = images[key];
                if (image == pbImage.Image)
                {
                    pbImage.Image = null;
                }
                images.Remove(key);
                imageUrls.RemoveAll((string name) => name == item.ToolTipText);
                lwImages.Items.Remove(item);
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти из приложения?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MainForm.Instance.Close();
            }
        }

        private void MonitorTeaching(object parameter)
        {
            while (workClass.IsTeaching)
            {
                this.BeginInvoke(
                    new Action(delegate()
                        { 
                            pbTeach.Value = (int)(pbTeach.Maximum * workClass.TeachingProgress); 
                        }));
                Thread.Sleep(500);
            }
            this.BeginInvoke(
                    new Action(delegate()
                        {
                            pbTeach.Value = pbTeach.Maximum;
                            btnDeleteAll.Enabled = true;
                            btnDeleteSelected.Enabled = true;
                            btnSelectFiles.Enabled = true;
                            btnTeachingControl.Enabled = true;
                            tsmiClose.Enabled = true;
                            tsmiExit.Enabled = true;

                            foreach (ListViewItem item in lwImages.Items)
                            {
                                item.ForeColor = Color.Black;
                            }
                        }));
        }

        private void btnTeachingControl_Click(object sender, EventArgs e)
        {
            pbTeach.Value = 0;
            btnDeleteAll.Enabled = false;
            btnDeleteSelected.Enabled = false;
            btnSelectFiles.Enabled = false;
            btnTeachingControl.Enabled = false;
            tsmiClose.Enabled = false;
            tsmiExit.Enabled = false;

            workClass.Teach(imageUrls);
            ThreadPool.QueueUserWorkItem(new WaitCallback(MonitorTeaching));
        }
    }
}
