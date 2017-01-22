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
                    dsResultado = con.SelectObterAniversariantes("spObterAnivesariantesPush",   dtInicio.Value, dtFim.Value);
                }
                else if (rbSumido.Checked)
                {
                    dsResultado = con.SelectObterClientesSemPedido("spObterClientesSemPedidoPush", dtInicio.Value, dtFim.Value);
                }
                else if (rbRegiao.Checked)
                {
                    dsResultado = con.SelectRegistroPorCodigo("Pessoa", "spObterClientesPorRegiaoPush",
                        int.Parse(cbxRegiao.SelectedValue.ToString()));
                }
                else if (rbProduto.Checked)
                {
                    dsResultado = con.SelectRegistroPorCodigoPeriodo("ItemsPedido", "spObterProdutoPorClientePush",
                        cbxGrupo.SelectedValue.ToString(), dtInicio.Value, dtFim.Value);
                }
                PopulaGrid(dsResultado);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void PopulaGrid(DataSet ds)
        {
            try
            {
                gridResultado.DataSource = null;
                gridResultado.AutoGenerateColumns = true;
                gridResultado.DataSource = ds;
                gridResultado.DataMember = "Pessoa";
                gridResultado.Refresh();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
           
        }

        private void rbRegiao_CheckedChanged(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxRegiao, "NomeRegiao", "Codigo", "RegiaoEntrega", "spObterRegioes");
        }

        private void rbProduto_CheckedChanged(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxGrupo, "Nome", "Codigo", "Grupo", "spObterGrupoAtivo");
        }
    }
}
