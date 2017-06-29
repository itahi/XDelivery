using CrystalDecisions.CrystalReports.Engine;
using DexComanda.Relatorios.Estoque;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DexComanda.Operações.Estoque
{
    public partial class frmConsultaInsumo : DexComanda.Relatorios.Clientes.Crystal.frmReportPadrao
    {
        public frmConsultaInsumo()
        {
            InitializeComponent();
        }

        private void Report(object sender, EventArgs e)
        {
            try
            {
                if (rbResumido.Checked)
                {
                    RelEstoqueResumido rel = new RelEstoqueResumido();
                    crystalReportViewer1.ReportSource = Utils.GerarReportSoDatas(rel, dtInicio.Value, dtFim.Value);
                }
                else
                {
                    RelEstoqueDetalhado rel = new RelEstoqueDetalhado();
                    crystalReportViewer1.ReportSource = Utils.GerarReportSoDatas(rel, dtInicio.Value, dtFim.Value);
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
