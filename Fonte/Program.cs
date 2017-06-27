using DexComanda.Models;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using DexComanda.Operações;

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
                MessageBox.Show("Preencha os dados para conexão ao banco de dados", "[xSistemas] Aviso");
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
                    string strNomePC = Utils.RetornaNomePc();
                    // Verifica se o Serviço do SQLSERVER está Ativo para inicia-lo
                    Utils.ServicoSQLATIVO();
                    con = new Conexao();

                    // Le o arquivo de configuração para montar a grid
                    Sessions.SqlProduto = ConfigurationManager.AppSettings["GridProduto"];
                    Sessions.SqlPedido = ConfigurationManager.AppSettings["GridPedido"];
                    Sessions.SqlPessoa = ConfigurationManager.AppSettings["GridPessoa"];
                    // Fim da montagem
                    DataSet servidor = con.SelectAll("Empresa", "spObterEmpresa");
                    if (servidor.Tables["Empresa"].Rows.Count > 0)
                    {
                        Empresa empresas = new Empresa()
                        {
                            Nome = servidor.Tables[0].Rows[0].Field<string>("Nome"),
                            CNPJ = servidor.Tables[0].Rows[0].Field<string>("CNPJ"),
                            Telefone = servidor.Tables[0].Rows[0].Field<string>("Telefone"),
                            Telefone2 = servidor.Tables[0].Rows[0].Field<string>("Telefone2"),
                            Endereco = servidor.Tables[0].Rows[0].Field<string>("Endereco"),
                            Cep = int.Parse(servidor.Tables[0].Rows[0].Field<string>("Cep")),
                            Cidade = servidor.Tables[0].Rows[0].Field<string>("Cidade"),
                            Numero = int.Parse(servidor.Tables[0].Rows[0].Field<string>("Numero")),
                            Bairro = servidor.Tables[0].Rows[0].Field<string>("Bairro"),
                            UF = servidor.Tables[0].Rows[0].Field<string>("UF"),
                            PontoReferencia = servidor.Tables[0].Rows[0].Field<string>("PontoReferencia"),
                            Banco = servidor.Tables[0].Rows[0].Field<string>("Banco"),
                            Contato = servidor.Tables[0].Rows[0].Field<string>("Contato"),
                            Servidor = servidor.Tables[0].Rows[0].Field<string>("Servidor"),
                            CaminhoBackup = servidor.Tables[0].Rows[0].Field<string>("CaminhoBackup"),
                            DataInicio = servidor.Tables[0].Rows[0].Field<DateTime>("DataInicio"),
                            UrlServidor = servidor.Tables[0].Rows[0].Field<string>("UrlServidor"),
                            ConfiguracaoSMS = servidor.Tables[0].Rows[0].Field<string>("ConfiguracaoSMS"),
                        };
                        Sessions.returnEmpresa = empresas;


                        if (servidor.Tables[0].Rows.Count > 0)
                        {
                            //con = new Conexao();
                            // Preenchendo campo caso retorne Null
                            bool i = false;      //
                            int Inteiro = 0;     //
                            string String = "0"; //
                            // Fim do Preenchimento dos campos

                            DataSet dsconfiguracao = con.SelectAll("Configuracao", "spObterConfiguracao");
                            if (dsconfiguracao.Tables["Configuracao"].Rows.Count > 0)
                            {
                                Configuracao configs = new Configuracao()
                                {
                                    cod = dsconfiguracao.Tables[0].Rows[0].Field<int>("Cod"),
                                    ImpViaCozinha = dsconfiguracao.Tables[0].Rows[0].Field<string>("ImpViaCozinha"),
                                    UsaDataNascimento = dsconfiguracao.Tables[0].Rows[0].Field<Boolean>("UsaDataNascimento"),
                                    UsaLoginSenha = dsconfiguracao.Tables[0].Rows[0].Field<Boolean>("UsaLoginSenha"),
                                    ControlaEntregador = dsconfiguracao.Tables[0].Rows[0].Field<Boolean>("ControlaEntregador"),
                                    ProdutoPorCodigo = dsconfiguracao.Tables[0].Rows[0].Field<string>("ProdutoPorCodigo"),
                                    Usa2Telefones = dsconfiguracao.Tables[0].Rows[0].Field<Boolean>("Usa2Telefones"),
                                    UsaControleMesa = dsconfiguracao.Tables[0].Rows[0].Field<Boolean>("UsaControleMesa"),
                                    ImprimeViaEntrega = dsconfiguracao.Tables[0].Rows[0].Field<string>("ImprimeViaEntrega"),
                                    ControlaFidelidade = dsconfiguracao.Tables[0].Rows[0].Field<string>("ControlaFidelidade"),
                                    DescontoDiaSemana = dsconfiguracao.Tables[0].Rows[0].Field<Boolean>("DescontoDiaSemana"),
                                    CobraTaxaGarcon = dsconfiguracao.Tables[0].Rows[0].Field<Boolean>("CobraTaxaGarcon"),
                                    EnviaSMS = dsconfiguracao.Tables[0].Rows[0].Field<Boolean>("EnviaSMS"),
                                    RepeteUltimoPedido = dsconfiguracao.Tables[0].Rows[0].Field<Boolean>("RepeteUltimoPedido"),
                                    RegistraCancelamentos = dsconfiguracao.Tables[0].Rows[0].Field<Boolean>("RegistraCancelamentos"),
                                    DadosApp = dsconfiguracao.Tables[0].Rows[0].Field<string>("DadosApp"),
                                    CidadesAtendidas = dsconfiguracao.Tables[0].Rows[0].Field<string>("CidadesAtendidas"),
                                    CobrancaProporcionalSN = dsconfiguracao.Tables[0].Rows[0].Field<Boolean>("CobrancaProporcionalSN"),
                                    QtdCaracteresImp = dsconfiguracao.Tables[0].Rows[0].Field<int>("QtdCaracteresImp"),
                                    DadosPush = dsconfiguracao.Tables[0].Rows[0].Field<string>("DadosPush"),
                                    Impressoras =  dsconfiguracao.Tables[0].Rows[0].Field<string>("Impressoras"),
                                    ExigeVendedorSN = dsconfiguracao.Tables[0].Rows[0].Field<Boolean>("ExigeVendedorSN"),
                                    ImprimeViaBalcao = dsconfiguracao.Tables[0].Rows[0].Field<string>("ImprimeViaBalcao")
                                };
                                Sessions.returnConfig = configs;
                            }
                            else
                            {
                                MessageBox.Show("Preencha as configurações que deseja utilizar", "[xSistemas] Aviso");
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
                                    if (con.Liberacao(Sessions.returnEmpresa.Nome, Cnpj, NomePC, MAcPC) != null)
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
                                                             "(27) 9 81667827 / lazaro.shev@gmail.com ", "[xSistemas] Licença Expirada");
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
                                    else if (empresas.CNPJ == "14904501000107" || empresas.CNPJ == "11301588709" || empresas.CNPJ == "11291880000119")
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
                                else if (empresas.Servidor == "DESKTOP-TGEH425" || empresas.Servidor == "DESKTOP-5K2U4E8" || empresas.CNPJ == "14904501000107" || empresas.CNPJ == "11301588709" || empresas.CNPJ == "10512501000100" || empresas.CNPJ == "11291880000119")
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
                        MessageBox.Show("[xSistemas] aviso", "Preencha os dados de sua empresas para validação da licença");
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

