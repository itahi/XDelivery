﻿//using HumanAPIClient.Model;
//using HumanAPIClient.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Web;
using HumanAPIClient.Service;
using HumanAPIClient.Model;
using DexComanda.Models;
using DexComanda;
namespace DexComanda
{
    public partial class frmEnvioSms : Form
    {
        //private SimpleSending cliente;
        //private SimpleMessage mensagem;
        private Conexao con;
        private DateTime DataInicial;
        private DateTime DataFinal;
        public string RetornoServ;
        private string NomeEmpresa;
        private string NomeCliente;
        private int TotalSelecionado;

        public frmEnvioSms()
        {
             
            InitializeComponent();
            con = new Conexao();
            NomeEmpresa = Sessions.returnEmpresa.Nome;

        }

        private void frmEnvioSms_Load(object sender, EventArgs e)
        {
            // MultipleSending Cliente = new MultipleSending("xsistemas", "XJzaDYXQ");
            lblRestante.Text = Convert.ToString(150 - NomeEmpresa.ToString().Length);

        }

        private void EnviarSMS(object sender, EventArgs e)
        {
                
            this.Cursor = Cursors.WaitCursor;
            if (rbAniversariantes.Checked == false && rbSemPedidos.Checked == false && txtMensagem.Text != "")
                {
                    MessageBox.Show("Selecione primeiro para que grupo enviará as mensagems", "Aviso XCommanda");
                }
                else
                {
                    if (rbAniversariantes.Checked)
                    {
                        if (txtDataFinal.Text.Replace("  /", "") != "" || txtDataFinal.Text.Replace("  /", "") != "")
                        {

                            lbl.Text= Convert.ToString(Utils.ClientesAniversariantes(txtDataInicial.Text, txtDataFinal.Text, txtMensagem.Text,cbxPorta.Text));
                        }
                        else
                        {
                            MessageBox.Show("Preencha o periodo para enviar", "Aviso XCommada");
                        }

                    }
                    else if (rbSemPedidos.Checked)
                    {
                        lbl.Text= Convert.ToString(Utils.ClientesSemPedidos(txtDataInicial.Text, txtDataFinal.Text, txtMensagem.Text, cbxPorta.Text));
                    }
                    else if (chkTodosClientes.Checked)
                    {
                        
                    }

                }

            lbl.Text = Convert.ToString(TotalSelecionado);
            this.Cursor = Cursors.Default;
        }
            
        

        private void SelecionarClientesEnvio(object sender, EventArgs e)
        {
            
        }


      

        private void ContadorCaracters(object sender, EventArgs e)
        {
           
        }

        private void txtMensagem_KeyDown(object sender, KeyEventArgs e)
        {
            int TotalCaracteres = 150 - NomeEmpresa.Length;
            int TotalContado = txtMensagem.Text.Length;

            if (txtMensagem.Text !="")
            {
                lblRestante.Text = Convert.ToString(TotalCaracteres - TotalContado);  
            }
            
        }
        private string AdicionaNomeCliente(string iMensagem)
        {
            if (iMensagem.Contains("@Cliente"))
            {
                iMensagem = iMensagem.Replace("@Cliente", NomeCliente);
            }

            return iMensagem;
        }

        private void chkTodosClientes_CheckedChanged(object sender, EventArgs e)
        {

        }
 

    }
}
