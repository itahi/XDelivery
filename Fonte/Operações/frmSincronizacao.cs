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
        private Conexao con;
        public frmSincronizacao()
        {
            InitializeComponent();
            con = new Conexao();
            listGrupos = new List<Grupo>();
        }

        private void Sincroniza(object sender, EventArgs e)
        {
            iParamToken = Convert.ToString(DateTime.Now).Replace("/", "").Replace(":", "").Replace(" ", "").Substring(0, 11) + "Adminx";
            iParamToken = Utils.CriptografarArquivo(iParamToken.Trim());

            try
            {
                CadastraCategorias(ObterCategorias());

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private DataSet ObterCategorias()
        {
            return con.SelectRegistroONline("Grupo");

        }
        private void CadastraCategorias(DataSet dsCategorias)
        {
            RestClient client = new RestClient("http://xfood.xsistemas.com.br/");
            RestRequest request = new RestRequest("ws/categorias/set", Method.POST);
            listGrupos = new List<Grupo>();
            var grupos = new Grupo();
            for (int i = 0; i < dsCategorias.Tables[0].Rows.Count; i++)
            {
                 grupos = new Grupo()
                 {
                     Codigo = dsCategorias.Tables["Grupo"].Rows[i].Field<int>("Codigo"),
                     NomeGrupo = dsCategorias.Tables["Grupo"].Rows[i].Field<string>("NomeGrupo"),
                 };

                listGrupos.Add(grupos);
            }
            request.AddParameter("token", iParamToken);
            request.AddParameter("token", grupos);

            RestResponse response = (RestResponse)client.Execute(request);

        }
    }
}
