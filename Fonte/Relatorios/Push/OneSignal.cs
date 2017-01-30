using DexComanda.Models.Operacoes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Push
{
   public class OneSignal
    {
        private Conexao con;
        public void BuscaCliente(int iDCliente, int iDMsg = 1)
        {
            con = new Conexao();
            DataSet dsCliente = con.SelectRegistroPorCodigo("Pessoa", "spObterPessoaPorCodigo", iDCliente);
            if (dsCliente.Tables[0].Rows.Count <= 0)
            {
                return;
            }
            string iTexto = "";
            switch (iDMsg)
            {
                case 1:
                    iTexto = "Seu pedido chegará em aproximadamente" + Sessions.returnConfig.PrevisaoEntrega + " minutos.";
                    break;
                case 2:
                    iTexto = "Opa seu pedido acaba de ser confirmado e impresso!";
                    break;
                case 3:
                    iTexto = "Seu Pedido está na cozinha, nas mãos de nossos mestre cuca";
                    break;
                case 4:
                    iTexto = "Seu pedido acabou de sair pra entrega, fique atento a buzina";
                    break;
                case 5:
                    iTexto = "Esperamos que tenha gostado, e além de voltar a pedir também nos deixe um comentário nas redes sociais";
                    break;
                

            }
            DataRow dRowCliente = dsCliente.Tables[0].Rows[0];
            string iNomeCliente, iUserId;
            iNomeCliente = dRowCliente.ItemArray.GetValue(1).ToString();
            iUserId = dRowCliente.ItemArray.GetValue(16).ToString();

            OneSignal one = new OneSignal();
            one.EnviaNotificacao(iNomeCliente, iTexto, iUserId);
        }
        /// <summary>
        /// Envia push para todos usuarios
        /// </summary>
        /// <param name="iTituloMsg">
        /// Titulo para msg</param>
        /// <param name="iTexto">
        /// Texto para ser exibido</param>
        public void EnviaNotificacao(string iTituloMsg, string iTexto,string iModoEntrega)
        {
            try
            {
                var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
                request.KeepAlive = true;
                request.Method = "POST";
                request.ContentType = "application/json";
                string irul = Sessions.returnEmpresa.UrlServidor;
                request.Headers.Add("authorization", "Basic " + Sessions.returnConfig.Pushauthorization);

                byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                        + "\"app_id\": \"" + Sessions.returnConfig.Pushapp_id + "\","
                                                        + "\"headings\": {\"en\": \"" + iTituloMsg + "\"},"
                                                        + "\"contents\": {\"en\": \"" + iTexto + "\"},"
                                                        + iModoEntrega
                                                        + "\"included_segments\": [\"All\"]}");



                string responseContent = null;

                try
                {
                    using (var writer = request.GetRequestStream())
                    {
                        writer.Write(byteArray, 0, byteArray.Length);
                    }

                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseContent = reader.ReadToEnd();
                        }
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Notificacao notify = new Notificacao();

                            notify = JsonConvert.DeserializeObject<Notificacao>(responseContent);

                        }
                    }

                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());

                }

                System.Diagnostics.Debug.WriteLine(responseContent);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        /// <summary>
        /// Envia o push para um usuário especifico
        /// </summary>
        /// <param name="iTituloMsg"></param>
        /// <param name="iTexto"></param>
        /// <param name="iUserID">
        /// Id do usuario </param>
        public void EnviaNotificacao(string iTituloMsg,string iTexto, string iUserID, string iModoEntrega)
        {
            try
            {
                var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
                request.KeepAlive = true;
                request.Method = "POST";
                request.ContentType = "application/json";
                string irul = Sessions.returnEmpresa.UrlServidor;
                request.Headers.Add("authorization", "Basic " + Sessions.returnConfig.Pushauthorization);
                //string byteArray2 = "{"
                //                                        + "\"app_id\": \"" + Sessions.returnConfig.Pushapp_id + "\","
                //                                        + "\"headings\": {\"en\": \"" + iTituloMsg + "\"},"
                //                                        + "\"contents\": {\"en\": \"" + iTexto + "\"},"
                //                                        + "\"include_player_ids\": [\"" + iUserID + "\"]}";
                
                byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                        + "\"app_id\": \"" + Sessions.returnConfig.Pushapp_id + "\","
                                                        + "\"headings\": {\"en\": \"" + iTituloMsg + "\"},"
                                                        + "\"contents\": {\"en\": \"" + iTexto + "\"},"
                                                        + iModoEntrega
                                                        + "\"include_player_ids\": [\"" + iUserID + "\"]}");



                string responseContent = null;

                try
                {
                    using (var writer = request.GetRequestStream())
                    {
                        writer.Write(byteArray, 0, byteArray.Length);
                    }

                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseContent = reader.ReadToEnd();
                        }
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Notificacao notify = new Notificacao();

                            notify = JsonConvert.DeserializeObject<Notificacao>(responseContent);

                        }
                    }
                   
                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());

                }

                System.Diagnostics.Debug.WriteLine(responseContent);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        /// <summary>
        /// Envia push para uma lista de usuarios filtrados
        /// </summary>
        /// <param name="iTituloMsg"></param>
        /// <param name="iTexto"></param>
        /// <param name="iUserID">
        /// Lista de usuarios separados por ,(virgula)</param>
        public void EnviaNotificacao(string iTituloMsg, string iTexto, List<string> iUserID,string iModoEntrega)
        {
            try
            {
                var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
                request.KeepAlive = true;
                request.Method = "POST";
                request.ContentType = "application/json";
                string irul = Sessions.returnEmpresa.UrlServidor;
                request.Headers.Add("authorization", "Basic " + Sessions.returnConfig.Pushauthorization);

                string result = String.Join(",",iUserID.ToArray());
                byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                         + "\"app_id\": \""+ Sessions.returnConfig.Pushapp_id+ "\","
                                         + "\"headings\": {\"en\": \"" + iTituloMsg + "\"},"
                                         + "\"contents\": {\"en\": \"" + iTexto + "\"},"
                                         + iModoEntrega
                                         + "\"include_player_ids\":["+ result + "]}");

                string responseContent = null;

                try
                {
                    using (var writer = request.GetRequestStream())
                    {
                        writer.Write(byteArray, 0, byteArray.Length);
                    }

                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseContent = reader.ReadToEnd();
                        }
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Notificacao notify = new Notificacao();

                            notify = JsonConvert.DeserializeObject<Notificacao>(responseContent);
                            MessageBox.Show(Bibliotecas.cMsgEnviadaOK + notify.recipients.ToString() + "usuários");
                        }
                    }

                  
                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());

                    //MessageBox.Show(ex.Message);
                }

                System.Diagnostics.Debug.WriteLine(responseContent);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
    }
}
