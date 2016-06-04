using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações.Funções
{
    public partial class frmLiberação : Form
    {
        public frmLiberação()
        {
            InitializeComponent();
        }

        private void frmLiberação_KeyPress(object sender, KeyPressEventArgs e)
        {

           
        }

        private void frmLiberação_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Autorizacao();
            }
        }
        private void Autorizacao()
        {
            Utils.EfetuarLogin(txtUser.Text, txtSenha.Text, false, 1);
        }
    }
}
