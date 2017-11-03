using DexComanda.Models.Produto;
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
    public partial class frmCupom : Form
    {
        private Conexao con;
        private int codigo;
        public frmCupom()
        {
            InitializeComponent();
        }

        private void AdicionarRegistro(object sender, EventArgs e)
        {
            try
            {
                if (txtCodCupom.Text=="")
                {
                    MessageBox.Show("Informe o código do cupom");
                    txtCodCupom.Focus();
                    return;
                }
                if (dtFim.Value<=dtInicio.Value)
                {
                    MessageBox.Show("Data final deve ser maior que data inicio");
                    dtFim.Focus();
                    return;
                }
                if (txtDesc.Text=="")
                {
                    MessageBox.Show("Informe o Desconto do cupom");
                    txtDesc.Focus();
                    return;
                }
                if (txtQtdPessoa.Text == "")
                {
                    MessageBox.Show("Informe a quantidade máxima de uso do cupom");
                    txtQtdPessoa.Focus();
                    return;
                }
                if (txtQdtCupom.Text == "")
                {
                    MessageBox.Show("Informe a Quantidade  do cupom");
                    txtQdtCupom.Focus();
                    return;
                }
                Cupom _newCupom = new Cupom()
                {
                    AtivoSN = chkAtivo.Checked,
                    CodCupom = txtCodCupom.Text,
                    DataValidade_Inicio = dtInicio.Value,
                    DataValidade_Fim = dtFim.Value,
                    Desconto = decimal.Parse(txtDesc.Text),
                    Quantidade = int.Parse(txtQdtCupom.Text),
                    QuantidadePessoa = 1
                };
                con.Insert("spAdicionarCupom", _newCupom);
                ListaRegistros();
                Utils.LimpaForm(this);
            }
            catch (Exception erro)
            {
                MessageBox.Show("AdicionarRegistro " + erro.Message);
            }
        }
        private void ListaRegistros()
        {
            try
            {
                Utils.PopularGrid_SP("Cupom", CuponGridView, "spObterCupom");
                if (CuponGridView.Rows.Count==0)
                {
                    return;
                }
                Utils.PopularGrid_SP("Pedido_Cupom", gridViewCupons, "spObterHistoricoCupomPorCodigo", int.Parse(CuponGridView.Rows[0].Cells["Codigo"].Value.ToString()));
            }
            catch (Exception erro)
            {
                MessageBox.Show("ListaRegistros " + erro.Message);
            }
        }

        private void frmCupom_Load(object sender, EventArgs e)
        {
            con= new Conexao();
            ListaRegistros();
        }

        private void txtDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }

        private void txtQdtCupom_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void txtQtdPessoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void EditarRegistro(object sender, EventArgs e)
        {
            if (CuponGridView.SelectedRows.Count==0)
            {
                MessageBox.Show("Selecione a linha que deseja ajudar");
                return;
            }
            codigo = int.Parse(CuponGridView.CurrentRow.Cells["Codigo"].Value.ToString());
            DataSet dsCupom = con.SelectRegistroPorCodigo("Cupom", "spObterCupomPorCodigo", codigo);
            if (dsCupom.Tables[0].Rows.Count<=0)
            {
                return;
            }
            Utils.PopularGrid_SP("Pedido_Cupom", gridViewCupons, "spObterHistoricoCupomPorCodigo", codigo);
            txtCodCupom.Text = dsCupom.Tables[0].Rows[0].Field<string>("CodCupom");
            txtDesc.Text = dsCupom.Tables[0].Rows[0].Field<decimal>("Desconto").ToString(); 
            txtQdtCupom.Text = dsCupom.Tables[0].Rows[0].Field<int>("Quantidade").ToString();
            txtQtdPessoa.Text = dsCupom.Tables[0].Rows[0].Field<int>("QuantidadePessoa").ToString(); 
            chkAtivo.Checked = dsCupom.Tables[0].Rows[0].Field<Boolean>("AtivoSN");
            dtInicio.Value = dsCupom.Tables[0].Rows[0].Field<DateTime>("DataValidade_Inicio"); 
            dtFim.Value = dsCupom.Tables[0].Rows[0].Field<DateTime>("DataValidade_Fim"); 

            this.btnAdicionar.Text = "Salvar [F12]";
            this.btnAdicionar.Click += new System.EventHandler(this.Salvar);
            this.btnAdicionar.Click -= new System.EventHandler(this.AdicionarRegistro);

            this.btnEditar.Text = "Cancelar [ESC]";
            this.btnEditar.Click += new System.EventHandler(this.Cancelar);
            this.btnEditar.Click -= new System.EventHandler(this.EditarRegistro);
        }
        private void Cancelar(object sender, EventArgs e)
        {

            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.Click += new System.EventHandler(this.AdicionarRegistro);
            this.btnAdicionar.Click -= new System.EventHandler(this.Salvar);

            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
        }
        private void Salvar(object sender, EventArgs e)
        {
            try
            {
                if (txtCodCupom.Text == "")
                {
                    MessageBox.Show("Informe o código do cupom");
                    txtCodCupom.Focus();
                    return;
                }
                if (dtFim.Value <= dtInicio.Value)
                {
                    MessageBox.Show("Data final deve ser maior que data inicio");
                    dtFim.Focus();
                    return;
                }
                if (txtDesc.Text == "")
                {
                    MessageBox.Show("Informe o Desconto do cupom");
                    txtDesc.Focus();
                    return;
                }
                if (txtQtdPessoa.Text == "")
                {
                    MessageBox.Show("Informe a quantidade máxima de uso do cupom");
                    txtQtdPessoa.Focus();
                    return;
                }
                if (txtQdtCupom.Text == "")
                {
                    MessageBox.Show("Informe a Quantidade  do cupom");
                    txtQdtCupom.Focus();
                    return;
                }
                Cupom _newCupom = new Cupom()
                {
                    Codigo = codigo,
                    AtivoSN = chkAtivo.Checked,
                    CodCupom = txtCodCupom.Text,
                    DataValidade_Inicio = dtInicio.Value,
                    DataValidade_Fim = dtFim.Value,
                    Desconto = decimal.Parse(txtDesc.Text),
                    Quantidade = int.Parse(txtQdtCupom.Text),
                    QuantidadePessoa = 1
                };
                con.Update("spAlterarCupom", _newCupom);
                ListaRegistros();
                Utils.LimpaForm(this);

                this.btnAdicionar.Text = "Adicionar [F12]";
                this.btnAdicionar.Click += new System.EventHandler(this.AdicionarRegistro);
                this.btnAdicionar.Click -= new System.EventHandler(this.Salvar);

                this.btnEditar.Text = "Editar [F11]";
                this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
                this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
                ListaRegistros();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Salvar " + erro.Message);
            }
        }

        private void CuponGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EditarRegistro(sender, e);
        }
    }
}
