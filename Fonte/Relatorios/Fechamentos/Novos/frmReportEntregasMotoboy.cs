﻿using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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

namespace DexComanda.Relatorios.Fechamentos.Novos
{
    public partial class frmReportEntregasMotoboy: Form
    {
        public frmReportEntregasMotoboy()
        {
            InitializeComponent();
        }

        private void Filtrar(object sender, EventArgs e)
        {
            RelEntregasMotoboy report;
            RelEntregasResumido repor2;
            try
            {
                if (rbDetalhado.Checked)
                {
                    report = new RelEntregasMotoboy();
                    crystalReportViewer1.ReportSource = Utils.GerarReportSoDatas(report, dtinicio.Value, dtFim.Value,horaInicio.Value.ToShortTimeString(), horaFim.Value.ToShortTimeString());
                }
                else
                {
                    repor2 = new RelEntregasResumido();
                    crystalReportViewer1.ReportSource = Utils.GerarReportSoDatas(repor2, dtinicio.Value, dtFim.Value, horaInicio.Value.ToShortTimeString(), horaFim.Value.ToShortTimeString());
                }
                crystalReportViewer1.Refresh();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
    }
}
