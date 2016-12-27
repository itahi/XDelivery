using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DexComanda.Relatorios.Fechamentos.Novos;
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
    public partial class frmEntregasPorRegiao : Form
    {
        public frmEntregasPorRegiao()
        {
            InitializeComponent();
        }

        private void GerarReport(object sender, EventArgs e)
        {
            try
            {
                RelEntregasPorRegiao report;
                RelEntregasMotoboyAgrupado report2;
                try
                {
                    if (rbRegiao.Checked)
                    {
                        report = new RelEntregasPorRegiao();
                        crystalReportViewer1.ReportSource = Utils.GerarReportSoDatas(report, dtInicio.Value, dtFim.Value);
                    }
                    else
                    {
                        report2 = new RelEntregasMotoboyAgrupado();
                        crystalReportViewer1.ReportSource = Utils.GerarReportSoDatas(report2, dtInicio.Value, dtFim.Value);
                    }
                    crystalReportViewer1.Refresh();


                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
    }
}
