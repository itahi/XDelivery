using DexComanda.Integração;
using DexComanda.Integração.iFood;
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
        public frmAtivaIntegracao()
        {
            InitializeComponent();
        }
        private async void BuscaToken()
        {
            try
            {
                request = new RestRequest("oauth/token",Method.POST);
                request.AddParameter("client_id", "xsistemas");
                request.AddParameter("client_secret", ")2@$v3JjH9");
                request.AddParameter("username", "POS-284683350");
                request.AddParameter("password", "POS-284683350");
                request.AddParameter("grant_type", "password");
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
               request.AddHeader("authorization", "bearer "+ newObjetc.access_token);
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
                    }

                    if (newListEventos.Count==0)
                    {
                        return;
                    }

                    ManipulaEventos();
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
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
                var response=await cliente.ExecuteTaskAsync(request);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private List<iFoodId> MontaOjbetID (List<iFoodEventos> list)
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
