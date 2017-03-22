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

        }
        private void ExibirRegistros()
        {
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
                    DataAlteracao = DateTime.Now,
                    DataCadastro = DateTime.Now,
                    Preco = decimal.Parse(txtPreco.Text.Replace("R$", "")),
                    UnidadeMedida = cbxUndMedida.SelectedText
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
                    Preco = decimal.Parse(txtPreco.Text),
                    UnidadeMedida = cbxUndMedida.SelectedText.ToString()
                };
                con.Update("spAlterarInsumo", ins);
                ExibirRegistros();
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
                if (registrosGridView.CurrentRow.Cells[0].Value.ToString()=="")
                {
                    return;
                }
                 codigo =int.Parse(registrosGridView.CurrentRow.Cells["Codigo"].Value.ToString());
                txtNome.Text = registrosGridView.CurrentRow.Cells["Nome"].Value.ToString();
                txtPreco.Text = registrosGridView.CurrentRow.Cells["Preco"].Value.ToString();
                cbxUndMedida.Text = registrosGridView.CurrentRow.Cells["UnidadeMedida"].Value.ToString();
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
    }
}
