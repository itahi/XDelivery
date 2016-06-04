using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Integração
{
   public class EnviaSMS_LOCASMS
    {

        public string EnviaSMSLista(Array iNumeros, string iUser, string iSennha, string iMensagem, string iNomeCampanha)
       {
           Array[] IRetorno = new Array[2];
           try
           {
               
               // Lista de Destinatários
               List<LOCASMS.Destination> Destinatarios = new List<LOCASMS.Destination>();

               // Carregando os Destinatarios do List View
               foreach (String numeroDestinatario in iNumeros)
               {
                   if (numeroDestinatario!=null)
                   {
                       Destinatarios.Add(new LOCASMS.Destination() { Number = numeroDestinatario }); 
                   }
                   
               }
               // Montando os Parametros
               LOCASMS.rSMS Parametros = new LOCASMS.rSMS();
               Parametros.Destinations = Destinatarios.ToArray();
              //  Parametros.JobDateTime = iDataEnvio; 

               // Determinando o Nome da CAmpanha
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

               MessageBox.Show("Erro ao enviar os SMS"+erro.Message);
           }

           return IRetorno.ToString();

       }
            
           
    }
}
