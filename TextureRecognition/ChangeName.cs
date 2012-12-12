using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextureRecognition
{
    public partial class ChangeName : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private static ChangeName instance;
        private TextureRecognition recognition;

        public ChangeName()
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

        internal static ChangeName Instance
        {
            get
            {
                return instance;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Введите сначала название класса");
                return;
            }

            if (recognition.Core.GetTextureClass(tbName.Text) != null)
            {
                MessageBox.Show("Имя занято. Введите другое имя.");
                return;
            }

            if (recognition.CurrentClass != null)
            {
                recognition.CurrentClass.Name = tbName.Text;
            }
            Hide();
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnApply_Click(sender, e);
            }
        }

        private void ChangeName_VisibleChanged(object sender, EventArgs e)
        {
            if (recognition.CurrentClass != null)
            {
                tbName.Text = recognition.CurrentClass.Name;
            }
            else
            {
                tbName.Text = "Текстура";
            }
            tbName.Focus();
            tbName.SelectAll();
        }
    }
}
