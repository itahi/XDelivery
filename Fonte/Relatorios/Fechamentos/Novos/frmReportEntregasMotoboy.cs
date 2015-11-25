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
    public partial class frmReportEntregasMotoboy : Form
    {
        public frmReportEntregasMotoboy()
        {
            //dtInicio.Value = DateTime.Now.AddDays(-1);
            //dtFim.Value = DateTime.Now;
            InitializeComponent();
        }

        private void GerarReport(object sender, EventArgs e)
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

                report.SetParameterValue("@DataInicio", dtInicio.Value.ToShortDateString());
                report.SetParameterValue("@DataFim", dtFim.Value.ToShortDateString());
                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n" + ex.InnerException.ToString());
            }
        }
    }
}
