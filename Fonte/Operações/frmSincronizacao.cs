using DexComanda.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void Sincroniza(object sender, EventArgs e)
        {
            iUrlWS = Sessions.returnEmpresa.UrlServidor;
            iParamToken = Convert.ToString(DateTime.Now).Replace("/", "").Replace(":", "").Replace(" ", "").Substring(0, 11) + "Adminx";
            iParamToken = Utils.CriptografarArquivo(iParamToken.Trim());
            iParamToken = iParamToken.ToLower();

            try
            {
                if (chkCategorias.Checked)
                {
                    CadastraCategorias(ObterDados("Grupo"));
                }
                if (chkProdutos.Checked)
                {
                    CadastrarProduto(ObterDados("Produto"));
                }
                if (chkOpcao.Checked)
                {
                  //  CadastrarOpcaoProduto(1);
                    CadastrarOpcao(ObterDados("Opcao"));

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private DataSet ObterDados(string iNomeTable)
        {
            return con.SelectRegistroONline(iNomeTable);
        }



        private void CadastrarOpcao(DataSet ds)
        {
            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/opcoes/set", Method.POST);
            prgbarOpcao.Value = 0;
            prgbarOpcao.Maximum = ds.Tables[0].Rows.Count;
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

                request.AddParameter("token", iParamToken);
                request.AddParameter("tipo", iTipoOpcao);
                request.AddParameter("nome", ds.Tables["Opcao"].Rows[i].Field<string>("Nome"));
                request.AddParameter("referenciaId", ds.Tables["Opcao"].Rows[i].Field<int>("Codigo"));
                RestResponse response = (RestResponse)client.Execute(request);
                prgbarOpcao.Value = i + 1;

                if (response.Content.ToString() == "true")
                {
                    con.AtualizaDataSincronismo("Opcao", ds.Tables["Opcao"].Rows[i].Field<int>("Codigo"));

                }

            }
        }
        private void CadastraCategorias(DataSet ds)
        {
            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/categorias/set", Method.POST);

            prgBarCategoria.Maximum = ds.Tables[0].Rows.Count;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string inome = ds.Tables["Grupo"].Rows[i].Field<string>("NomeGrupo");
                int iCod = ds.Tables["Grupo"].Rows[i].Field<int>("Codigo");
                request.AddParameter("token", iParamToken);
                request.AddParameter("nomeCategoria", inome);
                request.AddParameter("idReferencia", iCod);
                RestResponse response = (RestResponse)client.Execute(request);
                prgBarCategoria.Value = i + 1;

                if (response.Content.ToString() == "true")
                {
                    con.AtualizaDataSincronismo("Grupo", iCod);
                }

            }
        }
        private void CadastrarProduto(DataSet ds)
        {
            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/produto/set", Method.POST);
            prgBarProduto.Maximum = ds.Tables[0].Rows.Count;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                decimal prProduto = ds.Tables["Produto"].Rows[i].Field<decimal>("PrecoProduto");
                request.AddParameter("token", iParamToken);
                request.AddParameter("idReferencia", ds.Tables["Produto"].Rows[i].Field<int>("Codigo"));
                request.AddParameter("nome", ds.Tables["Produto"].Rows[i].Field<string>("NomeProduto"));
                request.AddParameter("preco", prProduto);
                //request.AddParameter("idReferenciaCategoria",10);
                request.AddParameter("idReferenciaCategoria", RetornaIDCategoria(ds.Tables["Produto"].Rows[i].Field<string>("GrupoProduto")));
                request.AddParameter("descricao", ds.Tables["Produto"].Rows[i].Field<string>("DescricaoProduto"));
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

        private int RetornaIDCategoria(string iNomeCategoria)
        {
            int iIDReturn=1;
            DataSet dsGrupo = con.SelectRegistroPorNome("@Nome", "Grupo", "spObterGrupoPorNome", iNomeCategoria);
            if (dsGrupo.Tables[0].Rows.Count>0)
            {
                DataRow dRowProduto = dsGrupo.Tables[0].Rows[0];
                 iIDReturn = int.Parse(dRowProduto.ItemArray.GetValue(0).ToString());
            }

            return iIDReturn;
        }

        private void CadastrarOpcaoProduto(int iCodProduto)
        {
            RestClient client = new RestClient(iUrlWS);
            RestRequest request = new RestRequest("ws/produto/opcao/set", Method.POST);

            DataSet ds = con.SelectRegistroPorCodigo("Produto_Opcao", "spObterOpcaoProdutoCodigo", iCodProduto);
            if (ds.Tables[0].Rows.Count > 0)
            {
                int iCodProd = ds.Tables["Produto_Opcao"].Rows[0].Field<int>("CodProduto");
                string[] opcao = new string[ds.Tables[0].Rows.Count];
                request.AddParameter("token", iParamToken);
                request.AddParameter("referenciaId", iCodProd);
                
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    int iCod = ds.Tables["Produto_Opcao"].Rows[i].Field<int>("CodOpcao");
                    decimal iprice = ds.Tables["Produto_Opcao"].Rows[i].Field<decimal>("Preco");
                    request.AddParameter("opcao["+iCod+"]", iprice); //Pronto
                }


               
                RestResponse response = (RestResponse)client.Execute(request);
                prgBarpagamento.Value = prgBarpagamento.Value + 1;
                if (response.Content.ToString() == "true")
                {
                    con.AtualizaDataSincronismo("Produto_Opcao", iCodProd);

                }
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


    }
}

