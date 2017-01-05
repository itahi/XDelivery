using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DexComanda.Relatorios.Delivery.Forms
{
    public partial class frmReporVendaOnline : DexComanda.Relatorios.Clientes.Crystal.frmReportPadrao
    {
        public frmReporVendaOnline()
        {
            InitializeComponent();
        }

        private void Gerar(object sender, EventArgs e)
        {
            try
            {
                RelVendasOnline rel = new RelVendasOnline();
                crystalReportViewer1.ReportSource = Utils.GerarReportSoDatas(rel, dtInicio.Value, dtFim.Value);
                crystalReportViewer1.Refresh(); 
            }
            catch (Exception erro)
            {

                throw;
            }
        }
    }
}
