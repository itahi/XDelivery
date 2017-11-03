using DexComanda.Integração;
using DexComanda.Integração.iFood;
using DexComanda.Integração.iFood.Pedido;
using DexComanda.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações
{
    public partial class frmAtivaIntegracao : Form
    {
        //private string iURliFood = "https://pos-api.ifood.com.br/";
        private RestClient cliente = new RestClient("https://pos-api.ifood.com.br/");
        private RestRequest request;
        private iFoodToken newObjetc;
        private List<iFoodEventos> newListEventos;
        private Conexao con;
        public frmAtivaIntegracao()
        {
            con = new Conexao();
            InitializeComponent();
        }
        private async void BuscaToken()
        {
            try
            {
                request = new RestRequest("oauth/token", Method.POST);
                request.AddParameter("client_id", "xsistemas");
                request.AddParameter("client_secret", ")2@$v3JjH9");
                request.AddParameter("username", "POS-284683350");
                request.AddParameter("password", "POS-284683350");
                request.AddParameter("grant_type", "password");
                // listBox1.Items.Add("Conectando a iFood.Com Aguarde");
                var response = await cliente.ExecuteTaskAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    newObjetc = new iFoodToken();
                    return;
                }
                newObjetc = SimpleJson.DeserializeObject<iFoodToken>(response.Content);
                Eventos();
            }
            catch (Exception)
            {
                newObjetc = new iFoodToken();
            }
        }

        private void btnAtivo_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;

        }
        private async void Eventos()
        {
            try
            {
                request = new RestRequest("v1.0/events:polling", Method.GET);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response = await cliente.ExecuteTaskAsync(request);

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
                        CapturandoPedidos(eventos.correlationId);
                    }

                    if (newListEventos.Count == 0)
                    {
                        return;
                    }

                    //  ManipulaEventos();
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
        private async void CapturandoPedidos(string strReference)
        {
            try
            {
                request = new RestRequest("v1.0/orders/" + strReference, Method.GET);
                //request.AddParameter("reference", strReference);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                var response = await cliente.ExecuteTaskAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return;
                }
                root iFoodRootList = new root();
                GravaPedidoDatabase(SimpleJson.DeserializeObject<root>(response.Content));

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        private void GravaPedidoDatabase(root iObjectRoot)
        {
            try
            {
                CadastraCliente(iObjectRoot.customer, iObjectRoot.deliveryAddress, iObjectRoot.deliveryAddress.coordinates);
            }
            catch (Exception erro)
            {

                throw;
            }
        }
        /// <summary>
        /// Confere se cliente já esta cadastrado no BD , caso sim ele atualiza
        /// </summary>
        /// <param name="iObjetc">Objeto Customer=Cliente basic</param>
        private void CadastraCliente(customer iObjetcPessoa, deliveryAddress iObjetEndereco, coordinates iObjeCood)
        {
            try
            {
                lock (this)
                {
                    timer1.Enabled = false;
                    string iNumTelefoneTratado;
                    int intCodPessoa = 0;
                    DataSet dsPessoa;
                    int intCodRegiao = con.RetornaCodRegiaoPorBairro(iObjetEndereco.neighborhood);
                    string iDDD = iObjetcPessoa.phone.Substring(0, 2);
                    iNumTelefoneTratado = Utils.ObterSomenteNumeros(iObjetcPessoa.phone).TrimEnd();
                    if (iNumTelefoneTratado.Length == 11)
                    {
                        iNumTelefoneTratado = iNumTelefoneTratado.Substring(2, 9);
                    }
                    else if (iNumTelefoneTratado.Length == 10)
                    {
                        iNumTelefoneTratado = iNumTelefoneTratado.Substring(2, 8);
                    }
                    else
                    {
                        iNumTelefoneTratado = iNumTelefoneTratado;
                    }

                    dsPessoa = con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", iNumTelefoneTratado);
                    if (dsPessoa.Tables[0].Rows.Count > 0)
                    {
                        intCodPessoa = dsPessoa.Tables[0].Rows[0].Field<int>("Codigo");
                    }


                    Pessoa newPessoa = new Pessoa()
                    {
                        Codigo = intCodPessoa,
                        Nome = iObjetcPessoa.name.ToUpper(),
                        Endereco = iObjetEndereco.streetName.ToUpper(),
                        Bairro = iObjetEndereco.neighborhood.ToUpper(),
                        Cidade = iObjetEndereco.city.ToUpper(),
                        Cep = iObjetEndereco.postalCode,
                        PontoReferencia = iObjetEndereco.reference.ToUpper(),
                        Telefone = iNumTelefoneTratado,
                        DDD = iDDD,
                        Telefone2 = "",
                        UF = iObjetEndereco.state.ToUpper(),
                        TicketFidelidade = 0,
                        CodRegiao = intCodRegiao,
                        DataCadastro = DateTime.Now,
                        DataNascimento = DateTime.Now,
                        Sexo = "1",
                        PFPJ = 'F',
                        CodOrigemCadastro = 1,
                        Observacao = iObjetEndereco.complement.ToUpper(),
                        Numero = iObjetEndereco.streetNumber,
                        user_id = "",
                        latitude = iObjeCood.latitude,
                        longitude = iObjeCood.longitude,
                        email = iObjetcPessoa.email
                    };
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
        }
        private void InsereAtualizaEndereco(int intCodPessoa,int intCodRegiao, deliveryAddress iObjetEnde)
        {
            try
            {
                int intCodEndereco = 0;
                DataSet dsEnde = con.ConsultaEnderecoPorCep(intCodPessoa, iObjetEnde.postalCode);
                if (dsEnde.Tables[0].Rows.Count > 0)
                {
                    intCodEndereco = dsEnde.Tables[0].Rows[0].Field<int>("Codigo");
                }
                Pessoa_Endereco pessEnd = new Pessoa_Endereco()
                {
                    Codigo = intCodEndereco,
                    CodPessoa = intCodPessoa,
                    Bairro = iObjetEnde.postalCode.ToUpper(),
                    Cep = iObjetEnde.postalCode,
                    Cidade = iObjetEnde.city.ToUpper(),
                    Endereco = iObjetEnde.streetName.ToUpper(),
                    CodRegiao = intCodRegiao,
                    NomeEndereco = "Principal",
                    Numero = iObjetEnde.streetNumber,
                    Complemento = iObjetEnde.complement.ToUpper(),
                    PontoReferencia = iObjetEnde.reference.ToUpper(),
                    UF = "ES"
                };
                if (intCodEndereco == 0)
                {
                    con.Insert("spAdicionarEndereco", pessEnd);
                }
                else
                {
                    con.Update("spAlterarEndereco", pessEnd);
                }
            }
            catch (Exception erro)
            {

                throw;
            }
        }
        /// <summary>
        /// Manipula os eventos Recebidos verifica os pedidos que foram confirmados
        /// </summary>
        private async void ManipulaEventos()
        {
            try
            {
                request = new RestRequest("v1.0/events/acknowledgment", Method.POST);
                request.AddHeader("authorization", "bearer " + newObjetc.access_token);
                request.AddJsonBody(MontaOjbetID(newListEventos));
                //listBox1.Items.Add("Verificando Pedido");
                var response = await cliente.ExecuteTaskAsync(request);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

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
        private void ExecutaTimer(object sender, EventArgs e)
        {
            // lock garante que cada timer será executado dentro da mesma transação 
            lock (this)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        ExecutaTimer(sender, e);
                    });
                    return;
                }

                Thread newTheadToken = new Thread(new ThreadStart(BuscaToken));
                newTheadToken.Start();
            }
        }

        // private async void ConfirmaPedidos()
    }
}
