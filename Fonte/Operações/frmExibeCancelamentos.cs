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
    public partial class frmExibeCancelamentos : Form
    {
        private Conexao con;
        private int intCodPessoa;
        private DataSet dsCancelamentos;
        private int i;
        public frmExibeCancelamentos(int iCodPessoa)
        {
            con = new Conexao();
            InitializeComponent();
            intCodPessoa = iCodPessoa;
        }

        private void frmExibeCancelamentos_Load(object sender, EventArgs e)
        {
            dsCancelamentos = con.SelectRegistroPorCodigo("HistoricoCancelamentos", "spObterCancelamentoPorPessoa", intCodPessoa);
            CancGridView.DataSource = null;
            CancGridView.AutoGenerateColumns = true;
            CancGridView.DataSource = dsCancelamentos;
            CancGridView.DataMember = "HistoricoCancelamentos";

        }

        private void MostraObservacoes(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (CancGridView.Rows.Count > 0)
            //{

            //    for (int i = 0; i < CancGridView.Rows.Count; i++)
            //    {
            //        txtObservacao.Text = CancGridView.Rows[i].Cells[].Value.ToString();
            //    }
            //}
            
            
        }
    }
}
