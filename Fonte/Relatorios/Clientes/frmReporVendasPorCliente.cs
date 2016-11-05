using DexComanda.Relatorios.Clientes.Crystal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DexComanda.Relatorios.Clientes
{
    public partial class frmReporVendasPorCliente : DexComanda.Relatorios.Clientes.Crystal.frmReportPadrao
    {
        public frmReporVendasPorCliente()
        {
            InitializeComponent();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            RelVendasPorCliente rel;
            rel = new RelVendasPorCliente();
            crystalReportViewer1.ReportSource = Utils.GerarReportSoDatas(rel, dtInicio.Value, dtFim.Value);
            crystalReportViewer1.Refresh();
        }
    }
}
