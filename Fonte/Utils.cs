﻿using DexComanda.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using HumanAPIClient.Service;
using HumanAPIClient.Model;
using Microsoft.Win32;
using System.ServiceProcess;
using DexComanda.Integração;
using MySql.Data.MySqlClient;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Collections;
using DexComanda.Relatorios.Delivery;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using DexComanda.Operações.Financeiro;
using DexComanda.Operações;
using Microsoft.VisualBasic;
using DexComanda.Models.WS;
using Newtonsoft.Json;
using DexComanda.Operações.Funções;
using DexComanda.Relatorios.Gerenciais.Cristal;
using DexComanda.Relatorios.Clientes.Crystal;
using System.Drawing.Printing;
using DexComanda.Models.Produto;
using DexComanda.Cadastros.Pessoa;
using DexComanda.Cadastros.Pedido;
using DexComanda.Relatorios.Caixa;
using DexComanda.Models.Configuracoes;
using System.Data.Sql;
using System.Text.RegularExpressions;

namespace DexComanda
{

    public class Utils
    {

        private static Conexao conexao;
        private static SimpleSending cliente;
        private static SimpleMessage mensagem;
        private static string RetornoServ;
        private static DateTime DataInicial;
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
        public static int intCodUserAutorizador;
        //  private static Boolean bCodigoPersonalizado =
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
                        MessageBox.Show("Usuário ou Senha incorretos.", "[xSistemas] Aviso");
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


            return Logado;
        }

