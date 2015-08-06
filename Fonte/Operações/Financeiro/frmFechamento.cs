using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações.Financeiro
{
    public partial class frmCaixaFechamento : Form
    {
        private Conexao con;
        public frmCaixaFechamento()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void frmCaixaFechamento_Load(object sender, EventArgs e)
        {
            cbxCaixas.DataSource = con.SelectAll("Caixa", "spObterDadosCaixa").Tables["cbxCaixas"];
            cbxCaixas.DisplayMember = "Numero";
            cbxCaixas.ValueMember = "Codigo";
        }

        private void FiltraCaixa(object sender, EventArgs e)
        {
            DataSet dsCaixa = con.SelectRegistroPorCodigo("Caixa", "spObterDadosCaixaPorCodigo", int.Parse(cbxCaixas.SelectedValue.ToString()));
            if (dsCaixa.Tables[0].Rows.Count>0)
            {
                DataRow dRow = dsCaixa.Tables[0].Rows[0];

                txtDtAbertura.Text   = dRow.ItemArray.GetValue(1).ToString();
               // txtVlrAbertura.Text  
            }
        }
    }
}
