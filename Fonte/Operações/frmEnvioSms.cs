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
                if (ConfigurationManager.AppSettings["ConfigSMS"]!=null)
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

        private void EnviarSMS(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            DataSet dsLista;
            dsLista = new DataSet();
            if (!rbAniversariantes.Checked && !rbSemPedidos.Checked && txtMensagem.Text != "" && !chkTodosClientes.Checked)
            {
                MessageBox.Show("Selecione primeiro para que grupo enviará as mensagems", "Aviso XCommanda");
                return;
            }
            else
            {
                if (rbAniversariantes.Checked)
                {
                    if (txtDataFinal.Text.Replace("  /", "") != "" || txtDataFinal.Text.Replace("  /", "") != "")
                    {
                        dsLista = con.RetornaListaPessoasSMS(Convert.ToDateTime(txtDataInicial.Text), Convert.ToDateTime(txtDataFinal.Text), true, false, false);
                    }
                    else
                    {
                        MessageBox.Show("Preencha o periodo para enviar", "Aviso XCommada");
                    }

                }
                else if (rbSemPedidos.Checked)
                {
                    if (txtDataInicial2.Text.Replace("  /", "") != "" || txtDataFinal2.Text.Replace("  /", "") != "")
                    {
                        dsLista = con.RetornaListaPessoasSMS(Convert.ToDateTime(txtDataInicial2.Text), Convert.ToDateTime(txtDataFinal2.Text), false, true, false);
                    }
                    else
                    {
                        MessageBox.Show("Preencha o periodo para enviar", "Aviso XCommada");
                    }
                }
                if (chkTodosClientes.Checked)
                {
                     dsLista = con.RetornaListaPessoasSMS(DateTime.Now, DateTime.Now, false, false, true);
                }

            }
            if (dsLista.Tables[0].Rows.Count > 0)
            {

                Utils.EnviaSMS_LOCASMS(dsLista, txtMensagem.Text, "Teste", "27981667827", "546936");
                lbl.Text = Convert.ToString(TotalSelecionado);
                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Seu filtro não retornou nenhum dado");
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

        private void chkTodosClientes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dsOpcao = con.SelectAdicionalLanche();

            DataSet dsLanches = con.SelectLanches();
            for (int i = 0; i < dsLanches.Tables[0].Rows.Count; i++)
            {
                Produto_Opcao prodOP = new Produto_Opcao()
                {
                    CodOpcao = dsOpcao.Tables["Opcao"].Rows[i].Field<int>("Codigo"),
                    CodProduto = dsLanches.Tables["Produto"].Rows[i].Field<int>("Codigo"),
                    DataAlteracao = DateTime.Now,
                    Preco = 0
                };
                con.Insert("spAdicionarOpcaProduto", prodOP);

            }
        }
    }
}
