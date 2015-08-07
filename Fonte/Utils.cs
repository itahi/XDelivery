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
        private const string LinkServidor = "Server=mysql.expertsistemas.com.br;Port=3306;Database=exper194_lazaro;Uid=exper194_lazaro;Pwd=@@3412064;";
        public static Boolean EfetuarLogin(string nomeUsuario, string senha , bool iAbreFrmPrincipal=true , int iNumCaixa=1)
        {

            if (nomeUsuario.Equals(""))
            {
                MessageBox.Show("Informe seu usuário.");
            }
            else if (senha.Equals(""))
            {
                MessageBox.Show("Informe sua senha.");
            }
            else
            {
                string hashSenha = EncryptMd5(nomeUsuario, senha);
                conexao = new Conexao();
                NomeEmpresa = Sessions.returnEmpresa.Nome;
                DataSet usuarios = conexao.SelectAll("Usuario", "spObterUsuario");

                DataView dv = usuarios.Tables[0].DefaultView;
                //  Sessions.returnUsuario = dv; 
                string query = "Nome = '" + nomeUsuario + "' AND Senha = '" + hashSenha + "'";

                dv.RowFilter = query;

                if (dv.Count > 0)
                {
                    //_CodUserLogado = int.Parse(dv[0].Row["Codigo"].ToString());
                    string _nome = dv[0].Row["Nome"].ToString();
                    string _senha = dv[0].Row["Senha"].ToString();

                    if (_nome.Equals(nomeUsuario) && _senha.Equals(hashSenha))
                    {
                        Sessions.returnUsuario = new Usuario()
                        {
                            Nome = _nome,
                            Senha = _senha,
                            Codigo = Convert.ToInt16(dv[0].Row["Codigo"].ToString()),
                            AcessaRelatoriosSN = Convert.ToBoolean(dv[0].Row["AcessaRelatoriosSN"].ToString()),
                            AdministradorSN = Convert.ToBoolean(dv[0].Row["AdministradorSN"].ToString()),
                            FinalizaPedidoSN = Convert.ToBoolean(dv[0].Row["FinalizaPedidoSN"].ToString()),
                            CancelaPedidosSN = Convert.ToBoolean(dv[0].Row["CancelaPedidosSN"].ToString()),
                            AlteraProdutosSN = Convert.ToBoolean(dv[0].Row["AlteraProdutosSN"].ToString()),
                            DescontoPedidoSN  = Convert.ToBoolean(dv[0].Row["DescontoPedidoSN"].ToString()),
                            DescontoMax = Convert.ToDouble(dv[0].Row["DescontoMax"].ToString()),
                            CaixaLogado = iNumCaixa
                        };

                        Sessions.retunrUsuario = Sessions.returnUsuario;
                        Logado = true;

                        if (iAbreFrmPrincipal)
                        {
                            Main principal = new Main();
                            
                            principal.ShowDialog();
                        }
                        

                    }
                    else
                    {
                        MessageBox.Show("Usuário ou Senha incorretos.","[XSistemas] Aviso");
                        Logado = false;
                    }
                }
                else if (nomeUsuario.Equals("admin"))
                {
                    frmConfiguracoes frmConfiguracoes = new frmConfiguracoes();
                    frmConfiguracoes.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Usuário ou senha incorretos","[XSistemas] Aviso");
                    Logado = false;
                }

            }
            return Logado;
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

        public static bool CaixaAberto(DateTime iDataRegistro , int iNumero)
        {
            bool iRetorno = false;
            DataRow dRow;
            DataSet dsCaixa;
            conexao = new Conexao();
            try
            {
                if (Sessions.returnUsuario != null)
                {
                    dsCaixa = conexao.SelectRegistroPorDataCodigo("Caixa", "spObterDadosCaixa", iDataRegistro, iNumero);
                    dRow = dsCaixa.Tables[0].Rows[0];
                    iRetorno = dRow.ItemArray.GetValue(7).ToString() == Convert.ToString(false);

                }
                else
                {
                    iRetorno = true;
                }
            }
            catch (Exception erro)
            {
                if (erro.Message.ToString() =="There is no row at position 0.")
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
        public static void RepetirUltimoPedido(int iCodCliente, Main iMain)
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

                frmCadastrarPedido frmRepetePedido = new frmCadastrarPedido(true,"0,00", "", "", TaxaEntrega, false, DateTime.Now, CodPedido, CodPessoa,
                                                                            "", FormaPagamento, "", "Balcao", iMain,0.00M);
                frmRepetePedido.ShowDialog();


            }


        }

        public static void AtualizaMesa(string iNumeroMesa, int iStatus)
        {
            conexao = new Conexao();
            Mesas mesas = new Mesas()
            {
               // Codigo = iCodigo,
                NumeroMesa = iNumeroMesa,
                StatusMesa = iStatus
            };
            conexao.Update("spAlteraStatusMesa", mesas);
        }

        public static int RetornaCodigoMesa(string iNumMesa)
        {
            DataSet iDados;
            int iRetorno = 0;
            conexao = new Conexao();
            iDados = conexao.SelectRegistroPorCodigo("Mesas", "spObterCodigoMesa", int.Parse(iNumMesa));
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
            string iRetorno="";
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
        public static void LimpaForm(System.Windows.Forms.Control parent)
        {
            foreach (System.Windows.Forms.Control ctrControl in parent.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.TextBox)))
                {
                    //Check to see if it's a textbox 
                    ((System.Windows.Forms.TextBox)ctrControl).Text = string.Empty;
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
                if (ctrControl.Controls.Count > 0)
                {
                    //Call itself to get all other controls in other containers 
                    //ClearForm(ctrControl);
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
                    System.IO.File.Delete(temp);
                    System.IO.File.Create(temp).Close();
                    System.IO.File.AppendAllText(temp, iText);
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
        public static int ClientesAniversariantes(string iDataInicial, string iDataFinal, string iMessagem, string iPorta)
        {
            conexao = new Conexao();
            DBExpertDataSet dbExpert = new DBExpertDataSet();
            DataInicial = Convert.ToDateTime(iDataInicial + "/" + DateTime.Now.Year + " 00:00:00");
            DataFinal = Convert.ToDateTime(iDataFinal + "/" + DateTime.Now.Year + " 23:59:59");
            DataSet ListaClientes = conexao.SelectObterAniversariantes("Pessoa", "spObterAnivesariantes", DataInicial, DataFinal);
            TotalSelecionado = ListaClientes.Tables["Pessoa"].Rows.Count;
            DialogResult resultado = MessageBox.Show("O Sistema enviará SMS para " + TotalSelecionado + " Clientes , deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                System.String[] Telefone = new System.String[TotalSelecionado];

                for (int i = 0; i < ListaClientes.Tables["Pessoa"].Rows.Count; i++)
                {
                    DataRow dRow = ListaClientes.Tables["Pessoa"].Rows[i];
                    string iNumero = dRow.ItemArray.GetValue(0).ToString();
                    NomeCliente = dRow.ItemArray.GetValue(1).ToString();

                    if (EHCelular(iNumero))
                    {
                        if (iNumero.Length == 8)
                        {
                            Telefone[i] = "279" + iNumero;
                        }
                        else
                        {
                            Telefone[i] = "27" + iNumero;
                        }

                    }

                }

                Integração.EnviaSMS_LOCASMS EnviarSMS = new EnviaSMS_LOCASMS();
                Array IRetorno = new Array[2];
                IRetorno = EnviarSMS.EnviaSMSLista(Telefone, "27981667827", "546936", iMessagem, "ClientesAniversariantes" + iDataInicial + iDataFinal);

                MessageBox.Show("Sms enviados e confirmados " + IRetorno, "Envio de SMS");
            }

            return TotalSelecionado;
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
        public static string CriptografarArquivo(string iArquivo)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(iArquivo));
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }

            return strBuilder.ToString().ToUpper();
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

        public static DataSet PopularGrid(string table, DataGridView gridView, string spName)
        {
            Conexao con = new Conexao();
            DataSet Dados = con.SelectAll(table, spName);

            gridView.DataSource = null;
            gridView.AutoGenerateColumns = true;
            gridView.DataSource = Dados;
            gridView.DataMember = table;
            con.Close();

            return Dados;
        }


        public static void PopularGrid(string table, DataGridView gridView)
        {
            Conexao con = new Conexao();
            gridView.DataSource = null;
            gridView.AutoGenerateColumns = true;
            gridView.DataSource = con.SelectAll(table, "spObter" + table);
            gridView.DataMember = table;
            con.Close();
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
                RegistroKey.SetValue("RegistroDex",lArquivoRegistro);
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
            string iRetorno, iRegistroCritografado, strDataLimiteRegistro,strDataLimiteAtual;
            bool OK = false;
            try
            {
                //  Utils.RetornaNomePc()+ empresas.CNPJ + empresas.Cidade + empresas.Nome
                RegistryKey RegistroKey = Registry.LocalMachine.OpenSubKey("Software", true);
                iRetorno    = RegistroKey.OpenSubKey("DexSistemas", true).GetValue("RegistroDex", true).ToString().ToString();
                strDataLimiteRegistro = RegistroKey.OpenSubKey("DexSistemas", true).GetValue("Expiracao", true).ToString().ToString();
                iRegistroCritografado = CriptografarArquivo(RetornaNomePc()+ Sessions.returnEmpresa.CNPJ + Sessions.returnEmpresa.Cidade + Sessions.returnEmpresa.Nome);
                    
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

           dRow =  con.SelectView("vwObterXSistemas", "XSistemas").Tables["XSistemas"].Rows[0];
           iRetorno = int.Parse(dRow.ItemArray.GetValue(0).ToString());

           iContRegistro = con.SelectRegistroPorData("XSistemas", "spObterDados",DateTime.Now.Date).Tables[0].Rows.Count;

           if ((iRetorno < 5) && (iContRegistro==0))
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

        public static string ServicoSQLATIVO()
        {
            string status = "";
            // string command = "SELECT * FROM sys.databases WHERE name = 'DbExpert'";
            try
            {

                ServiceController MeuServico = new ServiceController("MSSQLSERVER");

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
            if (Regiao.Rows.Count>0)
            {
                iRetorno = decimal.Parse(Regiao.Rows[0]["TaxaServico"].ToString()); 
            }
           

            return iRetorno;
        }

        public static void ControlaEventos(string iTipoEvento, string LocalEvento)
        {

            if (Sessions.retunrUsuario != null)
            {
                EventosSistema eventos = new EventosSistema()
                {
                    CodUsuario = Sessions.retunrUsuario.Codigo,
                    TipoEvento = iTipoEvento,
                    DataEvento = DateTime.Now,
                    LocalEvento = LocalEvento,

                };
                conexao.Insert("spAdicionarEvento", eventos);
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
