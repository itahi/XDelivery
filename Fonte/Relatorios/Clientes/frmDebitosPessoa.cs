using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DexComanda.Relatorios.Clientes.Crystal;
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

namespace DexComanda.Relatorios.Clientes
{
    public partial class frmDebitosPessoa : Form
    {
        public frmDebitosPessoa()
        {
            InitializeComponent();
        }
        private void Detalhado()
        {
            RelDebitosGeral report;
            report = new RelDebitosGeral();

            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            report.Load(Directory.GetCurrentDirectory() + @"\RelDebitosGeral.rpt");
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

            report.SetParameterValue("@DataInicio", dtinicio.Value);
            report.SetParameterValue("@DataFim", dtFim.Value);
            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.Refresh();

        }

        private void Resumido()
        {
            RelDebitosResumido report;
            report = new RelDebitosResumido();

            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            report.Load(Directory.GetCurrentDirectory() + @"\RelDebitosResumido.rpt");
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

            report.SetParameterValue("@DataI", dtinicio.Value);
            report.SetParameterValue("@DataF", dtFim.Value);
            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.Refresh();

        }
        private void Filtrar(object sender, EventArgs e)
        {
            try
            {

               
                try
                {
                    if (rbDetalhado.Checked)
                    {
                        Detalhado();
                    }
                    else
                    {
                        Resumido();
                    }

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
