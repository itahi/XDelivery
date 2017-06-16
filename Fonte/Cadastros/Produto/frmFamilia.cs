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

namespace DexComanda.Cadastros.Produto
{
    public partial class frmFamilia : Form
    {
        private Conexao con;
        int rowIndex;
        int codigo;
        public frmFamilia()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void btnAdicionarGrupo_Click(object sender, EventArgs e)
        {
            if (txtNome.Text=="")
            {
                MessageBox.Show("Preencha o nome da familia");
                txtNome.Focus();
                return;
            }
            Familia fami = new Familia()
            {
                Nome = txtNome.Text,
                AtivoSN = chkAtivo.Checked,
                DataAlteracao = DateTime.Now,
                OnlineSN = chkOnline.Checked,
                PaiSN = true
            };
            con.Insert("spAdicionarFamilia", fami);
            Utils.ControlaEventos("Inserir", this.Name);
            Utils.LimpaForm(this);
            Utils.PopularGrid_SP("Grupo", FamiliaGridView, "spObterFamilia");
        }

        private void EditarFamilia(object sender, EventArgs e)
        {
            codigo = int.Parse(this.FamiliaGridView.Rows[rowIndex].Cells[0].Value.ToString());
            DataSet dsFamilia = con.SelectRegistroPorCodigo("Grupo", "spObterFamiliaPorCodigo", codigo);
            if (dsFamilia.Tables[0].Rows.Count>0)
            {
                txtNome.Text = dsFamilia.Tables[0].Rows[0].Field<string>("NomeGrupo");
                chkAtivo.Checked = dsFamilia.Tables[0].Rows[0].Field<Boolean>("AtivoSN");
                chkOnline.Checked = dsFamilia.Tables[0].Rows[0].Field<Boolean>("OnlineSN");

                this.btnAdicionarGrupo.Text = "Salvar [F12]";
                this.btnAdicionarGrupo.Click += new System.EventHandler(this.SalvarGrupo);
                this.btnAdicionarGrupo.Click -= new System.EventHandler(this.btnAdicionarGrupo_Click);

                this.btnEditarGrupo.Text = "Cancelar [ESC]";
                this.btnEditarGrupo.Click += new System.EventHandler(this.Cancelar);
                this.btnEditarGrupo.Click -= new System.EventHandler(this.EditarFamilia);

            }

            
           
        }
        private void Cancelar(object sender, EventArgs e)
        {

           
            this.btnAdicionarGrupo.Text = "Adicionar";
            this.btnAdicionarGrupo.Click += new System.EventHandler(this.btnAdicionarGrupo_Click);
            this.btnAdicionarGrupo.Click -= new System.EventHandler(this.SalvarGrupo);

            this.btnEditarGrupo.Text = "Editar";
            this.btnEditarGrupo.Click += new System.EventHandler(this.EditarFamilia);
            this.btnEditarGrupo.Click -= new System.EventHandler(this.Cancelar);
        }
        private void SalvarGrupo(object sender, EventArgs e)
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Preencha o nome da familia");
                txtNome.Focus();
                return;
            }
            Familia fam = new Familia()
            {
                Codigo = codigo,
                Nome = txtNome.Text,
                AtivoSN = chkAtivo.Checked,
                DataAlteracao = DateTime.Now,
                OnlineSN = chkOnline.Checked,
                PaiSN = true
            };
            con.Update("spAlterarFamilia", fam);
            Utils.LimpaForm(this);
            Utils.PopularGrid_SP("Grupo", FamiliaGridView, "spObterFamilia");
            Utils.ControlaEventos("Alterar", this.Name);
            this.btnAdicionarGrupo.Text = "Adicionar [F12]";
            this.btnAdicionarGrupo.Click += new System.EventHandler(this.btnAdicionarGrupo_Click);
            this.btnAdicionarGrupo.Click -= new System.EventHandler(this.SalvarGrupo);

            this.btnEditarGrupo.Text = "Editar [F11]";
            this.btnEditarGrupo.Click += new System.EventHandler(this.EditarFamilia);
            this.btnEditarGrupo.Click -= new System.EventHandler(this.Cancelar);
          
        }

        private void FamiliaGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void frmFamilia_Load(object sender, EventArgs e)
        {
            Utils.PopularGrid_SP("Grupo", FamiliaGridView, "spObterFamilia");
        }

        private void MenuAuxiliar(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem ExcluirFamilia = new MenuItem("Excluir Familia");
                ExcluirFamilia.Click += DeletarFamilia;
                m.MenuItems.Add(ExcluirFamilia);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }
        private void DeletarFamilia(object sender, EventArgs e)
        {
            try
            {
                if (FamiliaGridView.SelectedRows.Count > 0)
                {
                    int CodGrupo = int.Parse(this.FamiliaGridView.SelectedCells[0].Value.ToString());
                    con.DeleteAll("Familia", "spExcluirFamilia", CodGrupo);
                    Utils.ControlaEventos("Excluir", this.Name);
                    MessageBox.Show("Item excluído com sucesso.");
                    Utils.PopularGrid("Familia", FamiliaGridView, "Codigo,Nome");

                }
                else
                {
                    MessageBox.Show("Selecione a Familia para excluir");
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possivel excluir o registro " + erro.Message);
            }
            

        }

        private void frmFamilia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.F12 && btnAdicionarGrupo.Text== "Adicionar [F12]")
            {
                btnAdicionarGrupo_Click(sender, e);
            }
            if (e.KeyCode == Keys.F12 && btnAdicionarGrupo.Text == "Salvar [F12]")
            {
                SalvarGrupo(sender, e);
            }

            if (e.KeyCode == Keys.F11 && btnEditarGrupo.Text == "Editar [F11]")
            {
                EditarFamilia(sender, e);
            }

        }
    }
}
