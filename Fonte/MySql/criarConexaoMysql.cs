using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DexGerencial
{
    class criarConexaoMysql
    {

        public criarConexaoMysql()
        {

        }

        //public bool salvarDadosDoBanco(string servidor, string banco, string usuario, string senha)
        //{

        //    try
        //    {
        //        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //        string con = "server=" + servidor + ";database=" + banco + ";uid='" + usuario + "';pwd='" + senha + "';Allow Zero Datetime=True";

        //        config.ConnectionStrings.ConnectionStrings["ADM.MysqlConnection"].ConnectionString = con;

        //        config.Save(ConfigurationSaveMode.Modified); // Salva o que foi modificado

        //        ConfigurationManager.RefreshSection("connectionStrings"); // Atualiza no app o bloco connectionStrings

        //        UI.Properties.Settings.Default.Reload();
        //        MessageBox.Show("Arquivo configurado com sucesso");
        //       return true;
               
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }




        //}

    }
}
