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

namespace DexComanda.Operações.Estoque
{
    public partial class frmLancaEstoqueInsumo : Form
    {
        private Conexao con;
        private int codInsumo;
        public frmLancaEstoqueInsumo()
        {
            InitializeComponent();
        }

        private void frmLancaEstoqueInsumo_Load(object sender, EventArgs e)
        {
            con = new Conexao();
        }

        private void cbxNomeInsumo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
                if (cbxNomeInsumo.Text.Length < 3)
                {
                    return;
                }
                DataSet dsInsumos = new DataSet();
                dsInsumos = con.SelectRegistroPorNome("@Nome", "Insumo", "spObterInsumoPorNome", cbxNomeInsumo.Text);
                if (dsInsumos.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < dsInsumos.Tables[0].Rows.Count; i++)
                {
                    lista.Add(dsInsumos.Tables[0].Rows[i].Field<string>("Nome"));
                }
                cbxNomeInsumo.AutoCompleteCustomSource = lista;
               
                cbxNomeInsumo.DataSource = dsInsumos.Tables[0];
                cbxNomeInsumo.ValueMember = "Codigo";
                cbxNomeInsumo.DisplayMember = "Nome";
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            
        }

        private void btnLançar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxNomeInsumo.SelectedValue.ToString()=="" || cbxNomeInsumo.SelectedValue==null)
                {
                    MessageBox.Show("Selecione o insumo na lista");
                    cbxNomeInsumo.Focus();
                    return;
                }
                if (txtPreco.Text=="")
                {
                    MessageBox.Show("Informe o preço de compra ");
                    txtPreco.Focus();
                    return;
                }
                if (txtQuantidade.Text=="")
                {
                    MessageBox.Show("Informe a quantidade comprada ");
                    txtQuantidade.Focus();
                    return;
                }

                gridviewinsumo.Rows.Add(cbxNomeInsumo.SelectedValue.ToString(), cbxNomeInsumo.Text, txtQuantidade.Text, txtPreco.Text);

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void ConfirmarLançamento(object sender, EventArgs e)
        {
            try
            {
                if (gridviewinsumo.Rows.Count==0)
                {
                    MessageBox.Show("É necessário que tenha ao menos 1 item lançado");
                    return;
                }
                for (int i = 0; i < gridviewinsumo.Rows.Count; i++)
                {
                    if (gridviewinsumo.Rows[i].Cells[0].Value==null || gridviewinsumo.Rows[i].Cells[0].Value.ToString()=="")
                    {
                        break;
                    }
                    Insumo_Estoque insu = new Insumo_Estoque()
                    {
                        CodInsumo = int.Parse(gridviewinsumo.Rows[i].Cells["Codigo"].Value.ToString()),
                        Quantidade = decimal.Parse(gridviewinsumo.Rows[i].Cells["Quantidade"].Value.ToString()),
                        Preco = decimal.Parse(gridviewinsumo.Rows[i].Cells["Preco"].Value.ToString())
                    };
                    con.Insert("spAdicionarInsumo_Estoque", insu);
                   
                }
                MessageBox.Show("Estoque Lançado com sucesso");
                gridviewinsumo.Rows.Clear();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void cbxNomeInsumo_DropDown(object sender, EventArgs e)
        {
            try
            {
                Utils.MontaCombox(cbxNomeInsumo, "Nome", "Codigo", "Insumo", "spObterInsumo");
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
    }
}
