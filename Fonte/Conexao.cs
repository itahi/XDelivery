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
            string BackupFileName = iNomeBanco + DateTime.Now.ToShortDateString().Replace("/", "") + ".bkp";
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

        public DataSet SelectAll(string table, string spName)
        {
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(command);
            ds = new DataSet();
            adapter.Fill(ds, table);
            return ds;
        }

        public DataSet SelectRegistroPorCodigoPeriodo(string table, string spName, string iCodPessoa,DateTime iDataI , DateTime iDataF)
        {
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CodPessoa", iCodPessoa);
            command.Parameters.AddWithValue("@DataInicio", iDataI);
            command.Parameters.AddWithValue("@DataFim", iDataF);
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

        public void Insert(string spName, Object obj)
        {
            SqlParameter codPedido = null;

            Type ObjectType = obj.GetType();
            PropertyInfo[] properties = ObjectType.GetProperties();

            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;

            if (spName == "spAlterarEmpresa" || spName == "spAdicionarPessoa" ||
                spName == "spAdicionarGrupo" || spName == "spAdicionarProduto" ||
                spName == "spAdicionarConfiguracao" || spName == "spAdicionarEntregador" ||
                spName == "spAdicionarEmpresa" || spName == "spAdicionarMensagen" || spName == "spAdicionarEvento")
            {

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

            else if (spName == "spAdicionarPedido" )
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
                else if (spName == "spAlterarTrocoParaFormaPagamento")
                {
                    if (p.Name.Equals("Codigo") || p.Name.Equals("TrocoPara") || p.Name.Equals("FormaPagamento"))
                    {
                        command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                    }
                }
                else if (spName == "spAlterarItemPedido")
                {
                    if (!p.Name.Equals("Codigo"))
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
                if (!p.Name.Equals("Codigo"))
                {
                    if (spName == "spExcluirItemPedido")
                    {
                        if (p.Name.Equals("CodProduto") || p.Name.Equals("CodPedido"))
                        {
                            command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));
                        }
                    }


                    //else if (spName == "spExcluirProduto")
                    //{

                    //   command.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj));

                    //}

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

        public DataSet SelectRegistroPorCodigo(string table, string spName, int codigo)
        {
            command = new SqlCommand(spName, conn);
            command.CommandType = CommandType.StoredProcedure;
            if (spName == "spObterCodigoMesa")
            {
                command.Parameters.AddWithValue("@NumeroMesa", codigo);
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
        public DataSet Liberacao(string CNPJ, string iNomePC, string iMAC)
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
                        Utils.ExcluiRegistro();
                    }



                    MysqlCommand.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Sem conexão com o servidor central para validação da Licença , tente novamente  reiniciando o sistema", "Dex Erro ");
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Erro na validação da licença" + e.Message);
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
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
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
