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
    public partial class frmFinalizacaoPedido : Form
    {
        private Conexao con;
        private Boolean bInserir;
        private frmCadastrarPedido iFrm;
        private int intCodPedido;
        public frmFinalizacaoPedido()
        {
            InitializeComponent();
            con = new Conexao();
        }

        private void frmFinalizacaoPedido_Load(object sender, EventArgs e)
        {

        }
        public frmFinalizacaoPedido(decimal iTotalPedido, frmCadastrarPedido frm,Boolean iInserir,int iCodPedido=0)
        {
            try
            {
                InitializeComponent();
                bInserir = iInserir;
                intCodPedido = iCodPedido;
                iFrm = frm;
                lblTotalPedido.Text = iTotalPedido.ToString();
                lblFalta.Text = iTotalPedido.ToString();
              //  intCodPedido = iCodPedido;
                con = new Conexao();
                DataSet dsFPagamento = con.SelectFormasPagamento();

                gridFormasPagamento.DataSource = null;
                gridFormasPagamento.AutoGenerateColumns = true;
                gridFormasPagamento.DataSource = dsFPagamento;
                gridFormasPagamento.DataMember = "FormaPagamento";
                if (dsFPagamento.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < gridFormasPagamento.Columns.Count; i++)
                    {
                        if (gridFormasPagamento.Columns["Codigo"].HeaderText == "Codigo")
                        {
                            gridFormasPagamento.Columns["Codigo"].Visible = false;
                        }
                    }

                    if (!gridFormasPagamento.Columns.Contains("Valor"))
                    {
                        gridFormasPagamento.Columns.Add("Valor", "Valor R$");
                       // gridFormasPagamento.Columns["Valor"].V
                            gridFormasPagamento.Refresh();
                    }
                    gridFormasPagamento.Refresh();
                    
                }

            }
            catch (Exception erro)
            {

                throw;
            }

        }

        private void Finaliza(object sender, EventArgs e)
        {
            try
            {
                decimal iTotalSomado = 0;
                for (int i = 0; i < gridFormasPagamento.Rows.Count; i++)
                {
                    if (gridFormasPagamento.Rows[i].Cells["Valor"].Value != null)
                    {
                        iTotalSomado = iTotalSomado + decimal.Parse(gridFormasPagamento.Rows[i].Cells["Valor"].Value.ToString());
                    }

                }
                if (iTotalSomado < decimal.Parse(lblTotalPedido.Text))
                {
                    MessageBox.Show("Valor devido é maior que o valor informado");
                    return;
                }
                else
                {
                    if (bInserir)
                    {
                        iFrm.btnGerarPedido_Click(sender, e);
                    }
                    
                    for (int intFor = 0; intFor < gridFormasPagamento.Rows.Count; intFor++)
                    {
                        if (gridFormasPagamento.Rows[intFor].Cells["Valor"].Value != null)
                        {
                            if (bInserir)
                            {
                                FinalizaPedido finalizaPed = new FinalizaPedido()
                                {
                                    CodPedido = iFrm.iCodPedido,
                                    CodPagamento = int.Parse(gridFormasPagamento.Rows[intFor].Cells["Codigo"].Value.ToString()),
                                    ValorPagamento = decimal.Parse(gridFormasPagamento.Rows[intFor].Cells["Valor"].Value.ToString())
                                };
                                con.Insert("spAdicionarFinalizaPedido_Pedido", finalizaPed);
                            }
                            else
                            {
                                FinalizaPedido finalizaPed = new FinalizaPedido()
                                {
                                    CodPedido = intCodPedido,
                                    CodPagamento = int.Parse(gridFormasPagamento.Rows[intFor].Cells["Codigo"].Value.ToString()),
                                    ValorPagamento = decimal.Parse(gridFormasPagamento.Rows[intFor].Cells["Valor"].Value.ToString())
                                };
                                con.Update("spAlteraFinalizaPedido_Pedido", finalizaPed);
                            }

                        }


                    }
                    this.Close();
                }
            }
            catch (Exception erro)
            {

                throw;
            }
           

        }
    }
}
