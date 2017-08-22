using DexComanda.Models;
using DexComanda.Models.WS;
using DexComanda.Push;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações.Pedido
{
    public partial class frmInformaEntregador_Coletivo : Form
    {
        private Conexao con;
        private DataGridView dtGridPedido;
        public frmInformaEntregador_Coletivo()
        {
            InitializeComponent();
        }
        public frmInformaEntregador_Coletivo(DataGridView gridView)
        {
            InitializeComponent();
            dtGridPedido = gridView;
        }

        private void cbxEntregador_DropDown(object sender, EventArgs e)
        {
            try
            {
                Utils.MontaCombox(cbxEntregador, "Nome", "Codigo", "Entregador", "spObterEntregadores");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

            
        }

        private void AdicionaPedido(object sender, EventArgs e)
        {
            try
            {
                if (txtCodPedido.Text=="")
                {
                    MessageBox.Show("Informe o código do pedido");
                    txtCodPedido.Focus();
                    return;
                }
                if (cbxEntregador.SelectedValue==null)
                {
                    MessageBox.Show("Informe o Entregador do pedido");
                    cbxEntregador.Focus();
                    return;
                }

                if (!Utils.PermiteEntregador(int.Parse(txtCodPedido.Text)))
                {
                    MessageBox.Show("Este pedido não permite entregadores");
                    return;
                }
                con = new Conexao(); 
                if (con.ConsultaPedido(txtCodPedido.Text).Tables[0].Rows.Count==0)
                {
                    MessageBox.Show("Este pedido Foi Finalizado ou não existe , por isso não pode ser alterado");
                    return;
                }

                int iCountLinhas = PedidosGridView.Rows.Count;
                if (PedidosGridView.DataSource != null)
                {
                    PedidosGridView.AutoGenerateColumns = false;
                    PedidosGridView.DataSource = null;
                    PedidosGridView.DataMember = null;
                }
                
                for (int i = 0; i < PedidosGridView.Rows.Count; i++)
                {
                    if (PedidosGridView.Rows[i].Cells["Codigo"].Value != null)
                    {
                        if (txtCodPedido.Text == PedidosGridView.Rows[i].Cells["Codigo"].Value.ToString())
                        {
                            MessageBox.Show("Esse pedido já foi lançado");
                            return;
                        }
                    }
                }
                PedidosGridView.Rows.Add();
                PedidosGridView.Rows[iCountLinhas].Cells[0].Value = int.Parse(txtCodPedido.Text);
                PedidosGridView.Rows[iCountLinhas].Cells[1].Value = int.Parse(cbxEntregador.SelectedValue.ToString());
                PedidosGridView.Rows[iCountLinhas].Cells[2].Value = cbxEntregador.Text.ToString();
                iCountLinhas = PedidosGridView.Rows.Count;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        private void MudaStatusPraEntrega(int intCodPedido)
        {
            try
            {
                int CodPedidoWS = Utils.VerificaPedidoOnline(intCodPedido);


                if (CodPedidoWS > 0)
                {
                    OneSignal one = new OneSignal();
                    // se for pedido online
                    one.AlteraStatusPedido(CodPedidoWS, StatusPedido.cPedidoNaEntrega, Utils.RetornaPessoa(intCodPedido));
                }
                con.AtualizaSituacao(intCodPedido, Sessions.retunrUsuario.Codigo, StatusPedido.cPedidoNaEntrega, dtGridPedido);

                //}
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btnConfirma_Click(object sender, EventArgs e)
        {
            if (!Utils.MessageBoxQuestion("Deseja continuar com essa alteração em "+ PedidosGridView.Rows.Count.ToString() + " Pedidos"))
            {
                return;
            }
            for (int i = 0; i < PedidosGridView.Rows.Count; i++)
            {
                InserirMotoboyPedido MotoBoy = new InserirMotoboyPedido()
                {
                    CodMotoBoy = int.Parse(PedidosGridView.Rows[i].Cells["CodEntregador"].Value.ToString()),
                    CodPedido = int.Parse(PedidosGridView.Rows[i].Cells["Codigo"].Value.ToString())
                };
                MudaStatusPraEntrega(MotoBoy.CodPedido);
                con.Update("spInsereBoyPedido", MotoBoy);
                
            }
            PedidosGridView.AutoGenerateColumns = false;
            PedidosGridView.DataSource = null;
            PedidosGridView.DataMember = null;
        }
    }
}
