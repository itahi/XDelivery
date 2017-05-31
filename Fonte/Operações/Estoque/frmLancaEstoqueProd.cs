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

namespace DexComanda.Operações
{
    public partial class frmLancaEstoqueProd : Form
    {
        private Conexao con;
        private int i = 0;
        private string strNomeOpcao;
        public frmLancaEstoqueProd()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void frmLancaEstoque_Load(object sender, EventArgs e)
        {
            con = new Conexao();
            try
            {
                cbxGrupo.DataSource = con.SelectAll("Grupo", "spObterGrupoAtivo").Tables["Grupo"];
                cbxGrupo.DisplayMember = "NomeGrupo";
                cbxGrupo.ValueMember = "Codigo";
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private Boolean SelecaoObrigatoria()
        {
            Boolean iReturn = false;
            //Percorre o controle groupbox
            if (grpOpcao.Enabled==false)
            {
                iReturn = false;
            }
            foreach (Control control in grpOpcao.Controls)
            {
                //Busca pelo controle radiobutton
                if (object.ReferenceEquals(control.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    if ((((System.Windows.Forms.RadioButton)control).Checked))
                    {
                        iReturn = true;
                        strNomeOpcao = ((System.Windows.Forms.RadioButton)control).Text; 
                        break;
                    }
                }
            }
            return iReturn;
        }
        private void LimpaTamanhosSabores()
        {

            // Percorre o GroupBox dos tamanhos e e  desmarca todos pra obrigar o usuario marcar o tamanho depois de selecionar os tamanhos
            foreach (System.Windows.Forms.Control ctrControl in grpOpcao.Controls)
            {
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    //Unselect all RadioButtons
                    ((System.Windows.Forms.RadioButton)ctrControl).Checked = false;
                }
            }
            

        }
        private void LancaMovimento(object sender, EventArgs e)
        {
            if (grpOpcao.Enabled && !SelecaoObrigatoria())
            {
                MessageBox.Show(" Selecione " + grpOpcao.Text + " para continuar");
                return;
            }
            try
            {
                gridMovimento.Rows.Add(cbxProdutos.SelectedValue.ToString(), cbxProdutos.Text+" "+strNomeOpcao, txtprecocompra.Text, txtQuantidade.Text);
                strNomeOpcao = "";
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            LimpaTamanhosSabores();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new Conexao();
            try
            {

                for (int i = 0; i < gridMovimento.Rows.Count; i++)
                {
                    if (gridMovimento.Rows[i].Cells[0].Value == null)
                    {
                        break;
                    }
                    Produto_Estoque prodEs = new Produto_Estoque()
                    {
                        CodProduto = int.Parse(gridMovimento.Rows[i].Cells["CodProduto"].Value.ToString()),
                        NomeProduto = gridMovimento.Rows[i].Cells["NomeProduto"].Value.ToString(),
                        PrecoCompra = decimal.Parse(gridMovimento.Rows[i].Cells["Preco"].Value.ToString()),
                        DataAtualizacao = DateTime.Now,
                        Quantidade = decimal.Parse(gridMovimento.Rows[i].Cells["Quantidade"].Value.ToString())
                    };
                    con.Insert("spAtualizaEstoque", prodEs);
                }

                MessageBox.Show("Estoque lançado com sucesso ");
                gridMovimento.Rows.Clear();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        private void txtprecocompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }

        private void cbxTipoProduto_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxProdutos.DataSource = con.SelectRegistroPorCodigo("Produto", "spObterProdutoEstoque", int.Parse(cbxGrupo.SelectedValue.ToString())).Tables["Produto"];
                cbxProdutos.DisplayMember = "NomeProduto";
                cbxProdutos.ValueMember = "Codigo";
                txtQuantidade.Text = "1";
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        private void EscondeTamanhos()
        {
            grpOpcao.Enabled = false;
            foreach (System.Windows.Forms.Control ctrControl in grpOpcao.Controls)
            {
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    //Unselect all 
                    ((System.Windows.Forms.RadioButton)ctrControl).Visible = false;
                }
            }
        }

        private void cbxProdutosGrid_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataSet dsOpcoes = con.RetornaOpcoesProduto(int.Parse(cbxProdutos.SelectedValue.ToString()));
            EscondeTamanhos();

            if (dsOpcoes.Tables[0].Rows.Count==0)
            {
                grpOpcao.Enabled = false;
                return;
            }
            for (int i = 0; i < dsOpcoes.Tables[0].Rows.Count; i++)
            {
              
                if (int.Parse(dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<string>("Tipo")) == 1)
                {
                    grpOpcao.Enabled = true;
                    grpOpcao.Text = dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<string>("NomeTipo");
                    foreach (System.Windows.Forms.Control control in grpOpcao.Controls)
                    {
                        if (!object.ReferenceEquals(control.GetType(), typeof(System.Windows.Forms.RadioButton)))
                        {
                            return;
                        }
                        if (!((System.Windows.Forms.RadioButton)control).Visible)
                        {
                            ((System.Windows.Forms.RadioButton)control).Text = dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<string>("Nome");
                            ((System.Windows.Forms.RadioButton)control).Visible = true;
                            break;
                        }
                    }
                }
            }
        }

        private void cbxGrupo_DropDown(object sender, EventArgs e)
        {
            try
            {
                cbxGrupo.DataSource = con.SelectAll("Grupo", "spObterGrupoAtivo").Tables["Grupo"];
                cbxGrupo.DisplayMember = "NomeGrupo";
                cbxGrupo.ValueMember = "Codigo";
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            
        }
    }
}
