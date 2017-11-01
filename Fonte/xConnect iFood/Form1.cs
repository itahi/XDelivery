using DexComanda;
using DexComanda.Integração.iFood;
using DexComanda.Integração.iFood.Pedido;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

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
                con = new Conexao();
              //  con.SelectAll("Pedido_iFood", "", "select idPedido,Data,Cliente,Total from Pedido_iFood where [status]='PLACED'");
                Utils.PopularGrid("Pedido_iFood", gridPedido, con.SelectAll("Pedido_iFood", "", "select Cliente,Data,Total,idPedido from Pedido_iFood where [status]='PLACED' order by Codigo desc"), "idPedido");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
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
                   // timer1.Enabled = false;
                    newClassiFood = new MetodosWS();
                    newClassiFood.BuscaToken();
                    newClassiFood.VerificaPedidos(newClassiFood.strTokenIFood);

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
                    newClassiFood.ManipulaEventos(newClassiFood.strTokenIFood, newClassiFood.newListEventos);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void MontaGridPedido(List<root> listpedido)
        {
            try
            {
                gridPedido.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                gridPedido.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                con = new Conexao();
                string sql = "select * from Pedido_iFood where [status]='PLACED'";
                DataSet dsPedidos = con.SelectAll("Pedido_iFood", "", sql);
                foreach (var pedidos in listpedido)
                {

                    string iSql = "select * from Pedido_iFood where idPedido=" + pedidos.reference;
                    if (con.SelectAll("Pedido_iFood", "", iSql).Tables[0].Rows.Count == 0)
                    {
                        newClassiFood.InserePedidoiFood(listpedido);
                        Utils.PopularGrid("Pedido_iFood", gridPedido, con.SelectAll("Pedido_iFood", "", "select Cliente,Data,Total,idPedido from Pedido_iFood where [status]='PLACED' order by Codigo desc"), "idPedido");
                        //intCountLinhas = gridPedido.Rows.Count + 1;
                        //gridPedido.Rows[intCountLinhas].Cells["idPedido"].Value = pedidos.reference;
                        //gridPedido.Rows[intCountLinhas].Cells["Cliente"].Value = pedidos.customer.name;
                        //gridPedido.Rows[intCountLinhas].Cells["Data"].Value = pedidos.createdAt.ToShortDateString();
                        //gridPedido.Rows[intCountLinhas].Cells["Total"].Value = double.Parse(pedidos.totalPrice.ToString());
                        CriaPoup("Chegou um novo Pedido", "Fique atento a sua impressora");
                    }
                  
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            
        }
        private DataSet MontaGridPedidoConfirmado(string strReference)
        {
            try
            {

                string iSql = "select p.Codigo,TotalPedido , " +
                             " (select Nome from Pessoa PS where PS.Codigo = P.CodPessoa) as Cliente " +
                             "  from Pedido P " +
                             "  where idiFood =" + strReference;
                con = new Conexao();
                DataSet ds = con.SelectAll("Pedido", "", iSql);
                return ds;
            }
            catch (Exception erro)
            {

                throw;
            }
        }
        private void CriaPoup(string strTitulo, string strConteudo)
        {
            var popupNotifier = new PopupNotifier();
            popupNotifier.TitleText = strTitulo;
            popupNotifier.ContentText = strConteudo;
            popupNotifier.IsRightToLeft = false;
            popupNotifier.Popup();
            if (strTitulo== "Chegou um novo Pedido")
            {
                ControlaSom(false);
                popupNotifier.Click += Click;
            }
            
        }
        private void ControlaSom(Boolean iparar)
        {

            string iSom = @"" + Directory.GetCurrentDirectory() + @"\som.wav";
            try
            {
                if (File.Exists(iSom))
                {
                    SoundPlayer simpleSound = new SoundPlayer(iSom);
                    simpleSound.PlayLooping();
                    if (iparar)
                    {
                        simpleSound.Stop();
                    }
                    
                }

            }
            catch (Exception erro)
            {

                throw;
            }
        }
        private new void Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

        }

        private void frmPrincipal_Resize(object sender, EventArgs e)
        {
            CriaPoup("xConnect - xSistemas", "Você será notificado a chegar novos Pedidos");
        }

        private void btnConfimar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridPedido.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selecione o Pedido que deseja confirmar");
                    return;
                }
                newClassiFood = new MetodosWS();
                newClassiFood.BuscaToken();
                newClassiFood.ConfirmaPedido(gridPedido.CurrentRow.Cells["idPedido"].Value.ToString());
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

        }
        private void btnNaEntrega_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridPedido.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selecione o Pedido que deseja confirmar");
                    return;
                }
                newClassiFood = new MetodosWS();
                newClassiFood.BuscaToken();
                newClassiFood.SaiuPraEntrega(gridPedido.CurrentRow.Cells["idPedido"].Value.ToString());
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

        }

        private void btnEntregue_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridPedido.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selecione o Pedido que deseja confirmar");
                    return;
                }
                newClassiFood = new MetodosWS();
                newClassiFood.BuscaToken();
                newClassiFood.Entregue(gridPedido.CurrentRow.Cells["idPedido"].Value.ToString());
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

        }

    }
}
