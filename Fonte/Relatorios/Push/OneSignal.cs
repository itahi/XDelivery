using DexComanda.Models.Operacoes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace DexComanda.Push
{
    public class OneSignal
    {
        private Conexao con;
        private string cUrlWs = "";
        private string iParamToken;
        /// <summary>
        /// Busca o idOneSignal do cliente no banco e envia o push para ele 
        /// </summary>
        /// <param name="iDCliente">
        /// Código do cliente no banco</param>
        /// <param name="iDMsg">
        /// Mensagem a ser enviada </param>
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
                    iTexto = "Seu pedido chegará em aproximadamente " + "60" + " minutos.";
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


            OneSignal one = new OneSignal();
            one.EnviaNotificacao(dsCliente.Tables[0].Rows[0].Field<string>("Nome"), iTexto, dsCliente.Tables[0].Rows[0].Field<string>("user_id"));
        }
        /// <summary>
        /// Envia push para um espeficico
        /// </summary>
        /// <param name="iTituloMsg">
        /// Titulo para msg</param>
        /// <param name="iTexto">
        /// Texto para ser exibido</param>
        public void EnviaNotificacao(string iTituloMsg, string iTexto,string iUserID)
        {
            try
            {
                var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
                request.KeepAlive = true;
                request.Method = "POST";
                request.ContentType = "application/json";
                string irul = Sessions.returnEmpresa.UrlServidor;
                request.Headers.Add("authorization", "Basic " + Utils.RetornaConfiguracaoPush().Pushauthorization);
                string appID = Utils.RetornaConfiguracaoPush().Pushapp_id;
                
                byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                              + "\"app_id\": \"" + appID + "\","
                                              + "\"headings\": {\"en\": \"" + iTituloMsg + "\"},"
                                              + "\"contents\": {\"en\": \"" + iTexto + "\"},"
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                 //   MessageBox.Show(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());

                }

                System.Diagnostics.Debug.WriteLine(responseContent);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        /// <summary>
        /// Envia o push para todos 
        /// </summary>
        /// <param name="iTituloMsg"></param>
        /// <param name="iTexto"></param>
        /// <param name="iUserID">
        /// Id do usuario </param>
        public bool EnviaNotificacao(string iTituloMsg,string iTexto)
        {
            Boolean iretur = false;
            try
            {
                var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
                request.KeepAlive = true;
                request.Method = "POST";
                request.ContentType = "application/json";
                string irul = Sessions.returnEmpresa.UrlServidor;
                string pushAut = Utils.RetornaConfiguracaoPush().Pushauthorization;
                string pushAppID = Utils.RetornaConfiguracaoPush().Pushapp_id;
                request.Headers.Add("authorization", "Basic "+ pushAut);



                byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                        + "\"app_id\": \"" + pushAppID + "\","
                                                        + "\"headings\": {\"en\": \"" + iTituloMsg + "\"},"
                                                        + "\"contents\": {\"en\": \"" + iTexto + "\"},"
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
                            MessageBox.Show("Parabéns " + notify.recipients.ToString() + " foram atingidos prepare-se para bombar nas vendas");
                            iretur = true;
                        }
                    }
                   
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());

                }

                System.Diagnostics.Debug.WriteLine(responseContent);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return iretur;
        }
        /// <summary>
        /// Envia push para uma lista de usuarios filtrados
        /// </summary>
        /// <param name="iTituloMsg"></param>
        /// <param name="iTexto"></param>
        /// <param name="iUserID">
        /// Lista de usuarios separados por ,(virgula)</param>
        public bool EnviaNotificacao(string iTituloMsg, string iTexto, List<string> iUserID,string iModoEntrega)
        {
            Boolean iReturn = false;
            try
            {
                var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
                request.KeepAlive = true;
                request.Method = "POST";
                request.ContentType = "application/json";
                string irul = Sessions.returnEmpresa.UrlServidor;
                request.Headers.Add("authorization", "Basic " + Utils.RetornaConfiguracaoPush().Pushauthorization);

                //Transforma os em uma lista separados por virgula
                string result = String.Join(",",iUserID.ToArray());

                byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                         + "\"app_id\": \""+ Utils.RetornaConfiguracaoPush().Pushapp_id + "\","
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
                            iReturn = true;
                        }
                    }

                  
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());

                    //MessageBox.Show(ex.Message);
                }

                System.Diagnostics.Debug.WriteLine(responseContent);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return iReturn;
        }
        public void AlteraStatusPedido(int iCodPedidows, int iStatus, int iIdCliente = 0)
        {
            try
            {
                GerarToken();
                RestClient client = new RestClient(cUrlWs);
                RestRequest request = new RestRequest("ws/pedidos/status", Method.POST);
                request.AddParameter("idPedido", iCodPedidows);
                request.AddParameter("idStatus", iStatus);
                request.AddParameter("token", iParamToken);
                RestResponse response = (RestResponse)client.Execute(request);
                OneSignal on = new OneSignal();

                on.BuscaCliente(iIdCliente, iStatus);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        private void GerarToken()
        {
            try
            {
                cUrlWs = Sessions.returnEmpresa.UrlServidor;
                iParamToken = Utils.CriptografarArquivo("xsistemas", false);
            }
            catch (Exception e)
            {

                MessageBox.Show("Geração do Token de validação " + e.Message);
            }

        }
    }
}
