using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DexComanda.Models;
namespace DexComanda
{
    public partial class frmEntregador : Form
    {
        Conexao con;
        //private Entregador entregador;
        public frmEntregador()
        {
            InitializeComponent();

            con = new Conexao();
            Utils.PopularGrid("Entregador", EntregadoresGridView, "spObterEntregadores");
        }
        private void Reset()
        {
            txtNome.Text = "";
            txtComissao.Text = "";
        }

        private void Salvar(object sender, EventArgs e)
        {
            try
            {
                Entregador entregador = new Entregador()
                {
                    Nome = txtNome.Text.ToString(),
                    Comissao = Convert.ToDecimal(txtComissao.Text),
                };
                con.Insert("spAdicionarEntregador", entregador);
                Utils.PopularGrid("Entregador", EntregadoresGridView, "spObterEntregadores");
                MessageBox.Show("Entregador Salvo com sucesso ! ");
                Reset();

            }
            catch (Exception)
            {
                MessageBox.Show("Registro não foi gravado , favor verificar os campos digitados ");

            }

        }

        private void frmEntregador_Load(object sender, EventArgs e)
        {

        }
    }
}
