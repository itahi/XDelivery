using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Relatorios.Fechamentos
{
    public partial class frmReportEntregasPorMotoboy : Form
    {
        private Conexao con;
        private int intCodMotoboy = 0;
        public frmReportEntregasPorMotoboy()
        {
            InitializeComponent();
            con = new Conexao();

            dataInicio.Value = DateTime.Now;
            dataFim.Value = DateTime.Now.AddDays(1);
        }

        private void frmReportEntregasPorMotoboy_Load(object sender, EventArgs e)
        {
            cbxEntregador.DataSource = con.SelectAll("Entregador", "spObterEntregadores").Tables["Entregador"];
            cbxEntregador.DisplayMember = "Nome";
            cbxEntregador.ValueMember = "Codigo";
        }

        private void Consultar(object sender, EventArgs e)
        {

            DataSet dsEntregas = con.SelectEntregaPorBoy(dataInicio.Value.ToShortDateString() + " 00:00:00", dataFim.Value.ToShortDateString() + " 23:59:59", int.Parse(cbxEntregador.SelectedValue.ToString()));
            if (dsEntregas.Tables[0].Rows.Count > 0)
            {
                DataView dvEntregas = dsEntregas.Tables[0].DefaultView;
                spEntregasPorBoyDataBindingSource.DataSource = dvEntregas;
              //  reportViewEntregas.RefreshReport();

            }
            else
            {
                MessageBox.Show("Não há registros com o filtro informado", "[XSistemas]");
            }
        }

        private void cbxEntregador_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cbxEntregador_Click(object sender, EventArgs e)
        {
            cbxEntregador.DataSource = con.SelectAll("Entregador", "spObterEntregadores").Tables["Entregador"];
            cbxEntregador.DisplayMember = "Nome";
            cbxEntregador.ValueMember = "Codigo";

            if (cbxEntregador.SelectedValue.ToString() != null)
            {
                intCodMotoboy = int.Parse(cbxEntregador.SelectedValue.ToString());
            }
           
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodos.Checked)
            {
                cbxEntregador.Enabled = false;
                intCodMotoboy = 0;
            }
            else
            {
                cbxEntregador.Enabled = true;
                intCodMotoboy = int.Parse(cbxEntregador.SelectedValue.ToString());
            }
        }
    }
}
