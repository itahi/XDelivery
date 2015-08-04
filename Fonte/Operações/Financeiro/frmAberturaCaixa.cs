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

namespace DexComanda.Operações.Financeiro
{
    public partial class frmAberturaCaixa : Form
    {
        private Conexao con;
        private int CodUser ;
        public frmAberturaCaixa()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void frmAberturaCaixa_Load(object sender, EventArgs e)
        {
            dtAbertura.Value = DateTime.Now;
            if (Sessions.retunrUsuario!=null)
            {
                DataSet dsUser = con.SelectAll("Usuario", "spObterUsuario");
                cbxFuncionario.DataSource = dsUser.Tables["Usuario"];
                cbxFuncionario.DisplayMember = "Nome";
                cbxFuncionario.ValueMember = "Codigo";

            }
           
        }

        private void Salvar(object sender, EventArgs e)
        {
            if (Utils.EfetuarLogin(cbxFuncionario.Text, txtSenha.Text,false))
            {
                Caixa caixa = new Caixa()
                {
                    Data = dtAbertura.Value,
                    Estado = false /*Caixa Aber*/,
                    Historico = "Abertura Inicial",
                    ValorAbertura = decimal.Parse(txtValor.Text.Replace(",", ".")),
                    Numero     = txtNumCaixa.Text

                };
                if (CodUser != 0)
                {
                    caixa.CodUsuario = CodUser;
                    if (!Utils.CaixaAberto(DateTime.Now,int.Parse(txtNumCaixa.Text)))
                    {
                        con.Insert("spAbrirCaixa", caixa);
                        MessageBox.Show("Caixa aberto", "[XSistemas] Aviso");
                        Utils.Restart();
                    }
                    else
                    {
                        MessageBox.Show("Caixa já esta aberto", "[XSistemas] Aviso");
                    }
                   
                }
                else
                {
                    MessageBox.Show("Usuario não selecionado", "[XSistemas] Aviso");
                }

            }
            
         }
            
           

        private void cbxFuncionario_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CodUser = int.Parse(cbxFuncionario.SelectedValue.ToString());
        }
    }
}
