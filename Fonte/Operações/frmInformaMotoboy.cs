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
    public partial class frmInformaMotoboy : Form
    {
        private Conexao con;
        public int CodMotoboy;

        public frmInformaMotoboy()
        {
            InitializeComponent();
        }

        private void frmInformaMotoboy_Load(object sender, EventArgs e)
        {
            con = new Conexao();

            this.cbxListaMotoboy.DataSource = con.SelectAll("Entregador", "spObterEntregadores").Tables["Entregador"];
            this.cbxListaMotoboy.DisplayMember = "Nome";
            this.cbxListaMotoboy.ValueMember = "Codigo";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cbxListaMotoboy.Text != "")
            {
                this.DialogResult = DialogResult.OK;
                CodMotoboy = int.Parse(cbxListaMotoboy.SelectedValue.ToString());
                this.Close();
            }
        }
    }
}