        /// <summary>
        /// Função para validar se o tipo de pedido é "Mesa"
        /// </summary>
        /// <param name="iCodPedido">
        /// Código do pedido clicado na grid</param>
        /// <returns>
        /// Verdadeiro/Falso</returns>
        public static bool VerificaSeEmesa(int iCodPedido)
        {
            string iReturn = conexao.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", iCodPedido).Tables[0].Rows[0]
            .ItemArray.GetValue(9).ToString();

            if (iReturn != "0" && iReturn != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int VerificaPedidoOnline(int CodPedido)
        {
            int iCodWs = 0;
            DataSet dPedido = conexao.SelectRegistroPorCodigo("Pedido", "spObterPedidoOnline", CodPedido);
            if (dPedido.Tables[0].Rows.Count > 0)
            {
                iCodWs = dPedido.Tables[0].Rows[0].Field<int>("CodigoPedidoWS");
            }
            return iCodWs;
        }
        public static Boolean PermiteEntregador(int iCodPedido)
        {
            Boolean iRetur = false;
            try
            {
                DataRow dROw = conexao.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", iCodPedido).Tables[0].Rows[0];
                if (dROw.ItemArray.GetValue(8).ToString() == "0 - Entrega")
                {
                    iRetur = true;
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

            return iRetur;
        }
        public static List<string> RetornaDescontoFidelidade(int intCodPessoa)
        {
            List<string> idProdutos = new List<string>();
            try
            {
                string strSql = "select sum(Pontos) as Pontos from Pessoa_Fidelidade where CodPessoa=" + intCodPessoa;
                DataSet dsPontos = conexao.SelectAll("Pessoa_Fidelidade", "", strSql);
                if (dsPontos.Tables["Pessoa_Fidelidade"].Rows[0].IsNull("Pontos")
                    )
                {
                    idProdutos = new List<string>();
                    return idProdutos;
                }
                int pontosAcumulados = dsPontos.Tables[0].Rows[0].Field<int>("Pontos");
                string iSqlProduto = " select Codigo,NomeProduto,PontoFidelidadeTroca as 'Pontos' from Produto where PontoFidelidadeTroca>0 and PontoFidelidadeTroca <=" + pontosAcumulados;
                DataSet dsProdutosParaTroca = conexao.SelectAll("Produto", "", iSqlProduto);
                if (dsProdutosParaTroca.Tables[0].Rows.Count <= 0)
                {
                    idProdutos = new List<string>();
                    return idProdutos;
                }
                else
                if (!MessageBoxQuestion("Este cliente tem " + pontosAcumulados.ToString() + " acumulados deseja usar agora?"))
                {
                    idProdutos = new List<string>();
                    return idProdutos;
                }
                else
                {
                    frmProdutosTrocaFidelidade frm = new frmProdutosTrocaFidelidade(dsProdutosParaTroca, pontosAcumulados);
                    frm.ShowDialog();
                    idProdutos = frm.codProdutos;
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("RetornaDescontoFidelidade " + erro.Message);
                idProdutos = new List<string>();
            }

            return idProdutos;
        }
        public static void AtualizaPessoa(int iCodPessoa, string iNome, string iCEP, string iEndereco,
            string iNumero, string iBairro, string iCidade, string iUF, string iPRerefencia, string iObservacao,
            string iTelefone, string iTelefone2, DateTime iDtNas, DateTime iDtCad, int iTickFid, int iCodRegiao,
            string iUserID, string iDDD, string iSexo)
        {
            Pessoa pess = new Pessoa()
            {
                Codigo = iCodPessoa,
                Bairro = iBairro,
                Cep = iCEP,
                Cidade = iCidade,
                CodRegiao = iCodRegiao,
                DataCadastro = iDtCad,
                DataNascimento = iDtNas,
                DDD = iDDD,
                Endereco = iEndereco,
                Nome = iNome,
                Numero = iNumero,
                Observacao = iObservacao,
                PFPJ = 'F',
                PontoReferencia = iPRerefencia,
                Sexo = iSexo,
                Telefone = iTelefone,
                Telefone2 = iTelefone2,
                TicketFidelidade = iTickFid,
                UF = iUF,
                user_id = iUserID
            };
            conexao.Update("spAlterarPessoa", pess);
        }
        /// <summary>
        /// Função para buscar os endereços do cliente caso ele tenha mais de 1
        /// </summary>
        /// <param name="iDPesso">
        /// Insere o Código da Pessoa 
        /// </param>
        /// <returns>
        /// Retorna o IdDoEndereço</returns>
        public static int MaisEnderecos(int iDPesso)
        {
            int iReturn = 0;
            if (conexao.SelectRegistroPorCodigo("Pessoa_Endereco", "spObterEnderecoPessoa",
                        iDPesso).Tables[0].Rows.Count > 0)
            {
                frmSelecionaEndereco frm = new frmSelecionaEndereco(iDPesso);
                frm.ShowDialog();
                iReturn = frm.intCodEndereco;
            }
            else
            {
                MessageBox.Show("Cliente possuí apenas 1 endereço cadastrado, caso deseje adicionar novos endereços clique em 'Atualizar Cliente' , vá na aba Endereços e adicione novos");
            }
            return iReturn;
        }
        public static DataSet ItensSelect(int iCodPedido, string strNomeImpressora, string iTipoPedido)
        {
            string iSqlWhere, iTipoAgrupamento;
            string iSqlJoin = "left join Produto P on P.Codigo = It.CodProduto ";
            if (iTipoPedido == "0 - Entrega")
            {
                iTipoAgrupamento = Utils.RetornaConfiguracaoDelivery().TipoAgrupamento;
            }
            else if (iTipoPedido == "2 - Balcao")
            {
                iTipoAgrupamento = Utils.RetornaConfiguracaoBalcao().TipoAgrupamento;
            }
            else
            {
                iTipoAgrupamento = Utils.RetornaConfiguracaoMesa().TipoAgrupamento;
            }

            if (iTipoAgrupamento == "Por Impressora")
            {
                iSqlWhere = " where  PE.Codigo=" + iCodPedido + " and ImprimeCozinhaSN = 1 and G.NomeImpressora='" + strNomeImpressora + "' and ImpressoSN=0";
            }
            else
            {
                iSqlWhere = " where  PE.Codigo=" + iCodPedido + " and ImprimeCozinhaSN = 1 and ImpressoSN=0 ";
            }

            if (Utils.MarcaTipoConfiguracaoProduto().TipoCodigo == "Personalizado")
            {
                iSqlJoin = "left join Produto P on P.CodigoPersonalizado = It.CodProduto ";
            }
            string iSql = " select IT.CodProduto ,PE.*,  " +
                           " CodGrupo, " +
                           " NomeImpressora, " +
                           " IT.CodProduto " +
                           " from ItemsPedido IT " +
                           " left join Pedido PE ON PE.Codigo = IT.CodPedido " +
                           iSqlJoin +
                           " LEFT JOIN GRUPO G ON G.Codigo = P.CodGrupo ";

            iSql = iSql + iSqlWhere;

            return conexao.SelectAll("ItemsPedido", "", iSql);
        }
        public static DataSet CarregaItens(int intCodPedido)
        {
            DataSet ds;
            ds = conexao.SelectRegistroPorCodigo("ItemsPedido", "spObterNomeImpressoraPorCodigoPedido", intCodPedido);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return new DataSet();
            }
            return ds;
        }
        public static DataSet ItensSelect(int iCodPedido, int intCodgrupo = 0,
            string iNomeImpressora = "", string iTipoPedido = "")
        {
            string iSqlWhere;
            string iSqlJoin = "left join Produto P on P.Codigo = It.CodProduto ";
            string iTipoAgrupamento;

            if (iTipoPedido == "0 - Entrega")
            {
                iTipoAgrupamento = Utils.RetornaConfiguracaoDelivery().TipoAgrupamento;
            }
            else if (iTipoPedido == "2 - Balcao")
            {
                iTipoAgrupamento = Utils.RetornaConfiguracaoBalcao().TipoAgrupamento;
            }
            else
            {
                iTipoAgrupamento = Utils.RetornaConfiguracaoMesa().TipoAgrupamento;
            }
            if (iTipoAgrupamento == "Por Impressora")
            {
                iSqlWhere = " where  PE.Codigo=" + iCodPedido + " and ImprimeCozinhaSN = 1 and G.NomeImpressora='" + iNomeImpressora + "'";
            }
            else if (iTipoAgrupamento == "Por Cozinha/Grupo")
            {
                iSqlWhere = " where  PE.Codigo=" + iCodPedido + " and ImprimeCozinhaSN = 1 and P.CodGrupo=" + intCodgrupo + "";
            }
            else
            {
                iSqlWhere = " where  PE.Codigo=" + iCodPedido + " and ImprimeCozinhaSN = 1 ";
            }
            //if (Utils.MarcaTipoConfiguracaoProduto().TipoCodigo == "Personalizado")
            //{
            //    iSqlJoin = "left join Produto P on P.CodigoPersonalizado = It.CodProduto ";
            //}
            string iSql = " select IT.CodProduto ,PE.*,  " +
                           " CodGrupo, " +
                           " NomeImpressora, " +
                           " IT.CodProduto,  " +
                           " IT.CodPedido " +
                           " from ItemsPedido IT " +
                           " left join Pedido PE ON PE.Codigo = IT.CodPedido " +
                           iSqlJoin +
                           " LEFT JOIN GRUPO G ON G.Codigo = P.CodGrupo ";

            iSql = iSql + iSqlWhere;

            return conexao.SelectAll("ItemsPedido", "", iSql);
        }
        /// <summary>
        /// Imprime a viaCozinha separada de acordo com filtro
        /// </summary>
        /// <param name="iCodPedido">
        /// Codigo do Pedido</param>


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
        public static Boolean ValidaPermissao(int iCodUser = 0, string iNomePermissao = "")
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
                    if (frm.Autorizacao)
                    {
                        intCodUserAutorizador = frm.CodUser;
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
                intCodUserAutorizador = iCodUser;
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
            return JsonConvert.SerializeObject(dadosApp, Formatting.None);
        }
        public static string SerializaObjeto(ConfiguracaoBuscaPorCodigo iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(List<Produto_DiaDisponivelSite> iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjetoDadosApp(DadosApp iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjetoDadosPush(DadosPush iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(DadosImpressoras iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(DadosPush iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(ImpressaoBalcao iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(ImpressaoDelivery iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }

        public static string SerializaObjeto(ImpressaoMesa iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(OpcaoDia iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(List<PrecoDiaProduto> iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(List<FidelidadeDias> iValores)
        {
            if (iValores == null)
            {
                return new List<FidelidadeDias>().ToString();
            }
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(MultiSabores iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(List<Models.Produto.OpcaoDia> iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(List<CidadesAtendidas> iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(List<HorariosEntregaJson> iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static string SerializaObjeto(Fidelidade iValores)
        {
            return JsonConvert.SerializeObject(iValores, Formatting.None);
        }
        public static ConfiguracaoBuscaPorCodigo MarcaTipoConfiguracaoProduto()
        {
            ConfiguracaoBuscaPorCodigo confi = new ConfiguracaoBuscaPorCodigo();
            try
            {
                confi = Utils.DeserializaObjetoConfig(Sessions.returnConfig.ProdutoPorCodigo);

                //if (confi.PorCodigo!=true)
                //{
                //    return;
                //}

                //chkProdutoCodigo.Checked = confi.PorCodigo;
                //cbxTipoCodigo.Text = confi.TipoCodigo;
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return confi;
        }
        public static List<FidelidadeDias> DeserializaObjetoFidelidade(string iValores)
        {
            if (iValores == "" || iValores == null)
            {
                return new List<FidelidadeDias>();
            }
            return JsonConvert.DeserializeObject<List<FidelidadeDias>>(iValores);
        }
        public static ConfiguracaoBuscaPorCodigo DeserializaObjetoConfig(string iValores)
        {
            if (iValores == "")
            {
                return new ConfiguracaoBuscaPorCodigo();
            }
            return JsonConvert.DeserializeObject<ConfiguracaoBuscaPorCodigo>(iValores);
        }
        public static DadosImpressoras DeserializaObjetoImpressoras(string iValores)
        {
            if (iValores == "" || iValores == null)
            {
                return new DadosImpressoras();
            }
            return JsonConvert.DeserializeObject<DadosImpressoras>(iValores);
        }
        public static List<PrecoDiaProduto> DeserializaObjeto(string iValores)
        {
            if (iValores == "")
            {
                return new List<PrecoDiaProduto>();
            }
            return JsonConvert.DeserializeObject<List<PrecoDiaProduto>>(iValores);
        }
        public  static DadosSMS DeserializaObjetoSMS(string iValores)
        {
            if (iValores == "")
            {
                return new DadosSMS();
            }
            return JsonConvert.DeserializeObject<DadosSMS>(iValores);
        }
        public static ImpressaoBalcao RetornaConfiguracaoBalcao()
        {
            ImpressaoBalcao imprBalcao = new Models.Configuracoes.ImpressaoBalcao();
            try
            {
                imprBalcao = Utils.DeserializaObjetoBalcao(Sessions.returnConfig.ImprimeViaBalcao);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return imprBalcao;
        }

        public static ImpressaoMesa RetornaConfiguracaoMesa()
        {
            ImpressaoMesa imprCozinha = new ImpressaoMesa();
            try
            {
                imprCozinha = Utils.DeserializaObjetoCozinha(Sessions.returnConfig.ImpViaCozinha);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return imprCozinha;
        }
        public static ImpressaoDelivery RetornaConfiguracaoDelivery()
        {
            ImpressaoDelivery imprDelivery = new ImpressaoDelivery();
            try
            {
                imprDelivery = Utils.DeserializaObjetoDelivery(Sessions.returnConfig.ImprimeViaEntrega);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return imprDelivery;
        }
        public static DadosSMS RetornaConfiguracaoSMS()
        {
            DadosSMS sms = new DadosSMS();
            try
            {
                sms = Utils.DeserializaObjetoSMS(Sessions.returnEmpresa.ConfiguracaoSMS);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return sms;
        }
        public static DadosPush RetornaConfiguracaoPush()
        {
            DadosPush dadosPush = new DadosPush();
            try
            {
                dadosPush = DeserializaObjetoPush(Sessions.returnConfig.DadosPush);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return dadosPush;
        }
        public static ImpressaoBalcao DeserializaObjetoBalcao(string iValores)
        {
            if (iValores == "" || iValores == null)
            {
                return new ImpressaoBalcao();
            }
            return JsonConvert.DeserializeObject<ImpressaoBalcao>(iValores);
        }
        public static ImpressaoMesa DeserializaObjetoCozinha(string iValores)
        {
            if (iValores == "" || iValores == null)
            {
                return new ImpressaoMesa();
            }
            return JsonConvert.DeserializeObject<ImpressaoMesa>(iValores);
        }
        public static ImpressaoDelivery DeserializaObjetoDelivery(string iValores)
        {
            if (iValores == "" || iValores == null)
            {
                return new ImpressaoDelivery();
            }
            return JsonConvert.DeserializeObject<ImpressaoDelivery>(iValores);
        }
        public static DadosPush DeserializaObjetoPush(string iValores)
        {
            if (iValores == "" || iValores == null)
            {
                return new DadosPush();
            }
            return JsonConvert.DeserializeObject<DadosPush>(iValores);
        }
        public static DadosApp DeserializaObjetoApp(string iValores)
        {
            if (iValores == "" || iValores == null)
            {
                return new DadosApp();
            }
            return JsonConvert.DeserializeObject<DadosApp>(iValores);
        }
        //public static DadosEnvioZenvia DeserializaObjetoZenvia(string iValores)
        //{
        //    if (iValores == "" || iValores == null)
        //    {
        //        return new DadosEnvioZenvia();
        //    }
        //    return JsonConvert.DeserializeObject<DadosEnvioZenvia>(iValores);
        //}
        //public static DadosEnvioLocaTWW DeserializaObjetoLocaSMS(string iValores)
        //{
        //    if (iValores == "" || iValores == null)
        //    {
        //        return new DadosEnvioLocaTWW();
        //    }
        //    return JsonConvert.DeserializeObject<DadosEnvioLocaTWW>(iValores);
        //}
        public static List<Produto_DiaDisponivelSite> DeserializaObjetoDias(string iValores)
        {
            if (iValores == "" || iValores == null)
            {
                return new List<Produto_DiaDisponivelSite>();
            }
            return JsonConvert.DeserializeObject<List<Produto_DiaDisponivelSite>>(iValores);

        }
        public static MultiSabores DeserializaObjeto4(string iValores)
        {
            if (iValores == "" || iValores == null)
            {
                return new MultiSabores();
            }
            return JsonConvert.DeserializeObject<MultiSabores>(iValores);

        }
        public static List<OpcaoDia> DeserializaObjeto3(string iValores)
        {
            if (iValores == "")
            {
                return new List<OpcaoDia>();
            }
            return JsonConvert.DeserializeObject<List<OpcaoDia>>(iValores);

        }
        public static List<CidadesAtendidas> DeserializaObjeto2(string iValores)
        {
            if (iValores == "")
            {
                return null;
            }
            return JsonConvert.DeserializeObject<List<CidadesAtendidas>>(iValores);

        }
        public static Fidelidade DeserializaObjeto5(string iValores)
        {
            if (iValores == "")
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Fidelidade>(iValores);

        }
        /// <summary>
        /// Verifica se o cliente tem pontos suficientes pra troca e quer trocar
        /// </summary>
        /// <param  
        /// <returns>Lista de itens selecionados pelo cliente</returns>
        public static void VerificaPontosFidelidade(int iCodPessoa)
        {
            //int pontosCliente = conexao.SelectRegistroPorCodigo("Pessoa_Fidelidade", "spObterFidelidadePessoa", iCodPessoa, false).Tables[0].
            //     Rows[0].Field<int>("Pontos");
            //int pontoTroca = conexao.SelectAll("Produto", "spObterMenorPontuacaoProduto").Tables[0].Rows[0].
            //    Field<int>("PontoFidelidadeTroca");
            //if (pontosCliente >= pontoTroca)
            //{
            //    if (Utils.MessageBoxQuestion("Esse cliente tem pontos suficientes para resgatar produtos, quer ver os produtos disponiveis ?" +
            //          " ** Será exibido uma lista de produtos possíveis a troca **"))
            //    {
            //        frmProdutosTrocaFidelidade frm = new frmProdutosTrocaFidelidade(pontosCliente);
            //        frm.ShowDialog();
            //    }

            //}
        }
        public static bool ValidaCNPJ(string vrCNPJ)

        {

            string CNPJ = vrCNPJ.Replace(".", "");

            CNPJ = CNPJ.Replace("/", "");

            CNPJ = CNPJ.Replace("-", "");



            int[] digitos, soma, resultado;

            int nrDig;

            string ftmt;

            bool[] CNPJOk;



            ftmt = "6543298765432";

            digitos = new int[14];

            soma = new int[2];

            soma[0] = 0;

            soma[1] = 0;

            resultado = new int[2];

            resultado[0] = 0;

            resultado[1] = 0;

            CNPJOk = new bool[2];

            CNPJOk[0] = false;

            CNPJOk[1] = false;



            try

            {

                for (nrDig = 0; nrDig < 14; nrDig++)

                {

                    digitos[nrDig] = int.Parse(

                        CNPJ.Substring(nrDig, 1));

                    if (nrDig <= 11)

                        soma[0] += (digitos[nrDig] *

                          int.Parse(ftmt.Substring(

                          nrDig + 1, 1)));

                    if (nrDig <= 12)

                        soma[1] += (digitos[nrDig] *

                          int.Parse(ftmt.Substring(

                          nrDig, 1)));

                }



                for (nrDig = 0; nrDig < 2; nrDig++)

                {

                    resultado[nrDig] = (soma[nrDig] % 11);

                    if ((resultado[nrDig] == 0) || (

                         resultado[nrDig] == 1))

                        CNPJOk[nrDig] = (

                        digitos[12 + nrDig] == 0);

                    else

                        CNPJOk[nrDig] = (

                        digitos[12 + nrDig] == (

                        11 - resultado[nrDig]));

                }

                return (CNPJOk[0] && CNPJOk[1]);

            }

            catch

            {

                return false;

            }

        }
        /// <summary>
        /// Controla Ações de Fidelidade e acumula os pontos
        /// </summary>
        /// <param name="intCodProduto"> Código do produto que foi vendido</param
        /// <param name="intCodPessoa"> Codigo do cliente</param>
        /// <param name="vlrItemsPedido"> Valor do pedido , para casos que o tipo de promoção for por valor</param>
        public static void ControleFidelidade(int intCodPedido, int intCodUser = 1)
        {
            try
            {
                Fidelidade fidelidade = new Fidelidade();
                fidelidade = DeserializaObjeto5(Sessions.returnConfig.ControlaFidelidade);
                if (fidelidade == null)
                {
                    return;
                }
                int iQuantidadePonto = 0;
                if (fidelidade.Tipo == "Por Ponto")
                {
                    DataSet dsItems = conexao.SelectRegistroPorCodigo("Produto", "spObterItensPedidoPonto", intCodPedido);
                    for (int i = 0; i < dsItems.Tables[0].Rows.Count; i++)
                    {
                        PessoaFidelidade _pessFidelidade = new PessoaFidelidade()
                        {
                            CodPedido = intCodPedido,
                            CodPessoa = dsItems.Tables[0].Rows[i].Field<int>("CodPessoa"),
                            CodProduto = dsItems.Tables[0].Rows[i].Field<int>("Codigo"),
                            CodUsuario = intCodUser,
                            
                        };

                        if (!dsItems.Tables[0].Rows[i].Field<Boolean>("FidelidadeSN"))
                        {
                            iQuantidadePonto =dsItems.Tables[0].Rows[i].Field<int>("PontoFidelidadeVenda");
                        }
                        else
                        {
                            iQuantidadePonto =- dsItems.Tables[0].Rows[i].Field<int>("PontoFidelidadeTroca");
                        }
                       
                        // Se tipo de fidelidade for por valor ele pega o total do pedido
                        //if (fidelidade.Tipo != "Por Ponto")
                        //{
                        //    iQuantidadePonto = Convert.ToInt16(dsItems.Tables[0].Rows[i].Field<decimal>("TotalItens"));
                        //}
                        //else
                        //{
                        //    iQuantidadePonto = dsItems.Tables[0].Rows[i].Field<int>("PontoFidelidadeVenda");
                        //}

                        //Verifica se tem indice de multiplicação e se o dia atual esta dentro dos dias considerados
                        List<FidelidadeDias> Listdias = DeserializaObjetoFidelidade(fidelidade.Dias);
                        foreach (var dias in Listdias)
                        {
                            if (dias.DiaSemana == DateTime.Now.DayOfWeek.ToString())
                            {
                                iQuantidadePonto = dsItems.Tables[0].Rows[i].Field<int>("PontoFidelidadeVenda") * fidelidade.Multiplicador;
                                break;
                            }
                        }
                        _pessFidelidade.Pontos = iQuantidadePonto;
                        conexao.Insert("spAdicionarPontosFidelidade", _pessFidelidade);
                    }
                }
                else
                {
                    DataSet dsItems = conexao.SelectRegistroPorCodigo("Produto", "spObterItensPedidoPorValor", intCodPedido);
                    //for (int intFor = 0; intFor < dsItems.Tables[0].Rows.Count; intFor++)
                    //{
                    PessoaFidelidade _pessFidelidade = new PessoaFidelidade()
                    {
                        CodPedido = intCodPedido,
                        CodPessoa = dsItems.Tables[0].Rows[0].Field<int>("CodPessoa"),
                        CodProduto = dsItems.Tables[0].Rows[0].Field<int>("CodProduto"),
                        CodUsuario = intCodUser,
                        //Pontos = dsItems.Tables[0].Rows[i].Field<int>("PontoFidelidadeVenda")
                    };

                    decimal var = dsItems.Tables[0].Rows[0].Field<decimal>("TotalItens");
                    iQuantidadePonto = Convert.ToInt16(dsItems.Tables[0].Rows[0].Field<decimal>("TotalItens"));
                    List<FidelidadeDias> Listdias = DeserializaObjetoFidelidade(fidelidade.Dias);
                    foreach (var dias in Listdias)
                    {
                        if (dias.DiaSemana == DateTime.Now.DayOfWeek.ToString())
                        {
                            iQuantidadePonto = Convert.ToInt16(dsItems.Tables[0].Rows[0].Field<decimal>("TotalItens")) * fidelidade.Multiplicador;
                            break;
                        }
                    }
                    _pessFidelidade.Pontos = iQuantidadePonto;
                    conexao.Insert("spAdicionarPontosFidelidade", _pessFidelidade);


                    //  }

                }


            }
            catch (Exception erro)
            {
                MessageBox.Show("Controle de Fidelidade " + erro.Message);
            }

        }
        public static void MontaCombox(ComboBox icbxName, string idisplayName,
            string iValueMember, string iTable, string iSP, int iCod = -1)
        {
            try
            {
                icbxName.DataSource = null;
                icbxName.DisplayMember = null;
                icbxName.ValueMember = null;
                if (iCod == -1)
                {
                    icbxName.DataSource = null;
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

                MessageBox.Show("Erro ao listar itens tabela " + iTable + erro.Message);
            }

        }

        public static CepUtil BuscaCEPOnline(string iCEP)
        {
            CepUtil cep = new CepUtil();
            try
            {
                Correios.AtendeClienteClient consulta = new Correios.AtendeClienteClient("AtendeClientePort");

                var resultado = consulta.consultaCEP(iCEP.Replace("-", ""));

                //Insere o CEP no Banco Local caso retorne do WS
                if (resultado != null)
                {
                    cep.Cep = iCEP.Replace("-", "");
                    cep.Bairro = resultado.bairro.ToUpper();
                    cep.Cidade = resultado.cidade.ToUpper();
                    cep.Estado = resultado.uf.ToUpper();
                    cep.Logradouro = resultado.end.ToUpper();
                    conexao.Insert("spAdicionarCep", cep);
                }
                else
                {
                    MessageBox.Show("Endereço não encontrado ou CEP inválido");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            //CepUtil cep = new CepUtil();
            //Boolean iReturn = false;
            //try
            //{
            //    Correios.AtendeClienteClient consulta = new Correios.AtendeClienteClient("AtendeClientePort");

            //    var resultado = await consulta.consultaCEPAsync(iCEP.Replace("-", ""));


            //    if (resultado != null)
            //    {
            //        cep.Cep = iCEP.Replace("-", "");
            //        cep.Bairro = resultado.@return.bairro;
            //        cep.Cidade = resultado.@return.cep;
            //        cep.Estado = resultado.@return.uf;
            //        cep.Logradouro = resultado.@return.end;
            //        conexao.Insert("spAdicionarCep", cep);
            //        iReturn = true;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Endereço não encontrado ou cep inválido");
            //    }
            //}
            //catch (Exception erro)
            //{
            //    MessageBox.Show(Bibliotecas.cException + erro.Message);
            //}

            return cep;
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
        public static string ImpressaoEntre_Epson(int iCodPedido, decimal iValorPago, string iPrevisaoEntrega, int iNumCopias = 0)
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

                for (int i = 0; i < iNumCopias; i++)
                {
                    report.PrintToPrinter(1, false, 0, 0);
                }


            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro na impressao :" + erro.Message);
            }
            return iRetorno;
        }
        public static string ImpressaoEntreganova(int iCodPedido, decimal iTotalReceber,
            int iNumCopias = 0, string iNomeImpressora = "",
            int CodEndereco = 0, decimal iTroco = 0)
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
                    PrinterSettings printersettings = new PrinterSettings();
                    printersettings.PrinterName = iNomeImpressora;
                    printersettings.Copies = 1;
                    printersettings.Collate = false;
                    //report.Load(Directory.GetCurrentDirectory() + @"\RelDelivery.rpt");
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
                    report.SetParameterValue("@CodEndereco", CodEndereco);
                    report.SetParameterValue("TotalReceber", iTotalReceber);
                    report.SetParameterValue("Troco", iTroco);
                    if (iNomeImpressora != "")
                    {
                        for (int i = 0; i < iNumCopias; i++)
                        {
                            report.PrintOptions.PrinterName = iNomeImpressora;
                            report.PrintToPrinter(printersettings, new PageSettings(), false);
                        }
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

        public static string ImpressaoEntreganova_Matricial(int iCodPedido, decimal iValorPago, int iNumCopias = 0)
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
                for (int i = 0; i < iNumCopias; i++)
                {
                    report.PrintToPrinter(0, true, 0, 0);
                }

            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ná impressao :" + erro.Message);
            }
            return iRetorno;
        }
        public static string ImpressaoEntreganova_20(int iCodPedido, decimal iValorPago, int iNumCopias = 0)
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

                    for (int i = 0; i < iNumCopias; i++)
                    {
                        report.PrintToPrinter(0, false, 0, 0);
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
        public static string ImpressaoBalcaoCozihanova(int iCodPedido, int iNumCopias = 0)
        {
            string iRetorno = ""; ;

            RelBalcao_Cozinha report;
            try
            {
                report = new RelBalcao_Cozinha();
                crtableLogoninfos = new TableLogOnInfos();
                crtableLogoninfo = new TableLogOnInfo();
                crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                report.Load(Directory.GetCurrentDirectory() + @"\RelBalcao_Cozinha.rpt");
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
                report.SetParameterValue("@CodEndereco", 0);

                for (int i = 0; i < iNumCopias; i++)
                {
                    report.PrintToPrinter(1, false, 0, 0);
                }

            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.InnerException.Message);
            }
            return iRetorno;
        }
        public static string ImpressaoCozihanova(int iCodPedido, int iNumCopias = 0)
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
                report.SetParameterValue("@CodEndereco", 0);

                for (int i = 0; i < iNumCopias; i++)
                {
                    report.PrintToPrinter(1, false, 0, 0);
                }

            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.InnerException.Message);
            }
            return iRetorno;
        }
        public static DadosApp RetornaDadosApp()
        {
            DadosApp app;
            if (Sessions.returnConfig.DadosApp != null)
            {
                app = DeserializaObjetoApp(Sessions.returnConfig.DadosApp);
            }
            else
            {
                app = new DadosApp();
            }

            return app;
        }
        public static DadosImpressoras RetornaImpressoras()
        {
            DadosImpressoras impressoras = DeserializaObjetoImpressoras(Sessions.returnConfig.Impressoras);
            return impressoras;
        }

        /// <summary>
        /// Gera relatório e imprimi
        /// </summary>
        /// <param name="iReport"> Relatório a ser montando</param>
        /// <param name="inCodigo">Código do registro a ser filtrado</param>
        /// <returns></returns>
        public static ReportClass GerarReportCodigo(ReportClass iReport, int inCodigo)
        {
            try
            {
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                string nameReport = iReport.FileName;
                iReport.Load(Directory.GetCurrentDirectory() + @"\" + nameReport + ".rpt");
                crConnectionInfo.ServerName = Sessions.returnEmpresa.Servidor;
                crConnectionInfo.DatabaseName = Sessions.returnEmpresa.Banco;
                crConnectionInfo.UserID = "sa";
                crConnectionInfo.Password = "1001";

                CrTables = iReport.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }

                iReport.SetParameterValue("@Codigo", inCodigo);

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return iReport;
        }
        public static ReportClass GerarReport(ReportClass iReport)
        {
            try
            {
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                string nameReport = iReport.FileName;
                iReport.Load(Directory.GetCurrentDirectory() + @"\" + nameReport + ".rpt");
                crConnectionInfo.ServerName = Sessions.returnEmpresa.Servidor;
                crConnectionInfo.DatabaseName = Sessions.returnEmpresa.Banco;
                crConnectionInfo.UserID = "sa";
                crConnectionInfo.Password = "1001";

                CrTables = iReport.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }


            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return iReport;
        }
        /// <summary>
        /// Função para listar o nome da instancia e servidores SQLSERVER , presentes na maquina na rede
        /// </summary>
        /// <returns>Nome da Maquina ou Nome da Maquina +Instancia</returns>
        public static string ListaServidoresSQL()
        {

            string strRetur = "";
            try
            {
                DataTable servers;
                servers = SqlDataSourceEnumerator.Instance.GetDataSources();
                for (int i = 0; i < servers.Rows.Count; i++)
                {
                    if ((servers.Rows[i]["InstanceName"] as string) != null)
                        strRetur = servers.Rows[i]["ServerName"] + "\\" + servers.Rows[i]["InstanceName"];
                    else
                        strRetur = servers.Rows[i]["ServerName"].ToString();
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return strRetur;
        }
        public static ReportClass GerarReportSoDatas(ReportClass iReport, DateTime dtInicio, DateTime dtFim,
            string horaInicio = " 00:00:00", string horaFIm = " 23:59:59")
        {

            // iReport = new ReportClass();
            //CrystalReportViewer crystalReportViewer1;
            try
            {
                //     crystalReportViewer1 = new CrystalReportViewer();
                var datInicio = Convert.ToDateTime(dtInicio.ToShortDateString() + " " + horaInicio);
                var datFim = Convert.ToDateTime(dtFim.ToShortDateString() + " " + horaFIm);

                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                string nameReport = iReport.FileName;
                iReport.Load(Directory.GetCurrentDirectory() + @"\" + nameReport + ".rpt");
                crConnectionInfo.ServerName = Sessions.returnEmpresa.Servidor;
                crConnectionInfo.DatabaseName = Sessions.returnEmpresa.Banco;
                crConnectionInfo.UserID = "sa";
                crConnectionInfo.Password = "1001";

                CrTables = iReport.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }

                iReport.SetParameterValue("@DataInicio", datInicio);
                iReport.SetParameterValue("@DataFim", datFim);

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return iReport;
        }
        public static void GerarReportSoDatas(ReportClass iReport, DateTime dtInicio)
        {

            try
            {
                var datInicio = Convert.ToDateTime(dtInicio.ToShortDateString() + " 00:00:00");
                var datFim = Convert.ToDateTime(dtInicio.ToShortDateString() + " 23:59:59");

                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                string nameReport = iReport.FileName;
                iReport.Load(Directory.GetCurrentDirectory() + @"\" + nameReport + ".rpt");
                crConnectionInfo.ServerName = Sessions.returnEmpresa.Servidor;
                crConnectionInfo.DatabaseName = Sessions.returnEmpresa.Banco;
                crConnectionInfo.UserID = "sa";
                crConnectionInfo.Password = "1001";

                CrTables = iReport.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }

                iReport.SetParameterValue("@DataI", datInicio);
                iReport.SetParameterValue("@DataF", datFim);
                iReport.SetParameterValue("@DataInicio", datInicio);
                iReport.SetParameterValue("@DataFim", datFim);
                iReport.PrintToPrinter(1, false, 0, 0);

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException);
            }

        }
        public static void ImpressaoCozihanova_SeparadoPorImpressora(int iCodPedido, string iNomeImpressora)
        {
            RelComandaMesa_PorImpressora report;
            crtableLogoninfos = new TableLogOnInfos();
            crtableLogoninfo = new TableLogOnInfo();
            crConnectionInfo = new ConnectionInfo();
            report = new RelComandaMesa_PorImpressora();
            try
            {
                Tables CrTables;
                PrinterSettings printersettings = new PrinterSettings();
                printersettings.Copies = 1;
                printersettings.Collate = false;
                printersettings.PrinterName = iNomeImpressora;
                report.Load(Directory.GetCurrentDirectory() + @"\RelComandaMesa_PorImpressora.rpt");
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
                report.SetParameterValue("@NomeImpressora", iNomeImpressora);

                if (iNomeImpressora != "")
                {
                    report.PrintOptions.PrinterName = iNomeImpressora;
                    report.PrintToPrinter(printersettings, new PageSettings(), false);
                }
                else
                {
                    report.PrintToPrinter(1, false, 0, 0);
                }

            }
            finally
            {
                report.Dispose();
            }
        }
        /// <summary>
        /// Imprime os items da Mesa ainda não impressos  ( Agrupa por Cozinha/Grupo)
        /// </summary>
        /// <param name="iCodPedido">
        /// Código do Pedido </param>
        /// <param name="iNomeImpressora">
        /// Nome da impressora para qual será enviado o pedido </param>
        /// <param name="iCodGrupo">
        /// Código do grupo </param>
        public static void ImpressaoCozihanova_SeparadoPorCozinhaGrupo(int iCodPedido, string iNomeImpressora, int iCodGrupo)
        {
            RelComandaMesaPorGrupo report;
            crtableLogoninfos = new TableLogOnInfos();
            crtableLogoninfo = new TableLogOnInfo();
            crConnectionInfo = new ConnectionInfo();
            report = new RelComandaMesaPorGrupo();
            try
            {
                Tables CrTables;
                PrinterSettings printersettings = new PrinterSettings();
                printersettings.Copies = 1;
                printersettings.Collate = false;
                printersettings.PrinterName = iNomeImpressora;
                report.Load(Directory.GetCurrentDirectory() + @"\RelComandaMesaPorGrupo.rpt");
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
                report.SetParameterValue("@CodGrupo", iCodGrupo);

                if (iNomeImpressora != "")
                {
                    report.PrintOptions.PrinterName = iNomeImpressora;
                    report.PrintToPrinter(printersettings, new PageSettings(), false);
                }
                else
                {
                    report.PrintToPrinter(1, false, 0, 0);
                }

            }
            finally
            {
                report.Dispose();
            }
        }
        public static void ImpressaoBalcao_SeparadoPorImpressora(int iCodPedido, string iNomeImpressora)
        {
            RelBalcaoCozinhaPorImpressora report;
            crtableLogoninfos = new TableLogOnInfos();
            crtableLogoninfo = new TableLogOnInfo();
            crConnectionInfo = new ConnectionInfo();
            report = new RelBalcaoCozinhaPorImpressora();
            try
            {
                Tables CrTables;
                PrinterSettings printersettings = new PrinterSettings();
                printersettings.Copies = 1;
                printersettings.Collate = false;
                printersettings.PrinterName = iNomeImpressora;
                report.Load(Directory.GetCurrentDirectory() + @"\RelBalcaoCozinhaPorImpressora.rpt");
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
                report.SetParameterValue("@NomeImpressora", iNomeImpressora);

                if (report.Database.Tables.Count == 0)
                {
                    return;
                }
                if (iNomeImpressora != "")
                {
                    report.PrintOptions.PrinterName = iNomeImpressora;
                    report.PrintToPrinter(printersettings, new PageSettings(), false);
                }
                else
                {
                    report.PrintToPrinter(1, false, 0, 0);
                }

            }
            finally
            {
                report.Dispose();
            }
        }
        public static void ImpressaoDeliveryCoziha_SeparadoPorImpressora(int iCodPedido, string iNomeImpressora)
        {
            RelDeliveryCozinhaPorImpressora report;
            crtableLogoninfos = new TableLogOnInfos();
            crtableLogoninfo = new TableLogOnInfo();
            crConnectionInfo = new ConnectionInfo();
            report = new RelDeliveryCozinhaPorImpressora();
            try
            {
                Tables CrTables;
                PrinterSettings printersettings = new PrinterSettings();
                printersettings.Copies = 1;
                printersettings.Collate = false;
                printersettings.PrinterName = iNomeImpressora;
                report.Load(Directory.GetCurrentDirectory() + @"\RelDeliveryCozinhaPorImpressora.rpt");
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
                report.SetParameterValue("@NomeImpressora", iNomeImpressora);

                if (report.Database.Tables.Count == 0)
                {
                    return;
                }
                if (iNomeImpressora != "")
                {
                    report.PrintOptions.PrinterName = iNomeImpressora;
                    report.PrintToPrinter(printersettings, new PageSettings(), false);
                }
                else
                {
                    report.PrintToPrinter(1, false, 0, 0);
                }

            }
            finally
            {
                report.Dispose();
            }
        }
        public static void ImpressaoBalcao_CozinhaPorGrupo(int iCodPedido, string iNomeImpressora, int iCodGrupo)
        {
            RelBalcaoCozinhaGrupoCozinha report;
            crtableLogoninfos = new TableLogOnInfos();
            crtableLogoninfo = new TableLogOnInfo();
            crConnectionInfo = new ConnectionInfo();
            report = new RelBalcaoCozinhaGrupoCozinha();
            try
            {
                Tables CrTables;
                PrinterSettings printersettings = new PrinterSettings();
                printersettings.Copies = 1;
                printersettings.Collate = false;
                printersettings.PrinterName = iNomeImpressora;
                report.Load(Directory.GetCurrentDirectory() + @"\RelBalcaoCozinhaGrupoCozinha.rpt");
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
                report.SetParameterValue("@CodGrupo", iCodGrupo);

                if (iNomeImpressora != "")
                {
                    report.PrintOptions.PrinterName = iNomeImpressora;
                    report.PrintToPrinter(printersettings, new PageSettings(), false);
                }
                else
                {
                    report.PrintToPrinter(1, false, 0, 0);
                }

            }
            finally
            {
                report.Dispose();
            }
        }
        public static void ImpressaoDelivery_CozinhaPorGrupo(int iCodPedido, string iNomeImpressora, int iCodGrupo)
        {
            RelDeliveryCozinhaGrupoCozinha report;
            crtableLogoninfos = new TableLogOnInfos();
            crtableLogoninfo = new TableLogOnInfo();
            crConnectionInfo = new ConnectionInfo();
            report = new RelDeliveryCozinhaGrupoCozinha();
            try
            {
                Tables CrTables;
                PrinterSettings printersettings = new PrinterSettings();
                printersettings.Copies = 1;
                printersettings.Collate = false;
                printersettings.PrinterName = iNomeImpressora;
                report.Load(Directory.GetCurrentDirectory() + @"\RelDeliveryCozinhaGrupoCozinha.rpt");
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
                report.SetParameterValue("@CodGrupo", iCodGrupo);

                if (iNomeImpressora != "")
                {
                    report.PrintOptions.PrinterName = iNomeImpressora;
                    report.PrintToPrinter(printersettings, new PageSettings(), false);
                }
                else
                {
                    report.PrintToPrinter(1, false, 0, 0);
                }

            }
            finally
            {
                report.Dispose();
            }
        }
        public static void ImpressaoMesaPorImpressora(int iCodPedido, int iCodGupo, int iNumCopias,
            string strNomeImpressora)
        {
            string iRetorno = string.Empty;

            RelComandaMesaPorGrupo report;
            report = new RelComandaMesaPorGrupo();
            crtableLogoninfos = new TableLogOnInfos();
            crtableLogoninfo = new TableLogOnInfo();
            crConnectionInfo = new ConnectionInfo();

            PrinterSettings printersettings = new PrinterSettings();
            printersettings.PrinterName = strNomeImpressora;
            printersettings.Copies = 1;
            printersettings.Collate = false;
            try
            {

                Tables CrTables;
                report.Load(Directory.GetCurrentDirectory() + @"\RelComandaMesaPorGrupo.rpt", OpenReportMethod.OpenReportByTempCopy);
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

                if (strNomeImpressora != "")
                {
                    for (int i = 0; i < iNumCopias; i++)
                    {
                        report.PrintOptions.PrinterName = strNomeImpressora;
                        report.PrintToPrinter(printersettings, new PageSettings(), false);
                    }

                }
                else
                {
                    for (int i = 0; i < iNumCopias; i++)
                    {
                        report.PrintToPrinter(1, true, 0, 0);
                    }
                }


            }

            catch (Exception erro)
            {

                MessageBox.Show(Bibliotecas.cException + erro.InnerException.Message);
            }
            //finally
            //{
            //    report.Dispose();
            //}
            // return iRetorno;
        }
        public static async void ImpressaMesaNova(int iCodPedido, int iCodGupo, Boolean iExport = false, int iNumCopias = 0, string iNomeImpressora = "", Boolean iImprimirAgora = false)
        {
            string iRetorno = string.Empty;

            RelComandaMesa report;
            report = new RelComandaMesa();
            crtableLogoninfos = new TableLogOnInfos();
            crtableLogoninfo = new TableLogOnInfo();
            crConnectionInfo = new ConnectionInfo();

            PrinterSettings printersettings = new PrinterSettings();
            printersettings.PrinterName = iNomeImpressora;
            //  printersettings.PrintFileName = "RelComandaMesa_" + iCodPedido + "";
            printersettings.Copies = 1;
            printersettings.Collate = false;
            try
            {

                Tables CrTables;
                report.Load(Directory.GetCurrentDirectory() + @"\RelComandaMesa.rpt", OpenReportMethod.OpenReportByTempCopy);
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

                if (iNomeImpressora != "")
                {
                    for (int i = 0; i < iNumCopias; i++)
                    {
                        report.PrintOptions.PrinterName = iNomeImpressora;
                        report.PrintToPrinter(printersettings, new PageSettings(), false);
                    }

                }
                else
                {
                    for (int i = 0; i < iNumCopias; i++)
                    {
                        // frmPrincipal frm = new frmPrincipal(false);
                        // await Task.Factory.StartNew(() => Thread.Sleep(1));
                        report.PrintToPrinter(1, true, 0, 0);

                        //  frm = new frmPrincipal(true);

                    }
                }


            }

            catch (Exception erro)
            {

                MessageBox.Show(Bibliotecas.cException + erro.InnerException.Message);
            }
            //finally
            //{
            //    report.Dispose();
            //}
            // return iRetorno;
        }
        public static void ImprimirCaixa(ReportClass iReport, string iValor, string iSolicitante)
        {
            crtableLogoninfos = new TableLogOnInfos();
            crtableLogoninfo = new TableLogOnInfo();
            crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            iReport.Load(Directory.GetCurrentDirectory() + @"\" + iReport.FileName + ".rpt");
            crConnectionInfo.ServerName = Sessions.returnEmpresa.Servidor;
            crConnectionInfo.DatabaseName = Sessions.returnEmpresa.Banco;
            crConnectionInfo.UserID = "sa";
            crConnectionInfo.Password = "1001";

            CrTables = iReport.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            iReport.SetParameterValue("Valor", iValor);
            iReport.SetParameterValue("Solicitante", iSolicitante);
            iReport.SetParameterValue("Usuario", Sessions.retunrUsuario.Nome);
            iReport.PrintToPrinter(1, false, 0, 0);
        }
        public static string ImprimirCancelamentos(DateTime dtInicio, DateTime dtFim)
        {
            string iRetorno = "";
            RelCancelamentos report;
            report = new RelCancelamentos();
            crtableLogoninfos = new TableLogOnInfos();
            crtableLogoninfo = new TableLogOnInfo();
            crConnectionInfo = new ConnectionInfo();
            try
            {

                try
                {
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
                    report.SetParameterValue("@DataI", dtInicio);
                    report.SetParameterValue("@DataF", dtFim);
                    report.PrintToPrinter(1, false, 0, 0);

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
        //public static string RelCaixaMOvimento()
        //{
        //    string iRetorno = "";
        //    RelComandaMesa report;
        //    report = new RelComandaMesa();
        //    crtableLogoninfos = new TableLogOnInfos();
        //    crtableLogoninfo = new TableLogOnInfo();
        //    crConnectionInfo = new ConnectionInfo();
        //    try
        //    {

        //        try
        //        {
        //            Tables CrTables;
        //            report.Load(Directory.GetCurrentDirectory() + @"\RelComandaMesa.rpt");
        //            crConnectionInfo.ServerName = Sessions.returnEmpresa.Servidor;
        //            crConnectionInfo.DatabaseName = Sessions.returnEmpresa.Banco;
        //            crConnectionInfo.UserID = "sa";
        //            crConnectionInfo.Password = "1001";

        //            CrTables = report.Database.Tables;
        //            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
        //            {
        //                crtableLogoninfo = CrTable.LogOnInfo;
        //                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
        //                CrTable.ApplyLogOnInfo(crtableLogoninfo);
        //            }
        //            report.SetParameterValue("@Codigo", iCodPedido);
        //            report.SetParameterValue("@CodGrupo", iCodGupo);

        //            if (report.Rows.Count > 0)
        //            {


        //                if (iExport)
        //                {
        //                    CrystalDecisions.Shared.DiskFileDestinationOptions reportExport =
        //                    new CrystalDecisions.Shared.DiskFileDestinationOptions();
        //                    reportExport.DiskFileName = Directory.GetCurrentDirectory() + @"\RelComandaMesa.txt";

        //                    report.ExportOptions.ExportDestinationType =
        //                    CrystalDecisions.Shared.ExportDestinationType.DiskFile;

        //                    report.ExportOptions.ExportFormatType =
        //                    CrystalDecisions.Shared.ExportFormatType.Text;

        //                    report.ExportOptions.DestinationOptions = reportExport;
        //                    report.Export();
        //                    iRetorno = Directory.GetCurrentDirectory() + @"\RelComandaMesa.txt";
        //                }
        //                else
        //                {
        //                    for (int i = 0; i < iNumCopias; i++)
        //                    {
        //                        report.PrintToPrinter(1, false, 0, 0);
        //                    }

        //                }
        //            }
        //        }
        //        finally
        //        {
        //            // report.Dispose();

        //        }
        //    }
        //    catch (Exception erro)
        //    {

        //        MessageBox.Show(erro.InnerException.Message);
        //    }
        //    return iRetorno;
        //}
        // public static string RelCaixaHistorico(DateTime idtInicio, DateTime idtFim, string iNumcaixa, string iEntradaSaida, string iCodPagamento, string iTurno)
        public static string RelCaixaHistorico(string iSqlExzecu)
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


                report.SetSQLCommandTable(crConnectionInfo, "CaixaMovimento", iSqlExzecu);
                //report.SetParameterValue("@Turno", iTurno);
                //report.SetParameterValue("@CodCaixa", iNumcaixa);
                //report.SetParameterValue("@DataI", idtInicio);
                //report.SetParameterValue("@DataF", idtFim);
                //report.SetParameterValue("@CodPagamento", iCodPagamento);
                //report.SetParameterValue("@EntradaSaida", iEntradaSaida);


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
        public static string ImpressaoFechamentoNovo(int iCodPedido, int iNumCopias = 0, string iNomeImpressora = "")
        {
            string iRetorno = "";
            try
            {
                RelFechamentoMesa report;
                report = new RelFechamentoMesa();

                try
                {
                    PrinterSettings printersettings = new PrinterSettings();
                    printersettings.PrinterName = iNomeImpressora;
                    printersettings.Copies = short.Parse(iNumCopias.ToString());
                    printersettings.Collate = false;

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
                    report.SetParameterValue("@CodEndereco", 0);
                    if (iNomeImpressora != "")
                    {
                        report.PrintOptions.PrinterName = iNomeImpressora;
                        report.PrintToPrinter(printersettings, new PageSettings(), false);

                    }
                    else
                    {
                        report.PrintToPrinter(1, false, 0, 0);
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
        public static string ImpressaoFechamentoNovo_20(int iCodPedido, int iNumCopias = 0)
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
                    report.PrintToPrinter(0, true, 0, 0);
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
        public static void ImpressaoPorCozinhaBalcao(int iCodPedido)
        {
            try
            {

                string TipoAgrupamentoDelivery = Utils.RetornaConfiguracaoDelivery().TipoAgrupamento;
                DataSet itemsPedido, dsItemsNaoImpresso;
                DataSet dsI = new DataSet();
                dsItemsNaoImpresso = Utils.CarregaItens(iCodPedido);

                if (dsItemsNaoImpresso.Tables.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < dsItemsNaoImpresso.Tables["ItemsPedido"].Rows.Count; i++)
                {
                    int CodGrupo = dsItemsNaoImpresso.Tables[0].Rows[i].Field<int>("CodGrupo");
                    string iNomeImpressora = dsItemsNaoImpresso.Tables[0].Rows[i].Field<string>("NomeImpressora");

                    if (TipoAgrupamentoDelivery == "Por Cozinha/Grupo")
                    {
                        //if (!ProdutosPorCodigo && TipoCodigo != "Personalizado")
                        //{
                        itemsPedido = conexao.SelectRegistroPorCodigo("ItemsPedido", "spObterItemsNaoImpressoPorGrupo", iCodPedido, "", CodGrupo);
                        if (itemsPedido.Tables[0].Rows.Count > 0)
                        {
                            Utils.ImpressaoDelivery_CozinhaPorGrupo(iCodPedido, iNomeImpressora, CodGrupo);
                        }

                    }
                    else
                    {
                        itemsPedido = conexao.SelectItensPorImpressora(iCodPedido, iNomeImpressora);
                        if (itemsPedido.Tables[0].Rows.Count == 0)
                        {
                            return;
                        }
                        Utils.ImpressaoDeliveryCoziha_SeparadoPorImpressora(iCodPedido, iNomeImpressora);
                    }
                    //
                    for (int intfor = 0; intfor < itemsPedido.Tables["ItemsPedido"].Rows.Count; intfor++)
                    {
                        AtualizaItemsImpresso Atualiza = new AtualizaItemsImpresso();
                        Atualiza.CodPedido = iCodPedido;
                        Atualiza.CodProduto = itemsPedido.Tables["ItemsPedido"].Rows[intfor].Field<int>("CodProduto");
                        Atualiza.ImpressoSN = true;
                        conexao.Update("spInformaItemImpresso", Atualiza);
                    }
                    ImpressaoPorCozinha(iCodPedido);
                }


            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel imprimir o Item da Mesa verificar a impressora" + erro.Message);
            }
        }
        public static void ImpressaoPorCozinha(int iCodPedido)
        {
            try
            {

                string TipoAgrupamentoDelivery = Utils.RetornaConfiguracaoDelivery().TipoAgrupamento;
                DataSet itemsPedido, dsItemsNaoImpresso, dsItems;
                DataSet dsI = new DataSet();
                dsItemsNaoImpresso = Utils.CarregaItens(iCodPedido);

                if (dsItemsNaoImpresso.Tables.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < dsItemsNaoImpresso.Tables["ItemsPedido"].Rows.Count; i++)
                {
                    int CodGrupo = dsItemsNaoImpresso.Tables[0].Rows[i].Field<int>("CodGrupo");
                    string iNomeImpressora = dsItemsNaoImpresso.Tables[0].Rows[i].Field<string>("NomeImpressora");

                    if (TipoAgrupamentoDelivery == "Por Cozinha/Grupo")
                    {
                        //if (!ProdutosPorCodigo && TipoCodigo != "Personalizado")
                        //{
                        itemsPedido = conexao.SelectRegistroPorCodigo("ItemsPedido", "spObterItemsNaoImpressoPorGrupo", iCodPedido, "", CodGrupo);
                        if (itemsPedido.Tables[0].Rows.Count > 0)
                        {
                            Utils.ImpressaoDelivery_CozinhaPorGrupo(iCodPedido, iNomeImpressora, CodGrupo);
                        }

                    }
                    else
                    {
                        itemsPedido = conexao.SelectItensPorImpressora(iCodPedido, iNomeImpressora);
                        if (itemsPedido.Tables[0].Rows.Count == 0)
                        {
                            return;
                        }
                        Utils.ImpressaoDeliveryCoziha_SeparadoPorImpressora(iCodPedido, iNomeImpressora);
                    }
                    //
                    for (int intfor = 0; intfor < itemsPedido.Tables["ItemsPedido"].Rows.Count; intfor++)
                    {
                        AtualizaItemsImpresso Atualiza = new AtualizaItemsImpresso();
                        Atualiza.CodPedido = iCodPedido;
                        Atualiza.CodProduto = itemsPedido.Tables["ItemsPedido"].Rows[intfor].Field<int>("CodProduto");
                        Atualiza.ImpressoSN = true;
                        conexao.Update("spInformaItemImpresso", Atualiza);
                    }
                    ImpressaoPorCozinha(iCodPedido);
                }


            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel imprimir o Item da Mesa verificar a impressora" + erro.Message);
            }
        }
        public static void ImpressaoBalcaoPorCozinha(int iCodPedido)
        {
            try
            {

                string TipoAgrupamentoDelivery = Utils.RetornaConfiguracaoDelivery().TipoAgrupamento;
                DataSet itemsPedido, dsItemsNaoImpresso;
                DataSet dsI = new DataSet();
                dsItemsNaoImpresso = Utils.CarregaItens(iCodPedido);

                if (dsItemsNaoImpresso.Tables.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < dsItemsNaoImpresso.Tables["ItemsPedido"].Rows.Count; i++)
                {
                    int CodGrupo = dsItemsNaoImpresso.Tables[0].Rows[i].Field<int>("CodGrupo");
                    string iNomeImpressora = dsItemsNaoImpresso.Tables[0].Rows[i].Field<string>("NomeImpressora");

                    if (TipoAgrupamentoDelivery == "Por Cozinha/Grupo")
                    {
                        itemsPedido = conexao.SelectRegistroPorCodigo("ItemsPedido", "spObterItemsNaoImpressoPorGrupo", iCodPedido, "", CodGrupo);
                        if (itemsPedido.Tables[0].Rows.Count > 0)
                        {
                            Utils.ImpressaoBalcao_CozinhaPorGrupo(iCodPedido, iNomeImpressora, CodGrupo);
                        }

                    }
                    else
                    {
                        itemsPedido = conexao.SelectItensPorImpressora(iCodPedido, iNomeImpressora);
                        if (itemsPedido.Tables[0].Rows.Count == 0)
                        {
                            return;
                        }
                        Utils.ImpressaoBalcao_SeparadoPorImpressora(iCodPedido, iNomeImpressora);
                    }
                    //
                    for (int intfor = 0; intfor < itemsPedido.Tables["ItemsPedido"].Rows.Count; intfor++)
                    {
                        AtualizaItemsImpresso Atualiza = new AtualizaItemsImpresso();
                        Atualiza.CodPedido = iCodPedido;
                        Atualiza.CodProduto = itemsPedido.Tables["ItemsPedido"].Rows[intfor].Field<int>("CodProduto");
                        Atualiza.ImpressoSN = true;
                        conexao.Update("spInformaItemImpresso", Atualiza);
                    }
                    ImpressaoPorCozinha(iCodPedido);
                }


            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel imprimir o Item da Mesa verificar a impressora" + erro.Message);
            }
        }
        public static string ImpressaoBalcao(int iCodPedido,
            int iNumCopias = 0, string iNomeImpressora = "")
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

                PrinterSettings printersettings = new PrinterSettings();
                printersettings.PrinterName = iNomeImpressora;
                printersettings.Collate = false;

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
                report.SetParameterValue("@CodEndereco", 0);

                if (iNomeImpressora != "")
                {
                    report.PrintOptions.PrinterName = iNomeImpressora;
                    report.PrintToPrinter(printersettings, new PageSettings(), false);

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

        public static void ImpressaoCaixa(string iTurno, DateTime dtInicio, DateTime dtFim)
        {

            try
            {
                RelFechamentoCaixa report;
                report = new RelFechamentoCaixa();
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;
                string str = Directory.GetCurrentDirectory();
                report.Load(str + @"\RelFechamentoCaixa.rpt");
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

                report.SetParameterValue("@Turno", iTurno);
                report.PrintToPrinter(1, false, 0, 0);



            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro na impressao :" + erro.Message);
            }
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
                //CodCaixa = caixa.CodCaixa,
                CodFormaPagamento = caixa.CodFormaPagamento,
                //Data = caixa.Data,
                Historico = caixa.Historico,
                NumeroDocumento = caixa.NumeroDocumento,
                Tipo = caixa.Tipo,
                Valor = caixa.Valor
            };
            conexao.Insert("spInserirMovimentoCaixa", caixa);
        }
        public static void IniciaSistema()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Splashcreen());
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message + " IniciaSistema");
            }



        }

        public static bool CaixaAberto(DateTime iDataRegistro, int iNumero, string iTurno)
        {
            bool iRetorno = false;
            DataRow dRow;
            DataSet dsCaixa;
            conexao = new Conexao();
            try
            {
                if (conexao.CaixaAbertoAnterior(iTurno).Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("O Caixa do dia anterior ainda esta aberto, favor execute o fechamento", "[xSistemas] Aviso ");
                    frmCaixaFechamento frm = new frmCaixaFechamento();
                    frm.ShowDialog();
                    //CaixaAberto(iDataRegistro, iNumero, iTurno);
                }
                dsCaixa = conexao.RetornaCaixaPorTurno(iNumero, iTurno, DateTime.Parse(iDataRegistro.ToShortDateString()));
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
                                iRetorno = false;
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
                if (erro.Message.ToString().Equals("There is no row at position 0."))
                {
                    MessageBox.Show("Numero de caixa Inexiste , favor verificar");
                    // Environment.Exit(0);

                }
                else
                {
                    MessageBox.Show(erro.Message, "[xSistemas] Algo de errado aconteceu");
                }

            }


            return iRetorno;
        }
        public async static void BuscaPedido(int iCodPedido)
        {
            try
            {
                DataSet ds;
                conexao = new Conexao();
                ds = conexao.SelectRegistroPorCodigo("Pedido", "spObterPedidoFinalizadoPorCodigo", iCodPedido);
                if (ds != null)
                {
                    DataRow dRowPedido = ds.Tables[0].Rows[0];
                    int CodPessoa = int.Parse(dRowPedido.ItemArray.GetValue(2).ToString());
                    string FormaPagamento = dRowPedido.ItemArray.GetValue(5).ToString();
                    string DescPedido = dRowPedido.ItemArray.GetValue(14).ToString();
                    int NumMesa = int.Parse(dRowPedido.ItemArray.GetValue(12).ToString());
                    string strTrocoPara = dRowPedido.ItemArray.GetValue(4).ToString();
                    string strTotalPedido = dRowPedido.ItemArray.GetValue(3).ToString();
                    string strTipoPedido = dRowPedido.ItemArray.GetValue(8).ToString();
                    string strMesa = dRowPedido.ItemArray.GetValue(9).ToString();
                    DateTime dtPedido = Convert.ToDateTime(dRowPedido.ItemArray.GetValue(7).ToString());
                    string strTroco = "0,00";

                    if (strTrocoPara != "0,00" && strTrocoPara != "0")
                    {
                        strTroco = Convert.ToString(decimal.Parse(strTrocoPara) - decimal.Parse(strTotalPedido));
                    }

                    // Retorna a Taxa de Entrega do cadastro do Cliente
                    decimal TaxaEntrega = Utils.RetornaTaxaPorCliente(CodPessoa, 0);
                    frmCadastrarPedido frm = new frmCadastrarPedido(true, DescPedido, NumMesa, strTroco, TaxaEntrega,
                                                     false, dtPedido, iCodPedido, CodPessoa, strTrocoPara, FormaPagamento,
                                                     strTipoPedido, strMesa, decimal.Parse(strTotalPedido));


                    frm.Show();
                    DesabilitaControls(frm);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        //Desativa todos controles da tela 
        private static void DesabilitaControls(Control frm)
        {
            foreach (Control ctrControl in frm.Controls)
            {
                foreach (Control ctl in frm.Controls)
                    if (ctl.Controls.Count > 0)
                        DesabilitaControls(ctl);
                    else
                    {
                        ctl.Enabled = false;
                    }
            }

        }
        public void ClearForm(System.Windows.Forms.Control parent)
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
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.Button)))
                {
                    ((System.Windows.Forms.Button)ctrControl).Enabled = false;
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
                    //Unselect all RadioButtons
                    ((System.Windows.Forms.PictureBox)ctrControl).Image = null;
                }
                if (ctrControl.Controls.Count > 0)
                {
                    //Call itself to get all other controls in other containers
                    ClearForm(ctrControl);
                }
            }
        }
        public static void RepetirUltimoPedido(int iCodCliente, int CodEndereco)
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
                int iCodEndereco = ds.Tables[0].Rows[0].Field<int>("CodEndereco");

                // Retorna a Taxa de Entrega do cadastro do Cliente
                TaxaEntrega = Utils.RetornaTaxaPorCliente(CodPessoa, 0);
                frmCadastrarPedido frmRepetePedido = new frmCadastrarPedido(true, "0,00", 0, "0,00", TaxaEntrega, false,
                                                                            DateTime.Now, CodPedido, CodPessoa,
                                                                            "", FormaPagamento, "", "Balcao", 0.00M, 0, 0, "", iCodEndereco,"",new List<string>());
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
                // conexao.Update("spAlteraStatusMesa", mesas);
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
        /// <summary>
        /// Função para permitir a digitação apenas de Valores numericos no Evento KeypRess
        /// </summary>
        /// <param name="Evento"> Evento da tecla</param>
        /// <returns></returns>
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
        public static void LimpaForm(Control parent)
        {
            foreach (Control ctrControl in parent.Controls)
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
                    //If its a RichTextBox clear the text
                    ((System.Windows.Forms.ComboBox)ctrControl).Text = "";
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

                if (ctrControl.Controls.Count > 0)
                {
                    //Call itself to get all other controls in other containers
                    LimpaForm(ctrControl);
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
                List<ListaSms> newListaSMS = new List<ListaSms>();
                ListaSms listaTransformada;
                for (int i = 0; i < ds.Tables["Pessoa"].Rows.Count; i++)
                {
                    DataRow dRow = ds.Tables["Pessoa"].Rows[i];
                    string iNumero = dRow.ItemArray.GetValue(0).ToString();
                    NomeCliente = dRow.ItemArray.GetValue(1).ToString();

                    listaTransformada = new ListaSms();
                    if (iNumero.Length == 8)
                    {
                        listaTransformada.Numero = "279" + iNumero;
                    }
                    else
                    {
                        listaTransformada.Numero = "27" + iNumero;
                    }
                    listaTransformada.Nome = NomeCliente;
                    newListaSMS.Add(listaTransformada);
                }
                EnviaSMS_LOCASMS EnviarSMS = new EnviaSMS_LOCASMS();
                EnviarSMS.EnviaSMSLista(newListaSMS, iUser, iSenha, iMessagem, iNomeCampanha);
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

            DataSet ListaClientes = conexao.SelectObterClientesSemPedido("spObterClientesSemPedido", DataInicial, DataFinal);
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
                        Valor = -iValor
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
        public static DataSet PopularGridPedido(string iCamposConsulta)
        {
            Conexao con = new Conexao();
            return con.SelectAll("Pedido", "", iCamposConsulta);

            //gridView.DataSource = null;
            //gridView.AutoGenerateColumns = true;
            //gridView.DataSource = Dados;
            //gridView.DataMember = "Pedido";

        }
        public static DataSet PopulaGrid_Novo(string table, DataGridView gridView, string iParametrosConsulta, bool iAtivo = true, string iFiltrosAd = "", int iRowIndex = 0)
        {
            Conexao con = new Conexao();
            DataSet Dados = null;
            Dados = con.SelectMontaGrid(table, iParametrosConsulta, iAtivo, iFiltrosAd);

            if (gridView != null)
            {
                gridView.DataSource = null;
                gridView.AutoGenerateColumns = true;
                gridView.DataSource = Dados;
                gridView.DataMember = table;
            }


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

        public static void PopularGrid(string table, DataGridView gridView, DataSet dsName)
        {
            try
            {
                Conexao con = new Conexao();
                gridView.DataSource = null;
                gridView.AutoGenerateColumns = true;
                gridView.DataSource = dsName;
                gridView.DataMember = table;
                con.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("PopularGrid " + erro.Message);
            }

        }
        /// <summary>
        /// Popula uma grid com resultado de um dataset , e da a opç~]ao de esconder uma coluna
        /// </summary>
        /// <param name="table"></param>
        /// <param name="gridView"></param>
        /// <param name="dsName"></param>
        /// <param name="iNomeColunaEsconder"></param>
        public static void PopularGrid(string table, DataGridView gridView, DataSet dsName,
            string iNomeColunaEsconder)
        {
            try
            {
                Conexao con = new Conexao();
                gridView.DataSource = null;
                gridView.AutoGenerateColumns = true;
                gridView.DataSource = dsName;
                gridView.DataMember = table;
                gridView.Columns[iNomeColunaEsconder].Visible = false;
                gridView.Refresh();
                con.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("PopularGrid " + erro.Message);
            }

        }
        public static void PopularGrid(string table, DataGridView gridView, DataSet dsName,
           List<string> strNomeColunas)
        {
            try
            {

                Conexao con = new Conexao();
                gridView.DataSource = null;
                gridView.AutoGenerateColumns = true;
                gridView.DataSource = dsName;
                gridView.DataMember = table;
                foreach (var item in strNomeColunas)
                {
                    var teste = item.ToString();
                    gridView.Columns[item.ToString()].Visible = false;
                }
              
                gridView.Refresh();
                con.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("PopularGrid " + erro.Message);
            }

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
        public static void CriarUsuario(string iConexao)
        {
            try
            {
                //Conexao.connectionString = iConexao;
                //Conexao conexao = new Conexao();
                SqlConnection conn = new SqlConnection(iConexao);
                conn.Open();
                string iSqlConsulta = "If not Exists (select loginname from master.dbo.syslogins" +
                                      "  where name = 'dex' and dbname = 'master') " +
                                      "  begin " +
                                      " use[master]" +
                                      " create login dex with password = '1234'" +
                                      " grant control server to dex" +
                                      " end";

                SqlCommand command = new SqlCommand(iSqlConsulta, conn);
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                //if (command.ExecuteNonQueryAsync().Result >0)
                //{
                //    MessageBox.Show("Usuario dex criado");
                //}
            }
            catch (Exception e)
            {
                MessageBox.Show(Bibliotecas.cException + e.Message);

            }

        }
        public static string EnderecoMAC()
        {
            try
            {
                return (from nic in NetworkInterface.GetAllNetworkInterfaces()
                        where nic.OperationalStatus == OperationalStatus.Up
                        select nic.GetPhysicalAddress().ToString()
                     ).FirstOrDefault();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message + "EnderecoMAC");
                return "";
            }


        }

        public string Registro()
        {
            string lStrResultado = "";
            Microsoft.Win32.RegistryKey regKey;
            regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\DexSistemas\\", true);
            lStrResultado = regKey.GetValue("Validade").ToString();
            return lStrResultado;
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
                RegistryKey RegistroKey = Registry.LocalMachine.OpenSubKey("Software", false);
                RegistroKey = RegistroKey.CreateSubKey("xSistemas", RegistryKeyPermissionCheck.ReadSubTree);
                RegistroKey.SetValue("xDelivery", lArquivoRegistro);
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
            string strNome = "";
            try
            {
                strNome = System.Net.Dns.GetHostName();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message + " RetornaNomePc");
            }
            return strNome;
        }
        public static bool LeArquivoRegistro()
        {
            string iRetorno, iRegistroCritografado, strDataLimiteRegistro;
            bool OK = false;
            try
            {
                RegistryKey RegistroKey = Registry.LocalMachine.OpenSubKey("Software", true);
                iRetorno = RegistroKey.OpenSubKey("xSistemas", true).GetValue("xDelivery", true).ToString().ToString();
                strDataLimiteRegistro = RegistroKey.OpenSubKey("xSistemas", true).GetValue("Expiracao", true).ToString().ToString();
                iRegistroCritografado = CriptografarArquivo(RetornaNomePc() + Sessions.returnEmpresa.CNPJ + Sessions.returnEmpresa.Cidade + Sessions.returnEmpresa.Nome);
                OK = iRegistroCritografado == iRetorno;
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro de falta de memória , tente executar como administrador :" + erro.Message, "[xSistemas]");
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
            try
            {
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

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message + "ContaRegistro");
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

                MessageBox.Show(Bibliotecas.cException + er.Message);
            }
            return iContraSenha;
        }

        public static string ServicoSQLATIVO()
        {
            string status = "";
            try
            {
                ServiceController MeuServico = new ServiceController("MSSQLSERVER", ListaServidoresSQL());
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

                MessageBox.Show(erro.Message + "FunçãoFree");
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
                iConexao.OpenAsync();
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

                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return mRetornoWS;

        }
        /// <summary>
        ///  Retorna a taxa de entrega do cliente
        /// </summary>
        /// <param name="iCodPessoa">
        /// Código do cliente</param>
        /// <param name="iCodEndereco">
        /// Codigo do Endereco</param>
        /// <returns></returns>
        public static decimal RetornaTaxaPorCliente(int iCodPessoa, int iCodEndereco)
        {
            decimal iRetorno = 0.00M;
            DataTable Regiao;
            if (iCodEndereco == 0)
            {
                Regiao = conexao.SelectRegistroPorCodigo("RegiaoEntrega", "spObterTaxaPorCliente", iCodPessoa).Tables["RegiaoEntrega"];
            }
            else
            {
                Regiao = conexao.SelectRegistroPorCodigo("RegiaoEntrega", "spObterTaxaPorClienteEndereco", iCodEndereco).Tables["RegiaoEntrega"];
            }

            if (Regiao.Rows.Count > 0)
            {
                iRetorno = decimal.Parse(Regiao.Rows[0]["TaxaServico"].ToString());
            }

            return iRetorno;
        }
        /// <summary>
        /// Recebe uma string e remove caracters que não forem numericos
        /// </summary>
        /// <param name="iValue"> string qualquer</param>
        /// <returns>só numero</returns>
        public static string ObterSomenteNumeros(string iValue)
        {
            iValue += ")";
            iValue = iValue.Substring(iValue.IndexOf("(") + 1);
            string ire;
            ire = Regex.Replace(iValue, "[^0-9 ,]+", "");
            return RemoverSpacos(ire);
        }
        public static string RemoverSpacos(string iValue)
        {
            string sem = ""; // Declaramos o futuro resultado
            foreach (char c in iValue.ToCharArray()) // Para cada 'letra' na frase
            {
                if (c != ' ') // Se a letra não for um espaço
                    sem += c; // É adicionada a string final
            }
            return sem;
        }

        public static void ControlaEventos(string iTipoEvento, string LocalEvento, int CodUser = 1)
        {

            try
            {
                EventosSistema eventos = new EventosSistema()
                {
                    CodUsuario = CodUser,
                    TipoEvento = iTipoEvento,
                    DataEvento = DateTime.Now,
                    LocalEvento = LocalEvento,

                };
                conexao.Insert("spAdicionarEvento", eventos);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        public static int RetornaPessoa(int iCodPedido)
        {
            return int.Parse(conexao.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", iCodPedido).Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
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
                config.Save(ConfigurationSaveMode.Full);

                // Force a reload of a changed section.
                //    ConfigurationManager.RefreshSection("appSettings");

            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possível carregar o arquivo de configuração " + erro.Message);
            }

        }

    }

}
