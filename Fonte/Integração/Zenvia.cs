using System;
using System.Net.Http;
namespace DexComanda.Integração
{
    /// <summary>
    /// Integação com a API 2.0 Rest do Zenvia serviço para envio de SMS Seguro 
    /// </summary>
    public class Zenvia
    {
        // private const string urlZenviaSendSMS = "https://api-rest.zenvia360.com.br/services/send-s";
        private const string BasicAutorization = "Basic eHNpc3RlbWFzLmVuZ2FnZTp4WXBDVjRYWHo0";
        public void IniciaServico()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Envio de sms Unico 
        /// </summary>
        /// <returns>
        /// Retorna objeto json com dados vindo do WS Zenvia</returns>
        private async void EnviaSMS(string iObjetoEnvio)
        {
            try
            {
                var baseAddress = new Uri("https://api-rest.zenvia360.com.br/services/send-sms");
                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic eHNpc3RlbWFzLmVuZ2FnZTp4WXBDVjRYWHo0");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                    using (var content = new StringContent("{  \"sendSmsRequest\":"+ iObjetoEnvio+"}",System.Text.Encoding.Default, "application/json"))
                    {
                        using (var response = await httpClient.PostAsync(baseAddress, content))
                        {
                            string responseData = await response.Content.ReadAsStringAsync();
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Envia sms para uma lista 
        /// </summary>
        /// <param name="iObjetoEnvio">
        /// Lista de objetos </param>
        public async void EnviaSMSLista(string iObjetoEnvio)
        {
            try
            {
                var baseAddress = new Uri("https://api-rest.zenvia360.com.br/services/send-sms-multiple");
                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic eHNpc3RlbWFzLmVuZ2FnZTp4WXBDVjRYWHo0");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    using (var content = new StringContent("{  \"sendSmsMultiRequest\": {    \"aggregateId\": 19627,    \"sendSmsRequestList\":" + iObjetoEnvio + "}}", System.Text.Encoding.Default, "application/json"))
                    {
                        using (var response = await httpClient.PostAsync(baseAddress, content))
                        {
                            string responseData = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
