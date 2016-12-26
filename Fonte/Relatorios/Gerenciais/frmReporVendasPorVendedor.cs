using DexComanda.Relatorios.Gerenciais.Cristal;
using System;
using System.Windows.Forms;

namespace DexComanda.Relatorios.Gerenciais
{
    public partial class frmReporVendasPorVendedor : DexComanda.Relatorios.Clientes.Crystal.frmReportPadrao
    {
        public frmReporVendasPorVendedor()
        {
            InitializeComponent();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                RelVendasPorVendedores relVe;
                relVe = new RelVendasPorVendedores();
                crystalReportViewer1.ReportSource = Utils.GerarReportSoDatas(relVe, dtInicio.Value, dtFim.Value);
                crystalReportViewer1.Refresh();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            
        }
    }
}
