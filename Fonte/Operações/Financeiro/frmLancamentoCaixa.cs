﻿using DexComanda.Models;
using DexComanda.Relatorios.Caixa;
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
    public partial class frmLancamentoCaixa : Form
    {
        private Conexao con;
        public frmLancamentoCaixa()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void frmLancamentoCaixa_Load(object sender, EventArgs e)
        {
            cbxTurno.SelectedIndex = 0;
            DataSet dsCaixas = con.SelectAll("Caixa", "spObterCaixa");
            cbxCaixas.DataSource = dsCaixas.Tables["Caixa"];
            cbxCaixas.DisplayMember = "Numero";
            cbxCaixas.ValueMember = "Codigo";


            cbxFormaPagamento.DataSource = con.SelectAll("FormaPagamento", "spObterFormaPagamento").Tables["FormaPagamento"];
            cbxFormaPagamento.DisplayMember = "Descricao";
            cbxFormaPagamento.ValueMember = "Codigo";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        private void frmLancamentoCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                Salvar(sender, e);
            }

            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Salvar(object sender, EventArgs e)
        {
            try
            {
                int intNumCaixa = 1;
                string strTipoMovimento = "";
                if (txtDescricao.Text == "" || txtSolicitante.Text == "" || txtValor.Text == "")
                {
                    MessageBox.Show(Bibliotecas.cCamposObrigatório);
                    return;
                }
                DateTime Dt = Convert.ToDateTime(dtMovimento.Value.ToShortDateString());
                if (cbxCaixas.SelectedText.ToString() != "")
                {
                    intNumCaixa = int.Parse(cbxCaixas.SelectedText.ToString());
                }
                DataSet ds = con.RetornaCaixaPorTurno(intNumCaixa, cbxTurno.Text, Dt);
                if (Convert.ToBoolean(ds.Tables[0].Rows[0].ItemArray.GetValue(7)))
                {
                    MessageBox.Show(Bibliotecas.cCaixaFechado);
                    return;
                }
                if (Utils.CaixaAberto(Dt, intNumCaixa, Sessions.retunrUsuario.Turno))
                {
                    CaixaMovimento cxMovimento = new CaixaMovimento()
                    {
                        //CodCaixa = intNumCaixa,
                        CodFormaPagamento = int.Parse(cbxFormaPagamento.SelectedValue.ToString()),
                        //Data = dtMovimento.Value,
                        CodUser = Sessions.retunrUsuario.Codigo,
                        Historico = txtDescricao.Text,
                        NumeroDocumento = txtDocumento.Text,
                        Turno = cbxTurno.Text,
                       // Valor = decimal.Parse(txtValor.Text)
                    };
                    if (intNumCaixa.ToString() == "" || txtValor.Text == "")
                    {
                        MessageBox.Show(Bibliotecas.cCamposObrigatório);
                        return;
                    }
                    if (rbEntrada.Checked)
                    {
                        cxMovimento.Tipo = 'E';
                        strTipoMovimento = "de entrada";
                        cxMovimento.Valor = decimal.Parse(txtValor.Text);
                    }
                    else if (rbSaida.Checked)
                    {
                        strTipoMovimento = "de saida";
                        cxMovimento.Tipo = 'S';
                        cxMovimento.Valor = -decimal.Parse(txtValor.Text);
                    }
                    else
                    {
                        MessageBox.Show("Selecione o tipo de movimento", "[xSistemas] Segurança");
                        return;
                    }

                    if (!Utils.MessageBoxQuestion("Deseja lançar um movimento " + strTipoMovimento + " no caixa do turno " +
                        cbxTurno.Text + " ?"))
                    {
                        return;
                    }
                    con.Insert("spInserirMovimentoCaixa", cxMovimento);
                    MessageBox.Show("Movimento lançado", "[xSistemas] Aviso");

                    if (!Utils.MessageBoxQuestion("Deseja imprimir o comprovante do lançamento?"))
                    {
                        return;
                    }
                    if (cxMovimento.Tipo == 'E')
                    {
                        RelSuprimento repor;
                        repor = new RelSuprimento();
                        Utils.ImprimirCaixa(repor, cxMovimento.Valor.ToString(), txtSolicitante.Text);
                    }
                    else
                    {
                        RelSangria repor;
                        repor = new RelSangria();
                        Utils.ImprimirCaixa(repor, cxMovimento.Valor.ToString(), txtSolicitante.Text);
                    }

                    Utils.LimpaForm(this);

                }
                else
                {
                    MessageBox.Show("Caixa do dia " + dtMovimento.Value.ToString() + " já foi fechado , lançamento não permitido", "[Xsistemas] Aviso");

                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }


        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }
    }
}
