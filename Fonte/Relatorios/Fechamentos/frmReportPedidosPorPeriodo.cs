﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DexComanda.Relatorios;
using Microsoft.ReportingServices.Interfaces;
using DexComanda.Models;
using Microsoft.Reporting.WinForms;


namespace DexComanda.Relatorios
{
    public partial class frmReportPedidosPorPeriodo : Form
    {

        private Conexao con;
        private string TelefoneCliente;
        private List<Pedido> listaDePedidos;
        private DataSet ds;
        private DataView dv;
        private string DataAtual;

        public frmReportPedidosPorPeriodo()
        {
            try
            {
                DataAtual = DateTime.Now.ToShortDateString();
                con = new Conexao();
                listaDePedidos = new List<Pedido>();
                InitializeComponent();
                dataInicio.Value = DateTime.Now;
                dataFim.Value = DateTime.Now.AddDays(1);
                
            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }
        }

        private void frmSelecionarCliente_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBExpertDataSet.Pedido' table. You can move, or remove it, as needed.
          //  con = new Conexao();
            try
            {
               // this.pedidoTableAdapter.Fill(this.dBExpertDataSet.Pedido);
                this.cbxFormaPagamento.DataSource = con.SelectAll("FormaPagamento", "spObterFormaPagamento").Tables["FormaPagamento"];
                this.cbxFormaPagamento.DisplayMember = "Descricao";
                this.cbxFormaPagamento.ValueMember = "Codigo";

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
           
        }

   
        private void clientesGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.rpvPedidoPorPessoa.RefreshReport();
        }


        private void dataFim_ValueChanged(object sender, EventArgs e)
        {

            //VisualisarRelatarioPedidoPorPessoa();
        }

        public void Consultar(object sender, EventArgs e)
        {
            try
            {
                DataSet pessoa = null;
                DataRow clienteRow = null;

                if (!txtTelefoneCliente.Text.Equals(""))
                {
                    TelefoneCliente = txtTelefoneCliente.Text;
                    //Consulta Cliente
                    pessoa = con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", TelefoneCliente);
                    clienteRow = pessoa.Tables["Pessoa"].Rows[0];
                }

                //Faz a consulta dos Pedidos
                ds = con.SelectAll("Pedido", "spObterPedidoFinalizado");

                //Pega a view padrão da tabela
                dv = ds.Tables[0].DefaultView;


                string query = "";
                var dtInicio = Convert.ToDateTime(dataInicio.Value.ToShortDateString() + " 00:00:00");
                var dtFim = Convert.ToDateTime(dataFim.Value.ToShortDateString() + " 23:59:59");
                if (!txtTelefoneCliente.Text.Equals(""))
                {
                    query = "CodPessoa = " + clienteRow["Codigo"] + " AND RealizadoEM >= '" + dtInicio + "' AND RealizadoEM <='" + dtFim + "' ";
                }
                else
                {
                    query = "RealizadoEM >= '" + dtInicio + "' AND RealizadoEM <='" + dtFim + "' ";
                }

                if (chkCancelados.Checked)
                {
                    ds = con.SelectAll("Pedido", "spObterPedidoCancelado");
                    dv = ds.Tables[0].DefaultView;
                    string PedidoCancelados = "Cancelado";
                    query = query + "AND [status]='" + PedidoCancelados + "'";

                }

                if (cbxFormaPagamento.Text != "")
                {
                    query = query + " AND FormaPagamento='" + cbxFormaPagamento.Text + "'";
                    dv.RowFilter = query;
                    //  dv.Sort = "Group by";
                }
                else
                {
                    dv.RowFilter = query;
                    dv.Sort = "Codigo ASC";
                }
                if (dv.Count > 0)
                {
                    //Override do datasource setado por padrão no frmSelecionarCliente.Designer.cs
                    this.vwObterPedidosFinalizadosBindingSource.DataSource = dv;

                    //Preenche o adapter e atualiza o reportview
                    //this.pedidoTableAdapter.Fill(this.dBExpertDataSet.Pedido);
                    this.rpvPedidoPorPessoa.RefreshReport();

                    ds = null;
                    dv = null;
                    txtTelefoneCliente.Text = "";

                }
                else
                {
                    MessageBox.Show("Nenhum pedido encontrado no filtro preenchido","Dex Aviso");
                    //clientesGridView.DataSource = null;
                    this.rpvPedidoPorPessoa.Clear();
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possivel gerar o relátório" + erro.Message," Dex Aviso", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void pedidoBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }


    }
}
