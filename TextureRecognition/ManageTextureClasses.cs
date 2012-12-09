using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ImageRecognition;

namespace TextureRecognition
{
    public partial class ManageTextureClasses : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private static ManageTextureClasses instance;
        private TextureRecognition recognition;
        private Bitmap colorImage, colorImage2;

        public ManageTextureClasses()
        {
            instance = this;

            InitializeComponent();

            regionColor.Color = Color.Red;
            colorImage = new Bitmap(pbColor.Width, pbColor.Height);
            colorImage2 = new Bitmap(pbColor2.Width, pbColor2.Height);
            UpdateColor();

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

        internal static ManageTextureClasses Instance
        {
            get
            {
                return instance;
            }
        }

        private void UpdateColor()
        {
            var render = Graphics.FromImage(colorImage);
            render.FillRectangle(new SolidBrush(regionColor.Color),
                0, 0, pbColor.Width, pbColor.Height);
            pbColor.Image = colorImage;

            if (cbList.SelectedItem != null)
            {
                var color = recognition.Core.GetTextureClass(cbList.SelectedItem.ToString()).RegionColor;
                render = Graphics.FromImage(colorImage2);
                render.FillRectangle(new SolidBrush(color),
                    0, 0, pbColor2.Width, pbColor2.Height);
                pbColor2.Image = colorImage2;
            }
        }

        private void UpdateClassesList()
        {
            cbList.Items.Clear();
            for (int i = 0; i < recognition.Core.TextureClassCount; ++i)
            {
                cbList.Items.Add(recognition.Core.GetTextureClass(i).Name);
            }
            cbList.SelectedIndex = recognition.Core.TextureClassCount - 1;
        }

        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            regionColor.ShowDialog();
            UpdateColor();
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            Hide();
            MainForm.Instance.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Введите название текстурного класса.");
                return;
            }

            try
            {
                recognition.Core.AddTextureClass(tbName.Text, regionColor.Color);
                UpdateClassesList();
            }
            catch (TextureClassDuplicateException ex)
            {
                MessageBox.Show("Текстурный класс с таким именем уже существует. Введите другое имя.");
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cbList.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить выбранный класс?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    recognition.Core.RemoveTextureClass(cbList.SelectedItem.ToString());
                    UpdateClassesList();
                }
            }
        }

        private void btnTeach_Click(object sender, EventArgs e)
        {
            if (cbList.SelectedItem == null)
            {
                MessageBox.Show("Не выбран текстурный класс.");
                return;
            }

            Hide();
            TeachTextureClass.Instance.SetTextureClass(cbList.SelectedItem.ToString());
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти из приложения?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MainForm.Instance.Close();
            }
        }

        private void cbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateColor();
        }        
    }
}
