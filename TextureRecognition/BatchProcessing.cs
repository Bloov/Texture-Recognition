using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using ImageRecognition;

namespace TextureRecognition
{
    public partial class BatchProcessing : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private static BatchProcessing instance;
        private TextureRecognition recognition;

        private List<string> imagesToRecognition;
        private Dictionary<string, Bitmap> images;
        private ImageList imageList;

        private List<RecognitionResult> answers;

        public BatchProcessing()
        {
            instance = this;

            InitializeComponent();

            recognition = TextureRecognition.Instance;

            imagesToRecognition = new List<string>();
            images = new Dictionary<string, Bitmap>();
            imageList = new ImageList();
            imageList.ImageSize = new Size(40, 40);
            imageList.ColorDepth = ColorDepth.Depth16Bit;
            lwImages.LargeImageList = imageList;
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

        internal static BatchProcessing Instance
        {
            get
            {
                return instance;
            }
        }

        private void BatchProcessing_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                Focus();
            }
        }

        private void tsmiBack_Click(object sender, EventArgs e)
        {
            Hide();
            RecognizeImage.Instance.Focus();
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти из приложения?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MainForm.Instance.Close();
            }
        }

        private void tsmiOptions_Click(object sender, EventArgs e)
        {
            RecognitionOptions.Instance.ShowDialog();
            Focus();
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

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in openImageDialog.FileNames)
                {
                    if (!imagesToRecognition.Contains(file))
                    {
                        imagesToRecognition.Add(file);
                        var image = new Bitmap(file);
                        images.Add(file, image);
                        imageList.Images.Add(file, image);
                        lwImages.Items.Add(Path.GetFileNameWithoutExtension(file), file);
                    }
                }
            }
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить выбраные изображения?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (ListViewItem item in lwImages.SelectedItems)
                {
                    var file = item.ImageKey;
                    imageList.Images.RemoveByKey(file);
                    var image = images[file];
                    if (image == pbImage.Image)
                    {
                        pbImage.Image = null;
                    }
                    images.Remove(file);
                    imagesToRecognition.Remove(file);
                    lwImages.Items.Remove(item);
                }
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить все образцы?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                imagesToRecognition.Clear();
                lwImages.Items.Clear();
                images.Clear();
                imageList.Images.Clear();
                pbImage.Image = null;
            }
        }

        private void btnRecognitionControl_Click(object sender, EventArgs e)
        {
            if (imagesToRecognition.Count == 0)
            {
                MessageBox.Show("Не выбраны изображения для распознования.");
                return;            
            }

            tsmiBack.Enabled = false;
            tsmiClose.Enabled = false;
            tsmiOptions.Enabled = false;
            btnSelectFiles.Enabled = false;
            btnDeleteSelected.Enabled = false;
            btnDeleteAll.Enabled = false;
            btnRecognitionControl.Enabled = false;
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(RecognizeProcess));
            ThreadPool.QueueUserWorkItem(new WaitCallback(RecognizeControl));
        }

        private void RecognizeProcess(object parameter)
        {
            var list = new List<Bitmap>();
            foreach (var file in imagesToRecognition)
            {
                list.Add(images[file]);
            }
            answers = recognition.Core.RecognizeImages(list);
            this.BeginInvoke(
                new Action(delegate()
                {
                    UpdateResults();
                }));
        }

        private void UpdateResults()
        {
            var glcmDictionary = new Dictionary<TextureClass, int>();
            var lbpDictionary = new Dictionary<TextureClass, int>();
            CountAnswers(answers, glcmDictionary, lbpDictionary);
            var strings = new List<string>();
            strings.Add("Всего образцов: " + imagesToRecognition.Count);
            for (int i = 0; i < recognition.Core.TextureClassCount; ++i)
            {
                var value = recognition.Core.GetTextureClass(i).Name + " - ";
                int count = 0;
                glcmDictionary.TryGetValue(recognition.Core.GetTextureClass(i), out count);
                value += "GLCM: " + count.ToString() + "; ";
                lbpDictionary.TryGetValue(recognition.Core.GetTextureClass(i), out count);
                value += "LBP: " + count.ToString() + "; ";
                strings.Add(value);
            }

            lbResult.Items.Clear();
            lbResult.Items.AddRange(strings.ToArray());
        }

        private void CountAnswers(List<RecognitionResult> results, 
            Dictionary<TextureClass, int> glcm, Dictionary<TextureClass, int> lbp)
        {
            foreach (var answer in results)
            {
                var glcmClass = answer[TextureFeatures.GLCM];
                if (glcm.ContainsKey(glcmClass))
                {
                    glcm[glcmClass] += 1;
                }
                else
                {
                    glcm[glcmClass] = 1;
                }

                var lbpClass = answer[TextureFeatures.LBP];
                if (lbp.ContainsKey(lbpClass))
                {
                    lbp[lbpClass] += 1;
                }
                else
                {
                    lbp[lbpClass] = 1;
                }
            }   
        
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
                    tsmiBack.Enabled = true;
                    tsmiClose.Enabled = true;
                    tsmiOptions.Enabled = true;
                    btnSelectFiles.Enabled = true;
                    btnDeleteSelected.Enabled = true;
                    btnDeleteAll.Enabled = true;
                    btnRecognitionControl.Enabled = true;   
                }));
        }
    }
}
