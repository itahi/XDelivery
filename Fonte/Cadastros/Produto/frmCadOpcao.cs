﻿using DexComanda.Models;
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

namespace DexComanda.Cadastros.Produto
{
    public partial class frmCadOpcao : Form
    {
        private Conexao con;
        private int codigoAlterarDeletar;
        private List<Models.Produto.OpcaoDia> listDias;
        private int rowIndex;
        public frmCadOpcao()
        {
            con = new Conexao();
            InitializeComponent();
        }
        private void MontaListPrecos(string ivalor)
        {
            try
            {
                List<OpcaoDia> pr = new List<OpcaoDia>();
                if (ivalor == "")
                {
                    return;
                }
                pr = Utils.DeserializaObjeto3(ivalor);
                if (pr.Count > 0)
                {
                    foreach (var item in pr)
                    {
                        foreach (System.Windows.Forms.Control obj in grpDiasDisponivel.Controls)
                        {
                            if (object.ReferenceEquals(obj.GetType(), typeof(System.Windows.Forms.CheckBox)))
                            {
                                if (item.Dia == ((System.Windows.Forms.CheckBox)obj).Tag.ToString())
                                {
                                    ((System.Windows.Forms.CheckBox)obj).Checked = true;
                                }
                            }

                        }
                    }
                }

            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }
        private List<Models.Produto.OpcaoDia> RetornaDiasMarcados()
        {
            listDias = new List<Models.Produto.OpcaoDia>();
            // var precosDia = new PrecoDiaProduto();
            foreach (System.Windows.Forms.Control TEXT in grpDiasDisponivel.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(TEXT.GetType(), typeof(System.Windows.Forms.CheckBox)))
                {
                    //Check to see if it's a textbox 
                    if (((System.Windows.Forms.CheckBox)TEXT).Checked)
                    {
                        var precosDia = new Models.Produto.OpcaoDia()
                        {
                            Dia = (((System.Windows.Forms.CheckBox)TEXT).Tag.ToString()),
                        };

                        listDias.Add(precosDia);
                    }

                    //If it is then set the text to String.Empty (empty textbox) 
                }

            }

            return listDias;
        }
        private void CadastraOpcao(object sender, EventArgs e)
        {
            try
            {
                if (txtNome.Text.Trim() != "" && cbxTipo.Text.Trim() != "")
                {
                    Opcao opcao = new Opcao()
                    {
                        DataAlteracao = DateTime.Now,
                        Nome = txtNome.Text,
                        Tipo = cbxTipo.SelectedValue.ToString(),
                        OnlineSN = chkOnlineSN.Checked,
                        AtivoSN = chkAtivoSN.Checked,
                        SinalOpcao = "",
                        DiasDisponivel = Utils.SerializaObjeto(RetornaDiasMarcados())

                    };
                    con.Insert("spAdicionarOpcao", opcao);
                    Utils.LimpaForm(this);
                    LimpaDias();
                    ListaOpcao();
                }
                else
                {
                    MessageBox.Show("Campos obrigatórios não preenchidos", "[xSistemas]");
                    return;
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message, "xSistemas");
            }
        }

        private void ListaOpcao(int iTipoOpcao=0)
        {
            if (iTipoOpcao==0)
            {
                Utils.PopularGrid("Opcao", OpcaoGridView); 
            }
            else
            {
                Utils.PopularGrid_SP("Opcao", OpcaoGridView, "spObterOpcaoPorTipo", iTipoOpcao);
            }
            
        }

        private void frmCadOpcao_Load(object sender, EventArgs e)
        {
           Utils.MontaCombox(cbxTipo, "Nome", "Codigo", "Produto_OpcaoTipo", "spObterTipoOpcao");

            ListaOpcao();
        }
        private void DeletaRegistro(object sender, EventArgs e)
        {
            try
            {
                if (OpcaoGridView.SelectedRows.Count > 0)
                {
                    codigoAlterarDeletar = int.Parse(this.OpcaoGridView.SelectedCells[0].Value.ToString());
                    con.DeleteAll("Opcao", "spExcluirOpcao", codigoAlterarDeletar);
                    Utils.ControlaEventos("Excluir", this.Name);
                    MessageBox.Show("Item excluído com sucesso.");
                    ListaOpcao();

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
                    MessageBox.Show("Não foi possivel deletar o registro pois o mesmo está sendo usando em algum produto", "xSistemas ");
                }
            }
            

        }

