
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexGerencial
{
    public class Dados
    {
        public Dados() { }

        public static MySqlConnection connMysql = Mysql.getInstancia().getConnection();
        public static MySqlCommand myCommandMysql { get; set; }
        public static MySqlDataAdapter mdaMysql { get; set; }
        public static MySqlDataReader mdrMysql { get; set; }
        public static DataSet dsMysql { get; set; }
        public static DataTable dtMysql { get; set; }
        public static DataRow rowMysql { get; set; }

        public static string query { get; set; }
        public static string dataMember { get; set; }

    }
}
