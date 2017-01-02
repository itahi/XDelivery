using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Cadastros.Pessoa
{
    public partial class frmSelecionaEndereco : Form
    {
        private int intCodPessoa;
        private Conexao con;
        public int intCodEndereco;
        public frmSelecionaEndereco()
        {
            InitializeComponent();
        }
        public frmSelecionaEndereco(int iCodPessoa)
        {
            try
            {
                InitializeComponent();
                con = new Conexao();
                intCodPessoa = iCodPessoa;
                Utils.MontaCombox(cbxEnderecos, "EnderecoCompleto", "Codigo", "Pessoa_Endereco", "spObterEnderecoCompletoPessoa", iCodPessoa);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            
        }
        private void frmSelecionaEndereco_Load(object sender, EventArgs e)
        {
           
        }

        private void ConfirmaSeleção(object sender, EventArgs e)
        {
            intCodEndereco = int.Parse(cbxEnderecos.SelectedValue.ToString());
            this.Close();
        }
    }
}
