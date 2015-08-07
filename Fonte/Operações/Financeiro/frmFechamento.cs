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
        private decimal vlrValorInformado, vlrValorSomado,vlrValorTotal;
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
            for (int i = 0; i < FechamentosGrid.Rows.Count; i++)
            {
                vlrValorTotal = vlrValorTotal + decimal.Parse(FechamentosGrid.Rows[i].Cells[1].Value.ToString());
            }

        }
        private void btnExecutar_Click(object sender, EventArgs e)
        {
            

            for (int i = 0; i < FechamentosGrid.Rows.Count; i++)
            {
                vlrValorSomado    =decimal.Parse(FechamentosGrid.Rows[i].Cells[1].Value.ToString());
                vlrValorInformado =decimal.Parse(FechamentosGrid.Rows[i].Cells[2].Value.ToString());
                if (vlrValorSomado != vlrValorInformado)
                {
                     DialogResult resultado = MessageBox.Show("Há uma diferença entre o valor somado e o valor informado do caixa! Deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                     if (resultado == DialogResult.Yes)
                     {
                         CaixaDiferenca caixaD = new CaixaDiferenca()
                         {
                             CodUsuario = Sessions.retunrUsuario.Codigo,
                             Data = DateTime.Now,
                             NumeroCaixa = cbxCaixas.SelectedValue.ToString(),
                             ValorInformado = vlrValorInformado,
                             ValorSomado = vlrValorSomado,
                             ValorDiferenca = vlrValorSomado - vlrValorInformado

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
              DialogResult resultado = MessageBox.Show("Deseja finalizar o Caixa Nº"+ cbxCaixas.SelectedValue.ToString()+" Essa operação não poderá ser desfeita , os movimentos serão lançados apenas no próximo dia! Deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
              if (resultado == DialogResult.Yes)
              {
                  Caixa caixa = new Caixa()
                  {
                      CodUsuario = Sessions.retunrUsuario.Codigo,
                      Data = DateTime.Now,
                      Estado = true,
                      Historico = "Fechamento Caixa",
                      Numero = cbxCaixas.SelectedValue.ToString(),
                      ValorFechamento = vlrValorTotal,

                  };
                  con.Update("spFecharCaixa", caixa);
              }

           
        }
    }
}
