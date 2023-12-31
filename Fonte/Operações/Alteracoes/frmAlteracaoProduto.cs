﻿using DexComanda.Models;
using DexComanda.Models.Alteracoes_Multiplas;
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

namespace DexComanda.Operações.Alteracoes
{
    public partial class frmAlteracaoProduto : Form
    {
        private Conexao con;
        private string Marcados;
        public frmAlteracaoProduto()
        {
            InitializeComponent();
            con = new Conexao();
        }

        private void frmAlteracaoProduto_Load(object sender, EventArgs e)
        {
            grpDesconto.Enabled = chkAtivaDesconto.Checked;

            this.cbxGrupo.DataSource = con.SelectAll("Grupo", "spObterGrupoAtivo").Tables["Grupo"];
            this.cbxGrupo.DisplayMember = "NomeGrupo";
            this.cbxGrupo.ValueMember = "Codigo";
        }

        private void Filtro(object sender, EventArgs e)
        {
            if (cbxGrupo.Enabled)
            {
                DataSet dsProd;
                dsProd = con.SelectRegistroPorNome("@GrupoProduto", "Produto", "spObterProdutoPorGrupo", this.cbxGrupo.Text);
                if (dsProd.Tables[0].Rows.Count > 0)
                {
                    GridViewProdutos.DataSource = null;
                    GridViewProdutos.DataSource = dsProd;
                    GridViewProdutos.AutoGenerateColumns = true;
                    GridViewProdutos.DataMember = "Produto";

                    for (int i = 0; i < GridViewProdutos.Columns.Count; i++)
                    {
                        if (GridViewProdutos.Columns[i].HeaderText != "NomeProduto")
                        {
                            GridViewProdutos.Columns[i].ReadOnly = true;
                        }
                    }
                    con.Close();
                }
            }
            else if (chkTodos.Checked)
            {
                DataSet dsProd;
                dsProd = con.SelectAll("Produto", "spObterProdutoAlteracao");
                GridViewProdutos.DataSource = null;
                GridViewProdutos.DataSource = dsProd;
                GridViewProdutos.AutoGenerateColumns = true;
                GridViewProdutos.DataMember = "Produto";

                for (int i = 0; i < GridViewProdutos.Columns.Count; i++)
                {
                    if (GridViewProdutos.Columns[i].HeaderText != "NomeProduto")
                    {
                        GridViewProdutos.Columns[i].ReadOnly = true;
                    }
                }
                con.Close();
            }
        }

        private void rbValor_Click(object sender, EventArgs e)
        {
            richTextGrande.Text = "";
            richTextGrande.Text = "Escolhendo essa opção os produtos da 'Grid' abaixo " +
                                   " terão o seu valor atual somado ao valor preenchido no campo 'Novo Valor' " +
                                   " Ex. Refrigerante R$10,00 + 1,50  = Novo valor R$11,50 ";
        }

        private void rbPorcentagem_Click(object sender, EventArgs e)
        {
            richTextGrande.Text = "";
            richTextGrande.Text = "Escolhendo essa opção os produtos da 'Grid' abaixo " +
                                   " terão o seu valor atual somado a porcentagem preenchida no campo 'Novo Valor' " +
                                   " Ex. Refrigerante R$10,00 + 10%  = Novo valor R$11,00 ";
        }

