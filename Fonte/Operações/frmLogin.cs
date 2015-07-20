using DexComanda.Models;
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
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
            
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Utils.EfetuarLogin(this.txtUsuario.Text.ToString(), this.txtSenha.Text.ToString()))
            {
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Utils.Kill();
            this.Dispose();
        }

        private void frmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                btnSalvar_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
