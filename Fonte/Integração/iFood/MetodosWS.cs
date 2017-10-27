using DexComanda.Integração.iFood.Pedido;
using DexComanda.Models.IntegracaoIFood;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DexComanda.Integração.iFood
{

    public class MetodosWS
    {
        private RestClient cliente = new RestClient("https://pos-api.ifood.com.br/");
        private RestRequest request;
        private iFoodToken newObjetc;
        public List<iFoodEventos> newListEventos;
        public List<root> listRootPedido = new List<root>();
        public string strTokenIFood;
        public bool bLojaOnline = false;
        private Conexao con;
        public MetodosWS()
        {

        }
        /// <summary>
        /// Busca o Token no WS baseando-se no user e senha do iConnect
        /// </summary>
        public async void BuscaToken()
        {
            try
            {
                string strUserName = "POS-284683350";
                string strPwd = "POS-284683350";
                //string strUserName = RetornaDadosIntegracao().UserName;
                //string strPwd = RetornaDadosIntegracao().Senha;
                //if (strUserName== "")
                //{
                //    MessageBox.Show("Configure a senha do iConnect para continuar");
                //    return;
                //}
                //if (strPwd == "")
                //{
                //    MessageBox.Show("Configure a UserName do iConnect para continuar");
                //    return;
                //}
                request = new RestRequest("oauth/token", Method.POST);
                request.AddParameter("client_id", "xsistemas");
                request.AddParameter("client_secret", ")2@$v3JjH9");
                request.AddParameter("username", strUserName);
                request.AddParameter("password", strPwd);
                request.AddParameter("grant_type", "password");
                var response =  cliente.Execute(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    newObjetc = new iFoodToken();
                    return;
                }
                newObjetc = SimpleJson.DeserializeObject<iFoodToken>(response.Content);
                strTokenIFood = newObjetc.access_token;
            }
            catch (Exception erro)
            {
                newObjetc = new iFoodToken();
                MessageBox.Show(erro.Message);
            }
        }
        private Integracao_IFood RetornaDadosIntegracao()
        {
            try
            {
                con = new Conexao();
                DataSet ds = con.SelectAll("IntegracaoIFood", "spObter_Integracao");
                if (ds.Tables.Count==0)
                {
                    return new Integracao_IFood();
                }
                Integracao_IFood newIntegracao = new Integracao_IFood()
                {
                    UserName = ds.Tables[0].Rows[0].Field<string>("UserName"),
                    Senha = ds.Tables[0].Rows[0].Field<string>("Senha")
                };

                return newIntegracao;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
                return new Integracao_IFood();
            }
        }
        /// <summary>
        /// Faz uma varredura no WS para verificar se tem pedidos disponiveis
        /// Caso o retorno for diferente de 404(ridiculo) tem pedido 
        /// </summary>
        /// <param name="strToken">Token obtido a rota oauth/token</param>
        public async void VerificaPedidos(string strToken)
        {
            try
            {
                if (strToken=="")
                {
                    return;
                }
                request = new RestRequest("v1.0/events:polling", Method.GET);
                request.AddHeader("authorization", "bearer " + strToken);
                var response =  cliente.Execute(request);
               // bLojaOnline = response.StatusCode == System.Net.HttpStatusCode.OK;
                // caso retornou diferente de 404 tem alguma coisa
                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    newListEventos = new List<iFoodEventos>();
                    // Deserializa o objeto transtrando uma lista
                    SimpleJson.DeserializeObject<List<iFoodEventos>>(response.Content);
                    foreach (var eventos in SimpleJson.DeserializeObject<List<iFoodEventos>>(response.Content))
                    {
                        var item = new iFoodEventos()
                        {
                            code = eventos.code,
                            correlationId = eventos.correlationId,
                            createdAt = eventos.createdAt,
                            id = eventos.id
                        };
                        newListEventos.Add(item);
                    }
                    

                    //if (newListEventos.Count == 0)
                    //{
                    //    return;
                    //}
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        /// <summary>
        /// Captura os pedidos do iFood 
        /// </summary>
        /// <param name="strReference">Parametro correlationId de v1.0/events:polling</param>
        public List<root> CapturandoPedidos(string strReference)
        {
            try
            {
                request = new RestRequest("v1.0/orders/" + strReference, Method.GET);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response =  cliente.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    listRootPedido.Add(SimpleJson.DeserializeObject<root>(response.Content));
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            return listRootPedido;
        }
        public async void IntegraPedido(string strReference)
        {
            try
            {
                request = new RestRequest("v1.0/orders/"+strReference+ "/statuses/integration" + strReference, Method.POST);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response = await cliente.ExecuteTaskAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Não foi possivel integrar o Pedido " + response.Content + response.StatusDescription);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message );
            }
        }

        /// <summary>
        /// Remove pedidos da fila de processamento após integralizar o pedido
        /// </summary>
        /// <param name="strToken"></param>
        public async void ManipulaEventos(string strToken,List<iFoodEventos> listEvents)
        {
            try
            {
                if (strToken=="" || listEvents==null)
                {
                    return;
                }
                request = new RestRequest("v1.0/events/acknowledgment", Method.POST);
                request.AddHeader("authorization", "bearer " + strToken);
                request.AddJsonBody(MontaOjbetID(listEvents));
                var response = await cliente.ExecuteTaskAsync(request);

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        /// <summary>
        /// Recebe uma lista com os eventos de cada pedido para manipular
        /// </summary>
        /// <param name="list">iFoodEvents </param>
        /// <returns>Lista de 'id' dos eventos que representa cada pedido</returns>
        private List<iFoodId> MontaOjbetID(List<iFoodEventos> list)
        {
            List<iFoodId> newList = new List<iFoodId>();
            foreach (var item in list)
            {
                var teste = new iFoodId()
                {
                    id = item.id
                };
                newList.Add(teste);
            }

            return newList;
        }
        public async void ConfirmaPedido(string strReference)
        {
            try
            {
                request = new RestRequest("v1.0/orders/" + strReference + "/statuses/confirmation" + strReference, Method.POST);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response = await cliente.ExecuteTaskAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Não foi possivel Confirmar o Pedido " + response.Content + response.StatusDescription);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        public async void SaiuPraEntrega(string strReference)
        {
            try
            {
                request = new RestRequest("v1.0/orders/" + strReference + "/statuses/dispatch" + strReference, Method.POST);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response = await cliente.ExecuteTaskAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Não foi Mudar o status pra saiu p/ Entrega " + response.Content + response.StatusDescription);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        public async void Entregue(string strReference)
        {
            try
            {
                request = new RestRequest("v1.0/orders/" + strReference + "/statuses/delivery" + strReference, Method.POST);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response = await cliente.ExecuteTaskAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Não foi Mudar o status pra Entregue " + response.Content + response.StatusDescription);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        public async void Rejeitar(string strReference,string strMotivoRecusa)
        {
            try
            {
                request = new RestRequest("v1.0/orders/" + strReference + "/statuses/rejection" + strReference, Method.POST);
                request.AddBody("details", strMotivoRecusa);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response = await cliente.ExecuteTaskAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Não foi Mudar o status pra Entregue " + response.Content + response.StatusDescription);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
    }
}
