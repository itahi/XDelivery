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

namespace DexComanda
{
    public partial class frmContato : Form
    {
        public frmContato()
        {
            InitializeComponent();
        }


        private void openCadastrarUsuarios(object sender, EventArgs e)
        {

        }

        private void openCadastrarGrupos(object sender, EventArgs e)
        {

        }

        private void openCadastrarProdutos(object sender, EventArgs e)
        {

        }

        private void openCadastrarCliente(object sender, EventArgs e)
        {

        }

        private void MainDex_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conexao con;
            con = new Conexao();
            DataSet dsOpcao = con.SelectAdicionalLanche();
            DataSet dsLanches = con.SelectLanches();
            try
            {
      
                for (int i = 0; i < dsLanches.Tables[0].Rows.Count; i++)
                {
                    for (int intFor = 0; intFor < dsOpcao.Tables[0].Rows.Count; intFor++)
                    {
                        Produto_Opcao prodOP = new Produto_Opcao()
                        {
                            CodOpcao = dsOpcao.Tables["Produto_Opcao"].Rows[intFor].Field<int>("CodOpcao"),
                            CodProduto = dsLanches.Tables["Produto"].Rows[i].Field<int>("Codigo"),
                            DataAlteracao = DateTime.Now,
                            Preco =  dsOpcao.Tables["Produto_Opcao"].Rows[intFor].Field<decimal>("Preco"),
                        };
                        con.Insert("spAdicionarOpcaProduto", prodOP);
                    }
                   

                }
            }
            catch (Exception erro)
            {

                throw;
            }
            
        }
    }
}
