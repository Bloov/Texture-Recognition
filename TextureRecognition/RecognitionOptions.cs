using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImageRecognition;

namespace TextureRecognition
{
    public partial class RecognitionOptions : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private static RecognitionOptions instance;

        public RecognitionOptions()
        {
            instance = this;
            
            InitializeComponent();
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

        internal static RecognitionOptions Instance
        {
            get
            {
                return instance;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            var iValue = 0;
            var dValue = 0.0;
            if (int.TryParse(tbFragmentSize.Text, out iValue))
            {
                RecognitionParameters.RecognitionFragmentSize = iValue;
                tbFragmentSize.Text = RecognitionParameters.RecognitionFragmentSize.ToString();
            }
            if (int.TryParse(tbNeighbors.Text, out iValue))
            {
                RecognitionParameters.NeededNeighborsNumber = iValue;
                tbNeighbors.Text = RecognitionParameters.NeededNeighborsNumber.ToString();
            }
            if (double.TryParse(tbDeviation.Text, out dValue))
            {
                RecognitionParameters.GLCMDeviationWeight = dValue;
                tbDeviation.Text = RecognitionParameters.GLCMDeviationWeight.ToString("F3");
            }
            if (double.TryParse(tbCompactFacotor.Text, out dValue))
            {
                RecognitionParameters.CompactFactor = dValue;
            }
            if (double.TryParse(tbDischargeFactor.Text, out dValue))
            {
                RecognitionParameters.DischargeFactor = dValue;
            }
            Hide();
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void RecognitionOptions_VisibleChanged(object sender, EventArgs e)
        {
            lblFragmentSize.Text = "Размер фрагмента для распознования\n(не менее " +
                RecognitionParameters.FragmentsSize.ToString() + " )";
            tbNeighbors.Text = RecognitionParameters.NeededNeighborsNumber.ToString();
            tbDeviation.Text = RecognitionParameters.GLCMDeviationWeight.ToString("F3");
            tbFragmentSize.Text = RecognitionParameters.RecognitionFragmentSize.ToString();
            tbCompactFacotor.Text = RecognitionParameters.CompactFactor.ToString("F3");
            tbDischargeFactor.Text = RecognitionParameters.DischargeFactor.ToString("F3");
            tbFragmentSize.SelectAll();
            tbFragmentSize.Focus();
        }

        private void tbFragmentSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == 13)
                {
                    tbDeviation.SelectAll();
                    tbDeviation.Focus();
                }
                return;
            }
            e.Handled = true;
        }

        private void tbRejectsLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || (e.KeyChar == ',') || char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == 13)
                {
                    tbNeighbors.SelectAll();
                    tbNeighbors.Focus();
                }
                return;
            }
            e.Handled = true;
        }

        private void tbNeighbors_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                btnApply.Focus();
                return;
            }
            e.Handled = true;
        }

        private void tbFragmentSize_Leave(object sender, EventArgs e)
        {
            var size = 0;
            if (!int.TryParse(tbFragmentSize.Text, out size))
            {
                MessageBox.Show("Недопустимое значение размера фрагмента. Повторите ввод.");
                tbFragmentSize.SelectAll();
                tbFragmentSize.Focus();
            }
        }

        private void tbRejectsLevel_Leave(object sender, EventArgs e)
        {
            var level = 0.0;
            if (!double.TryParse(tbDeviation.Text, out level))
            {
                MessageBox.Show("Недопустимое значение уровня. Повторите ввод.");
                tbDeviation.SelectAll();
                tbDeviation.Focus();
            }
        }

        private void tbNeighbors_Leave(object sender, EventArgs e)
        {
            var count = 0;
            if (!int.TryParse(tbNeighbors.Text, out count))
            {
                MessageBox.Show("Недопустимое значение числа соседей. Повторите ввод.");
                tbNeighbors.SelectAll();
                tbNeighbors.Focus();
            }
        }
    }
}
