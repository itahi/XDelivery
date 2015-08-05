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
    public partial class frmCaixaMovimento : Form
    {
        private Conexao con;

        public frmCaixaMovimento()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void ExecutaFiltro(object sender, EventArgs e)
        {
           // con.SelectCaixaMovimetoFiltro(
        }
    }
}
