using CrystalDecisions.CrystalReports.Engine;
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
            try
            {
                report = new RelEntregasMotoboy();

                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                report.Load(Directory.GetCurrentDirectory() + @"\RelEntregasMotoboy.rpt");
                crConnectionInfo.ServerName = Sessions.returnEmpresa.Servidor;
                crConnectionInfo.DatabaseName = Sessions.returnEmpresa.Banco;
                crConnectionInfo.UserID = "dex";
                crConnectionInfo.Password = "1234";

                CrTables = report.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }
                var dtInicio = Convert.ToDateTime(dtinicio.Value.ToShortDateString() + " 00:00:00");
                var datFim = Convert.ToDateTime(dtFim.Value.ToShortDateString() + " 23:59:59");

                report.SetParameterValue("@DataI", dtInicio);
                report.SetParameterValue("@DataF", datFim);
                if (report.Rows.Count == 0)
                {
                    MessageBox.Show("Não há resultados com o filtro selecionado");
                    return;
                }
                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
    }
}
