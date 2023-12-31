﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Relatorios
{
    public partial class frmReportProdutos : Form
    {
        private Conexao con;
        public frmReportProdutos()
        {
            InitializeComponent();
            con = new Conexao();
        }
        
        private void frmReportProdutos_Load(object sender, EventArgs e)
        {
            this.cbxGrupos.DataSource = con.SelectAll("Grupo", "spObterGrupo").Tables["Grupo"];
            this.cbxGrupos.DisplayMember = "NomeGrupo";
            this.cbxGrupos.ValueMember = "Codigo";
        }

        private void Consultar(object sender, EventArgs e)
        {
            DataSet Produto = null;
            DataRow produtoRow = null;

            if (cbxGrupos.Text!="")
            {
                Produto = con.SelectRegistroPorNome("@GrupoProduto","Produto", "spObterProdutoPorGrupo", cbxGrupos.Text);
                if (Produto.Tables["Produto"].Rows.Count>0)
                {
                    produtoRow = Produto.Tables["Produto"].Rows[0];  
                } 
                else
                {
                    MessageBox.Show("Não há produtos cadastrados nesse Grupo");
                }
                
            }

            // Consulta Produto
            DataSet ds = con.SelectAll("Produto", "spObterProduto");

            DataView dv = ds.Tables[0].DefaultView;

            string query = "";
            if (cbxGrupos.Text!="")
            {
                query = "GrupoProduto = '" + cbxGrupos.Text + "'";
                dv.RowFilter = query;

                if (dv.Count>0)
                {
                    this.produtoBindingSource.DataSource = dv;
                    this.reportProdutos.RefreshReport();
                }
            }
            else
            {
                dv.RowFilter = "";
                this.produtoBindingSource.DataSource = dv;
                this.reportProdutos.RefreshReport();
            }
        }
    }
}
