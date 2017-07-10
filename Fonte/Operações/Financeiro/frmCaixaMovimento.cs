using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DexComanda.Relatorios.Caixa;
using DexComanda.Relatorios.Fechamentos.Novos;
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

namespace DexComanda.Operações
{
    public partial class frmCaixaMovimento : Form
    {
        private Conexao con;
        public DataSet dsMovimentoFiltro;
        public frmCaixaMovimento()
        {
            con = new Conexao();
            InitializeComponent();
        }



        private void GerarReport()
        {
            try
            {
                //      DateTime dtInicioFiltro = Convert.ToDateTime(dtInicio.Value.ToShortDateString() + " " + horaInicio.Value.ToShortTimeString());
                //    DateTime dtFimFiltro = Convert.ToDateTime(dtFim.Value.ToShortDateString() + " " + horaFim.Value.ToShortTimeString());
                bool bEstado = dtInicio.Value.ToShortDateString() != DateTime.Now.ToShortDateString();
                if (!rbEntradaSaida.Checked)
                {
                    RelCaixaHistorico report;
                    report = new RelCaixaHistorico();
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    string str = Directory.GetCurrentDirectory();
                    report.Load(Directory.GetCurrentDirectory() + @"\RelCaixaHistorico.rpt");
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

                   
                    report.SetParameterValue("@Turno", cbxTurno.Text);
                    report.SetParameterValue("@Estado", bEstado);
                    report.SetParameterValue("@DataI", dtInicio.Value.ToShortDateString());
                  //  report.SetParameterValue("@DataF", dtFim.Value.ToShortDateString());
                    report.SetParameterValue("@EntradaSaida", OperacaoMarcada());
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.Refresh();
                }
                else
                {
                    RelCaixaHistorico2 report;
                    report = new RelCaixaHistorico2();
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    string str = Directory.GetCurrentDirectory();
                    report.Load(Directory.GetCurrentDirectory() + @"\RelCaixaHistorico2.rpt");
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

                    report.SetParameterValue("@Turno", cbxTurno.Text);
                    report.SetParameterValue("@DataI", dtInicio.Value);
                    report.SetParameterValue("@Estado", bEstado); 
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.Refresh();
                }
               
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            
        }
        private void ExecutaFiltro(object sender, EventArgs e)
        {
            GerarReport();
        }

        //private void SomaValores()
        //{
        //    decimal vlrSaidas = 0.00M;
        //    decimal vlrEntrada = 0.00M;
        //    for (int i = 0; i < MovimentosGridView.Rows.Count; i++)
        //    {
        //        if (MovimentosGridView.Rows[i].Cells[6].Value.ToString() == "Entrada")
        //        {
        //            vlrEntrada = vlrEntrada + decimal.Parse(MovimentosGridView.Rows[i].Cells[5].Value.ToString());
        //        }
        //        else if (MovimentosGridView.Rows[i].Cells[6].Value.ToString() == "Saida")
        //        {
        //            vlrSaidas = vlrSaidas + decimal.Parse(MovimentosGridView.Rows[i].Cells[5].Value.ToString());
        //        }
        //    }

        //    lblEntradas.Text = vlrEntrada.ToString();
        //    lblSaidas.Text = vlrSaidas.ToString();
        //    if (rbEntradaSaida.Checked)
        //    {
        //        lblLiquido.Text = Convert.ToString(vlrEntrada - vlrSaidas);
        //    }
        //}

        private void frmCaixaMovimento_Load(object sender, EventArgs e)
        {
            cbxTurno.SelectedIndex = 0;
            DataSet dsCaixas = con.SelectAll("CaixaCadastro", "spObterCaixa");
            cbxNumCaixa.DataSource = dsCaixas.Tables["CaixaCadastro"];
            cbxNumCaixa.DisplayMember = "Numero";
            cbxNumCaixa.ValueMember = "Codigo";

            //cbxFPagamento.DataSource = con.SelectAll("FormaPagamento", "spObterFormaPagamento").Tables["FormaPagamento"];
            //cbxFPagamento.DisplayMember = "Descricao";
            //cbxFPagamento.ValueMember = "Codigo";

        }

        private void chkFPagamento_CheckedChanged(object sender, EventArgs e)
        {
           // cbxFPagamento.Enabled = !chkFPagamento.Checked;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Utils.RelCaixaHistorico(con.strSqlExecutado);

            // Utils.RelCaixaHistorico(dtInicio.Value,dtFim.Value,cbxNumCaixa.Text, OperacaoMarcada(), FormaPagamento(), cbxTurno.Text);
        }
        private string OperacaoMarcada()
        {
            string iReturn = "";
            foreach (System.Windows.Forms.Control TEXT in grpMovimento.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(TEXT.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    //Check to see if it's a textbox 
                    if (((System.Windows.Forms.RadioButton)TEXT).Checked)
                    {
                        iReturn = ((System.Windows.Forms.RadioButton)TEXT).Tag.ToString();
                    }
                }
            }
            if (rbEntradaSaida.Checked)
            {
                iReturn = "'E','S'";
            }
            return iReturn;
        }
        //private string FormaPagamento()
        //{
        //    string iReturn = "";
        //    if (!chkFPagamento.Checked)
        //    {
        //        iReturn = cbxFPagamento.SelectedValue.ToString();
        //    }
        //    else
        //    {
        //        DataSet dsFormasPagamento = con.SelectFormasPagamento();
        //        for (int i = 0; i < dsFormasPagamento.Tables[0].Rows.Count; i++)
        //        {
        //            //// Itext = dsFormasPagamento.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+ Itext;
        //            //iReturn = dsFormasPagamento.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+",";
        //            //if (i == dsFormasPagamento.Tables[0].Rows.Count)
        //            //{
        //            //    iReturn = iReturn + dsFormasPagamento.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
        //            //}

        //        }

        //    }
        //    return iReturn;

        //}

        private void rbSaida_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
