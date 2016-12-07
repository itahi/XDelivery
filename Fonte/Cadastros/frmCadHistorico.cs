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

namespace DexComanda.Cadastros
{
    public partial class frmCadMotivosCancelamento : Form
    {
        Conexao con;
        int rowIndex;
        int codigo;

        public frmCadMotivosCancelamento()
        {
            InitializeComponent();
            con = new Conexao();
            Utils.PopularGrid("MotivoCancelamento", MotivosGridView);

            for (int i = 0; i < MotivosGridView.Columns.Count; i++)
            {
                if (MotivosGridView.Columns[i].HeaderText != "Nome")
                {
                    MotivosGridView.Columns.RemoveAt(i);
                }
            }
        }

        private void Adicionar(object sender, EventArgs e)
        {
            try
            {
                MotivoCancelamento motivoCancelamento = new MotivoCancelamento()
                {
                    Nome = txtNome.Text,
                    DataCadastro = DateTime.Now
                };

                con.Insert("spAdicionaMotivoCancelamento", motivoCancelamento);
                Utils.LimpaForm(this);
                Utils.ControlaEventos("Inserir", this.Name);
                Utils.PopularGrid_SP("MotivoCancelamento", MotivosGridView,"spObterMotivoCancelamento");
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao gravar registro :" + ex.Message,"Dex Aviso");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
           
                
            codigo = int.Parse(this.MotivosGridView.CurrentRow.Cells[0].Value.ToString());
            this.txtNome.Text = this.MotivosGridView.CurrentRow.Cells[1].Value.ToString();

            this.btnAdicionar.Text = "Salvar [F12]";
            this.btnAdicionar.Click += new System.EventHandler(this.SalvarMotivo);
            this.btnAdicionar.Click -= new System.EventHandler(this.Adicionar);

            this.btnEditar.Text = "Cancelar [ESC]";
            this.btnEditar.Click += new System.EventHandler(this.Cancelar);
            this.btnEditar.Click -= new System.EventHandler(this.btnEditar_Click);
          
        }

        private void Cancelar(object sender, EventArgs e)
        {

            Button iButton = (Button)sender;

            if (iButton.Name == "btnEditar")
            {
                this.txtNome.Text = "";
            }
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.Click += new System.EventHandler(this.Adicionar);
            this.btnAdicionar.Click -= new System.EventHandler(this.SalvarMotivo);

            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
        }
        private void SalvarMotivo(object sender, EventArgs e)
        {
            try
            {
                MotivoCancelamento motivos = new MotivoCancelamento()
                {
                    Codigo = codigo,
                    Nome = txtNome.Text,
                    DataCadastro = Convert.ToDateTime(MotivosGridView.SelectedRows[rowIndex].Cells[2].Value.ToString())
                };
                if (txtNome.Text!="")
                {
                    con.Update("spAlteraMotivoCancelamento", motivos);
                    Utils.LimpaForm(this);
                    Utils.ControlaEventos("Alterar", this.Name);
                    Utils.PopularGrid("MotivoCancelamento", MotivosGridView);  
                }
                else
                {
                    MessageBox.Show("Preencha corretamente os dados","DEX Aviso");
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Não foi possivel editar o Registro:"+ex.Message, "DEX Aviso");
            }

            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.Click += new System.EventHandler(this.Adicionar);
            this.btnAdicionar.Click -= new System.EventHandler(this.SalvarMotivo);

            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);

            this.txtNome.Text = "";

        }

        private void frmCadMotivosCancelamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12 && btnAdicionar.Text == "Adicionar [F12]")
            {
                Adicionar(sender, e);
            }
            else if (btnAdicionar.Text == "Salvar [F12]" && e.KeyCode == Keys.F12)
            {
                SalvarMotivo(sender, e);
            }

            else if (e.KeyCode == Keys.F11 && btnEditar.Text == "Editar [F11]")
            {
                btnEditar_Click(sender, e);
            }
            else if (btnEditar.Text == "Cancelar [ESC]" && e.KeyCode == Keys.Escape)
            {
                Cancelar(sender, e);
            }
        }

        private void Excluir(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView dgv = sender as DataGridView;

                ContextMenu m = new ContextMenu();
                MenuItem ExcluirFP = new MenuItem(" 0 - Excluir Registro");
                ExcluirFP.Click += DeletarRegistro;
                m.MenuItems.Add(ExcluirFP);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));
            }

        }
        private void DeletarRegistro(object sender, EventArgs e)
        {
            try
            {
                if (MotivosGridView.SelectedRows.Count > 0)
                {
                    int CodRegistro = int.Parse(this.MotivosGridView.SelectedCells[0].Value.ToString());
                    con.DeleteAll("MOtivoCancelamento", "spExcluirMotivoCancelamento", CodRegistro);
                    Utils.ControlaEventos("Excluir", this.Name);
                    MessageBox.Show("Item excluído com sucesso.");
                    Utils.PopularGrid("MotivoCancalemento", MotivosGridView);

                }
                else
                {
                    MessageBox.Show("Selecione o grupo para excluir");
                }

            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possivel excluir o registro - Motivo :" + erro.Message);
            }
            
        }

        private void MotivosGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEditar_Click(sender, e);
        }
    }
}
