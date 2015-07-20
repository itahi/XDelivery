using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace DexGerencial
{
    public class Mysql
    {
        private static readonly Mysql instancia = new Mysql();
        private static MySqlConnection conexao;



        public static Mysql getInstancia()
        {
            return instancia;
        }

        public MySqlConnection getConnection()
        {
            string conn = "Server=69.64.46.57;Port=3306;Database=digitale_licensa;Uid=digit_user;Pwd=Dex2016d;"; //ConfigurationManager.ConnectionStrings["ADM.MysqlConnection"].ToString();

            conexao = new MySqlConnection(conn);
            return conexao;
        }

    }
}
