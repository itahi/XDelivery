using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações
{
    public partial class frmLancaEstoque : Form
    {
        private Conexao con;
        public frmLancaEstoque()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void ListaProdutosGrupo(object sender, EventArgs e)
        {
            this.cbxProdutosGrid.DataSource = con.SelectRegistroPorNome("@GrupoProduto", "Produto", "spObterProdutoPorGrupo", this.cbxTipoProduto.Text).Tables["Produto"];
            this.cbxProdutosGrid.DisplayMember = "NomeProduto";
            this.cbxProdutosGrid.ValueMember = "Codigo";
            this.txtQuantidade.Text = "1";
        }

        private void frmLancaEstoque_Load(object sender, EventArgs e)
        {
            this.cbxTipoProduto.DataSource = con.SelectAll("Grupo", "spObterGrupoAtivo").Tables["Grupo"];
            this.cbxTipoProduto.DisplayMember = "NomeGrupo";
            this.cbxTipoProduto.ValueMember = "Codigo";
        }

        private void LancaMovimento(object sender, EventArgs e)
        {
            string lTipoMovimento="";
            if (rbEntrada.Checked)
	        {
		         lTipoMovimento = "E";
	        }
            else
	        {
             lTipoMovimento = "S";
	        }
            DataTable dt = new DataTable();
            try
            {
                for (int i = 0; i <gridMovimento.Rows.Count; i++)
                {
                    gridMovimento.Rows[i].Cells["CodProduto"].Value = cbxProdutosGrid.SelectedValue.ToString();
                    gridMovimento.Rows[i].Cells["NomeProduto"].Value = cbxProdutosGrid.Text;
                    gridMovimento.Rows[i].Cells["Quantidade"].Value = txtQuantidade.Text;
                    gridMovimento.Rows[i].Cells["TipoMovimento"].Value = lTipoMovimento;
                    i++;
                }
                
               
            }
            catch (Exception)
            {
                
                throw;
            }
            

            
        }
    }
}
