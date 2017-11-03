using DexComanda.Models.IntegracaoIFood;
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
    public partial class frmDadosiFood : Form
    {
        private Conexao con;
        private int codigo;
        public frmDadosiFood()
        {
            InitializeComponent();
            con = new Conexao();
        }

        private void frmDadosiFood_Load(object sender, EventArgs e)
        {
            ListaRegistros();
        }
        private void ListaRegistros()
        {
            try
            {
                Utils.PopularGrid_SP("IntegracaoIFood", DadosGridView, "spObter_Integracao");
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void AdicionarRegistro(object sender, EventArgs e)
        {
            try
            {
                if (cbxOrigemCliente.SelectedText=="")
                {
                    MessageBox.Show("Selecione a classificação do Cliente");
                    cbxOrigemCliente.Focus();
                    return;
                }
                if (DadosGridView.Rows.Count>1)
                {
                    MessageBox.Show("Você já possui um registro gravado ");
                    return;
                }
                if (txtSenha.Text=="" || txtUsuario.Text=="")
                {
                    MessageBox.Show("Preencha corretamente os campos Usuario e senha");
                    return;
                }

                Integracao_IFood newIntegra = new Integracao_IFood()
                {
                    UserName = txtUsuario.Text,
                    Senha = txtSenha.Text,
                    CodOrigem = int.Parse(cbxOrigemCliente.SelectedValue.ToString())
                };
                con.Insert("spAdicionar_Integracao", newIntegra);
                ListaRegistros();
                Utils.LimpaForm(groupBox1);
            }
            catch (Exception erro)
            {

                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        private void SalvarRegistro(object sender, EventArgs e)
        {
            try
            {
                Integracao_IFood newIntegra = new Integracao_IFood()
                {
                    Codigo = codigo,
                    Senha = txtSenha.Text,
                    UserName = txtUsuario.Text,
                    CodOrigem = int.Parse(cbxOrigemCliente.SelectedValue.ToString())
                };
                con.Update("spAlterar_Integracao", newIntegra);
                ListaRegistros();
                Utils.LimpaForm(this);
                this.btnAdicionar.Text = "Adicionar [F12]";
                this.btnAdicionar.Click += new System.EventHandler(this.AdicionarRegistro);
                this.btnAdicionar.Click -= new System.EventHandler(this.SalvarRegistro);

                this.btnEditar.Text = "Editar [F11]";
                this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
                this.btnEditar.Click -= new System.EventHandler(this.Cancelar);

            }
            catch (Exception erro)
            {

                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        private void Cancelar(object sender, EventArgs e)
        {
            Utils.LimpaForm(this);
        }
        private void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                codigo = int.Parse(DadosGridView.CurrentRow.Cells["Codigo"].Value.ToString());
                txtSenha.Text = DadosGridView.CurrentRow.Cells["Senha"].Value.ToString();
                txtUsuario.Text = DadosGridView.CurrentRow.Cells["UserName"].Value.ToString();
                Utils.MontaCombox(cbxOrigemCliente, "NomeOrigem", "CodOrigem", "IntegracaoIFood", "spObter_IntegracaoPorCodigo",
                    int.Parse(DadosGridView.CurrentRow.Cells["CodOrigem"].Value.ToString()));
                this.btnAdicionar.Text = "Salvar [F12]";
                this.btnAdicionar.Click += new System.EventHandler(this.SalvarRegistro);
                this.btnAdicionar.Click -= new System.EventHandler(this.AdicionarRegistro);

                this.btnEditar.Text = "Cancelar [ESC]";
                this.btnEditar.Click += new System.EventHandler(this.Cancelar);
                this.btnEditar.Click -= new System.EventHandler(this.EditarRegistro);

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void cbxOrigemCliente_DropDown(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxOrigemCliente, "Nome", "Codigo", "Pessoa_OrigemCadastro", "spObterPessoa_OrigemCadastro");
        }
    }
}
