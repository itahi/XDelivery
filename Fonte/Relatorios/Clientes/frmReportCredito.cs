using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Relatorios.Clientes
{
    public partial class frmReportCredito : Form
    {
        private RelCreditoDebito rel;
        public frmReportCredito(int iCodPessoa , DateTime iDtInici, DateTime idtFim)
        {
            InitializeComponent();
            rel = new RelCreditoDebito();
            rel.Parameter_CodPessoa.Attributes.Add("@CodPessoa", iCodPessoa);
            rel.Parameter_DataInicio.Attributes.Add("@DataInicio", iDtInici);
            rel.Parameter_DataFim.Attributes.Add("@DataFim", idtFim);
            rel.PrintToPrinter(0, true, 0, 0);
        }

        private void frmReportCredito_Load(object sender, EventArgs e)
        {

        }
    }
}
