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
        private DataSet dsResultado;
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
                Utils.MontaCombox(cbxOrigemCadastro, "Nome", "Codigo", "Pessoa_OrigemCadastro", "spObterPessoa_OrigemCadastro");
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

        private DataSet PreencheFiltro()
        {
             dsResultado = null;
            try
            {
                if (rbAniversariantes.Checked)
                {
                    dsResultado = con.SelectObterAniversariantes("spObterAnivesariantes",
                       dtInicio.Value,
                       dtFim.Value);
                }
                else if (rbSemPedidos.Checked)
                {
                    dsResultado = con.SelectObterClientesSemPedido("spObterClientesSemPedido",
                        dtInicio.Value,
                        dtFim.Value);
                }
                else if (rbOrigemCadastro.Checked)
                {
                    dsResultado = con.SelectRegistroPorCodigo("Pessoa", "spObterPessoaPorCodOrigemCadastro", int.Parse(cbxOrigemCadastro.SelectedValue.ToString()));
                }
                else if (rbProduto.Checked)
                {
                    dsResultado = con.SelectRegistroPorCodigoPeriodo("Pessoa", "spObterProdutoPorCliente",
                     cbxGrupo.SelectedValue.ToString(), dtInicio.Value, dtFim.Value);
                }
                else if (rbRegiao.Checked)
                {
                    dsResultado = con.SelectRegistroPorCodigo("Pessoa", "spObterClientesPorRegiao", int.Parse(cbxRegiao.SelectedValue.ToString()));
                }

                PopulaGrid(dsResultado, "Pessoa");
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            lblNumero.Text = dsResultado.Tables[0].Rows.Count.ToString();
            return dsResultado;

        }
        private void PopulaGrid(DataSet ds, string table)
        {
            try
            {
                if (ds.Tables[0].Rows.Count==0)
                {
                    MessageBox.Show(Bibliotecas.cFiltroRetornaVazio);
                    return;
                }
                gridResultado.DataSource = null;
                gridResultado.AutoGenerateColumns = true;
                gridResultado.DataSource = ds;
                gridResultado.DataMember = table;

                for (int i = 0; i < gridResultado.Columns.Count; i++)
                {
                    if (gridResultado.Columns[i].HeaderText != "Nome")
                    {
                        gridResultado.Columns[i].Visible = false;
                    }
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        private void EnviarSMS(object sender, EventArgs e)
        {
            PreencheFiltro();
            
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


        private void rbOndeConheceu_CheckedChanged(object sender, EventArgs e)
        {
            cbxOrigemCadastro.Enabled = rbOrigemCadastro.Checked;
        }

        private void DisparaSMS(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (gridResultado.Rows.Count == 0)
            {
                MessageBox.Show(Bibliotecas.cFiltroRetornaVazio);
            }
            else
            {
                Utils.EnviaSMS_LOCASMS(dsResultado, txtMensagem.Text, "Teste", "27981667827", "546936");
                //lbl.Text = Convert.ToString(TotalSelecionado);
                this.Cursor = Cursors.Default;

            }
        }

        private void rbRegiao_CheckedChanged(object sender, EventArgs e)
        {
            grpPeriodo.Enabled = !rbRegiao.Checked;
            Utils.MontaCombox(cbxRegiao, "NomeRegiao", "Codigo", "RegiaoEntrega", "spObterRegioes");
        }
    }
}
