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
    public partial class frmHistoricoCancelamento : Form
    {
        Conexao con;
        public int CodMotivo;
        public string ObsCancelamento;
        public frmHistoricoCancelamento()
        {
            InitializeComponent();
            con = new Conexao();
            this.cbxMotivo.DataSource = con.SelectAll("MotivoCancelamento", "spObterMotivoCancelamento").Tables["MotivoCancelamento"];
            this.cbxMotivo.DisplayMember = "Nome";
            this.cbxMotivo.ValueMember = "Codigo";
        }

        private void frmHistoricoCancelamento_Load(object sender, EventArgs e)
        {

        }

        private void Salvar(object sender, EventArgs e)
        {
            if (cbxMotivo.Text != "")
            {
                this.DialogResult = DialogResult.OK;
                CodMotivo         = int.Parse(cbxMotivo.SelectedValue.ToString());
                ObsCancelamento   = txtObservacao.Text;
                this.Close();
            }
        }

        private void frmHistoricoCancelamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                Salvar(sender, e); 
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
