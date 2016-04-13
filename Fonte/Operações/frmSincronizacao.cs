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
        private List<Grupo> listGrupos;
        private string iUrlWS;
        private string strPrevisaoEntrega = "0";
        private Conexao con;
        private List<DadosApp> newDados;
        public frmSincronizacao()
        {
            InitializeComponent();
            con = new Conexao();
            listGrupos = new List<Grupo>();
        }
        private void GerarToken()
        {

            iParamToken = Convert.ToString(DateTime.Now).Replace("/", "").Replace(":", "").Replace(" ", "").Substring(0, 11) + "Adminx";
            iParamToken = Utils.CriptografarArquivo(iParamToken.Trim());
            iParamToken = iParamToken.ToLower();
        }
        private void Sincroniza()
        {
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
                                con.AtualizaDataSincronismo("Produto", -1, "DataAlteracao");
                                LimparUrlAmigaveis();
                                CadastraLinkApp(true);
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
                if (!chkPrevisao.Checked)
                {
                    if (Utils.MessageBoxQuestion("Deseja desativar a previsão de entrega exibida no site/app?"))
                    {
                        CadastraPrevisao();
                    }
                }
                else
                {
                    CadastraPrevisao();
                }
                if (chkMobile.Checked)
                {
                    CadastraLinkApp();
                }

                CadastraPedidoMinimo();

            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao sincronizar " + erro.Message);
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
        private void CadastraLinkApp(Boolean iLimpar=false)
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
                    string strPlataformas = Utils.GravaJson("android","");
                  //  strPlataformas = strPlataformas + Utils.GravaJson("ios", "");
                    newDados = JsonConvert.DeserializeObject<List<DadosApp>>(strPlataformas);
                    foreach (var item in newDados)
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
            MudaLabel("Pedido Mínimo");
            RestResponse response = (RestResponse)client.Execute(request);

            ReturnPadrao lRetorno = new ReturnPadrao();
            lRetorno = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);
            lblMinimo.Visible = true;
            lblMinimo.Text = lRetorno.msg;
        }
        private void CadastraPrevisao()
        {

            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/previsao/entrega/set", Method.POST);
            request.AddParameter("token", iParamToken);
            request.AddParameter("tempo", strPrevisaoEntrega);
            request.AddParameter("status", Convert.ToInt32(chkPrevisao.Checked));
            MudaLabel("Previsão de Entrega");
            RestResponse response = (RestResponse)client.Execute(request);

            ReturnPadrao lRetorno = new ReturnPadrao();
            lRetorno = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);
            if (lRetorno.status == true)
            {
                prgBarPrevisao.Value = 100;
            }
        }
        private DataSet ObterDados(string iNomeTable)
        {
            if (iNomeTable != "FormaPagamento")
            {
                return con.SelectRegistroONline(iNomeTable);
            }
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

            MudaLabel("Regioes de Entrega");
            prgBarRegiao.Maximum = ds.Tables[0].Rows.Count;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                RestClient client = new RestClient(iUrlWS);
                RestRequest request = new RestRequest("ws/regiaoEntrega/set", Method.POST);
                request.AddParameter("token", iParamToken);
                request.AddParameter("cep", ds.Tables["RegiaoEntrega"].Rows[i].Field<string>("CEP"));
                request.AddParameter("nome", ds.Tables["RegiaoEntrega"].Rows[i].Field<string>("NomeRegiao"));
                request.AddParameter("valor", ds.Tables["RegiaoEntrega"].Rows[i].Field<decimal>("TaxaServico"));
                request.AddParameter("referencia_id", ds.Tables["RegiaoEntrega"].Rows[i].Field<int>("Codigo"));
                request.AddParameter("ativo", Convert.ToInt16(ds.Tables["RegiaoEntrega"].Rows[i].Field<Boolean>("OnlineSN")));

                if (ds.Tables["RegiaoEntrega"].Rows[i].Field<decimal>("valorMinimoFreteGratis") > 0)
                {
                    request.AddParameter("valorMinimoFreteGratis", Convert.ToInt16(ds.Tables["RegiaoEntrega"].Rows[i].Field<decimal>("valorMinimoFreteGratis")));
                }

                RestResponse response = (RestResponse)client.Execute(request);
                prgBarRegiao.Value = i + 1;

                if (response.Content.ToString() == "true")
                {
                    con.AtualizaDataSincronismo("RegiaoEntrega", ds.Tables[0].Rows[i].Field<int>("Codigo"));
                }


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
                    RestResponse response = (RestResponse)client.Execute(request);
                    prgBarProduto.Increment (i + 1);

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
        //private void CadastrarFamilia(DataSet ds)
        //{
        //    try
        //    {
        //        RestClient client = new RestClient(iUrlWS);
        //        RestRequest request = new RestRequest("ws/categorias/set", Method.POST);
        //        prgBarProduto.Value = 0;
        //        prgBarProduto.Maximum = ds.Tables[0].Rows.Count;
        //        DataRow dRow;
        //        MudaLabel("Familia");
        //        GerarToken();
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            int AtivoSN = 0, iCod;
        //            string inome = "", idReferenciaCategoriaPai = "0";
        //            dRow = ds.Tables[0].Rows[i];

        //            iCod = int.Parse(dRow.ItemArray.GetValue(0).ToString());
        //            inome = dRow.ItemArray.GetValue(1).ToString();
        //            if (Convert.ToBoolean(dRow.ItemArray.GetValue(2).ToString()) == true)
        //            {
        //                AtivoSN = 1;
        //            }
        //            request.AddParameter("idReferenciaCategoriaPai", 0);
        //            request.AddParameter("token", iParamToken);
        //            request.AddParameter("nomeCategoria", inome);
        //            request.AddParameter("ativo", AtivoSN);
        //            request.AddParameter("idReferencia", iCod);
        //            RestResponse response = (RestResponse)client.Execute(request);
        //            prgBarProduto.Value = i + 1;

        //            if (response.Content.ToString() == "true")
        //            {
        //                con.AtualizaDataSincronismo("Familia", iCod);
        //            }
        //            prgBarProduto.Value = i + 1;

        //        }

        //    }
        //    catch (Exception er)
        //    {

        //        MessageBox.Show("Erro ao cadastrarar Grupo" + er.Message + er.InnerException);
        //    }

        //}
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
                    RestResponse response = (RestResponse)client.Execute(request);
                    prgBarProduto.Increment(i+1);

                    if (response.Content.ToString() == "true")
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

        private void CadastrarProduto(DataSet ds)
        {
            try
            {
                MudaLabel("Produto");
                decimal iPrecoProduto = 0;
               // ManipulaProgressBar(ds.Tables[0].Rows.Count);
                DataRow dRow;
                prgBarProduto.Value = 0;
                prgBarProduto.Maximum = ds.Tables[0].Rows.Count;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    RestClient client = new RestClient(iUrlWS);
                    RestRequest request = new RestRequest("ws/produto/set", Method.POST);
                    GerarToken();
                    dRow = ds.Tables[0].Rows[i];
                    decimal prProduto = decimal.Parse(dRow.ItemArray.GetValue(3).ToString());
                    DateTime dtSinc = DateTime.Now.AddYears(1);
                    DateTime dtFoto = DateTime.Now.AddYears(1);


                    if (dRow.ItemArray.GetValue(10).ToString() != "")
                    {
                        dtSinc = Convert.ToDateTime(dRow.ItemArray.GetValue(10).ToString());
                    }
                    if (dRow.ItemArray.GetValue(15).ToString() != "")
                    {
                        dtFoto = Convert.ToDateTime(dRow.ItemArray.GetValue(15).ToString());
                    }


                    string iCaminhoImagem = dRow.ItemArray.GetValue(12).ToString();
                    request.AddParameter("token", iParamToken);
                    request.AddParameter("idReferencia", dRow.ItemArray.GetValue(0).ToString());
                    request.AddParameter("nome", dRow.ItemArray.GetValue(1).ToString());

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
                    if (dRow.ItemArray.GetValue(14).ToString() != "")
                    {
                        if (Convert.ToDateTime(dRow.ItemArray.GetValue(14).ToString()) > DateTime.Now && prPromocao > 0)
                        {
                            request.AddParameter("precoPromocao", prPromocao);
                            request.AddParameter("dataInicial", dRow.ItemArray.GetValue(13).ToString());
                            request.AddParameter("dataFinal", dRow.ItemArray.GetValue(14).ToString());
                        }
                    }

                    if (File.Exists(iCaminhoImagem) && dtFoto > dtSinc)
                    {
                        request.AddFile("imagem", iCaminhoImagem);
                    }
                    request.AddParameter("idReferenciaCategoria", dRow.ItemArray.GetValue(16).ToString());
                    // request.AddParameter("idReferenciaCategoria", RetornaIDCategoria(dRow.ItemArray.GetValue(4).ToString()));
                    request.AddParameter("descricao", dRow.ItemArray.GetValue(2).ToString());
                    int bAtivoSn = 0;
                    if (Convert.ToBoolean(dRow.ItemArray.GetValue(8).ToString()) == true)
                    {
                        bAtivoSn = 1;
                    }
                    request.AddParameter("ativo", bAtivoSn);
                    request.AddParameter("maxOptions", dRow.ItemArray.GetValue(11).ToString());

                    prgBarProduto.Increment (i+1);

                    RestResponse response = (RestResponse)client.Execute(request);

                    if (response.Content.ToString() == "true")
                    {
                        con.AtualizaDataSincronismo("Produto", int.Parse(dRow.ItemArray.GetValue(0).ToString()));
                        CadastrarOpcaoProduto(int.Parse(dRow.ItemArray.GetValue(0).ToString()));
                    }
                    iCaminhoImagem = "";

                    // request = null;
                }
            }
            catch (Exception er)
            {

                MessageBox.Show("Erro ao cadastrarar Produto" + er.Message + er.InnerException);
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
              //  ManipulaProgressBar(ds.Tables[0].Rows.Count);
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
                        prgBarProduto.Increment( i + 1);
                    }

                    RestResponse response = (RestResponse)client.Execute(request);

                    if (response.Content.ToString() == "true")
                    {
                        con.AtualizaDataSincronismo("Produto_Opcao", iCodProd, iCodOpcao);
                    }

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
            Sincroniza();
            //Thread NovaTread;
            //try
            //{

            //    NovaTread = new Thread(new ThreadStart(Sincroniza));
            //    NovaTread.Start();
            //    while (!NovaTread.IsAlive)
            //    {
            //        Thread.Sleep(1);
            //        NovaTread.Abort();
            //        NovaTread.Join();
            //    }
            //}
            //finally
            //{
            //    //
            //}


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
            if (Sessions.returnConfig.PrevisaoEntrega != null)
            {
                strPrevisaoEntrega = Sessions.returnConfig.PrevisaoEntrega;
            }

            chkPrevisao.Text = chkPrevisao.Text + " " + strPrevisaoEntrega + "min.";
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

