using DexComanda.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações.Financeiro
{
    public partial class frmAberturaCaixa : Form
    {
        private Conexao con;
        private int CodUser;
        public frmAberturaCaixa()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void frmAberturaCaixa_Load(object sender, EventArgs e)
        {
            dtAbertura.Value = DateTime.Now;

          //  cbxFuncionario.Enabled = true;
            DataSet dsUsuario = con.SelectAll("Usuario", "spObterUsuario");
            cbxFuncionario.DataSource = dsUsuario.Tables[0];
            cbxFuncionario.DisplayMember = "Nome";
            cbxFuncionario.ValueMember = "Codigo";
            //cbxFuncionario.Enabled = false;

            DataSet dsCaixas = con.SelectAll("CaixaCadastro", "spObterCaixa");
            cbxCaixas.DataSource = dsCaixas.Tables["CaixaCadastro"];
            cbxCaixas.DisplayMember = "Numero";
            cbxCaixas.ValueMember = "Codigo";

            cbxFuncionario.Text = Sessions.retunrUsuario.Nome;
        }

        private void Salvar(object sender, EventArgs e)
        {
            try
            {
                if (Utils.EfetuarLogin(cbxFuncionario.Text, txtSenha.Text, false))
                {
                    Caixa caixa = new Caixa()
                    {
                        Data = dtAbertura.Value,
                        Estado = false /*Caixa Aber*/,
                        Historico = "Abertura Inicial",
                        ValorAbertura = decimal.Parse(txtValor.Text),
                        Numero = cbxCaixas.Text

                    };
                    if (cbxFuncionario.Text != "")
                    {
                        caixa.CodUsuario = Sessions.retunrUsuario.Codigo;

                        CaixaMovimento cxMovi = new CaixaMovimento()
                        {
                            CodCaixa = int.Parse(caixa.Numero),
                            CodFormaPagamento = 1,
                            Data = caixa.Data,
                            Historico = "Lançamento abertura",
                            NumeroDocumento = caixa.Numero,
                            Tipo = 'E',
                            Valor = caixa.ValorAbertura,
                            CodUser = caixa.CodUsuario
                        };

                        // Lança movimento no Caixa de abertura
                        con.Insert("spAbrirCaixa", caixa);
                        con.Insert("spInserirMovimentoCaixa", cxMovi);

                        con.LimpaTabela("Produto_Estoque", "spLimparEstoque");
                        MessageBox.Show("Caixa aberto", "[xSistemas] Aviso");
                        Utils.Restart();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuario não selecionado", "[xSistemas] Aviso");
                    }

                }
            }
            catch (Exception erro)
            {


                MessageBox.Show(erro.Message);
            }

        }



        private void cbxFuncionario_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CodUser = int.Parse(cbxFuncionario.SelectedValue.ToString());
        }

        private void cbxFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }
    }
}
