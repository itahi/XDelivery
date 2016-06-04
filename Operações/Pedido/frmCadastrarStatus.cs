using DexComanda.Models.Operacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações.Pedido
{
    public partial class frmCadastrarStatus : Form
    {
        private Conexao con;
        private int codigoAlterar, rowIndex;
        public frmCadastrarStatus()
        {
            InitializeComponent();
            con = new Conexao();
            Utils.PopularGrid("PedidoStatus", GridView);
        }

        private void Adicionar(object sender, EventArgs e)
        {
            try
            {
                PedidoStatus ped = new PedidoStatus()
                {
                    Nome = txtNome.Text,
                    Status = int.Parse(cbxStatus.Text.Substring(0, 1).Trim()),
                    //  DataAlteracao = DateTime.Now,
                    AlertarSN = true,
                    EnviarSN = chkOnline.Checked
                };
                con.Insert("spAdicionarPedidoStatus", ped);
            }
            catch (Exception erro)
            {

                throw;
            }
            Utils.PopularGrid("PedidoStatus", GridView);
            Utils.LimpaForm(this);
        }

        private void Editar(object sender, EventArgs e)
        {
            try
            {
                codigoAlterar = int.Parse(GridView.Rows[rowIndex].Cells["Codigo"].Value.ToString());
                DataSet ds = con.SelectRegistroPorCodigo("PedidoStatus", "spObterPedidoStatusPorCodigo", codigoAlterar);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtNome.Text = ds.Tables[0].Rows[0].Field<string>("Nome");
                    chkOnline.Checked = ds.Tables[0].Rows[0].Field<Boolean>("EnviarSN");
                    cbxStatus.Text = RetornaStatus(ds.Tables[0].Rows[0].Field<int>("Status"));

                    this.btnAdicionar.Text = "Salvar [F12]";
                    this.btnAdicionar.Click += new System.EventHandler(this.Salvar);
                    this.btnAdicionar.Click -= new System.EventHandler(this.Adicionar);

                    this.btnEditar.Text = "Cancelar [ESC]";
                    this.btnEditar.Click += new System.EventHandler(this.Cancelar);
                    this.btnEditar.Click -= new System.EventHandler(this.Editar);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            
        }
        private void Salvar(object sender, EventArgs e)
        {
            PedidoStatus ped = new PedidoStatus()
            {
                Codigo = codigoAlterar,
                Nome = this.txtNome.Text,
                AlertarSN = true,
                EnviarSN = chkOnline.Checked,
                Status = int.Parse(cbxStatus.Text.Substring(0,1).Trim())
                
            };
           
            if (txtNome.Text != "" && cbxStatus.Text!="")
            {
                con.Update("spAlterarPedidoStatus", ped);
                Utils.ControlaEventos("Alterar", this.Name);
                this.btnAdicionar.Text = "Adicionar [F12]";
                this.btnAdicionar.Click += new System.EventHandler(this.Adicionar);
                this.btnAdicionar.Click -= new System.EventHandler(this.Salvar);

                this.btnEditar.Text = "Editar [F11]";
                this.btnEditar.Click += new System.EventHandler(this.Editar);
                this.btnEditar.Click -= new System.EventHandler(this.Cancelar);

                //ExibirGrupos();
            }
            else
            {
                MessageBox.Show("Preencha o nome para continuar", "Dex Aviso");
            }


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
            this.btnAdicionar.Click -= new System.EventHandler(this.Salvar);

            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.Editar);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
        }
        private string RetornaStatus(int iCodigo)
        {
            string iReturn = "";
            if (iCodigo==1)
            {
                iReturn = "1 - Aberto";
            }
            else if (iCodigo==2)
            {
                iReturn = "2 - Impresso";
            }
            else if (iCodigo==3)
            {
                iReturn = "3 - Na Cozinha";
            }
            else if (iCodigo == 4)
            {
                iReturn = "4 - Saiu pra entrega";
            }
            else if (iCodigo == 5)
            {
                iReturn = "5 - Entregue";
            }
            return iReturn;
        }

        private void GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < GridView.Rows.Count; i++)
            {
                if (this.GridView.Rows[i].Selected)
                {
                    rowIndex = this.GridView.Rows[i].Index;
                }
            }
        }
    }
}
