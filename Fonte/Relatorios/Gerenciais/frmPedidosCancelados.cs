using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Relatorios.Gerenciais
{
    public partial class frmPedidosCancelados : Form
    {
        public frmPedidosCancelados()
        {
            InitializeComponent();
        }

        private void GerarImpressao(object sender, EventArgs e)
        {
            try
            {
                Utils.ImprimirCancelamentos(dtInicio.Value, dtFim.Value);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
    }
}
