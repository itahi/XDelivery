using DexComanda.Relatorios.Gerenciais.Cristal;
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
    public partial class frmConsultaEstoque : Form
    {
        private Conexao con;
        public frmConsultaEstoque()
        {
            InitializeComponent();
        }

        private void frmConsultaEstoque_Load(object sender, EventArgs e)
        {
            con = new Conexao();
        }

        
        private void ListaGrupos(object sender, EventArgs e)
        {
            try
            {
                Utils.MontaCombox(cbxGrupo, "NomeGrupo", "Codigo", "Grupo", "spObterGrupoAtivo");
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void btnImprmir_Click(object sender, EventArgs e)
        {
            try
            {
                RelEstoque rel = new RelEstoque();
                crystalReportViewer1.ReportSource = Utils.GerarReportCodigo(rel, int.Parse(cbxGrupo.SelectedValue.ToString()));
                crystalReportViewer1.Refresh();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            

        }

       
    }
}
