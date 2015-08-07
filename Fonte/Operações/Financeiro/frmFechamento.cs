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
    public partial class frmCaixaFechamento : Form
    {
        private Conexao con;
        public frmCaixaFechamento()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void frmCaixaFechamento_Load(object sender, EventArgs e)
        {
            cbxCaixas.DataSource = con.SelectAll("Caixa", "spObterCaixaAberto").Tables["Caixa"];
            cbxCaixas.DisplayMember = "Numero";
            cbxCaixas.ValueMember = "Codigo";
        }

        private void FiltraCaixa(object sender, EventArgs e)
        {
            DataSet dsCaixa = con.SelectRegistroPorCodigo("Caixa", "spObterDadosCaixaPorCodigo", int.Parse(cbxCaixas.SelectedValue.ToString()));
            if (dsCaixa.Tables[0].Rows.Count > 0)
            {
                DataRow dRow = dsCaixa.Tables[0].Rows[0];

                txtDtAbertura.Text = dRow.ItemArray.GetValue(1).ToString();
                txtVlrAbertura.Text = dRow.ItemArray.GetValue(4).ToString();
                txtUAbertura.Text = dRow.ItemArray.GetValue(7).ToString();

                txtUFechamento.Text = Sessions.retunrUsuario.Nome;
                // txtVlrAbertura.Text  
                ConsultaMovimentoCaixa();
            }
        }
        private void ConsultaMovimentoCaixa()
        {
            DataSet dsMovimento = con.SelectRegistroPorCodigo("CaixaMovimento", "spTotaisCaixa", int.Parse(cbxCaixas.SelectedValue.ToString()));

            if (dsMovimento.Tables[0].Rows.Count > 0)
            {
                FechamentosGrid.DataSource = null;
                FechamentosGrid.AutoGenerateColumns = true;
                FechamentosGrid.DataSource = dsMovimento;
                FechamentosGrid.DataMember = "CaixaMovimento";

                for (int i = 0; i < FechamentosGrid.Columns.Count; i++)
                {
                    if (FechamentosGrid.Columns[i].HeaderText == "CodCaixa")
                    {
                        FechamentosGrid.Columns.RemoveAt(i);
                    }
                }

                //DataGridViewColumn Coluna = new DataGridViewColumn();
                FechamentosGrid.Columns.Add("ValorInformado", "ValorInformado");
                FechamentosGrid.Refresh();
                con.Close();
            }

        }

        private void cbxCaixas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
