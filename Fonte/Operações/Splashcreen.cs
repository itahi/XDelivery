using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda
{
    public partial class Splashcreen : Form
    {
        public Splashcreen()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (pbCarrega.Value < 100)
            {
                pbCarrega.Value = pbCarrega.Value + 5;
            }
            else
            {
                timer.Enabled = false;
                this.Visible = false;
                if (Sessions.returnConfig != null)
                {
                    frmLogin frmLogin = new frmLogin();
                    frmLogin.ShowDialog();

                }
                else
                {
                    frmConfiguracoes frmConfiguracoes = new frmConfiguracoes();
                    frmConfiguracoes.ShowDialog();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
