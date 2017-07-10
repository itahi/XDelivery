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
    public partial class frmCadastroStatus : Form
    {
        Conexao con;
        int rowIndex;
        int codigo;

        public frmCadastroStatus()
        {
            InitializeComponent();
            con = new Conexao();
        }
        
      
        private void btnAdicionarGrupo_Click(object sender, EventArgs e)
        {
            try
            {
                PedidoStatus pedS = new PedidoStatus()
                {
                    Nome = txbNome.Text,
                    AlertarSN = chkAlertar.Checked,
                    Status = int.Parse(cbxOrder.Text),
                    EnviarSN = true,
                   // DataAlteracao = DateTime.Now
                };
                con.Insert("spAdicionarPedidoStatus", pedS);
                ListaStauts();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel salvar o registro " + erro.Message);
            }
            

        }
        private void ListaStauts()
        {
            Utils.LimpaForm(this);
            Utils.PopulaGrid_Novo("PedidoStatus", StatusGridView, " Codigo, Nome,Status,"+
                                                "    case AlertarSN " +
                                                "    when 1 then 'SIM' " +
                                                "    when 0 then 'NAO' " +
                                                "    end               " +
                                                "     as               " +
                                                "     'Alerta Cliente'");
        }

        private void frmCadastroStatus_Load(object sender, EventArgs e)
        {
            ListaStauts();
        }

        private void StatusGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowIndex = e.RowIndex;
            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possivel obter a linha do registro " + erro.Message);
            }
            
        }

        private void btnEditarGrupo_Click(object sender, EventArgs e)
        {
            codigo = int.Parse(StatusGridView.CurrentRow.Cells["Codigo"].Value.ToString());
            txbNome.Text = StatusGridView.CurrentRow.Cells["Nome"].Value.ToString();
            chkAlertar.Checked = StatusGridView.CurrentRow.Cells["Alerta Cliente"].Value.ToString() == "SIM";
            cbxOrder.Text = StatusGridView.CurrentRow.Cells["Status"].Value.ToString();

            this.btnAdicionar.Text = "Salvar [F12]";
            this.btnAdicionar.Click += new System.EventHandler(this.Salvar);
            this.btnAdicionar.Click -= new System.EventHandler(this.btnAdicionarGrupo_Click);

            this.btnEditar.Text = "Cancelar [ESC]";
            this.btnEditar.Click += new System.EventHandler(this.Cancelar);
            this.btnEditar.Click -= new System.EventHandler(this.btnEditarGrupo_Click);

        }
        private void Salvar(object sender, EventArgs e)
        {
            PedidoStatus pedido = new PedidoStatus()
            {
                Codigo = codigo,
                Nome = txbNome.Text,
                AlertarSN = chkAlertar.Checked,
                EnviarSN = true,
                Status = int.Parse(cbxOrder.Text)
            };
           
            if (txbNome.Text != ""|| cbxOrder.Text!="")
            {
                con.Update("spAlterarPedidoStatus", pedido);
                Utils.ControlaEventos("Alterar", this.Name);
                this.btnAdicionar.Text = "Adicionar [F12]";
                this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionarGrupo_Click);
                this.btnAdicionar.Click -= new System.EventHandler(this.Salvar);

                this.btnEditar.Text = "Editar [F11]";
                this.btnEditar.Click += new System.EventHandler(this.btnEditarGrupo_Click);
                this.btnEditar.Click -= new System.EventHandler(this.Cancelar);

                ListaStauts();
            }
            else
            {
                MessageBox.Show("Preencha os campos obrigatórios para continuar", "xSistemas Aviso");
            }


        }
        private void Cancelar(object sender, EventArgs e)
        {

            Button iButton = (Button)sender;
            ListaStauts();
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionarGrupo_Click);
            this.btnAdicionar.Click -= new System.EventHandler(this.Salvar);

            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditarGrupo_Click);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
        }

        private void frmCadastroStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12 && btnAdicionar.Text == "Adicionar [F12]")
            {
                btnAdicionarGrupo_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F12 && btnAdicionar.Text == "Salvar [F12]")
            {
                Salvar(sender, e);
            }
            else if (e.KeyCode == Keys.F11 && btnEditar.Text == "Editar [F11]")
            {
                btnEditarGrupo_Click(sender, e);
            }
        }
    }
}
