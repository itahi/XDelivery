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
    public partial class frmAlteracaoCliente : Form
    {
        private Conexao con;
        public frmAlteracaoCliente()
        {
            InitializeComponent();
            con = new Conexao();
        }

        private void frmAlteracaoCliente_Load(object sender, EventArgs e)
        {
            this.cbxRegiao.DataSource = con.SelectAll("RegiaoEntrega", "spObterRegioes").Tables["RegiaoEntrega"];
            this.cbxRegiao.DisplayMember = "NomeRegiao";
            this.cbxRegiao.ValueMember = "Codigo";
        }

        private void Filtro(object sender, EventArgs e)
        {
            if (txtBairro.Text!="" || cbxRegiao.SelectedValue.ToString()!="")
            {
                int iIDRegiao = 0;
                    if (chkPesquisaRegiao.Checked)
                {
                    iIDRegiao = int.Parse(cbxRegiao.SelectedValue.ToString());
                }
               
                    
                DataSet dspessoa = con.RetornaDadosPessoa(txtBairro.Text, iIDRegiao);
                if (dspessoa.Tables[0].Rows.Count>0)
                {
                    GridView.DataSource = null;
                    GridView.DataSource = dspessoa;
                    GridView.DataMember = "Pessoa";
                    GridView.AutoGenerateColumns = true; 
                }
                else
                {
                    MessageBox.Show("Sem Registros com filtro Informado");
                    GridView.DataSource = null;
                    GridView.DataMember = "";
                     GridView.AutoGenerateColumns = true;
                }

                for (int i = 0; i < GridView.Columns.Count; i++)
                {
                    if (GridView.Columns[i].HeaderText == "Codigo"|| GridView.Columns[i].HeaderText == "CodRegiao")
                    {
                        GridView.Columns[i].ReadOnly = true;
                    }
                }
            }
        }

        private void ExecutarAlteracoes(object sender, EventArgs e)
        {
            if (Utils.MessageBoxQuestion("Confirma alteração de "+ GridView.Rows.Count.ToString() +" Clientes?"))
            {
                for (int i = 0; i < GridView.Rows.Count; i++)
                {
                    AlteracaoCliente newCliente = new AlteracaoCliente();
                    if (txtNewBairro.Text != "")
                    {
                        newCliente.Bairro = txtNewBairro.Text;
                    }
                    else
                    {
                        newCliente.Bairro = GridView.Rows[i].Cells["Bairro"].Value.ToString();
                    }
                    if (cbxNewRegiao.SelectedValue.ToString() != "" && chkNovaRota.Checked)
                    {
                        newCliente.CodRegiao = int.Parse(cbxNewRegiao.SelectedValue.ToString());

                    }
                    else
                    {
                        newCliente.CodRegiao = int.Parse(GridView.Rows[i].Cells["CodRegiao"].Value.ToString());
                    }
                    newCliente.Cidade = GridView.Rows[i].Cells["Cidade"].Value.ToString();
                    newCliente.Nome = GridView.Rows[i].Cells["Nome"].Value.ToString();
                    newCliente.Telefone = GridView.Rows[i].Cells["Telefone"].Value.ToString();
                    newCliente.Codigo = int.Parse(GridView.Rows[i].Cells["Codigo"].Value.ToString());

                    con.Update("spAlterarMultiploPessoa", newCliente);
                }

                GridView.DataSource = null;
                GridView.DataMember = null;
                GridView.AutoGenerateColumns = false;
            }
            
        }

        private void cbxNewRegiao_Click(object sender, EventArgs e)
        {
            this.cbxNewRegiao.DataSource = con.SelectAll("RegiaoEntrega", "spObterRegioes").Tables["RegiaoEntrega"];
            this.cbxNewRegiao.DisplayMember = "NomeRegiao";
            this.cbxNewRegiao.ValueMember = "Codigo";
        }
    }
}
