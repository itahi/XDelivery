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
    public partial class frmAlteraSenha : Form
    {
        private Conexao con;
        public frmAlteraSenha()
        {
            InitializeComponent();
            con = new Conexao();
        }

        private void frmAlteraSenha_Load(object sender, EventArgs e)
        {
            DataSet dsUsuarios = con.SelectAll("Usuario", "spObterUsuario");
            cbxLogin.DataSource = dsUsuarios.Tables[0];
            cbxLogin.ValueMember = "Codigo";
            cbxLogin.DisplayMember = "Nome";

            cbxLogin.Enabled = Sessions.retunrUsuario.AdministradorSN;
            cbxLogin.Text = Sessions.retunrUsuario.Nome;

        }

        private void btnAlteraSenha_Click(object sender, EventArgs e)
        {
            if (txtSenhaAntiga.Text !="" && txtSenhanova.Text !="")
            {
                string iSenhaAntiga = Sessions.retunrUsuario.Senha;

                if (Utils.EncryptMd5(cbxLogin.Text, txtSenhaAntiga.Text) == iSenhaAntiga)
                {
                    AlteraSenha user = new AlteraSenha()
                    {
                        Codigo = Sessions.retunrUsuario.Codigo,
                        Senha = Utils.EncryptMd5(cbxLogin.Text, txtSenhanova.Text)
                    };

                    con.Update("spAlterarSenha", user);
                    MessageBox.Show("Senha alterada");
                    Utils.Restart();
                }
                else
                {
                    MessageBox.Show("Senha anterior não confere");
                }
                
            }
        }
    }
}
