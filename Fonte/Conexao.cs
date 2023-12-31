﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using DexComanda.Models;
using System.IO;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.Management;
using System.Net;
using XIntegrador.Classe.Local;
using Microsoft.Win32;
using DexComanda.Models.WS;

namespace DexComanda
{
    public class Conexao
    {
        private MySqlCommand MysqlCommand;
        private MySqlConnection MysqlConnection;
        private MySqlDataAdapter MysqlDataAdapter;
        private static string dataMember;
        private DataSet ds;
        private DataSet Dados;
        private SqlCommand command;
        private static SqlConnection conn;
        private SqlDataAdapter adapter;
        public string strSqlExecutado;
        private int lastCodigo;
        public static string connectionString = null;
        public ConnectionState statusConexao;
        public Conexao()
        {
            try
            {

                if (connectionString != null)
                {
                    conn = new SqlConnection(connectionString);
                    if (conn.State != ConnectionState.Open )
                    {
                        conn.Open();
                    }
                    statusConexao = conn.State;
                }
            }
            catch (SqlException msg)
            {
                MessageBox.Show("Erro conexao com o SQLSERVER " + msg.Message);
            }

            //finally
            //{
            //    conn.Dispose();
            //}

        }
        public void AlteraStatusPedido(int iCodPedido, int iCodUser, int iCodStatus)
        {
            PedidoStatusMovimento ped = new PedidoStatusMovimento()
            {
                CodPedido = iCodPedido,
                CodStatus = iCodStatus,
                CodUsuario = iCodUser,
                DataAlteracao = DateTime.Now
            };
            Insert("spAdicionarPedidoStatusMovimento", ped);
        }
        public DataSet SelectPedidoPorCodigoiFood(string references)
        {
            string iSql = "select * from Pedido where idiFood=@idIFood ";
            command = new SqlCommand(iSql, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@idIFood", references);

            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, "Pedido");

            return ds;
        }
        public void GravaLatLong(int intCodPessoa, double lat, double longi)
        {
            try
            {
                lock (this)
                {
                    new Conexao();
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    // conn = new SqlConnection();
                    string iSql = "update pessoa set latitude=@Latitude ,longitude=@Longitude where Codigo=" + intCodPessoa;
                    command = new SqlCommand(iSql, conn);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Latitude", lat);
                    command.Parameters.AddWithValue("@Longitude", longi);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            finally
            {
                conn.Close();

            }
        }
        public decimal RetornaDescontoCupom(string iCupom, int intCodPessoa)
        {
            decimal iReturn = 0;
            try
            {
                string iSqlCupom = "select Desconto from Cupom where AtivoSN=1 and CodCupom='" + iCupom + "' and cast(GETDATE() as date) between DataValidade_Inicio and DataValidade_Fim and Quantidade> 0";
                command = new SqlCommand(iSqlCupom, conn);
                command.CommandType = CommandType.Text;
                adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds, "Cupom");
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show("Cupom inválido ou expirado");
                    return 0;
                }
                iReturn = ds.Tables[0].Rows[0].Field<decimal>("Desconto");
            }
            catch (Exception erro)
            {
                MessageBox.Show("RetornaDescontoCupom " + erro.Message);
            }
            return iReturn;
        }

