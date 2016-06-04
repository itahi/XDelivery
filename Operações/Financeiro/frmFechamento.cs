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
    public partial class frmCaixaFechamento : Form
    {
        private Conexao con;
        private string iDataFiltro;
        private decimal vlrValorInformado, vlrValorSomado, vlrValorTotal;
        public frmCaixaFechamento()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void frmCaixaFechamento_Load(object sender, EventArgs e)
        {

        }

        private void FiltraCaixa(object sender, EventArgs e)
        {
            iDataFiltro = DateTime.Now.ToShortDateString();//Convert.ToDateTime(txtDtAbertura.Text + " 23:59:59");
            DataSet dsCaixa = con.SelectRegistroPorDataCodigo("Caixa", "spObterDadosCaixaPorCodigo", Convert.ToDateTime(iDataFiltro), int.Parse(cbxCaixas.Text));
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
            decimal vlrEntrada = 0.00M;
            decimal vlrSaida = 0.00M;
            DataSet dsMovimento = con.SelectCaixaFechamento(iDataFiltro + " 00:00:00", iDataFiltro + " 23:59:59", cbxCaixas.Text, "CaixaMovimento");

            if (dsMovimento.Tables[0].Rows.Count > 0)
            {

                FechamentosGrid.DataSource = null;
                FechamentosGrid.AutoGenerateColumns = true;
                FechamentosGrid.DataSource = dsMovimento;
                FechamentosGrid.DataMember = "CaixaMovimento";

                for (int i = 0; i < FechamentosGrid.Rows.Count; i++)
                {
                    if (FechamentosGrid.Rows[i].Cells["Tipo Movimento"].Value.ToString() == "Entradas")
                    {
                        vlrEntrada = vlrEntrada + decimal.Parse(FechamentosGrid.Rows[i].Cells["Total Somado"].Value.ToString());
                    }
                    else
                    {
                        vlrSaida = vlrSaida + decimal.Parse(FechamentosGrid.Rows[i].Cells[2].Value.ToString());
                    }
                }


                vlrValorTotal = vlrEntrada - vlrSaida;

                if (!FechamentosGrid.Columns.Contains("ValorInformado"))
                {
                    FechamentosGrid.Columns.Add("ValorInformado", "ValorInformado");
                    
                    FechamentosGrid.Refresh();

                }

                con.Close();
            }

        }
        private void btnExecutar_Click(object sender, EventArgs e)
        {


            for (int i = 0; i < FechamentosGrid.Rows.Count; i++)
            {
                vlrValorSomado = decimal.Parse(FechamentosGrid.Rows[i].Cells["Total Somado"].Value.ToString());
                vlrValorInformado = decimal.Parse(FechamentosGrid.Rows[i].Cells["ValorInformado"].Value.ToString());
                if (vlrValorSomado != vlrValorInformado)
                {
                    DialogResult resultado = MessageBox.Show("Há uma diferença entre o valor somado e o valor informado do caixa! Deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        CaixaDiferenca caixaD = new CaixaDiferenca()
                        {
                            CodUsuario = Sessions.retunrUsuario.Codigo,
                            Data = DateTime.Now,
                            NumeroCaixa = cbxCaixas.Text,
                            ValorInformado = vlrValorInformado,
                            ValorSomado = vlrValorSomado,
                            ValorDiferenca = vlrValorSomado - vlrValorInformado,
                            Tipo = Convert.ToChar(FechamentosGrid.Rows[i].Cells["Tipo Movimento"].Value.ToString().Substring(0, 1))

                        };
                        con.Insert("spAdicionaDiferenca", caixaD);
                        vlrValorTotal = vlrValorTotal + vlrValorSomado;
                    }
                    else
                    {
                        return;
                    }
                }

            }
            FechaCaixa();
        }
        private void FechaCaixa()
        {
            DialogResult resultado = MessageBox.Show("Deseja finalizar o Caixa Nº: " + cbxCaixas.Text + " Essa operação não poderá ser desfeita , os movimentos serão lançados apenas no próximo dia! Deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                Caixa caixa = new Caixa()
                {
                    CodUsuario = Sessions.retunrUsuario.Codigo,
                    Data = DateTime.Now,
                    Estado = true,
                    Historico = "Fechamento Caixa",
                    Numero = cbxCaixas.Text,
                    ValorFechamento = vlrValorTotal,

                };
                con.Update("spFecharCaixa", caixa);
                Utils.Restart();
            }

        }

        private void cbxCaixas_Click(object sender, EventArgs e)
        {
            cbxCaixas.DataSource = con.SelectAll("CaixaCadastro", "spObterCaixa").Tables["CaixaCadastro"];
            cbxCaixas.DisplayMember = "Numero";
            cbxCaixas.ValueMember = "Codigo";
        }
    }
}
