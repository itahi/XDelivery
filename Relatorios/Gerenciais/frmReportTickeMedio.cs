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

                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                report.Load(Directory.GetCurrentDirectory() + @"\RelTickeMedio.rpt");
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
                var datInicio = Convert.ToDateTime(dtInicio.Value.ToShortDateString() + " 00:00:00");
                var datFim = Convert.ToDateTime(dtFim.Value.ToShortDateString() + " 23:59:59");

                report.SetParameterValue("@DataInicio", datInicio);
                report.SetParameterValue("@DataFim", datFim);
                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    }

