using DexComanda.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Cadastros
{
    public partial class frmCadCaixa : Form
    {
        private Conexao con;
        
        public frmCadCaixa()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void frmCadCaixa_Load(object sender, EventArgs e)
        {
         
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                CaixaCadastros cx = new CaixaCadastros()
                {
                    DataCadastro = DateTime.Now,
                    Nome = txtNome.Text,
                    Numero = txtNum.Text
                };

                con.Insert("spAdicionarCaixa", cx);
                Utils.ControlaEventos("Cad", this.Name);
                MessageBox.Show("Caixa cadastrado", "[xSistemas]");
                Utils.LimpaForm(this);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException);
            }
          
        }
    }
}
