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

        private void tsmiOptions_Click(object sender, EventArgs e)
        {
            var result = RecognitionOptions.Instance.ShowDialog();
            Focus();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                coreC = coreD = coreF = null;
                workSampleC = workSampleD = workSampleF = null;
                workImageC = workImageD = workImageF = null;

                lbOutput.Items.Add("Настройки изменены");
                lbOutput.SelectedIndex = lbOutput.Items.Count - 1;
            }
        }        
        
        private RecognitionSample sample;
        private Bitmap workImageC;
        private RecognitionCore coreC;        
        private RecognitionSample workSampleC;
        private Bitmap workImageD;
        private RecognitionCore coreD;  
        private RecognitionSample workSampleD;
        private Bitmap workImageF;
        private RecognitionCore coreF;        
        private RecognitionSample workSampleF;
        
        private List<List<string>> teachingSamples;
        private List<string> workSamples;
        private bool complete;

        private void btnSelectSample_Click(object sender, EventArgs e)
        {
            if (openSample.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                workSamples = new List<string>();
                workSamples.AddRange(openSample.FileNames);
                lbOutput.Items.Add("Загружено " + workSamples.Count.ToString() + " примеров.");

                SelectSample(openSample.FileName);                
            }            
        }

        private void SelectSample(string file)
        {
            sample = new RecognitionSample(recognition.Core);
            sample.LoadSample(file);
            this.BeginInvoke(
               new Action(delegate()
               {
                   lbOutput.Items.Add("Загружен пример " + sample.Path + " (блоков: " +
                       sample.Fragments.ToString() + ")");
                   pbSample.Image = sample.GetSampleImage();
               }));              

            workSampleC = workSampleD = workSampleF = null;
            workImageC = workImageD = workImageF = null;            
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
                lbOutput.Items.Add("Добавлено " + teachingSamples[index].Count + " примеров в класс " +
                    recognition.Core.GetTextureClass(index).Name);

                coreC = coreD = coreF = null;
                workSampleC = workSampleD = workSampleF = null;
                workImageC = workImageD = workImageF = null;
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

        private void btnCompact_Click(object sender, EventArgs e)
        {
            msMenu.Enabled = false;
            btnSelectSample.Enabled = false;
            btnSelectSamples.Enabled = false;
            cbTextureClass.Enabled = false;
            btnCompact.Enabled = false;
            btnDischarged.Enabled = false;
            btnFull.Enabled = false;

            lbOutput.Items.Add(" ");
            ThreadPool.QueueUserWorkItem(new WaitCallback(RecognizeThread), 2);
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

            lbOutput.Items.Add(" ");
            ThreadPool.QueueUserWorkItem(new WaitCallback(RecognizeThread), 3);
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

            lbOutput.Items.Add(" ");
            ThreadPool.QueueUserWorkItem(new WaitCallback(RecognizeThread), 1);       
        }

        private void RecognizeThread(object param)
        {
            var id = (int)param;

            RecognitionCore core = null;
            RecognitionSample currentSample = null;
            Bitmap currentImage = null;            
            string pref = "";

            switch (id)
            {
                case 1:
                    pref = "    (Полная выборка)";
                    core = coreF;
                    currentSample = workSampleF;
                    currentImage = workImageF;
                    break;

                case 2:
                    pref = "    (Компактная выборка)";
                    core = coreC;
                    currentSample = workSampleC;
                    currentImage = workImageC;
                    break;

                case 3:
                    pref = "    (Разряженная выборка)";
                    core = coreD;
                    currentSample = workSampleD;
                    currentImage = workImageD;
                    break;
                default:
                    break;
            }

            if (core == null)
            {
                core = new RecognitionCore();
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
                            lbOutput.SelectedIndex = lbOutput.Items.Count - 1;
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
                    
                    var result = this.BeginInvoke(
                        new Action(delegate()
                        {
                            var count = currentClass.KnownSamplesNumber(TextureFeatures.GLCM);
                            var part = count / (float)teachingSamples[i].Count;
                            lbOutput.Items.Add("    После обучения получено образцов GLCM: " + 
                                count.ToString() + " (" + part.ToString() + ")");

                            count = currentClass.KnownSamplesNumber(TextureFeatures.LBP);
                            part = count / (float)teachingSamples[i].Count;
                            lbOutput.Items.Add("    После обучения получено образцов LBP: " + 
                                count.ToString() + " (" + part.ToString() + ")");

                            lbOutput.SelectedIndex = lbOutput.Items.Count - 1;
                        }));
                    while (!result.IsCompleted)
                    {
                        Thread.Sleep(50);
                    }
                }
            }

            switch (id)
            {
                case 1:
                    coreF = core;
                    break;
                case 2:
                    coreC = core;
                    break;
                case 3:
                    coreD = core;
                    break;
                default:
                    break;
            }

            
            if (currentSample == null)
            {
                this.BeginInvoke(
                    new Action(delegate()
                    {
                        lbOutput.Items.Add("Начато распознавание тестового изображения");
                        lbOutput.SelectedIndex = lbOutput.Items.Count - 1;
                    }));

                currentSample = new RecognitionSample(core, sample.Path);
                currentSample.Recognize();
                currentImage = currentSample.GetSampleImage();
                switch (id)
                {
                    case 1:
                        workSampleF = currentSample;
                        workImageF = currentImage;
                        break;
                    case 2:
                        workSampleC = currentSample;
                        workImageC = currentImage;
                        break;
                    case 3:
                        workSampleD = currentSample;
                        workImageD = currentImage;
                        break;
                    default:
                        break;
                }
            }

            var iRes = this.BeginInvoke(
                new Action(delegate()
                {
                    lbOutput.Items.Add(currentSample.Path);
                    var result = sample.CompareFeature(currentSample, TextureFeatures.GLCM);
                    lbOutput.Items.Add(pref + " Соответствие GLCM = " + result.ToString());
                    result = sample.CompareFeature(currentSample, TextureFeatures.LBP);
                    lbOutput.Items.Add(pref + " Соответствие LBP = " + result.ToString());
                    lbOutput.SelectedIndex = lbOutput.Items.Count - 1;

                    pbWork.Image = currentImage;

                    msMenu.Enabled = true;
                    btnSelectSample.Enabled = true;
                    btnSelectSamples.Enabled = true;
                    cbTextureClass.Enabled = true;
                    btnCompact.Enabled = true;
                    btnDischarged.Enabled = true;
                    btnFull.Enabled = true;                    
                }));
            while (!iRes.IsCompleted)
            {
                Thread.Sleep(20);
            }

            complete = true;
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            btnAuto.Enabled = false;
            ThreadPool.QueueUserWorkItem(new WaitCallback(AutoThread));       
        }   
     
        private void AutoThread(object param)
        {
            if (workSamples == null)
            {
                return;
            }

            var factor = 0.2;
            for (int j = 0; j < 5; ++j)
            {
                coreC = coreD = coreF = null;
                RecognitionParameters.CompactFactor = factor;
                RecognitionParameters.DischargeFactor = factor;
                complete = true;
                for (int i = 0; i < workSamples.Count; ++i)
                {
                    SelectSample(workSamples[i]);

                    complete = false;
                    this.BeginInvoke(
                       new Action(delegate()
                       {
                           btnCompact_Click(null, null);
                       }));
                    while (!complete)
                    {
                        Thread.Sleep(200);
                    }

                    complete = false;
                    this.BeginInvoke(
                       new Action(delegate()
                       {
                           btnDischarged_Click(null, null);
                       }));
                    while (!complete)
                    {
                        Thread.Sleep(200);
                    }

                    /*complete = false;
                    this.BeginInvoke(
                       new Action(delegate()
                       {
                           btnFull_Click(null, null);
                       }));
                    while (!complete)
                    {
                        Thread.Sleep(200);
                    }*/
                }

                factor += 0.05;
            }
            this.BeginInvoke(
               new Action(delegate()
               {
                   btnAuto.Enabled = true;                           
               }));    
        }
    }
}
