using DexComanda.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DexComanda;
using System.Configuration;

namespace DexComanda
{

    public static class Program
    {
        private static string CNPJ = null;
        private static DateTime DataInicio = DateTime.Now;
        public static DateTime DataExpiracao;// = DateTime.Now;
        public static DateTime DiaMesAtual = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        private static string Versao = null;
        private static Conexao con;
        private static DataRow dRow;
        private static DataRow config;

        //   private static Empresa empresa;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {

            var temp = Directory.GetCurrentDirectory() + @"\ConnectionString_DexComanda.txt";

            if (!File.Exists(temp))
            {
                MessageBox.Show("Preencha os dados para conexão ao banco de dados", "Aviso XSistemas");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmConfiguracoes());
            }
            else
            {
                try
                {
                    StreamReader tempDex = new StreamReader(temp);

                    string sLine = "";
                    ArrayList arrText = new ArrayList();

                    sLine = tempDex.ReadLine();


                    Conexao.connectionString = sLine;
                    string[] words = sLine.Split(';');
                    string[] iText = words[0].Split('\'');

                    //Verifica se o Serviço do SQLSERVER está Ativo para inicia-lo
                    if (Utils.RetornaNomePc() == iText[0])
                    {
                        Utils.ServicoSQLATIVO(iText[0]);
                    }
                    

                    con = new Conexao();

                    // Le o arquivo de configuração para montar a grid
                    Sessions.SqlProduto = ConfigurationManager.AppSettings["GridProduto"];
                    Sessions.SqlPedido = ConfigurationManager.AppSettings["GridPedido"];
                    Sessions.SqlPessoa = ConfigurationManager.AppSettings["GridPessoa"];
                    // Fim da montagem
                    DataSet servidor = con.SelectAll("Empresa", "spObterEmpresa");
                    if (servidor.Tables["Empresa"].Rows.Count > 0)
                    {
                        dRow = servidor.Tables["Empresa"].Rows[0];
                        Empresa empresas = new Empresa()
                        {
                            Nome = dRow.ItemArray.GetValue(1).ToString(),
                            CNPJ = dRow.ItemArray.GetValue(2).ToString(),
                            Telefone = dRow.ItemArray.GetValue(3).ToString(),
                            Telefone2 = dRow.ItemArray.GetValue(4).ToString(),
                            Endereco = dRow.ItemArray.GetValue(5).ToString(),
                            Cep = int.Parse(dRow.ItemArray.GetValue(6).ToString()),
                            Cidade = dRow.ItemArray.GetValue(7).ToString(),
                            Numero = int.Parse(dRow.ItemArray.GetValue(8).ToString()),
                            Bairro = dRow.ItemArray.GetValue(9).ToString(),
                            UF = dRow.ItemArray.GetValue(10).ToString(),
                            PontoReferencia = dRow.ItemArray.GetValue(11).ToString(),
                            Banco = dRow.ItemArray.GetValue(12).ToString(),
                            Contato = dRow.ItemArray.GetValue(13).ToString(),
                            Servidor = dRow.ItemArray.GetValue(15).ToString(),
                            CaminhoBackup = dRow.ItemArray.GetValue(16).ToString(),

                            //  VersaoBanco = dRow.ItemArray.GetValue(15).ToString(),
                            DataInicio = Convert.ToDateTime(dRow.ItemArray.GetValue(18).ToString()),
                            UrlServidor = dRow.ItemArray.GetValue(19).ToString()

                            //  Versao = dRow.ItemArray.GetValue(17).ToString()
                        };
                        Sessions.returnEmpresa = empresas;


                        if (servidor.Tables[0].Rows.Count > 0)
                        {
                            con = new Conexao();
                            // Preenchendo campo caso retorne Null
                            bool i = false;      //
                            int Inteiro = 0;     //
                            string String = "0"; //
                            // Fim do Preenchimento dos campos

                            var configuracao = con.SelectAll("Configuracao", "spObterConfiguracao");
                            if (configuracao.Tables["Configuracao"].Rows.Count > 0)
                            {
                                config = configuracao.Tables["Configuracao"].Rows[0];


                                Configuracao configs = new Configuracao()
                                 {
                                     cod = int.Parse(config.ItemArray.GetValue(0).ToString()),
                                     ImpViaCozinha = bool.Parse(config.ItemArray.GetValue(1).ToString()),
                                     UsaDataNascimento = bool.Parse(config.ItemArray.GetValue(2).ToString()),
                                     UsaLoginSenha = bool.Parse(config.ItemArray.GetValue(3).ToString()),
                                     ControlaEntregador = bool.Parse(config.ItemArray.GetValue(5).ToString()),
                                     ProdutoPorCodigo = bool.Parse(config.ItemArray.GetValue(6).ToString()),
                                     Usa2Telefones = bool.Parse(config.ItemArray.GetValue(7).ToString()),
                                     UsaControleMesa = bool.Parse(config.ItemArray.GetValue(9).ToString()),
                                     ImprimeViaEntrega = bool.Parse(config.ItemArray.GetValue(10).ToString()),
                                     ControlaFidelidade = bool.Parse(config.ItemArray.GetValue(11).ToString()),
                                     PedidosParaFidelidade = int.Parse(config.ItemArray.GetValue(12).ToString()),
                                     DescontoDiaSemana = bool.Parse(config.ItemArray.GetValue(13).ToString()),
                                     PrevisaoEntregaSN = bool.Parse(config.ItemArray.GetValue(14).ToString()),
                                     CobraTaxaGarcon = bool.Parse(config.ItemArray.GetValue(19).ToString()),
                                     ImpLPT = bool.Parse(config.ItemArray.GetValue(21).ToString()),
                                     EnviaSMS = bool.Parse(config.ItemArray.GetValue(23).ToString()),
                                     RepeteUltimoPedido = bool.Parse(config.ItemArray.GetValue(27).ToString()),
                                     RegistraCancelamentos = bool.Parse(config.ItemArray.GetValue(28).ToString()),
                                     DadosApp = config.ItemArray.GetValue(29).ToString(),
                                     Pushauthorization = config.ItemArray.GetValue(30).ToString(),
                                     Pushapp_id = config.ItemArray.GetValue(31).ToString(),
                                     CidadesAtendidas = config.ItemArray.GetValue(32).ToString(),
                                     GCM = config.ItemArray.GetValue(34).ToString(),
                                     ImpressoraCozinha = config.ItemArray.GetValue(35).ToString(),
                                     ImpressoraEntrega = config.ItemArray.GetValue(36).ToString(),
                                     ImpressoraCopaBalcao = config.ItemArray.GetValue(37).ToString(),
                                     CobrancaProporcionalSN = bool.Parse(config.ItemArray.GetValue(38).ToString()),

                                };
                                if (config.ItemArray.GetValue(4).ToString() != "")
                                {
                                    configs.QtdCaracteresImp = int.Parse(config.ItemArray.GetValue(4).ToString());
                                }
                                if (config.ItemArray.GetValue(15).ToString() != "")
                                {
                                    configs.PrevisaoEntrega = config.ItemArray.GetValue(15).ToString();
                                }
                                if (config.ItemArray.GetValue(20).ToString() != "")
                                {
                                    configs.TamanhoFont = config.ItemArray.GetValue(20).ToString();
                                }
                                if (config.ItemArray.GetValue(22).ToString() != "")
                                {
                                    configs.PortaLPT = config.ItemArray.GetValue(22).ToString();
                                }
                                if (config.ItemArray.GetValue(24).ToString() != "")
                                {
                                    configs.ViasEntrega = config.ItemArray.GetValue(24).ToString();
                                }
                                if (config.ItemArray.GetValue(25).ToString() != "")
                                {
                                    configs.ViasCozinha = config.ItemArray.GetValue(25).ToString();
                                }
                                if (config.ItemArray.GetValue(26).ToString() != "")
                                {
                                    configs.ViasBalcao = config.ItemArray.GetValue(26).ToString();
                                }

                                configs.ExigeVendedorSN = bool.Parse(config.ItemArray.GetValue(33).ToString());
                                Sessions.returnConfig = configs;
                            }
                            else
                            {
                                MessageBox.Show("Preencha as configurações que deseja utilizar, nao operação do Sistema", "XCommanda Aviso");
                                frmConfiguracoes frm = new frmConfiguracoes();
                                frm.ShowDialog();
                            }
                            string Cnpj = empresas.CNPJ;
                            string MAcPC = Utils.EnderecoMAC();
                            string NomePC = Utils.RetornaNomePc();


                            //Caso tenha internet ele valida a Licença
                            if (con.IsConnected() == true)
                            {
                                if (servidor.Tables["Empresa"].Rows.Count > 0)
                                {
                                    // DataSet Licenca = con.Liberacao(Sessions.returnEmpresa.CNPJ);
                                    if (con.Liberacao(Sessions.returnEmpresa.Nome, Sessions.returnEmpresa.CNPJ, NomePC, MAcPC) != null)
                                    {
                                        // Cria Registro Para Usar Off
                                        Utils.GravaRegistro(Utils.RetornaNomePc() + empresas.CNPJ + empresas.Cidade + empresas.Nome);
                                        // Inicia Sistema

                                        // Verifica se Abriu o Sistema 15 dias sem internet e limpa dando mais 5 dias  
                                        int intAbriu15Vezes = Utils.ContaRegistro(empresas.Nome + empresas.CNPJ);
                                        if (intAbriu15Vezes >= 15)
                                        {
                                            con.LimpaTabela("XSistemas", "spCalculaSistema");
                                            return;
                                        }
                                        else
                                        {
                                            Utils.IniciaSistema();
                                        }
                                        

                                    }
                                    else
                                    {
                                        if (Utils.JaUsouFree(Cnpj, MAcPC, NomePC))
                                        {
                                            MessageBox.Show("Licença não está ativa , favor entrar em contato com suporte" +
                                                              "(27) 9 8166-7827 / lazaro.shev@gmail.com ", "DEX Licença Expirada");

                                            Utils.ExcluiRegistro();
                                            Application.Exit();
                                        }
                                        else if (Utils.CriaLicencaFree(empresas.CNPJ, empresas.Nome, empresas.Contato, empresas.Telefone))
                                        {
                                            MessageBox.Show("Foi gerado uma Licença temporária que terá duração de 30 dias ," +
                                                            " aproveite para conhecer nosso sistema ! Caso deseja ativar vá até a ABA Contato " +
                                                            " e veja o procedimento , clique em OK para continuar", "Liberação Grátis");

                                            Utils.IniciaSistema();
                                        }
                                        else
                                        {
                                            Utils.ExcluiRegistro();
                                            MessageBox.Show("Não foi possivel validar a licença , favor entrar em contato com suporte" +
                                                             "(27) 9 81667827 / lazaro.shev@gmail.com ", "DEX Licença Expirada");
                                        }

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Preencha os dados de sua empresas para validação da licença", "DEX Aviso");
                                    frmConfiguracoes frm = new frmConfiguracoes();
                                    frm.ShowDialog();
                                }
                            }
                            else
                            {

                                MessageBox.Show("Não foi encontrado uma conexão com a internet ", "xSistemas - Atenção");
                               // Utils.IniciaSistema();
                                if (Utils.LeArquivoRegistro())
                                {
                                    int intAbriu15Vezes = Utils.ContaRegistro(empresas.Nome + empresas.CNPJ);
                                    if (intAbriu15Vezes < 15)
                                    {
                                        Utils.IniciaSistema();
                                    }
                                    else if (empresas.CNPJ == "14904501000107" || empresas.CNPJ == "11301588709" || empresas.CNPJ== "11291880000119")
                                    {
                                        Utils.IniciaSistema();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Voce precisa se conectar a Internet para obter uma nova Licença", "Aviso Importante");
                                        Application.Exit();
                                    }

                                }
                                // CNPJ OSVALDO
                                else if (empresas.Servidor=="DESKTOP-TGEH425"|| empresas.Servidor== "DESKTOP-5K2U4E8" || empresas.CNPJ == "14904501000107" || empresas.CNPJ == "11301588709" || empresas.CNPJ == "10512501000100" ||empresas.CNPJ== "11291880000119")
                                {
                                    Utils.IniciaSistema();
                                }
                                else
                                {
                                   // Utils.IniciaSistema();
                                    MessageBox.Show("Licença Expirada, conecte-se a internet para uma renovação");
                                    frmLicencaOFFLINE frm = new frmLicencaOFFLINE();
                                    frm.ShowDialog();
                                }

                            }

                        }

                    }
                    else
                    {
                        MessageBox.Show("Aviso Dex", "Preencha os dados de sua empresas para validação da licença");
                        frmConfiguracoes frm = new frmConfiguracoes();
                        frm.ShowDialog();
                    }


                }
                catch (Exception e)
                {

                    MessageBox.Show("Erro ao iniciar as configurações do sistema ," +
                    "há valores nulos que não podem ser carregados" + " " + e.Message);
                }



            }


        }

    }

}

