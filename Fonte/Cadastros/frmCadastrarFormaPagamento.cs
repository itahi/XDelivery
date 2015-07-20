
using DexComanda.Models;
using Dominio;
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
    public partial class frmCadastrarFormaPagamento : Form
    {
        Conexao con;
        int rowIndex;
        int codigo;

        public frmCadastrarFormaPagamento()
        {
            InitializeComponent();
            con = new Conexao();
            Utils.PopularGrid("FormaPagamento", FPGridView, "spObterFormaPagamento");
        }

        private void Adicionar(object sender, EventArgs e)
        {
            DexComanda.Models.FormasPagamento fp = new DexComanda.Models.FormasPagamento()
            {
                Codigo = codigo,
                Descricao = this.txtNomeFP.Text.ToString(),
                DescontoSN = chkDesconto2.Checked
            };

            if (txtNomeFP.Text != "")
            {
                con.Insert("spAdicionarFormaPagamento", fp);
                Utils.ControlaEventos("Inserir",this.Name);
                Utils.LimpaForm(this);
                Utils.PopularGrid("FormaPagamento", FPGridView, "spObterFormaPagamento");  
            }
            else
            {
                MessageBox.Show("Preencha o nome para Continuar", "DexAviso");
            }
            
        }

        //Só habilita a edicao trocando os nomes os metodos dos botoes
        private void Editar(object sender, EventArgs e)
        {
            if (FPGridView.SelectedRows.Count >0)
            {
                codigo = int.Parse(this.FPGridView.SelectedRows[rowIndex].Cells[0].Value.ToString());
                this.txtNomeFP.Text = this.FPGridView.SelectedRows[rowIndex].Cells[1].Value.ToString();
                chkDesconto2.Checked = Convert.ToBoolean(this.FPGridView.SelectedRows[rowIndex].Cells[2].Value.ToString()); 

                this.btnAdicionar.Text = "Salvar";
                this.btnAdicionar.Click += new System.EventHandler(this.SalvarFP);
                this.btnAdicionar.Click -= new System.EventHandler(this.Adicionar);

                this.btnEditarFP.Text = "Cancelar";
                this.btnEditarFP.Click += new System.EventHandler(this.Cancelar);
                this.btnEditarFP.Click -= new System.EventHandler(this.Editar); 
            }
            else
            {
                MessageBox.Show("Selecione um registro para editar", "Dex Aviso");
            }
            
        }

        private void Cancelar(object sender, EventArgs e)
        {

            Button iButton = (Button)sender;

            if (iButton.Name == "btnEditar")
            {
                this.txtNomeFP.Text = "";
            }
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.Click += new System.EventHandler(this.Adicionar);
            this.btnAdicionar.Click -= new System.EventHandler(this.SalvarFP);
            codigo = 0;
            this.btnEditarFP.Text = "Editar";
            this.btnEditarFP.Click += new System.EventHandler(this.Editar);
            this.btnEditarFP.Click -= new System.EventHandler(this.Cancelar);
        }


        //Esse metodo é para salvar
        private void SalvarFP(object sender, EventArgs e)
        {
            DexComanda.Models.FormasPagamento fp = new DexComanda.Models.FormasPagamento()
            {
                Codigo      = codigo,
                Descricao   = this.txtNomeFP.Text.ToString(),
                DescontoSN  = chkDesconto2.Checked 
            };

            con.Update("spAlterarFormaPagamento", fp);
            Utils.ControlaEventos("Alterar", this.Name);

            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.Click += new System.EventHandler(this.Adicionar);
            this.btnAdicionar.Click -= new System.EventHandler(this.SalvarFP);

            this.btnEditarFP.Text = "Editar";
            this.btnEditarFP.Click += new System.EventHandler(this.Editar);
            this.btnEditarFP.Click -= new System.EventHandler(this.Cancelar);

            this.txtNomeFP.Text = "";
            Utils.PopularGrid("FormaPagamento", FPGridView, "spObterFormaPagamento");
        }

        private void formaPgtGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int total = this.FPGridView.SelectedRows.Count;

            for (int i = 0; i < total; i++)
            {
                if (this.FPGridView.Rows[i].Selected)
                {
                    rowIndex = this.FPGridView.Rows[i].Index;
                }
            }
        }

        private void frmCadastrarFormaPagamento_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F12) && (btnAdicionar.Text == "Adicionar [F12]"))
            {
                Adicionar(sender, e);
            }
           
        }

        private void DeletarRegistro(object sender, EventArgs e)
        {
            if (FPGridView.SelectedRows.Count > 0)
            {
                int CodRegistro = int.Parse(this.FPGridView.SelectedCells[0].Value.ToString());
                con.DeleteAll("FormaPagamento", "spExcluirFormaPagamento", CodRegistro);
                Utils.ControlaEventos("Excluir",this.Name);
                MessageBox.Show("Item excluído com sucesso.");
                Utils.PopularGrid("FormaPagamento", FPGridView, "spObterFormaPagamento");

            }
            else
            {
                MessageBox.Show("Selecione o grupo para excluir");
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
            
    }
}
