using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DexComanda.Relatorios.Gerenciais.Cristal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Relatorios.Gerenciais
{
    public partial class frmReportTickeMedio : Form
    {
        public frmReportTickeMedio()
        {
            InitializeComponent();
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            RelTickeMedio report;
            try
            {
                report = new RelTickeMedio();
                var datInicio = Convert.ToDateTime(dtInicio.Value.ToShortDateString() + " 00:00:00");
                var datFim = Convert.ToDateTime(dtFim.Value.ToShortDateString() + " 23:59:59");
                crystalReportViewer1.ReportSource = Utils.GerarReportSoDatas(report, datInicio, datFim); ;
                crystalReportViewer1.Refresh();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
    }
}

