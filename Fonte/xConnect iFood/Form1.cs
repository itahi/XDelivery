using DexComanda;
using DexComanda.Integração.iFood;
using DexComanda.Integração.iFood.Pedido;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace xConnect_iFood
{
    public partial class frmPrincipal : Form
    {
        private Conexao con;
        private MetodosWS newClassiFood;
        private int intCountLinhas = 0;
        public frmPrincipal()
        {
            con = new Conexao();
            newClassiFood = new MetodosWS();
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception erro)
            {
                throw;
            }
        }
        private void ExecutaBusca(object sender, EventArgs e)
        {
            try
            {
                lock (this)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            ExecutaBusca(sender, e);
                        });
                        return;
                    }


                   
                    newClassiFood = new MetodosWS();
                    newClassiFood.BuscaToken();
                    newClassiFood.VerificaPedidos(newClassiFood.strTokenIFood);
                    newClassiFood.ManipulaEventos(newClassiFood.strTokenIFood, newClassiFood.newListEventos);
                  //  ManipulaLabel(newClassiFood.bLojaOnline);

                    if (newClassiFood.newListEventos == null)
                    {
                        return;
                    }
                    foreach (var idPedidos in newClassiFood.newListEventos)
                    {
                        newClassiFood.CapturandoPedidos(idPedidos.correlationId);
                    }


                    if (newClassiFood.listRootPedido == null)
                    {
                        return;
                    }

                    MontaGridPedido(newClassiFood.listRootPedido);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void MontaGridPedido(List<root> listpedido)
        {
            gridPedido.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridPedido.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            foreach (var pedidos in listpedido)
            {
                gridPedido.Rows.Add();
                gridPedido.Rows[intCountLinhas].Cells["id"].Value = pedidos.reference;
                gridPedido.Rows[intCountLinhas].Cells["Cliente"].Value = pedidos.customer.name;
                gridPedido.Rows[intCountLinhas].Cells["Data"].Value = pedidos.createdAt.ToShortDateString();
                gridPedido.Rows[intCountLinhas].Cells["Valor"].Value = double.Parse(pedidos.totalPrice.ToString());
                intCountLinhas = gridPedido.Rows.Count;
            }
        }

    }
}
