using DexComanda.Models;
using DexComanda.Relatorios.Caixa;
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
            cbxTurno.SelectedIndex = 0;
        }
        private void ConsultaMovimentoCaixa()
        {
            try
            {
                DataSet dsMovimento = con.SelectCaixaFechamento(iDataFiltro + " 00:00:00", iDataFiltro + " 23:59:59", cbxTurno.Text, "CaixaMovimento");
                if (dsMovimento.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < FechamentosGrid.Columns.Count; i++)
                    {
                        if (FechamentosGrid.Columns[i].HeaderText != "Total Somado")
                        {
                            FechamentosGrid.Columns[i].ReadOnly = false;
                        }
                    }
                    FechamentosGrid.DataSource = null;
                    FechamentosGrid.AutoGenerateColumns = true;
                    FechamentosGrid.DataSource = dsMovimento;
                    FechamentosGrid.DataMember = "CaixaMovimento";
                    FechamentosGrid.Columns["Total Somado"].Visible = false;
                    if (!FechamentosGrid.Columns.Contains("ValorInformado"))
                    {
                        FechamentosGrid.Columns.Add("ValorInformado", "ValorInformado");
                        FechamentosGrid.Refresh();
                    }

                    con.Close();

                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
           

        }
        private void btnExecutar_Click(object sender, EventArgs e)
        {
            if (FechamentosGrid.Rows.Count==0)
            {
                MessageBox.Show("Filtre o caixa que deseja fechar");
                return;
            }
            for (int i = 0; i < FechamentosGrid.Rows.Count; i++)
            {
                if (FechamentosGrid.Rows[i].Cells["ValorInformado"].Value==null|| FechamentosGrid.Rows[i].Cells["ValorInformado"].Value.ToString()=="")
                {
                    MessageBox.Show("Informe o valor de fechamento para cada forma de pagamento");
                    return;
                }
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
                            Tipo = 'E'

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbxCaixas.SelectedIndex<0)
            {
                MessageBox.Show("Selecione o caixa que deseja filtrar");
                cbxCaixas.Focus();
                return;
            }
            if (cbxTurno.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione o turno que deseja filtrar");
                cbxTurno.Focus();
                return;
            }
            iDataFiltro = DateTime.Now.ToShortDateString();
            DataSet dsCaixa = con.RetornaCaixaPorTurno(int.Parse(cbxCaixas.Text), cbxTurno.Text, Convert.ToDateTime(iDataFiltro));
            if (dsCaixa.Tables[0].Rows.Count > 0)
            {

               
                btnExecutar.Enabled = true;
                DataRow dRow = dsCaixa.Tables[0].Rows[0];

                txtDtAbertura.Text = dRow.ItemArray.GetValue(1).ToString();
                txtVlrAbertura.Text = dRow.ItemArray.GetValue(4).ToString();

                txtUFechamento.Text = Sessions.retunrUsuario.Nome;
                // txtVlrAbertura.Text  
                ConsultaMovimentoCaixa();

            }
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
                    Turno = cbxTurno.Text

                };
                con.Update("spFecharCaixa", caixa);
                if (Utils.MessageBoxQuestion("Deseja imprimir o fechamento de caixa ?"))
                {
                    Utils.ImpressaoCaixa(caixa.Turno, Convert.ToDateTime(txtDtAbertura.Text), Convert.ToDateTime(dtFechamento.Text));
                    Application.DoEvents();
                }
                this.Close();
               // Utils.Restart();
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