        private void ExecutarAlteracoes(object sender, EventArgs e)
        {

            if (txtnewValue.Text == "" && chkAlteraPreco.Checked)
            {
                MessageBox.Show("Preencha o campo com o valor desejado");
                txtnewValue.Focus();
                return;
            }

            if (Utils.MessageBoxQuestion("Confirma a Alteração de " + GridViewProdutos.Rows.Count.ToString() + " Produtos?"))
            {
                try
                {
                    for (int i = 0; i < GridViewProdutos.Rows.Count; i++)
                    {
                        decimal dcNewPreco = 0;
                        txtnewValue.Text = "0";
                        if (rbPorcentagem.Checked)
                        {
                            dcNewPreco = decimal.Parse(GridViewProdutos.Rows[i].Cells["PrecoProduto"].Value.ToString()) + decimal.Parse(GridViewProdutos.Rows[i].Cells["PrecoProduto"].Value.ToString()) * decimal.Parse(txtnewValue.Text) / 100;
                        }
                        else if (rbValor.Checked)
                        {
                            dcNewPreco = decimal.Parse(GridViewProdutos.Rows[i].Cells["PrecoProduto"].Value.ToString()) + dcNewPreco;
                        }
                        else
                        {
                            dcNewPreco = decimal.Parse(GridViewProdutos.Rows[i].Cells["PrecoProduto"].Value.ToString());
                        }
                        MultiplaProduto prod = new MultiplaProduto()
                        {
                            Codigo = int.Parse(GridViewProdutos.Rows[i].Cells["Codigo"].Value.ToString()),
                            Nome = GridViewProdutos.Rows[i].Cells["NomeProduto"].Value.ToString(),
                            Preco = dcNewPreco,//decimal.Parse(GridViewProdutos.Rows[i].Cells["Preco"].Value.ToString()),
                            DataAlteracao = DateTime.Now
                        };

                        if (chkAtivaDesconto.Checked)
                        {
                            var datInicio = Convert.ToDateTime(dtInicio.Value.ToShortDateString() + " 00:00:00");
                            var datFim = Convert.ToDateTime(dtFim.Value.ToShortDateString() + " 23:59:59");
                            prod.DiasDesconto = DiasSelecinado();
                            prod.DataInicioPromocao = datInicio;
                            prod.DataFimPromocao = datFim;
                            prod.PrecoDesconto = decimal.Parse(GridViewProdutos.Rows[i].Cells["PrecoProduto"].Value.ToString()) - decimal.Parse(GridViewProdutos.Rows[i].Cells["PrecoProduto"].Value.ToString()) * decimal.Parse(txtPorcentagem.Text) / 100;
                        }
                        else
                        {
                            prod.DiasDesconto = "";
                            prod.DataInicioPromocao = DateTime.Now.AddYears(-1);
                            prod.DataFimPromocao = DateTime.Now.AddYears(-1);
                            prod.PrecoDesconto = 0;
                        }
                        con.Update("spAlterarMultiploProduto", prod);

                        DataSet dsOpcoes = con.SelectOpcaoProduto(prod.Codigo.ToString());
                        for (int intFor = 0; intFor < dsOpcoes.Tables[0].Rows.Count; intFor++)
                        {
                            int iTipo = int.Parse(dsOpcoes.Tables[0].Rows[intFor].Field<string>("Tipo"));
                            if (iTipo == 1)
                            {
                                decimal iPorcentagemDesc = 0;
                                if (txtPorcentagem.Text != "")
                                {
                                    iPorcentagemDesc = decimal.Parse(txtPorcentagem.Text);
                                }
                                Produto_Opcao prodOp = new Produto_Opcao()
                                {
                                    CodOpcao = dsOpcoes.Tables[0].Rows[intFor].Field<int>("CodOpcao"),
                                    CodProduto = prod.Codigo,
                                    CodTipo = dsOpcoes.Tables[0].Rows[intFor].Field<int>("CodTipo"),
                                    DataAlteracao = DateTime.Now,
                                    Preco = dsOpcoes.Tables[0].Rows[intFor].Field<decimal>("Preco"),
                                    PrecoProcomocao = dsOpcoes.Tables[0].Rows[intFor].Field<decimal>("Preco") - dsOpcoes.Tables[0].Rows[intFor].Field<decimal>("Preco") * iPorcentagemDesc / 100

                                };
                                con.Update("spAlterarOpcaoProduto", prodOp);
                            }

                        }

                        if (AdicionaisGridView.Rows.Count > 0)
                        {
                            for (int intFor = 0; intFor < AdicionaisGridView.Rows.Count; intFor++)
                            {
                                con.SalvarAdicionais(prod.Codigo,
                                    int.Parse(AdicionaisGridView.Rows[intFor].Cells["CodOpcao"].Value.ToString()),
                                    decimal.Parse(AdicionaisGridView.Rows[intFor].Cells["Valor"].Value.ToString()),
                                    int.Parse(AdicionaisGridView.Rows[intFor].Cells["CodTipo"].Value.ToString()));
                            }
                        }
                        //if (InsumoGridView.Rows.Count>0)
                        //{
                            for (int intInsumo = 0; intInsumo < InsumoGridView.Rows.Count; intInsumo++)
                            {
                                InsumoProduto insPro = new InsumoProduto()
                                {
                                    CodInsumo = int.Parse(InsumoGridView.Rows[intInsumo].Cells["COdInsumo"].Value.ToString()),
                                    CodProduto = int.Parse(GridViewProdutos.Rows[i].Cells["Codigo"].Value.ToString()),
                                    Quantidade = decimal.Parse(InsumoGridView.Rows[intInsumo].Cells["Quantidade"].Value.ToString())
                                };
                                con.Insert("spAdicionarProdutoInsumo", insPro);
                            }
                        //}
                    }
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }

            }
            GridViewProdutos.DataSource = null;
            GridViewProdutos.DataMember = null;
            // RemoveColunas(AdicionaisGridView);

        }
        public string DiasSelecinado()
        {
            Marcados = "";
            if (chkSegunda.Checked)
            {
                Marcados = "Monday";
            }
            if (chkTerca.Checked)
            {
                Marcados = Marcados + ";Tuesday";
            }
            if (ChkQuarta.Checked)
            {
                Marcados = Marcados + ";Wednesday ";
            }
            if (chkQuinta.Checked)
            {
                Marcados = Marcados + ";Thursday";
            }
            if (ChkSexta.Checked)
            {
                Marcados = Marcados + ";Friday";
            }
            if (ChkSabado.Checked)
            {
                Marcados = Marcados + ";Sunday";
            }
            if (chkDomingo.Checked)
            {
                Marcados = Marcados + ";Saturday";
            }

            return Marcados;
        }


