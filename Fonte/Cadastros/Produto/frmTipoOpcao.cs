﻿using DexComanda.Models;
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
    public partial class frmTipoOpcao : Form
    {
        private Conexao con;
        private int rowIndex;
        public frmTipoOpcao()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void frmTipoOpcao_Load(object sender, EventArgs e)
        {
            ListaTipoOpcao();
            grpMaxMin.Enabled = rbMultipla.Checked;
        }

        private void rbMultipla_CheckedChanged(object sender, EventArgs e)
        {
            grpMaxMin.Enabled = rbMultipla.Checked;
        }
        private int RetornaOpcao()
        {
            int iReturn = 0;
            foreach (System.Windows.Forms.Control ctrControl in grpTipo.Controls)
            {
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    if (((System.Windows.Forms.RadioButton)ctrControl).Checked)
                    {
                        iReturn = int.Parse(((System.Windows.Forms.RadioButton)ctrControl).Tag.ToString());
                    }
                    
                }
            }
            return iReturn;
        }
        private void ListaTipoOpcao()
        {
            TipoOpcaoGrid.DataSource = con.RetornaTipoOpcao();
            TipoOpcaoGrid.DataMember = "Produto_OpcaoTipo";
            TipoOpcaoGrid.AutoGenerateColumns = true;

            for (int i = 0; i < TipoOpcaoGrid.Columns.Count; i++)
            {
                if (TipoOpcaoGrid.Columns[i].HeaderText != "Nome" && TipoOpcaoGrid.Columns[i].HeaderText != "Codigo")
                {
                    TipoOpcaoGrid.Columns[i].Visible = false;
                }
            }
            TipoOpcaoGrid.Refresh();
        }
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!rbMultipla.Checked && !rbTexto.Checked && !rbUnica.Checked)
                {
                    MessageBox.Show("Marque uma opção para continuar");
                    grpMaxMin.Focus();
                    return;
                }
                else
                {
                    ProdutoOpcao_Tipo prodOp = new ProdutoOpcao_Tipo()
                    {
                        Nome = txtNome.Text.Trim(),
                        Tipo = RetornaOpcao(),
                        MaximoOpcionais = 0,
                        MinimoOpcionais = 0,
                        OrdenExibicao = int.Parse(cbxOrdem.Text),
                        DataAlteracao = DateTime.Now,
                    };
                    if (grpTipo.Enabled && rbMultipla.Checked)
                    {
                        if (txtMax.Text=="" && txtMinimo.Text=="")
                        {
                            MessageBox.Show("Campos obrigatórios não preenchidos");
                            txtMax.Focus();
                            return;
                        }
                        else
                        {
                            prodOp.MaximoOpcionais = int.Parse(txtMax.Text);
                            prodOp.MinimoOpcionais = int.Parse(txtMinimo.Text);
                        }
                        
                    }
                    con.Insert("spAdicionarProduto_OpcaoTipo", prodOp);
                    ListaTipoOpcao();
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int intCodopcaTipo = int.Parse(TipoOpcaoGrid.SelectedRows[rowIndex].Cells[0].Value.ToString());
                DataSet ds = con.SelectRegistroPorCodigo("Produto_OpcaoTipo", "spObterProduto_OpcaoTipoPorCodigo", intCodopcaTipo);
                if (ds.Tables["Produto_OpcaoTipo"].Rows.Count > 0)
                {
                    txtNome.Text = ds.Tables[0].Rows[0].Field<string>("Nome");
                    cbxOrdem.Text = Convert.ToString(ds.Tables[0].Rows[0].Field<int>("OrdenExibicao"));
                    int iTipo = int.Parse(ds.Tables[0].Rows[0].Field<string>("Tipo"));
                    MarcaRadio(iTipo);
                    if (grpMaxMin.Visible)
                    {
                        txtMax.Text = Convert.ToString(ds.Tables[0].Rows[0].Field<int>("MaximoOpcionais"));
                        txtMinimo.Text = Convert.ToString(ds.Tables[0].Rows[0].Field<int>("MinimoOpcionais"));
                    }
               
                }
            }
            catch (Exception erro)
            {

                throw;
            }
           
        }
        private void MarcaRadio(int iTipo)
        {
            foreach (System.Windows.Forms.Control ctrControl in grpTipo.Controls)
            {
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    if (int.Parse(((System.Windows.Forms.RadioButton)ctrControl).Tag.ToString())== iTipo)
                    {
                        (((System.Windows.Forms.RadioButton)ctrControl).Checked) = true;
                        return;
                    }

                }
            }

        }
        private void TipoOpcaoGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int total = this.TipoOpcaoGrid.SelectedRows.Count;

            for (int i = 0; i < total; i++)
            {
                if (this.TipoOpcaoGrid.Rows[i].Selected)
                {
                    rowIndex = this.TipoOpcaoGrid.Rows[i].Index;
                }
            }
        }
    }
}
