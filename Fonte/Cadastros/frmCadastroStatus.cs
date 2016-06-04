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
            codigo = int.Parse(StatusGridView.Rows[rowIndex].Cells["Codigo"].Value.ToString());
            txbNome.Text = StatusGridView.Rows[rowIndex].Cells["Nome"].Value.ToString();
            chkAlertar.Checked = StatusGridView.Rows[rowIndex].Cells["Alerta Cliente"].Value.ToString() == "SIM";
            cbxOrder.Text = StatusGridView.Rows[rowIndex].Cells["Status"].Value.ToString();

            this.btnAdicionarGrupo.Text = "Salvar [F12]";
            this.btnAdicionarGrupo.Click += new System.EventHandler(this.Salvar);
            this.btnAdicionarGrupo.Click -= new System.EventHandler(this.btnAdicionarGrupo_Click);

            this.btnEditarGrupo.Text = "Cancelar [ESC]";
            this.btnEditarGrupo.Click += new System.EventHandler(this.Cancelar);
            this.btnEditarGrupo.Click -= new System.EventHandler(this.btnEditarGrupo_Click);

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
                this.btnAdicionarGrupo.Text = "Adicionar [F12]";
                this.btnAdicionarGrupo.Click += new System.EventHandler(this.btnAdicionarGrupo_Click);
                this.btnAdicionarGrupo.Click -= new System.EventHandler(this.Salvar);

                this.btnEditarGrupo.Text = "Editar [F11]";
                this.btnEditarGrupo.Click += new System.EventHandler(this.btnEditarGrupo_Click);
                this.btnEditarGrupo.Click -= new System.EventHandler(this.Cancelar);

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
            this.btnAdicionarGrupo.Text = "Adicionar";
            this.btnAdicionarGrupo.Click += new System.EventHandler(this.btnAdicionarGrupo_Click);
            this.btnAdicionarGrupo.Click -= new System.EventHandler(this.Salvar);

            this.btnEditarGrupo.Text = "Editar";
            this.btnEditarGrupo.Click += new System.EventHandler(this.btnEditarGrupo_Click);
            this.btnEditarGrupo.Click -= new System.EventHandler(this.Cancelar);
        }

    }
}
