﻿using DexComanda.Models;
using DexComanda.Models.Alteracoes_Multiplas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações.Alteracoes
{
    public partial class frmAlterarOpcao : Form
    {
        private Conexao con;
        public frmAlterarOpcao()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void txtNovoPreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }

        private void frmAlterarOpcao_Load(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxTipoOpcao, "Nome", "Codigo", "Produto_OpcaoTipo", "spObterTipoOpcao");
        }

        private void Filtrar(object sender, EventArgs e)
        {
            DataSet ds;
            if (cbxOpcao.SelectedValue!=null)
            {
                ds = con.RetornaOpcoes(int.Parse(cbxOpcao.SelectedValue.ToString()));

                GridView.DataSource = ds;
                GridView.DataMember = "Produto_Opcao";
                GridView.AutoGenerateColumns = true;
            }
        }

        private void ExecutarAlteracao(object sender, EventArgs e)
        {
            try
            {
                if (Utils.MessageBoxQuestion("Todos os registros os " + GridView.Rows.Count + " do Filtro abaixo serão alterados, deseja prosseguir?"))
                {
                    if (txtNovoPreco.Text=="0")
                    {
                        if (!Utils.MessageBoxQuestion("Todos Produtos receberão o preço 0,00"))
                        {
                            return;
                        }
                        
                    }
                    decimal dblNovoPreco;
                    Boolean boolAtivoSN = false;
                    Boolean boolOnlineSN = false;

                    for (int i = 0; i < GridView.Rows.Count; i++)
                    {
                        dblNovoPreco = decimal.Parse(txtNovoPreco.Text);
                        boolAtivoSN= rbAtivo.Checked;
                        boolOnlineSN = rbOnline.Checked;

                        //Opcao opcao = new Opcao()
                        //{
                        //    Codigo = int.Parse(GridView.Rows[i].Cells["Codigo"].Value.ToString()),
                        //    AtivoSN = boolAtivoSN,
                        //    OnlineSN = boolOnlineSN,
                        //    DataAlteracao = DateTime.Now,
                        //    Nome = GridView.Rows[i].Cells["Nome"].Value.ToString(),
                        //    Tipo = cbxTipoOpcao.SelectedValue.ToString()
                        //};
                        AlteracaoMultiplaOpcao multiOpcao = new AlteracaoMultiplaOpcao()
                        {
                            CodOpcao = int.Parse(GridView.Rows[i].Cells["CodOpcao"].Value.ToString()),
                            OnlineSN = boolOnlineSN,
                            Preco = dblNovoPreco
                        };
                        con.Update("spAlterarMultiploOpcao", multiOpcao);
                    }
                }
                GridView.DataSource = null;
                GridView.DataMember = null;

            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel executar a operação " + erro.Message);
            }
           
        }

        private void BuscaOpcao(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxOpcao, "Nome", "Codigo", "Opcao", "spObterOpcaoPorTipo",int.Parse(cbxTipoOpcao.SelectedValue.ToString()));
        }
    }
}
