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
    public partial class frmBairrosRegiao : Form
    {
        private Conexao con;
        private int rowIndex;
        private int intCodregiao;
        private DataSet dsBairros;
        private DataRow dRow;

        public frmBairrosRegiao()
        {
            InitializeComponent();
            con = new Conexao();
        }

        private void frmBairrosRegiao_Load(object sender, EventArgs e)
        {
            //ListaRegiao();
            this.cbxRegiao.DataSource = con.SelectAll("RegiaoEntrega", "spObterRegioes").Tables["RegiaoEntrega"];
            this.cbxRegiao.DisplayMember = "NomeRegiao";
            this.cbxRegiao.ValueMember = "Codigo";
            if (cbxRegiao.SelectedValue.ToString()!="")
            {
                ListaBairrosPorRegiao(int.Parse(cbxRegiao.SelectedValue.ToString()));
            }
            

        }
        
        private void Adicionar(object sender, EventArgs e)
        {
           
            if (cbxRegiao.SelectedIndex != -1 && txtCEP.Text != "")
            {
                try
                {
                    RegiaoEntrega_Bairros reg = new RegiaoEntrega_Bairros()
                    {
                        CodRegiao = int.Parse(cbxRegiao.SelectedValue.ToString()),
                        CEP = txtCEP.Text.Replace("-",""),
                        Nome = txtBairro.Text,
                        DataCadastro = DateTime.Now,
                        AtivoSN = chkAtivo.Checked,
                        OnlineSN = chkOnlineSN.Checked
                    };
                    con.Insert("spAdicionaBairrosRegiao", reg);
                    con.AtualizaDataSincronismo("RegiaoEntrega", reg.CodRegiao, "DataAlteracao");
                    Utils.ControlaEventos("Inserir", this.Name);
                  ///  Utils.LimpaForm(this);
                    ListaBairrosPorRegiao(int.Parse(cbxRegiao.SelectedValue.ToString()));
                    txtBairro.Text = "";
                    txtCEP.Text = "";
                    txtCEP.Focus();
                }
                catch (Exception erro)
                {
                    if (erro.Message.Contains("Violação da restrição UNIQUE KEY"))
                    {
                        MessageBox.Show("Esse cep já está vinculado a outra região", "[xSistemas]");
                    }
                    else
                    {
                        MessageBox.Show(erro.Message);
                    }
                }


            }
            else
            {
                MessageBox.Show("Campos obrigatórios não preenchidos", "[xSistemas]");
            }
        }
        private void ListaRegiao()
        {
            Utils.PopularGrid("RegiaoEntrega_Bairros", RegioesGridView);
            Utils.LimpaForm(this);
        }

        private void txtCEP_KeyDown(object sender, KeyEventArgs e)
        {
         

        }


        private void Editar(object sender, EventArgs e)
        {
            try
            {
                if (RegioesGridView.SelectedRows.Count > 0)
                {
                    DataSet dsRegiao = con.SelectRegistroPorCodigo("RegiaoEntrega", "spObterRegioesPorCodigo", int.Parse(this.RegioesGridView.CurrentRow.Cells["CodRegiao"].Value.ToString()));
                    if (dsRegiao.Tables[0].Rows.Count > 0)
                    {
                        txtCEP.Enabled = false;
                        txtBairro.Enabled = false;

                        DataRow dRow = dsRegiao.Tables[0].Rows[0];
                        cbxRegiao.SelectedValue = int.Parse(dRow.ItemArray.GetValue(0).ToString());
                        cbxRegiao.SelectedText = dRow.ItemArray.GetValue(2).ToString();
                        txtBairro.Text = RegioesGridView.CurrentRow.Cells["Nome"].Value.ToString();
                        txtCEP.Text = RegioesGridView.CurrentRow.Cells["CEP"].Value.ToString();
                        chkAtivo.Checked = bool.Parse(RegioesGridView.CurrentRow.Cells["AtivoSN"].Value.ToString());
                        chkOnlineSN.Checked = bool.Parse(RegioesGridView.CurrentRow.Cells["OnlineSN"].Value.ToString());
                        this.btnAdicionar.Text = "Salvar";
                        this.btnAdicionar.Click += new System.EventHandler(this.Salvar);
                        this.btnAdicionar.Click -= new System.EventHandler(this.Adicionar);

                        this.btnEditar.Text = "Cancelar";
                        this.btnEditar.Click += new System.EventHandler(this.Cancelar);
                        this.btnEditar.Click -= new System.EventHandler(this.Editar);
                    }

                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
           
        }
        private void Cancelar(object sender, EventArgs e)
        {

            Button iButton = (Button)sender;

            Utils.LimpaForm(this);
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.Click += new System.EventHandler(this.Adicionar);
            this.btnAdicionar.Click -= new System.EventHandler(this.Salvar);

            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.Editar);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
            txtCEP.Enabled = true;
            txtBairro.Enabled = true;
        }

        private void Salvar(object sender, EventArgs e)
        {
            string iCodSelecionado = cbxRegiao.SelectedValue.ToString();
            try
            {
                if (cbxRegiao.Text=="" || txtCEP.Text=="" || txtBairro.Text=="")
                {
                    MessageBox.Show("Campos obrigatórios não preenchidos");
                    return;
                }
              
                RegiaoEntrega_Bairros reg = new RegiaoEntrega_Bairros()
                {
                    CEP = txtCEP.Text,
                    CodRegiao = int.Parse(cbxRegiao.SelectedValue.ToString()),
                    DataCadastro = DateTime.Now,
                    Nome = txtBairro.Text,
                    AtivoSN = chkAtivo.Checked,
                    OnlineSN = chkOnlineSN.Checked
                };
                if (con.SelectCEPRegiao(txtBairro.Text).Tables[0].Rows.Count > 0 && iCodSelecionado != reg.CodRegiao.ToString())
                {
                    MessageBox.Show("Esse cep já está vinculado a outra região");
                    return;
                }

                con.Update("spAlterarBairrosRegiao", reg);
                con.AtualizaDataSincronismo("RegiaoEntrega_Bairros", reg.CodRegiao, "DataCadastro","CodRegiao");
                Utils.ControlaEventos("Alterar", this.Name);
                txtBairro.Text = "";
                txtCEP.Text = "";
                txtCEP.Enabled = true;
                txtBairro.Enabled = true;
                ListaBairrosPorRegiao(reg.CodRegiao);
            }
            catch (Exception erro)
            {

                if (erro.Message.Contains("Violação da restrição UNIQUE KEY"))
                {
                    MessageBox.Show("Esse cep já está vinculado a outra região", "[xSistemas]");
                }
            }

        }

        private void RegioesGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowIndex = e.RowIndex;
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
                    
           
        }

        private void ListaBairrosPorRegiao(int iCodRegiao)
        {
            DataSet dsBairros = con.SelectRegistroPorCodigo("RegiaoEntrega_Bairros", "spObterRegiaoEntrega_BairrosPorCodigo", int.Parse(cbxRegiao.SelectedValue.ToString()));
            if (dsBairros.Tables[0].Rows.Count > 0)
            {
                RegioesGridView.DataSource = null;
                RegioesGridView.AutoGenerateColumns = true;
                RegioesGridView.DataSource = dsBairros;
                RegioesGridView.DataMember = "RegiaoEntrega_Bairros";
            }
            else
            {
                RegioesGridView.DataSource = null;
            }
        }
        private void ListaBairrosRegiao(object sender, EventArgs e)
        {
            try
            {
                ListaBairrosPorRegiao(int.Parse(cbxRegiao.SelectedValue.ToString()));
            }
            catch (Exception erro)
            {
               MessageBox.Show("Não foi possivel listar os bairros dessa região");   
            }            
        
            
        }

        private void MenuAuxiliar(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem ExcluirProduto = new MenuItem("Excluir Bairro");
                ExcluirProduto.Click += DeletarRegistro;
                m.MenuItems.Add(ExcluirProduto);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }
        private void DeletarRegistro(object sender, EventArgs e)
        {
            if (RegioesGridView.SelectedRows.Count > 0)
            {
                int CoRegiao = int.Parse(this.RegioesGridView.SelectedCells[0].Value.ToString());
                string Cep = RegioesGridView.SelectedCells[2].Value.ToString();
                con.DeleteBairroRegiao("RegiaoEntrega_Bairros", "spExcluirBairroRegiao", CoRegiao, Cep);
                Utils.ControlaEventos("Excluir", this.Name);
                con.AtualizaDataSincronismo("RegiaoEntrega", CoRegiao, "DataAlteracao");
                MessageBox.Show("Item excluído com sucesso.");
                ListaBairrosPorRegiao(CoRegiao);
            }
            else
            {
                MessageBox.Show("Selecione o grupo para excluir");
            }

        }

        private void RegioesGridView_DoubleClick(object sender, EventArgs e)
        {
            Editar(sender, e);
        }

        private void txtCEP_TextChanged(object sender, EventArgs e)
        {

            if (txtCEP.Text.Length >= 8)
            {
                DataSet dsCEp = con.RetornaCEPporBairro(txtCEP.Text.Replace("-", ""), false);
                if (dsCEp.Tables[0].Rows.Count > 0)
                {
                    txtBairro.Text = con.RetornaCEPporBairro(txtCEP.Text.Replace("-", ""), false).Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    btnAdicionar.Focus();
                }
                else
                {
                    MessageBox.Show("Registro não encontrado", "[xSistemas]");
                }

            }
        }
    }
}
