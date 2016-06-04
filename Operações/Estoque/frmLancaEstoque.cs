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

namespace DexComanda.Operações
{
    public partial class frmLancaEstoque : Form
    {
        private Conexao con;
        private int i = 0;
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
       
                gridMovimento.Rows.Add(cbxProdutosGrid.SelectedValue.ToString(), cbxProdutosGrid.Text, txtQuantidade.Text, lTipoMovimento);
                
               
            }
            catch (Exception erro)
            {
                
                throw;
            }
            

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new Conexao();
            try
            {

                for (int i = 0; i < gridMovimento.Rows.Count-1; i++)
                {
                    Produto_Estoque prodEs = new Produto_Estoque()
                    {
                        CodProduto = int.Parse(gridMovimento.Rows[i].Cells["CodProduto"].Value.ToString()),
                        DataAtualizacao = DateTime.Now,
                        //Quantidade = decimal .Parse(gridMovimento.Rows[i].Cells["Quantidade"])
                    };
                    if (gridMovimento.Rows[i].Cells["TipoMovimento"].Value.ToString() =="E")
                    {
                        prodEs.Quantidade = decimal.Parse(gridMovimento.Rows[i].Cells["Quantidade"].Value.ToString());
                    }
                    else
                    {
                        prodEs.Quantidade = -decimal.Parse(gridMovimento.Rows[i].Cells["Quantidade"].Value.ToString());
                    }
                    con.Insert("spAtualizaEstoque", prodEs);
                }

                MessageBox.Show("Movimento confirmado ");
                gridMovimento.Rows.Clear();
            }
            catch (Exception erro)
            {

                throw;
            }
          

            
        }
    }
}
