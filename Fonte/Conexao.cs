using System;
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

namespace DexComanda
{
    public class Conexao
    {
        private MySqlCommand MysqlCommand;
        private MySqlConnection MysqlConnection;
        private MySqlDataAdapter MysqlDataAdapter;
        //   private const int CmysqlTimeOut = 50000;
        private static string dataMember;
        private DataSet ds;
        private DataSet Dados;
        private SqlCommand command;
        private static SqlConnection conn;
        private SqlDataAdapter adapter;
        private int lastCodigo;
        public static string connectionString = null;

        public Conexao()
        {
            try
            {
                if (connectionString != null)
                {
                    conn = new SqlConnection(connectionString);
                    conn.Open();
                }
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message, "Erro conexao com o SQLSERVER");
            }
        }

        // Rotina para efetuar Backup Automátizado do Banco de dados ao Encerrar o Programa
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
        public DataSet RetornarTaxaPorBairro(string iNOmeBairro)
        {
            string lSqlConsulta = " select R.Codigo , " +
                                  " R.TaxaServico , " +
                                  " R.NomeRegiao " +
                                  " from RegiaoEntrega R " +
                                  " left join RegiaoEntrega_Bairros RG on RG.CodRegiao = R.Codigo " +
                                  "  where RG.Nome like '%"+ iNOmeBairro +"%'";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
        //    command.Parameters.AddWithValue("@NomeBairro", iNOmeBairro);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "RegiaoEntrega");
            return ds;

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
        public void OpenConection(string servidor, string banco)
        {
            connectionString = @"Data Source=" + servidor + ";Initial Catalog=" + banco + "; User ID=dex; Password=1234; Trusted_Connection=False; ";
            conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();

                MessageBox.Show("Conectado ao banco de dados.");
                // Utils.CriarUsuario(connectionString, "dex", "1234");

                var temp = Directory.GetCurrentDirectory() + @"\ConnectionString_DexComanda.txt";

                if (!System.IO.File.Exists(temp))
                {
                    System.IO.File.Create(temp).Close();
                    System.IO.File.AppendAllText(temp, connectionString);
                }
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message + "Não pode ser aberta um conexão com o banco de dados.");
            }
        }

        public void Close()
        {
            // conn.Close();
        }

        public void SalvarAdicionais(int iCodProduto, int iCodOpcao, decimal iPreco)
        {
            Produto_Opcao prod = new Produto_Opcao()
            {
                CodProduto = iCodProduto,
                CodOpcao = iCodOpcao,//int.Parse(AdicionaisGridView.Rows[i].Cells["CodOpcao"].Value.ToString()),
                Preco = iPreco,//decimal.Parse(AdicionaisGridView.Rows[i].Cells["Preco"].Value.ToString()),
                DataAlteracao = DateTime.Now
            };
            Insert("spAdicionarOpcaProduto", prod);

        }


        public DataSet SelectAll(string table, string spName)
        {
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, table);
            return ds;
        }
        public DataSet RetornaOpcoesProduto(int iDProduto)
        {
            string lSqlConsulta = " select Op.Nome, "+
                                  " PoT.Tipo," +
                                  " Prod.Preco," +
                                  " ISNULL((select MaximoAdicionais from Produto P where P.Codigo=Prod.CodProduto  ),0) as MaximoAdicionais,"+
                                  " PoT.Nome as NomeTipo " +
                                  " from Produto_Opcao Prod" +
                                  " join Opcao Op  on Op.Codigo = Prod.CodOpcao" +
                                  " join Produto_OpcaoTipo PoT on PoT.Codigo = Op.Tipo" +
                                  "  where Prod.CodProduto = @CodProduto" +
                                  " order by PoT.Nome";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@CodProduto", iDProduto);


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
            string lSqlConsulta = " select Codigo,Descricao from FormaPagamento";
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
            string lSqlConsulta = "";

            command = new SqlCommand();

            try
            {
                if (iSemPedido)
                {
                    lSqlConsulta = "select Telefone,Nome from Pessoa P" +
                                  "  where  " +
                                  "  P.Codigo not in ( Select Codigo from Pedido where RealizadoEm between @Data1 and @Data2 )" +
                                  "  and " +
                                  "  (SUBSTRING(Telefone,0,2) = 9 " +
                                  "  or SUBSTRING(Telefone,0,2) =8)";


                    command.Parameters.AddWithValue("@Data1", iData1);
                    command.Parameters.AddWithValue("@Data2", iData2);
                }
                else if (iAniversariante)
                {
                    lSqlConsulta = "select Telefone,Nome from Pessoa P " +
                                  "  where  " +
                                  "  Cast(DataNascimento as date) between @Data1 and @Data2 " +
                                  "  and " +
                                  "  (SUBSTRING(Telefone,0,2) = 9 " +
                                  "  or SUBSTRING(Telefone,0,2) =8 )";

                    command.Parameters.AddWithValue("@Data1", iData1);
                    command.Parameters.AddWithValue("@Data2", iData2);
                }
                else if (iTodos)
                {
                    lSqlConsulta = "select top 2000 Telefone,Nome from Pessoa P" +
                                   " where  " +
                                   " (SUBSTRING(Telefone,0,2) = 9 " +
                                   " or SUBSTRING(Telefone,0,2) =8  " +
                                   " and DATALENGTH(Telefone) >=8)   and Codigo >26349";
                }
            }
            catch (Exception erro)
            {

                throw;
            }

            command.CommandText = lSqlConsulta;
            command.Connection = conn;
            command.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Pessoa");
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
        public DataSet SelectCaixaFechamento(string iDataI, string iDataF, string iNumCaixa, string table = "CaixaMovimento")
        {
            string lSqlConsulta = " select " +
                                 " case Tipo " +
                                 " when 'E' then 'Entradas'" +
                                 " when 'S' then 'Saidas'" +
                                 " end" +
                                 " as 'Tipo Movimento', " +
                                //" Cx.CodCaixa," +
                                " Fp.Descricao ," +
                                " sum(cx.Valor) as 'Total Somado'" +
                                " from CaixaMovimento CX" +
                                " left join FormaPagamento FP on FP.Codigo = Cx.CodFormaPagamento" +
                                " where " +
                                " CX.CodCaixa = @Numero AND" +
                                "  CX.Data BETWEEN @DataI  AND @DataF " +
                                " group by CodCaixa,Fp.Descricao,Tipo";


            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Numero", iNumCaixa);
            command.Parameters.AddWithValue("@DataI", iDataI);
            command.Parameters.AddWithValue("@DataF", iDataF);

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, table);
            return ds;
        }
      
        public DataSet SelectRegistroONline(string iNomeTable)
        {
            string lSqlConsulta = " select * from " + iNomeTable + " where DataAlteracao>DataSincronismo or DataSincronismo is null ";

            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, iNomeTable);
            return ds;

        }
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
                                  "  ,RB.CEP ,Isnull(RB.OnlineSN,0) as OnlineSN" +
                                  "   from RegiaoEntrega RG " +
                                  "  join RegiaoEntrega_Bairros RB on RB.CodRegiao = RG.Codigo and RB.OnlineSN=1 " +
                                  "  WHERE (RG.DataAlteracao > RG.DataSincronismo or RG.DataSincronismo is null ) " +
                                  " and RG.ONLINESN=1 " ;

            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "RegiaoEntrega");
            return ds;

        }
        public DataSet RetornaCEPporBairro(string iNomeBairro, Boolean iCidadePadrao)
        {
            string lSqlConsulta = "select cep,bairro cep from base_cep where cep=" + iNomeBairro;
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


        public void AtualizaDataSincronismo(string iNomeTable, int iCodigo, string iDataAtualizar = "DataSincronismo")
        {

            string lSqlConsulta = " update " + iNomeTable + " set " + iDataAtualizar + "=GetDate() where Codigo=" + iCodigo;// and AtivoSN=1";

            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();

        }
        public void AtualizaDataSincronismo(string iNomeTable, int iCodProduto, int iCodOpcao, string iDataAtualizar = "DataSincronismo")
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
        public DataSet SelectCaixaMovimetoFiltro(string iDataI, string iDataF, string iTipo, string iCdFormaPagt, string table = "CaixaMovimento", string iNumCaixa = "1")
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
                                    " LEFT JOIN Caixa          CX ON CX.Codigo = CXM.CodCaixa " +
                                " where " +
                                "  CXM.Data BETWEEN  '" + iDataI.ToString() + "' AND '" + iDataF.ToString() + "'";

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


            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, table);
            return ds;
        }
        public DataSet DeleteAll(string table, string spName, int CodigoDeletar)
        {
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Codigo", CodigoDeletar);
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }
        public DataSet Delete(string table, string spName, int CodProduto, int CodOpcao)
        {
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
        public DataSet SelectMontaGrid(string iTable, string iParametrosConsulta, Boolean iAtivos = true)
        {
            string iSql = "", iSubSelect = "subSelect";

            if (iParametrosConsulta != null)
            {
                iSql = "select " + iParametrosConsulta + iSubSelect + " from " + iTable;

                if (iTable == "Pedido")
                {
                    iSql = iSql.Replace(iSubSelect, "") + " Pd where Finalizado = 0 and [status] ='Aberto' ORDER BY Pd.Codigo DESC";
                }
                else
                if (iTable == "Produto")
                {
                    if (Sessions.returnEmpresa.CNPJ == "13004606798" || Sessions.returnEmpresa.CNPJ == "21207218000191")
                    {
                        iSql = iSql.Replace(iSubSelect, ",(select top 1 Quantidade from Produto_Estoque E where E.CodProduto = Produto.Codigo and E.DataAtualizacao between '" + DateTime.Now.Date.ToShortDateString() + " 00:00:00" + "' and '" + DateTime.Now.Date.ToShortDateString() + " 23:59:59') as QtdVendida");
                    }

                    if (iAtivos)
                    {
                        iSql = iSql.Replace(iSubSelect, "") + " where AtivoSN=1";
                    }
                    else
                    {
                        iSql = iSql.Replace(iSubSelect, "") + " where AtivoSN=0";
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
            return ds;
        }
        public DataSet SelectOpcaoProduto(string iCodProduto)
        {
            string iSql = " select PO.CODOPCAO, PO.Preco,OP.Nome,PRO.Tipo  " +
                          "  from Produto_Opcao PO " +
                          " left join Opcao OP on OP.Codigo = PO.CodOpcao " +
                          "join Produto_OpcaoTipo PRO on PRO.Codigo = Op.Tipo"+
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
                spName == "spAdicionarConfiguracao" || spName == "spAdicionarEntregador" || spName == "spInserirMovimentoCaixa" ||
                spName == "spAdicionarEmpresa" || spName == "spAdicionarMensagen" || spName == "spAdicionarEvento" || spName == "spAdicionarOpcaProduto" || spName == "spAdicionarProduto_OpcaoTipo")
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
                            Console.WriteLine(propriedade.Name);
                            command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
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
                    if (!propriedade.Name.Equals("CaixaLogado") && !propriedade.Name.Equals("Codigo"))
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
                        Console.WriteLine(propriedade.Name);
                        command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
                    }
                }

            }
            //else if (spName == "spAdicionarOpcao")
            //{
            //    CodOpcao = new SqlParameter("@Codigo", SqlDbType.Int);
            //    CodOpcao.Direction = ParameterDirection.Output;
            //    command.Parameters.Add(CodOpcao);

            //    foreach (PropertyInfo propriedade in properties)
            //    {
            //        if (!propriedade.Name.Equals("Codigo") && !propriedade.Name.Equals("DataSincronismo"))
            //        {
            //            Console.WriteLine(propriedade.Name);
            //            command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(obj));
            //        }
            //    }

            //}
            else if (spName == "spCriarPedido" || spName == "spAdicionaHistoricoCancelamento")
            {

                //  Console.WriteLine("spCriarPedido");

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

            Type ObjectType = obj.GetType();
            PropertyInfo[] properties = ObjectType.GetProperties();
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            //int pIndex = 0;
            foreach (PropertyInfo p in properties)
            {

                if (spName == "spAlterarTotalPedido")
                {
                    if (p.Name.Equals("Codigo") || p.Name.Equals("TotalPedido") || p.Name.Equals("Tipo") || p.Name.Equals("NumeroMesa"))
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }
                }
                //else if (spName=="spAlterarOpcaoProduto")
                //{
                //    if (!p.Name.Equals("DataSincronismo") ||!p.Name.Equals("Codigo"))
                //    {
                //        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                //    }
                //}
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
                else if (spName == "spAlterarItemPedido" || spName == "spAlteraStatusMesa" || spName == "spFecharCaixa")
                {
                    if (!p.Name.Equals("Codigo") && !p.Name.Equals("ValorAbertura"))
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }

                }
                else if (spName == "spAlterarUsuario")
                {

                    if (!p.Name.Equals("CaixaLogado"))
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

            Type ObjectType = obj.GetType();
            PropertyInfo[] properties = ObjectType.GetProperties();

            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            //int pIndex = 0;
            foreach (PropertyInfo p in properties)
            {

                if (spName == "spExcluirItemPedido")
                {
                    if (p.Name.Equals("CodProduto") || p.Name.Equals("CodPedido"))
                    {
                        if (!p.Name.Equals("Codigo"))
                        {
                            command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                        }
                    }
                }

                else if (spName == "spExcluirOpcaoProduto")
                {
                    if (p.Name.Equals("CodProduto") || p.Name.Equals("CodOpcao"))
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }
                }
            }
            int n = command.ExecuteNonQuery();
        }

        public DataSet SelectPessoaPorTelefone(string table, string spName, string telefone)
        {

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
            command = new SqlCommand("select * from  RegiaoEntrega_Bairros where AtivoSN=1 and  NOME='"+ iBairro +"'", conn);
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
        public DataSet SelectObterAniversariantes(string table, string spName, DateTime DataInicial, DateTime DataFinal)
        {

            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DataInicial", DataInicial);
            command.Parameters.AddWithValue("@DataFinal", DataFinal);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }
        public DataSet SelectObterClientesSemPedido(string table, string spName, DateTime DataInicial, DateTime DataFinal)
        {

            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DataInicial", DataInicial);
            command.Parameters.AddWithValue("@DataFinal", DataFinal);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

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
            if (spName.Equals("spObterProdutoCompleto") || spName.Equals("spObterProdutoPorCodigo"))
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

            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue(@iParametro, grupoProduto);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

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

            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Cep", cep);
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
        }
        public DataSet ValidaMultiplasFormasPagamento(int iCodPedido)
        {
            string lSqlConsulta = " select * from Pedido_Finalizacao PF"+
                                  "  join Pedido P on P.Codigo = PF.CodPedido"+
                                  " where"+
                                  " CodPedido = @Codigo and Codigo = @Codigo ";
            command = new SqlCommand(lSqlConsulta, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Codigo", iCodPedido);
          

            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, "Pedido_Finalizacao");
            return ds;
        }

        public DataSet SelectRegistroPorCodigo(string table, string spName, int codigo)
        {
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            if (spName == "spObterCodigoMesa")
            {
                command.Parameters.AddWithValue("@NumeroMesa", codigo);
            }
            else if (spName == "spObterHistoricoPorPessoa")
            {
                command.Parameters.AddWithValue("@CodPessoa", codigo);
            }
            else
            {
                command.Parameters.AddWithValue("@Codigo", codigo);

            }

            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

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
            else if (spName == "spObterHistoricoPorPessoa")
            {
                command.Parameters.AddWithValue("@CodPessoa", codigo);
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

        public DataSet SinalizarPedidoConcluido(string table, string spName, int codigo)
        {
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Codigo", codigo);
            //   command.Parameters.AddWithValue("@status", "Fechado");
            adapter = new SqlDataAdapter(command);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adapter.Fill(ds, table);

            return ds;
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
            int Tabela;
            Boolean AtivoSn = false;
            DataRow Colunas;
            try
            {
                MysqlConnection = new MySqlConnection("Server=mysql.expertsistemas.com.br;Port=3306;Database=exper194_lazaro;Uid=exper194_lazaro;Pwd=@@3412064;");
                // MysqlCommand.CommandTimeout = CmysqlTimeOut;
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
                        Utils.ExcluiRegistro();
                    }



                    MysqlCommand.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Sem conexão com o servidor central para validação da Licença , tente novamente  reiniciando o sistema", "[xSistemas] Erro ");
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Erro na validação da licença" + e.Message);
                int intAbriu5Vezes = Utils.ContaRegistro(iNomeEmpresa + CNPJ);
                if (intAbriu5Vezes >= 5)
                {
                    MessageBox.Show(" Não foi possivel validar seu acesso o sistema  será encerrado!");
                    Utils.Kill();
                }
            }
            return ds;
        }
        public void InserirLicencaTemporaria(string CNPJ, bool AtivoSN, string NomeEmpresa, DateTime dataLiberacao, DateTime DataExpiracao)
        {
            try
            {
                MysqlConnection = new MySqlConnection("Server=mysql.expertsistemas.com.br;Port=3306;Database=exper194_lazaro;Uid=exper194_lazaro;Pwd=@@3412064;");
                // MysqlCommand.CommandTimeout = CmysqlTimeOut;
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
