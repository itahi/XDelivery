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
    public partial class frmLancaEstoqueProd : Form
    {
        private Conexao con;
        private int i = 0;
        public frmLancaEstoqueProd()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void ListaProdutosGrupo(object sender, EventArgs e)
        {
           
        }

        private void frmLancaEstoque_Load(object sender, EventArgs e)
        {
            this.cbxGrupo.DataSource = con.SelectAll("Grupo", "spObterGrupoAtivo").Tables["Grupo"];
            this.cbxGrupo.DisplayMember = "NomeGrupo";
            this.cbxGrupo.ValueMember = "Codigo";
        }

        private void LancaMovimento(object sender, EventArgs e)
        {
            string lTipoMovimento = "";
            DataTable dt = new DataTable();
            try
            {
              gridMovimento.Rows.Add(cbxProdutosGrid.SelectedValue.ToString(), cbxProdutosGrid.Text,txtprecocompra.Text ,txtQuantidade.Text);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new Conexao();
            try
            {

                for (int i = 0; i < gridMovimento.Rows.Count ; i++)
                {
                    if (gridMovimento.Rows[i].Cells[0].Value==null)
                    {
                        break;
                    }
                    Produto_Estoque prodEs = new Produto_Estoque()
                    {
                        CodProduto = int.Parse(gridMovimento.Rows[i].Cells["CodProduto"].Value.ToString()),
                        NomeProduto = gridMovimento.Rows[i].Cells["NomeProduto"].Value.ToString(),
                        PrecoCompra = decimal.Parse(gridMovimento.Rows[i].Cells["Preco"].Value.ToString()),
                        DataAtualizacao = DateTime.Now,
                        Quantidade = decimal.Parse(gridMovimento.Rows[i].Cells["Quantidade"].Value.ToString())
                    };
                    con.Insert("spAtualizaEstoque", prodEs);
                }

                MessageBox.Show("Estoque lançado com sucesso ");
                gridMovimento.Rows.Clear();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        private void txtprecocompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }

        private void cbxTipoProduto_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxProdutosGrid.DataSource = con.SelectRegistroPorCodigo("Produto", "spObterProdutoPorGrupoComOpcao", int.Parse(cbxGrupo.SelectedValue.ToString())).Tables["Produto"];
                cbxProdutosGrid.DisplayMember = "Nome";
                cbxProdutosGrid.ValueMember = "Codigo";
                txtQuantidade.Text = "1";
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
           
        }
    }
}
