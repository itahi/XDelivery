using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DexComanda.Relatorios.Fechamentos.Novos.Impressao_Termica;
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

namespace DexComanda.Relatorios.Impressao_Termica
{
    public partial class frmReportVendasPorVendedor : Form
    {
        public frmReportVendasPorVendedor()
        {
            InitializeComponent();
        }

        private void GerarRelatório(object sender, EventArgs e)
        {
           
                RelVendasPorVendedor report;
                try
                {
                    report = new RelVendasPorVendedor();

                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    report.Load(Directory.GetCurrentDirectory() + @"\RelVendasPorVendedor.rpt");
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


                    report.SetParameterValue("@DataInicio", txtdtInicio.Value.ToShortDateString() + " 00:00:00");
                    report.SetParameterValue("@DataFim", txtdtFim.Value.ToShortDateString() + " 23:59:59");
                    if (report.Rows.Count == 0)
                    {
                        MessageBox.Show("Não há resultados com o filtro selecionado");
                        return;
                    }
                    crReportVendasPorVendedor.ReportSource = report;
                    crReportVendasPorVendedor.Refresh();
                }
                catch (Exception erro)
                {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
                }

            }
            }
    }

