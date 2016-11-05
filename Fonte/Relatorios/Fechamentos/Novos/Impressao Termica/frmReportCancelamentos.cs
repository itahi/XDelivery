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

namespace DexComanda.Relatorios.Fechamentos.Novos.Impressao_Termica
{
    public partial class frmReportCancelamentos : Form
    {
        private static TableLogOnInfos crtableLogoninfos;
        private static TableLogOnInfo crtableLogoninfo;
        private static ConnectionInfo crConnectionInfo;
        private static Tables CrTables;

        public frmReportCancelamentos()
        {
            InitializeComponent();
        }

        private void Filtrar(object sender, EventArgs e)
        {
            string iRetorno = "";
            RelCancelamentos report;
            report = new RelCancelamentos();
            crtableLogoninfos = new TableLogOnInfos();
            crtableLogoninfo = new TableLogOnInfo();
            crConnectionInfo = new ConnectionInfo();
            try
            {
                var dataInicio = Convert.ToDateTime(dtInicio.Value.ToShortDateString() + " 00:00:00");
                var dataFim = Convert.ToDateTime(dtFim.Value.ToShortDateString() + " 23:59:59");
                report.Load(Directory.GetCurrentDirectory() + @"\RelCancelamentos.rpt");
                crConnectionInfo.ServerName = Sessions.returnEmpresa.Servidor;
                crConnectionInfo.DatabaseName = Sessions.returnEmpresa.Banco;
                crConnectionInfo.UserID = "sa";
                crConnectionInfo.Password = "1001";

                CrTables = report.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }
                report.SetParameterValue("@DataI", dataInicio);
                report.SetParameterValue("@DataF",dataFim);
                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
            finally
            {
                // report.Dispose();

            }

        }
    }
}
