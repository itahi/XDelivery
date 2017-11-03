using DexComanda;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xConnect_iFood
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var temp = Directory.GetCurrentDirectory() + @"\ConnectionString_DexComanda.txt";
            if (File.Exists(temp))
            {
                StreamReader tempDex = new StreamReader(temp);
                // string sLine = "";
                //sLine = tempDex.ReadLine();
                Conexao.connectionString = tempDex.ReadLine();
            }
           

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmPrincipal());
        }
    }
}
