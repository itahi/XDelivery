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
        public frmTransfereMesa()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void CarregaMesas(ComboBox icbx)
        {
            DataSet dsDados = con.SelectAll("Mesas", "spObterMesasAbertas");
            icbx.DataSource = dsDados.Tables["Mesas"];
            icbx.DisplayMember = "NumeroMesa";
            icbx.ValueMember = "Codigo";

        }

        private void PopulaGrid(string intCodMesa,DataGridView dtGrid)
        {
            try
            {
                dtGrid.DataSource = null;
                dtGrid.AutoGenerateColumns = true;
                dtGrid.DataSource = con.SelectRegistroPorCodigo("ItemsPedido", "spObterPedidoPorNumeroMesa", int.Parse(intCodMesa));
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

        private void cbxListaMesasO_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PopulaGrid(cbxListaMesasO.SelectedValue.ToString(), gridOrigem);
        }

        private void cbxListaMesasO_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PopulaGrid(cbxListaMesasO.SelectedValue.ToString(), gridOrigem);
        }
    }
}
