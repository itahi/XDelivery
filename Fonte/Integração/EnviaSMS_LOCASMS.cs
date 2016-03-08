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

        public string EnviaSMSLista(Array iNumero, string iUser, string iSennha, string iMensagem, string iNomeCampanha)
       {
           Array[] IRetorno = new Array[2];
           try
           {
               
               // Lista de Destinatários
               List<LOCASMS.Destination> Destinatarios = new List<LOCASMS.Destination>();

                foreach (String numeroDestinatario in iNumero)
                {
                    
                        Destinatarios.Add(new LOCASMS.Destination() { Number = numeroDestinatario });
                        // 
                    

                }
                // Carregando os Destinatarios do List View

                //LOCASMS.Destination dest = new LOCASMS.Destination();

                //    string iNumero = dataSet.Tables[0].Rows[i].Field<string>("Telefone");

                //    if (iNumero.Length==8)
                //    {
                //        iNumero = "279" + iNumero;
                //    }
                //    else
                //    {
                //        iNumero = "27" + iNumero;
                //    }
                //    dest.Number = iNumero;
                //    dest.Name = dataSet.Tables[0].Rows[i].Field<string>("Nome");
                //     Destinatarios.Add(new LOCASMS.Destination { dest });
                //   Destinatarios.Add(Destinatarios { Name = dataSet.Tables[0].Rows[i].Field<string>("Nome") });

                //foreach (String numeroDestinatario in ds)
                //{
                //    if (numeroDestinatario!=null)
                //    {
                //        Destinatarios.Add(new LOCASMS.Destination() { Number = numeroDestinatario });
                //       // 
                //     }

                //}

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
