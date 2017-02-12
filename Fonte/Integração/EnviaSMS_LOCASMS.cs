using DexComanda.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Integração
{
    public class EnviaSMS_LOCASMS
    {

        public string EnviaSMSLista(List<ListaSms> list, string iUser, string iSennha, string iMensagem, string iNomeCampanha)
        {
            Array[] IRetorno = new Array[2];
            try
            {
                // Lista de Destinatários
                List<LOCASMS.Destination> Destinatarios = new List<LOCASMS.Destination>();
                foreach (var clientes in list)
                {
                    Destinatarios.Add(new LOCASMS.Destination() { Number = clientes.Numero, Name = clientes.Nome });
                }

                // Montando os Parametros
                LOCASMS.rSMS Parametros = new LOCASMS.rSMS();
                
                // Adicionando lista ao objeto
                Parametros.Destinations = Destinatarios.ToArray();
                
                // Determinando o Nome da Campanha
                Parametros.Subject = iNomeCampanha;
                
                // Determinando a Mensagem a ser encaminhada
                Parametros.Msg = iMensagem;

                // Objeto de Comunicação com o WebService
                LOCASMS.ServiceSMSSoapClient Client = new LOCASMS.ServiceSMSSoapClient();

                // Acionando o Recurso no WebService
                IRetorno[0] = Client.SendSMS(iUser, iSennha, Parametros).ToArray();

                IRetorno[1] = Convert.ToString(Parametros.Destinations.Length).ToArray();
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao enviar os SMS" + erro.Message);
            }

            return IRetorno.ToString();

        }


    }
}
