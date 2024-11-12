using System;
using System.Drawing;
using System.Windows.Forms;

namespace QueueSystem
{
    public partial class Form1 : Form
    {
        private double rho = -1;

        public Form1()
        {
            InitializeComponent();
            SetDefulatProp();
        }

        private void SetDefulatProp()
        {
            txtLs.ReadOnly = true;
            txtLq.ReadOnly = true;
            txtWs.ReadOnly = true;
            txtWq.ReadOnly = true;
            txtRho.ReadOnly = true;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double lambda = double.Parse(txtLambda.Text);
                double mu = double.Parse(txtMho.Text);
                if (mu == 0)
                {
                    throw new DivideByZeroException("لا يمكن ان تكون ميو صفر");
                }
                if (mu <= lambda)
                {
                    throw new SystemUnstableException("لا يمكن ان تكون متساوية القيم");
                }
                rho = lambda / mu;
                txtRho.Text = rho.ToString("F2");

                #region color
                if (rho < 0.5)
                {
                    txtRho.BackColor = Color.LightGreen;
                }
                else if (rho < 0.8)
                {
                    txtRho.BackColor = Color.Orange;
                }
                else
                {
                    txtRho.BackColor = Color.Red;
                }
                #endregion
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                    MessageBox.Show("تأكد من إدخال قيم صحيحة للمدخلات.");
                else if (ex is DivideByZeroException)
                {
                    txtMho.BackColor = Color.Red;
                    MessageBox.Show(ex.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CalcPerformance_Click(object sender, EventArgs e)
        {
            try
            {
                double lambda = double.Parse(txtLambda.Text);
                double mu = double.Parse(txtMho.Text);

                if (lambda >= mu)
                {
                    HighlightTextBox(txtLs, Color.Red);
                    throw new SystemUnstableException("تأكد أن معدل الوصول اكبر من معدل الخدمة");
                }

                double rho = lambda / mu;

                double L = rho / (1 - rho);
                double Lq = Math.Pow(rho, 2) / (1 - rho);
                double W = 1 / (mu - lambda);
                double Wq = rho / (mu - lambda);

                txtLs.Text = L.ToString("F2");//F2 to show only two number after point 
                txtLq.Text = Lq.ToString("F2");
                txtWs.Text = W.ToString("F2");
                txtWq.Text = Wq.ToString("F2");

            }
            catch (Exception ex)
            {
                if (ex is FormatException || ex is DivideByZeroException)
                {
                    MessageBox.Show("تأكد من إدخال قيم صحيحة للمدخلات.");
                }
                else if (ex is SystemUnstableException)
                {
                    MessageBox.Show("تأكد أن معدل الوصول اكبر من معدل الخدمة");
                }
            }
        }

        private Color GetColorBasedOnValue(double value, double threshold)
        {
            if (value < threshold)
            {
                return Color.LightGreen;
            }
            else if (value < threshold * 2)
            {
                return Color.Orange;
            }
            else
            {
                return Color.Red;
            }
        }

        private void HighlightTextBox(TextBox textBox, Color color)
        {
            textBox.BackColor = color;
        }

        private void CalcP0_Click(object sender, EventArgs e)
        {
            if (rho == -1)
            {
                throw new Exception("the rho is Negtive");
            }
            txtP0.Text = (1 - rho).ToString();
        }

        private void ClacPn_Click(object sender, EventArgs e)
        {
            try
            {
                if (rho == -1)
                {
                    throw new Exception("يجب حساب قيمة Rho أولاً.");
                }

                double n = double.Parse(txtN.Text); // افترض أن هناك صندوق نص لإدخال قيمة n
                double Pn = Math.Pow(rho, n) * (1 - rho);
                txtPn.Text = Pn.ToString("F2");
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    MessageBox.Show("تأكد من إدخال قيمة صحيحة لـ n.");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}

     