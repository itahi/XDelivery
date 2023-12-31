﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DexComanda.Models;
using DexComanda;

namespace DexComanda
{
    public partial class frmReceita : Form
    {
        private Conexao con;
        private List<Grupo> grupos;
        private int codProduto;

        public frmReceita(int codProduto)
        {
           
            InitializeComponent();
            con = new Conexao();
            grupos = new List<Grupo>();
        }

        private void frmReceita_Load(object sender, EventArgs e)
        {
            this.cbxGrupo.DataSource = con.SelectAll("Grupo", "spObterGrupo").Tables["Grupo"];
            this.cbxGrupo.DisplayMember = "NomeGrupo";
            this.cbxGrupo.ValueMember = "Codigo";
        }

        private void cbxGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxNomeProduto.DataSource = con.SelectRegistroPorNome("@GrupoProduto","Produto", "spObterProdutoPorGrupo", this.cbxGrupo.Text).Tables["Produto"];
            this.cbxNomeProduto.DisplayMember = "NomeProduto";
            this.cbxNomeProduto.ValueMember = "Codigo";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cbxNomeProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxNomeProduto.SelectedValue.ToString())).Tables["Produto"];
            var ReceitaProduto = (Produto.Rows[0]["DescricaoProduto"].ToString());
            var NomeProduto = (Produto.Rows[0]["NomeProduto"].ToString());
            if (!NomeProduto.ToString().Equals(""))
            {
                mmoConteudo.Text = ReceitaProduto.ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
