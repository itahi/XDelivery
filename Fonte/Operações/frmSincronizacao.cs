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

            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao sincronizar " + erro.Message);
            }
        }
        private void CadastraLinkApp()
        {
            try

            {
                newDados = new List<DadosApp>();
                newDados= JsonConvert.DeserializeObject<List<DadosApp>>(Sessions.returnConfig.DadosApp);
                RestClient client = new RestClient(iUrlWS);
                RestResponse response= new RestResponse();
                RestRequest request = new RestRequest("ws/loja/baixarrApp", Method.POST);

                foreach (DadosApp item in newDados)
                {
                    request.AddParameter("token", iParamToken);
                    request.AddParameter("plataforma", item.plataforma);
                    request.AddParameter("urlBaixarApp",item.url);
                    MudaLabel("Link APP");
                    response = (RestResponse)client.Execute(request);
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
            if (iNomeTable!="FormaPagamento")
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
                RestClient client = new RestClient(iUrlWS);
                RestRequest request = new RestRequest("ws/opcao/tipo/set", Method.POST);
                int iMaxOpcionais = 0; int iMinumum = 0;
                MudaLabel("Tipo de Opção");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    iMaxOpcionais = ds.Tables["Produto_OpcaoTipo"].Rows[i].Field<int>("MinimoOpcionais");
                    iMinumum = ds.Tables["Produto_OpcaoTipo"].Rows[i].Field<int>("MaximoOpcionais");
                    request.AddParameter("token", iParamToken);
                    request.AddParameter("nome", ds.Tables["Produto_OpcaoTipo"].Rows[i].Field<string>("nome"));
                    request.AddParameter("tipo", ds.Tables["Produto_OpcaoTipo"].Rows[i].Field<string>("tipo"));
                    request.AddParameter("referenciaId", ds.Tables["Produto_OpcaoTipo"].Rows[i].Field<int>("Codigo"));
                    request.AddParameter("ordenExibicao", ds.Tables["Produto_OpcaoTipo"].Rows[i].Field<int>("OrdenExibicao"));
                    request.AddParameter("minimoSelecao", iMaxOpcionais);
                    request.AddParameter("maximoSelecao", iMinumum);
                    RestResponse response = (RestResponse)client.Execute(request);

                    ReturnPadrao lRetorno = new ReturnPadrao();
                    lRetorno = JsonConvert.DeserializeObject<ReturnPadrao>(response.Content);
                    if (lRetorno.status == true)
                    {
                        con.AtualizaDataSincronismo("Produto_OpcaoTipo", ds.Tables[0].Rows[i].Field<int>("Codigo"));
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
            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/regiaoEntrega/set", Method.POST);
            MudaLabel("Regioes de Entrega");
            prgBarRegiao.Maximum = ds.Tables[0].Rows.Count;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                request.AddParameter("token", iParamToken);
                request.AddParameter("cep", ds.Tables["RegiaoEntrega"].Rows[i].Field<string>("CEP"));
                request.AddParameter("nome", ds.Tables["RegiaoEntrega"].Rows[i].Field<string>("NomeRegiao"));
                request.AddParameter("valor", ds.Tables["RegiaoEntrega"].Rows[i].Field<decimal>("TaxaServico"));
                request.AddParameter("referencia_id", ds.Tables["RegiaoEntrega"].Rows[i].Field<int>("Codigo"));
                request.AddParameter("ativo", Convert.ToInt16(ds.Tables["RegiaoEntrega"].Rows[i].Field<Boolean>("OnlineSN")));
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
                RestClient client = new RestClient(iUrlWS);
                RestRequest request = new RestRequest("ws/opcoes/set", Method.POST);
                prgBarProduto.Value = 0;
                prgBarProduto.Maximum = ds.Tables[0].Rows.Count;
                MudaLabel("Opções");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    request.AddParameter("token", iParamToken);
                    request.AddParameter("tipo", ds.Tables["Opcao"].Rows[i].Field<int>("Tipo"));
                    request.AddParameter("nome", ds.Tables["Opcao"].Rows[i].Field<string>("Nome"));
                    request.AddParameter("referenciaId", ds.Tables["Opcao"].Rows[i].Field<int>("Codigo"));
                    RestResponse response = (RestResponse)client.Execute(request);
                    prgBarProduto.Value = i + 1;

                    if (response.Content.ToString() == "true")
                    {
                        con.AtualizaDataSincronismo("Opcao", ds.Tables["Opcao"].Rows[i].Field<int>("Codigo"));
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
            lblSinc.Visible = true;
            lblSinc.Text = "Sincronizando " + iNomeTabela;
        }
        private void CadastraCategorias(DataSet ds)
        {
            try
            {
                RestClient client = new RestClient(iUrlWS);
                RestRequest request = new RestRequest("ws/categorias/set", Method.POST);
                prgBarProduto.Value = 0;
                prgBarProduto.Maximum = ds.Tables[0].Rows.Count;
                MudaLabel("Grupo");
                GerarToken();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string inome = ds.Tables["Grupo"].Rows[i].Field<string>("NomeGrupo");
                    int iCod = ds.Tables["Grupo"].Rows[i].Field<int>("Codigo");
                    int AtivoSN = Convert.ToInt16(ds.Tables["Grupo"].Rows[i].Field<Boolean>("OnlineSN"));

                    request.AddParameter("token", iParamToken);
                    request.AddParameter("nomeCategoria", inome);
                    request.AddParameter("ativo", AtivoSN);
                    request.AddParameter("idReferencia", iCod);
                    RestResponse response = (RestResponse)client.Execute(request);
                    prgBarProduto.Value = i + 1;

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
                RestClient client = new RestClient(iUrlWS);
                RestRequest request = new RestRequest("ws/produto/set", Method.POST);
                MudaLabel("Produto");
                decimal iPrecoProduto = 0;
                prgBarProduto.Value = 0;
                prgBarProduto.Maximum = ds.Tables[0].Rows.Count;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    decimal prProduto = ds.Tables["Produto"].Rows[i].Field<decimal>("PrecoProduto");
                    DateTime dtSinc = DateTime.Now.AddYears(1);
                    DateTime dtFoto = DateTime.Now.AddYears(1);
                    ds.Tables["Produto"].Rows[i].Field<DateTime>("DataFoto");

                    if (ds.Tables["Produto"].Rows[i].Field<DateTime>("DataSincronismo") != null)
                    {
                        dtSinc = ds.Tables["Produto"].Rows[i].Field<DateTime>("DataSincronismo");
                    }
                    if (ds.Tables["Produto"].Rows[i].Field<DateTime>("DataFoto") != null)
                    {
                        dtFoto = ds.Tables["Produto"].Rows[i].Field<DateTime>("DataFoto");
                    }



                    string iCaminhoImagem = ds.Tables["Produto"].Rows[i].Field<string>("UrlImagem");
                    request.AddParameter("token", iParamToken);
                    request.AddParameter("idReferencia", ds.Tables["Produto"].Rows[i].Field<int>("Codigo"));
                    request.AddParameter("nome", ds.Tables["Produto"].Rows[i].Field<string>("NomeProduto"));

                    if (Sessions.returnEmpresa.CNPJ == "09395874000160")
                    {
                        prProduto = prProduto + con.RetornaPrecoComEmbalagem(ds.Tables["Produto"].Rows[i].Field<string>("GrupoProduto"), ds.Tables["Produto"].Rows[i].Field<int>("Codigo"));
                    }
                    else
                    {
                        prProduto = ds.Tables["Produto"].Rows[i].Field<decimal>("PrecoProduto");
                    }

                    request.AddParameter("preco", prProduto);
                    decimal prPromocao = ds.Tables["Produto"].Rows[i].Field<decimal>("PrecoDesconto");
                    if (ds.Tables["Produto"].Rows[i].Field<DateTime>("DataFimPromocao") > DateTime.Now && prPromocao > 0)
                    {
                        request.AddParameter("precoPromocao", prPromocao);
                        request.AddParameter("dataInicial", ds.Tables["Produto"].Rows[i].Field<DateTime>("DataInicioPromocao"));
                        request.AddParameter("dataFinal", ds.Tables["Produto"].Rows[i].Field<DateTime>("DataFimPromocao"));
                    }
                    if (File.Exists(iCaminhoImagem) && dtFoto > dtSinc)
                    {
                        request.AddFile("imagem", iCaminhoImagem);
                    }

                    request.AddParameter("idReferenciaCategoria", RetornaIDCategoria(ds.Tables["Produto"].Rows[i].Field<string>("GrupoProduto")));
                    request.AddParameter("descricao", ds.Tables["Produto"].Rows[i].Field<string>("DescricaoProduto"));
                    request.AddParameter("ativo", Convert.ToInt32(ds.Tables["Produto"].Rows[i].Field<Boolean>("OnlineSN")));
                    request.AddParameter("maxOptions", ds.Tables["Produto"].Rows[i].Field<int>("MaximoAdicionais"));

                    prgBarProduto.Value = i + 1;

                    RestResponse response = (RestResponse)client.Execute(request);

                    if (response.Content.ToString() == "true")
                    {
                        con.AtualizaDataSincronismo("Produto", ds.Tables["Produto"].Rows[i].Field<int>("Codigo"));
                        CadastrarOpcaoProduto(ds.Tables["Produto"].Rows[i].Field<int>("Codigo"));
                    }
                    iCaminhoImagem = "";
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

        private void CadastrarOpcaoProduto(int iCodProduto)
        {

            try
            {
                RestClient client = new RestClient(iUrlWS);
                RestRequest request = new RestRequest("ws/produto/opcao/set", Method.POST);
                DataSet ds = con.SelectRegistroPorCodigo("Produto_Opcao", "spObterOpcaoProdutoCodigo", iCodProduto);
                int iCodOpcao = 0;
                MudaLabel("Opcoes/Adicionais");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int iCodProd = ds.Tables["Produto_Opcao"].Rows[0].Field<int>("CodProduto");
                    string[] opcao = new string[ds.Tables[0].Rows.Count];
                    DateTime dtFimPromo = ds.Tables["Produto_Opcao"].Rows[0].Field<DateTime>("DataFimPromocao");
                    request.AddParameter("token", iParamToken);
                    request.AddParameter("referenciaId", iCodProd);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        decimal iprice = 0;
                        iCodOpcao = ds.Tables["Produto_Opcao"].Rows[i].Field<int>("CodOpcao");
                        if (dtFimPromo >= Convert.ToDateTime(DateTime.Now.ToShortDateString()) && ds.Tables["Produto_Opcao"].Rows[i].Field<decimal>("PrecoProcomocao") > 0)
                        {
                            iprice = ds.Tables["Produto_Opcao"].Rows[i].Field<decimal>("PrecoProcomocao");
                        }
                        else
                        {
                            iprice = ds.Tables["Produto_Opcao"].Rows[i].Field<decimal>("Preco");
                        }

                        request.AddParameter("opcao[" + iCodOpcao + "]", iprice);
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
            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/loja/cartoes", Method.POST);

            prgBarpagamento.Maximum = ds.Tables[0].Rows.Count;
            request.AddParameter("token", iParamToken);
            int iCod = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (File.Exists(ds.Tables["FormaPagamento"].Rows[i].Field<string>("CaminhoImagem")))
                {
                    iCod = ds.Tables["FormaPagamento"].Rows[i].Field<int>("Codigo");
                    request.AddFile("cartao[" + i + "]", ds.Tables["FormaPagamento"].Rows[i].Field<string>("CaminhoImagem"), "imagem/png");
                    
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
    }
}

