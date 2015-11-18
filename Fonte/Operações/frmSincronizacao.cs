using DexComanda.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações
{
    public partial class frmSincronizacao : Form
    {
        private string iParamToken;
        private List<Grupo> listGrupos;
        private string iUrlWS;
        private Conexao con;
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
        private void Sincroniza(object sender, EventArgs e)
        {
            iUrlWS = Sessions.returnEmpresa.UrlServidor;
            GerarToken();

            try
            {
                if (chkProdutos.Checked)
                {
                    CadastraCategorias(ObterDados("Grupo"));
                    CadastrarOpcao(ObterDados("Opcao"));
                    CadastrarProduto(ObterDados("Produto"));
                }
                if (chkRegiaoEntrega.Checked)
                {
                    CadastraRegioes(con.RetornaRegiao());
                }
                
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao sincronizar " + erro.Message);
            }
        }
        private DataSet ObterDados(string iNomeTable)
        {
            return con.SelectRegistroONline(iNomeTable);
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
                iTotalOrSub = 5;
            }
            else
            {
                iTotalOrSub = 4;
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
            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/opcoes/set", Method.POST);
            prgBarProduto.Value = 0;
            prgBarProduto.Maximum = ds.Tables[0].Rows.Count;
            MudaLabel("Opções");
            // CadastrarOpcaoProduto(ds.Tables["Opcao"].Rows[i].Field<int>("Codigo"));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int iTipoOpcao = 1;
                if (ds.Tables["Opcao"].Rows[i].Field<string>("Tipo") == "Selecao unica")
                {
                    iTipoOpcao = 1;
                }
                else if (ds.Tables["Opcao"].Rows[i].Field<string>("Tipo") == "Multipla Selecao")
                {
                    iTipoOpcao = 2;
                }
                else
                {
                    iTipoOpcao = 3;
                }

                request.AddParameter("token", iParamToken);
                request.AddParameter("tipo", iTipoOpcao);
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
        private void MudaLabel(string iNomeTabela)
        {
            lblSincronismo.Visible = true;
            lblSincronismo.Text = "Sincronizando " + iNomeTabela;
        }
        private void CadastraCategorias(DataSet ds)
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
        // Metodo para converter imagems embytes
        public byte[] ConverterFotoEmByte(System.IO.Stream iArquivo)
        {
            long originalPosition = iArquivo.Position;
            iArquivo.Position = 0;

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = iArquivo.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = iArquivo.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                iArquivo.Position = originalPosition;
            }
        }
        private void CadastrarProduto(DataSet ds)
        {
            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/produto/set", Method.POST);
            MudaLabel("Produto");
            prgBarProduto.Value = 0;
            prgBarProduto.Maximum = ds.Tables[0].Rows.Count;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                decimal prProduto = ds.Tables["Produto"].Rows[i].Field<decimal>("PrecoProduto");
                string iCaminhoImagem = ds.Tables["Produto"].Rows[i].Field<string>("UrlImagem");
                request.AddParameter("token", iParamToken);
                request.AddParameter("idReferencia", ds.Tables["Produto"].Rows[i].Field<int>("Codigo"));
                request.AddParameter("nome", ds.Tables["Produto"].Rows[i].Field<string>("NomeProduto"));
                request.AddParameter("preco", prProduto);
                if (iCaminhoImagem !=null && iCaminhoImagem!="")
                {
                    request.AddFile("imagem", iCaminhoImagem);
                }
                
                //  request.AddParameter("precoPromocao", 10);
                request.AddParameter("idReferenciaCategoria", RetornaIDCategoria(ds.Tables["Produto"].Rows[i].Field<string>("GrupoProduto")));
                request.AddParameter("descricao", ds.Tables["Produto"].Rows[i].Field<string>("DescricaoProduto"));
                request.AddParameter("ativo", Convert.ToInt32(ds.Tables["Produto"].Rows[i].Field<Boolean>("OnlineSN")));
                request.AddParameter("maxOptions", ds.Tables["Produto"].Rows[i].Field<int>("MaximoAdicionais"));
                //   request.AddParameter("precoPromocao", ds.Tables["Produto"].Rows[i].Field<int>("PrecoDesconto"));
                //   request.AddParameter("dataInicial", ds.Tables["Produto"].Rows[i].Field<int>("MaximoAdicionais"));
                //   request.AddParameter("dataFinal", ds.Tables["Produto"].Rows[i].Field<int>("MaximoAdicionais"));

                // request.AddParameter("imagem", "  ");
                // request.AddParameter("lista", " "); 
                prgBarProduto.Value = i + 1;

                RestResponse response = (RestResponse)client.Execute(request);

                if (response.Content.ToString() == "true")
                {
                    con.AtualizaDataSincronismo("Produto", ds.Tables["Produto"].Rows[i].Field<int>("Codigo"));
                    CadastrarOpcaoProduto(ds.Tables["Produto"].Rows[i].Field<int>("Codigo"));
                }

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
                    request.AddParameter("token", iParamToken);
                    request.AddParameter("referenciaId", iCodProd);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        iCodOpcao = ds.Tables["Produto_Opcao"].Rows[i].Field<int>("CodOpcao");
                        decimal iprice = ds.Tables["Produto_Opcao"].Rows[i].Field<decimal>("Preco");
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
            RestRequest request = new RestRequest("ws/formapagamento/set", Method.POST);

            prgBarpagamento.Maximum = ds.Tables[0].Rows.Count;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string inome = ds.Tables["FormaPagamento"].Rows[i].Field<string>("Nome");
                int iCod = ds.Tables["FormaPagamento"].Rows[i].Field<int>("Codigo");
                request.AddParameter("token", iParamToken);
                request.AddParameter("nomeformapagamento", inome);
                request.AddParameter("idReferencia", iCod);
                RestResponse response = (RestResponse)client.Execute(request);
                prgBarpagamento.Value = i + 1;
                if (response.Content.ToString() == "true")
                {
                    con.AtualizaDataSincronismo("FormaPagamento", iCod);
                }

            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

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

        private void txtcaminhoImage_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSincronizar_Click(object sender, EventArgs e)
        {
            Sincroniza(sender, e);
        }

        private void SincAdicionais(object sender, EventArgs e)
        {
            double iPerceDesconto;
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
        }
    }
}

