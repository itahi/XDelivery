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
            cbxFormaPagamento.DataSource = con.SelectAll("FormaPagamento", "spObterFormaPagamento").Tables["FormaPagamento"];
            cbxFormaPagamento.DisplayMember = "Descricao";
            cbxFormaPagamento.ValueMember = "Codigo";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Utils.CaixaAberto(Convert.ToDateTime(dtMovimento.Value.ToShortDateString()), int.Parse(txtNumCaixa.Text)))
            {
                CaixaMovimento cxMovimento = new CaixaMovimento()
                {
                    CodCaixa = int.Parse(txtNumCaixa.Text),
                    CodFormaPagamento = int.Parse(cbxFormaPagamento.SelectedValue.ToString()),
                    Data = Convert.ToDateTime(dtMovimento.Value.ToShortDateString()),
                    Historico = txtDescricao.Text,
                    Valor = decimal.Parse(txtValor.Text),
                    NumeroDocumento = txtDocumento.Text

                };
                if (rbEntrada.Checked)
                {
                    cxMovimento.Tipo = 'E';
                }
                else if ( rbSaida.Checked)
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

        private void frmLancamentoCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                btnSalvar_Click(sender, e);
            }
            
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        
    }
}
