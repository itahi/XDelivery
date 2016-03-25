using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DexComanda.Models;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Cadastros
{
    public partial class frmAdicionarMesa : Form
    {

        Conexao con;
        int rowIndex;
        int codigo;
        int lStatusMesa;
        public frmAdicionarMesa()
        {
            InitializeComponent();
            con = new Conexao();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
           
        }

        private void ListaMesas()
        {
            this.MesasGridView.DataSource = null;
            this.MesasGridView.AutoGenerateColumns = true;
            this.MesasGridView.DataSource = con.SelectAll("Mesas", "spObterMesas");
            this.MesasGridView.DataMember = "Mesas";

        }

        private void frmAdicionarMesa_Load(object sender, EventArgs e)
        {
            ListaMesas();
        }

        private void EditarRegistro(object sender, EventArgs e)
        {
            lStatusMesa = 0;
            cbxStatusMesa.Text = "";
            codigo = int.Parse(this.MesasGridView.SelectedRows[rowIndex].Cells[0].Value.ToString());
            this.txtNumeroMesa.Text = this.MesasGridView.SelectedRows[rowIndex].Cells[1].Value.ToString();
            lStatusMesa = int.Parse(this.MesasGridView.SelectedRows[rowIndex].Cells[3].Value.ToString());
            cbxStatusMesa.SelectedText = "";
            if (lStatusMesa == 1)
            {
                cbxStatusMesa.SelectedText = "Aberta";
            }
            else if (lStatusMesa == 2)
            {
                cbxStatusMesa.SelectedItem = "Ocupada";
            }
            else if (lStatusMesa == 3)
            {
                cbxStatusMesa.SelectedItem = "Reservada";
            }
            else
            {
                cbxStatusMesa.SelectedItem = "Aberta";
            }

            this.btnSalvar.Text = "Salvar [F12]";
            this.btnSalvar.Click += new System.EventHandler(this.SalvarMesa);
            this.btnSalvar.Click -= new System.EventHandler(this.AdicionarMesa);

            this.btnEditar.Text = "Cancelar [ESC]";
            this.btnEditar.Click += new System.EventHandler(this.Cancelar);
            this.btnEditar.Click -= new System.EventHandler(this.EditarRegistro);
        }
        private void Cancelar(object sender, EventArgs e)
        {
            Utils.LimpaForm(this);
        }
        private void SalvarMesa(object sender, EventArgs e)
        {
                Mesas mesa = new Mesas()
                {
                    Codigo = codigo,
                    NumeroMesa = this.txtNumeroMesa.Text
                    
                };
                if (cbxStatusMesa.Text == "Aberta")
                {
                    mesa.StatusMesa = 1;
                    
                }
                else if (cbxStatusMesa.Text == "Ocupada")
                {
                    mesa.StatusMesa = 2;
                }
                else if (cbxStatusMesa.Text == "Reservada")
                {
                    mesa.StatusMesa = 3;
                }
                else
                {
                    mesa.StatusMesa = 1;
                }

             
                if (txtNumeroMesa.Text != "")
                {
                    con.Update("spAlteraMesas", mesa);
                    Utils.ControlaEventos("Alterar", this.Name);
                    this.btnSalvar.Text = "Adicionar [F12]";
                    this.btnSalvar.Click += new System.EventHandler(this.AdicionarMesa);
                    this.btnSalvar.Click -= new System.EventHandler(this.SalvarMesa);

                    this.btnEditar.Text = "Editar [F11]";
                    this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
                    this.btnEditar.Click -= new System.EventHandler(this.Cancelar);

                    Utils.LimpaForm(this);
                    ListaMesas();
                }
                else
                {
                    MessageBox.Show("Preencha o nome para continuar", "Dex Aviso");
                }


        }

        private void AdicionarMesa(object sender, EventArgs e)
        {
            try
            {
                if (!MesaExiste(txtNumeroMesa.Text,MesasGridView))
                {
                    Mesas mesas = new Mesas()
                    {
                        NumeroMesa = txtNumeroMesa.Text
                    };

                    if (cbxStatusMesa.Text == "Aberta")
                    {
                        mesas.StatusMesa = 1;
                    }
                    else if (cbxStatusMesa.Text == "Ocupada")
                    {
                        mesas.StatusMesa = 2;
                    }
                    else if (cbxStatusMesa.Text == "Reservada")
                    {
                        mesas.StatusMesa = 3;
                    }

                    if (txtNumeroMesa.Text != "" && cbxStatusMesa.Text != "")
                    {
                        con.Insert("spAdicionarMesas", mesas);
                        Utils.ControlaEventos("Inserir", this.Name);
                        txtNumeroMesa.Text = "";
                        txtNumeroMesa.Focus();
                        ListaMesas();
                    }
                    else
                    {
                        MessageBox.Show("Campos não pode ficar vazios", "Dex Aviso");
                    }
 
                }
                
               
            }
            catch (Exception er)
            {

                MessageBox.Show("Erro ao cadastrar mesa " + er.Message);
            }
        }
        private bool MesaExiste(string iNumeroMesa , DataGridView iGrid)
        {
            bool iRetorno = false;
            for (int i = 0; i < iGrid.Rows.Count; i++)
            {
                if (iGrid.Rows[i].Cells["NumeroMesa"].Value.ToString()== iNumeroMesa)
                {
                    iRetorno = true;
                    MessageBox.Show("Mesa já cadastrada","Dex Aviso");
                    break;
                }
            }
            return iRetorno;
        }

    }
}
