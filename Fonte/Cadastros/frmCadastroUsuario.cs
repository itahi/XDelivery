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
        private Main parentMain;
        string mSenha;
        int rowIndex;
        int codigo;

        public frmCadastroUsuario()
        {
            // this.parentMain = parent;

            InitializeComponent();
        }

        private void frmCadastroUsuario_Load(object sender, EventArgs e)
        {
            con = new Conexao();
            Utils.PopularGrid_SP("Usuario", usuariosGridView, "spObterUsuario");

        }
        private Boolean NomeCadastrado(string iNomeUser)
        {
            Boolean iRetur=true;
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

            //  this_FormClosing();
        }
        private void this_FormClosing()
        {
            this.parentMain.PopularGrid(false, "Usuario", usuariosGridView, "spObterUsuario");
            this.Dispose();
        }

        private void Editar(object sender, EventArgs e)
        {
            if (usuariosGridView.SelectedRows.Count > 0)
            {
                codigo = int.Parse(this.usuariosGridView.SelectedRows[rowIndex].Cells[0].Value.ToString());
                txtNomeUsuario.Text = this.usuariosGridView.SelectedRows[rowIndex].Cells[1].Value.ToString();
                txtSenha.Enabled = false;
                mSenha = usuariosGridView.SelectedRows[rowIndex].Cells[2].Value.ToString();
                chkCancelaPedidos.Checked = Convert.ToBoolean(usuariosGridView.SelectedRows[rowIndex].Cells[3].Value.ToString());
                chkAlteraProdutos.Checked = Convert.ToBoolean(usuariosGridView.SelectedRows[rowIndex].Cells[4].Value.ToString());
                chkAdministrador.Checked = Convert.ToBoolean(usuariosGridView.SelectedRows[rowIndex].Cells[5].Value.ToString());
                chkAcessaRelat.Checked = Convert.ToBoolean(usuariosGridView.SelectedRows[rowIndex].Cells[6].Value.ToString());
                chkFechaPedido.Checked = Convert.ToBoolean(usuariosGridView.SelectedRows[rowIndex].Cells[7].Value.ToString());
                chkDescSN.Checked = Convert.ToBoolean(usuariosGridView.SelectedRows[rowIndex].Cells[8].Value.ToString());
                txtDesconto.Text = usuariosGridView.SelectedRows[rowIndex].Cells[9].Value.ToString();

                chkAlteraPedido.Checked = Convert.ToBoolean(usuariosGridView.SelectedRows[rowIndex].Cells[10].Value.ToString());
                chkAbreCliente.Checked = Convert.ToBoolean(usuariosGridView.SelectedRows[rowIndex].Cells[11].Value.ToString());
                chkAbreCaixa.Checked = Convert.ToBoolean(usuariosGridView.SelectedRows[rowIndex].Cells[12].Value.ToString());
                chkEditaCliente.Checked = Convert.ToBoolean(usuariosGridView.SelectedRows[rowIndex].Cells[13].Value.ToString());


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

        private void CancelarEdicao(object sender, EventArgs e)
        {
            Utils.LimpaForm(this);
            Utils.PopularGrid_SP("Usuario", usuariosGridView, "spObterUsuario");
        }

        private void SalvarEdicao(object sender, EventArgs e)
        {

            if (txtNomeUsuario.Text != "")
            {
                Usuario user = new Usuario()
                {
                    Codigo = codigo,
                    Nome = txtNomeUsuario.Text,
                    Senha = usuariosGridView.SelectedRows[rowIndex].Cells[2].Value.ToString(),
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
            else
            {
                MessageBox.Show("Campos não podem ficar em branco", "Aviso");
            }



        }

        private void chkDescSN_CheckedChanged(object sender, EventArgs e)
        {
            txtDesconto.Enabled = chkDescSN.Checked;
            if (!chkDescSN.Checked)
            {
                txtDesconto.Text = "0";
            }
        }
    }
}
