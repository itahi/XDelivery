using DexComanda.Models;
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
                if (dsProd.Tables[0].Rows.Count>0)
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
                dsProd = con.SelectAll("Produto", "spObterProduto");
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
            richTextGrande.Text = "Escolhendo essa opção os produtos da 'Grid' abaixo "+
                                   " terão o seu valor atual somado ao valor preenchido no campo 'Novo Valor' "+
                                   " Ex. Refrigerante R$10,00 + 1,50  = Novo valor R$11,50 ";
        }

        private void rbPorcentagem_Click(object sender, EventArgs e)
        {
            richTextGrande.Text = "";
            richTextGrande.Text = "Escolhendo essa opção os produtos da 'Grid' abaixo " +
                                   " terão o seu valor atual somado a porcentagem preenchida no campo 'Novo Valor' "+
                                   " Ex. Refrigerante R$10,00 + 10%  = Novo valor R$11,00 ";
        }

        private void ExecutarAlteracoes(object sender, EventArgs e)
        {

            if (txtnewValue.Text=="" && chkAlteraPreco.Checked)
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
                                    int.Parse(AdicionaisGridView.Rows[intFor].Cells["CodOpcao"].Value.ToString()), decimal.Parse(AdicionaisGridView.Rows[intFor].Cells["Valor"].Value.ToString()));
                            }
                        }
                    }
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
                
            }
            GridViewProdutos.DataSource = null;
            GridViewProdutos.DataMember = null;
            
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
        private void MenuAuxiliar(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem Excluir = new MenuItem(" 0 - Remover Produto da Lista de Alterações ");
                //  MenuItem Excluir2 = new MenuItem("  ");
                Excluir.Click += DeletarRegistro;
                m.MenuItems.Add(Excluir);
                ///   m.MenuItems.Add(Excluir2);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }
        private void DeletarRegistro(object sender, EventArgs e)
        {
            try
            {

                if (GridViewProdutos.SelectedRows.Count>0)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkAtivaDesconto_CheckedChanged(object sender, EventArgs e)
        {
            grpDesconto.Enabled = chkAtivaDesconto.Checked;
        }

        private void btnOpcao_Click(object sender, EventArgs e)
        {

            pnlAdicionais.Visible = true;
            pnlAdicionais.Show();
            if (pnlAdicionais.Visible)
            {
                this.cbxOpcao.DataSource = con.SelectAll("Opcao", "spObterOpcao").Tables["Opcao"];
                this.cbxOpcao.DisplayMember = "Nome";
                this.cbxOpcao.ValueMember = "Codigo";
                AdicionaisGridView.Columns.Add("CodOpcao", "CodOpcao");
                AdicionaisGridView.Columns.Add("Valor", "Valor");
                AdicionaisGridView.Columns.Add("Nome", "Nome");
            }
            //frmOpcaoProduto frm = new frmOpcaoProduto();
            //frm.StartPosition = FormStartPosition.CenterParent;
            //frm.ShowDialog();
            
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
                iCountLinhas = AdicionaisGridView.Rows.Count;
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
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

        private void Excluir(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem Excluir = new MenuItem(" 0 - Remover Produto da Lista de Alterações ");
                //  MenuItem Excluir2 = new MenuItem("  ");
                Excluir.Click += RemoverLinha;
                m.MenuItems.Add(Excluir);
                ///   m.MenuItems.Add(Excluir2);

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

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            cbxGrupo.Enabled = !chkTodos.Checked;
        }

        private void chkAlteraPreco_CheckedChanged(object sender, EventArgs e)
        {
            grpPrecos.Enabled = chkAlteraPreco.Checked;
        }
    }
}
