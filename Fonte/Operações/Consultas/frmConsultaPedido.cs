using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações.Consultas
{
    public partial class frmConsultaPedido : Form
    {
        private Conexao con;
        public frmConsultaPedido()
        {
            InitializeComponent();
        }

        private void dtInicio_ValueChanged(object sender, EventArgs e)
        {

        }

        private void BuscaPedidos(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.DataSource = null;
                dataGridView1.AutoGenerateColumns = true;
                DataSet ds = con.ConsultaPedido(txtCodPedido.Text, dtInicio.Value, dtFim.Value);
                if (ds.Tables[0].Rows.Count==0)
                {
                    MessageBox.Show(Bibliotecas.cFiltroRetornaVazio);
                    return;
                }
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Pedido";
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void txtCodPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void frmConsultaPedido_Load(object sender, EventArgs e)
        {
            con = new Conexao();
        }

        private void frmConsultaPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void frmConsultaPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                BuscaPedidos(sender, e);
            }
        }

        private void AbrePedido(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString()!=null)
            {
                Utils.BuscaPedido(int.Parse(dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString()));
            }
        }
    }
}
