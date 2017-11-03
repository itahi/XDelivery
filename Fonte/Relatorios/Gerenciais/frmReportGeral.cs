using DexComanda.Relatorios.Gerenciais.Cristal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DexComanda.Relatorios.Gerenciais
{
    public partial class frmReportGeral : DexComanda.Relatorios.Clientes.Crystal.frmReportPadrao
    {
        public frmReportGeral()
        {
            InitializeComponent();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                RelVendasGeral rel;
                rel = new RelVendasGeral();
                crystalReportViewer1.ReportSource = Utils.GerarReportSoDatas(rel, dtInicio.Value, dtFim.Value);
                crystalReportViewer1.Refresh();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
    }
}
