using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DexComanda.Models;

namespace DexComanda
{
    public partial class frmCadastrarEntregador : Form
    {
        Conexao con;
        int rowIndex;
        int codigo;

        public frmCadastrarEntregador()
        {
            InitializeComponent();
            con = new Conexao();
            ListaEntregadores();
        }
        private void ListaEntregadores()
        {
            this.EntregGridView.DataSource = null;
            this.EntregGridView.AutoGenerateColumns = true;
            this.EntregGridView.DataSource = con.SelectAll("Entregador", "spObterEntregadores");
            this.EntregGridView.DataMember = "Entregador";

        }

        private void AdicionarEntregador(object sender, EventArgs e)
        {
            try
            {
                Entregador entregador = new Entregador()
                {
                    Nome = txtNome.Text
                    
                };
                if (txtComissao.Text.Trim()!="")
                {
                    entregador.Comissao = decimal.Parse(txtComissao.Text);
                }
                else
	            {
                    entregador.Comissao = 0;
	            }
                con.Insert("spAdicionarEntregador", entregador);
                Utils.ControlaEventos("Inserir", this.Name);
                MessageBox.Show("Registro adicionado", "Sucesso");
                Utils.LimpaForm(this);
                ListaEntregadores();
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
        }

        private void EditarRegistro(object sender, EventArgs e)
        {
            codigo = int.Parse(this.EntregGridView.CurrentRow.Cells[0].Value.ToString());
            this.txtNome.Text = this.EntregGridView.CurrentRow.Cells[1].Value.ToString();
            this.txtComissao.Text = this.EntregGridView.CurrentRow.Cells[2].Value.ToString();

            this.btnAdicionar.Text = "Salvar [F12]";
            this.btnAdicionar.Click -= new System.EventHandler(this.AdicionarEntregador);
            this.btnAdicionar.Click += new System.EventHandler(this.AtualizarRegistro);

            this.btnEditar.Text = "Cancelar [ESC]";
            this.btnEditar.Click += new System.EventHandler(this.Cancelar);
            this.btnEditar.Click -= new System.EventHandler(this.EditarRegistro);  
        }

        private void Cancelar(object sender, EventArgs e)
        {

            Button iButton = (Button)sender;

            if (iButton.Name == "btnEditarGrupo")
            {
                this.txtNome.Text = "";
            }
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.Click += new System.EventHandler(this.AdicionarEntregador);
            this.btnAdicionar.Click -= new System.EventHandler(this.AtualizarRegistro);

            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.Click += new System.EventHandler(this.Cancelar);
            this.btnEditar.Click -= new System.EventHandler(this.EditarRegistro); 
        }
        private void AtualizarRegistro(object sender, EventArgs e)
        {
            Entregador entregador = new Entregador()
            {
                Codigo = codigo,
                Nome = txtNome.Text,
                Comissao = decimal.Parse(txtComissao.Text)
            };
            if (txtNome.Text!="")
            {
                con.Update("spAlterarEntregador", entregador);
                Utils.ControlaEventos("Alterar", this.Name);
                this.btnAdicionar.Text = "Adicionar [F12]";
                this.btnAdicionar.Click -= new System.EventHandler(this.AtualizarRegistro);

                this.btnEditar.Text = "Editar [F11]";
                this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
                this.btnEditar.Click -= new System.EventHandler(this.Cancelar);

                this.txtNome.Text = "";
                this.txtComissao.Text = "";
                ListaEntregadores();
            }
            else
            {
                MessageBox.Show("Preencha o nome para continuar", "Dex Aviso");
                txtNome.Focus();
            }

        }

        private void EntregGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int total = this.EntregGridView.SelectedRows.Count;

            for (int i = 0; i < total; i++)
            {
                if (this.EntregGridView.Rows[i].Selected)
                {
                    rowIndex = this.EntregGridView.Rows[i].Index;
                }
            }
        }

        private void frmCadastrarEntregador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12 && btnAdicionar.Text == "Adicionar [F12]")
            {
                AdicionarEntregador(sender, e);
            }
            else if (e.KeyCode == Keys.F12 && btnAdicionar.Text =="Salvar [F12]" )
            {
                EditarRegistro(sender, e);
            }
        }

        private void frmCadastrarEntregador_Load(object sender, EventArgs e)
        {

        }
    }
}
