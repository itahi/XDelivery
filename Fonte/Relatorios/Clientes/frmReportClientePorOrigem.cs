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
    public partial class frmReportClientePorOrigem : DexComanda.Relatorios.Clientes.Crystal.frmReportPadrao
    {
        public frmReportClientePorOrigem()
        {
            InitializeComponent();
        }

        private void dtInicio_ValueChanged(object sender, EventArgs e)
        {

        }

        private void GerarReport(object sender, EventArgs e)
        {
            try
            {
                RelClientesPorOrigem rel = new RelClientesPorOrigem();
                crystalReportViewer1.ReportSource = Utils.GerarReport(rel);
                crystalReportViewer1.Refresh();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
           

        }
    }
}