        private void RetonaTipo(int iTipo)
        {
            Utils.MontaCombox(cbxTipo, "Nome", "Codigo", "Produto_OpcaoTipo", "spObterProduto_OpcaoTipoPorCodigo", iTipo);
        }
        private void LimpaDias()
        {
            foreach (System.Windows.Forms.Control ctrControl in grpDiasDisponivel.Controls)
            {
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.CheckBox)))
                {
                    //Unselect all RadioButtons
                    ((System.Windows.Forms.CheckBox)ctrControl).Checked = false;
                }
            }
        }
        private void EditarOpcao(object sender, EventArgs e)
        {
            try
            {
               
                codigoAlterarDeletar = int.Parse(this.OpcaoGridView.Rows[rowIndex].Cells[0].Value.ToString());

                //DataSet ds = con.SelectRegistroPorCodigo("Opcao", "spObterOpcaoPorCodigo", codigoAlterarDeletar);

                RetonaTipo(int.Parse(OpcaoGridView.Rows[rowIndex].Cells["Tipo"].Value.ToString()));
                txtNome.Text = this.OpcaoGridView.Rows[rowIndex].Cells["Nome"].Value.ToString();
                chkAtivoSN.Checked = bool.Parse(OpcaoGridView.Rows[rowIndex].Cells[3].Value.ToString());
                chkOnlineSN.Checked = bool.Parse(OpcaoGridView.Rows[rowIndex].Cells[4].Value.ToString());
                //txtSinalOpcao.Text = this.OpcaoGridView.Rows[rowIndex].Cells[5].Value.ToString();
                MontaListPrecos(this.OpcaoGridView.Rows[rowIndex].Cells["DiasDisponivel"].Value.ToString());
                this.btnAdicionar.Text = "Salvar [F12]";
                this.btnAdicionar.Click += new System.EventHandler(this.Salvar);
                this.btnAdicionar.Click -= new System.EventHandler(this.CadastraOpcao);

                this.btnEditar.Text = "Cancelar [ESC]";
                this.btnEditar.Click += new System.EventHandler(this.Cancelar);
                this.btnEditar.Click -= new System.EventHandler(this.EditarOpcao);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
           
        }
        private void Cancelar(object sender, EventArgs e)
        {
            Button iButton = (Button)sender;

            if (iButton.Name == "btnEditar")
            {
                Utils.LimpaForm(this);
            }
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.Click += new System.EventHandler(this.CadastraOpcao);
         
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.EditarOpcao);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
        }
        private void Salvar(object sender, EventArgs e)
        {
            try
            {
                Opcao opcao = new Opcao()
                {
                    Codigo = codigoAlterarDeletar,
                    Nome = txtNome.Text,
                    Tipo = cbxTipo.SelectedValue.ToString(),
                    DataAlteracao = DateTime.Now,
                    OnlineSN = chkOnlineSN.Checked,
                    AtivoSN = chkAtivoSN.Checked,
                    SinalOpcao = "",
                    DiasDisponivel = Utils.SerializaObjeto(RetornaDiasMarcados())
                };
                con.Update("spAlteraOpcao", opcao);
                // Utils.LimpaForm(this);
                LimpaDias();
                con.AtualizaProdutosOpcao(codigoAlterarDeletar);
                this.btnAdicionar.Text = "Adicionar";
                this.btnAdicionar.Click += new System.EventHandler(this.CadastraOpcao);
                this.btnAdicionar.Click -= new System.EventHandler(this.Salvar);

                this.btnEditar.Text = "Editar";
                this.btnEditar.Click += new System.EventHandler(this.EditarOpcao);
                this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
              
                ListaOpcao(int.Parse(opcao.Tipo));

            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message, "xSistemas");
            }
        }

        private void OpcaoGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowIndex = e.RowIndex;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void OpcaoGridView_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem Excluir = new MenuItem("Excluir Opcao");
                Excluir.Click += DeletaRegistro;
                m.MenuItems.Add(Excluir);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }

        private void cbxTipo_DropDown(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxTipo, "Nome", "Codigo", "Produto_OpcaoTipo", "spObterTipoOpcao");
        }

        private void cbxTipo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbxTipo.SelectedIndex != null)
            {
                ListaOpcao(int.Parse(cbxTipo.SelectedValue.ToString()));
            }
        }

        //private void FiltraOpcaoPorTipo(object sender, EventArgs e)
        //{
        //    if (cbxTipo.SelectedIndex != null)
        //    {
        //        ListaOpcao(int.Parse(cbxTipo.SelectedValue.ToString()));
        //    }
        //}
    }
}
