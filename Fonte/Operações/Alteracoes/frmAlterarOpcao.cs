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
           
        }

        private void Filtrar(object sender, EventArgs e)
        {
            try
            {

                DataSet ds;
                if (cbxOpcao.SelectedValue==null && cbxTipoOpcao.SelectedValue==null)
                {
                    MessageBox.Show("Selecione um tipo de busca para continuar");
                    return;
                }
                if (cbxTipoOpcao.SelectedValue!=null && cbxOpcao.SelectedValue==null)
                {
                    ds = con.RetornaOpcoesPortipo(int.Parse(cbxTipoOpcao.SelectedValue.ToString()));
                    GridView.DataMember = "Opcao";
                }
                else
                {
                    ds = con.RetornaOpcoes(int.Parse(cbxOpcao.SelectedValue.ToString()));
                    GridView.DataMember = "Produto_Opcao";
                }
                GridView.AutoGenerateColumns = true;
                GridView.DataSource = ds;
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            
        }

        private void ExecutarAlteracao(object sender, EventArgs e)
        {
            try
            {
                if (Utils.MessageBoxQuestion("Todos os registros os " + GridView.Rows.Count + " do Filtro abaixo serão alterados, deseja prosseguir?"))
                {
                    if (txtNovoPreco.Text == "0")
                    {
                        if (!Utils.MessageBoxQuestion("Todos Produtos receberão o preço 0,00"))
                        {
                            return;
                        }

                    }
                    decimal dblNovoPreco;
                    Boolean boolAtivoSN = false;
                    Boolean boolOnlineSN = false;
                    dblNovoPreco = decimal.Parse(txtNovoPreco.Text);
                    boolAtivoSN = rbAtivo.Checked;
                    boolOnlineSN = rbOnline.Checked;

                    for (int i = 0; i < GridView.Rows.Count; i++)
                    {
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
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }

        private void BuscaOpcao(object sender, EventArgs e)
        {
           
        }

        private void cbxOpcao_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbxOpcao_DropDown(object sender, EventArgs e)
        {
            if (cbxTipoOpcao.SelectedValue == null)
            {
                MessageBox.Show("Selecione o filtro tipo opção para continuar");
                return;
            }
            Utils.MontaCombox(cbxOpcao, "Nome", "Codigo", "Opcao", "spObterOpcaoPorTipo", int.Parse(cbxTipoOpcao.SelectedValue.ToString()));
        }

        private void cbxTipoOpcao_DropDown(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxTipoOpcao, "Nome", "Codigo", "Produto_OpcaoTipo", "spObterTipoOpcao");
        }
    }
}
