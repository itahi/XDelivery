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
    public partial class frmProdutosTrocaFidelidade : Form
    {
        private Conexao con;
        private DataSet dsProdutos;
        private int pontosAcumulados;
        public List<string> codProdutos = new List<string>();
        public frmProdutosTrocaFidelidade()
        {
            InitializeComponent();
        }

        public frmProdutosTrocaFidelidade(DataSet ds,int intPontosAcumulados)
        {
            con = new Conexao();
            dsProdutos = ds;
            pontosAcumulados = intPontosAcumulados;
            InitializeComponent();
        }
        private void frmProdutosTrocaFidelidade_Load(object sender, EventArgs e)
        {
            lblSaldo.Text = "Saldo Pontos:" + pontosAcumulados.ToString();
            Utils.PopularGrid("Produto", produtoGridView, dsProdutos,"Codigo");

        }

        private void btnConfirma_Click(object sender, EventArgs e)
        {
            try
            {
                int PontosProdutos = 0;
                codProdutos = new List<string>();
                for (int i = 0; i < produtoGridView.Rows.Count; i++)
                {
                    if ((produtoGridView.Rows[i].Cells["Selecionar"].Value !=null))
                    {
                        if (Boolean.Parse(produtoGridView.Rows[i].Cells["Selecionar"].Value.ToString()))
                        {
                            PontosProdutos += int.Parse(produtoGridView.Rows[i].Cells["Pontos"].Value.ToString());
                            codProdutos.Add("'"+produtoGridView.Rows[i].Cells["Codigo"].Value.ToString()+"'");
                        }
                    }
                    
                }
                if (PontosProdutos > pontosAcumulados)
                {
                    MessageBox.Show("Você não tem pontos suficientes para trocar , selecione menos produtos");
                    return;
                }

                this.Hide();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            

        }
    }
}
