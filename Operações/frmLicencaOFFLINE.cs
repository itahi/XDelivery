﻿using System;
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

    public partial class frmLicencaOFFLINE : Form
    {
        private string DataAtual = DateTime.Now.ToShortDateString();
        private string CNPJRetorno = Sessions.returnEmpresa.CNPJ;
        private static Conexao con;
        private string DataHoraGeracao;
        private string AnoAtual;
        public frmLicencaOFFLINE()
        {
            InitializeComponent();
            con = new Conexao();
            DataHoraGeracao = DateTime.Now.ToShortDateString().Replace("/", "").Replace(":", "").Replace(" ", "");
          //  DiaAtual = DataAtual.ToString().Substring(0, 2);
            AnoAtual = DataAtual.ToString().Substring(6, 4);

            txtSenha.Text = CNPJRetorno + DataHoraGeracao;
        }

        private void frmLicencaOFFLINE_Load(object sender, EventArgs e)
        {
            // DataAtual.ToString().Substring(1, 2) + DataAtual.ToString().Substring(6, 9) +
        }

        private void ValidaChave(object sender, EventArgs e)
        {
            try
            {
                if (txtContraSenha.Text != "")
                {
                    if (Utils.GeraRetornaContraSenha(txtSenha.Text) == txtContraSenha.Text.ToUpper())   
                    {
                        BloqueiaUso Empresa = new BloqueiaUso()
                        {
                            VersaoBanco = "0",
                            DataInicio =DateTime.Now

                        };
                        con.Update("spAltera", Empresa);

                        
                        Utils.GravaRegistro(Utils.GeraRetornaContraSenha(txtSenha.Text) + DataHoraGeracao);
                        MessageBox.Show("Licença liberada", "Liberação OffLine");
                        this.Close();
                        Utils.Restart();
                    }
                    else
                    {
                        MessageBox.Show("Contra senha não é válida para sua licença", "Aviso");
                    }

                }
                else
                {
                    MessageBox.Show("Contra senha deve ser preenchida", "Aviso");
                    txtContraSenha.Focus();
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message, "DEX [ ERRO]");
            }
        }

    }
}
