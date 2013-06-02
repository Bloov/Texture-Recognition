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
            teachingSamples = new List<List<string>>();            
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
        
        private RecognitionSample sample;
        private RecognitionSample workSample;
        private List<List<string>> teachingSamples;

        private void btnSelectSample_Click(object sender, EventArgs e)
        {
            if (openSample.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sample = new RecognitionSample(recognition.Core);
                sample.LoadSample(openSample.FileName);
                lbOutput.Items.Add("Загружен пример " + sample.Path + " (блоков: " +
                    sample.Fragments.ToString() + ")");
            }
        }

        private void UpdateClasses()
        {
            cbTextureClass.Items.Clear();
            teachingSamples.Clear();
            for (int i = 0; i < recognition.Core.TextureClassCount; ++i)
            {
                cbTextureClass.Items.Add(recognition.Core.GetTextureClass(i).Name);
                teachingSamples.Add(new List<string>());
            }
            cbTextureClass.SelectedIndex = 0;
        }

        private void btnSelectSamples_Click(object sender, EventArgs e)
        {
            if (openImages.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var index = cbTextureClass.SelectedIndex;
                teachingSamples[index].Clear();
                teachingSamples[index].AddRange(openImages.FileNames);
                lbSamples.Items.Clear();                
                lbSamples.Items.AddRange(teachingSamples[index].ToArray());
                lbOutput.Items.Add("Добавлено " + teachingSamples[index].Count + " примеров в класс " +
                    recognition.Core.GetTextureClass(index).Name);
            }
        }

        private void cbTextureClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = cbTextureClass.SelectedIndex;
            if (index >= 0)
            {
                lbSamples.Items.Clear();
                lbSamples.Items.AddRange(teachingSamples[index].ToArray());
            }
        }

        private void OptimizeTeaching_VisibleChanged(object sender, EventArgs e)
        {
            UpdateClasses();
        }

        private List<Bitmap> GetImagesToTeach(List<string> files)
        {
            var result = new List<Bitmap>();
            for (int i = 0; i < files.Count; ++i)
            {
                result.Add(new Bitmap(files[i]));
            }
            return result;
        }

        private void btnFull_Click(object sender, EventArgs e)
        {
            msMenu.Enabled = false;
            btnSelectSample.Enabled = false;
            btnSelectSamples.Enabled = false;
            cbTextureClass.Enabled = false;
            btnCompact.Enabled = false;
            btnDischarged.Enabled = false;
            btnFull.Enabled = false;            

            ThreadPool.QueueUserWorkItem(new WaitCallback(RecognizeThread), 1);       
        }

        private void btnDischarged_Click(object sender, EventArgs e)
        {
            msMenu.Enabled = false;
            btnSelectSample.Enabled = false;
            btnSelectSamples.Enabled = false;
            cbTextureClass.Enabled = false;
            btnCompact.Enabled = false;
            btnDischarged.Enabled = false;
            btnFull.Enabled = false;

            ThreadPool.QueueUserWorkItem(new WaitCallback(RecognizeThread), 3);  
        }

        private void btnCompact_Click(object sender, EventArgs e)
        {
            msMenu.Enabled = false;
            btnSelectSample.Enabled = false;
            btnSelectSamples.Enabled = false;
            cbTextureClass.Enabled = false;
            btnCompact.Enabled = false;
            btnDischarged.Enabled = false;
            btnFull.Enabled = false;

            ThreadPool.QueueUserWorkItem(new WaitCallback(RecognizeThread), 2);  
        }

        private void RecognizeThread(object param)
        {
            var id = (int)param;
            string pref = "";
            switch (id)
            {
                case 1:
                    pref = "(Полная выборка)";
                    break;

                case 2:
                    pref = "(Компактная выборка)";
                    break;

                case 3:
                    pref = "(Разряженная выборка)";
                    break;
                default:
                    break;
            }

            var core = new RecognitionCore();
            for (int i = 0; i < recognition.Core.TextureClassCount; ++i)
            {
                var their = recognition.Core.GetTextureClass(i);
                core.AddTextureClass(their.Name, their.RegionColor);
            }

            for (int i = 0; i < core.TextureClassCount; ++i)
            {
                var currentClass = core.GetTextureClass(i);
                this.BeginInvoke(
                    new Action(delegate()
                    {
                        lbOutput.Items.Add("Начато обучение классу " + currentClass.Name);
                    }));
                switch (id)
                {
                    case 1:
                        currentClass.Teach(teachingSamples[i], GetImagesToTeach(teachingSamples[i]));
                        break;

                    case 2:
                        currentClass.TeachCompact(teachingSamples[i], GetImagesToTeach(teachingSamples[i]));
                        break;

                    case 3:
                        currentClass.TeachDischarged(teachingSamples[i], GetImagesToTeach(teachingSamples[i]));
                        break;

                    default:
                        currentClass.Teach(teachingSamples[i], GetImagesToTeach(teachingSamples[i]));
                        break;
                }                
                while (currentClass.IsTeaching)
                {
                    Thread.Sleep(200);
                }
            }

            this.BeginInvoke(
                new Action(delegate()
                {
                    lbOutput.Items.Add("Начато распознавание тестового изображения");
                }));
            workSample = new RecognitionSample(core, sample.Path);
            workSample.Recognize();

            this.BeginInvoke(
                new Action(delegate()
                {
                    var result = sample.CompareFeature(workSample, TextureFeatures.GLCM);
                    lbOutput.Items.Add(pref + " Соответствие GLCM = " + result.ToString());
                    result = sample.CompareFeature(workSample, TextureFeatures.LBP);
                    lbOutput.Items.Add(pref + " Соответствие LBP = " + result.ToString());

                    msMenu.Enabled = true;
                    btnSelectSample.Enabled = true;
                    btnSelectSamples.Enabled = true;
                    cbTextureClass.Enabled = true;
                    btnCompact.Enabled = true;
                    btnDischarged.Enabled = true;
                    btnFull.Enabled = true;
                }));    
        }        
    }
}
