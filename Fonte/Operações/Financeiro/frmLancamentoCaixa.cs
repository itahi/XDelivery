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

namespace DexComanda.Cadastros
{
    public partial class frmLancamentoCaixa : Form
    {
        private Conexao con;
        public frmLancamentoCaixa()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void frmLancamentoCaixa_Load(object sender, EventArgs e)
        {
            DataSet dsCaixas = con.SelectAll("CaixaCadastro", "spObterCaixa");
            cbxCaixas.DataSource = dsCaixas.Tables["CaixaCadastro"];
            cbxCaixas.DisplayMember = "Numero";
            cbxCaixas.ValueMember = "Codigo";
            
            
            cbxFormaPagamento.DataSource = con.SelectAll("FormaPagamento", "spObterFormaPagamento").Tables["FormaPagamento"];
            cbxFormaPagamento.DisplayMember = "Descricao";
            cbxFormaPagamento.ValueMember = "Codigo";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
           
        }

        private void frmLancamentoCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                Salvar(sender, e);
            }
            
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Salvar(object sender, EventArgs e)
        {
            int intNumCaixa=1;
            if (cbxCaixas.SelectedText.ToString()!="")
            {
                intNumCaixa =int.Parse(cbxCaixas.SelectedText.ToString());
            }
            if (Utils.CaixaAberto(Convert.ToDateTime(dtMovimento.Value.ToShortDateString()), intNumCaixa))
            {
                CaixaMovimento cxMovimento = new CaixaMovimento()
                {
                    CodCaixa = intNumCaixa,
                    CodFormaPagamento = int.Parse(cbxFormaPagamento.SelectedValue.ToString()),
                    Data = Convert.ToDateTime(dtMovimento.Value.ToShortDateString()),
                    CodUser = Sessions.retunrUsuario.Codigo,
                    Historico = txtDescricao.Text,
                    Valor = decimal.Parse(txtValor.Text),
                    NumeroDocumento = txtDocumento.Text

                };
                if (intNumCaixa.ToString() == "" || txtValor.Text == "")
                {
                    MessageBox.Show("Campos obrigatórios não podem ficar vazios");
                    return;
                }
                if (rbEntrada.Checked)
                {
                    cxMovimento.Tipo = 'E';
                }
                else if (rbSaida.Checked)
                {
                    cxMovimento.Tipo = 'S';
                }
                else
                {
                    MessageBox.Show("Selecione o tipo de movimento", "[XSistemas] Segurança");
                    return;
                }

                con.Insert("spInserirMovimentoCaixa", cxMovimento);
                MessageBox.Show("Movimento lançado", "[Xsistemas] Aviso");
                Utils.LimpaForm(this);

            }
            else
            {
                MessageBox.Show("Caixa do dia " + dtMovimento.Value.ToString() + " já foi fechado , lançamento não permitido", "[Xsistemas] Aviso");

            }
        }
        
    }
}
