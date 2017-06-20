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

namespace DexComanda.Cadastros.Pessoa
{
    public partial class frmCadastroOrigem : Form
    {
        private Conexao con;
        private int codigo;
        public frmCadastroOrigem()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void frmCadastroOrigem_Load(object sender, EventArgs e)
        {
            ListaRegistros();
        }
        private void ListaRegistros()
        {
            try
            {
                this.OrigemGridView.AutoGenerateColumns = true;
                this.OrigemGridView.DataSource = Utils.PopularGrid_SP("Pessoa_OrigemCadastro", OrigemGridView, "spObterPessoa_OrigemCadastro");
                this.OrigemGridView.DataMember = "Pessoa_OrigemCadastro";
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        private void DeletaRegistro(object sender, EventArgs e)
        {
            if (OrigemGridView.SelectedRows.Count > 0)
            {
                int codRegistro = int.Parse(this.OrigemGridView.CurrentRow.Cells["Codigo"].Value.ToString());
                string strNome = OrigemGridView.CurrentRow.Cells["Nome"].Value.ToString();
                if (!Utils.MessageBoxQuestion("Deseja excluir o registro " + strNome + " ?"))
                {
                    return;
                }

                con.DeleteAll("Pessoa_OrigemCadastro", "spExcluirPessoa_OrigemCadastro", codRegistro);
                Utils.ControlaEventos("Excluir", this.Name);
                MessageBox.Show("Item excluído com sucesso.");
                ListaRegistros();
            }
            else
            {
                MessageBox.Show("Selecione o grupo para excluir");
            }

        }

        private void AdicionarRegistro(object sender, EventArgs e)
        {
            try
            {
                Pessoa_OrigemCadastro pess = new Pessoa_OrigemCadastro()
                {
                    Nome = txtNome.Text,
                    AtivoSN = chkAtivoSN.Checked
                };
                con.Insert("spAdicionarPessoa_OrigemCadastro", pess);
                Utils.LimpaForm(this);
                ListaRegistros();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void MenuAuxiliar(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem ExcluirProduto = new MenuItem("Excluir Registro?");
                ExcluirProduto.Click += DeletaRegistro;
                m.MenuItems.Add(ExcluirProduto);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }

        private void frmCadastroOrigem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12 && btnAdicionar.Text == "Adicionar [F12]")
            {
                AdicionarRegistro(sender, e);
            }
            else if (btnAdicionar.Text == "Salvar [F12]" && e.KeyCode == Keys.F12)
            {
                SalvarRegistro(sender, e);
            }

            else if (e.KeyCode == Keys.F11 && btnEditar.Text == "Editar [F11]")
            {
                EditarRegistro(sender, e);
            }
            else if (btnEditar.Text == "Cancelar [ESC]" && e.KeyCode == Keys.Escape)
            {
                Cancelar(sender, e);
            }
        }

        private void Cancelar(object sender, EventArgs e)
        {
            Button iButton = (Button)sender;

            if (iButton.Name == "btnEditarGrupo")
            {
                Utils.LimpaForm(this);
            }
        }
        private void SalvarRegistro(object sender, EventArgs e)
        {
            Pessoa_OrigemCadastro pess = new Pessoa_OrigemCadastro()
            {
                AtivoSN = chkAtivoSN.Checked,
                Codigo = codigo,
                Nome = txtNome.Text
            };
            con.Update("spAlterarPessoa_OrigemCadastro", pess);


            Utils.ControlaEventos("Alterar", this.Name);
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.Click += new System.EventHandler(this.AdicionarRegistro);
            this.btnAdicionar.Click -= new System.EventHandler(this.SalvarRegistro);

            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
            Utils.LimpaForm(this);
            ListaRegistros();

        }
        private void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                codigo = int.Parse(OrigemGridView.CurrentRow.Cells["Codigo"].Value.ToString());
                txtNome.Text = OrigemGridView.CurrentRow.Cells["Nome"].Value.ToString();
                chkAtivoSN.Checked = Convert.ToBoolean(OrigemGridView.CurrentRow.Cells["AtivoSN"].Value.ToString());


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
    }
}
