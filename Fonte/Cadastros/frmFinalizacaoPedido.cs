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
        public frmFinalizacaoPedido()
        {
            InitializeComponent();
            con = new Conexao();
        }

        private void frmFinalizacaoPedido_Load(object sender, EventArgs e)
        {

        }
        public frmFinalizacaoPedido(decimal iTotalPedido)
        {
            try
            {
                InitializeComponent();
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
                        if (gridFormasPagamento.Columns[i].HeaderText == "Codigo")
                        {
                            gridFormasPagamento.Columns.RemoveAt(i);
                        }
                    }
                    gridFormasPagamento.Refresh();
                    
                }

            }
            catch (Exception erro)
            {

                throw;
            }

        }
    }
}
