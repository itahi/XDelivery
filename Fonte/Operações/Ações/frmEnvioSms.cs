//using HumanAPIClient.Model;
//using HumanAPIClient.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Web;
using HumanAPIClient.Service;
using HumanAPIClient.Model;
using DexComanda.Models;
using DexComanda;
using DexComanda.Integração;
using System.Configuration;
using DexComanda.Models.Zenvia;
using Newtonsoft.Json;

namespace DexComanda
{
    public partial class frmEnvioSms : Form
    {
        //private SimpleSending cliente;
        //private SimpleMessage mensagem;
        private Conexao con;
        private DateTime DataInicial;
        private DateTime DataFinal;
        public string RetornoServ;
        private string NomeEmpresa;
        private string NomeCliente;
        private int TotalSelecionado;
        private string pLogin;
        private string pSenha;

        public frmEnvioSms()
        {

            InitializeComponent();
            con = new Conexao();
            NomeEmpresa = Sessions.returnEmpresa.Nome;

        }

        private void frmEnvioSms_Load(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["ConfigSMS"] != null)
                {
                    string Itext = ConfigurationManager.AppSettings["ConfigSMS"].ToString();

                    string[] words = Itext.Split(',');
                    for (int i = 0; i < words.Length - 1; i++)
                    {
                        pLogin = words[0];
                        pSenha = words[1];
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Arquivo de configuração para envio de SMS não está na pasta , favor ir até Configurações > Promoção preencher os campos e salvar");
                    frmConfiguracoes frm = new frmConfiguracoes();
                    frm.Show();
                }


            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro na leitura do arquivo de configuração " + erro.Message);
            }

            // Caracters restantes no texto
            lblRestante.Text = Convert.ToString(150 - NomeEmpresa.ToString().Length);

        }
        /// <summary>
        /// Envia Sms usando plataforma zenvia
        /// </summary>
        /// <param name="ds">
        /// DataSet com dados vindo do filtros</param>
        /// <param name="itextMSG">
        /// Texto do Envio</param>
        private void EnviaSMSZenvia(DataSet ds,string itextMSG)
        {
            Zenvia newzen = new Zenvia();
            sendSmsMultiRequest sms = new sendSmsMultiRequest();
            List<sendSmsMultiRequest> newList = new List<sendSmsMultiRequest>();
            Random newRandon = new Random();
            string strtelefoneFormatado = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sms = new sendSmsMultiRequest()
                {
                    callbackOption = "NONE",
                    from = Sessions.returnEmpresa.Nome,
                    id = newRandon.Next().ToString(),
                    msg = itextMSG
                };

                if (ds.Tables[0].Rows[i].Field<string>("Telefone").Length==8)
                {
                    strtelefoneFormatado = "55279" + ds.Tables[0].Rows[i].Field<string>("Telefone");
                }
                else if (ds.Tables[0].Rows[i].Field<string>("Telefone").Length == 9)
                {
                    strtelefoneFormatado = "5527" + ds.Tables[0].Rows[i].Field<string>("Telefone");
                }
                else
                {
                    strtelefoneFormatado = "55" + ds.Tables[0].Rows[i].Field<string>("Telefone");
                }
                sms.to = strtelefoneFormatado;
                newList.Add(sms);
            }
            newzen.EnviaSMSLista(JsonConvert.SerializeObject(newList));
        }

        private void EnviarSMS(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            DataSet dsLista;
            dsLista = new DataSet();
            if (!rbAniversariantes.Checked && !rbSemPedidos.Checked && txtMensagem.Text != "")
            {
                MessageBox.Show("Selecione primeiro para que grupo enviará as mensagems", "[xSistemas] Aviso");
                return;
            }
            else
            {
                if (rbAniversariantes.Checked)
                {
                    dsLista = con.RetornaListaPessoasSMS(dtInicio.Value, dtFim.Value, true, false, false);

                }
                else if (rbSemPedidos.Checked)
                {

                    dsLista = con.RetornaListaPessoasSMS(dtInicio.Value, dtFim.Value, false, true, false);

                }
                //if (chkTodosClientes.Checked)
                //{
                //    dsLista = con.RetornaListaPessoasSMS(DateTime.Now, DateTime.Now, false, false, true);
                //}

            }
            if (dsLista.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show(Bibliotecas.cFiltroRetornaVazio);
            }
            else
            {
                Utils.EnviaSMS_LOCASMS(dsLista, txtMensagem.Text, "Teste", "27981667827", "546936");
                lbl.Text = Convert.ToString(TotalSelecionado);
                this.Cursor = Cursors.Default;

            }
        }

        private void txtMensagem_KeyDown(object sender, KeyEventArgs e)
        {
            int TotalCaracteres = 150 - NomeEmpresa.Length;
            int TotalContado = txtMensagem.Text.Length;

            if (txtMensagem.Text != "")
            {
                lblRestante.Text = Convert.ToString(TotalCaracteres - TotalContado);
            }

        }
        private string AdicionaNomeCliente(string iMensagem)
        {
            if (iMensagem.Contains("@Cliente"))
            {
                iMensagem = iMensagem.Replace("@Cliente", NomeCliente);
            }

            return iMensagem;
        }
    }
}
