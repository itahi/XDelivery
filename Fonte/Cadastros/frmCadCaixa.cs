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
            CaixaCadastros cx = new CaixaCadastros()
            {
                Data = DateTime.Now,
                Nome = txtNome.Text,
                Numero = txtNum.Text
            };

            con.Insert("spAdicionarCaixa", cx);
            Utils.ControlaEventos("Cadastro", this.Name);
            Utils.LimpaForm(this);
        }
    }
}
