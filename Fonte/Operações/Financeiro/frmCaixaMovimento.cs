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
        private DataSet dsMovimentoFiltro;
        public frmCaixaMovimento()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void ExecutaFiltro(object sender, EventArgs e)
        {
            string iTipo="E";
            if (rbEntrada.Checked)
	        {
		     iTipo = "E";
	        }
            else if (rbSaida.Checked)
	        {
             iTipo = "S";
	        }
            else if (rbEntradaSaida.Checked)
	        {
		      iTipo = "ES";
	        }
            else
	        {
                MessageBox.Show("Selecione o tipo de movimento para filtrar","[XSistemas");
                return;
	        }

            dsMovimentoFiltro = con.SelectCaixaMovimetoFiltro("spObterCaixaMovimetoFiltro", dtInicio.Value, dtFim.Value, iTipo, "", "", cbxNumCaixa.SelectedValue.ToString());

        }
    }
}
