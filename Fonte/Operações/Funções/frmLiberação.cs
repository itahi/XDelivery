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

namespace DexComanda.Operações.Funções
{
    public partial class frmLiberação : Form
    {
        private string PermissaoConsultada;
        public Boolean Autorizacao;
        private Usuario user;
        public int CodUser;
        public frmLiberação()
        {
            InitializeComponent();
            Conexao con;
        }

        public frmLiberação(string iNOmePermissao)
        {
            InitializeComponent();
            PermissaoConsultada = iNOmePermissao;

            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowDialog();
        }
        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            user = Utils.RetornaDadosUsuario(txtUser.Text, txtSenha.Text);
            CodUser = user.Codigo;
            Autorizacao = Utils.ValidaPermissao(user.Codigo, PermissaoConsultada);
            this.Hide();
        }

        private void frmLiberação_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAutorizar_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
