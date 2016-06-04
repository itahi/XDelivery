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
using XIntegrador.Classe.Local;

namespace DexComanda.Operações.Pedido
{
    public partial class frmAlterarStatusPedido : Form
    {
        private Conexao con;
        public frmAlterarStatusPedido()
        {
            InitializeComponent();
            con = new Conexao();
        }

        private void FiltrarRegistros(object sender, EventArgs e)
        {
            try
            {
                DateTime dataInicio = Convert.ToDateTime(dtInicio.Value.ToShortDateString() + " 00:00:00");
                DateTime dataFim = Convert.ToDateTime(dtFim.Value.ToShortDateString() + " 23:59:59");
                DataSet ds = con.RetornaPedidosOnline(chkSomenteOnline.Checked, dataInicio, dataFim);
                if (ds.Tables[0].Rows.Count > 0)
                {
                   

                    gridViewPedidos.DataSource = null;
                    gridViewPedidos.DataSource = ds;
                    
                    gridViewPedidos.Refresh();
                    gridViewPedidos.AutoGenerateColumns = true;
                    gridViewPedidos.DataMember = "Pedido";
                    for (int i = 0; i < gridViewPedidos.Columns.Count; i++)
                    {
                        if (gridViewPedidos.Columns[i].HeaderText == "CodPessoa")
                        {
                            gridViewPedidos.Columns[i].Visible = false;
                        }
                    }

                    if (!gridViewPedidos.Columns.Contains("Status"))
                    {
                        DataGridViewComboBoxColumn dtColumn = new DataGridViewComboBoxColumn();
                        dtColumn.Name = "Status";
                        dtColumn.HeaderText = "Situação do Pedido";
                        dtColumn.Items.Add("3 - Na Cozinha");
                        dtColumn.Items.Add("4 - Saiu pra entrega");
                        dtColumn.Items.Add("5 - Entregue");
                        dtColumn.Items.Add("6 - CANCELADO");
                        gridViewPedidos.Columns.Add(dtColumn);
                    }
                    con.Close();
                }
            }
            catch (Exception erro)
            {

                throw;
            }

        }

        private void ExecutarAlteracoes(object sender, EventArgs e)
        {
            if (Utils.MessageBoxQuestion("Deseja executar a alteração em" + gridViewPedidos.Rows.Count + "Pedidos?"))
            {
                for (int i = 0; i < gridViewPedidos.Rows.Count; i++)
                {
                    if (gridViewPedidos.Rows[i].Cells["Codigo"].Value != null)
                    {
                        PedidoStatusMovimento ped = new PedidoStatusMovimento()
                        {
                            CodPedido = int.Parse(gridViewPedidos.Rows[i].Cells["Codigo"].Value.ToString()),
                            CodStatus = int.Parse(gridViewPedidos.Rows[i].Cells["Status"].Value.ToString().Substring(0, 1)),
                            CodUsuario = Sessions.retunrUsuario.Codigo,
                            DataAlteracao = DateTime.Now
                        };
                        if (gridViewPedidos.Rows[i].Cells["Status"].Value.ToString() == "6 - CANCELADO")
                        {
                            frmHistoricoCancelamento frm = new frmHistoricoCancelamento();
                            int CodPessoa;
                            try
                            {
                                frm.ShowDialog();
                                if (frm.DialogResult == DialogResult.OK)
                                {
                                    HistoricoCancelamento Hist = new HistoricoCancelamento()
                                    {
                                        CodMotivo = frm.CodMotivo,
                                        CodPessoa = int.Parse(gridViewPedidos.Rows[i].Cells["CodPessoa"].Value.ToString()),
                                        Data = DateTime.Now,
                                        Motivo = frm.ObsCancelamento
                                    };

                                    con.Insert("spAdicionaHistoricoCancelamento", Hist);
                                }
                            }

                            finally
                            {
                                //this.dis

                            }
                            CancelarPedido cancPedido = new CancelarPedido()
                            {
                                Codigo = ped.CodPedido,
                                status = "Cancelado",
                                RealizadoEm = DateTime.Now
                            };
                            con.Update("spCancelarPedido", cancPedido);
                            Utils.ControlaEventos("CancPedido", this.Name);
                        }
                        con.Insert("spAdicionarPedidoStatusMovimento", ped);
                    }
                }
                gridViewPedidos.DataSource = null;
            }
            
        }
    }
}
