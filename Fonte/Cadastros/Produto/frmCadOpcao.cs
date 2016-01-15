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
    public partial class frmCadOpcao : Form
    {
        private Conexao con;
        private int codigoAlterarDeletar;
        private int rowIndex;
        public frmCadOpcao()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void CadastraOpcao(object sender, EventArgs e)
        {
            try
            {
                if (txtNome.Text.Trim() != "" && cbxTipo.Text.Trim() != "")
                {
                    Opcao opcao = new Opcao()
                    {
                        DataAlteracao = DateTime.Now,
                        Nome = txtNome.Text,
                        Tipo = cbxTipo.SelectedValue.ToString(),
                        OnlineSN = chkOnlineSN.Checked,
                        AtivoSN = chkAtivoSN.Checked

                    };
                    con.Insert("spAdicionarOpcao", opcao);
                    Utils.LimpaForm(this);
                    ListaOpcao();
                }
                else
                {
                    MessageBox.Show("Campos obrigatórios não preenchidos", "[xSistemas]");
                    return;
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message, "xSistemas");
            }
        }

        private void ListaOpcao()
        {
            Utils.PopularGrid("Opcao", OpcaoGridView);
        }

        private void frmCadOpcao_Load(object sender, EventArgs e)
        {
           Utils.MontaCombox(cbxTipo, "Nome", "Codigo", "Produto_OpcaoTipo", "spObterTipoOpcao");

            ListaOpcao();
        }
        private void DeletaRegistro(object sender, EventArgs e)
        {
            try
            {
                if (OpcaoGridView.SelectedRows.Count > 0)
                {
                    codigoAlterarDeletar = int.Parse(this.OpcaoGridView.SelectedCells[0].Value.ToString());
                    con.DeleteAll("Opcao", "spExcluirOpcao", codigoAlterarDeletar);
                    Utils.ControlaEventos("Excluir", this.Name);
                    MessageBox.Show("Item excluído com sucesso.");
                    ListaOpcao();

                }
                else
                {
                    MessageBox.Show("Selecione o grupo para excluir");
                }
            }
            catch (Exception erro)
            {

                if (erro.Message.Contains("A instrução DELETE conflitou"))
                {
                    MessageBox.Show("Não foi possivel deletar o registro pois o mesmo está sendo usando em algum produto", "xSistemas ");
                }
            }
            

        }

        private void RetonaTipo(int iTipo)
        {
            Utils.MontaCombox(cbxTipo, "Nome", "Codigo", "Produto_OpcaoTipo", "spObterProduto_OpcaoTipoPorCodigo", iTipo);
        }

        private void EditarOpcao(object sender, EventArgs e)
        {
            try
            {
                codigoAlterarDeletar = int.Parse(this.OpcaoGridView.SelectedRows[rowIndex].Cells[0].Value.ToString());
                RetonaTipo(int.Parse(OpcaoGridView.SelectedRows[rowIndex].Cells[1].Value.ToString()));
                txtNome.Text = OpcaoGridView.SelectedRows[rowIndex].Cells[2].Value.ToString();
                chkAtivoSN.Checked = Convert.ToBoolean(OpcaoGridView.SelectedRows[rowIndex].Cells[3].Value.ToString());
                chkOnlineSN.Checked = Convert.ToBoolean(OpcaoGridView.SelectedRows[rowIndex].Cells[4].Value.ToString());
                this.btnAdicionar.Text = "Salvar [F12]";
                this.btnAdicionar.Click += new System.EventHandler(this.Salvar);
                this.btnAdicionar.Click -= new System.EventHandler(this.CadastraOpcao);

                this.btnEditar.Text = "Cancelar [ESC]";
                this.btnEditar.Click += new System.EventHandler(this.Cancelar);
                this.btnEditar.Click -= new System.EventHandler(this.EditarOpcao);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
           
        }
        private void Cancelar(object sender, EventArgs e)
        {
            Button iButton = (Button)sender;

            if (iButton.Name == "btnEditar")
            {
                Utils.LimpaForm(this);
            }
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.Click += new System.EventHandler(this.CadastraOpcao);
         
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.EditarOpcao);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
        }
        private void Salvar(object sender, EventArgs e)
        {
            try
            {
                Opcao opcao = new Opcao()
                {
                    Codigo = codigoAlterarDeletar,
                    Nome = txtNome.Text,
                    Tipo = cbxTipo.SelectedValue.ToString(),
                    DataAlteracao= DateTime.Now,
                    OnlineSN = chkOnlineSN.Checked,
                    AtivoSN = chkAtivoSN.Checked
                };
                con.Update("spAlteraOpcao", opcao);
                Utils.LimpaForm(this);

                this.btnAdicionar.Text = "Adicionar";
                this.btnAdicionar.Click += new System.EventHandler(this.CadastraOpcao);
                this.btnAdicionar.Click -= new System.EventHandler(this.Salvar);

                this.btnEditar.Text = "Editar";
                this.btnEditar.Click += new System.EventHandler(this.EditarOpcao);
                this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
              
               // ListaOpcao();

            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message, "xSistemas");
            }
        }

        private void OpcaoGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int total = this.OpcaoGridView.SelectedRows.Count;

            for (int i = 0; i < total; i++)
            {
                if (this.OpcaoGridView.Rows[i].Selected)
                {
                    rowIndex = this.OpcaoGridView.Rows[i].Index;
                }
            }
        }

        private void OpcaoGridView_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem Excluir = new MenuItem("Excluir Opcao");
                Excluir.Click += DeletaRegistro;
                m.MenuItems.Add(Excluir);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }

        private void cbxTipo_DropDown(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxTipo, "Nome", "Codigo", "Produto_OpcaoTipo", "spObterTipoOpcao");
        }
    }
}
