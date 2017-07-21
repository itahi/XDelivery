using Microsoft.SqlServer.Management.Smo;
using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DexComanda.Operações
{
    public partial class frmConectaBanco : Form
    {
        private DataTable servers;
        public frmConectaBanco()
        {
            InitializeComponent();
        }

        private void ListaServidores(object sender, EventArgs e)
        {
            try
            {
                string strNomePc = Utils.RetornaNomePc();
                servers = SqlDataSourceEnumerator.Instance.GetDataSources();
                cbxServidor.Items.Clear();
                for (int i = 0; i < servers.Rows.Count; i++)
                {
                    if ((servers.Rows[i]["InstanceName"] as string) != null)
                        cbxServidor.Items.Add(servers.Rows[i]["ServerName"] + "\\" + servers.Rows[i]["InstanceName"]);
                    else
                        cbxServidor.Items.Add(servers.Rows[i]["ServerName"]);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }


        }

        private void ListBancoDados(object sender, EventArgs e)
        {
            //if (cbxServidor.SelectedIndex<0)
            //{
            //    MessageBox.Show("Selecione antes o Servidor");
            //    cbxServidor.Focus();
            //    return;
            //}
            //for (int i = 0; i < servers.Rows.Count; i++)
            //{
            //    cbxServidor.Items.Add(servers.Rows[i].);
            //}
        }

        private void ConectaBancoDeDados(object sender, EventArgs e)
        {
            try
            {
                if (cbxServidor.SelectedIndex < 0)
                {
                    MessageBox.Show("Selecione antes o Servidor");
                    cbxServidor.Focus();
                    return;
                }
                if (cbxListaBanco.SelectedIndex < 0)
                {
                    MessageBox.Show("Selecione antes o Servidor");
                    cbxListaBanco.Focus();
                    return;
                }
                Conexao con = new Conexao();
                if (con.OpenConection(cbxServidor.Text, cbxListaBanco.Text))
                {
                    frmConfiguracoes frm = new frmConfiguracoes();
                    frm.txtBanco.Text = cbxListaBanco.Text;
                    frm.txtServidor.Text = cbxServidor.Text;
                    frm.Show();
                    //Utils.Restart();
                }

            }
            catch (SqlException erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void cbxListaBanco_DropDown(object sender, EventArgs e)
        {
            if (cbxServidor.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione antes o Servidor");
                cbxServidor.Focus();
                return;
            }
            Server srv;
            srv = new Server(cbxServidor.Text.ToString());

            cbxListaBanco.Items.Clear();
            foreach (Database db in srv.Databases)
            {
                cbxListaBanco.Items.Add(db.Name);
            }
        }
    }
}
