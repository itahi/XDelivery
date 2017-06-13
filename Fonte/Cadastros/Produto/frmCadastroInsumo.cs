using DexComanda.Models.Produto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Cadastros.Produto
{
    public partial class frmCadastroInsumo : Form
    {
        private Conexao con;
        private int codigo;
        public frmCadastroInsumo()
        {
            InitializeComponent();
            ExibirRegistros();
        }

        private void frmCadastroInsumo_Load(object sender, EventArgs e)
        {
            ExibirRegistros();
        }
        private void ExibirRegistros()
        {
            Utils.LimpaForm(this);
            con = new Conexao();
            this.registrosGridView.DataSource = null;
            this.registrosGridView.AutoGenerateColumns = true;
            this.registrosGridView.DataSource = Utils.PopularGrid_SP("Insumo", registrosGridView, "spObterInsumo");
            this.registrosGridView.DataMember = "Insumo";
        }

        private void AdicionarRegistro(object sender, EventArgs e)
        {
            try
            {
                Insumo insumo = new Insumo()
                {
                    AtivoSN = chkAtivoSN.Checked,
                    Nome = txtNome.Text,
                    UnidadeMedida = cbxUndMedida.Text
                };
                con.Insert("spAdicionarInsumo", insumo);
                ExibirRegistros();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        private void txtPreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }
        private void SalvarRegistro(object sender, EventArgs e)
        {
            try
            {
                Insumo ins = new Insumo()
                {
                    Codigo = codigo,
                    AtivoSN = chkAtivoSN.Checked,
                    Nome = txtNome.Text,
                    UnidadeMedida = cbxUndMedida.Text.ToString()
                };
                con.Update("spAlterarInsumo", ins);
                ExibirRegistros();

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

            Button iButton = (Button)sender;

            if (iButton.Name == "btnEditarGrupo")
            {
                Utils.LimpaForm(this);
            }
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.Click += new System.EventHandler(this.AdicionarRegistro);
            this.btnAdicionar.Click -= new System.EventHandler(this.SalvarRegistro);

            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
        }
        private void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                if (registrosGridView.CurrentRow.Cells[0].Value.ToString() == "")
                {
                    return;
                }
                codigo = int.Parse(registrosGridView.CurrentRow.Cells["Codigo"].Value.ToString());
                txtNome.Text = registrosGridView.CurrentRow.Cells["Nome"].Value.ToString();
                cbxUndMedida.Text =registrosGridView.CurrentRow.Cells["UnidadeMedida"].Value.ToString();
                chkAtivoSN.Checked = Convert.ToBoolean(registrosGridView.CurrentRow.Cells["AtivoSN"].Value.ToString());

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
        private void DeletaRegistro(object sender, EventArgs e)
        {
            try
            {
                if (registrosGridView.SelectedRows.Count > 0)
                {
                    int codRegistro = int.Parse(this.registrosGridView.CurrentRow.Cells["Codigo"].Value.ToString());
                    con.DeleteAll("Insumo", "spExcluirInsumo", codRegistro);
                    Utils.ControlaEventos("Excluir", this.Name);
                    MessageBox.Show("Item excluído com sucesso.");
                    ExibirRegistros();
                }
                else
                {
                    MessageBox.Show("Selecione o registro para excluir");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }


        }
        private void MenuAuxiliar(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenu m = new ContextMenu();
                    MenuItem Excluir = new MenuItem("Excluir Registro");
                    Excluir.Click += DeletaRegistro;
                    m.MenuItems.Add(Excluir);

                    int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                    m.Show(dgv, new Point(e.X, e.Y));

                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void frmCadastroInsumo_KeyDown(object sender, KeyEventArgs e)
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
    }
}