        private void ExcluirOpcao(object sender, EventArgs e)
        {
            try
            {
                if (AdicionaisGridView.SelectedRows.Count > 0)
                {
                    AdicionaisGridView.Rows.RemoveAt(AdicionaisGridView.CurrentRow.Index);
                }

                else
                {
                    MessageBox.Show("Selecione o registro para excluir");
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }
        private void DeletarRegistro(object sender, EventArgs e)
        {
            try
            {

                if (GridViewProdutos.SelectedRows.Count > 0)
                {
                    GridViewProdutos.Rows.RemoveAt(GridViewProdutos.CurrentRow.Index);
                }

                else
                {
                    MessageBox.Show("Selecione o registro para excluir");
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }

        private void chkAtivaDesconto_CheckedChanged(object sender, EventArgs e)
        {
            grpDesconto.Enabled = chkAtivaDesconto.Checked;
        }

        private void RemoveColunas(DataGridView dv,Panel pnl)
        {
            for (int i = 0; i < dv.Rows.Count; i++)
            {
                dv.Rows.Clear();
            }
            pnl.Visible = false;
            pnl.Hide();
        }
        private void btnOpcao_Click(object sender, EventArgs e)
        {

            pnlAdicionais.Visible = true;
            pnlAdicionais.Show();
            if (pnlAdicionais.Visible)
            {
                //AdicionaisGridView.Columns.Add("CodOpcao", "CodOpcao");
                //AdicionaisGridView.Columns.Add("Valor", "Valor");
                //AdicionaisGridView.Columns.Add("Nome", "Nome");
                //AdicionaisGridView.Columns.Add("CodTipo", "CodTipo");
            }

        }

        private void btnAdicionarOpcao_Click(object sender, EventArgs e)
        {
            try
            {
                int iCountLinhas = AdicionaisGridView.Rows.Count;
                if (AdicionaisGridView.DataSource != null)
                {
                    AdicionaisGridView.AutoGenerateColumns = false;
                    AdicionaisGridView.DataSource = null;
                    AdicionaisGridView.DataMember = null;
                }

                AdicionaisGridView.Rows.Add();
                AdicionaisGridView.Rows[iCountLinhas].Cells[0].Value = int.Parse(cbxOpcao.SelectedValue.ToString());
                AdicionaisGridView.Rows[iCountLinhas].Cells[1].Value = decimal.Parse(txtPrecoOpcao.Text);
                AdicionaisGridView.Rows[iCountLinhas].Cells[2].Value = cbxOpcao.Text.ToString();
                AdicionaisGridView.Rows[iCountLinhas].Cells["CodTipo"].Value = int.Parse(cbxTipoOpcao.SelectedValue.ToString());
                iCountLinhas = AdicionaisGridView.Rows.Count;
            }
            catch (Exception erro)
            {

                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void Excluir(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem Excluir = new MenuItem(" 0 - Remover Produto da Lista de Alterações ");
                Excluir.Click += RemoverLinha;
                m.MenuItems.Add(Excluir);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }
        private void RemoverLinha(object sender, EventArgs e)
        {
            try
            {
                if (AdicionaisGridView.SelectedRows.Count > 0)
                {
                    AdicionaisGridView.Rows.RemoveAt(AdicionaisGridView.CurrentRow.Index);
                }

                else
                {
                    MessageBox.Show("Selecione o registro para excluir");
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }
        private void RemoverLinhaInsumo(object sender, EventArgs e)
        {
            try
            {
                if (InsumoGridView.SelectedRows.Count > 0)
                {
                    InsumoGridView.Rows.RemoveAt(InsumoGridView.CurrentRow.Index);
                }

                else
                {
                    MessageBox.Show("Selecione o registro para excluir");
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            cbxGrupo.Enabled = !chkTodos.Checked;
        }

        private void chkAlteraPreco_CheckedChanged(object sender, EventArgs e)
        {
            grpPrecos.Enabled = chkAlteraPreco.Checked;
        }

        private void btnAdicionarOpcao_Click_1(object sender, EventArgs e)
        {
            try
            {
                int iCountLinhas = AdicionaisGridView.Rows.Count;
                if (AdicionaisGridView.DataSource != null)
                {
                    AdicionaisGridView.AutoGenerateColumns = false;
                    AdicionaisGridView.DataSource = null;
                    AdicionaisGridView.DataMember = null;
                }

                for (int i = 0; i < AdicionaisGridView.Rows.Count; i++)
                {
                    if (cbxOpcao.SelectedValue.ToString() == AdicionaisGridView.Rows[i].Cells["CodOpcao"].Value.ToString())
                    {
                        if (!Utils.MessageBoxQuestion("Essa opção já esta vinculada a esse produto , deseja adicionar novamente?"))
                        {
                            return;
                        }
                    }
                }
                AdicionaisGridView.Rows.Add();
                AdicionaisGridView.Rows[iCountLinhas].Cells[0].Value = int.Parse(cbxOpcao.SelectedValue.ToString());
                AdicionaisGridView.Rows[iCountLinhas].Cells[1].Value = decimal.Parse(txtPrecoOpcao.Text);
                AdicionaisGridView.Rows[iCountLinhas].Cells[2].Value = cbxOpcao.Text.ToString();
                AdicionaisGridView.Rows[iCountLinhas].Cells["CodTipo"].Value = int.Parse(cbxTipoOpcao.SelectedValue.ToString());
                iCountLinhas = AdicionaisGridView.Rows.Count;
            }
            catch (Exception erro)
            {

                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }

        private void ListaTipos(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxTipoOpcao, "Nome", "Codigo", "Produto_OpcaoTipo", "spObterTipoOpcao");
        }

        private void ListaOpcao(object sender, EventArgs e)
        {
            if (cbxTipoOpcao.SelectedIndex <= -1)
            {
                Utils.MontaCombox(cbxOpcao, "Nome", "Codigo", "Opcao", "spObterOpcao");
            }
            else
            {
                Utils.MontaCombox(cbxOpcao, "Nome", "Codigo", "Opcao", "spObterOpcaoPorTipo", int.Parse(cbxTipoOpcao.SelectedValue.ToString()));
            }
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (!Utils.MessageBoxQuestion(" Todos Produtos Selecionados no Filtro receberão esses items adicionais, Deseja continuar?"))
            {
                return;
            }
            else
            {
                pnlAdicionais.Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlAdicionais.Visible = false;
            RemoveColunas(AdicionaisGridView, pnlAdicionais);
        }
        private void MenuAuxilarOpcoes(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem Excluir = new MenuItem(" 0 - Excluir ");
                Excluir.Click += ExcluirOpcao;
                m.MenuItems.Add(Excluir);
                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }

        private void MenuAuxiliar(object sender, MouseEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem Excluir = new MenuItem(" 0 - Remover da lista de alterações ");
                Excluir.Click += DeletarRegistro;
                m.MenuItems.Add(Excluir);
                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }

        }

        private void GridViewProdutos_MouseClick(object sender, MouseEventArgs e)
        {
            MenuAuxiliar(sender, e);
        }

        private void btnInsumoVincular_Click(object sender, EventArgs e)
        {
            pnlInsumos.Visible = true;
            pnlInsumos.Show();
        }

        private void cbxInsumo_DropDown(object sender, EventArgs e)
        {
            try
            {
                Utils.MontaCombox(cbxInsumo, "Nome", "Codigo", "Insumo", "spObterInsumo");
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!Utils.MessageBoxQuestion(" Todos Produtos Selecionados no Filtro receberão esses items adicionais, Deseja continuar?"))
            {
                return;
            }
            else
            {
                pnlAdicionais.Visible = false;
            }
        }

        private void btnInsumoSalvar_Click(object sender, EventArgs e)
        {
            if (!Utils.MessageBoxQuestion(" Todos Produtos Selecionados no Filtro receberão esses insumos, Deseja continuar?"))
            {
                return;
            }
            else
            {
                pnlInsumos.Visible = false;
            }
        }

        private void cbxTipoOpcao_DropDown(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxTipoOpcao, "Nome", "Codigo", "Produto_OpcaoTipo", "spObterTipoOpcao");
        }

        private void cbxOpcao_DropDown(object sender, EventArgs e)
        {
            if (cbxTipoOpcao.SelectedIndex <= -1)
            {
                Utils.MontaCombox(cbxOpcao, "Nome", "Codigo", "Opcao", "spObterOpcao");
            }
            else
            {
                Utils.MontaCombox(cbxOpcao, "Nome", "Codigo", "Opcao", "spObterOpcaoPorTipo", int.Parse(cbxTipoOpcao.SelectedValue.ToString()));
            }
        }

        private void btnAdicionarOpcao_Click_2(object sender, EventArgs e)
        {
            try
            {
                int iCountLinhas = AdicionaisGridView.Rows.Count;
                if (AdicionaisGridView.DataSource != null)
                {
                    AdicionaisGridView.AutoGenerateColumns = false;
                    AdicionaisGridView.DataSource = null;
                    AdicionaisGridView.DataMember = null;
                }

                for (int i = 0; i < AdicionaisGridView.Rows.Count; i++)
                {
                    if (cbxOpcao.SelectedValue.ToString() == AdicionaisGridView.Rows[i].Cells["CodOpcao"].Value.ToString())
                    {
                        if (!Utils.MessageBoxQuestion("Essa opção já esta vinculada a esse produto , deseja adicionar novamente?"))
                        {
                            return;
                        }
                    }
                }
                AdicionaisGridView.Rows.Add();
                AdicionaisGridView.Rows[iCountLinhas].Cells[0].Value = int.Parse(cbxOpcao.SelectedValue.ToString());
                AdicionaisGridView.Rows[iCountLinhas].Cells[1].Value = decimal.Parse(txtPrecoOpcao.Text);
                AdicionaisGridView.Rows[iCountLinhas].Cells[2].Value = cbxOpcao.Text.ToString();
                AdicionaisGridView.Rows[iCountLinhas].Cells["CodTipo"].Value = int.Parse(cbxTipoOpcao.SelectedValue.ToString());
                iCountLinhas = AdicionaisGridView.Rows.Count;
            }
            catch (Exception erro)
            {

                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void btnInsumoAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int iCountLinhas = InsumoGridView.Rows.Count;
                if (InsumoGridView.DataSource != null)
                {
                    InsumoGridView.AutoGenerateColumns = false;
                    InsumoGridView.DataSource = null;
                    InsumoGridView.DataMember = null;
                }

                for (int i = 0; i < InsumoGridView.Rows.Count; i++)
                {
                    if (cbxInsumo.SelectedValue.ToString() == InsumoGridView.Rows[i].Cells["CodInsumo"].Value.ToString())
                    {
                        if (!Utils.MessageBoxQuestion("Este insumo já foi vinculado, deseja vincular novamente?"))
                        {
                            return;
                        }
                    }
                }
                InsumoGridView.Rows.Add();
                InsumoGridView.Rows[iCountLinhas].Cells["CodInsumo"].Value = int.Parse(cbxInsumo.SelectedValue.ToString());
                InsumoGridView.Rows[iCountLinhas].Cells["Insumo"].Value = cbxInsumo.Text;
                InsumoGridView.Rows[iCountLinhas].Cells["Quantidade"].Value = txtQtdInsumo.Text;
                iCountLinhas = AdicionaisGridView.Rows.Count;
            }
            catch (Exception erro)
            {

                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void MenuAuxiliarOpcao(object sender, MouseEventArgs e)
        {
            Excluir(sender, e);
        }

        private void InsumoGridView_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem Excluir = new MenuItem(" 0 - Remover Insumo ");
                Excluir.Click += RemoverLinhaInsumo;
                m.MenuItems.Add(Excluir);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            RemoveColunas(AdicionaisGridView, pnlAdicionais);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoveColunas(InsumoGridView, pnlInsumos);
        }
    }
}
