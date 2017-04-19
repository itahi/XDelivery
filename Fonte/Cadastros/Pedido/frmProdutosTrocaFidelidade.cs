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
        private int intPontostroca;
        public frmProdutosTrocaFidelidade()
        {
            InitializeComponent();
        }

        public frmProdutosTrocaFidelidade(int iPontosFidelidade)
        {
            con = new Conexao();
            intPontostroca = iPontosFidelidade;
            InitializeComponent();
        }
        private void frmProdutosTrocaFidelidade_Load(object sender, EventArgs e)
        {
           // con.SelectRegistroPorCodigo("Produto", "spObterProdutosDisponivelsPratroca", intPontostroca);
            Utils.PopularGrid_SP("Produto", produtoGridView, "spObterProdutosDisponivelsPratroca", intPontostroca);
        }
    }
}
