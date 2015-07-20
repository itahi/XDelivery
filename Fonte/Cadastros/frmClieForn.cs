using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda
{
    public partial class frmClieForn : Form
    {
        private Conexao Con;
        public frmClieForn()
        {
            if (Conexao.connectionString != null)
            {
                Con = new Conexao();
            }
            else {
                Con = new Conexao();
            }
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSairEvent(object sender, EventArgs e)
        {

            this.Close();
        }

        private void btnCancelarEvent(object sender, EventArgs e)
        {

        }

        private void frmEmpresa_Load(object sender, EventArgs e)
        {
            //this.txtServidor.Text = Con.SelectAll("spObterEmpresa",)
        }

        private void txtCEP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //List<String> list = new List<String>();
                if (this.txtCEP.Text.Equals(""))
                {
                    MessageBox.Show("Informe o Cep.");
                }
                else
                {
                    int cep = int.Parse(this.txtCEP.Text);
                    DataSet endereco = Con.SelectEnderecoPorCep("base_cep", "spObterEnderecoPorCep", cep);

                    if (endereco.Tables["base_cep"].Rows.Count > 0)
                    {

                        DataRow dRow = endereco.Tables["base_cep"].Rows[0];

                        this.txtEndereco.Text = dRow.ItemArray.GetValue(0).ToString();
                        this.txtBairro.Text = dRow.ItemArray.GetValue(1).ToString();
                        this.txtCidade.Text = dRow.ItemArray.GetValue(2).ToString();
                        this.txtEstado.Text = dRow.ItemArray.GetValue(3).ToString();
                    }
                    else
                    {
                        MessageBox.Show("Cep não cadastrado");

                        this.txtEndereco.Focus();
                    }
                }
            }
        }

        private void SalvarEmpresa(object sender, EventArgs e)
        {
            int _cep = int.Parse(this.txtCEP.Text);

            DexComanda.Models.Empresa Empresa = new Models.Empresa()
            {
                Nome = txtNome.Text,
                Telefone = txtTelefone1.Text,
                Telefone2 = txtTelefone2.Text,
                Endereco = txtEndereco.Text,
                Cep = _cep,
                Cidade = txtCidade.Text,
                Numero = int.Parse(txtNumero.Text),
                Bairro = txtBairro.Text,
                UF = txtEstado.Text,
                PontoReferencia = txtPontoReferencia.Text,
              
            };
            Con.Insert("spAdicionaEmpresa", Empresa);
            MessageBox.Show("Empresa Cadastrada com Sucesso");

            this.Dispose();
            Application.Restart();
          //  ClearForm(this);
            
        }

        private void btnConectarAoBanco_Click(object sender, EventArgs e)
        {
           
            
        }

        private void txtCNPJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Campo = txtCNPJ.Text;
            if (e.KeyChar == 13)
            {
                if (Models.ValidarCNPJ.IsCnpj(Campo))
                {
                    txtContato.Focus();
                }
            }
        }
    }
}