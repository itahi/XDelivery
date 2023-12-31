﻿using DexComanda.Models;
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
    public partial class frmFinalizacaoPedido : Form
    {
        private Conexao con;
        public bool bMultiPlasFP = false;
        public bool boolFinalizou = false;
        private Boolean bInserir;
        private frmCadastrarPedido iFrm;
        private int intCodPedido;
        private decimal iTotalSomado = 0;
        private int intCodigoMesa = 0;
        public frmFinalizacaoPedido()
        {
            InitializeComponent();
            con = new Conexao();
        }

        private void frmFinalizacaoPedido_Load(object sender, EventArgs e)
        {

        }
        public frmFinalizacaoPedido(decimal iTotalPedido, Boolean iInserir, int iCodPedido = 0, int iCodMesa = 0)
        {
            try
            {
                InitializeComponent();
                intCodigoMesa = iCodMesa;
                if (iCodMesa==0)
                {
                    lblFinalizacao.Text = "Fechamento Pedido:" + iCodPedido.ToString();
                    lblNumeroMesa.Visible = false;
                }
              
                bInserir = iInserir;
                intCodPedido = iCodPedido;
                lblNumeroMesa.Text = iCodMesa.ToString();
                lblTotalPedido.Text = iTotalPedido.ToString();
                lblFalta.Text = iTotalPedido.ToString();
                con = new Conexao();
                DataSet dsFPagamento = con.SelectFormasPagamento();

                gridFormasPagamento.DataSource = null;
                gridFormasPagamento.AutoGenerateColumns = true;
                gridFormasPagamento.DataSource = dsFPagamento;
                gridFormasPagamento.DataMember = "FormaPagamento";
                if (dsFPagamento.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < gridFormasPagamento.Columns.Count; i++)
                    {
                        if (gridFormasPagamento.Columns["Codigo"].HeaderText == "Codigo")
                        {
                            gridFormasPagamento.Columns["Codigo"].Visible = false;
                        }
                        if (gridFormasPagamento.Columns["Descricao"].HeaderText == "Descricao")
                        {
                            gridFormasPagamento.Columns["Descricao"].ReadOnly = true;
                        }
                    }

                    if (!gridFormasPagamento.Columns.Contains("Valor"))
                    {
                        gridFormasPagamento.Columns.Add("Valor", "Valor R$");
                        gridFormasPagamento.Refresh();
                    }
                    gridFormasPagamento.Refresh();

                }

            }

            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        void TeclaPressionada(object sender, KeyPressEventArgs e)

        {

            if (!char.IsNumber(e.KeyChar) && !char.IsPunctuation(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar)
                && e.KeyChar!=Convert.ToChar(Keys.Back))

                e.Handled = true;
        }
       
        private void Finaliza(object sender, EventArgs e)
        {
            try
            {
                iTotalSomado = 0;
                for (int i = 0; i < gridFormasPagamento.Rows.Count; i++)
                {
                    if (gridFormasPagamento.Rows[i].Cells["Valor"].Value != null)
                    {
                        iTotalSomado = iTotalSomado + decimal.Parse(gridFormasPagamento.Rows[i].Cells["Valor"].Value.ToString());
                    }

                }
                if (iTotalSomado < decimal.Parse(lblTotalPedido.Text))
                {
                    MessageBox.Show("Valor devido é maior que o valor informado");
                    return;
                }
                else if (iTotalSomado > decimal.Parse(lblTotalPedido.Text))
                {
                    MessageBox.Show("Os Valores somados são maiores que o valor do Pedido");
                    return;
                }
                else
                {
                    int intCodPedid = intCodPedido;
                    for (int intFor = 0; intFor < gridFormasPagamento.Rows.Count; intFor++)
                    {
                        if (gridFormasPagamento.Rows[intFor].Cells["Valor"].Value != null
                            && gridFormasPagamento.Rows[intFor].Cells["Valor"].Value.ToString() != "")
                        {
                            FinalizaPedido finalizaPed = new FinalizaPedido()
                            {
                                CodPedido = intCodPedid,
                                CodPagamento = int.Parse(gridFormasPagamento.Rows[intFor].Cells["Codigo"].Value.ToString()),
                                ValorPagamento = decimal.Parse(gridFormasPagamento.Rows[intFor].Cells["Valor"].Value.ToString())
                            };
                            con.Insert("spAdicionarFinalizaPedido_Pedido", finalizaPed);
                        }
                    }
                    boolFinalizou = true;
                    con.SinalizarPedidoConcluido("Pedido", "spSinalizarPedidoConcluido", intCodPedido, int.Parse(txtNumero.Text));

                    if (intCodigoMesa>0)
                    {
                        if (Utils.MessageBoxQuestion("Deseja imprimir a conferencia desta dessa mesa?"))
                        {
                            Utils.ImpressaoFechamentoNovo(intCodPedid, 1);
                        }
                    }
                    
                    this.Close();
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cErroGravacao + erro.Message);
            }


        }

        private void Valida(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl && gridFormasPagamento.Columns.Contains("Valor"))

            {
                for (int i = 0; i < gridFormasPagamento.Rows.Count; i++)
                {
                    if (gridFormasPagamento.Rows[i].Cells["Valor"].Value != null)
                    {
                        e.Control.KeyPress += new KeyPressEventHandler(TeclaPressionada);
                        //   CalculaRestante(decimal.Parse(gridFormasPagamento.Rows[i].Cells["Valor"].Value.ToString()));
                    }

                }

            }
        }
        void CalculaRestante(decimal iValorDigitado)
        {
            lblFalta.Text = Convert.ToString(decimal.Parse(lblTotalPedido.Text) - iValorDigitado);
        }

        private void gridFormasPagamento_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal iValue = 0;
            for (int i = 0; i < gridFormasPagamento.Rows.Count; i++)
            {
                if (gridFormasPagamento.Rows[i].Cells["Valor"].Value != null)
                {
                    iValue = iValue + decimal.Parse(gridFormasPagamento.Rows[i].Cells["Valor"].Value.ToString());
                    CalculaRestante(iValue);
                }
            }

        }

        private void frmFinalizacaoPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                Finaliza(sender, e);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
