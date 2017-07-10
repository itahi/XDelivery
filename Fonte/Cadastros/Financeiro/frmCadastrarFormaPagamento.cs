
using DexComanda.Models;
using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            Utils.PopularGrid_SP("FormaPagamento", FPGridView, "spObterFormaPagamento");
        }

        private void Cancelar(object sender, EventArgs e)
        {

            Button iButton = (Button)sender;

            if (iButton.Name == "btnEditar")
            {
                this.txtNomeFP.Text = "";
            }
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            this.btnAdicionar.Click -= new System.EventHandler(this.SalvarFP);
            codigo = 0;
            this.btnEditarFP.Text = "Editar";
            this.btnEditarFP.Click += new System.EventHandler(this.btnEditarFP_Click);
            this.btnEditarFP.Click -= new System.EventHandler(this.Cancelar);
        }


        //Esse metodo é para salvar
        private void SalvarFP(object sender, EventArgs e)
        {
            DexComanda.Models.FormasPagamento fp = new DexComanda.Models.FormasPagamento()
            {
                Codigo = codigo,
                Descricao = this.txtNomeFP.Text.ToString(),
                DescontoSN = chkDesconto2.Checked,
                GeraFinanceiro = chkFinanceiro.Checked,
                OnlineSN = chkOnline.Checked,
                DataAlteracao = DateTime.Now,
                CaminhoImagem = txtcaminhoImage.Text,
                AtivoSN = chkAtivoSN.Checked
                
            };

            con.Update("spAlterarFormaPagamento", fp);
            Utils.ControlaEventos("Alterar", this.Name);
            txtcaminhoImage.Text = "";
            txtNomeFP.Text = "";
            this.btnAdicionar.Text = "Adicionar [F12]";

            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            this.btnAdicionar.Click -= new System.EventHandler(this.SalvarFP);

            this.btnEditarFP.Text = "Editar [F11]";
            this.btnEditarFP.Click += new System.EventHandler(this.btnEditarFP_Click);
            this.btnEditarFP.Click -= new System.EventHandler(this.Cancelar);
            
            Utils.PopularGrid_SP("FormaPagamento", FPGridView, "spObterFormaPagamento");
        }

        private void formaPgtGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowIndex = e.RowIndex;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel obter o código da linha " + erro.Message);
            }
            
                   
            
        }

        private void frmCadastrarFormaPagamento_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F12) && (btnAdicionar.Text == "Adicionar [F12]"))
            {
                btnAdicionar_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F11 && btnEditar.Text == "Editar [F11")
            {
                btnEditarFP_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F12 && btnAdicionar.Text == "Salvar [F12]")
            {
                SalvarFP(sender, e);
            }

        }

        private void DeletarRegistro(object sender, EventArgs e)
        {
            if (FPGridView.SelectedRows.Count > 0)
            {
                int CodRegistro = int.Parse(this.FPGridView.SelectedCells[0].Value.ToString());
                con.DeleteAll("FormaPagamento", "spExcluirFormaPagamento", CodRegistro);
                Utils.ControlaEventos("Excluir", this.Name);
                MessageBox.Show("Item excluído com sucesso.");
                Utils.PopularGrid_SP("FormaPagamento", FPGridView, "spObterFormaPagamento");

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

        private void frmCadastrarFormaPagamento_Load(object sender, EventArgs e)
        {

        }

        private void SelecionarImagem(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            opn.Title = "Selecione a imagem da bandeira para exibir no site";
            opn.CheckFileExists = true;
            opn.Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" + "All files (*.*)|*.*";
            if (opn.ShowDialog() == DialogResult.OK)
            {
                txtcaminhoImage.Text = opn.FileName.ToString();

                if (File.Exists(txtcaminhoImage.Text))
                {
                    img.Load(txtcaminhoImage.Text);
                }

                con.AtualizaDataSincronismo("FormaPagamento", codigo, "DataFoto");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtcaminhoImage.Text = "";
            img.Dispose();
            con.AtualizaDataSincronismo("Produto", codigo, "DataFoto");
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            DexComanda.Models.FormasPagamento fp = new DexComanda.Models.FormasPagamento()
            {
                Codigo = codigo,
                Descricao = this.txtNomeFP.Text.ToString(),
                DescontoSN = chkDesconto2.Checked,
                GeraFinanceiro = chkFinanceiro.Checked,
                OnlineSN = chkOnline.Checked,
                AtivoSN = chkAtivoSN.Checked,
                DataAlteracao = DateTime.Now,
                CaminhoImagem = txtcaminhoImage.Text
            };

            if (txtNomeFP.Text != "")
            {
                con.Insert("spAdicionarFormaPagamento", fp);
                Utils.ControlaEventos("Inserir", this.Name);
                Utils.LimpaForm(this);
                Utils.PopularGrid_SP("FormaPagamento", FPGridView, "spObterFormaPagamento");
            }
            else
            {
                MessageBox.Show("Preencha o nome para Continuar", "DexAviso");
            }
            //Adicionar(sender, e);
        }

        private void btnEditarFP_Click(object sender, EventArgs e)
        {
            if (FPGridView.SelectedRows.Count > 0)
            {
                codigo = int.Parse(this.FPGridView.CurrentRow.Cells[0].Value.ToString());
                this.txtNomeFP.Text = this.FPGridView.CurrentRow.Cells["Descricao"].Value.ToString();
                chkDesconto2.Checked = Convert.ToBoolean(this.FPGridView.CurrentRow.Cells[2].Value.ToString());
                chkFinanceiro.Checked = Convert.ToBoolean(this.FPGridView.CurrentRow.Cells[3].Value.ToString());
                chkOnline.Checked = Convert.ToBoolean(this.FPGridView.CurrentRow.Cells[4].Value.ToString());
                txtcaminhoImage.Text = FPGridView.CurrentRow.Cells[5].Value.ToString();
                chkAtivoSN.Checked = Convert.ToBoolean(FPGridView.CurrentRow.Cells[6].Value.ToString());
                this.btnAdicionar.Text = "Salvar [F12]";
                this.btnAdicionar.Click += new System.EventHandler(this.SalvarFP);
                this.btnAdicionar.Click -= new System.EventHandler(this.btnAdicionar_Click);

                this.btnEditarFP.Text = "Cancelar";
                this.btnEditarFP.Click += new System.EventHandler(this.Cancelar);
                this.btnEditarFP.Click -= new System.EventHandler(this.btnEditarFP_Click);
            }
            else
            {
                MessageBox.Show("Selecione um registro para editar", "Dex Aviso");
            }

            //Editar(sender, e);
        }

        private void txtcaminhoImage_TextChanged(object sender, EventArgs e)
        {
            if (txtcaminhoImage.Text == "")
            {
                return;
            }
            if (File.Exists(txtcaminhoImage.Text)) 
            {
                img.Load(txtcaminhoImage.Text);
            }
            else
            {
                MessageBox.Show("Arquivo de imagem não existe no caminho " + txtcaminhoImage.Text + " informado, favor verificar");
            }
        }

        private void frmCadastrarFormaPagamento_KeyDown_1(object sender, KeyEventArgs e)
        {
        }
    }
}
