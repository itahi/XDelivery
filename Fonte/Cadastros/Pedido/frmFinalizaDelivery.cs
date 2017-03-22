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

namespace DexComanda.Cadastros.Pedido
{
    public partial class frmFinalizaDelivery : Form
    {
        private Conexao con;
        private int intCodPedido;
        public frmFinalizaDelivery()
        {
            InitializeComponent();
        }
        public frmFinalizaDelivery(int iCodPedido)
        {
            InitializeComponent();
            lblNumeroPedido.Text = intCodPedido.ToString();
            intCodPedido = iCodPedido;
        }

        private void frmFinalizaDelivery_Load(object sender, EventArgs e)
        {
            try
            {
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
                        if (gridFormasPagamento.Columns["Descricao"].HeaderText == "Descricao")
                        {
                            gridFormasPagamento.Columns["Descricao"].ReadOnly = true;
                        }
                    }

                    if (!gridFormasPagamento.Columns.Contains("Valor"))
                    {
                        gridFormasPagamento.Columns.Add("Valor", "Valor R$");
                        gridFormasPagamento.Refresh();
                    }
                    gridFormasPagamento.Refresh();

                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }


        }

        private void FinalizarPedido(object sender, EventArgs e)
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
                    MessageBox.Show("Valor do pedido é maior que o valor informado");
                    return;
                }
                else if (iTotalSomado > decimal.Parse(lblTotalPedido.Text))
                {
                    MessageBox.Show("Os Valores somados são maiores que o valor do Pedido");
                    return;
                }
                else
                {
                    for (int intFor = 0; intFor < gridFormasPagamento.Rows.Count; intFor++)
                    {
                        if (gridFormasPagamento.Rows[intFor].Cells["Valor"].Value != null
                            && gridFormasPagamento.Rows[intFor].Cells["Valor"].Value.ToString() != "")
                        {
                            FinalizaPedido finalizaPed = new FinalizaPedido()
                            {
                                CodPedido = intCodPedido,
                                CodPagamento = int.Parse(gridFormasPagamento.Rows[intFor].Cells["Codigo"].Value.ToString()),
                                ValorPagamento = decimal.Parse(gridFormasPagamento.Rows[intFor].Cells["Valor"].Value.ToString())
                            };
                           // con.Insert("spAdicionarFinalizaPedido_Pedido", finalizaPed);
                        }
                    }
                    //ExecutaImpressao(intCodPedido);
                    this.Close();
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        private void ExecutaImpressao(int intCodigoPedido)
        {
            try
            {

                DataSet ds = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", intCodigoPedido);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                DateTime DataPed = ds.Tables[0].Rows[0].Field<DateTime>("RealizadoEM");
                int intCodEndereco = ds.Tables[0].Rows[0].Field<int>("CodEndereco");
                string strTipoPedido = ds.Tables[0].Rows[0].Field<string>("Tipo");
                int QtViasEntrega = int.Parse(Sessions.returnConfig.ViasEntrega);
                string dblPRevisao = DataPed.AddMinutes(Convert.ToDouble(Sessions.returnConfig.PrevisaoEntrega)).ToShortTimeString();

                if (Sessions.returnConfig.ImpViaCozinha)
                {
                    Utils.ImpressaoCozihanova(intCodigoPedido);
                }

                if (Sessions.returnConfig.ImprimeViaEntrega && strTipoPedido== "0 - Entrega")
                {
                    Utils.ImpressaoEntreganova(intCodigoPedido, decimal.Parse(txtTrocoPara.Text.Replace("R$,", "")), dblPRevisao
                        , false, QtViasEntrega,Sessions.returnConfig.ImpressoraEntrega,false, intCodEndereco);
                }
                else if (strTipoPedido== "2 - Balcao")
                {
                    Utils.ImpressaoBalcao(intCodigoPedido, false, int.Parse(Sessions.returnConfig.ViasBalcao), Sessions.returnConfig.ImpressoraCopaBalcao);
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        void TeclaPressionada(object sender, KeyPressEventArgs e)

        {
            if (!char.IsNumber(e.KeyChar) && !char.IsPunctuation(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }
        private void ValidaTexto(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewTextBoxEditingControl && gridFormasPagamento.Columns.Contains("Valor"))
                {
                    for (int i = 0; i < gridFormasPagamento.Rows.Count; i++)
                    {
                        if (gridFormasPagamento.Rows[i].Cells["Valor"].Value != null)
                        {
                            e.Control.KeyPress += new KeyPressEventHandler(TeclaPressionada);
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
    }
}
