using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações
{
    public partial class frmCaixaMovimento : Form
    {
        private Conexao con;
        private DataSet dsMovimentoFiltro;
        public frmCaixaMovimento()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void ExecutaFiltro(object sender, EventArgs e)
        {
            string iTipo="E";
            string iFPagamento="";
            if (rbEntrada.Checked)
	        {
		     iTipo = "E";
	        }
            else if (rbSaida.Checked)
	        {
             iTipo = "S";
	        }
            else if (rbEntradaSaida.Checked)
	        {
		      iTipo = "ES";
	        }

            else
	        {
                MessageBox.Show("Selecione o tipo de movimento para filtrar","[XSistemas");
                return;
	        }

            if (cbxFPagamento.Enabled)
            {
                iFPagamento = cbxFPagamento.SelectedValue.ToString();
            }

            dsMovimentoFiltro = con.SelectCaixaMovimetoFiltro(dtInicio.Value.ToShortDateString() + " 00:00:00", dtFim.Value.ToShortDateString() + " 23:59:59", iTipo, iFPagamento, "CaixaMovimento", cbxNumCaixa.SelectedValue.ToString());

            if (dsMovimentoFiltro.Tables[0].Rows.Count>0)
            {
                MovimentosGridView.DataSource = null;
                MovimentosGridView.AutoGenerateColumns = true;
                MovimentosGridView.DataSource = dsMovimentoFiltro;
                MovimentosGridView.DataMember = "CaixaMovimento";
                con.Close();

                SomaValores();
            }
            else
            {
                MessageBox.Show("Não há registros com o filtro selecionado", "[XSistemas] Aviso");
                return;
            }
          
          
        }
        private void SomaValores()
        {
            decimal vlrSaidas   =0.00M;
            decimal  vlrEntrada =0.00M;
            for (int i = 0; i < MovimentosGridView.Rows.Count; i++)
            {
                if (MovimentosGridView.Rows[i].Cells[6].Value.ToString() == "Entrada")
                {
                    vlrEntrada = vlrEntrada + decimal.Parse(MovimentosGridView.Rows[i].Cells[5].Value.ToString());
                }
                else if (MovimentosGridView.Rows[i].Cells[6].Value.ToString() == "Saida")
                {
                    vlrSaidas = vlrSaidas + decimal.Parse(MovimentosGridView.Rows[i].Cells[5].Value.ToString());
                }
            }

            lblEntradas.Text = vlrEntrada.ToString();
            lblSaidas.Text = vlrSaidas.ToString();
            if (rbEntradaSaida.Checked)
            {
                lblLiquido.Text = Convert.ToString(vlrEntrada - vlrSaidas); 
            }
        }

        private void frmCaixaMovimento_Load(object sender, EventArgs e)
        {
            DataSet dsCaixas = con.SelectAll("CaixaCadastro", "spObterCaixa");
            cbxNumCaixa.DataSource = dsCaixas.Tables["CaixaCadastro"];
            cbxNumCaixa.DisplayMember = "Numero";
            cbxNumCaixa.ValueMember = "Codigo";

            cbxFPagamento.DataSource = con.SelectAll("FormaPagamento", "spObterFormaPagamento").Tables["FormaPagamento"];
            cbxFPagamento.DisplayMember = "Descricao";
            cbxFPagamento.ValueMember = "Codigo";
    
        }

        private void chkFPagamento_CheckedChanged(object sender, EventArgs e)
        {
            cbxFPagamento.Enabled = !chkFPagamento.Checked;
        }
    }
}