        /// <summary>
        /// Verifica se há uma versão do SQLServer Instalado
        /// </summary>
        /// <returns>
        /// Verdadeiro para sim , False para não</returns>
        public bool SqlServerInstalado()
        {
            bool iReturn = false;
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Microsoft SQL Server Local DB\\Installed Versions");
                if (key != null && key.GetSubKeyNames().Count() > 0)
                {
                    iReturn = true;
                }
                else
                {
                    MessageBox.Show("Não há uma versão do SQLSERVER instalado nessa estacação");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return iReturn;
        }
        /// <summary>
        /// Rotina para efetuar Backup Automátizado do Banco de dados ao Encerrar o Programa
        /// </summary>
        /// <param name="iNomeServidor">
        /// Nome do Servidor de Banco de Dados</param>
        /// <param name="iNomeBanco">
        /// Nome do Banco de Dados </param>
        /// <param name="iLocalBackup">
        /// Local onde será gravado o Backup</param>
        public void BackupBanco(string iNomeServidor, string iNomeBanco, string iLocalBackup)
        {
            string BackupFileName = iNomeBanco + DateTime.Now.ToShortDateString().Replace("/", "") + ".bak";
            string SqlComBackup = @"BACKUP DATABASE " + iNomeBanco + " TO DISK = N'" + iLocalBackup + @"\" + BackupFileName + @"'";
            SqlConnection SqlConec = null;
            SqlCommand cmdBkUp = null;
            try
            {
                SqlConec = new SqlConnection(connectionString);
                cmdBkUp = new SqlCommand(SqlComBackup, SqlConec);

                SqlConec.Open();
                if (SqlConec.State == ConnectionState.Open)
                {
                    try
                    {
                        if (cmdBkUp.ExecuteNonQuery() != 0)
                        {

                            MessageBox.Show("Backup Efetuado em " + iLocalBackup + "\\" + BackupFileName, "Aviso de Segurança");
                        }
                    }
                    catch (Exception erro)
                    {

                        MessageBox.Show("Backup do banco de dados não foi efetuado a causa do possivel erro é:" + erro.Message);
                    }


                }
            }
            finally
            {

                if (SqlConec.State == ConnectionState.Open)
                {
                    SqlConec.Close();
                }
            }


        }
        public void ConsultaInsumos(int intCodProdut, decimal dQtdProduto)
        {
            try
            {
                string strSql = " select * from Produto_Insumo where CodProduto=@CodProduto ";
                command = new SqlCommand(strSql, conn);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@CodProduto", intCodProdut);
                adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds, "Produto_Insumo");
                for (int i = 0; i < ds.Tables["Produto_Insumo"].Rows.Count; i++)
                {
                    BaixarEstoque(ds.Tables["Produto_Insumo"].Rows[i].Field<int>("CodInsumo"), dQtdProduto, ds.Tables[0].Rows[i].Field<decimal>("Quantidade"));
                }
            }
            catch (SqlException erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        private void BaixarEstoque(int intCodInsumo, decimal dcQuantidade, decimal dcQuantidadeBaixar)
        {
            try
            {
                command = new SqlCommand("spBaixaEstoqueInsumo", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CodInsumo", intCodInsumo);
                command.Parameters.AddWithValue("@Quantidade", dcQuantidade);
                command.Parameters.AddWithValue("@QuantidadeBaixar", dcQuantidadeBaixar);
                command.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        public DataSet ContaEstoque(string strNomeProduto)
        {
            try
            {
                command = new SqlCommand("spContaEstoqueAtual", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@NomeProduto", strNomeProduto);
                adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds, "Produto_Estoque");

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return ds;
        }
        public Boolean ControlaEstoque(int intCodProduto)
        {
            try
            {
                command = new SqlCommand("spObterProdutoPorCodigo", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Codigo", intCodProduto);
                command.Parameters.AddWithValue("@AtivoSN", true);
                adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds, "Produto");

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return ds.Tables[0].Rows[0].Field<Boolean>("ControlaEstoque");
        }
        public DataSet ConsultaEnderecoPorCep(int iCodPEssoa, string iCEP)
        {
            command = new SqlCommand("spObterEnderecoPessoaCEP", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CodPessoa", iCodPEssoa);
            command.Parameters.AddWithValue("@Cep", iCEP);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, "Pessoa_Endereco");

            return ds;
        }
        /// <summary>
        /// Retorna iDRegião baseando-se no CEP
        /// </summary>
        /// <param name="strCEP"> cep </param>
        /// <returns></returns>
        public int RetornaCodRegiaoPorBairro(string strBairro)
        {
            int intReturn = 0;
            try
            {
                new Conexao();
                string iSqlConsulta = "select CodRegiao from RegiaoEntrega_Bairros where Nome=@Nome";
                command = new SqlCommand(iSqlConsulta, conn);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Nome", strBairro);
                adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds, "RegiaoEntrega_Bairros");
                if (ds.Tables.Count == 0)
                {
                    return 0;
                }
                return ds.Tables[0].Rows[0].Field<int>("CodRegiao");
            }
            catch (Exception erro)
            {
                return intReturn;
            }

        }
        public Boolean ValidaEstoque(int intCodProduto, string strNomeProd)
        {
            Boolean breturn = true;

            try
            {
                DataSet dsProduto;
                DataSet dsEstoque;
                dsProduto = SelectProdutoCompleto("Produto", "spObterProdutoPorCodigo", intCodProduto);
                dsEstoque = ContaEstoque(strNomeProd);
                decimal estAtual = 0;
                if (dsEstoque.Tables[0].Rows.Count > 0)
                {
                    if (dsProduto.Tables[0].Rows[0].Field<decimal>("EstoqueMinimo") <= dsEstoque.Tables[0].Rows[0].Field<decimal>("EstoqueAtual"))
                    {
                        MessageBox.Show("Estoque minimo para vendas atingido");
                    }
                    if (ContaEstoque(dsProduto.Tables[0].Rows[0].Field<string>("NomeProduto")).Tables[0].Rows[0].Field<decimal>("EstoqueAtual") <= 0)
                    {
                        MessageBox.Show("Produto não tem estoque disponivel");
                        breturn = false;
                    }
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return breturn;
        }
        public async void AtualizaImpressaoBalcao(int intCodPedido)
        {
            new Conexao();
            string iSqlConsulta = " update Pedido set ImpressoSN=1 where Codigo=@Codigo";
            command = new SqlCommand(iSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Codigo", intCodPedido);
            await command.ExecuteNonQueryAsync();

        }
        public DataSet RetornaPedidosOnline(Boolean iOnlineSN, DateTime iDataInicio, DateTime iDataFim)
        {
            string iSqlConsulta = " select P.CodPessoa, P.Codigo , Pe.Nome , P.TotalPedido, P.RealizadoEm," +
                                 " P.CodigoPedidoWS from Pedido P " +
                                 " join Pessoa Pe on Pe.Codigo = P.CodPessoa " +
                                 " where RealizadoEm between @dataInicio and @dataFim ";
            if (iOnlineSN)
            {
                iSqlConsulta = iSqlConsulta + " and CodigoPedidoWS is not null ";
            }

            command = new SqlCommand(iSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@dataInicio", iDataInicio);
            command.Parameters.AddWithValue("@dataFim", iDataFim);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Pedido");
            return ds;

        }

        public Decimal CalculaPrecoInsumo(int intCodProduto)
        {
            decimal iRetur = 0;
            try
            {
                string iSql = "select  " +
                              "  Isnull(sum(I.Preco * P.Quantidade),0) as Soma " +
                              "  from Produto_Insumo P " +
                              "  left join Insumo I on I.Codigo = P.CodInsumo " +
                              " where CodProduto=@CodProduto";

                command = new SqlCommand(iSql, conn);
                command.Parameters.AddWithValue("@CodProduto", intCodProduto);
                command.CommandType = CommandType.Text;
                adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds, "Pedido");
                iRetur = ds.Tables[0].Rows[0].Field<decimal>("Soma");

            }
            catch (SqlException erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return iRetur;
        }
        public DataSet ConsultaPedido(string iCodPedido, DateTime dtInicio, DateTime dtFim)
        {
            string iSql = " select " +
                    " Pd.Codigo, " +
                    " case PD.Tipo " +
                    " when '1 - Mesa'   then 'Mesa' + ' - ' + PD.NumeroMesa " +
                    " when '2 - Balcao' then 'Cliente Balcao ' + PD.Senha + ' ' + Pd.Observacao " +
                    " when '0 - Entrega' then P.Nome " +
                    " end as  'Nome Cliente' " +
                    ",Finalizado,TotalPedido, " +
                    " (select top 1 PS.Nome from PedidoStatusMovimento PSM  join PedidoStatus PS on Status = PSM.CodStatus where PSM.CodPedido = PD.Codigo  order by PSM.DataAlteracao desc ) as 'Situacao Pedido' " +
                    " ,(select Nome from Entregador where Codigo = PD.CodMotoboy) as 'Entregador', " +
                    " (Select Nome from Usuario where Cod = PD.CodUsuario) as 'Atendente' " +
                    " from Pedido Pd " +
                    " join Pessoa P on P.Codigo = Pd.CodPessoa " +
                    " and Pd.RealizadoEM between '" + dtInicio.ToShortDateString() + " 00:00:00" + "'  and  '" + dtFim.ToShortDateString() + " 23:59:59" + "'";

            if (iCodPedido != "")
            {
                iSql = iSql + " and Pd.Codigo=" + iCodPedido;
            }
            command = new SqlCommand(iSql, conn);
            command.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Pedido");
            return ds;
        }
        /// <summary>
        /// Consulta se o pedido esta aberto
        /// </summary>
        /// <param name="iCodPedido">
        /// Codigo do pedido</param>
        /// <returns></returns>
        public DataSet ConsultaPedido(string iCodPedido)
        {
            string iSql = " select " +
                    " Pd.Codigo, " +
                    " case PD.Tipo " +
                    " when '1 - Mesa'   then 'Mesa' + ' - ' + PD.NumeroMesa " +
                    " when '2 - Balcao' then 'Cliente Balcao ' + PD.Senha + ' ' + Pd.Observacao " +
                    " when '0 - Entrega' then P.Nome " +
                    " end as  'Nome Cliente' " +
                    ",Finalizado,TotalPedido, " +
                    " (select top 1 PS.Nome from PedidoStatusMovimento PSM  join PedidoStatus PS on Status = PSM.CodStatus where PSM.CodPedido = PD.Codigo  order by PSM.DataAlteracao desc ) as 'Situacao Pedido' " +
                    " ,(select Nome from Entregador where Codigo = PD.CodMotoboy) as 'Entregador', " +
                    " (Select Nome from Usuario where Cod = PD.CodUsuario) as 'Atendente' " +
                    " from Pedido Pd " +
                    " join Pessoa P on P.Codigo = Pd.CodPessoa " +
                    " where Pd.Codigo=@Codigo and Finalizado=0";


            command = new SqlCommand(iSql, conn);
            command.Parameters.AddWithValue("@Codigo", iCodPedido);
            command.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Pedido");
            return ds;
        }
        /// <summary>
        /// Retornas informações dos produto filtrados
        /// </summary>
        /// <param name="iCod1"></param>
        /// <param name="iCod2"></param>
        /// <param name="iCod3"></param>
        /// <param name="iCod4"></param>
        /// <param name="iCodOpcao"></param>
        /// <param name="iPrecoMar"></param>
        /// <returns></returns>
        public DataSet RetornaMaioresPrecos(int iCod1, int iCod2, string iCod3,
            string iCod4, string iCodOpcao, Boolean iPrecoMar = true)
        {
            string iSqlConsulta = "";
            string sqlWhere = "";
            //if (Utils.MarcaTipoConfiguracaoProduto().TipoCodigo=="Personalizado")
            //{
            //    sqlWhere = " where P.CodigoPersonalizado in (@Cod1, @Cod2, @Cod3, @Cod4) and CodOpcao = @CodOpcao";
            //}
            //else
            //{
            //    sqlWhere = "where CodProduto in (@Cod1,@Cod2,@Cod3,@Cod4) and CodOpcao =@CodOpcao";
            //}

            //Retorna o maior preço entre os produtos
            if (!iPrecoMar)
            {
                iSqlConsulta = "select" +
                                    " max(PO.Preco + P.PrecoProduto) as PrecoOpcao" +
                                    " from" +
                                    " Produto_Opcao PO" +
                                    " left join Produto P on P.Codigo = PO.CodProduto " +
                                    " left join Opcao O on O.Codigo = PO.CodOpcao and O.Tipo = 1" +
                                    " where ( CodProduto in (@Cod1,@Cod2,@Cod3,@Cod4) ) and CodOpcao =@CodOpcao";
            }
            //Retorna o preço médio entre os produtos
            else
            {
                iSqlConsulta = "select " +
                                     " AVG(PO.Preco + P.PrecoProduto) as PrecoOpcao " +
                                     " from " +
                                     " Produto_Opcao PO" +
                                     " left" +
                                     " join Produto P on P.Codigo = PO.CodProduto" +
                                     " left join Opcao O on O.Codigo = PO.CodOpcao and O.Tipo = 1" +
                                     " where ( CodProduto in (@Cod1,@Cod2,@Cod3,@Cod4)) and CodOpcao =@CodOpcao";
            }


            command = new SqlCommand(iSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Cod1", iCod1);
            command.Parameters.AddWithValue("@Cod2", iCod2);
            command.Parameters.AddWithValue("@Cod3", iCod3);
            command.Parameters.AddWithValue("@Cod4", iCod4);
            command.Parameters.AddWithValue("@CodOpcao", iCodOpcao);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Produto_Opcao");
            return ds;

        }

        public DataSet RetornaMaioresPrecosCodPersonalizado(int iCod1, int iCod2, string iCod3,
           string iCod4, string iCodOpcao, Boolean iPrecoMar = true)
        {
            string iSqlConsulta = "";
            string sqlWhere = "";

            //Retorna o maior preço entre os produtos
            if (!iPrecoMar)
            {
                iSqlConsulta = "select" +
                                    " max(PO.Preco + P.PrecoProduto) as PrecoOpcao" +
                                    " from" +
                                    " Produto_Opcao PO" +
                                    " left join Produto P on P.Codigo = PO.CodProduto or P.CodigoPersonalizado=PO.CodProduto" +
                                    " left join Opcao O on O.Codigo = PO.CodOpcao and O.Tipo = 1" +
                                    " where (P.CodigoPersonalizado in (@Cod1,@Cod2,@Cod3,@Cod4) AND p.CodigoPersonalizado>0) and CodOpcao =@CodOpcao";
            }
            //Retorna o preço médio entre os produtos
            else
            {
                iSqlConsulta = "select " +
                                     " AVG(PO.Preco + P.PrecoProduto) as PrecoOpcao " +
                                     " from " +
                                     " Produto_Opcao PO" +
                                     " left" +
                                     " join Produto P on P.Codigo = PO.CodProduto" +
                                     " left join Opcao O on O.Codigo = PO.CodOpcao and O.Tipo = 1" +
                                     " where ( P.CodigoPersonalizado in (@Cod1,@Cod2,@Cod3,@Cod4) AND p.CodigoPersonalizado>0   )and CodOpcao =@CodOpcao";
            }


            command = new SqlCommand(iSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Cod1", iCod1);
            command.Parameters.AddWithValue("@Cod2", iCod2);
            command.Parameters.AddWithValue("@Cod3", iCod3);
            command.Parameters.AddWithValue("@Cod4", iCod4);
            command.Parameters.AddWithValue("@CodOpcao", iCodOpcao);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Produto_Opcao");
            return ds;

        }
        public int BuscaPaiGrupo(int iCodGrupo)
        {
            DataSet dsGrupo;
            int iReturn = 0;
            dsGrupo = SelectRegistroPorCodigo("Grupo", "spObterGrupoPorCodigo", iCodGrupo);
            if (dsGrupo.Tables[0].Rows.Count > 0)
            {
                iReturn = int.Parse(dsGrupo.Tables[0].Rows[0].ItemArray.GetValue(6).ToString());
            }
            return iReturn;
        }

        /// <summary>
        /// Executa insert básico no banco de dados ( Regiãoo de entrega,Forma Pagamento,Caixa,StatusPedido)
        /// </summary>
        public void ExecutaInsertBasico()
        {
            try
            {
                if (!Utils.MessageBoxQuestion("O Banco de dados está vazio , deseja executar a inicialização básica para iniciar as operações"))
                {
                    return;
                }

                // ---- Região Entrega padrão "Balcão" ----
                RegioesEntrega regioes = new RegioesEntrega()
                {
                    NomeRegiao = "Balcão/Retirada",
                    TaxaServico = 0,
                    DataAlteracao = DateTime.Now,
                    OnlineSN = false,
                    AtivoSN = true,
                    valorMinimoFreteGratis = 0,
                    PrevisaoEntrega = "",
                };
                Insert("spAdicionaRegiao", regioes);

                //-----Forma de pagamento padrão Dinheiro ---
                FormasPagamento fp = new FormasPagamento()
                {
                    Codigo = 0,
                    Descricao = "Dinheiro",
                    DescontoSN = false,
                    GeraFinanceiro = false,
                    OnlineSN = false,
                    AtivoSN = true,
                    DataAlteracao = DateTime.Now,
                    CaminhoImagem = ""
                };
                Insert("spAdicionarFormaPagamento", fp);

                //-----Caixa padrão 1 ----
                CaixaCadastros cx = new CaixaCadastros()
                {
                    DataCadastro = DateTime.Now,
                    Nome = "Caixa",
                    Numero = "1"
                };
                Insert("spAdicionarCaixa", cx);

                //----Status Pedido Padrão ----]
                //for (int i = 1; i < 5; i++)
                //{
                PedidoStatus pedS = new PedidoStatus()
                {
                    Nome = "Aberto",
                    AlertarSN = false,
                    Status = 1,
                    EnviarSN = true,
                    // DataAlteracao = DateTime.Now
                };
                Insert("spAdicionarPedidoStatus", pedS);
                PedidoStatus pedS2 = new PedidoStatus()
                {
                    Nome = "CONFIRMADO",
                    AlertarSN = false,
                    Status = 2,
                    EnviarSN = true,
                    // DataAlteracao = DateTime.Now
                };
                Insert("spAdicionarPedidoStatus", pedS2);
                PedidoStatus pedS3 = new PedidoStatus()
                {
                    Nome = "NA COZINHA",
                    AlertarSN = false,
                    Status = 3,
                    EnviarSN = true,
                    // DataAlteracao = DateTime.Now
                };
                Insert("spAdicionarPedidoStatus", pedS3);
                PedidoStatus pedS4 = new PedidoStatus()
                {
                    Nome = "NA ENTREGA",
                    AlertarSN = false,
                    Status = 4,
                    EnviarSN = true,
                    // DataAlteracao = DateTime.Now
                };
                Insert("spAdicionarPedidoStatus", pedS4);

                // ---- Origem Cadastro " ----
                Pessoa_OrigemCadastro pess = new Pessoa_OrigemCadastro()
                {
                    AtivoSN = true,
                    Nome = "Não Respondeu"
                };
                Insert("spAdicionarPessoa_OrigemCadastro", pess);

                // ---- Usuario  " ----
                string _senha = Utils.EncryptMd5("caixa", "1");
                Usuario usuario = new Usuario()
                {
                    Nome = "caixa",
                    Senha = _senha,
                    FinalizaPedidoSN = true,
                    AdministradorSN = true,
                    AbreFechaCaixaSN = true
                };
                Insert("spAdicionarUsuario", usuario);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        public int RetornaIDCategoria(string iNomeCategoria)
        {
            int iIDReturn = 1;
            DataSet dsGrupo = SelectRegistroPorNome("@Nome", "Grupo", "spObterGrupoPorNome", iNomeCategoria);
            if (dsGrupo.Tables[0].Rows.Count > 0)
            {
                DataRow dRowProduto = dsGrupo.Tables[0].Rows[0];
                iIDReturn = int.Parse(dRowProduto.ItemArray.GetValue(0).ToString());
            }

            return iIDReturn;
        }
        public int RetornaGrupoProduto(int iCodProduto)
        {
            string iSqlConsulta = "select CodGrupo from Produto where Codigo=@Codigo";

            command = new SqlCommand(iSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Codigo", iCodProduto);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Produto");
            return int.Parse(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
        }

        public DataSet RetornaCaixaPorTurno(int iCodCaixa, string iTurno, DateTime iData)
        {
            string iSqlConsulta = "select top 1 * from Caixa " +
                                   " where " +
                                   //" Numero=@Numero and " +
                                   "  Turno=@Turno " +
                                   //" and Data=@Data " +
                                   " and Estado=0" +
                                   // " HorarioFechamento>@HorarioFechamento" +
                                   " order by Codigo desc";
            command = new SqlCommand(iSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Turno", iTurno);
            // command.Parameters.AddWithValue("@Data", DateTime.Now.ToShortDateString());
            // command.Parameters.AddWithValue("@HorarioFechamento", DateTime.Now);

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Caixa");
            return ds;
        }
        public DataSet CaixaAbertoAnterior(string iTurno)
        {
            new Conexao();
            command = new SqlCommand("spCaixaAbertoAnterior", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Turno", iTurno);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Caixa");
            return ds;
        }
        /// <summary>
        /// Lista os Bairros baseando-se nas cidades atendidas ( Configuração > Config Gerais > Cidades Atendidas)
        /// </summary>
        /// <returns>
        /// Lista de bairros </returns>
        public DataSet ListaBairro(string iNomeBairro)
        {
            List<CidadesAtendidas> cidades = new List<CidadesAtendidas>();
            cidades = Utils.DeserializaObjeto2(Sessions.returnConfig.CidadesAtendidas);
            List<string> listCidades = new List<string>();
            foreach (var item in cidades)
            {
                listCidades.Add("'" + item.Cidade + "'");
            }

            string iSqlCidades = string.Join(",", listCidades);
            string iSql = "select DISTINCT(bairro) from base_cep where cidade in (" + iSqlCidades + ") and bairro like '%" + iNomeBairro + "%'";
            command = new SqlCommand(iSql, conn);
            command.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "base_cep");
            return ds;

        }
        public DataSet RetornarTaxaPorBairro(string iNOmeBairro)
        {
            string lSqlConsulta = " select R.Codigo , " +
                                  " R.TaxaServico , " +
                                  " R.NomeRegiao ," +
                                  " RG.Nome" +
                                  " from RegiaoEntrega R " +
                                  " left join RegiaoEntrega_Bairros RG on RG.CodRegiao = R.Codigo " +
                                  "  where RG.Nome like '%" + iNOmeBairro + "%'";

            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "RegiaoEntrega");
            return ds;

        }

        //public void AdicionaPontosFidelidade (int intCodPedido,int intCodUsuario)
        //{
        //    try
        //    {
        //        DataSet dsItensPedido = SelectRegistroPorCodigo("Produto", "spObterItensPedidoPonto", intCodPedido);
        //        for (int i = 0; i < dsItensPedido.Tables[0].Rows.Count; i++)
        //        {
        //            PessoaFidelidade pess = new PessoaFidelidade()
        //            {
        //                CodPedido = intCodPedido,
        //                CodPessoa = dsItensPedido.Tables[0].Rows[i].Field<int>("CodPessoa"),
        //                CodProduto = dsItensPedido.Tables[0].Rows[i].Field<int>("Codigo"),
        //                CodUsuario = intCodUsuario,
        //                Pontos = dsItensPedido.Tables[0].Rows[i].Field<int>("PontoFidelidadeVenda")
        //            };
        //            Insert("spAdicionarPontosFidelidade", pess);
        //        }
        //    }
        //    catch (Exception erro)
        //    {
        //        MessageBox.Show(erro.Message);
        //    }
        //}
        public DataSet RetornaCEPPorBairro(string iNOmeBairro)
        {
            string lSqlConsulta = " select top 1 cep,bairro from base_cep " +
                                  " where bairro like '%" + iNOmeBairro + "%'" +
                                  " and cidade ='" + Sessions.returnEmpresa.Cidade + "'";

            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "base_cep");
            return ds;

        }

        protected bool ConsultaPedidoExiste(int iCodPedido, int iCodStatus)
        {
            Boolean iReturn = false;
            string lSqlConsulta = "select * from PedidoStatusMovimento " +
                                  " where CodPedido=@CodPedido and CodStatus=@CodStatus";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@CodPedido", iCodPedido);
            command.Parameters.AddWithValue("@CodStatus", iCodStatus);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "PedidoStatusMovimento");
            return iReturn = ds.Tables[0].Rows.Count > 0;
        }
        public void AtualizaSituacao(int iCodPedido, int iCodUser, int iCodStatus, DataGridView grid = null)
        {
            try
            {
                PedidoStatusMovimento ped;

                if (ConsultaPedidoExiste(iCodPedido, iCodStatus))
                {
                    ped = new PedidoStatusMovimento()
                    {
                        CodPedido = iCodPedido,
                        CodStatus = iCodStatus,
                        CodUsuario = iCodUser,
                        DataAlteracao = DateTime.Now
                    };
                    Update("spAlteraPedidoStatusMovimento", ped);
                }
                else
                {
                    ped = new PedidoStatusMovimento()
                    {
                        CodPedido = iCodPedido,
                        CodStatus = iCodStatus,
                        CodUsuario = iCodUser,
                        DataAlteracao = DateTime.Now
                    };
                    Insert("spAdicionarPedidoStatusMovimento", ped);
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel alterar o statuso do Pedido" + erro.Message);
            }

            Utils.PopulaGrid_Novo("Pedido", grid, Sessions.SqlPedido);
        }
        /// <summary>
        /// Exclusivo para Aja Aja
        /// </summary>
        /// <param name="iGrupoProduto"></param>
        /// <param name="iCodProduto"></param>
        /// <returns></returns>
        public decimal RetornaPrecoComEmbalagem(string iGrupoProduto, int iCodProduto)
        {
            decimal iReturnPreco = 0;
            //string lSqlConsulta = " select * from Produto where GrupoProduto=@GrupoProduto";

            string Grupo1 = "ENTRADAS;PRATOS ESPECIAIS; Sushis Especiais;Yakissoba;SASHIMI ;SOBREMESA;SUSHI ESPECIAIS;TEMAKI;SUSHI MIX;" +
                           "HOSSOMAKI ;HOSSO MAKI HOT;URAMAKI ;FUTUMAKI ; OKONOMIS (NIGUIRI); Temaki; SUSHI JO; Sobremesa;YAKISSOBA ";
            Grupo1 = Grupo1.ToUpper();
            //////////////////////////////////////
            string Grupo2 = "TEPPAN YAKI";
            int[] CodProdutos = new int[12];
            CodProdutos[0] = 96;
            // CodProdutos[1] = 97;
            CodProdutos[4] = 142;
            CodProdutos[5] = 143;
            CodProdutos[6] = 102;
            CodProdutos[7] = 103;
            CodProdutos[8] = 142;
            CodProdutos[9] = 143;
            int[] CodProduto1 = new int[2];
            CodProduto1[0] = 105;

            int[] CodProduto2 = new int[2];
            CodProduto2[0] = 106;

            int[] CodProduto3 = new int[6];
            CodProduto3[0] = 98;
            CodProduto3[1] = 99;
            CodProduto3[2] = 100;
            CodProduto3[3] = 101;
            // CodProduto3[5] = 103;
            // CodProduto3[5] = 98;
            if (iCodProduto == 97)
            {
                iReturnPreco = 4.0M;
            }
            else if (CodProdutos.Contains(iCodProduto))
            {
                iReturnPreco = 2.50M;
            }
            else if (Grupo1.Contains(iGrupoProduto) || CodProduto3.Contains(iCodProduto))
            {
                iReturnPreco = 1.50M;
            }
            else if (Grupo2.Contains(iGrupoProduto) || CodProdutos.Contains(iCodProduto))
            {

                iReturnPreco = 2.50M;
            }
            else if (CodProduto1.Contains(iCodProduto))
            {
                iReturnPreco = 6.50M;
            }
            else if (CodProduto2.Contains(iCodProduto))
            {
                iReturnPreco = 7.50M;
            }
            else if (iCodProduto == 104 || iCodProduto == 97)
            {
                iReturnPreco = 4.0M;
            }

            return iReturnPreco;
        }
        /// <summary>
        /// Cria uma conexão com banco de dados e caso consiga ele cria o arquivo ConnectionString na pasa do sistema
        /// </summary>
        /// <param name="servidor">Nome do Servidor ou Servidor/Instancia</param>
        /// <param name="banco">Nome do banco de dados instanciado</param>
        public bool OpenConection(string servidor, string banco)
        {
            connectionString = @"Data Source=" + servidor + ";Initial Catalog=" + banco + "; User ID=dex; Password=1234; Trusted_Connection=False; MultipleActiveResultSets=True;";
            conn = new SqlConnection(connectionString);
            try
            {
                //Utils.CriarUsuario(connectionString);
                conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    MessageBox.Show("Não foi possivel conectar ao Banco:" + banco + Bibliotecas.cException);
                    return false;
                }
                MessageBox.Show("Conectado ao banco de dados " + banco);

                var temp = Directory.GetCurrentDirectory() + @"\ConnectionString_DexComanda.txt";

                if (!System.IO.File.Exists(temp))
                {
                    System.IO.File.Create(temp).Close();
                    System.IO.File.AppendAllText(temp, connectionString);
                }
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message + " Não pode ser aberta um conexão com o banco de dados.");
            }

            return conn.State == ConnectionState.Open;
        }

        public async void Close()
        {
           // conn.Close();
        }

        public void SalvarAdicionais(int iCodProduto, int iCodOpcao, decimal iPreco, int iCodTipo)
        {
            Produto_Opcao prod = new Produto_Opcao()
            {
                CodProduto = iCodProduto,
                CodOpcao = iCodOpcao,//int.Parse(AdicionaisGridView.Rows[i].Cells["CodOpcao"].Value.ToString()),
                Preco = iPreco,//decimal.Parse(AdicionaisGridView.Rows[i].Cells["Preco"].Value.ToString()),
                DataAlteracao = DateTime.Now,
                CodTipo = iCodTipo
            };
            Insert("spAdicionarOpcaProduto", prod);

        }


        public DataSet SelectAll(string table, string spName = "", string iSqlSelect = "")
        {
            try
            {
             //   new Conexao();
                command = new SqlCommand(spName, conn);
                if (iSqlSelect != "")
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = iSqlSelect;
                }
                else
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                if (conn.State != ConnectionState.Open)
                {
                    SelectAll(table, spName, iSqlSelect);
                }
                adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds, table);
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

            return ds;
        }
        public DataSet RetornaOpcoesProduto(int iDProduto)
        {
            string lSqlConsulta = " select Op.Nome, " +
                                  " PoT.Tipo," +
                                  " ISNULL((select MaximoAdicionais from Produto P where P.Codigo=Prod.CodProduto  ),0) as MaximoAdicionais," +
                                  " PoT.Nome as NomeTipo, " +
                                  " P.PrecoProduto as PrecoProduto,  " +
                                  " Prod.Preco as PrecoSoOpcao,  " +
                                  " Prod.CodOpcao, " +
                                  " Pot.Nome as NomeTipo," +
                                  " P.DiaSemana as DiaSemana" +
                                  " from Produto_Opcao Prod" +
                                  " join Opcao Op  on Op.Codigo = Prod.CodOpcao" +
                                  " join Produto_OpcaoTipo PoT on PoT.Codigo = Op.Tipo" +
                                 "  join Produto P on P.Codigo = Prod.CodProduto" +
                                  "  where Prod.CodProduto = @CodProduto and Op.AtivoSN=1" +
                                  " order by PoT.OrdenExibicao asc";
            //" order by PoT.OrdenExibicao,Op.Nome";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@CodProduto", iDProduto);


            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Produto_Opcao");
            return ds;
        }
        public DataSet RetornaOpcoesCodPersonalizado(int iDProduto)
        {
            string lSqlConsulta = " select " +
                                  " Op.Nome,  PoT.Tipo," +
                                  " Prod.Preco as Preco, " +
                                  " P.PrecoProduto as PrecoProduto,  " +
                                 " ISNULL((select MaximoAdicionais from Produto P where P.Codigo = Prod.CodProduto  ),0) as MaximoAdicionais," +
                                 " PoT.Nome as NomeTipo, " +
                                  " Prod.CodOpcao, " +
                                 " Prod.Preco as PrecoSoOpcao , " +
                                 " Pot.Nome as NomeTipo" +
                                 "  from Produto_Opcao Prod join Opcao Op on Op.Codigo = Prod.CodOpcao" +
                                 "  join Produto_OpcaoTipo PoT on PoT.Codigo = Op.Tipo" +
                                  " join Produto P on P.CodigoPersonalizado = Prod.CodProduto" +
                                 " where P.CodigoPersonalizado = @Codigo order by PoT.OrdenExibicao,Op.Nome";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Codigo", iDProduto);


            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Produto_Opcao");
            return ds;
        }
        public DataSet RetornaOpcoesRadio(int iDProduto)
        {
            string lSqlConsulta = " select Op.Nome, " +
                                  " PoT.Tipo," +
                                  " Prod.Preco," +
                                  " ISNULL((select MaximoAdicionais from Produto P where P.Codigo=Prod.CodProduto  ),0) as MaximoAdicionais," +
                                  " PoT.Nome as NomeTipo, " +
                                  " Prod.CodOpcao " +
                                  " from Produto_Opcao Prod" +
                                  " join Opcao Op  on Op.Codigo = Prod.CodOpcao" +
                                  " join Produto_OpcaoTipo PoT on PoT.Codigo = Op.Tipo and Pot.Tipo=1" +
                                  "  where Prod.CodProduto = @CodProduto" +
                                  " order by Prod.Preco";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@CodProduto", iDProduto);


            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Produto_Opcao");
            return ds;
        }
        public DataSet RetornaOpcoesProduto2(int iDProduto)
        {
            string lSqlConsulta = " select Op.Nome, " +
                                  " PoT.Tipo," +
                                  " Prod.Preco," +
                                  " ISNULL((select MaximoAdicionais from Produto P where P.Codigo=Prod.CodProduto  ),0) as MaximoAdicionais," +
                                  " PoT.Nome as NomeTipo, " +
                                  " Prod.CodOpcao " +
                                  " from Produto_Opcao Prod" +
                                  " join Opcao Op  on Op.Codigo = Prod.CodOpcao" +
                                  " join Produto_OpcaoTipo PoT on PoT.Codigo = Op.Tipo and Op.Tipo=1" +
                                  "  where Prod.CodProduto = @CodProduto" +
                                  " order by Prod.Preco";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@CodProduto", iDProduto);


            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Produto_Opcao");
            return ds;
        }

        public DataSet RetornaOpcoesPortipo(int iCodTipo)
        {
            command = new SqlCommand("spObterOpcaoPorTipo", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Codigo", iCodTipo);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Opcao");
            return ds;
        }
        public decimal ConsultaEstoqueProduto(string strNomeProduto)
        {
            decimal qtdEstoque = 0;
            try
            {

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return qtdEstoque;
        }
        public DataSet RetornaOpcoes(int iIDOpcao)
        {
            string lSqlConsulta = " select " +
                                  " (select NomeProduto from  Produto where Produto.Codigo=Prod.CodProduto) as 'Nome do Produto'," +
                                  " Op.Codigo , " +
                                  " Op.Nome as 'Nome Opcao', " +
                                  " Prod.Preco " +
                                  " from Produto_Opcao Prod" +
                                  " join Opcao Op  on Op.Codigo = Prod.CodOpcao" +
                                  " join Produto_OpcaoTipo PoT on PoT.Codigo = Op.Tipo" +
                                  " where Prod.CodOpcao = @CodOpcao" +
                                  " order by PoT.Nome";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@CodOpcao", iIDOpcao);


            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Produto_Opcao");
            return ds;
        }


        public DataSet RetornaTipoOpcao()
        {
            string lSqlConsulta = " select * from Produto_OpcaoTipo ";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Produto_OpcaoTipo");
            return ds;
        }
        public DataSet RetornaDadosPessoa(string iNomeBairros, int iIDRegiao)
        {
            string lSqlConsulta = " select Codigo,Nome,Cidade,Bairro,Telefone,CodRegiao from Pessoa where teste ";
            if (iNomeBairros != "")
            {
                lSqlConsulta = lSqlConsulta.Replace("teste", " bairro like '%" + iNomeBairros + "%'");
                //   command.Parameters.AddWithValue("@Bairro", iNomeBairros);
            }
            else if (iIDRegiao != 0)
            {
                lSqlConsulta = lSqlConsulta.Replace("teste", "CodRegiao=" + iIDRegiao + "");
                //  command.Parameters.AddWithValue("@CodRegiao", iIDRegiao);
            }
            else if (iNomeBairros != "" && iIDRegiao != 0)
            {
                lSqlConsulta = lSqlConsulta.Replace("teste", " bairro like '%@Bairro% and CodRegiao=@CodRegiao ");
                //   lSqlConsulta = lSqlConsulta.Replace("teste", "CodRegiao=" + iIDRegiao + "");
            }
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Pessoa");
            return ds;
        }
        public DataSet SelectFormasPagamento()
        {
            string lSqlConsulta = " select Codigo,Descricao from FormaPagamento where AtivoSn=1";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "FormaPagamento");
            return ds;
        }
        public DataSet SelectAdicionalLanche()
        {
            // string lSqlConsulta = " select * from Opcao  where Tipo='Texto Livre'  ";
            //   "  where Tipo = 'Texto livre'";
            string lSqlConsulta = "select  p.CodOpcao,P.Preco from Produto_Opcao P " +
                                  "  left join Opcao O on O.Codigo = P.CodOpcao " +
                                  "  where O.Tipo = 'Multipla Selecao' ";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Produto_Opcao");
            return ds;
        }
        public DataSet SelectLanches()
        {
            string lSqlConsulta = "select * from Produto where GrupoProduto='LANCHES' AND CODIGO >1 ";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Produto");
            return ds;
        }


        public DataSet RetornaListaPessoasSMS(DateTime iData1, DateTime iData2, Boolean iAniversariante, Boolean iSemPedido, Boolean iTodos)
        {
            try
            {
                if (iSemPedido)
                {
                    ds = SelectObterClientesSemPedido("spObterClientesSemPedido",
                       iData1,
                      iData1);
                }
                else if (iAniversariante)
                {
                    ds = SelectObterAniversariantes("spObterAnivesariantes",
                       iData1,
                       iData2);
                }
                //else if (iTodos)
                //{
                //    lSqlConsulta = "select Telefone,Nome from Pessoa P" +
                //                   " where  " +
                //                   " (SUBSTRING(Telefone,0,2) ='9' " +
                //                   " or SUBSTRING(Telefone,0,2) ='8'  " +
                //                   " and DATALENGTH(Telefone) >=8) ";
                //}
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return ds;
        }

        public DataSet SelectRegistroPorCodigoPeriodo(string table, string spName, string iCodPessoa, DateTime iDataI, DateTime iDataF)
        {
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            if (spName == "spTotaisCaixa")
            {
                command.Parameters.AddWithValue("@Numero", iCodPessoa);
                command.Parameters.AddWithValue("@Data", iDataI);
            }
            else if (spName == "spObterProdutoPorClientePush")
            {
                command.Parameters.AddWithValue("@DataInicio", iDataI);
                command.Parameters.AddWithValue("@DataFim", iDataF);
                command.Parameters.AddWithValue("@CodGrupo", iCodPessoa);
            }
            else
            {
                command.Parameters.AddWithValue("@CodPessoa", iCodPessoa);
                command.Parameters.AddWithValue("@DataInicio", iDataI);
                command.Parameters.AddWithValue("@DataFim", iDataF);
            }

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, table);
            return ds;
        }
        public DataSet SelectCaixaFechamento(string iDataI, string iDataF, string iTurno, string table = "CaixaMovimento")
        {
            string lSqlConsulta = " select " +
                                //" case Tipo " +
                                //" when 'E' then 'Entradas'" +
                                //" when 'S' then 'Saidas'" +
                                //" end" +
                                //" as 'Tipo Movimento', " +
                                //" Cx.CodCaixa," +
                                " Fp.Descricao ," +
                                " sum(cx.Valor) as 'Total Somado'" +
                                " from CaixaMovimento CX" +
                                " left join FormaPagamento FP on FP.Codigo = Cx.CodFormaPagamento" +
                                " where " +
                                " CX.CodCaixa = (select Codigo from Caixa where Turno=@Turno and Estado=0) " +
                               //" AND CX.Data BETWEEN @DataI  AND @DataF " +
                               " group by CodCaixa,Fp.Descricao";
            // " group by CodCaixa,Fp.Descricao,Tipo";


            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Turno", iTurno);
            // command.Parameters.AddWithValue("@DataI", iDataI);
            // command.Parameters.AddWithValue("@DataF", iDataF);

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, table);
            return ds;
        }

        public DataSet SelectRegistroONline(string iNomeTable)
        {

            string lSqlConsulta;
            if (iNomeTable == "Produto")
            {
                lSqlConsulta = " select P.*,ISNULL( G.MultiploSabores,0) as MultiploSabores from Produto P join Grupo G on G.Codigo=P.CodGrupo where P.DataAlteracao>P.DataSincronismo or P.DataSincronismo is null";

            }
            else
            {
                lSqlConsulta = " select * from " + iNomeTable + " where DataAlteracao>DataSincronismo or DataSincronismo is null ";
            }


            if (iNomeTable == "Grupo")
            {
                lSqlConsulta = lSqlConsulta + "order by PaiSN desc";
            }
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, iNomeTable);
            return ds;

        }
        public DataSet Select_Empresa_HorarioEntrega(string iNomeTable)
        {
            string lSqlConsulta = "";
            lSqlConsulta = " select * from " + iNomeTable;
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, iNomeTable);
            return ds;

        }
        /// <summary>
        /// Busca produtos marcaddos como online
        /// </summary>
        /// <param name="iNomeTable"></param>
        /// <returns></returns>
        public DataSet SelectRegistroONlineSemData(string iNomeTable)
        {

            string lSqlConsulta = " select * from " + iNomeTable + " where OnlineSN=1 and AtivoSN=1";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, iNomeTable);
            return ds;

        }

        public DataSet RetornaRegiao()
        {
            string lSqlConsulta = " select RG.Codigo, RG.NomeRegiao,RG.TaxaServico" +
                                  "  ,RB.CEP ,Isnull(RB.OnlineSN,0) as OnlineSN , " +
                                  "  Isnull(RG.valorMinimoFreteGratis,0) as valorMinimoFreteGratis," +
                                  " IsNull(RG.PrevisaoEntrega,0) as PrevisaoEntrega" +
                                  "   from RegiaoEntrega RG " +
                                  "  join RegiaoEntrega_Bairros RB on RB.CodRegiao = RG.Codigo" +
                                  "  WHERE (RG.DataAlteracao > RG.DataSincronismo or RG.DataSincronismo is null ) ";
            // " and RG.ONLINESN=1 ";

            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "RegiaoEntrega");
            return ds;

        }
        public DataSet RetornaCEPporBairro(string iCEP, Boolean iCidadePadrao = false)
        {
            string lSqlConsulta = "select cep,bairro cep from base_cep where cep=" + iCEP;
            if (iCidadePadrao)
            {
                lSqlConsulta = lSqlConsulta + " and cidade='" + Sessions.returnEmpresa.Cidade + "'";
            }
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "base_cep");
            return ds;

        }

        //public DataSet SelectObterOpcaoProduto()
        //{
        //    string lSqlConsulta = "";

        //    command = new SqlCommand(lSqlConsulta, conn);
        //    command.CommandType = CommandType.Text;

        //    adapter = new SqlDataAdapter(command);
        //    ds = new DataSet();
        //    adapter.Fill(ds, "Produto_Opcao");
        //    return ds;

        //}
        public void AtualizaProdutosOpcao(int iCodOpcao)
        {
            string lSqlConsulta;
            lSqlConsulta = "Update Produto set DataAlteracao=GetDate() " +
                           "where Codigo in (Select CodProduto from Produto_Opcao " +
                           "where CodOpcao=" + iCodOpcao.ToString() + ")";
            // and AtivoSN=1";

            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();

        }


        public void AtualizaDataSincronismo(string iNomeTable, int iCodigo, string iDataAtualizar = "DataSincronismo", string NomeCod = "Codigo")
        {
            string lSqlConsulta;
            new Conexao();
            if (iCodigo != -1)
            {
                lSqlConsulta = " update " + iNomeTable + " set " + iDataAtualizar + "=GetDate() where " + NomeCod + "= " + iCodigo;
            }
            else
            {
                lSqlConsulta = " update " + iNomeTable + " set " + iDataAtualizar + "=GetDate()";
            }

            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();

        }
        public void AtualizaDataSincronismo(string iNomeTable, int iCodProduto, int iCodOpcao, string iDataAtualizar = "DataSincronismo", string NomeCod = "Codigo")
        {
            string lSqlConsulta = " update " + iNomeTable + " set " + iDataAtualizar + "=GetDate() where CodProduto=" + iCodProduto + " and CodOpcao=" + iCodOpcao;// and AtivoSN=1";

            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();

        }
        public DataSet SelectEntregaPorBoy(string iDataI, string iDataF, int CodMotoboy, string table = "Entregador")
        {
            string lSqlConsulta = "  select  " +
                                    " count(P.CodMotoboy) as QuantidadeEntregas," +
                                    " cast(P.RealizadoEm as date) as RealizadoEm," +
                                    " (select Codigo from Entregador E where P.CodMotoboy = E.Codigo) as CodMotoboy, " +
                                    " (select Nome from Entregador E where P.CodMotoboy = E.Codigo) as Nome," +
                                    " (select NomeRegiao from RegiaoEntrega R where R.Codigo = Pes.CodRegiao) as Regiao" +
                                    " from " +
                                    " Pedido P " +
                                    " left join Pessoa Pes on Pes.Codigo = P.CodPessoa " +
                                    " where P.Finalizado =1  " +
                                    " and  P.RealizadoEm between'" + iDataI.ToString() + "' and '" + iDataF.ToString() + "'";
            if (CodMotoboy != 0)
            {
                lSqlConsulta = lSqlConsulta + " and P.CodMotoboy=" + CodMotoboy;
            }
            lSqlConsulta = lSqlConsulta + "group by P.CodMotoboy,cast(P.RealizadoEm as date)";



            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, table);
            return ds;
        }
        public DataSet SelectCaixaMovimetoFiltro(string iDataI, string iDataF, string iTipo, string iCdFormaPagt, string table = "CaixaMovimento", string iNumCaixa = "1", string iTurno = "Dia")
        {
            string lSqlConsulta = " select " +
                                    " CX.Numero as 'Numero Caixa'," +
                                    " CXM.DATA, " +
                                    " CXM.Historico, " +
                                    " CXM.NumeroDocumento, " +
                                    " FP.Descricao AS 'FORMA PAGAMENTO'," +
                                    " CXM.Valor, " +
                                    " case  CXM.Tipo" +
                                    " when 'E' THEN 'Entrada'" +
                                    " when 'S' then 'Saida' " +
                                    " end  " +
                                    " as  " +
                                    " 'Tipo Movimento' " +
                                    " from CaixaMovimento  CXM " +
                                    " LEFT JOIN FormaPagamento FP ON FP.Codigo = CXM.CodFormaPagamento " +
                                    " LEFT JOIN Caixa          CX ON CX.Codigo = CXM.CodCaixa" +

                                " where " +
                                " CXM.Turno ='" + iTurno + "'" +
                                " and CXM.Data BETWEEN  '" + iDataI.ToString() + "' AND '" + iDataF.ToString() + "'";


            if (iNumCaixa != "")
            {
                lSqlConsulta = lSqlConsulta + " and CXM.CodCaixa  = '" + iNumCaixa + "'";
            }
            if (iCdFormaPagt != "")
            {
                lSqlConsulta = lSqlConsulta + " and  CXM.CodFormaPagamento = '" + iCdFormaPagt + " '";
            }
            if (iTipo != "ES")
            {
                lSqlConsulta = lSqlConsulta + " and  CXM.Tipo ='" + iTipo + "'";
            }

            //      lSqlConsulta = lSqlConsulta + " and CX.Turno ='" + iTurno + "'";

            strSqlExecutado = lSqlConsulta;
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, table);
            return ds;
        }
        public DataSet DeleteAll(string table, string spName, int CodigoDeletar)
        {
            new Conexao();
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Codigo", CodigoDeletar);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }
        /// <summary>
        /// Reabri o pedido mudando seu status para aberto
        /// </summary>
        /// <param name="intCodPedido"> Código do Pedido que deseja reabrir</param>
        public void ReabrirPedido(int intCodPedido)
        {
            try
            {
                new Conexao();
                command = new SqlCommand("spReabrirPedido", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CodPedido", intCodPedido);
                if (command.ExecuteNonQuery() > 0)
                    MessageBox.Show("Pedido " + intCodPedido.ToString() + " reaberto com sucesso ");

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }

        public DataSet Delete(string table, string spName, int CodProduto, int CodOpcao)
        {
            new Conexao();
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CodProduto", CodProduto);
            command.Parameters.AddWithValue("@CodOpcao", CodOpcao);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }
        public DataSet DeleteBairroRegiao(string itable, string ispName, int iCodRegiao, string iCEP)
        {
            command = new SqlCommand(ispName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CodRegiao", iCodRegiao);
            command.Parameters.AddWithValue("@CEP", iCEP);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, itable);

            return ds;
        }
        public DataSet LimpaTabela(string table, string spName)
        {
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }
        public DataSet SelectObterUltimoPedido(int iCodCliente)
        {
            command = new SqlCommand("sbObterUltimoPedido", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CodPessoa", iCodCliente);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Pedido");

            return ds;

        }

        public DataSet SelectPuro(string iTable)
        {
            string iSql = "select * from " + iTable;
            command = new SqlCommand(iSql, conn);
            command.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, iTable);
            return ds;
        }
        public string MontaGridPedido(int statusPedido)
        {
            string iSql = "";
            try
            {

                //  string sqlStatusPedidoComFiltro = "(select PS.Nome from PedidoStatusMovimento PSM join  PedidoStatus PS on Status = PSM.CodStatus where PSM.CodPedido="+statusPedido.ToString()+") as 'Situacao Pedido'";
                //string sqlStatusPedido = "(select top 1 PS.Nome from PedidoStatusMovimento PSM join  PedidoStatus PS on Status = PSM.CodStatus where PSM.CodPedido=PD.Codigo order by PSM.DataAlteracao desc) as 'Situacao Pedido'";
                iSql = "select " + Sessions.SqlPedido + " from Pedido ";

                iSql = iSql + " Pd join Pessoa P on P.Codigo=Pd.CodPessoa join PedidoStatusMovimento PSM on PSM.CodPedido=PD.Codigo ";
                iSql = iSql + " where Finalizado = 0 and PD.status ='Aberto' and PSM.CodStatus=" + statusPedido.ToString() + " ORDER BY Pd.Codigo DESC";

            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possivel montar a grid " + erro.Message);
            }

            return iSql;
        }
        public DataSet SelectMontaGrid(string iTable, string iParametrosConsulta, Boolean iAtivos = true, string iFiltrosAdicionais = "")
        {
            try
            {
                string iSql = "", iSubSelect = "subSelect";
                if (iParametrosConsulta != null)
                {
                    iSql = "select " + iParametrosConsulta + iSubSelect + " from " + iTable;

                    if (iTable == "Pedido")
                    {
                        iSql = iSql + " Pd join Pessoa P on P.Codigo=Pd.CodPessoa";
                        iSql = iSql.Replace(iSubSelect, "") + " where Finalizado = 0 and PD.status ='Aberto'" + iFiltrosAdicionais + " ORDER BY Pd.Codigo DESC";
                    }
                    else
                    if (iTable == "Produto")
                    {
                        if (Sessions.returnEmpresa.CNPJ == Bibliotecas.cNUTRIFOOD) //Nutrifood
                        {
                            iSql = iSql.Replace(iSubSelect, ",(select top 1 Quantidade from Produto_Estoque E where E.CodProduto = Produto.Codigo and E.DataAtualizacao between '" + DateTime.Now.Date.ToShortDateString() + " 00:00:00" + "' and '" + DateTime.Now.Date.ToShortDateString() + " 23:59:59') as QtdVendida");
                        }
                        if (iAtivos)
                        {
                            iSql = iSql.Replace(iSubSelect, "") + " where AtivoSN=1" + iFiltrosAdicionais;
                        }
                        else
                        {
                            iSql = iSql.Replace(iSubSelect, "") + " where AtivoSN=0" + iFiltrosAdicionais;
                        }

                    }
                    else
                    {
                        iSql = iSql.Replace(iSubSelect, "");
                    }

                }
                else
                {
                    iSql = "select * from " + iTable;
                }


                command = new SqlCommand(iSql, conn);
                command.CommandType = CommandType.Text;
                adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds, iTable);
            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possivel montar a grid " + erro.Message);
            }

            return ds;
        }
        public DataSet SelectOpcaoProduto(string iCodProduto)
        {
            string iSql = " select PO.CODOPCAO, PO.Preco,OP.Nome,PRO.Tipo,PRO.Codigo as CodTipo  " +
                          "  from Produto_Opcao PO " +
                          " left join Opcao OP on OP.Codigo = PO.CodOpcao " +
                          "join Produto_OpcaoTipo PRO on PRO.Codigo = Op.Tipo" +
                          " where " +
                          " PO.CodProduto = @CodProduto";

            command = new SqlCommand(iSql, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@CodProduto", iCodProduto);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Produto_Opcao");
            return ds;
        }
        public DataSet SelectInsumoProduto(int iCodProduto)
        {
            command = new SqlCommand("spObterInsumoPorCodProduto", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CodProduto", iCodProduto);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Produto_Insumo");

            return ds;
        }
        public DataSet SelectPessoaPorNome(string iNome, string iSqlConsulta, string iParam)
        {
            iSqlConsulta = "select " + iSqlConsulta + " from Pessoa  where " + iParam + " like '%" + iNome + "%'";
            command = new SqlCommand(iSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Pessoa");
            return ds;
        }

        public void Insert(string spName, Object obj)
        {
            if (this == null)
            {
                new Conexao();
            }
            if (statusConexao != ConnectionState.Open)
            {
                MessageBox.Show("Você precisa estar conectado ao banco de dados para continuar");
                return;
            }
            SqlParameter codPedido = null;
            SqlParameter CodPessoa = null;
            SqlParameter CodProduto = null;
            //   SqlParameter CodOpcao = null;
            Type ObjectType = obj.GetType();
            PropertyInfo[] properties = ObjectType.GetProperties();

            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;

            if (spName == "spAlterarEmpresa" || spName == "spAdicionarPessoa" || spName == "spAdicionarCaixa" || spName == "spAdicionaHistorico" ||
                spName == "spAdicionarGrupo" || spName == "spAdicionarProduto" ||
                spName == "spAdicionarConfiguracao" || spName == "spAdicionarEntregador" || spName == "spInserirMovimentoCaixa" || spName == "spAdicionarPedidoStatus" ||
                spName == "spAdicionarEmpresa" || spName == "spAdicionarFamilia" || spName == "spAdicionarMensagen" || spName == "spAdicionarEvento"
                || spName == "spAdicionarOpcaProduto" || spName == "spAdicionarProduto_OpcaoTipo" || spName == "spAdicionarEmpresa_HorarioEntrega" || spName == " spAdicionarPessoa_OrigemCadastro")
            {

                if (spName == "spAdicionarProduto")
                {
                    CodProduto = new SqlParameter("@Codigo", SqlDbType.Int);
                    CodProduto.Direction = ParameterDirection.Output;
                    command.Parameters.Add(CodProduto);
                }
                foreach (PropertyInfo propriedade in properties)
                {
                    if (!propriedade.Name.Equals("Codigo"))
                    {
                        if (!propriedade.Name.Equals("cod"))
                        {
                            if ((propriedade.Name.ToString() == "CodigoPersonalizado" || propriedade.Name.ToString() == "CodFamilia") && propriedade.GetValue(obj).ToString() == "0")
                            {
                                command.Parameters.AddWithValue("@" + propriedade.Name, DBNull.Value);
                            }
                            else
                            {
                                Console.WriteLine(propriedade.Name);
                                command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
                            }

                        }
                    }
                }

            }
            else if (spName == "spAdicionarGrupo")
            {
                foreach (PropertyInfo propriedade in properties)
                {
                    if (!propriedade.Name.Equals("DataSincronismo") && !propriedade.Name.Equals("Codigo"))
                    {
                        command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
                    }
                }
            }
            else if (spName == "spAdicionaBairrosRegiao")
            {
                foreach (PropertyInfo propriedade in properties)
                {
                    if (!propriedade.Name.Equals("DataSincronismo"))
                    {
                        command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
                    }
                }
            }
            else if (spName == "spAdicionarOpcao")
            {
                foreach (PropertyInfo propriedade in properties)
                {
                    if (!propriedade.Name.Equals("DataSincronismo") && !propriedade.Name.Equals("Codigo"))
                    {
                        command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
                    }
                }
            }
            else if (spName == "spAdicionarUsuario")
            {
                foreach (PropertyInfo propriedade in properties)
                {
                    if (!propriedade.Name.Equals("CaixaLogado") && !propriedade.Name.Equals("Codigo") && !propriedade.Name.Equals("Turno"))
                    {
                        Console.WriteLine(propriedade.Name);
                        command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
                    }
                }
            }

            else if (spName == "spAbrirCaixa")
            {
                foreach (PropertyInfo propriedade in properties)
                {
                    if (!propriedade.Name.Equals("Codigo") && !propriedade.Name.Equals("ValorFechamento"))
                    {
                        Console.WriteLine(propriedade.Name);
                        command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
                    }
                }

            }

            else if (spName == "spAdicionarPedido")
            {
                codPedido = new SqlParameter("@Codigo", SqlDbType.Int);
                codPedido.Direction = ParameterDirection.Output;
                command.Parameters.Add(codPedido);

                foreach (PropertyInfo propriedade in properties)
                {
                    if (!propriedade.Name.Equals("Codigo"))
                    {
                        if ((propriedade.Name.ToString() == "CodUsuario" && propriedade.GetValue(obj).ToString() == "0")
                           || (propriedade.Name.ToString() == "Senha" && propriedade.GetValue(obj) == null))
                        {
                            command.Parameters.AddWithValue("@" + propriedade.Name, DBNull.Value);
                        }
                        else
                        {
                            Console.WriteLine(propriedade.Name);
                            command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
                        }

                    }
                }

            }
            else if (spName == "spCriarPedido" || spName == "spAdicionaHistoricoCancelamento")
            {
                foreach (PropertyInfo propriedade in properties)
                {
                    if (!propriedade.Name.Equals("Codigo"))
                    {
                        Console.WriteLine(propriedade.Name);
                        command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
                    }
                }

            }

            else if (spName == "spAlteraFidelidade" || spName == "spZerarFidelidade")
            {

                Console.WriteLine("spAlteraFidelidade");

                foreach (PropertyInfo propriedade in properties)
                {

                    Console.WriteLine(propriedade.Name);
                    command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));

                }

            }
            else if (spName == "spAdicionarClienteDelivery")
            {
                CodPessoa = new SqlParameter("@Codigo", SqlDbType.Int);
                CodPessoa.Direction = ParameterDirection.Output;
                command.Parameters.Add(CodPessoa);
                foreach (PropertyInfo propriedade in properties)
                {
                    if (!propriedade.Name.Equals("Codigo"))
                    {
                        Console.WriteLine(propriedade.Name);
                        command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
                    }
                }

            }
            else if (spName == "spAdicionarEndereco")
            {
                foreach (PropertyInfo propriedade in properties)
                {
                    Console.WriteLine(propriedade.Name);
                    command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
                }
            }
            else
            {
                foreach (PropertyInfo propriedade in properties)
                {
                    if (!propriedade.Name.Equals("Codigo"))
                    {
                        Console.WriteLine(propriedade.Name);
                        command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
                    }
                }
            }

            int n = command.ExecuteNonQuery();

            if (n > 0 && spName == "spAdicionarPedido")
            {
                lastCodigo = int.Parse(codPedido.Value.ToString());
            }
            else if (spName == "spAdicionarClienteDelivery")
            {
                lastCodigo = int.Parse(CodPessoa.Value.ToString());
            }
            else if (spName == "spAdicionarProduto")
            {
                lastCodigo = int.Parse(CodProduto.Value.ToString());
            }
            //else if (spName == "spAdicionarOpcao")
            //{
            //    lastCodigo = int.Parse(CodOpcao.Value.ToString());
            //}

        }

        public int getLastCodigo()
        {
            return lastCodigo;
        }

        public void Update(string spName, Object obj)
        {
            if (this == null)
            {

            }
            new Conexao();
            if (statusConexao != ConnectionState.Open)
            {
                MessageBox.Show("Você precisa estar conectado ao banco de dados para continuar");
                return;
            }
            Type ObjectType = obj.GetType();
            PropertyInfo[] properties = ObjectType.GetProperties();
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            //int pIndex = 0;
            foreach (PropertyInfo p in properties)
            {

                if (spName == "spAlterarTotalPedido")
                {

                    if (p.Name.Equals("CodUsuario") || p.Name.Equals("Codigo") || p.Name.Equals("TotalPedido")
                        || p.Name.Equals("Tipo") || p.Name.Equals("NumeroMesa") || p.Name.Equals("HorarioEntrega")
                        || p.Name.Equals("DescontoValor") || p.Name.Equals("Observacao") || p.Name.Equals("CodEndereco"))
                    {
                        if (p.Name.ToString() == "CodUsuario" && p.GetValue(obj).ToString() == "0")
                        {
                            command.Parameters.AddWithValue("@" + p.Name, DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                        }

                    }
                }
                else if (spName == "spAlterarProdutoInsumo")
                {
                    if (!p.Name.Equals("Codigo") || !p.Name.Equals("Quantidade"))
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }
                }
                else if (spName == "spAlterarGrupo")
                {
                    if (!p.Name.Equals("DataSincronismo") || !p.Name.Equals("Codigo"))
                    {
                        if (p.Name.ToString() == "CodFamilia" && p.GetValue(obj).ToString() == "0")
                        {
                            command.Parameters.AddWithValue("@" + p.Name, DBNull.Value);
                        }
                        else
                        {
                            //  Console.WriteLine(p.Name);
                            command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                        }
                        //   command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }


                }
                else if (spName == "spAlteraOpcao")
                {
                    if (!p.Name.Equals("DataSincronismo"))
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }
                }
                else if (spName == "spAlterarTrocoParaFormaPagamento")
                {
                    if (p.Name.Equals("Codigo") || p.Name.Equals("TrocoPara") || p.Name.Equals("FormaPagamento"))
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }
                }
                else if (/*spName == "spAlterarItemPedido" ||*/ spName == "spFecharCaixa")
                {
                    if (!p.Name.Equals("Codigo") && !p.Name.Equals("ValorAbertura") && !p.Name.Equals("NumeroMesa"))
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }

                }
                else if (spName == "spAlteraStatusMesa")
                {
                    if (!p.Name.Equals("NumeroMesa"))
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }
                }
                else if (spName == "spAlterarUsuario")
                {

                    if (!p.Name.Equals("CaixaLogado") && !p.Name.Equals("Turno"))
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }
                }
                else if (spName == "spAdicionarItemAoPedido")
                {

                    if (!p.Name.Equals("Codigo"))
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }

                }
                else if (spName == "spAlterarEmpresa")
                {

                    if (!p.Name.Equals("Codigo"))
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }

                }
                else if (spName == "spAlterarBairrosRegiao")
                {
                    if (!p.Name.Equals("DataSincronismo"))
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }
                }
                else
                {
                    command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                }
            }
            int n = command.ExecuteNonQuery();
        }

        public void Delete(string spName, Object obj)
        {
            try
            {
                new Conexao();
                if (statusConexao != ConnectionState.Open)
                {
                    MessageBox.Show("Você precisa estar conectado ao banco de dados para continuar");
                    return;
                }
                Type ObjectType = obj.GetType();
                PropertyInfo[] properties = ObjectType.GetProperties();

                command = new SqlCommand(spName, conn);
                command.CommandType = CommandType.StoredProcedure;
                //int pIndex = 0;
                foreach (PropertyInfo p in properties)
                {

                    if (spName == "spExcluirItemPedido")
                    {
                        if (p.Name.Equals("CodProduto") || p.Name.Equals("CodPedido") || p.Name.Equals("Codigo") || p.Name.Equals("NomeProduto"))
                        {
                            command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                        }
                    }

                    else if (spName == "spExcluirOpcaoProduto")
                    {
                        if (p.Name.Equals("CodProduto") || p.Name.Equals("CodOpcao"))
                        {
                            command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                        }
                    }
                    else if (spName == "spExcluirFinalizaPedido_Pedido")
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            int n = command.ExecuteNonQuery();
        }

        public DataSet SelectPessoaPorTelefone(string table, string spName, string telefone)
        {
            new Conexao();
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Telefone", telefone);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }
        public DataSet SelectRegistroPorData(string itable, string ispName, DateTime iDataRegistro)
        {
            command = new SqlCommand(ispName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Data", iDataRegistro);

            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, itable);

            return ds;
        }
        public DataSet SelectRegistroPorDataCodigo(string itable, string ispName, DateTime iDataRegistro, int iNumero)
        {
            command = new SqlCommand(ispName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Data", iDataRegistro);
            command.Parameters.AddWithValue("@Numero", iNumero);

            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, itable);

            return ds;
        }
        public DataSet SelectObterPedidosPorData(string itable, string ispName, DateTime iDataInicio, DateTime iDataFim)
        {
            command = new SqlCommand(ispName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DataInicio", iDataInicio);
            command.Parameters.AddWithValue("@DataFim", iDataFim);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, itable);

            return ds;
        }
        public DataSet SelectObterItemsVendidos(string itable, string ispName, DateTime iDataInicio, DateTime iDataFim)
        {
            command = new SqlCommand("select * from " + ispName, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@DataInicio", iDataInicio);
            command.Parameters.AddWithValue("@DataFim", iDataFim);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, itable);

            return ds;
        }
        public DataSet SelectView(string iNomeView, string iTable)
        {
            command = new SqlCommand("select * from " + iNomeView, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, iTable);

            return ds;
        }
        public DataSet SelectCEPRegiao(string iBairro)
        {
            command = new SqlCommand("select * from  RegiaoEntrega_Bairros where AtivoSN=1 and  NOME='" + iBairro + "'", conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, "RegiaoEntrega_Bairros");

            return ds;
        }

        public DataSet SelectObterPedidosPorCliente(string table, string spName, int CodPessoa)
        {

            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CodPessoa", CodPessoa);
            //   command.Parameters.AddWithValue("@DataInicio", DataInicio);
            //  command.Parameters.AddWithValue("@DataFim", DataFim);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }
        public DataSet SelectObterAniversariantes(string spName, DateTime DataInicial, DateTime DataFinal)
        {

            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DataInicial", DataInicial.ToShortDateString());
            command.Parameters.AddWithValue("@DataFinal", DataFinal.ToShortDateString());
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, "Pessoa");

            return ds;
        }
        public DataSet SelectObterClientesSemPedido(string spName, DateTime DataInicial, DateTime DataFinal)
        {
            try
            {
                command = new SqlCommand(spName, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DataInicial", DataInicial);
                command.Parameters.AddWithValue("@DataFinal", DataFinal);
                adapter = new SqlDataAdapter(command);

                ds = new DataSet();
                adapter.Fill(ds, "Pessoa");
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }


            return ds;
        }
        public DataSet SelectPessoaPorCodigo(string table, string spName, int codigo)
        {

            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Codigo", codigo);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }

        public DataSet SelectProdutoCompleto(string table, string spName, int codigo)
        {

            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            if (spName.Equals("spObterProdutoCompleto") || spName.Equals("spObterProdutoPorCodigo")
                || spName.Equals("spObterProdutoCompletoPorCodPersonalizado"))
            {
                command.Parameters.AddWithValue("@Codigo", codigo);
                command.Parameters.AddWithValue("@AtivoSN", true);
            }
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }


        public DataSet SelectRegistroPorNome(string @iParametro, string table, string spName, string grupoProduto)
        {

            try
            {
                command = new SqlCommand(spName, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(@iParametro, grupoProduto);
                adapter = new SqlDataAdapter(command);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                ds = new DataSet();
                adapter.Fill(ds, table);

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return ds;
        }
        public DataSet LoginUsuario(string iLogin, string iSenha)
        {
            try
            {
                command = new SqlCommand("spLogin", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nome", iLogin);
                command.Parameters.AddWithValue("@Senha", iSenha);
                adapter = new SqlDataAdapter(command);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                ds = new DataSet();
                adapter.Fill(ds, "Usuario");
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }



            return ds;
        }

        public DataSet SelectObterRegistroPorString(string iNome, string itable)
        {
            string iSqlConsulta = "select * from " + itable + " where ";

            command = new SqlCommand(iSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Nome", iNome);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, itable);

            return ds;
        }
        public DataSet SelectObterFormaPagamentoPorNOme(string iNomeFP, string itable)
        {
            string iSqlConsulta = "spObterFPNOme";

            command = new SqlCommand(iSqlConsulta, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Nome", iNomeFP);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, itable);

            return ds;
        }
        public DataSet InsertCep(string table, string spName, CepUtil cep)
        {

            Type ObjectType = cep.GetType();
            PropertyInfo[] properties = ObjectType.GetProperties();

            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            //int pIndex = 0;
            foreach (PropertyInfo p in properties)
            {
                command.Parameters.AddWithValue("@" + p.Name, p.GetValue(cep));
            }
            int n = command.ExecuteNonQuery();
            if (n > 0)
            {
                MessageBox.Show("Cep Cadastrado");
            }
            else
            {
                MessageBox.Show("Erro ao Cadastrar , preencha todos campos");
            }
            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }

        public DataSet SelectEnderecoPorCep(string table, string spName, int cep)
        {
            try
            {
                Conexao con = new Conexao();
                if (con.statusConexao != ConnectionState.Open)
                {
                    MessageBox.Show(Bibliotecas.cSemConexao);
                    return new DataSet();
                }
                command = new SqlCommand(spName, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Cep", cep);
                adapter = new SqlDataAdapter(command);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                ds = new DataSet();
                adapter.Fill(ds, table);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }


            return ds;
        }
        public DataSet ValidaMultiplasFormasPagamento(int iCodPedido)
        {
            string lSqlConsulta = " select * from Pedido_Finalizacao PF" +
                                  "  join Pedido P on P.Codigo = PF.CodPedido" +
                                  " where" +
                                  " CodPedido = @Codigo and Codigo = @Codigo ";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Codigo", iCodPedido);


            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Pedido_Finalizacao");
            return ds;
        }
        public DataSet SelectItensPorImpressora(int intCodPedido, string strNomeimpressora)
        {
            try
            {
                new Conexao();
                command = new SqlCommand("spObterItemsNaoImpressoPorImpressora", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Codigo", intCodPedido);
                command.Parameters.AddWithValue("@NomeImpressora", strNomeimpressora);
                adapter = new SqlDataAdapter(command);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                ds = new DataSet();
                adapter.Fill(ds, "ItemsPedido");

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return ds;
        }
        public DataSet SelectRegistroPorCodigo(string table, string spName, List<string> listCodigo)
        {
            try
            {
                string strLista = string.Join(",", listCodigo);
                string iSqlConsulta = "select Codigo,NomeProduto from Produto where Codigo in (" + strLista + ")";
                command = new SqlCommand(iSqlConsulta, conn);
                command.CommandType = CommandType.Text;
                adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds, table);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }


            return ds;
        }
        public DataSet SelectRegistroPorCodigo(string table, string spName, int codigo, string iCodString = "0", int iCodigo2 = 0)
        {
            try
            {
                new Conexao();
                command = new SqlCommand(spName, conn);
                command.CommandType = CommandType.StoredProcedure;
                if (spName == "spObterCodigoMesa")
                {
                    command.Parameters.AddWithValue("@NumeroMesa", iCodString);
                }
                else if (spName == "spObterPedidoPorSenha")
                {
                    command.Parameters.AddWithValue("@Senha", iCodString);
                }
                else if (spName == "spObterHistoricoPorPessoa")
                {
                    command.Parameters.AddWithValue("@CodPessoa", codigo);
                }
                else if (spName == "spObterItemsNaoImpresso")
                {
                    command.Parameters.AddWithValue("@Codigo", codigo);
                    command.Parameters.AddWithValue("@NomeImpressora", iCodString);
                }
                else if (spName == "spObterProdutoCodigoInterno")
                {
                    command.Parameters.AddWithValue("@Codigo", codigo);
                    command.Parameters.AddWithValue("@CodProduto", iCodString);
                }
                else if (spName == "spObterItemsNaoImpressoPorCodigo")
                {
                    command.Parameters.AddWithValue("@Codigo", codigo);
                }
                else if (spName == "spObterItemsNaoImpressoPorGrupo" || spName == "spObterItemsNaoImpressoPorCozinhaCodPersonalizado")
                {
                    command.Parameters.AddWithValue("@Codigo", codigo);
                    command.Parameters.AddWithValue("@CodGrupo", iCodigo2);
                }

                else
                {
                    command.Parameters.AddWithValue("@Codigo", codigo);
                }

                adapter = new SqlDataAdapter(command);
                // adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                ds = new DataSet();
                adapter.Fill(ds, table);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }


            return ds;
        }
        public DataSet SelectRegistroPorCodigo(string table, string spName, int codigo, Boolean AtivoSN)
        {
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            if (spName == "spObterCodigoMesa")
            {
                command.Parameters.AddWithValue("@NumeroMesa", codigo);
            }
            else if (spName == "spObterUsuarioPorCodigo")
            {
                command.Parameters.AddWithValue("@Codigo", codigo);
            }
            else if (spName == "spObterHistoricoPorPessoa")
            {
                command.Parameters.AddWithValue("@CodPessoa", codigo);
            }
            else if (spName == "spObterFidelidadePessoa")
            {
                command.Parameters.AddWithValue("@Codigo", codigo);
            }
            else
            {
                command.Parameters.AddWithValue("@Codigo", codigo);
                command.Parameters.AddWithValue("@AtivoSN", AtivoSN);
            }

            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }
        public DataSet SelectItemsPedido(string table, string spName)
        {
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }

        public DataSet SinalizarPedidoConcluido(string table, string spName, int codigo, int iNumeroPessoas)
        {
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Codigo", codigo);
            command.Parameters.AddWithValue("@NumeroPessoas", iNumeroPessoas);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }
        /// <summary>
        /// Transfere determinado item de uma mesa para outra
        /// </summary>
        /// <param name="intCodItem"> Código do item na tabela ItemPedido.Codigo</param>
        /// <param name="intCodigoMesaDestino"> Código da mesa destino que receberá o item Mesas.Codigo</param>
        /// <returns></returns>
        public int TransfereItemMesa(int intCodItem, int intCodigoMesaDestino)
        {
            int iReturn = 0;
            try
            {
                command = new SqlCommand("spTransfereItemMesa", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Codigo", intCodItem);
                command.Parameters.AddWithValue("@CodigoMesaDestino", intCodigoMesaDestino);

                adapter = new SqlDataAdapter(command);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                iReturn = command.ExecuteNonQuery();

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException);
            }
            return iReturn;
        }
        public int TransfereMesa(int intCodPedidoOrigem, string iNumMesaOrigem, int iCodUser
            , decimal iTotalPedido
            , int iCodMesaOrigem, int iCodPessoa, int iNewMesa)
        {
            int iReturn = 0;
            try
            {
                new Conexao();
                command = new SqlCommand("spTransfeMesa", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Codigo", intCodPedidoOrigem);
                command.Parameters.AddWithValue("@NumeroMesa", iNumMesaOrigem);
                command.Parameters.AddWithValue("@CodUsuario", iCodUser);
                command.Parameters.AddWithValue("@TotalPedido", iTotalPedido);
                command.Parameters.AddWithValue("@CodigoMesa", iCodMesaOrigem);
                command.Parameters.AddWithValue("@CodPessoa", iCodPessoa);
                command.Parameters.AddWithValue("@NewMesa", iNewMesa);

                adapter = new SqlDataAdapter(command);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                command.ExecuteNonQuery();

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return iReturn;
        }

        public DataSet SelectItemsExtrasPorPedido(string table, string spName, int codigo)
        {
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CodPedido", codigo);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }
        public DataSet Liberacao(string iNomeEmpresa, string CNPJ, string iNomePC, string iMAC)
        {
            try
            {
                int Tabela;
                Boolean AtivoSn = false;
                DataRow Colunas;
                try
                {

                    MysqlConnection = new MySqlConnection("Server=mysql.expertsistemas.com.br;Port=3306;Database=exper194_lazaro;Uid=exper194_lazaro;Pwd=@@3412064;");
                    MysqlConnection.Open();
                    if (MysqlConnection.State == ConnectionState.Open)
                    {
                        MysqlCommand = new MySqlCommand("select cnpj,AtivoSN,NomePC,MACPC from Licenca where cnpj='" + CNPJ + "' and NomePC='" + iNomePC + "' and MACPC='" + iMAC + "'", MysqlConnection);
                        MysqlDataAdapter = new MySqlDataAdapter(MysqlCommand);
                        MysqlDataAdapter.Fill(ds, "Licenca");
                        if (ds.Tables["Licenca"].Rows.Count > 0)
                        {
                            Colunas = ds.Tables["Licenca"].Rows[0];

                            if (Colunas.ItemArray.GetValue(1).ToString() == "1")
                            {
                                AtivoSn = true;

                                if (!AtivoSn)
                                {
                                    ds = null;
                                }
                            }
                            else
                            {
                                ds = null;
                            }

                        }
                        else
                        {
                            ds = null;
                        }



                        MysqlCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        MessageBox.Show("Sem conexão com o servidor [xSistemas - Amazon AWS] para validação da Licença , tente novamente  reiniciando o sistema", "[xSistemas] Erro ");
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("Erro na validação da licença o sistema abrirá em modo segurança" + e.Message);
                    int intAbriu5Vezes = Utils.ContaRegistro(iNomeEmpresa + CNPJ);
                    if (intAbriu5Vezes >= 5)
                    {
                        MessageBox.Show(" Não foi possivel validar seu acesso o sistema  será encerrado!");
                        Application.Exit();
                    }
                    else
                    {
                        Utils.IniciaSistema();
                    }
                }
            }
            finally
            {
                MysqlConnection.Close();

            }
            return ds;
        }
        public void InserirLicencaTemporaria(string CNPJ, bool AtivoSN, string NomeEmpresa, DateTime dataLiberacao, DateTime DataExpiracao)
        {
            try
            {
                MysqlConnection = new MySqlConnection("Server=mysql.expertsistemas.com.br;Port=3306;Database=exper194_lazaro;Uid=exper194_lazaro;Pwd=@@3412064;");
                MysqlConnection.Open();
                MysqlCommand = new MySqlCommand("insert into Licenca(CNPJ,AtivoSN,Nome,DataLiberacao,DataExpiracao) values " + CNPJ + ", " + AtivoSN + " ," + NomeEmpresa + ", " + dataLiberacao + " ," + DataExpiracao, MysqlConnection);
                MysqlDataAdapter = new MySqlDataAdapter(MysqlCommand);
                MysqlDataAdapter.Fill(ds, "Licenca");
                MysqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                MessageBox.Show("Não foi possivel criar uma licença de testes" +
                    e.Message);
            }
        }

        //Método da API
        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);

        // Um método que verifica se esta conectado
        public Boolean IsConnected()
        {
            try
            {
                int Description;

                return InternetGetConnectedState(out Description, 0);
                //using (var client = new WebClient())
                //using (var stream = client.OpenRead("http://www.google.com"))
                //{
                //    return true;
                //}
            }
            catch
            {
                return false;
            }
        }


        //public DataSet SelecionaItensAtualizados(string iSpnome ,DateTime iDataAtualizacao, string iTabela)
        //{
        //    //command = new SqlCommand(iSpnome, conn);
        //    //command.CommandType = CommandType.StoredProcedure;
        //    //command.Parameters.AddWithValue("@Codigo", CodigoDeletar);
        //    //adapter = new SqlDataAdapter(command);
        //    //ds = new DataSet();
        //    //adapter.Fill(ds, iTabela);


        //    return ds;
        //}


    }
}
