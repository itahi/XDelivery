using DexComanda.Cadastros;
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
        private Conexao con;
        public string iNumeroCaixaLogado;
        public frmLogin()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            DataSet dsCaixa = con.SelectAll("CaixaCadastro", "spObterCaixa");
            if (dsCaixa.Tables[0].Rows.Count > 0)
            {
                cbxCaixas.DataSource = dsCaixa.Tables[0];
                cbxCaixas.DisplayMember = "Numero";
                cbxCaixas.ValueMember = "Codigo";
            }
            else
            {
                MessageBox.Show("Não há caixas cadastrados , favor efetuar o cadastro para continuar");
                frmCadCaixa frm = new frmCadCaixa();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    Utils.Restart();
                }
            }

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cbxCaixas.Text != "")
            {
                int iNumeroCaixa = int.Parse(cbxCaixas.Text);
                if (Utils.EfetuarLogin(this.txtUsuario.Text.ToString(), this.txtSenha.Text.ToString(), true, iNumeroCaixa,true))
                {
                    if (Utils.CaixaAberto(DateTime.Now,iNumeroCaixa))
                    {
                        frmPrincipal frmPrincipal = new frmPrincipal();
                        this.Hide();
                        frmPrincipal.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione o Caixa para entrar", "[XSistemas] Aviso");
                return;
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

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtSenha.Text != "" && e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnSalvar_Click(sender, e);
            }
        }
    }
}
