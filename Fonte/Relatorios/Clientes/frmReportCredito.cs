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

namespace DexComanda.Relatorios.Clientes
{
    public partial class frmReportCredito : Form
    {
        private RelCreditoDebito report;
        public frmReportCredito(int iCodPessoa , DateTime iDtInici, DateTime idtFim)
        {
            InitializeComponent();
            report = new RelCreditoDebito();
           
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            report.Load(Directory.GetCurrentDirectory() + @"\RelCreditoDebito.rpt");
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

            report.Parameter_CodPessoa.Attributes.Add("@CodPessoa", iCodPessoa);
            report.Parameter_DataInicio.Attributes.Add("@DataInicio", iDtInici);
            report.Parameter_DataFim.Attributes.Add("@DataFim", idtFim);
            report.PrintToPrinter(0, true, 0, 0);
        }

        private void frmReportCredito_Load(object sender, EventArgs e)
        {

        }
    }
}
