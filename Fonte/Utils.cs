using DexComanda.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Net.Mail;
using HumanAPIClient.Service;
using HumanAPIClient.Model;
using Microsoft.Win32;
using DexComanda.Models;
using System.ServiceProcess;
using DexComanda.Integração;
using Microsoft.SqlServer.Server;
using System.Linq;
using MySql.Data.MySqlClient;
using System.IO.Ports;
using System.Threading;
using System.IO;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Runtime.InteropServices;
using System.Collections;
using DexComanda.Relatorios.Delivery;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Diagnostics;
using System.Configuration;
using DexComanda.Relatorios.Clientes;
using DexComanda.Relatorios.Fechamentos.Novos;
using DexComanda.Operações.Financeiro;
using DexComanda.Operações;
using Microsoft.VisualBasic;
using DexComanda.Models.WS;
using Newtonsoft.Json;
using DexComanda.Operações.Funções;
using DexComanda.Relatorios.Fechamentos.Novos.Impressao_Termica;
using System.Data.Common;

namespace DexComanda
{
    public class Utils
    {
        private static Conexao conexao;
        private static SimpleSending cliente;
        private static SimpleMessage mensagem;
        private static string RetornoServ;
        private static DateTime DataInicial;
        private static string NomeEmpresa;
        private static DateTime DataFinal;
        private static string NomeCliente;
        private static int TotalSelecionado;
        private static bool Logado;
        public static int _CodUserLogado;
        private static string temp;
        private static MySqlConnection MysqlConnection;
        private static MySqlCommand MysqlCommand;
        private static MySqlDataAdapter MysqlDataAdapter;
        private static DataSet dados;
        private static DataSet mRetornoWS;
        private static TableLogOnInfos crtableLogoninfos;
        private static TableLogOnInfo crtableLogoninfo;
        private static ConnectionInfo crConnectionInfo;
        private static Tables CrTables;
        public static Boolean bMult;
        private static string strProxImpressora = "";
        private const string LinkServidor = "Server=mysql.expertsistemas.com.br;Port=3306;Database=exper194_lazaro;Uid=exper194_lazaro;Pwd=@@3412064;";
        public static Boolean EfetuarLogin(string nomeUsuario, string senha, bool iAbreFrmPrincipal = true, int iNumCaixa = 1, Boolean iAlterarUserLogado = false, string iTurno = "Dia")
        {

            try
            {
                if (nomeUsuario.Equals(""))
                {
                    MessageBox.Show("Informe seu usuário.");
                    Logado = false;
                }
                else if (senha.Equals(""))
                {
                    MessageBox.Show("Informe sua senha.");
                    Logado = false;
                }
                else
                {
                    Conexao conexao = new Conexao();
                    string hashSenha = EncryptMd5(nomeUsuario, senha);
                    DataSet dsUsuario = conexao.LoginUsuario(nomeUsuario, hashSenha);
                    if (dsUsuario.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("Usuário o senha incorretos");
                        Logado = false;

                    }
                    else if (hashSenha == dsUsuario.Tables[0].Rows[0].Field<string>("Senha").ToString())
                    {
                        if (iAlterarUserLogado)
                        {


                            Sessions.retunrUsuario = new Usuario()
                            {
                                Nome = nomeUsuario,
                                Senha = dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(),
                                Codigo = int.Parse(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()),
                                AcessaRelatoriosSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(6).ToString()),
                                AdministradorSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(5).ToString()),
                                FinalizaPedidoSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(8).ToString()),
                                CancelaPedidosSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(3).ToString()),
                                AlteraProdutosSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(5).ToString()),
                                DescontoPedidoSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(7).ToString()),
                                DescontoMax = Convert.ToDouble(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()),
                                AbreFechaCaixaSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(12).ToString()),
                                EditaPedidoSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(10).ToString()),
                                VisualizaDadosClienteSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(11).ToString()),
                                AlteraDadosClienteSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(12).ToString()),
                                CaixaLogado = iNumCaixa,
                                Turno = iTurno

                            };
                            Sessions.retunrUsuario = Sessions.retunrUsuario;
                        }
                        Logado = true;
                    }
                    else
                    {
                        MessageBox.Show("Usuário ou Senha incorretos.", "[XSistemas] Aviso");
                        Logado = false;
                    }
                }

                if (nomeUsuario.Equals("admin"))
                {
                    frmConfiguracoes frmConfiguracoes = new frmConfiguracoes();
                    frmConfiguracoes.ShowDialog();
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

            //conexao = new Conexao();
            //NomeEmpresa = Sessions.returnEmpresa.Nome;
            //DataSet usuarios = conexao.SelectAll("Usuario", "spObterUsuario");

            //DataView dv = usuarios.Tables[0].DefaultView;
            ////  Sessions.retunrUsuario = dv; 
            //string query = "Nome = '" + nomeUsuario + "' AND Senha = '" + hashSenha + "'";

            //dv.RowFilter = query;

            //    if (dv.Count > 0)
            //    {
            //        //_CodUserLogado = int.Parse(dv[0].Row["Codigo"].ToString());
            //        string _nome = dv[0].Row["Nome"].ToString();
            //        string _senha = dv[0].Row["Senha"].ToString();

            //        if (_nome.Equals(nomeUsuario) && _senha.Equals(hashSenha))
            //        {
            //            Sessions.retunrUsuario = new Usuario()
            //            {
            //                Nome = _nome,
            //                Senha = _senha,
            //                Codigo = Convert.ToInt16(dv[0].Row["Codigo"].ToString()),
            //                AcessaRelatoriosSN = Convert.ToBoolean(dv[0].Row["AcessaRelatoriosSN"].ToString()),
            //                AdministradorSN = Convert.ToBoolean(dv[0].Row["AdministradorSN"].ToString()),
            //                FinalizaPedidoSN = Convert.ToBoolean(dv[0].Row["FinalizaPedidoSN"].ToString()),
            //                CancelaPedidosSN = Convert.ToBoolean(dv[0].Row["CancelaPedidosSN"].ToString()),
            //                AlteraProdutosSN = Convert.ToBoolean(dv[0].Row["AlteraProdutosSN"].ToString()),
            //                DescontoPedidoSN = Convert.ToBoolean(dv[0].Row["DescontoPedidoSN"].ToString()),
            //                DescontoMax = Convert.ToDouble(dv[0].Row["DescontoMax"].ToString()),
            //                CaixaLogado = iNumCaixa
            //            };

            //            Sessions.retunrUsuario = Sessions.retunrUsuario;
            //            Logado = true;


            //        }
            //        else
            //        {
            //            MessageBox.Show("Usuário ou Senha incorretos.", "[XSistemas] Aviso");
            //            Logado = false;
            //        }
            //    }
            //    else if (nomeUsuario.Equals("admin"))
            //    {
            //        frmConfiguracoes frmConfiguracoes = new frmConfiguracoes();
            //        frmConfiguracoes.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Usuário ou senha incorretos", "[XSistemas] Aviso");
            //        Logado = false;
            //    }

            //}
            return Logado;
        }

        public static Usuario RetornaDadosUsuario(string iNome, string iSenha)
        {
            Conexao conexao = new Conexao();
            Usuario user = new Usuario();
            string hashSenha = EncryptMd5(iNome, iSenha);
            DataSet dsUsuario = conexao.LoginUsuario(iNome, hashSenha);

            if (dsUsuario.Tables[0].Rows.Count > 0)
            {
                user =
                new Usuario()
                {
                    Nome = iNome,
                    Codigo = int.Parse(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()),
                    Senha = dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(),
                    CancelaPedidosSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(3).ToString()),
                    AlteraProdutosSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(4).ToString()),
                    AdministradorSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(5).ToString()),
                    AcessaRelatoriosSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(6).ToString()),
                    DescontoPedidoSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(7).ToString()),
                    FinalizaPedidoSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(8).ToString()),
                    DescontoMax = Convert.ToDouble(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()),
                    EditaPedidoSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(10).ToString()),
                    VisualizaDadosClienteSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(11).ToString()),
                    AbreFechaCaixaSN = Convert.ToBoolean(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(12).ToString()),
                    AlteraDadosClienteSN = Boolean.Parse(dsUsuario.Tables[0].Rows[0].ItemArray.GetValue(13).ToString()),

                    // CaixaLogado = iNumCaixa

                };

            }
            else
            {
                MessageBox.Show("Usuario ou Senha incorretos");
            }

            return user;
        }
        public static Boolean ValidaPermissao(int iCodUser, string iNomePermissao)
        {
            Boolean retur = false;
            Conexao con = new Conexao();
            DataSet ds = con.SelectRegistroPorCodigo("Usuario", "spObterUsuarioPorCodigo", iCodUser, true);
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                if (iNomePermissao == ds.Tables[0].Columns[i].ColumnName)
                {
                    retur = Boolean.Parse(ds.Tables[0].Rows[0].ItemArray.GetValue(i).ToString());
                    break;
                }
            }

            if (!retur)
            {
                if (MessageBoxQuestion(Bibliotecas.cSolicitarPermissao))
                {
                    frmLiberação frm = new frmLiberação(iNomePermissao);
                    //frm.ShowDialog();
                    if (frm.Autorizacao)
                    {
                        retur = true;
                        frm.Close();
                    }
                    else
                    {
                        MessageBox.Show(Bibliotecas.cUsuarioSemPermissao);
                    }
                }

            }
            else
            {
                retur = true;
            }
            return retur;

        }
        public static void HistoricoCancelamentos(int iCodPessoa)
        {
            int intQuantidadeCancelamento = conexao.SelectRegistroPorCodigo("HistoricoCancelamentos", "spObterCancelamentoPorPessoa", iCodPessoa).Tables[0].Rows.Count;
            if (intQuantidadeCancelamento > 0)
            {

                if (MessageBoxQuestion("Cliente possui " + intQuantidadeCancelamento + "  Cancelamento(s) Deseja visualizar ?"))
                {
                    frmExibeCancelamentos frm = new frmExibeCancelamentos(iCodPessoa);
                    frm.ShowDialog();
                }
            }
        }
        public static string GravaJson(string iPlataforma, string iUrl)
        {
            DadosApp dadosApp = new DadosApp();
            dadosApp.plataforma = iPlataforma;
            dadosApp.url = iUrl;
            return '[' + JsonConvert.SerializeObject(dadosApp, Formatting.None) + ']';

        }
        public static string SerializaObjeto(List<PrecoDiaProduto> iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(List<CidadesAtendidas> iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }

        public static List<PrecoDiaProduto> DeserializaObjeto(string iValores)
        {
            if (iValores == "")
            {
                return null;
            }
            return JsonConvert.DeserializeObject<List<PrecoDiaProduto>>(iValores);

        }
        public static void MontaCombox(ComboBox icbxName, string idisplayName,
            string iValueMember, string iTable, string iSP, int iCod = -1)
        {
            try
            {
                if (iCod == -1)
                {
                    icbxName.DataSource = conexao.SelectAll(iTable, iSP).Tables[iTable];
                }
                else
                {
                    DataSet ds = conexao.SelectRegistroPorCodigo(iTable, iSP, iCod);
                    icbxName.DataSource = ds.Tables[0];
                    // icbxName.Text = ds.Tables[0].Rows[0].Field<string>("NomeGrupo");
                }


                icbxName.DisplayMember = idisplayName;
                icbxName.ValueMember = iValueMember;
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao listar itens do " + icbxName + erro.Message);
            }

        }
        public static void MontaCombox(ComboBox icbxName, string idisplayName,
           string iValueMember, string iGrupo)
        {
            try
            {

                DataSet ds = conexao.SelectRegistroPorNome("@GrupoProduto", "Produto", "spObterProdutoPorGrupo", iGrupo);
                icbxName.DataSource = ds.Tables[0];
                icbxName.DisplayMember = idisplayName;
                icbxName.ValueMember = iValueMember;
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao listar itens do " + icbxName + erro.Message);
            }

        }

        public static bool VerificaCaixaAbertoDiaAnterior()
        {
            Boolean CaixaAberto = false;
            DataSet dsCaixa = conexao.SelectRegistroPorData("Caixa", "spObterDadosCaixa", DateTime.Now.AddDays(-1));
            DataRow dRowCaixa;
            for (int i = 0; i < dsCaixa.Tables[0].Rows.Count; i++)
            {
                dRowCaixa = dsCaixa.Tables[0].Rows[i];
                CaixaAberto = dRowCaixa.ItemArray.GetValue(7).ToString() == "0";
            }
            return CaixaAberto;
        }
        public static Boolean MessageBoxQuestion(string iMessage)
        {
            Boolean iResposta = false;
            DialogResult resultado = MessageBox.Show(iMessage, "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                iResposta = true;
            }
            return iResposta;
        }
        public static Boolean ImputStringQuestion()
        {
            Boolean iReturn = false;
            string strValor = Interaction.InputBox("Informe a senha mestre", "[xSistemas]", "", 100, 200);
            iReturn = strValor == "xAdminx77";

            return iReturn;


        }
        public static void ImprimirHistoricoCliente_Epson(int iCodPessoa, DateTime iDtInici, DateTime idtFim)
        {
            RelHistoricoCliente_Epson report;
            try
            {
                report = new RelHistoricoCliente_Epson();

                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                report.Load(Directory.GetCurrentDirectory() + @"\RelHistoricoCliente_Epson.rpt");
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

                report.SetParameterValue("@CodPessoa", iCodPessoa);
                report.SetParameterValue("@DataInicio", iDtInici.Date);
                report.SetParameterValue("@DataFim", idtFim.Date);


                report.PrintToPrinter(0, true, 0, 0);
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }
        public static void ImprimirHistoricoCliente(int iCodPessoa, DateTime iDtInici, DateTime idtFim)
        {
            RelHistoricoCliente report;
            try
            {
                report = new RelHistoricoCliente();

                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                report.Load(Directory.GetCurrentDirectory() + @"\RelHistoricoCliente.rpt");
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

                report.SetParameterValue("@CodPessoa", iCodPessoa);
                report.SetParameterValue("@DataInicio", iDtInici.Date);
                report.SetParameterValue("@DataFim", idtFim.Date);


                report.PrintToPrinter(0, true, 0, 0);
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }
        public static string ImpressaoEntre_Epson(int iCodPedido, decimal iValorPago, string iPrevisaoEntrega, Boolean iExport = false, int iNumCopias = 0)
        {
            string iRetorno = ""; ;

            RelDelivery_Epson
                report;
            try
            {
                report = new RelDelivery_Epson();
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                report.Load(Directory.GetCurrentDirectory() + @"\RelDelivery_Epson.rpt");
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

                report.SetParameterValue("@Codigo", iCodPedido);
                report.SetParameterValue("ValorPago", iValorPago);
                report.SetParameterValue("PrevEntrega", iPrevisaoEntrega);
                if (iExport)
                {
                    CrystalDecisions.Shared.DiskFileDestinationOptions reportExport =
                    new CrystalDecisions.Shared.DiskFileDestinationOptions();
                    reportExport.DiskFileName = Directory.GetCurrentDirectory() + @"\RelDelivery_Epson.txt";

                    report.ExportOptions.ExportDestinationType =
                    CrystalDecisions.Shared.ExportDestinationType.DiskFile;

                    report.ExportOptions.ExportFormatType =
                    CrystalDecisions.Shared.ExportFormatType.Text;

                    report.ExportOptions.DestinationOptions = reportExport;
                    report.Export();
                    iRetorno = Directory.GetCurrentDirectory() + @"\RelDelivery_Epson.txt";
                }
                else
                {
                    for (int i = 0; i < iNumCopias; i++)
                    {
                        report.PrintToPrinter(1, false, 0, 0);
                    }


                }
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro na impressao :" + erro.Message);
            }
            return iRetorno;
        }
        public static string ImpressaoEntreganova(int iCodPedido, decimal iValorPago, string iPrevisaoEntrega, Boolean iExport = false, int iNumCopias = 0)
        {
            string iRetorno = ""; ;

            RelDelivery report;
            report = new RelDelivery();
            try
            {

                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                try
                {

                    report.Load(Directory.GetCurrentDirectory() + @"\RelDelivery.rpt");
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

                    report.SetParameterValue("@Codigo", iCodPedido);
                    report.SetParameterValue("ValorPago", iValorPago);
                    report.SetParameterValue("PrevEntrega", iPrevisaoEntrega);
                    if (iExport)
                    {
                        CrystalDecisions.Shared.DiskFileDestinationOptions reportExport =
                        new CrystalDecisions.Shared.DiskFileDestinationOptions();
                        reportExport.DiskFileName = Directory.GetCurrentDirectory() + @"\RelDelivery.txt";

                        report.ExportOptions.ExportDestinationType =
                        CrystalDecisions.Shared.ExportDestinationType.DiskFile;

                        report.ExportOptions.ExportFormatType =
                        CrystalDecisions.Shared.ExportFormatType.Text;

                        report.ExportOptions.DestinationOptions = reportExport;
                        report.Export();
                        iRetorno = Directory.GetCurrentDirectory() + @"\RelDelivery.txt";
                    }
                    else
                    {
                        for (int i = 0; i < iNumCopias; i++)
                        {
                            report.PrintToPrinter(1, false, 0, 0);
                        }


                    }
                }
                catch (Exception erro)
                {

                    MessageBox.Show("Erro na impressao :" + erro.Message);
                }
            }
            finally
            {
                report.Dispose();

            }
            return iRetorno;
        }

        public static string ImpressaoEntreganova_Matricial(int iCodPedido, decimal iValorPago, string iPrevisaoEntrega, Boolean iExport = false, int iNumCopias = 0)
        {
            string iRetorno = ""; ;

            RelDelivery_Matricial report;
            try
            {
                report = new RelDelivery_Matricial();
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                report.Load(Directory.GetCurrentDirectory() + @"\RelDelivery_Matricial.rpt");
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

                report.SetParameterValue("@Codigo", iCodPedido);
                report.SetParameterValue("ValorPago", iValorPago);
                report.SetParameterValue("PrevEntrega", iPrevisaoEntrega);
                if (iExport)
                {
                    CrystalDecisions.Shared.DiskFileDestinationOptions reportExport =
                    new CrystalDecisions.Shared.DiskFileDestinationOptions();
                    reportExport.DiskFileName = Directory.GetCurrentDirectory() + @"\RelDelivery.txt";

                    report.ExportOptions.ExportDestinationType =
                    CrystalDecisions.Shared.ExportDestinationType.DiskFile;

                    report.ExportOptions.ExportFormatType =
                    CrystalDecisions.Shared.ExportFormatType.Text;

                    report.ExportOptions.DestinationOptions = reportExport;
                    report.Export();
                    iRetorno = Directory.GetCurrentDirectory() + @"\RelDelivery_Matricial.txt";
                }
                else
                {
                    for (int i = 0; i < iNumCopias; i++)
                    {
                        report.PrintToPrinter(0, true, 0, 0);
                    }

                }
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ná impressao :" + erro.Message);
            }
            return iRetorno;
        }
        public static string ImpressaoEntreganova_20(int iCodPedido, decimal iValorPago, Boolean iExport = false, int iNumCopias = 0)
        {
            string iRetorno = "";
            RelDelivery_20 report;
            report = new RelDelivery_20();
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;
            try
            {

                try
                {

                    report.Load(Directory.GetCurrentDirectory() + @"\RelDelivery_20.rpt");
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


                    report.SetParameterValue("@Codigo", iCodPedido);
                    report.SetParameterValue("ValorPago", iValorPago);
                    if (iExport)
                    {
                        CrystalDecisions.Shared.DiskFileDestinationOptions reportExport =
                        new CrystalDecisions.Shared.DiskFileDestinationOptions();
                        reportExport.DiskFileName = Directory.GetCurrentDirectory() + @"\RelDelivery_20.txt";

                        report.ExportOptions.ExportDestinationType =
                        CrystalDecisions.Shared.ExportDestinationType.DiskFile;

                        report.ExportOptions.ExportFormatType =
                        CrystalDecisions.Shared.ExportFormatType.Text;

                        report.ExportOptions.DestinationOptions = reportExport;
                        report.Export();
                        iRetorno = Directory.GetCurrentDirectory() + @"\RelDelivery_20.txt";
                    }
                    else
                    {
                        for (int i = 0; i < iNumCopias; i++)
                        {
                            report.PrintToPrinter(0, false, 0, 0);
                        }

                    }
                }
                finally
                {
                    report.Dispose();
                }

            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.InnerException.Message);
            }
            return iRetorno;
        }
        public static string ImpressaoCozihanova(int iCodPedido, Boolean iExport = false, int iNumCopias = 0)
        {
            string iRetorno = ""; ;

            RelDelivey_Cozinha report;
            try
            {
                report = new RelDelivey_Cozinha();
                //  report.PrintOptions.PrinterName = "MP-4200 TH";
                crtableLogoninfos = new TableLogOnInfos();
                crtableLogoninfo = new TableLogOnInfo();
                crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                report.Load(Directory.GetCurrentDirectory() + @"\RelDelivey_Cozinha.rpt");
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
                report.SetParameterValue("@Codigo", iCodPedido);
                if (iExport)
                {
                    CrystalDecisions.Shared.DiskFileDestinationOptions reportExport =
                    new CrystalDecisions.Shared.DiskFileDestinationOptions();
                    reportExport.DiskFileName = Directory.GetCurrentDirectory() + @"\RelDelivey_Cozinha.txt";

                    report.ExportOptions.ExportDestinationType =
                    CrystalDecisions.Shared.ExportDestinationType.DiskFile;

                    report.ExportOptions.ExportFormatType =
                    CrystalDecisions.Shared.ExportFormatType.Text;

                    report.ExportOptions.DestinationOptions = reportExport;
                    report.Export();
                    iRetorno = Directory.GetCurrentDirectory() + @"\RelDelivey_Cozinha.txt";
                }
                else
                {
                    for (int i = 0; i < iNumCopias; i++)
                    {
                        report.PrintToPrinter(1, false, 0, 0);
                    }

                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.InnerException.Message);
            }
            return iRetorno;
        }
        /// <summary>
        /// Função para imprimir Pedido
        /// </summary>
        /// <param name="iCodPedido">Inteiro Código do Pedido a ser impresso</param>
        ///  <param name="iExport">Boolean Código do Pedido a ser impresso</param>
        /// <returns>String do Pedido para impressoras Matriciais quando iExpport for True.</returns>
        public static string ImpressaMesaNova(int iCodPedido, int iCodGupo, Boolean iExport = false, int iNumCopias = 0, string iNomeImpressora = "", Boolean iImprimirAgora = false)
        {
            string iRetorno = "";
            RelComandaMesa report;
            report = new RelComandaMesa();
            crtableLogoninfos = new TableLogOnInfos();
            crtableLogoninfo = new TableLogOnInfo();
            crConnectionInfo = new ConnectionInfo();
            try
            {

                try
                {

                    System.Drawing.Printing.PrinterSettings printersettings = new System.Drawing.Printing.PrinterSettings();
                    printersettings.PrinterName = iNomeImpressora;
                    printersettings.Copies = 1;
                    printersettings.Collate = false;

                    Tables CrTables;
                    if (iNomeImpressora != "")
                    {
                        report.PrintOptions.PrinterName = iNomeImpressora;
                    }

                    report.Load(Directory.GetCurrentDirectory() + @"\RelComandaMesa.rpt");
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
                    report.SetParameterValue("@Codigo", iCodPedido);
                    report.SetParameterValue("@CodGrupo", iCodGupo);

                    if (report.Rows.Count > 0)
                    {


                        if (iExport)
                        {
                            CrystalDecisions.Shared.DiskFileDestinationOptions reportExport =
                            new CrystalDecisions.Shared.DiskFileDestinationOptions();
                            reportExport.DiskFileName = Directory.GetCurrentDirectory() + @"\RelComandaMesa.txt";

                            report.ExportOptions.ExportDestinationType =
                            CrystalDecisions.Shared.ExportDestinationType.DiskFile;

                            report.ExportOptions.ExportFormatType =
                            CrystalDecisions.Shared.ExportFormatType.Text;

                            report.ExportOptions.DestinationOptions = reportExport;
                            report.Export();
                            iRetorno = Directory.GetCurrentDirectory() + @"\RelComandaMesa.txt";
                        }
                        else
                        {
                            for (int i = 0; i < iNumCopias; i++)
                            {
                                //if (iNomeImpressora=="")
                                //{
                                report.PrintToPrinter(1, false, 0, 0);
                                //}
                                //else
                                //{

                                //}

                            }

                        }
                    }
                }
                finally
                {
                    // report.Dispose();

                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.InnerException.Message);
            }
            return iRetorno;
        }

        public static string RelCaixaHistorico(DateTime idtInicio,DateTime idtFim,string iNumcaixa,string iEntradaSaida,string iCodPagamento,string iTurno)
        {
            string iRetorno = ""; ;

            RelCaixaHistorico report;
            report = new RelCaixaHistorico();
            try
            {
                crtableLogoninfos = new TableLogOnInfos();
                crtableLogoninfo = new TableLogOnInfo();
                crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

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

                report.SetParameterValue("@Turno", "Dia");
                report.SetParameterValue("@CodCaixa", "1");
                report.SetParameterValue("@DataI", "01/08/2016");
                report.SetParameterValue("@DataF", "31/08/2016");
                report.SetParameterValue("@CodPagamento", "1");
                report.SetParameterValue("@EntradaSaida", "E");

                report.SaveAs("Rel", false);
                report.PrintToPrinter(1, false, 0, 0);
               

            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message + " - " + erro.StackTrace);
            }
            return iRetorno;
        }
        //public static string ReportFechamento_Novo(DateTime iDataI,DateTime iDataFim)
        //{
        //    string iRetorno = ""; ;
        //    RelFechamento_Novo report;
        //    try
        //    {
        //        report = new RelFechamento_Novo();

        //        crtableLogoninfos = new TableLogOnInfos();
        //        crtableLogoninfo = new TableLogOnInfo();
        //        crConnectionInfo = new ConnectionInfo();
        //        Tables CrTables;

        //        report.Load(Directory.GetCurrentDirectory() + @"\RelFechamento_Novo.rpt");
        //        crConnectionInfo.ServerName = Sessions.returnEmpresa.Servidor;
        //        crConnectionInfo.DatabaseName = Sessions.returnEmpresa.Banco;
        //        crConnectionInfo.UserID = "dex";
        //        crConnectionInfo.Password = "1234";

        //        CrTables = report.Database.Tables;
        //        foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
        //        {
        //            crtableLogoninfo = CrTable.LogOnInfo;
        //            crtableLogoninfo.ConnectionInfo = crConnectionInfo;
        //            CrTable.ApplyLogOnInfo(crtableLogoninfo);
        //        }
        //        report.SetParameterValue("DataInicio", DateTime.Now);
        //        report.SetParameterValue("DataFim", DateTime.Now);

        //    }
        //    catch (Exception erro)
        //    {

        //        MessageBox.Show(erro.InnerException.Message);
        //    }
        //    return iRetorno;
        //}
        public static string ImpressaoFechamentoNovo(int iCodPedido, Boolean iExport = false, int iNumCopias = 0)
        {
            string iRetorno = "";
            try
            {
                RelFechamentoMesa report;
                report = new RelFechamentoMesa();


                try
                {


                    crtableLogoninfos = new TableLogOnInfos();
                    crtableLogoninfo = new TableLogOnInfo();
                    crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    report.Load(Directory.GetCurrentDirectory() + @"\RelFechamentoMesa.rpt");
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
                    report.SetParameterValue("@Codigo", iCodPedido);
                    if (iExport)
                    {
                        CrystalDecisions.Shared.DiskFileDestinationOptions reportExport =
                        new CrystalDecisions.Shared.DiskFileDestinationOptions();
                        reportExport.DiskFileName = Directory.GetCurrentDirectory() + @"\RelFechamentoMesa.txt";

                        report.ExportOptions.ExportDestinationType =
                        CrystalDecisions.Shared.ExportDestinationType.DiskFile;

                        report.ExportOptions.ExportFormatType =
                        CrystalDecisions.Shared.ExportFormatType.Text;

                        report.ExportOptions.DestinationOptions = reportExport;
                        report.Export();
                        iRetorno = Directory.GetCurrentDirectory() + @"\RelFechamentoMesa.txt";
                    }
                    else
                    {

                        report.PrintToPrinter(0, true, 0, 0);
                    }
                }
                finally
                {
                    report.Dispose();
                }
            }

            catch (Exception erro)
            {

                MessageBox.Show(erro.InnerException.Message);
            }
            return iRetorno;
        }
        public static string ImpressaoFechamentoNovo_20(int iCodPedido, Boolean iExport = false, int iNumCopias = 0)
        {
            string iRetorno = ""; ;
            RelFechamentoMesa_20 report;

            try
            {
                report = new RelFechamentoMesa_20();

                try
                {


                    crtableLogoninfos = new TableLogOnInfos();
                    crtableLogoninfo = new TableLogOnInfo();
                    crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    report.Load(Directory.GetCurrentDirectory() + @"\RelFechamentoMesa_20.rpt");
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
                    report.SetParameterValue("@Codigo", iCodPedido);
                    if (iExport)
                    {
                        CrystalDecisions.Shared.DiskFileDestinationOptions reportExport =
                        new CrystalDecisions.Shared.DiskFileDestinationOptions();
                        reportExport.DiskFileName = Directory.GetCurrentDirectory() + @"\RelFechamentoMesa_20.txt";

                        report.ExportOptions.ExportDestinationType =
                        CrystalDecisions.Shared.ExportDestinationType.DiskFile;

                        report.ExportOptions.ExportFormatType =
                        CrystalDecisions.Shared.ExportFormatType.Text;

                        report.ExportOptions.DestinationOptions = reportExport;
                        report.Export();
                        iRetorno = Directory.GetCurrentDirectory() + @"\RelFechamentoMesa_20.txt";
                    }
                    else
                    {

                        report.PrintToPrinter(0, true, 0, 0);
                    }
                }
                finally
                {
                    report.Dispose();

                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.InnerException.Message);
            }
            return iRetorno;
        }
        public static string ImpressaoBalcao(int iCodPedido, Boolean iExport = false, int iNumCopias = 0)
        {
            string iRetorno = ""; ;
            RelBalcao report;
            try
            {
                report = new RelBalcao();

                crtableLogoninfos = new TableLogOnInfos();
                crtableLogoninfo = new TableLogOnInfo();
                crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                report.Load(Directory.GetCurrentDirectory() + @"\RelBalcao.rpt");
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
                report.SetParameterValue("@Codigo", iCodPedido);
                if (iExport)
                {
                    CrystalDecisions.Shared.DiskFileDestinationOptions reportExport =
                    new CrystalDecisions.Shared.DiskFileDestinationOptions();
                    reportExport.DiskFileName = Directory.GetCurrentDirectory() + @"\RelBalcao.txt";

                    report.ExportOptions.ExportDestinationType =
                    CrystalDecisions.Shared.ExportDestinationType.DiskFile;

                    report.ExportOptions.ExportFormatType =
                    CrystalDecisions.Shared.ExportFormatType.Text;

                    report.ExportOptions.DestinationOptions = reportExport;
                    report.Export();
                    iRetorno = Directory.GetCurrentDirectory() + @"\RelBalcao.txt";
                }
                else
                {
                    report.PrintToPrinter(0, false, 0, 0);
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.InnerException.Message);
            }
            return iRetorno;
        }

        public static string ImpressaoCaixa(int iCaixa, string iTurno, DateTime dtInicio, DateTime dtFim)
        {
            string iRetorno = ""; ;

            RelFechamentoCaixa report;
            report = new RelFechamentoCaixa();
            try
            {

                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                try
                {

                    string str = Directory.GetCurrentDirectory();
                    report.Load(Directory.GetCurrentDirectory() + @"\RelFechamentoCaixa.rpt");
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

                    report.SetParameterValue("@Caixa", iCaixa);
                    report.SetParameterValue("@Turno", iTurno);
                    report.SetParameterValue("@DataI", dtInicio);
                    report.SetParameterValue("@DataF", dtFim);

                    report.PrintToPrinter(1, false, 0, 0);



                }
                catch (Exception erro)
                {

                    MessageBox.Show("Erro na impressao :" + erro.Message);
                }
            }
            finally
            {
                report.Dispose();

            }
            return iRetorno;
        }
        public static Boolean LicencaSomenteOnline(string iCNPJ)
        {
            Boolean iReturn = false;


            return iReturn;
        }
        public static void LancarMovimentoCaixa(CaixaMovimento caixa)
        {
            caixa = new Models.CaixaMovimento()
            {
                CodCaixa = caixa.CodCaixa,
                CodFormaPagamento = caixa.CodFormaPagamento,
                Data = caixa.Data,
                Historico = caixa.Historico,
                NumeroDocumento = caixa.NumeroDocumento,
                Tipo = caixa.Tipo,
                Valor = caixa.Valor
            };
            conexao.Insert("spInserirMovimentoCaixa", caixa);
        }

        public static void IniciaSistema()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Splashcreen());


        }

        public static bool CaixaAberto(DateTime iDataRegistro, int iNumero, string iTurno)
        {
            bool iRetorno = false;
            DataRow dRow;
            DataSet dsCaixa;
            conexao = new Conexao();
            try
            {
                dsCaixa = conexao.RetornaCaixaPorTurno(iNumero, iTurno, Convert.ToDateTime(iDataRegistro.ToShortDateString()));
                if (dsCaixa.Tables[0].Rows.Count > 0)
                {
                    dRow = dsCaixa.Tables[0].Rows[0];
                    iRetorno = dRow.ItemArray.GetValue(7).ToString() == Convert.ToString(false);
                }
                else

                {
                    if (Sessions.returnEmpresa.CNPJ != Bibliotecas.cCasteloPlus && Sessions.returnEmpresa.CNPJ != Bibliotecas.cTopsAcai && Sessions.returnEmpresa.CNPJ != Bibliotecas.cElShaday && Sessions.returnEmpresa.CNPJ != Bibliotecas.cCarangoVix)
                    {
                        if (ValidaPermissao(Sessions.retunrUsuario.Codigo, "AbreFechaCaixaSN"))
                        {
                            if (MessageBoxQuestion("O Caixa não esta aberto sistema funcionará somente no modo consulta, deseja abrir agora?"))
                            {
                                frmAberturaCaixa frm = new frmAberturaCaixa();
                                frm.ShowDialog();
                            }
                            else
                            {
                                iRetorno = true;
                            }
                        }
                    }
                    else
                    {
                        iRetorno = true;
                    }

                    // MessageBox.Show("O Caixa não esta aberto, sistema funcionará somente no modo consulta");
                }

                //}
                //else
                //{
                //    iRetorno = true;
                //}
            }
            catch (Exception erro)
            {
                if (erro.Message.ToString() == "There is no row at position 0.")
                {
                    MessageBox.Show("Numero de caixa Inexiste , favor verificar");
                    // Environment.Exit(0);

                }
                else
                {
                    MessageBox.Show(erro.Message, "[XSistemas] Algo de errado aconteceu");
                }

            }


            return iRetorno;
        }
        public static void RepetirUltimoPedido(int iCodCliente, Main iMain = null)
        {
            DataSet ds;
            int CodPessoa, CodPedido;
            string FormaPagamento;
            decimal TaxaEntrega;
            // Retorna o Ultimo Pedido do Cliente
            conexao = new Conexao();
            ds = conexao.SelectObterUltimoPedido(iCodCliente);
            if (ds != null)
            {
                DataRow Linhas = ds.Tables[0].Rows[0];
                CodPedido = int.Parse(Linhas.ItemArray.GetValue(0).ToString());
                CodPessoa = int.Parse(Linhas.ItemArray.GetValue(1).ToString());
                FormaPagamento = Linhas.ItemArray.GetValue(3).ToString();
                // Retorna a Taxa de Entrega do cadastro do Cliente
                TaxaEntrega = Utils.RetornaTaxaPorCliente(CodPessoa, conexao);

                frmCadastrarPedido frmRepetePedido = new frmCadastrarPedido(true, "0,00", "", "", TaxaEntrega, false, DateTime.Now, CodPedido, CodPessoa,
                                                                            "", FormaPagamento, "", "Balcao", iMain, 0.00M);
                frmRepetePedido.ShowDialog();


            }


        }

        public static void AtualizaMesa(int iCodigoMesa, int iStatus)
        {
            try
            {
                conexao = new Conexao();
                Mesas mesas = new Mesas()
                {
                    Codigo = iCodigoMesa,
                    //NumeroMesa = iNumeroMesa,
                    StatusMesa = iStatus
                };
                conexao.Update("spAlteraStatusMesa", mesas);
            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possível alterar o Status da mesa " + erro.Message);
            }

        }

        public static int RetornaCodigoMesa(string iNumMesa)
        {
            DataSet iDados;
            int iRetorno = 0;
            conexao = new Conexao();
            iDados = conexao.SelectRegistroPorCodigo("Mesas", "spObterCodigoMesa", 0, iNumMesa);
            if (iDados != null)
            {
                DataRow iLinha = iDados.Tables[0].Rows[0];
                iRetorno = int.Parse(iLinha.ItemArray.GetValue(0).ToString());
            }

            return iRetorno;

        }

        public static string RetornaNumeroMesa(int iCodigo)
        {
            DataSet iDados;
            string iRetorno = "";
            conexao = new Conexao();
            iDados = conexao.SelectRegistroPorCodigo("Mesas", "spObterNumeroMesa", iCodigo);
            if (iDados != null)
            {
                DataRow iLinha = iDados.Tables[0].Rows[0];
                iRetorno = iLinha.ItemArray.GetValue(0).ToString();
            }

            return iRetorno;

        }
        public static bool SoPermiteNumeros(KeyPressEventArgs Evento)
        {
            bool iRetorno = true;
            Keys back = Keys.Back;
            char[] whitespace = { '\x09', '\x20', '\xA0' };
            if (!Char.IsNumber(Evento.KeyChar) && Evento.KeyChar != Convert.ToChar(back)
                && Evento.KeyChar != Convert.ToChar(Keys.Enter) && Evento.KeyChar != Convert.ToChar(Keys.Escape))
            {
                Evento.Handled = true;
                iRetorno = false;
                MessageBox.Show("Esse campo só permite números", "Aviso Dex");

            }
            return iRetorno;

        }
        public static bool SoDecimais(KeyPressEventArgs Evento)
        {
            bool iRetorno = true;
            Keys back = Keys.Back;
            char[] whitespace = { '\x09', '\x20', '\xA0' };
            if (!Char.IsNumber(Evento.KeyChar) && Evento.KeyChar != Convert.ToChar(back)
                && Evento.KeyChar != Convert.ToChar(Keys.Enter) && Evento.KeyChar != Convert.ToChar(","))
            {
                Evento.Handled = true;
                iRetorno = false;
                MessageBox.Show("Esse campo só permite números", "Aviso Dex");

            }
            return iRetorno;

        }
        public static void ExibirDadosForm(Control parent, Boolean iExibir)
        {
            foreach (System.Windows.Forms.Control ctrControl in parent.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.TextBox)))
                {
                    //Check to see if it's a textbox 
                    if (!iExibir && ((System.Windows.Forms.TextBox)ctrControl).Name != "txtNomeCliente")
                    {
                        ((System.Windows.Forms.TextBox)ctrControl).PasswordChar = 'X';

                    }

                    //If it is then set the text to String.Empty (empty textbox) 
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RichTextBox)))
                {
                    //If its a RichTextBox clear the text
                    ((System.Windows.Forms.RichTextBox)ctrControl).Visible = iExibir;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.ComboBox)))
                {
                    //Next check if it's a dropdown list 
                    ((System.Windows.Forms.ComboBox)ctrControl).Visible = iExibir;
                    //If it is then set its SelectedIndex to 0 
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.CheckBox)))
                {
                    //Next uncheck all checkboxes
                    ((System.Windows.Forms.CheckBox)ctrControl).Visible = iExibir;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    //Unselect all RadioButtons
                    ((System.Windows.Forms.RadioButton)ctrControl).Visible = iExibir;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.PictureBox)))
                {
                    ((System.Windows.Forms.PictureBox)ctrControl).Visible = iExibir;
                    // (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))(
                }
            }
        }
        public static void LimpaForm(System.Windows.Forms.Control parent)
        {
            foreach (System.Windows.Forms.Control ctrControl in parent.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.TextBox)))
                {
                    //Check to see if it's a textbox 
                    ((System.Windows.Forms.TextBox)ctrControl).Text = "";
                    //If it is then set the text to String.Empty (empty textbox) 
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RichTextBox)))
                {
                    //If its a RichTextBox clear the text
                    ((System.Windows.Forms.RichTextBox)ctrControl).Text = string.Empty;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.ComboBox)))
                {
                    //Next check if it's a dropdown list 
                    ((System.Windows.Forms.ComboBox)ctrControl).SelectedIndex = -1;
                    //If it is then set its SelectedIndex to 0 
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.CheckBox)))
                {
                    //Next uncheck all checkboxes
                    ((System.Windows.Forms.CheckBox)ctrControl).Checked = false;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    //Unselect all RadioButtons
                    ((System.Windows.Forms.RadioButton)ctrControl).Checked = false;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.PictureBox)))
                {
                    ((System.Windows.Forms.PictureBox)ctrControl).Image = null;
                    // (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))(
                }
            }
        }

        public static string CriaArquivoTxt(string iNomeArquivo, string iText)
        {
            try
            {
                temp = Directory.GetCurrentDirectory() + @"\" + iNomeArquivo + ".txt";

                if (!System.IO.File.Exists(temp))
                {
                    System.IO.File.Create(temp).Close();
                    System.IO.File.AppendAllText(temp, iText);

                }
                else
                {

                    StreamReader tempDex = new StreamReader(temp);
                    ArrayList arrText = new ArrayList();
                    iText = tempDex.ReadLine();

                }
            }
            catch (Exception Erro)
            {

                MessageBox.Show("Não foi possivel criar o arquivo em :" + temp + " favor verificar se possui privilégios no destino" +
                                " Informar o erro ao suporte " + Erro.Message);
            }
            return iText;
        }
        //public static string[] RetornoTxt()
        //{
        //    string []linhas;
        //    int cont = 0;
        //    string Caminho;

        //    try
        //    {
        //        Caminho = Directory.GetCurrentDirectory() +@"\ConfigSMS.txt";
        //        System.IO.StreamReader file = new System.IO.StreamReader(Caminho);

        //        while ((linhas = file.ReadLine()) != null)
        //        {
        //            Arquivo.Add(linhas);
        //            cont++;
        //        }

        //        file.Close();
        //    }
        //    catch (Exception e)
        //    {

        //        MessageBox.Show("Arquivo para envio de SMS não encontrado em" + e.Message);
        //    }

        //    return Arquivo;

        //}

        public static void ImpressaoLPT1(string iArquivo, string iPorta)
        {
            try
            {
                string iCaminho = Directory.GetCurrentDirectory() + @"\" + "Impressao.txt";
                System.IO.File.Copy(iCaminho, iPorta);
                System.IO.File.Copy("#10" + "#17", iPorta);
                System.IO.File.Delete(iCaminho);
            }
            catch (Exception E)
            {

                MessageBox.Show("Não foi possivel imprimir o Pedido, favor tentar novamente ou informar o erro ao suporter " + E.Message, "Aviso Dex");
            }

        }
        public static void ImpressaoSerial(string iArquivo, string iPorta, int iVelocidade)
        {
            try
            {
                SerialPort _serialPort = new SerialPort(iPorta);
                _serialPort.PortName = iPorta;
                _serialPort.BaudRate = iVelocidade;
                _serialPort.Parity = Parity.None;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.ReadTimeout = 300;
                //_serialPort.WriteTimeout = 500;
                _serialPort.NewLine = System.Environment.NewLine;

                _serialPort.Open();

                if (_serialPort.IsOpen)
                {
                    _serialPort.WriteLine(iArquivo);
                    _serialPort.Close();
                    Thread.Sleep(15);
                }
                else
                {
                    MessageBox.Show("Porta " + iPorta + " Não aberta");
                }

            }
            catch (Exception E)
            {

                MessageBox.Show("Não foi possivel imprimir o Pedido, favor tentar novamente ou informar o erro ao suporte " + E.Message, "Aviso Dex");
            }



        }
        public string EnviaEmail(string iProvedor, string iFrom, string iPara, string iAssunto, string iMensagem, bool iSSL, string iUsuario, string ISenha)
        {
            System.Net.Mail.MailMessage Email = new System.Net.Mail.MailMessage();
            Email.To.Add(iPara);
            Email.Subject = iMensagem;
            Email.From = new System.Net.Mail.MailAddress(iFrom);
            Email.Body = iMensagem;

            System.Net.Mail.SmtpClient smpt = new System.Net.Mail.SmtpClient();
            smpt.Host = iProvedor;
            smpt.EnableSsl = iSSL;

            smpt.Credentials = new System.Net.NetworkCredential(iUsuario, ISenha);
            smpt.Send(Email);

            return smpt.ToString();
        }
        public static string EnvioSMS(string iPara, string iMessagem, string iFrom)
        {
            cliente = new SimpleSending("xsistemas", "IdJQcMl5DU");
            mensagem = new SimpleMessage();
            mensagem.To = iPara;
            mensagem.From = iFrom;
            mensagem.Message = iMessagem;
            mensagem.Schedule = DateTime.Now.ToString();
            List<String> retorno = cliente.send(mensagem);

            return RetornoServ = retorno[0].ToString();
        }
        public static string EnviaSMS_LOCASMS(DataSet ds, string iMessagem, string iNomeCampanha, string iUser, string iSenha)
        {
            string strRetorno = "";

            if (Utils.MessageBoxQuestion("O Sistema enviará SMS para " + ds.Tables[0].Rows.Count + " Clientes , deseja continuar?"))
            {
                System.String[] Telefone = new System.String[ds.Tables[0].Rows.Count];
                System.String[] Nome = new System.String[ds.Tables[0].Rows.Count];

                for (int i = 0; i < ds.Tables["Pessoa"].Rows.Count; i++)
                {
                    DataRow dRow = ds.Tables["Pessoa"].Rows[i];
                    string iNumero = dRow.ItemArray.GetValue(0).ToString();
                    NomeCliente = dRow.ItemArray.GetValue(1).ToString();

                    if (iNumero.Length == 8)
                    {
                        Telefone[i] = "279" + iNumero;

                    }
                    else
                    {
                        Telefone[i] = "27" + iNumero;
                    }
                    Nome[i] = NomeCliente;
                }

                Integração.EnviaSMS_LOCASMS EnviarSMS = new EnviaSMS_LOCASMS();

                string strQuantidadeEnvio, iText;

                iText = EnviarSMS.EnviaSMSLista(Telefone, iUser, iSenha, iMessagem, iNomeCampanha);

                string[] IRetorno = iText.Split(',');

                for (int i = 0; i < IRetorno.Length; i++)
                {
                    strRetorno = IRetorno[i].ToString();
                }
                //  MessageBox.Show("Resultado"IRetorno[0].ToStr);
            }

            return strRetorno;
        }

        public static bool EHCelular(string iNumero)
        {
            bool IRETORNO = false;

            if (iNumero.Substring(0, 1).Contains("8") || iNumero.Substring(0, 1).Contains("9"))
            {
                IRETORNO = true;
            }
            return IRETORNO;
        }
        public static int ClientesSemPedidos(string iDataInicial, string iDataFinal, string iMessagem, string iPorta)
        {
            conexao = new Conexao();
            DBExpertDataSet dbExpert = new DBExpertDataSet();
            DataInicial = Convert.ToDateTime(iDataInicial + "/" + DateTime.Now.Year + " 00:00:00");
            DataFinal = Convert.ToDateTime(iDataFinal + "/" + DateTime.Now.Year + " 23:59:59");

            DataSet ListaClientes = conexao.SelectObterClientesSemPedido("Pedido", "spObterClientesSemPedido", DataInicial, DataFinal);
            TotalSelecionado = ListaClientes.Tables["Pessoa"].Rows.Count;
            DialogResult resultado = MessageBox.Show("O Sistema enviará SMS para " + TotalSelecionado + " Clientes , deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                foreach (DataRow item in ListaClientes.Tables["Pedido"].Rows)
                {
                    string Telefone = item["Telefone"].ToString();
                    NomeCliente = item["Nome"].ToString();

                    if (Telefone.Length == 8)
                    {
                        EnvioSMSModen.EnviaSMS(9600, iPorta, "+55279" + Telefone, AdicionaNomeCliente(iMessagem));
                    }
                    else
                    {
                        EnvioSMSModen.EnviaSMS(9600, iPorta, "+5527" + Telefone, AdicionaNomeCliente(iMessagem));
                    }
                }
            }

            return TotalSelecionado;

        }
        private static string AdicionaNomeCliente(string iMensagem)
        {
            if (iMensagem.Contains("@Cliente"))
            {
                iMensagem = iMensagem.Replace("@Cliente", NomeCliente);
            }

            return iMensagem;
        }

        public static string EncryptMd5(string _login, string _senha)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(_login + "_" + _senha));
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }

            return strBuilder.ToString().ToUpper();
        }
        public static string CriptografarArquivo(string iArquivo, Boolean iUUPER = true)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(iArquivo));
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }
            if (iUUPER)
            {
                return strBuilder.ToString().ToUpper();
            }
            else
            {
                return strBuilder.ToString();
            }

        }
        public static string CriptoGrafarOnExecute(string iNomeMaquina, string iCNPJ)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(iCNPJ));
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }

            return strBuilder.ToString().ToUpper();
        }

        public static void Restart()
        {
            MessageBox.Show("A aplicação será reiniciada.");

            // Application.Exit();
            //Kill();
            Application.Restart();
        }
        public static void GeraHistorico(DateTime iData, int iCodPessoa, decimal iValor, int iCodUsuario, string iHistorico, char iTipo, string iFormaPagamento)
        {
            Conexao con = new Conexao();

            DataSet dsFormaPagamento = con.SelectObterFormaPagamentoPorNOme(iFormaPagamento, "FormaPagamento");
            if (dsFormaPagamento.Tables[0].Rows.Count > 0)
            {
                DataRow dRowFpagamento = dsFormaPagamento.Tables[0].Rows[0];

                if (Convert.ToBoolean(dRowFpagamento.ItemArray.GetValue(3).ToString()) == true)
                {
                    HistoricoPessoa hist = new Models.HistoricoPessoa()
                    {
                        CodPessoa = iCodPessoa,
                        CodUsuario = iCodUsuario,
                        Data = iData,
                        Historico = iHistorico,
                        Tipo = iTipo,
                        Valor = iValor
                    };
                    con.Insert("spAdicionaHistorico", hist);
                }
            }

        }

        public static DataSet PopularGrid(string table, DataGridView gridView, string iCamposConsulta)
        {
            Conexao con = new Conexao();
            DataSet Dados = null;
            Dados = con.SelectMontaGrid(table, iCamposConsulta);

            gridView.DataSource = null;
            gridView.AutoGenerateColumns = true;
            gridView.DataSource = Dados;
            gridView.DataMember = table;
            con.Close();

            return Dados;
        }
        public static DataSet PopulaGrid_Novo(string table, DataGridView gridView, string iParametrosConsulta, bool iAtivo = true, string iFiltrosAd = "", int iRowIndex = 0)
        {
            Conexao con = new Conexao();
            DataSet Dados = null;
            Dados = con.SelectMontaGrid(table, iParametrosConsulta, iAtivo, iFiltrosAd);

            gridView.DataSource = null;
            gridView.AutoGenerateColumns = true;
            // gridView.TabIndex = iRowIndex;
            gridView.DataSource = Dados;
            gridView.DataMember = table;

            con.Close();

            return Dados;
        }

        public static DataSet PopularGrid_SP(string table, DataGridView gridView, string spName, int CodRegistro = -1)
        {
            Conexao con = new Conexao();
            DataSet Dados = null;
            if (CodRegistro == -1)
            {
                Dados = con.SelectAll(table, spName);
            }

            else
            {
                Dados = con.SelectRegistroPorCodigo(table, spName, CodRegistro);
            }


            gridView.DataSource = null;
            gridView.AutoGenerateColumns = true;
            gridView.DataSource = Dados;
            gridView.DataMember = table;
            con.Close();

            return Dados;
        }


        public static DataSet PopularGrid(string table, DataGridView gridView)
        {
            Conexao con = new Conexao();
            DataSet Dados = con.SelectAll(table, "spObter" + table);

            gridView.DataSource = null;
            gridView.AutoGenerateColumns = true;
            gridView.DataSource = Dados;
            gridView.DataMember = table;
            con.Close();

            return Dados;
        }

        public static void Kill()
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("DexComanda");
            // Before starting the new process make sure no other MyProcessName is running.
            foreach (System.Diagnostics.Process p in process)
            {
                p.Kill();
            }


        }

        public static void ScripAtualizar(string caminho, string script, string ConectionString)
        {
            //Pega o caminho completo
            string fullPath = Directory.GetDirectoryRoot(caminho);

            //
            string arquivo = caminho + "\\" + script;

            FileStream fileToRead = File.Open(arquivo, FileMode.Open);
            string linhas = "";
            string line;
            StreamReader sr = new StreamReader(fileToRead);

            while ((line = sr.ReadLine()) != null)
            {
                linhas += line + "\r\n";
            }

            SqlConnection SqlConnection = new SqlConnection(ConectionString);
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.Connection = SqlConnection;
            try
            {
                SqlCommand.CommandText = linhas;

                SqlConnection.Open();
                SqlCommand.ExecuteNonQuery();
                MessageBox.Show("Banco de Dados Atualizado com sucesso");
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro ao executar a atualizacao favor entrar em contato com suporte  e informar a Mensagem a seguir:" +
                e.Message);
                throw;
            }

        }
        public static Boolean ValidaData(DateTime DataInicio, DateTime dataFim)
        {
            DataInicio = DataInicio.AddMonths(1);
            if (DataInicio >= dataFim)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static void CriarUsuario(string iConexao, string iNomeUser, string iSenha)
        {
            try
            {
                SqlConnection SqlConnection = new SqlConnection(iConexao);
                SqlCommand SqlCommand = new SqlCommand();
                SqlCommand.Connection = SqlConnection;

                SqlCommand.CommandText = "create login dex with password='1234'; CREATE USER [digital] FOR LOGIN dex WITH DEFAULT_SCHEMA=[dbo]";

                SqlConnection.Open();
                SqlCommand.ExecuteNonQuery();
                MessageBox.Show("Usuario Criado");
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e.Message);

            }

        }
        public static string EnderecoMAC()
        {
            return (from nic in NetworkInterface.GetAllNetworkInterfaces()
                    where nic.OperationalStatus == OperationalStatus.Up
                    select nic.GetPhysicalAddress().ToString()
                         ).FirstOrDefault();

        }

        //public static DateTime CriaRegistroInstalacao(DateTime iDataInstalacao)
        //{
        //    DateTime iDataValidade;
        //    RegistryKey RegistroKey;
        //    try
        //    {
        //        if (RegistroKey.OpenSubKey("Software\\DexSistemas\\Validade")!="")
        //        {

        //        } 
        //        RegistroKey = Registry.LocalMachine.OpenSubKey("Software", true);
        //        RegistroKey = RegistroKey.CreateSubKey("DexSistemas");
        //        RegistroKey.SetValue("Validade", CriptografarArquivo(iDataInstalacao.ToString()));

        //    }
        //    finally 
        //    {


        //    }

        //    return iDataValidade;
        //}
        public string Registro()
        {
            string lStrResultado = "";
            Microsoft.Win32.RegistryKey regKey;
            regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\DexSistemas\\", true);
            lStrResultado = regKey.GetValue("Validade").ToString();
            return lStrResultado;
        }
        public static void ExcluiRegistro()
        {
            RegistryKey RegistroKey;
            try
            {
                // RegistroKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\DexSistemas\\", true);
                //RegistroKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\DexSistemas\\", true); //<--over here!
                //reg.DeleteSubKey("{" + RegistroKey.GetValue().ToString() + "}");
                //if (RegistroKey.ValueCount>0)
                //{
                //    RegistroKey.DeleteSubKey("RegistroDex");
                //    RegistroKey.DeleteSubKey("Validade");
                //    RegistroKey.Close();
                //}

            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }
        }

        public static string GravaDataLiberacao(string iDataLiberacao)
        {
            string iRetorno;
            RegistryKey RegistroKey;
            try
            {
                RegistroKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\DexSistemas\\", true);


                if (RegistroKey.GetValue("Validade").ToString() != "")
                {
                    iRetorno = RegistroKey.GetValue("Validade").ToString();
                }
                else
                {
                    RegistroKey = Registry.LocalMachine.OpenSubKey("Software", true);
                    RegistroKey = RegistroKey.CreateSubKey("DexSistemas");
                    RegistroKey.SetValue("RegistroDex", CriptografarArquivo(iDataLiberacao));

                    RegistroKey.Close();
                    iRetorno = RegistroKey.GetValue("Validade").ToString();
                }
                return iRetorno;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static Boolean GravaRegistro(string iArquivo)
        {
            bool Retorno = false;
            try
            {
                // Cria Registro Inicial 

                string lArquivoRegistro = CriptografarArquivo(iArquivo);

                RegistryKey RegistroKey = Registry.LocalMachine.OpenSubKey("Software", true);
                RegistroKey = RegistroKey.CreateSubKey("DexSistemas");
                RegistroKey.SetValue("RegistroDex", lArquivoRegistro);
                RegistroKey.Close();

                Retorno = true;

            }
            catch (Exception deuruim)
            {

                MessageBox.Show("Não foi possivel verificar a existencia do Registro entre em contato com suporte " + deuruim.Message);
            }

            return Retorno;
        }

        public static string RetornaNomePc()
        {
            string PcName = System.Net.Dns.GetHostName();
            return PcName;
        }
        public static bool LeArquivoRegistro()
        {
            string iRetorno, iRegistroCritografado, strDataLimiteRegistro, strDataLimiteAtual;
            bool OK = false;
            try
            {
                //  Utils.RetornaNomePc()+ empresas.CNPJ + empresas.Cidade + empresas.Nome
                RegistryKey RegistroKey = Registry.LocalMachine.OpenSubKey("Software", true);
                iRetorno = RegistroKey.OpenSubKey("DexSistemas", true).GetValue("RegistroDex", true).ToString().ToString();
                strDataLimiteRegistro = RegistroKey.OpenSubKey("DexSistemas", true).GetValue("Expiracao", true).ToString().ToString();
                iRegistroCritografado = CriptografarArquivo(RetornaNomePc() + Sessions.returnEmpresa.CNPJ + Sessions.returnEmpresa.Cidade + Sessions.returnEmpresa.Nome);

                //CriptoGrafarOnExecute(RetornaNomePc(), Sessions.returnEmpresa.CNPJ + Sessions.returnEmpresa.Cidade + Sessions.returnEmpresa.Nome);

                if (iRegistroCritografado == iRetorno)
                {
                    OK = true;
                }

            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro de falta de memória , tente executar como administrador :" + erro.Message, "Dex Erro");
            }

            return OK;
        }

        public static int ContaRegistro(string iRegistroMD5)
        {
            int iRetorno = 0;
            Conexao con;
            int iContRegistro;
            DataRow dRow;
            con = new Conexao();

            dRow = con.SelectView("vwObterXSistemas", "XSistemas").Tables["XSistemas"].Rows[0];
            iRetorno = int.Parse(dRow.ItemArray.GetValue(0).ToString());

            iContRegistro = con.SelectRegistroPorData("XSistemas", "spObterDados", DateTime.Now.Date).Tables[0].Rows.Count;

            if ((iRetorno < 15) && (iContRegistro == 0))
            {
                AtualizaData data = new Models.AtualizaData()
                {
                    Data = DateTime.Now.Date,
                    RegistroMD5 = CriptografarArquivo(iRegistroMD5 + DateTime.Now.ToString())
                };
                con.Insert("spInsereRegistro", data);
            }

            return iRetorno;

        }

        public static string GeraRetornaContraSenha(string iSenha)
        {
            string iContraSenha = "";
            string iPrimeiraPosicao, iSegundaPosicao, iUltimaPosicao = null;

            try
            {
                iPrimeiraPosicao = iSenha.Substring(1, 1) + 1;
                iSegundaPosicao = iSenha.Substring(2, 1) + 2;
                iUltimaPosicao = iSenha.Substring(13, 1) + 13;

                // iContraSenha = CriptografarArquivo(iSenha + iPrimeiraPosicao + iSegundaPosicao + iUltimaPosicao);
                iContraSenha = CriptografarArquivo(iSenha);
            }
            catch (Exception er)
            {

                throw;
            }
            return iContraSenha;
        }

        public static string ServicoSQLATIVO(string iNomePC)
        {
            string status = "";
            // string command = "SELECT * FROM sys.databases WHERE name = 'DbExpert'";
            try
            {
                iNomePC = iNomePC.Replace("Data Source=", "");
                //Process[] remoteByName = Process.GetProcessesByName("MySQL", "NomeMaquina");
                // MSSQLSERVER
                //// 2. Using an IP address to specify the machineName parameter.
                //Process[] ipByName = Process.GetProcessesByName("notepad", "192.168.xx.x");
                ServiceController MeuServico = new ServiceController("MSSQLSERVER", iNomePC);

                status = MeuServico.Status.ToString();

                if (status.Equals("Stopped") || (status.Equals("Paused")))
                {
                    try
                    {
                        MeuServico.Start();
                        MeuServico.WaitForStatus(ServiceControllerStatus.Running);
                    }
                    catch (Exception ErroServico)
                    {

                        MessageBox.Show("Não foi possivel iniciar o serviço do SQLSERVER pois:" + ErroServico.Message);
                    }

                }
            }
            catch (Exception Erro1)
            {

                MessageBox.Show(Erro1.Message);
            }

            return status;
        }

        public static void CriaUsuario(string iConexao)
        {
            string iScript = "create login dex with password='1234' CREATE USER [digital] FOR LOGIN dex WITH DEFAULT_SCHEMA=[dbo]";
            SqlConnection sqlConection = new SqlConnection(iConexao);
            SqlCommand sqlCommand = new SqlCommand(iScript);
            sqlConection.Open();
            sqlCommand.ExecuteNonQuery();
        }
        public static Boolean CriaLicencaFree(string iCnpj, string iNome, string iEmail, string iTelefone)
        {
            DateTime DataLib = DateTime.Now;
            string iDataLiberacao = string.Format(DataLib.ToString("yyyyMMddHHmmss"));

            DateTime DataExp = DataLib.AddMonths(1);
            string iDataExpiracao = string.Format(DataExp.ToString("yyyyMMddHHmmss"));

            string NomePC = RetornaNomePc();
            string MacPc = EnderecoMAC();
            Boolean iCriouLicenca = false;
            bool iAtivo = true;
            try
            {
                MysqlConnection = new MySqlConnection("Server=mysql.expertsistemas.com.br;Port=3306;Database=exper194_lazaro;Uid=exper194_lazaro;Pwd=@@3412064;");
                MysqlConnection.Open();
                if (MysqlConnection.State == ConnectionState.Open)
                {
                    // MessageBox.Show("Abriu Conexão");
                    MysqlCommand = new MySqlCommand("insert into  Licenca (CNPJ,DataLiberacao,DataExpiracao,AtivoSn,Nome,Telefone,Email,NomePC,MACPC) values " +
                                                    "('" + iCnpj + "','" + iDataLiberacao + "','" + iDataExpiracao + "'," + iAtivo + ",'" + iNome + "','" +
                                                    iTelefone + "','" + iEmail + "','" + NomePC + "','" + MacPc + "')", MysqlConnection);
                    MysqlDataAdapter = new MySqlDataAdapter(MysqlCommand);
                    MysqlDataAdapter.InsertCommand = MysqlCommand;
                    if (MysqlCommand.ExecuteNonQuery() == 1)
                    {
                        iCriouLicenca = true;
                    }
                }
                else
                {
                    MessageBox.Show("Erro DexCommanda" + "Não foi possivel conectar ao servidor para geração de Licença Free,#13" +
                                    "verifique sua coneão com a internet /Firewall/Anti-Virus - e tente novamente");
                }
                MysqlConnection.Close();
            }
            catch (Exception e)
            {

                MessageBox.Show("Conexão com Servidor de Licença" + e.Message, "DexAViso");
            }
            return iCriouLicenca;
        }
        public static Boolean JaUsouFree(string iCnpj, string iMACPC, string iNomePC)
        {
            Boolean lEncontrou = false;
            MySqlConnection iConexao;
            mRetornoWS = new DataSet();
            string lConsulta = "select * from Licenca where CNPJ='" + iCnpj + "' and MACPC='" + iMACPC + "' and NomePC='" + iNomePC + "'";
            try
            {
                iConexao = new MySqlConnection(LinkServidor);
                iConexao.Open();
                if (iConexao.State == ConnectionState.Open)
                {
                    MysqlCommand = new MySqlCommand(lConsulta, iConexao);
                    MysqlDataAdapter = new MySqlDataAdapter(MysqlCommand);
                    MysqlCommand.ExecuteNonQuery();
                    MysqlDataAdapter.Fill(mRetornoWS, "Licenca");
                    if (mRetornoWS.Tables["Licenca"].Rows.Count > 0)
                    {
                        lEncontrou = true;
                    }

                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
            return lEncontrou;
        }

        public static DataSet DadosLicenca(string iCnpj, string iMACPC, string iNomePC)
        {
            MySqlConnection iConexao;
            mRetornoWS = new DataSet();
            string lConsulta = "select * from Licenca where CNPJ='" + iCnpj + "' and MACPC='" + iMACPC + "' and NomePC='" + iNomePC + "'";
            try
            {
                iConexao = new MySqlConnection(LinkServidor);
                iConexao.Open();
                if (iConexao.State == ConnectionState.Open)
                {
                    MysqlCommand = new MySqlCommand(lConsulta, iConexao);
                    MysqlDataAdapter = new MySqlDataAdapter(MysqlCommand);
                    MysqlCommand.ExecuteNonQuery();
                    MysqlDataAdapter.Fill(mRetornoWS, "Licenca");

                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

            return mRetornoWS;

        }
        public static decimal RetornaTaxaPorCliente(int iCodPessoa, Conexao con)
        {
            decimal iRetorno = 0.00M;
            var Regiao = con.SelectRegistroPorCodigo("RegiaoEntrega", "spObterTaxaPorCliente", iCodPessoa).Tables["RegiaoEntrega"];
            if (Regiao.Rows.Count > 0)
            {
                iRetorno = decimal.Parse(Regiao.Rows[0]["TaxaServico"].ToString());
            }


            return iRetorno;
        }

        public static void ControlaEventos(string iTipoEvento, string LocalEvento)
        {

            //if (Sessions.retunrUsuario != null)
            //{
            EventosSistema eventos = new EventosSistema()
            {
                CodUsuario = Sessions.retunrUsuario.Codigo,
                TipoEvento = iTipoEvento,
                DataEvento = DateTime.Now,
                LocalEvento = LocalEvento,

            };
            conexao.Insert("spAdicionarEvento", eventos);
            //}

        }
        public static void SalvarConfiguracao(string iChave, string iValue)
        {
            try
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Add an Application Setting.
                config.AppSettings.Settings.Remove(iChave);
                config.AppSettings.Settings.Add(iChave, iValue);

                // Save the configuration file.
                //config.Save(ConfigurationSaveMode.Full);
                string meuProcesso = Process.GetCurrentProcess().ProcessName;
                config.Save(ConfigurationSaveMode.Full);

                // Force a reload of a changed section.
                //    ConfigurationManager.RefreshSection("appSettings");

            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possível carregar o arquivo de configuração " + erro.Message);
            }



        }


        // Rotina para efetuar Backup Automátizado do Banco de dados
        //public static void BackupBanco(string iNomeServidor, string iNomeBanco, string iLocalBackup)
        //{
        //    var PatchArquivo = Path.Combine("@E:\\Dados\\BKp.bkp");
        //    try
        //    {

        //        var sc = new ServerConnection(iNomeServidor, "sa", "1001");
        //        var server = new Server(sc);

        //        if (server.Databases[iNomeBanco] != null)
        //        {
        //            //Criando o diretorio do Backup
        //            //if (!Directory.Exists("@"+iLocalBackup))
        //            //{
        //            //    Directory.CreateDirectory("@"+iLocalBackup);
        //            //}

        //            // Criando o objeto Backup
        //            var bak = new Backup();
        //            bak.Incremental = false;

        //            bak.Action = BackupActionType.Database;
        //            //string data = DateTime.Now.Date.ToString("MM-dd-yy");
        //            bak.BackupSetName = iNomeBanco + "_Backup"+DateTime.Now.ToShortDateString().Replace("/","")+".bkp";

        //            // Definindo o banco de dados a ser salvo
        //            bak.Database = iNomeBanco;

        //            bak.Checksum = true;

        //            // Adcionando um destino para o backup
        //            BackupDeviceItem destino = new BackupDeviceItem(bak.BackupSetName, DeviceType.File);

        //            bak.Devices.Add(destino);
        //            // Executando o backup
        //            bak.SqlBackup(server);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);

        //    }
        //}


    }

}
