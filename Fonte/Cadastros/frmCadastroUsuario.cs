using DexComanda.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda
{
    public partial class frmCadastroUsuario : Form
    {
        private Conexao con;
        private Usuario usuario;
        string mSenha;
        int codigo;
        public frmCadastroUsuario()
        {
            // this.parentMain = parent;

            InitializeComponent();
        }
        private void ListaRegistros()
        {
            try
            {
                con = new Conexao();
                DataSet dsUser = con.SelectAll("Usuario", "spObterUsuarioGrid");
                Utils.PopularGrid("Usuario", usuariosGridView, dsUser);
            }
            catch (Exception erro)
            {

                throw;
            }
        }

        private void frmCadastroUsuario_Load(object sender, EventArgs e)
        {
            ListaRegistros();

        }
        private Boolean NomeCadastrado(string iNomeUser)
        {
            Boolean iRetur = true;
            for (int i = 0; i < usuariosGridView.Rows.Count; i++)
            {
                if (iNomeUser == usuariosGridView.Rows[i].Cells["Nome"].Value.ToString())
                {
                    MessageBox.Show("Existe um usuario cadastrado com esse nome");
                    iRetur = false;
                }
            }
            return iRetur;
        }
        private void AdicionarSalvarUsuario(object sender, EventArgs e)
        {
            if (txtNomeUsuario.Text != "")
            {
                string hashSenha = Utils.EncryptMd5(this.txtNomeUsuario.Text.ToString(), this.txtSenha.Text.ToString());

                Usuario usuario = new Usuario()
                {
                    Nome = txtNomeUsuario.Text,
                    Senha = hashSenha.ToUpper(),
                    AlteraProdutosSN = chkAlteraProdutos.Checked,
                    CancelaPedidosSN = chkCancelaPedidos.Checked,
                    AdministradorSN = chkAdministrador.Checked,
                    AcessaRelatoriosSN = chkAcessaRelat.Checked,
                    FinalizaPedidoSN = chkFechaPedido.Checked,
                    DescontoPedidoSN = chkDescSN.Checked,
                    DescontoMax = double.Parse(txtDesconto.Text),
                    AbreFechaCaixaSN = chkAbreCaixa.Checked,
                    AlteraDadosClienteSN = chkEditaCliente.Checked,
                    EditaPedidoSN = chkAlteraPedido.Checked,
                    VisualizaDadosClienteSN = chkAbreCliente.Checked,
                    AtivoSN = chkAtivo.Checked

                };


                con.Insert("spAdicionarUsuario", usuario);
                MessageBox.Show("Usuario cadastrado com sucesso");
                Utils.ControlaEventos("Inserir", this.Name);
                Utils.PopularGrid_SP("Usuario", usuariosGridView, "spObterUsuario");
            }
            else
            {
                MessageBox.Show("Preecha corretamente os campos para continuar");

            }

        }
        private void this_FormClosing()
        {
            this.Dispose();
        }

        private void Editar(object sender, EventArgs e)
        {
            try
            {
                if (usuariosGridView.SelectedRows.Count > 0)
                {
                    codigo = int.Parse(usuariosGridView.CurrentRow.Cells[0].Value.ToString());
                    DataSet dsUserSelecionado = con.SelectRegistroPorCodigo("Usuario", "spObterUsuarioPorCodigo", codigo);
                    if (dsUserSelecionado.Tables[0].Rows.Count == 0)
                    {
                        return;
                    }

                    txtNomeUsuario.Text = dsUserSelecionado.Tables[0].Rows[0].Field<string>("Nome");
                    mSenha = dsUserSelecionado.Tables[0].Rows[0].Field<string>("Senha");
                    chkCancelaPedidos.Checked = dsUserSelecionado.Tables[0].Rows[0].Field<Boolean>("CancelaPedidosSN");
                    chkAlteraProdutos.Checked = dsUserSelecionado.Tables[0].Rows[0].Field<Boolean>("AlteraProdutosSN");
                    chkAdministrador.Checked = dsUserSelecionado.Tables[0].Rows[0].Field<Boolean>("AdministradorSN");
                    chkAcessaRelat.Checked = dsUserSelecionado.Tables[0].Rows[0].Field<Boolean>("AcessaRelatoriosSN");
                    chkFechaPedido.Checked = dsUserSelecionado.Tables[0].Rows[0].Field<Boolean>("FinalizaPedidoSN");
                    chkDescSN.Checked = dsUserSelecionado.Tables[0].Rows[0].Field<Boolean>("DescontoPedidoSN");
                    txtDesconto.Text = dsUserSelecionado.Tables[0].Rows[0].Field<decimal>("DescontoMax").ToString();

                    chkAlteraPedido.Checked = dsUserSelecionado.Tables[0].Rows[0].Field<Boolean>("EditaPedidoSN");
                    chkAbreCliente.Checked = dsUserSelecionado.Tables[0].Rows[0].Field<Boolean>("VisualizaDadosClienteSN");
                    chkAbreCaixa.Checked = dsUserSelecionado.Tables[0].Rows[0].Field<Boolean>("AbreFechaCaixaSN");
                    chkEditaCliente.Checked = dsUserSelecionado.Tables[0].Rows[0].Field<Boolean>("AlteraDadosClienteSN");
                    chkAtivo.Checked = dsUserSelecionado.Tables[0].Rows[0].Field<Boolean>("AtivoSN");

                    btnSalvar.Text = " Salvar ";
                    btnSalvar.Click += new System.EventHandler(this.SalvarEdicao);
                    btnSalvar.Click -= new System.EventHandler(this.AdicionarSalvarUsuario);

                    btnCancelar.Text = "Cancelar";
                    btnCancelar.Click += new System.EventHandler(this.CancelarEdicao);
                    btnCancelar.Click -= new System.EventHandler(this.Editar);
                }
                else
                {
                    MessageBox.Show("Selecione um registro para editar", "Dex Aviso");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            
        }

        private void CancelarEdicao(object sender, EventArgs e)
        {
            Utils.LimpaForm(this);
            Utils.PopularGrid_SP("Usuario", usuariosGridView, "spObterUsuario");
        }

        private void SalvarEdicao(object sender, EventArgs e)
        {

            if (txtNomeUsuario.Text == "")
            {
                MessageBox.Show("Preencha o nome do usuario");
                txtNomeUsuario.Focus();
                return;

            }
            if (txtSenha.Text == "")
            {
                MessageBox.Show("Preencha a senha do usuario");
                txtSenha.Focus();
                return;
            }
            Usuario user = new Usuario()
            {
                Codigo = codigo,
                Nome = txtNomeUsuario.Text,
                Senha = Utils.EncryptMd5(this.txtNomeUsuario.Text.ToString(), this.txtSenha.Text.ToString()),
                CancelaPedidosSN = chkCancelaPedidos.Checked,
                AlteraProdutosSN = chkAlteraProdutos.Checked,
                AdministradorSN = chkAdministrador.Checked,
                AcessaRelatoriosSN = chkAcessaRelat.Checked,
                FinalizaPedidoSN = chkFechaPedido.Checked,
                DescontoPedidoSN = chkDescSN.Checked,
                DescontoMax = double.Parse(txtDesconto.Text),
                AbreFechaCaixaSN = chkAbreCaixa.Checked,
                AlteraDadosClienteSN = chkEditaCliente.Checked,
                EditaPedidoSN = chkAlteraPedido.Checked,
                VisualizaDadosClienteSN = chkAbreCliente.Checked
            };

            con.Update("spAlterarUsuario", user);

            btnSalvar.Text = " Adicionar";
            this.btnSalvar.Click += new System.EventHandler(this.AdicionarSalvarUsuario);
            this.btnSalvar.Click -= new System.EventHandler(this.SalvarEdicao);

            this.btnCancelar.Text = "Editar";
            this.btnCancelar.Click += new System.EventHandler(this.Editar);
            this.btnCancelar.Click -= new System.EventHandler(this.CancelarEdicao);
            Utils.ControlaEventos("Alterar", this.Name);
            MessageBox.Show("Usuario alterado com sucesso");
            Utils.LimpaForm(this);
        }

        private void chkDescSN_CheckedChanged(object sender, EventArgs e)
        {
            txtDesconto.Enabled = chkDescSN.Checked;
            if (!chkDescSN.Checked)
            {
                txtDesconto.Text = "0";
            }
        }

        private void usuariosGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Editar(sender, e);
        }

        private void usuariosGridView_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem Excluir = new MenuItem("Excluir Usuário");
                Excluir.Click += DeletaRegistro;
                m.MenuItems.Add(Excluir);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }
        private void DeletaRegistro(object sender, EventArgs e)
        {
            try
            {
                if (usuariosGridView.SelectedRows.Count > 0)
                {
                    int intCod = int.Parse(this.usuariosGridView.CurrentRow.Cells[0].Value.ToString());
                    con.DeleteAll("Usuario", "spExcluirUsuario", intCod);
                    Utils.ControlaEventos("Excluir", this.Name);
                    MessageBox.Show("Usuario excluído com sucesso.");
                    ListaRegistros();

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
                    MessageBox.Show("Não foi possivel deletar o registro pois o mesmo ja foi usando ", "xSistemas ");
                    return;
                }
                MessageBox.Show(erro.Message);
            }


        }
    }
}
