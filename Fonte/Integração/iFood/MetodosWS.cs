using DexComanda.Integração.iFood.Pedido;
using DexComanda.Models;
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
        public List<PedidoTela> listPedidoTela = new List<PedidoTela>();
        public string strTokenIFood;
        public bool bLojaOnline = true;
        private Conexao con;
        private int prvCodEndereco;
        private int prvCodPedido;
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
                //string strUserName = "POS-284683350";
                //string strPwd = "POS-284683350";
                Integracao_IFood newDados = RetornaDadosIntegracao();
                string strUserName = newDados.UserName;
                string strPwd = newDados.Senha;
                if (strUserName == "")
                {
                    MessageBox.Show("Configure a senha do iConnect para continuar");
                    return;
                }
                if (strPwd == "")
                {
                    MessageBox.Show("Configure a UserName do iConnect para continuar");
                    return;
                }
                request = new RestRequest("oauth/token", Method.POST);
                request.AddParameter("client_id", "xsistemas");
                request.AddParameter("client_secret", ")2@$v3JjH9");
                request.AddParameter("username", strUserName);
                request.AddParameter("password", strPwd);
                request.AddParameter("grant_type", "password");
                var response = cliente.Execute(request);
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
                if (ds.Tables.Count == 0)
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
        public void VerificaPedidos(string strToken)
        {
            try
            {
                if (strToken == "")
                {
                    return;
                }
                request = new RestRequest("v1.0/events:polling", Method.GET);
                request.AddHeader("authorization", "bearer " + strToken);
                var response = cliente.Execute(request);

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
                }

            }
            catch (Exception erro)
            {
                bLojaOnline = false;
                MessageBox.Show(erro.Message);
            }
        }
        public void InserePedidoiFood(List<root> list)
        {
            try
            {
                con = new Conexao();
                foreach (var item in list)
                {
                    string iSql = "select * from Pedido_iFood where idPedido=" + item.reference;
                    if(con.SelectAll("Pedido_iFood", "", iSql).Tables[0].Rows.Count>0)
                    {
                        continue;
                    }

                    //if (item.code != "PLACED")
                    //{
                    //    return;
                    //}
                    PedidoiFood newPedido = new PedidoiFood()
                    {
                        Data = item.createdAt,
                        idPedido = item.reference,
                        status = "PLACED",
                        Cliente = item.customer.name,
                        Total = item.totalPrice
                    };
                    con.Insert("spAdicionarPedido_iFood", newPedido);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Captura os pedidos do iFood baseando no reference que é o id do pedido
        /// </summary>
        /// <param name="strReference">Parametro correlationId de v1.0/events:polling</param>
        public List<root> CapturandoPedidos(string strReference)
        {
            try
            {
                request = new RestRequest("v1.0/orders/" + strReference, Method.GET);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response = cliente.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    root newrootPedido = SimpleJson.DeserializeObject<root>(response.Content);
                    listRootPedido.Add(newrootPedido);
                    CadastraCliente(newrootPedido);
                }


            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
           // InserePedidoiFood(listRootPedido);
            return listRootPedido;
        }
        public async void IntegraPedido(string strReference)
        {
            try
            {
                request = new RestRequest("v1.0/orders/" + strReference + "/statuses/integration", Method.POST);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response = await cliente.ExecuteTaskAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    MessageBox.Show("Não foi possivel integrar o Pedido " + response.Content + response.StatusDescription);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        /// <summary>
        /// Remove pedidos da fila de processamento após integralizar o pedido
        /// </summary>
        /// <param name="strToken"></param>
        public async void ManipulaEventos(string strToken, List<iFoodEventos> listEvents)
        {
            try
            {
                if (strToken == "" || listEvents == null)
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
                request = new RestRequest("v1.0/orders/" + strReference + "/statuses/confirmation", Method.POST);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response = await cliente.ExecuteTaskAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    MessageBox.Show("Não foi possivel Confirmar o Pedido " + response.Content + response.StatusDescription);
                    return;
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
                request = new RestRequest("v1.0/orders/" + strReference + "/statuses/dispatch", Method.POST);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response = await cliente.ExecuteTaskAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    MessageBox.Show("Não foi Mudar o status pra saiu p/ Entrega " + response.Content + response.StatusDescription);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        /// <summary>
        /// Faz o parse no pedido para cadastrar o cliente na base local
        /// </summary>
        /// <param name="iRoot"></param>
        /// <returns>Retorna o ID cliente cadastrado na base local</returns>
        public int CadastraCliente(root iRoot)
        {
            int intCodPessoa = 0;
            try
            {
                lock (this)
                {
                    string iNumTelefoneTratado;
                    DataSet dsPessoa;
                    int intCodRegiao = con.RetornaCodRegiaoPorBairro(iRoot.deliveryAddress.neighborhood);
                    string iDDD = iRoot.customer.phone.Substring(0, 2);
                    iNumTelefoneTratado = Utils.ObterSomenteNumeros(iRoot.customer.phone).TrimEnd();
                    if (iNumTelefoneTratado.Length == 11)
                    {
                        iNumTelefoneTratado = iNumTelefoneTratado.Substring(2, 9);
                    }
                    else if (iNumTelefoneTratado.Length == 10)
                    {
                        iNumTelefoneTratado = iNumTelefoneTratado.Substring(2, 8);
                    }

                    dsPessoa = con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", iNumTelefoneTratado);
                    if (dsPessoa.Tables[0].Rows.Count > 0)
                    {
                        intCodPessoa = dsPessoa.Tables[0].Rows[0].Field<int>("Codigo");
                    }

                    Pessoa newPessoa = new Pessoa()
                    {
                        Codigo = intCodPessoa,
                        Nome = iRoot.customer.name.ToUpper(),
                        Endereco = iRoot.deliveryAddress.streetName.ToUpper(),
                        Bairro = iRoot.deliveryAddress.neighborhood.ToUpper(),
                        Cidade = iRoot.deliveryAddress.city.ToUpper(),
                        Cep = iRoot.deliveryAddress.postalCode,
                       
                        Telefone = iNumTelefoneTratado,
                        DDD = iDDD,
                        Telefone2 = "",
                        UF = iRoot.deliveryAddress.state.ToUpper(),
                        TicketFidelidade = 0,
                        CodRegiao = intCodRegiao,
                        DataCadastro = DateTime.Now,
                        DataNascimento = DateTime.Now,
                        Sexo = "1",
                        PFPJ = 'F',
                        CodOrigemCadastro = 1,
                        //Observacao = iRoot.deliveryAddress.complement.ToUpper(),
                        Numero = iRoot.deliveryAddress.streetNumber,
                        user_id = "",
                        latitude = iRoot.deliveryAddress.coordinates.latitude,
                        longitude = iRoot.deliveryAddress.coordinates.longitude,
                        email = iRoot.customer.email
                    };
                    if (iRoot.deliveryAddress.reference!=null)
                    {
                        newPessoa.PontoReferencia = iRoot.deliveryAddress.reference.ToUpper();
                    }
                    else
                    {
                        newPessoa.PontoReferencia = "";
                    }
                    if (iRoot.deliveryAddress.complement!=null)
                    {
                        newPessoa.Observacao = iRoot.deliveryAddress.complement.ToUpper();
                    }
                    else
                    {
                        newPessoa.Observacao = "";
                    }
                    if (intCodPessoa == 0)
                    {
                        con.Insert("spAdicionarClienteDelivery", newPessoa);
                        intCodPessoa = con.getLastCodigo();
                    }
                    else
                    {
                        con.Update("spAlterarPessoa", newPessoa);
                    }
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

            return intCodPessoa;
        }
        private int InsereAtualizaEndereco(int iCodPessoa,root iRootPedido)
        {
            int intReturnCodEnd = 0;
            int intCodRegiao = 0;
            try
            {
                DataSet ds = con.ConsultaEnderecoPorCep(iCodPessoa, iRootPedido.deliveryAddress.postalCode);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    prvCodEndereco = ds.Tables[0].Rows[0].Field<int>("CondEndereco");
                    intReturnCodEnd = prvCodEndereco;
                }
                intCodRegiao = con.RetornaCodRegiaoPorBairro(iRootPedido.deliveryAddress.neighborhood);
                Pessoa_Endereco pessEnd = new Pessoa_Endereco()
                {
                    Codigo = prvCodEndereco,
                    CodPessoa = iCodPessoa,
                    Bairro = iRootPedido.deliveryAddress.neighborhood,
                    Cidade = iRootPedido.deliveryAddress.city,
                    Endereco = iRootPedido.deliveryAddress.streetName,
                    CodRegiao = intCodRegiao,
                    NomeEndereco = "Principal",
                    Numero = iRootPedido.deliveryAddress.streetNumber,
                    Complemento = iRootPedido.deliveryAddress.complement,
                    PontoReferencia = iRootPedido.deliveryAddress.reference,
                    UF = iRootPedido.deliveryAddress.state
                };
                if (iRootPedido.deliveryAddress.postalCode == "" || iRootPedido.deliveryAddress.postalCode == null)
                {
                    pessEnd.Cep = "";
                }
                else
                {
                    pessEnd.Cep = iRootPedido.deliveryAddress.postalCode;
                }
                if (prvCodEndereco == 0)
                {
                    con.Insert("spAdicionarEndereco", pessEnd);
                    intReturnCodEnd =  con.getLastCodigo();
                }
                else
                {
                    con.Update("spAlterarEndereco", pessEnd);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return intReturnCodEnd;
        }
        private void CadastraPedido(string strReference, root iPedido)
        {
            try
            {
                con = new Conexao();
                if (con.SelectPedidoPorCodigoiFood(strReference).Tables["Pedido"].Rows.Count == 0)
                {
                    Models.Pedido newPedido = new Models.Pedido()
                    {
                        CodPessoa = CadastraCliente(iPedido),
                        TotalPedido = iPedido.totalPrice,
                        FormaPagamento = iPedido.payments[0].name,
                        RealizadoEm = iPedido.createdAt,
                        NumeroMesa = "0",
                        Status = "Aberto",
                        PedidoOrigem = "iFood",
                        DescontoValor = 0,
                        CodUsuario = 1,
                        CodigoPedidoWS = 0,
                        idiFood = iPedido.reference,
                        HorarioEntrega = "",
                        Observacao = "",
                       // CodEndereco = InsereAtualizaEndereco(newPedido.CodPessoa, iPedido)
                        Senha = "",
                        PagoFidelidade = false,
                        Cupom = "",
                        TrocoPara = iPedido.payments[0].changeFor
                    };
                    newPedido.CodEndereco = InsereAtualizaEndereco(newPedido.CodPessoa, iPedido);
                    if (iPedido.type == "DELIVERY")
                    {
                        newPedido.Tipo = "0 - Entrega";
                    }
                    else
                    {
                        newPedido.Tipo = "2 - Balcao";
                    }
                    con.Insert("spAdicionarPedido", newPedido);
                    prvCodPedido = con.getLastCodigo();
                    con.AlteraStatusPedido(prvCodPedido, 1, 1);
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
                request = new RestRequest("v1.0/orders/" + strReference + "/statuses/delivery", Method.POST);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response = await cliente.ExecuteTaskAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    MessageBox.Show("Não foi Mudar o status pra Entregue " + response.Content + response.StatusDescription);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        public async void Rejeitar(string strReference, string strMotivoRecusa)
        {
            try
            {
                request = new RestRequest("v1.0/orders/" + strReference + "/statuses/rejection", Method.POST);
                request.AddBody("details", strMotivoRecusa);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response = await cliente.ExecuteTaskAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
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
