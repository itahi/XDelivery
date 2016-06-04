using DexComanda.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda
{
    public partial class frmCadastroEmailSMS : Form
    {
        private Conexao con;
        public frmCadastroEmailSMS()
        {
            InitializeComponent();
            con = new Conexao();
        }

        private void frmCadastroEmailSMS_Load(object sender, EventArgs e)
        {

        }

        private void SalvarMsgNiver(object sender, EventArgs e)
        {
            if (txtMsgAniversariante.Text=="")
            {
                Mensagens msg = new Mensagens()
                {
                    Tipo = "AN",
                    Conteudo = txtMsgAniversariante.Text
                };
                con.Insert("spAdicionarMensagen", msg);
                MessageBox.Show("Mensage alterada/adicionada com sucesso", "Cadastro OK");
            }
            else
            {
                Mensagens msg = new Mensagens()
                {
                    Codigo = 1,
                    Conteudo = txtMsgAniversariante.Text
                };
                con.Update("spAlterarMensagens",msg);
                MessageBox.Show("Mensage alterada/adicionada com sucesso", "Cadastro OK");
            }
        }

        private void SalvarSemPedido(object sender, EventArgs e)
        {
            if (txtMsgClienteSemPedido.Text=="")
            {
                Mensagens msg = new Mensagens()
                {
                    Tipo = "SP",
                    Conteudo = txtMsgClienteSemPedido.Text
                };
                con.Insert("spAdicionarMensagen", msg);
                MessageBox.Show("Mensage alterada/adicionada com sucesso", "Cadastro OK");
            }
        }

        private void brnSalvarMelhorCliente_Click(object sender, EventArgs e)
        {
            if (txtMsgMelhorCliente.Text == "")
            {
                Mensagens msg = new Mensagens()
                {
                    Tipo = "MC",
                    Conteudo = txtMsgMelhorCliente.Text
                };
                con.Insert("spAdicionarMensagen", msg);
                MessageBox.Show("Mensage alterada/adicionada com sucesso", "Cadastro OK");
            }
        }
    }
}
