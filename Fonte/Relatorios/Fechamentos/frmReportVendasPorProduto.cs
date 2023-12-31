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

namespace DexComanda.Relatorios
{
    public partial class frmReportVendasPorProduto : Form
    {
        private Conexao con;
        private List<ItemPedido> itemsdoPedido;
        private DataSet ds;
        private DataView dv;
        private DateTime DataInicio;
        private DateTime DataFim;
        private string query;
        public frmReportVendasPorProduto()
        {
            InitializeComponent();
            con = new Conexao();
            itemsdoPedido = new List<ItemPedido>();
            txtdtInicio.Value = DateTime.Now;
            txtdtFim.Value    = DateTime.Now;
        }

        private void frmReportVendas_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsRelatorio.vwObterPedidosFinalizados' table. You can move, or remove it, as needed.
           // this.vwObterItemsVendidosTableAdapter.Fill(this.dsRelatorio.vwObterPedidosFinalizados);

        }

        private void GeraReport(object sender, EventArgs e)
        {
            try
            {
                // Faz a Consulta
                DataInicio = Convert.ToDateTime(txtdtInicio.Value.ToShortDateString() + " 00:00:00.000");
                DataFim = Convert.ToDateTime(txtdtFim.Value.ToShortDateString() + " 23:59:59.999");
                ds = con.SelectObterItemsVendidos("ItemsPedido", "vwObterItemsVendidos", DataInicio, DataFim);

                query = "RealizadoEM >= '" + DataInicio + "' AND RealizadoEM <='" + DataFim + "' ";
               // Pega a view padrão da tabela
               dv = ds.Tables[0].DefaultView;
               dv.RowFilter = query;

               if (dv.Count > 0)
               {
                   this.vwObterItemsVendidosBindingSource.DataSource = dv;
                   this.rptProdutosVenda.RefreshReport();

               }
               else
               {
                   MessageBox.Show("Não há pedidos no Periodo selecionado","Aviso");
               }
                                
            }
            catch (Exception t)
            {

                MessageBox.Show(t.Message);
            }
        }
    }
}
