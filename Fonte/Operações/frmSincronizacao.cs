using DexComanda.Models;
using DexComanda.Models.WS;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações
{
    public partial class frmSincronizacao : Form
    {
        private string iParamToken;
        private int store_idAtual = Sessions.returnEmpresa.Id_loja;
        private List<Grupo> listGrupos;
        private string iUrlWS;
        private Conexao con;
        private List<DadosApp> newDados;
        private string strMultisabores = "2";
        public frmSincronizacao()
        {
            InitializeComponent();
            con = new Conexao();
            listGrupos = new List<Grupo>();
        }
        private void GerarToken()
        {

            iParamToken = Convert.ToString(DateTime.Now).Replace("/", "").Replace(":", "").Replace(" ", "").Substring(0, 11) + "Adminx";
            //if (Sessions.returnEmpresa.Nome == "NOME DA SUA EMPRESA" || 
            //    Sessions.returnEmpresa.CNPJ== "23267492000018" || Sessions.returnEmpresa.CNPJ == Bibliotecas.cGaleto|| 
            //    Sessions.returnEmpresa.CNPJ== Bibliotecas.cCarangoVix || Sessions.returnEmpresa.CNPJ== Bibliotecas.cTropicalExpress
            //   || Sessions.returnEmpresa.CNPJ == Bibliotecas.cAcaiFood
            //    )
            //{
            //    iParamToken = Utils.CriptografarArquivo("xsistemas");
            //}
            //else
            //{
            //    iParamToken = Utils.CriptografarArquivo(iParamToken);
            //}

            // iParamToken = Utils.CriptografarArquivo(iParamToken);
            iParamToken = Utils.CriptografarArquivo("xsistemas");
            iParamToken = iParamToken.ToLower();
        }

        private void Sincroniza()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(Sincroniza));
                return;
            }

            iUrlWS = Sessions.returnEmpresa.UrlServidor;
            GerarToken();
            try
            {
                //CadastraPrevisao();
                if (chkFPagamento.Checked)
                {
                    // Envia as formas de pagamento ao site , enviando a imagem da bandeira 
                    CadastraFormaPagamento(ObterDados("FormaPagamento"));
                }

                if (chkProdutos.Checked)
                {
                    if (chkLink.Checked)
                    {
                        if (Utils.MessageBoxQuestion("Essa operação irá sincronizar todos produtos de seu banco de dados para o servidor Online , deseja continuar?"))
                        {
                            if (Utils.ImputStringQuestion())
                            {
                                con.AtualizaDataSincronismo("Grupo", -1, "DataAlteracao");
                                con.AtualizaDataSincronismo("Produto_OpcaoTipo", -1, "DataAlteracao");
                                con.AtualizaDataSincronismo("Opcao", -1, "DataAlteracao");
                                con.AtualizaDataSincronismo("Produto", -1, "DataAlteracao");
                                con.AtualizaDataSincronismo("Produto_Opcao", -1, "DataAlteracao");
                                LimparUrlAmigaveis();
                            }
                        }
                    }

                    // Sincronizar Grupos
                    CadastraCategorias(ObterDados("Grupo"));
                    // Sincronizar Tipo Opcao
                    CadastrarTipoOpcao(ObterDados("Produto_OpcaoTipo"));
                    // Sincronizar Opcao
                    CadastrarOpcao(ObterDados("Opcao"));
                    // Sincronizar Produtos
                    CadastrarProduto(ObterDados("Produto"));
                }
                if (chkRegiaoEntrega.Checked)
                {
                    CadastraRegioes(con.RetornaRegiao());
                }
                if (chkMobile.Checked)
                {
                    CadastraPush();
                    CadastraLinkApp(true);
                }

                if (txtVlrMinimo.Text != "" && txtVlrMinimo.Text != "0")
                {
                    CadastraPedidoMinimo();
                }

                if (chkHorarios.Checked)
                {
                    CadastrarHorarios(con.Select_Empresa_HorarioEntrega("Empresa_HorarioEntrega"));
                }
                if (chkMesas.Checked)
                {
                    CadastraMesas();
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao sincronizar " + erro.Message);
            }
        }
        private void CadastraPush()
        {
            try
            {
                GerarToken();
                RestClient client = new RestClient(iUrlWS);
                RestResponse response = new RestResponse();
                RestRequest request = new RestRequest("ws/loja/setOpenSignalId", Method.POST);
                request.AddParameter("token", iParamToken);
                request.AddParameter("open_signal_id", Utils.RetornaConfiguracaoPush().Pushapp_id);
                request.AddParameter("store_id", "0");
                request.AddParameter("nome_cliente", Sessions.returnEmpresa.Nome);
                request.AddParameter("gcm_sender_id", Utils.RetornaConfiguracaoPush().GCM);
                request.AddParameter("onesignal_api_key", Utils.RetornaConfiguracaoPush().Pushauthorization);
                request.AddParameter("store_id", store_idAtual);
                MudaLabel("Códigos OneSignal");
                response = (RestResponse)client.Execute(request);
                ReturnPadrao lRetorno = new ReturnPadrao();
                lRetorno = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel enviados os dados do OneSignal " + erro.Message);
            }


        }
        private void LimparUrlAmigaveis()
        {
            try

            {
                GerarToken();
                RestClient client = new RestClient(iUrlWS);
                RestResponse response = new RestResponse();
                RestRequest request = new RestRequest("ws/urlamigaveis/deleteall", Method.POST);
                request.AddParameter("token", iParamToken);
                MudaLabel("Links amigaveis");
                response = (RestResponse)client.Execute(request);
                ReturnPadrao lRetorno = new ReturnPadrao();
                lRetorno = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);

            }
            catch (Exception er)
            {

                MessageBox.Show("Erro LimparUrlAmigaveis" + er.Message + er.InnerException);
            }
        }
        private void CadastraLinkApp(Boolean iLimpar = false)
        {
            try
            {
                newDados = new List<DadosApp>();
                newDados = JsonConvert.DeserializeObject<List<DadosApp>>(Sessions.returnConfig.DadosApp);
                RestClient client = new RestClient(iUrlWS);
                RestResponse response = new RestResponse();
                RestRequest request = new RestRequest("ws/loja/baixarrApp", Method.POST);

                if (iLimpar)
                {
                    string strPlataformas = Utils.GravaJson("android", "");
                    newDados = JsonConvert.DeserializeObject<List<DadosApp>>(strPlataformas);
                    foreach (var item in newDados)
                    {
                        request.AddParameter("token", iParamToken);
                        request.AddParameter("plataforma", item.plataforma);
                        request.AddParameter("urlBaixarApp", item.url);
                        request.AddParameter("store_id", store_idAtual);
                        MudaLabel("Link APP");
                        response = (RestResponse)client.Execute(request);
                        prgBarMobile.Increment(1);
                        ReturnPadrao lRetorno = new ReturnPadrao();
                        lRetorno = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);
                    }
                }
                prgBarMobile.Value = 0;
                prgBarMobile.Maximum = newDados.Count;
                foreach (DadosApp item in newDados)
                {
                    request.AddParameter("token", iParamToken);
                    request.AddParameter("plataforma", item.plataforma);
                    request.AddParameter("urlBaixarApp", item.url);
                    MudaLabel("Link APP");
                    response = (RestResponse)client.Execute(request);
                    prgBarMobile.Increment(1);
                    ReturnPadrao lRetorno = new ReturnPadrao();
                    lRetorno = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);
                }
            }
            catch (Exception er)
            {

                MessageBox.Show("Erro ao cadastrar Link App " + er.Message + er.InnerException);
            }
        }
        private void CadastraPedidoMinimo()
        {
            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/pedido/minimo", Method.POST);
            request.AddParameter("token", iParamToken);
            request.AddParameter("valor", txtVlrMinimo.Text);
            request.AddParameter("store_id", store_idAtual);
            MudaLabel("Pedido Mínimo");
            RestResponse response = (RestResponse)client.Execute(request);
            ReturnPadrao lRetorno = new ReturnPadrao();
            lRetorno = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);
            lblMinimo.Visible = true;
            lblMinimo.Text = lRetorno.msg;
        }

        private DataSet ObterDados(string iNomeTable)
        {
            if (iNomeTable != "FormaPagamento")
            {
                return con.SelectRegistroONline(iNomeTable);
            }
            //else if (iNomeTable== "Empresa_HorarioEntrega")
            //{
            //    return con.Select_Empresa_HorarioEntrega(iNomeTable);
            //}
            else
            {
                return con.SelectRegistroONlineSemData(iNomeTable);
            }

        }

        private void CadastrarTipoOpcao(DataSet ds)
        {
            try
            {

                int iMaxOpcionais = 0; int iMinumum = 0;
                MudaLabel("Tipo de Opção");
                DataRow dRow;
                prgBarProduto.Value = 0;
                prgBarProduto.Maximum = ds.Tables[0].Rows.Count;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    RestClient client = new RestClient(iUrlWS);
                    RestRequest request = new RestRequest("ws/opcao/tipo/set", Method.POST);
                    dRow = ds.Tables[0].Rows[i];

                    iMaxOpcionais = int.Parse(dRow.ItemArray.GetValue(3).ToString());
                    iMinumum = int.Parse(dRow.ItemArray.GetValue(4).ToString());
                    request.AddParameter("token", iParamToken);
                    request.AddParameter("nome", dRow.ItemArray.GetValue(1).ToString());
                    request.AddParameter("tipo", dRow.ItemArray.GetValue(2).ToString());
                    request.AddParameter("referenciaId", dRow.ItemArray.GetValue(0).ToString());
                    request.AddParameter("ordenExibicao", dRow.ItemArray.GetValue(5).ToString());
                    request.AddParameter("minimoSelecao", iMinumum);
                    request.AddParameter("maximoSelecao", iMaxOpcionais);
                    request.AddParameter("store_id", store_idAtual);
                    RestResponse response = (RestResponse)client.Execute(request);

                    ReturnPadrao lRetorno = new ReturnPadrao();
                    lRetorno = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);

                    prgBarProduto.Increment(i + 1);
                    if (lRetorno.status == true)
                    {
                        con.AtualizaDataSincronismo("Produto_OpcaoTipo", int.Parse(dRow.ItemArray.GetValue(0).ToString()));
                    }
                }
            }
            catch (Exception er)
            {

                MessageBox.Show("Erro ao cadastrarar Produto_OpcaoTipo " + er.Message + er.InnerException);
            }

        }

        private void CadastrarBanner(string iBanner)
        {
            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/banner/promocao/set", Method.POST);
            request.AddParameter("token", iParamToken);
            request.AddFile("imagem", iBanner);
            // request.AddFile("imagem", ConverterFotoEmByte(iBanner), "photo.jpg", "image/pjpeg");

            RestResponse response = (RestResponse)client.Execute(request);

            ReturnPadrao lRetorno = new ReturnPadrao();
            lRetorno = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);
            //  lblReturn.Visible = lRetorno.status;
            lbRetornoImage.Visible = true;
            lbRetornoImage.Text = lRetorno.msg;

        }
        private void CadastrarDesconto(double iValorDescont = 0)
        {

            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/total/desconto", Method.POST);
            request.AddParameter("token", iParamToken);
            request.AddParameter("status", Convert.ToInt16(chkDesconto.Checked));
            int iTotalOrSub;
            if (rbSub.Checked)
            {
                iTotalOrSub = 2;
            }
            else
            {
                iTotalOrSub = 1;
            }
            request.AddParameter("ordemExibicao", iTotalOrSub);
            string iFormasPagamento = "1";
            if (chkDinheiro.Checked)
            {
                iFormasPagamento = "1";
            }
            if (chkCartao.Checked)
            {
                iFormasPagamento = "2";
            }
            if (chkCartao.Checked && chkDinheiro.Checked)
            {
                iFormasPagamento = "1,2";
            }
            request.AddParameter("formasPagamento", iFormasPagamento);
            if (txtPercentualDesconto.Text != "")
            {
                request.AddParameter("percentualDesconto", int.Parse(txtPercentualDesconto.Text));
            }
            else
            {
                request.AddParameter("percentualDesconto", 0);
            }

            RestResponse response = (RestResponse)client.Execute(request);

            ReturnPadrao lRetorno = new ReturnPadrao();
            lRetorno = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);
            //  lblReturn.Visible = lRetorno.status;
            lblReturn.Visible = true;
            lblReturn.Text = lRetorno.msg;
        }

        private void CadastraRegioes(DataSet ds)
        {
            try
            {
                MudaLabel("Regioes de Entrega");
                prgBarRegiao.Maximum = ds.Tables[0].Rows.Count;
                RestClient client = new RestClient(iUrlWS);
                RestRequest request = new RestRequest("ws/regiaoEntrega/set", Method.POST);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    request.AddParameter("token", iParamToken);
                    // request.AddParameter("store_id", "1");
                    request.AddParameter("cep", ds.Tables["RegiaoEntrega"].Rows[i].Field<string>("CEP"));
                    request.AddParameter("nome", ds.Tables["RegiaoEntrega"].Rows[i].Field<string>("NomeRegiao"));
                    request.AddParameter("valor", ds.Tables["RegiaoEntrega"].Rows[i].Field<decimal>("TaxaServico"));
                    request.AddParameter("referencia_id", ds.Tables["RegiaoEntrega"].Rows[i].Field<int>("Codigo"));
                    request.AddParameter("ativo", Convert.ToInt16(ds.Tables["RegiaoEntrega"].Rows[i].Field<Boolean>("OnlineSN")));
                    request.AddParameter("store_id", store_idAtual);
                    if (ds.Tables["RegiaoEntrega"].Rows[i].Field<string>("PrevisaoEntrega") != "0")
                    {
                        request.AddParameter("previsao_entrega", ds.Tables["RegiaoEntrega"].Rows[i].Field<string>("PrevisaoEntrega"));
                    }
                    if (ds.Tables["RegiaoEntrega"].Rows[i].Field<decimal>("valorMinimoFreteGratis") > 0)
                    {
                        request.AddParameter("valorMinimoFreteGratis", Convert.ToInt16(ds.Tables["RegiaoEntrega"].Rows[i].Field<decimal>("valorMinimoFreteGratis")));
                    }

                    RestResponse response = (RestResponse)client.Execute(request);
                    //ReturnPadrao lReturn = new ReturnPadrao();
                    //lReturn = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);

                    prgBarRegiao.Value = i + 1;

                    if (response.Content.Contains(" true"))
                    {
                        con.AtualizaDataSincronismo("RegiaoEntrega_Bairros", ds.Tables[0].Rows[i].Field<int>("Codigo"), "DataSincronismo", "CodRegiao");
                    }


                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message + "Erro ao cadastrar/sincronizar Regiões de Entrega ");
            }

        }
        private void CadastrarOpcao(DataSet ds)
        {
            try
            {

                prgBarProduto.Value = 0;
                prgBarProduto.Maximum = ds.Tables[0].Rows.Count;
                DataRow dRow;
                MudaLabel("Opções");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    RestClient client = new RestClient(iUrlWS);
                    RestRequest request = new RestRequest("ws/opcoes/set", Method.POST);
                    dRow = ds.Tables[0].Rows[i];
                    request.AddParameter("token", iParamToken);
                    request.AddParameter("tipo", dRow.ItemArray.GetValue(2).ToString());
                    request.AddParameter("nome", dRow.ItemArray.GetValue(1).ToString());
                    request.AddParameter("referenciaId", dRow.ItemArray.GetValue(0).ToString());
                    request.AddParameter("dias_exibicao", dRow.ItemArray.GetValue(8).ToString());
                    request.AddParameter("store_id", store_idAtual);
                    RestResponse response = (RestResponse)client.Execute(request);
                    prgBarProduto.Increment(i + 1);

                    if (response.Content.ToString() == "true")
                    {
                        con.AtualizaDataSincronismo("Opcao", int.Parse(dRow.ItemArray.GetValue(0).ToString()));
                    }

                }
            }
            catch (Exception er)
            {

                MessageBox.Show("Erro ao cadastrar Opcao" + er.Message + er.InnerException);
            }

        }
        private void MudaLabel(string iNomeTabela)
        {
            Application.DoEvents();
            lblSinc.Visible = true;
            lblSinc.Text = "Sincronizando " + iNomeTabela;
        }
        private void CadastraCategorias(DataSet ds)
        {
            try
            {
                //  ManipulaProgressBar(ds.Tables[0].Rows.Count);
                DataRow dRow;
                MudaLabel("Grupo");
                GerarToken();
                prgBarProduto.Maximum = 0;
                prgBarProduto.Maximum = ds.Tables[0].Rows.Count;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    RestClient client = new RestClient(iUrlWS);
                    RestRequest request = new RestRequest("ws/categorias/set", Method.POST);
                    dRow = ds.Tables[0].Rows[i];
                    string inome;
                    int iCod;
                    int AtivoSN = 0;
                    string idReferenciaCategoriaPai = "0";
                    iCod = int.Parse(dRow.ItemArray.GetValue(0).ToString());
                    if (dRow.ItemArray.GetValue(8).ToString() != "")
                    {
                        idReferenciaCategoriaPai = dRow.ItemArray.GetValue(8).ToString();
                    }

                    inome = dRow.ItemArray.GetValue(1).ToString();

                    if (Convert.ToBoolean(dRow.ItemArray.GetValue(3).ToString()) == true)
                    {
                        AtivoSN = 1;
                    }
                    request.AddParameter("idReferenciaCategoriaPai", idReferenciaCategoriaPai);
                    request.AddParameter("token", iParamToken);
                    request.AddParameter("nomeCategoria", inome);
                    request.AddParameter("ativo", AtivoSN);
                    request.AddParameter("idReferencia", iCod);
                    request.AddParameter("store_id", store_idAtual);
                    RestResponse response = (RestResponse)client.Execute(request);
                    prgBarProduto.Increment(i + 1);

                    if (response.Content.Contains("true"))
                    {
                        con.AtualizaDataSincronismo("Grupo", iCod);
                    }

                }

            }
            catch (Exception er)
            {

                MessageBox.Show("Erro ao cadastrarar Grupo" + er.Message + er.InnerException);
            }

        }
        private List<MesaWS> MontaObjetoMesa(DataSet ds)
        {
            List<MesaWS> listmesas = new List<MesaWS>();
            prgBarMesa.Maximum = ds.Tables[0].Rows.Count;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var mesas = new MesaWS()
                {
                    id = ds.Tables[0].Rows[i].Field<int>("Codigo"),
                    name = ds.Tables[0].Rows[i].Field<string>("NumeroMesa"),
                    status = Convert.ToInt16(ds.Tables[0].Rows[i].Field<string>("StatusMesa"))
                };
                listmesas.Add(mesas);

            }
            return listmesas;
        }


        private void CadastraMesas()
        {
            try
            {
                int iCod = 0;
                DataRow dRow;
                RestClient client = new RestClient(iUrlWS);
                RestRequest request = new RestRequest("ws/v1/lojas/1/mesas", Method.POST);
                request.AddHeader("token", iParamToken);
                request.AddHeader("store_id", store_idAtual.ToString());
                DataSet ds = ObterDados("Mesas");
                request.AddJsonBody(MontaObjetoMesa(ds));
                var restResponse = client.Execute(request);
                ConfirmaObjeto(restResponse.Content, ds, "Mesas");
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        private void ConfirmaObjeto(string strObjetRetur, DataSet dsObjetEnvio, string strNomeTable)
        {
            try
            {
                ObjetoRetornoWSBLL list = JsonConvert.DeserializeObject<ObjetoRetornoWSBLL>(strObjetRetur);
                int[] lista = list.id;
                if (list.status == true)
                {
                    for (int i = 0; i < dsObjetEnvio.Tables[0].Rows.Count; i++)
                    {
                        con.AtualizaDataSincronismo(strNomeTable, int.Parse(dsObjetEnvio.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()));
                        prgBarMesa.Increment(1);
                    }
                }


            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        private void CadastrarHorarios(DataSet ds)
        {
            try
            {

                //  ManipulaProgressBar(ds.Tables[0].Rows.Count);
                DataRow dRow;
                MudaLabel("Horarios Entrega");
                GerarToken();
                prgBarHorarios.Maximum = 0;
                prgBarHorarios.Maximum = ds.Tables[0].Rows.Count;
                List<HorariosEntregaJson> horariosList = new List<HorariosEntregaJson>();
                RestClient client = new RestClient(iUrlWS);
                RestRequest request = new RestRequest("ws/loja/horarioEntrega", Method.POST);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dRow = ds.Tables[0].Rows[i];
                    int AtivoSN = 0;
                    if (Convert.ToBoolean(dRow.ItemArray.GetValue(3).ToString()) == true)
                    {
                        AtivoSN = 1;
                    }

                    HorariosEntregaJson horariosJson = new HorariosEntregaJson()
                    {
                        referencia_id = int.Parse(dRow.ItemArray.GetValue(0).ToString()),
                        ativo = AtivoSN,
                        horario_entrega = dRow.ItemArray.GetValue(2).ToString(),
                        limite_horario_pedido = dRow.ItemArray.GetValue(1).ToString(),
                    };
                    horariosList.Add(horariosJson);
                    prgBarHorarios.Increment(i + 1);
                }
                if (horariosList.Count <= 0)
                {
                    MessageBox.Show("Não há horarios cadastrados para sincronizar");
                    return;
                }
                string iretur = Utils.SerializaObjeto(horariosList);
                request.AddParameter("horarios", Utils.SerializaObjeto(horariosList));
                request.AddParameter("store_id", store_idAtual);
                RestResponse response = (RestResponse)client.Execute(request);

            }
            catch (Exception er)
            {

                MessageBox.Show("Erro ao cadastrarar Grupo" + er.Message + er.InnerException);
            }

        }

        private void CadastrarProduto(DataSet ds)
        {
            try
            {
                MudaLabel("Produto");
                DataRow dRow;
                prgBarProduto.Value = 0;
                prgBarProduto.Maximum = ds.Tables[0].Rows.Count;
                if (Sessions.returnEmpresa.CNPJ == Bibliotecas.cTioPaulinho)
                {
                    strMultisabores = "4";
                }
                //= Sessions.returnEmpresa.CNPJ == Bibliotecas.cTioPaulinho;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    RestClient client = new RestClient(iUrlWS);
                    RestRequest request = new RestRequest("ws/produto/set", Method.POST);
                    request.AddParameter("token", iParamToken);
                    dRow = ds.Tables[0].Rows[i];
                    decimal prProduto = decimal.Parse(dRow.ItemArray.GetValue(3).ToString());
                    DateTime dtSinc = DateTime.Now.AddYears(1);
                    DateTime dtFoto = DateTime.Now.AddYears(1);

                    if (dRow.ItemArray.GetValue(9).ToString() != "")
                    {
                        dtSinc = Convert.ToDateTime(dRow.ItemArray.GetValue(9).ToString());
                    }
                    else
                    {
                        dtSinc = DateTime.Now.AddYears(-1);
                    }
                    if (dRow.ItemArray.GetValue(14).ToString() != "")
                    {
                        dtFoto = Convert.ToDateTime(dRow.ItemArray.GetValue(14).ToString());
                    }


                    string iCaminhoImagem = dRow.ItemArray.GetValue(11).ToString();
                    request.AddParameter("token", iParamToken);
                    request.AddParameter("idReferencia", dRow.ItemArray.GetValue(0).ToString());
                    request.AddParameter("nome", dRow.ItemArray.GetValue(1).ToString());
                    request.AddParameter("dias_exibicao", ds.Tables[0].Rows[i].Field<string>("DiaDisponivelSite"));
                    request.AddParameter("keyWords", ds.Tables[0].Rows[i].Field<string>("PalavrasChaves"));
                    request.AddParameter("store_id", store_idAtual);
                    //Caso o grupo esteja marcado como multi sabores ele enviará esse parametro
                    if (ds.Tables[0].Rows[i].Field<int>("MultiploSabores") == 1)
                    {
                        request.AddParameter("multi_product_id", dRow.ItemArray.GetValue(15).ToString());
                        request.AddParameter("multi_product_max", strMultisabores);
                    }
                    if (Sessions.returnEmpresa.CNPJ == "09395874000160")
                    {
                        prProduto = prProduto + con.RetornaPrecoComEmbalagem(dRow.ItemArray.GetValue(4).ToString(), int.Parse(dRow.ItemArray.GetValue(0).ToString()));
                    }
                    else
                    {
                        prProduto = decimal.Parse(dRow.ItemArray.GetValue(3).ToString());
                    }

                    request.AddParameter("preco", prProduto);
                    decimal prPromocao = decimal.Parse(dRow.ItemArray.GetValue(5).ToString());
                    if (dRow.ItemArray.GetValue(13).ToString() != "")
                    {
                        if (Convert.ToDateTime(dRow.ItemArray.GetValue(13).ToString()) > DateTime.Now && prPromocao > 0)
                        {
                            request.AddParameter("precoPromocao", prPromocao);
                            request.AddParameter("dataInicial", dRow.ItemArray.GetValue(12).ToString());
                            request.AddParameter("dataFinal", dRow.ItemArray.GetValue(13).ToString());
                        }
                    }

                    if (File.Exists(iCaminhoImagem) && dtFoto > dtSinc)
                    {
                        request.AddFile("imagem", iCaminhoImagem);
                    }
                    request.AddParameter("idReferenciaCategoria", dRow.ItemArray.GetValue(15).ToString());
                    // request.AddParameter("idReferenciaCategoria", RetornaIDCategoria(dRow.ItemArray.GetValue(4).ToString()));
                    request.AddParameter("descricao", dRow.ItemArray.GetValue(2).ToString());
                    int bAtivoSn = 0;
                    if (Convert.ToBoolean(dRow.ItemArray.GetValue(7).ToString()) == true)
                    {
                        bAtivoSn = 1;
                    }
                    request.AddParameter("ativo", bAtivoSn);
                    request.AddParameter("maxOptions", dRow.ItemArray.GetValue(10).ToString());
                    request.AddParameter("pontos_fidelidade", dRow.ItemArray.GetValue(22).ToString());
                    request.AddParameter("pontos_para_troca", dRow.ItemArray.GetValue(23).ToString());
                    prgBarProduto.Increment(i + 1);
                    RestResponse response = (RestResponse)client.Execute(request);

                    if (response.Content.Contains("true"))
                    {
                        con.AtualizaDataSincronismo("Produto", int.Parse(dRow.ItemArray.GetValue(0).ToString()));
                        CadastrarOpcaoProduto(int.Parse(dRow.ItemArray.GetValue(0).ToString()));
                    }
                    iCaminhoImagem = "";
                }
            }
            catch (Exception er)
            {

                MessageBox.Show("Erro ao cadastrarar Produto " + er.Message + er.InnerException);
            }

        }
        private System.IO.Stream RetornaArquivo(string iCaminho)
        {
            // convert string to stream
            byte[] byteArray = Encoding.UTF8.GetBytes(iCaminho);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;
        }
        private int RetornaIDCategoria(string iNomeCategoria)
        {
            int iIDReturn = 1;
            DataSet dsGrupo = con.SelectRegistroPorNome("@Nome", "Grupo", "spObterGrupoPorNome", iNomeCategoria);
            if (dsGrupo.Tables[0].Rows.Count > 0)
            {
                DataRow dRowProduto = dsGrupo.Tables[0].Rows[0];
                iIDReturn = int.Parse(dRowProduto.ItemArray.GetValue(0).ToString());
            }

            return iIDReturn;
        }

        private void ManipulaProgressBar(int imax)
        {
            //prgBarProduto.Value = 0;
            //prgBarProduto.Maximum = imax;
        }
        private void CadastrarOpcaoProduto(int iCodProduto)
        {
            try
            {
                DataSet ds = con.SelectRegistroPorCodigo("Produto_Opcao", "spObterOpcaoProdutoCodigo", iCodProduto);
                int iCodOpcao = 0;
                MudaLabel("Opcoes/Adicionais");
                prgBarProduto.Value = 0;
                prgBarProduto.Maximum = ds.Tables[0].Rows.Count;

                DataRow dRow;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    RestClient client = new RestClient(iUrlWS);
                    RestRequest request = new RestRequest("ws/produto/opcao/set", Method.POST);
                    int iCodProd = ds.Tables["Produto_Opcao"].Rows[0].Field<int>("CodProduto");
                    string[] opcao = new string[ds.Tables[0].Rows.Count];
                    request.AddParameter("token", iParamToken);
                    request.AddParameter("referenciaId", iCodProd);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //if (ds.Tables[0].Rows[i].Field<Boolean>("OnlineSN"))
                        //{
                        dRow = ds.Tables[0].Rows[i];
                        DateTime dtFimPromo = Convert.ToDateTime(dRow.ItemArray.GetValue(7).ToString());
                        decimal iprice = 0;
                        iCodOpcao = int.Parse(dRow.ItemArray.GetValue(1).ToString());
                        if (dtFimPromo >= Convert.ToDateTime(DateTime.Now.ToShortDateString()) && decimal.Parse(dRow.ItemArray.GetValue(6).ToString()) > 0)
                        {
                            iprice = decimal.Parse(dRow.ItemArray.GetValue(6).ToString());
                        }
                        else
                        {
                            iprice = decimal.Parse(dRow.ItemArray.GetValue(2).ToString());
                        }

                        request.AddParameter("opcao[" + iCodOpcao + "]", iprice);
                        //}
                        prgBarProduto.Increment(i + 1);
                    }

                    RestResponse response = (RestResponse)client.Execute(request);

                    if (response.Content.Equals(true))
                    {
                        con.AtualizaDataSincronismo("Produto_Opcao", iCodProd, iCodOpcao);
                    }
                    //else
                    //{
                    //    Utils.CriaArquivoTxt("LogNaoSincronizados", response.Content);
                    //}

                }
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao cadastrar opção do Produto" + erro.Message);
            }


        }
        private void CadastraFormaPagamento(DataSet ds)
        {
            prgBarpagamento.Maximum = ds.Tables[0].Rows.Count;
            int iCod = 0;
            DataRow dRow;
            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/loja/cartoes", Method.POST);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                request.AddParameter("token", iParamToken);
                dRow = ds.Tables[0].Rows[i];
                if (File.Exists(dRow.ItemArray.GetValue(7).ToString()))
                {
                    iCod = int.Parse(dRow.ItemArray.GetValue(0).ToString());
                    request.AddFile("cartao[" + i + "]", dRow.ItemArray.GetValue(7).ToString(), "imagem/png");
                    request.AddParameter("store_id", store_idAtual);

                }
                prgBarpagamento.Value = prgBarpagamento.Value + 1;
                con.AtualizaDataSincronismo("FormaPagamento", iCod);
            }
            RestResponse response = (RestResponse)client.Execute(request);
            ReturnPadrao lReturn = new ReturnPadrao();
            lReturn = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);

        }

        private void SelecionarImage(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            opn.Title = "Selecione a imagem pro site";
            opn.CheckFileExists = true;
            opn.Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" + "All files (*.*)|*.*";

            if (opn.ShowDialog() == DialogResult.OK)
            {
                txtcaminhoImage.Text = opn.FileName.ToString();
            }
        }


        private void btnSincronizar_Click(object sender, EventArgs e)
        {
            //  Sincroniza();
            Thread NovaTread;
            try
            {
                lock (this)
                {
                    NovaTread = new Thread(new ThreadStart(Sincroniza));
                    NovaTread.Start();
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }


        }
        private void SincronizacaoEmTread()
        {
            // Sincroniza(sender, e);
        }
        private void SincAdicionais(object sender, EventArgs e)
        {
            double iPerceDesconto;
            GerarToken();
            iUrlWS = Sessions.returnEmpresa.UrlServidor;
            if (txtPercentualDesconto.Text != "")
            {
                iPerceDesconto = double.Parse(txtPercentualDesconto.Text);
            }
            else
            {
                iPerceDesconto = 0;
            }
            CadastrarDesconto(iPerceDesconto);

            if (txtcaminhoImage.Text.Trim() != "")
            {
                CadastrarBanner(txtcaminhoImage.Text);
            }
            else if (chkRemover.Checked)
            {
                RemoverBanner();
            }
        }
        private void RemoverBanner()
        {
            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/banner/promocao/delete", Method.POST);
            request.AddParameter("token", iParamToken);

            RestResponse response = (RestResponse)client.Execute(request);

            ReturnPadrao lRetorno = new ReturnPadrao();
            lRetorno = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);
            //  lblReturn.Visible = lRetorno.status;
            lbRetornoImage.Visible = true;
            lbRetornoImage.Text = lRetorno.msg;
        }
        private void frmSincronizacao_Load(object sender, EventArgs e)
        {
            //    if (Sessions.returnConfig.PrevisaoEntrega != null)
            //    {
            //        strPrevisaoEntrega = Sessions.returnConfig.PrevisaoEntrega;
            //    }

            //    chkPrevisao.Text = chkPrevisao.Text + " " + strPrevisaoEntrega + " min.";
        }

        private void chkRemover_CheckedChanged(object sender, EventArgs e)
        {
            grpBanner.Enabled = !chkRemover.Checked;
        }

        private void txtVlrMinimo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }

        private void chkLink_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

