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
    public partial class frmTransfereMesa : Form
    {
        private Conexao con;
        private int intCodPedido;
        private decimal decTotalPedido;
        private int intCodPessoa;
        public frmTransfereMesa(int iCodPedido, decimal iTotalPedido, int iCodPessoa)
        {
            con = new Conexao();
            InitializeComponent();
          //  intCodPedido = iCodPedido;
            decTotalPedido = iTotalPedido;
            intCodPessoa = iCodPessoa;
        }

        private void CarregaMesas(ComboBox icbx)
        {
            DataSet dsDados = con.SelectAll("Mesas", "spObterMesas");
            icbx.DataSource = dsDados.Tables["Mesas"];
            icbx.DisplayMember = "NumeroMesa";
            icbx.ValueMember = "Codigo";

        }

        private void PopulaGrid(int intCodMesa, DataGridView dtGrid)
        {
            try
            {
                dtGrid.DataSource = null;
                dtGrid.AutoGenerateColumns = true;
                DataSet ds  = con.SelectRegistroPorCodigo("ItemsPedido", "spObterPedidoPorNumeroMesa", intCodMesa);
                if (ds.Tables[0].Rows.Count==0)
                {
                    MessageBox.Show(Bibliotecas.cFiltroRetornaVazio);
                    return;
                }
                intCodPedido = ds.Tables[0].Rows[0].Field<int>("CodPedido");
                dtGrid.DataSource = ds;
                dtGrid.DataMember = "ItemsPedido";
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        private void frmTransfereMesa_Load(object sender, EventArgs e)
        {
            con = new Conexao();
            CarregaMesas(cbxListaMesasO);
            CarregaMesas(cbxListaMesasD);
        }
        private void cbxListaMesasO_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PopulaGrid(int.Parse(cbxListaMesasO.SelectedValue.ToString()), gridOrigem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utils.MessageBoxQuestion("Deseja transferir TODOS itens da mesa "+cbxListaMesasO.Text + "para mesa: "+cbxListaMesasD.Text))
                {
                    return;
                }
                if (cbxListaMesasO.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("Selecione a mesa origem para ser transferida");
                    cbxListaMesasO.Focus();
                    return;
                }
                if (cbxListaMesasD.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("Selecione a mesa origem para ser transferida");
                    cbxListaMesasD.Focus();
                    return;
                }
                if (cbxListaMesasD.SelectedValue.ToString() == cbxListaMesasO.SelectedValue.ToString())
                {
                    MessageBox.Show("Mesa de origem não pode ser igual mesa destino");
                    cbxListaMesasD.Focus();
                    return;
                }

                if (gridOrigem.Rows.Count==0)
                {
                    MessageBox.Show("A lista deve conter pelo menos 1 item para ser transferido");
                    return;
                }
                //if (gridDestino.Rows.Count>0)
                //{
                //    MessageBox.Show("A mesa " + cbxListaMesasD.Text + "já possuí itens");
                //    return;
                //}
                int iRetunr=  con.TransfereMesa(intCodPedido, cbxListaMesasD.Text.ToString(), Sessions.retunrUsuario.Codigo, decTotalPedido,
                int.Parse(cbxListaMesasO.SelectedValue.ToString()), intCodPessoa, int.Parse(cbxListaMesasD.SelectedValue.ToString()));
                if (iRetunr!=0)
                {
                    gridOrigem.DataSource = null;
                    gridOrigem.Refresh();
                    PopulaGrid(int.Parse(cbxListaMesasD.SelectedValue.ToString()), gridDestino);
                }
                
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }

        private void cbxListaMesasD_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PopulaGrid(int.Parse(cbxListaMesasD.SelectedValue.ToString()), gridDestino);
        }

        private void MenuAuxiliar(object sender, MouseEventArgs e)
        {
            try
            {
                //Se grid não tiver nada selecionado ele vaza
                if (gridOrigem.CurrentRow.Cells[0].Value == null)
                {
                    return;
                }
                DataGridView dgv = sender as DataGridView;
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenu menuPrincipal = new ContextMenu();
                    MenuItem TransfereItens = new MenuItem(" Transferir este item?");
                    TransfereItens.Click += TransferirItemMesa;
                    menuPrincipal.MenuItems.Add(TransfereItens);

                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            
        }
        private void TransferirItemMesa(object sender, EventArgs e)
        {
            try
            {
                int intCodItem = int.Parse(gridOrigem.CurrentRow.Cells["Codigo"].Value.ToString());
                int intCodPedido = int.Parse(gridOrigem.CurrentRow.Cells["CodPedido"].Value.ToString());

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        }
}
