using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace DexGerencial
{
    public class ConexaoMysql
    {
        public ConexaoMysql()
        {
            if (Dados.connMysql.State != ConnectionState.Open)
            {
                Dados.connMysql.Open();
            }
        }  
    }
}
