using DexComanda.Push;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações.Ações
{
    public partial class frmEnvioPush : Form
    {
        private Conexao con;
        private string strTable;
        public frmEnvioPush()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void BuscarFiltro(object sender, EventArgs e)
        {
            DataSet dsResultado = null;
            try
            {
                if (rbAniversario.Checked)
                {
                    dsResultado = con.SelectObterAniversariantes("spObterAnivesariantesPush",
                       dtInicio.Value,
                       dtFim.Value);
                }
                else if (rbSumido.Checked)
                {
                    dsResultado = con.SelectObterClientesSemPedido("spObterClientesSemPedidoPush",
                        dtInicio.Value, 
                        dtFim.Value);
                }
                else if (rbRegiao.Checked)
                {
                    dsResultado = con.SelectRegistroPorCodigo("Pessoa", "spObterClientesPorRegiaoPush",
                        int.Parse(cbxRegiao.SelectedValue.ToString()));
                }
                else if (rbProduto.Checked)
                {
                    dsResultado = con.SelectRegistroPorCodigoPeriodo("Pessoa", "spObterProdutoPorClientePush",
                        cbxGrupo.SelectedValue.ToString(), dtInicio.Value, dtFim.Value);
                }
                else if (rbOrigemCadastro.Checked)
                {
                    dsResultado = con.SelectRegistroPorCodigo("Pessoa", "spObterPessoa_OrigemCadastroPush", int.Parse(cbxOrigemCadastro.SelectedValue.ToString()));
                }
                PopulaGrid(dsResultado,"Pessoa");
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        private void PopulaGrid(DataSet ds,string table)
        {
            try
            {
                if (ds.Tables[0].Rows.Count==0)
                {
                    MessageBox.Show(Bibliotecas.cFiltroRetornaVazio);
                    return;
                }
                gridResultado.DataSource = null;
                gridResultado.AutoGenerateColumns = true;
                gridResultado.DataSource = ds;
                gridResultado.DataMember = table;

                for (int i = 0; i < gridResultado.Columns.Count; i++)
                {
                    if (gridResultado.Columns[i].HeaderText != "Nome" )
                    {
                        gridResultado.Columns[i].Visible = false;
                    }
                }
                lblNumero.Text = gridResultado.Rows.Count.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
           
        }

        private void rbRegiao_CheckedChanged(object sender, EventArgs e)
        {
            grpPeriodo.Enabled =! rbRegiao.Checked;
            Utils.MontaCombox(cbxRegiao, "NomeRegiao", "Codigo", "RegiaoEntrega", "spObterRegioes");
        }

        private void rbProduto_CheckedChanged(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxGrupo, "Nome", "Codigo", "Grupo", "spObterGrupoAtivo");
        }
        private string ModoEntrega()
        {
            string strModoEntrega = "";
            if (rbControlado.Checked)
            {
                strModoEntrega = "last-active";
            }
            else
            {
                return "";
            }

            return "\"delayed_option\": \"" + strModoEntrega + "\" ,";
        }
        private void EnviarPush(object sender, EventArgs e)
        {
            try
            {
                if (txtTextoMsg.Text == string.Empty)
                {
                    MessageBox.Show("Preencha o texto que deseja enviar na mensagem");
                    return;
                }
                if (rbTodos.Checked)
                {
                    OneSignal on = new OneSignal();
                    on.EnviaNotificacao(txtTitulo.Text, txtTextoMsg.Text,ModoEntrega());
                    return;
                }
                if (gridResultado.Rows.Count==0)
                {
                    MessageBox.Show("Primeiro selecione os clientes a enviar");
                    return;
                }
                if (Utils.MessageBoxQuestion(Bibliotecas.cTextoConferido))
                {
                    OneSignal on = new OneSignal();
                    List<string> listaId = new List<string>();
                    for (int i = 0; i < gridResultado.Rows.Count; i++)
                    {
                        listaId.Add("\""+gridResultado.Rows[i].Cells["user_id"].Value.ToString()+"\"");
                    }
                    on.EnviaNotificacao(txtTitulo.Text ,txtTextoMsg.Text, listaId, ModoEntrega());
                }
            }
            catch (Exception erros)
            {
                MessageBox.Show(Bibliotecas.cException + erros.Message);
            }
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            grpPeriodo.Enabled = !rbTodos.Checked;
        }

        private void rbProduto_CheckedChanged_1(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxGrupo, "NomeGrupo", "Codigo", "Grupo", "spObterGrupoAtivo");
        }
        private void rbOrigemCadastro_CheckedChanged(object sender, EventArgs e)
        {
            grpPeriodo.Enabled = !rbOrigemCadastro.Checked;
            Utils.MontaCombox(cbxOrigemCadastro, "Nome", "Codigo", "Pessoa_OrigemCadastro", "spObterPessoa_OrigemCadastro");
        }
    }
}
