using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DexComanda.Operações.Funções
{
    public class RotinasGlobal
    {
        public void ExecutaBackup(string iNomeBanco, string iCaminhoBackup, Conexao iConection)
        {
            try
            {
                iConection = new Conexao();
                var Server = new Server() ;

                //Criando o diretorio do Backup
                if (!Directory.Exists(@iCaminhoBackup))
                {
                    Directory.CreateDirectory(@iCaminhoBackup);
                }

                // Criando o objeto Backup
                var bak = new Backup();
                bak.Incremental = false;

                bak.Action = BackupActionType.Database;
                bak.BackupSetName = "BKP"+iNomeBanco + DateTime.Now.ToShortDateString();

                // Definindo o banco de dados a ser salvo
                bak.Database = iNomeBanco;

                bak.Checksum = true;

                // Adcionando um destino para o backup
                bak.Devices.Add(new BackupDeviceItem(iCaminhoBackup, DeviceType.File));

                // Executando o backup
                bak.SqlBackup(Server);

            }

            catch (Exception)
            {

                throw;
            }

        }
    }
}
