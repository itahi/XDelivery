﻿using DexComanda.Cadastros;
using DexComanda.Cadastros.Produto;
using DexComanda.Integração;
using DexComanda.Models;
using DexComanda.Operações;
using DexComanda.Operações.Alteracoes;
using DexComanda.Operações.Financeiro;
using DexComanda.Relatorios;
using DexComanda.Relatorios.Fechamentos.Novos;
using DexComanda.Relatorios.Gerenciais;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda
{
    public partial class frmPrincipal : Form
    {
        private Conexao con;
        private int rowIndex;
        private List<ItemPedido> items;
        private int CodPedidoWS = 0;
        private Boolean RepeteUltimoPedido = Sessions.returnConfig.RepeteUltimoPedido;
        private int iCaixaAberto;
        private string cUrlWs = "";
        private string iParamToken;
        private CancelarPedido cancelPedid;
        private bool ControlaMesas = Sessions.returnConfig.UsaControleMesa;
        public frmPrincipal()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void ConsultarCliente(object sender, EventArgs e)
        {
            BuscarCliente(txbTelefoneCliente.Text);

        }
        private void ExecutaRepeticaoPedido(int iCodPessoa)
        {
            DataRow LinhasPedido;
            int QuantidadePedidos = 0;
            DataSet ListaPedidos = con.SelectRegistroPorCodigo("Pedido", "spContaPedidosPorCliente", iCodPessoa);
            LinhasPedido = ListaPedidos.Tables["Pedido"].Rows[0];
            if (LinhasPedido.ItemArray.GetValue(0).ToString() != "0")
            {
                QuantidadePedidos = int.Parse(LinhasPedido.ItemArray.GetValue(0).ToString());
            }

            if (QuantidadePedidos > 0)
            {
                if (Utils.MessageBoxQuestion("Deseja Repetir o Ultimo Pedido ?"))
                {
                    Utils.RepetirUltimoPedido(iCodPessoa);
                }
                else
                {
                    IniciaPedido(iCodPessoa);
                }

            }
            else
            {
                IniciaPedido(iCodPessoa);
            }
        }
        private void IniciaPedido(int CodPessoa)
        {
            var TaxaEntrega = Utils.RetornaTaxaPorCliente(CodPessoa, con);

            frmCadastrarPedido CadPedido = new frmCadastrarPedido(false, "0,00", "", "0.00", TaxaEntrega, false, DateTime.Now, 0, CodPessoa,
                                                                    "0.00", "", "", "");
            CadPedido.ShowDialog();
            Utils.LimpaForm(this);
            Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido);
            TotalizaPedidos();
        }

        private void AbreTelaPedido(string iTelefone)
        {

            if (iTelefone != "")
            {
                var telefone = iTelefone;
                //  DBExpertDataSet dbExpert = new DBExpertDataSet();

                DataSet pessoaTelefone = con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", telefone);

                if ((pessoaTelefone.Tables["Pessoa"].Rows.Count == 0))
                {
                    DialogResult resultado = MessageBox.Show("Telefone não encontrado! Deseja cadastrar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        frmCadastroCliente frm = new frmCadastroCliente();
                        frm.txtTelefone.Text = telefone;
                        frm.ShowDialog();
                    }
                }
                else if ((pessoaTelefone.Tables["Pessoa"].Rows.Count == 1))
                {
                    if (Utils.CaixaAberto(DateTime.Now, Sessions.retunrUsuario.CaixaLogado))
                    {
                        DataSet Pessoa = con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", telefone);
                        DataRow dRow = Pessoa.Tables["Pessoa"].Rows[0];

                        int CodigoPessoa = int.Parse(dRow.ItemArray.GetValue(0).ToString());
                        this.txtNome.Text = dRow.ItemArray.GetValue(1).ToString();
                        this.txtEndereco.Text = dRow.ItemArray.GetValue(2).ToString();
                        this.txtBairro.Text = dRow.ItemArray.GetValue(3).ToString();
                        this.txtCidade.Text = dRow.ItemArray.GetValue(4).ToString();
                        this.txtPontoReferencia.Text = dRow.ItemArray.GetValue(5).ToString();
                        if (dRow.ItemArray.GetValue(7).ToString() != "")
                        {
                            this.txtEndereco.Text = this.txtEndereco.Text + ", Nº " + dRow.ItemArray.GetValue(7).ToString();
                        }

                        if (Sessions.returnConfig.RegistraCancelamentos)
                        {
                            Utils.HistoricoCancelamentos(CodigoPessoa);
                        }

                        if (RepeteUltimoPedido)
                        {
                            ExecutaRepeticaoPedido(CodigoPessoa);

                        }
                        else
                        {
                            IniciaPedido(CodigoPessoa);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Caixa Precisa esta aberto", "XSistemas");
                    }


                    Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido);
                }
                else if ((pessoaTelefone.Tables["Pessoa"].Rows.Count >= 1))
                {
                    MessageBox.Show("Há mais de um cliente cadastrado com o telefone informado, favor verificar os cadastros", "Aviso de inconsistencia");
                }
            }
            else
            {
                MessageBox.Show("Preencha o campo telefone para buscar", "Dex Aviso");
            }
            //LimpaCampos();


        }

        private void BuscarCliente(string iTelefone)
        {
            try
            {

                if (iCaixaAberto > 0)
                {
                    AbreTelaPedido(iTelefone);
                }
                else
                {
                    MessageBox.Show("Caixa precisa estar aberto", "[XSistemas] Aviso");
                }
                LimpaCampos();
            }

            catch (Exception errobusca)
            {

                MessageBox.Show(errobusca.Message, "Erro");
            }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxGrupoProduto, "NomeGrupo", "Codigo", "Grupo", "spObterGrupoAtivo");
            
            int iNumeroCaixa = Sessions.returnUsuario.CaixaLogado;
            iCaixaAberto = con.SelectRegistroPorDataCodigo("Caixa", "spObterDadosCaixa", DateTime.Now, iNumeroCaixa).Tables["Caixa"].Rows.Count;
            if (iCaixaAberto>0)
            {
                aberturaCaixaToolStripMenuItem.Enabled = false;
                lblCaixa.Text = "Caixa Aberto";
                lblCaixa.ForeColor = Color.Green;
            }


            this.txbTelefoneCliente.Focus();

            Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto);
            Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido);
            Utils.PopulaGrid_Novo("Pessoa", clientesGridView, Sessions.SqlPessoa);
            MontaMenu();
            TotalizaPedidos();

        }
        private void MontaMenu() // Monta o menu de opções
        {
            if (Sessions.returnEmpresa.CNPJ == "22695578000142" || Sessions.returnEmpresa.CNPJ == "22678091000151")
            {
                aberturaCaixaToolStripMenuItem.Enabled = false;
                controleDeEstoqueToolStripMenuItem.Enabled = false;
            }

            // Menu Visivel
            //relatórioToolStripMenuItem.Enabled = Sessions.returnUsuario.AcessaRelatoriosSN;
            entregadorToolStripMenuItem.Visible = Sessions.returnConfig.ControlaEntregador;
            envioDeSMSToolStripMenuItem.Enabled = Sessions.returnConfig.EnviaSMS;
            alterarSenhaToolStripMenuItem.Visible = Sessions.returnConfig.UsaLoginSenha;
            usuáriosToolStripMenuItem.Visible = Sessions.returnConfig.UsaLoginSenha;
            entregasPorMotoboyToolStripMenuItem.Visible = Sessions.returnConfig.ControlaEntregador;
            // entregadorToolStripMenuItem.Visible = Sessions.returnConfig.ControlaEntregador;


            this.txtUsuarioLogado.Text = Sessions.returnUsuario.Nome;
            usuáriosToolStripMenuItem.Enabled = Sessions.retunrUsuario.AdministradorSN;
            relatórioToolStripMenuItem.Enabled = Sessions.retunrUsuario.AcessaRelatoriosSN;
            configuraçãoToolStripMenuItem.Enabled = Sessions.retunrUsuario.AdministradorSN;
            usuáriosToolStripMenuItem.Visible = Sessions.returnConfig.UsaLoginSenha;
            operaçõesToolStripMenuItem.Enabled = Sessions.retunrUsuario.AdministradorSN;
            lançamentoAvulsoToolStripMenuItem.Enabled = iCaixaAberto > 0;

        }

        private void FiltraGrupo(object sender, EventArgs e)
        {
            if (cbxGrupoProduto.SelectedIndex != 0)
            {

                Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto, !chkProdutosInativos.Checked, " and CodGrupo=" + cbxGrupoProduto.SelectedValue.ToString());
            }
        }

        private void BuscaProduto(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtNomeProd.Text != "")
                {
                    if (cbxGrupoProduto.SelectedIndex != 0)
                    {
                        Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto, !chkProdutosInativos.Checked, " and CodGrupo=" + cbxGrupoProduto.SelectedValue.ToString() + " and NomeProduto like '%" + txtNomeProd.Text + "%'");
                    }
                    else
                    {
                        Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto, !chkProdutosInativos.Checked, " and NomeProduto like '%" + txtNomeProd.Text + "%'");
                    }

                }
                else
                {
                    Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto, !chkProdutosInativos.Checked);
                }
            }

        }

        private void chkProdutosInativos_CheckedChanged(object sender, EventArgs e)
        {
            Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto, !chkProdutosInativos.Checked);
        }

        private void produtosGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                CarregaProduto(int.Parse(produtosGridView.SelectedCells[0].Value.ToString()));
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
        }
        private void CarregaProduto(int iCodProduto)
        {
            //Carrega Produto
            DataSet dsProduto = con.SelectRegistroPorCodigo("Produto", "spObterProdutoPorCodigo", iCodProduto, !chkProdutosInativos.Checked);

            DataRow dRowProduto = dsProduto.Tables["Produto"].Rows[0];

            frmCadastrarProduto frm = new frmCadastrarProduto(int.Parse(dRowProduto.ItemArray.GetValue(7).ToString()), dRowProduto.ItemArray.GetValue(0).ToString(), dRowProduto.ItemArray.GetValue(12).ToString(), dRowProduto.ItemArray.GetValue(3).ToString(),
                                                              decimal.Parse(dRowProduto.ItemArray.GetValue(1).ToString()), dRowProduto.ItemArray.GetValue(2).ToString(),
                                                              Convert.ToBoolean(dRowProduto.ItemArray.GetValue(6).ToString()), decimal.Parse(dRowProduto.ItemArray.GetValue(5).ToString()),
                                                              dRowProduto.ItemArray.GetValue(4).ToString(), dRowProduto.ItemArray.GetValue(8).ToString(), dRowProduto.ItemArray.GetValue(9).ToString(), Convert.ToDateTime(dRowProduto.ItemArray.GetValue(10).ToString()), Convert.ToDateTime(dRowProduto.ItemArray.GetValue(11).ToString()));
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto);

        }
        private void DeletarProduto(object sender, EventArgs e)
        {
            try
            {
                if (produtosGridView.SelectedRows.Count > 0)
                {
                    if (Utils.MessageBoxQuestion("Deseja Excluir o Produto " + produtosGridView.SelectedCells[1].Value))
                    {
                        int Codigo = int.Parse(this.produtosGridView.SelectedCells[0].Value.ToString());
                        con.DeleteAll("Produto", "spExcluirProduto", Codigo);
                        MessageBox.Show("Item excluído com sucesso.");
                        Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto);

                    }
                }
                else
                {
                    MessageBox.Show("Selecione o produto para excluir");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possivel excluir o produto , pois ele provavelmente ja foi usado em algum pedido");
            }


        }
        private void MenuAuxiliar(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem ExcluirProduto = new MenuItem("Excluir Produto");
                ExcluirProduto.Click += DeletarProduto;
                m.MenuItems.Add(ExcluirProduto);
                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }

        private void FiltraTipoPedido(object sender, EventArgs e)
        {
            try
            {
                DataSet ds;
                ds = new DataSet("Pedido");
                if (cbxFiltroTipo.Text.Contains("Todos"))
                {
                    ds = Utils.PopularGrid("Pedido", pedidosGridView, Sessions.SqlPedido);
                }
                else
                {
                    string Consulta = null;
                    DataSet result = con.SelectAll("Pedido", "spObterPedido");
                    Consulta = cbxFiltroTipo.Text;

                    Consulta = "Tipo LIKE '%" + cbxFiltroTipo.Text + "%'";

                    var Linhas = result.Tables[0].DefaultView;
                    Linhas.RowFilter = Consulta;

                    var newDS = new DataSet("Pedido");
                    var newDT = Linhas.ToTable();

                    ds.Tables.Add(newDT);


                }
                this.pedidosGridView.DataSource = null;
                this.pedidosGridView.AutoGenerateColumns = true;
                this.pedidosGridView.DataSource = ds;
                this.pedidosGridView.DataMember = "Pedido";
                TotalizaPedidos();
                

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao buscar clientes" + ex.Message);
            }
        }
        private void TotalizaPedidos ()
        {
            double dblTotalPedidos = 0;
            for (int i = 0; i < pedidosGridView.Rows.Count; i++)
            {
                if (pedidosGridView.Columns.Contains("TotalPedido"))
                {
                    dblTotalPedidos = dblTotalPedidos + double.Parse(pedidosGridView.Rows[i].Cells["TotalPedido"].Value.ToString());
                }
                
            }
            lblValor.Text = dblTotalPedidos.ToString();
            lblQtd.Text = pedidosGridView.Rows.Count.ToString();
        }
        private void BuscaPedidoCliente(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (txtBuscaPedido.Text != "")
                    {
                        Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido, true, " and Nome like '%" + txtBuscaPedido.Text + "%'");
                    }
                    else
                    {
                        Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido, true);
                    }

                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
        }

        private void MarcarPedidos(object sender, DataGridViewCellEventArgs e)
        {
            bool blSelecionado = Convert.ToBoolean(this.pedidosGridView.CurrentRow.Cells["Finalizado"].Value.ToString());
            //if (pedidosGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            if (e.RowIndex >= 0)
            {
                if (pedidosGridView.CurrentRow.Cells["Finalizado"].Value.ToString() != null)
                {
                    if (blSelecionado)
                    {
                        pedidosGridView.CurrentRow.Cells["Finalizado"].Value = false;
                    }
                    else
                    {
                        pedidosGridView.CurrentRow.Cells["Finalizado"].Value = true;
                    }
                }

            }
        }
        private void CarregaPedido(int iCodPedido)
        {
            DataSet DsPedido;
            DataRow DvPedido;
            DsPedido = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", iCodPedido);
            DvPedido = DsPedido.Tables[0].Rows[0];
            string iTipo = DvPedido.ItemArray.GetValue(8).ToString();
            decimal TaxaServico = 0;
            if (iTipo == "0 - Entrega" || iTipo == "Entrega")
            {
                TaxaServico = Utils.RetornaTaxaPorCliente(int.Parse(DvPedido.ItemArray.GetValue(2).ToString()), con);
            }


            string strTrocoPara = DvPedido.ItemArray.GetValue(4).ToString();
            string strTotalPedido = DvPedido.ItemArray.GetValue(3).ToString();
            string strDescPedido = DvPedido.ItemArray.GetValue(14).ToString();
            string strTroco = "0,00";
            decimal MargemGarcon = decimal.Parse(DvPedido.ItemArray.GetValue(16).ToString());
            if (strTrocoPara != "0,00" && strTrocoPara != "0")
            {
                strTroco = Convert.ToString(decimal.Parse(strTrocoPara) - decimal.Parse(strTotalPedido));
            }

            frmCadastrarPedido frm = new frmCadastrarPedido(false, strDescPedido, DvPedido.ItemArray.GetValue(9).ToString(),
                                      strTroco, TaxaServico, true, Convert.ToDateTime(DvPedido.ItemArray.GetValue(7).ToString()),
                                      int.Parse(DvPedido.ItemArray.GetValue(1).ToString()), int.Parse(DvPedido.ItemArray.GetValue(2).ToString()), DvPedido.ItemArray.GetValue(4).ToString(),
                                      DvPedido.ItemArray.GetValue(5).ToString(), DvPedido.ItemArray.GetValue(8).ToString(), DvPedido.ItemArray.GetValue(9).ToString(), null, decimal.Parse(strTotalPedido), MargemGarcon);
            frm.ShowDialog();
        }
        private void EditarPedido(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int codigo = 0;
                int codigoPessoa = 0;
                decimal tPara = 0.00M;
                decimal TotalPedido = 0.00M;
                decimal TaxaEntrega = 0.00M;
                string TrocoTotal = "0.00";
                string fPagamento = null;
                string MesaBalcao, tipoPedido, NumMesa, PedidoOrigem = "";
                DateTime DataPedido;

                /* Code here */
                if (pedidosGridView.SelectedCells.Count > 0)
                {
                    codigo = int.Parse(pedidosGridView.CurrentRow.Cells["Codigo"].Value.ToString()); //SelectedRows[pedidosGridView.CurrentRow.Index].Cells["Codigo"].Value.ToString());
                    CarregaPedido(codigo);

                }
                Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido); 

            }
            catch (Exception es)
            {

                MessageBox.Show(es.Message);
            }
        }

        private void MenuAuxiliarPedido(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                ContextMenu subMenu = new ContextMenu();
                MenuItem CancPedido = new MenuItem(" 0 - Cancelar Pedidos");
                MenuItem FinalizarPed = new MenuItem(" 1 - Finalizar Este Pedido?");
                MenuItem FinalizaSelecionados = new MenuItem(" 2 - Finalizar Todos Selecionado?");
                MenuItem ImprimeConferenciaMesa = new MenuItem(" Imprimir Conferencia desta Mesa");
                MenuItem PedidoONline = new MenuItem(" X - Status Pedido");
               
                if (pedidosGridView.Rows.Count > 0)
                {
                    PedidoONline = new MenuItem(" X - Status Pedido");

                    DataSet dsStatus = con.SelectAll("PedidoStatus","", "select * from PedidoStatus");
                    //for (int i = 0; i < dsStatus.Tables[0].Rows.Count; i++)
                    //{
                        
                        MenuItem StatusNaCozinha = new MenuItem(" 0 - Pedido na Cozinha");
                        MenuItem StatusNaEntrega = new MenuItem(" 1 - Saiu pra entrega");
                        MenuItem StatusCancelado = new MenuItem(" 2 - Cancelado");

                        PedidoONline.MenuItems.Add(StatusNaCozinha);
                        PedidoONline.MenuItems.Add(StatusNaEntrega);
                        PedidoONline.MenuItems.Add(StatusCancelado);
                        StatusCancelado.Enabled = Sessions.returnUsuario.CancelaPedidosSN;
                        StatusNaCozinha.Click += PedidoNaCozinha;
                        StatusNaEntrega.Click += PedidoNaEntrega;
                    //}
                   

                }


                CancPedido.Click += CancelarPedidos;
                FinalizarPed.Click += FinalizaCancelaPEdidos;
                FinalizaSelecionados.Click += FinalizaTodos;
                if (Sessions.retunrUsuario != null)
                {
                    FinalizaSelecionados.Enabled = Sessions.returnUsuario.FinalizaPedidoSN;
                    FinalizarPed.Enabled = Sessions.retunrUsuario.FinalizaPedidoSN;
                    CancPedido.Enabled = Sessions.retunrUsuario.CancelaPedidosSN;
                }

                m.MenuItems.Add(CancPedido);
                m.MenuItems.Add(FinalizarPed);
                m.MenuItems.Add(FinalizaSelecionados);
                m.MenuItems.Add(PedidoONline);
                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));


            }
        }
        private void FinalizaTodos(object sender, EventArgs e)
        {
            try
            {
                bool ControlaMesas = Sessions.returnConfig.UsaControleMesa;
                int codigo;
                bool Marcado;
                string NumeroMesa;
                int iCodMesa;

                if (Utils.MessageBoxQuestion("Deseja ** FINALIZAR ** Todos Pedidos Selecionado?"))
                {

                    for (int i = 0; i < pedidosGridView.Rows.Count; i++)
                    {
                        Marcado = Convert.ToBoolean(this.pedidosGridView.Rows[i].Cells["Finalizado"].Value.ToString());
                        decimal dblTotalPedido;
                        if (Marcado)
                        {
                            codigo = int.Parse(pedidosGridView.Rows[i].Cells["Codigo"].Value.ToString());
                            //int.Parse(pedidosGridView.CurrentRow[i].Cells["Codigo"].Value.ToString());

                            DataSet dsPedido = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", codigo);
                            DataRow dRowPedido = dsPedido.Tables[0].Rows[0];

                            CodPedidoWS = VerificaPedidoOnline(codigo);
                            if (CodPedidoWS > 0)
                            {
                                AlteraStatusPedido(CodPedidoWS, 5);
                                // MessageBox.Show("Atualização Realizada com Sucesso, pedido entregue");
                            }
                            int intCodPessoa = int.Parse(dRowPedido.ItemArray.GetValue(2).ToString());
                            dblTotalPedido = decimal.Parse(dRowPedido.ItemArray.GetValue(3).ToString());

                            // Atualiza Ticket Fidelidade
                            AtualizarFidelidade(intCodPessoa);

                            // Grava Débito caso o Tipo de Pagamento gerar financeiro 
                            string strFormaPagamento = dRowPedido.ItemArray.GetValue(5).ToString();
                            Utils.GeraHistorico(DateTime.Now, int.Parse(dRowPedido.ItemArray.GetValue(2).ToString()), dblTotalPedido, Sessions.retunrUsuario.Codigo, "Pedido Nº " + codigo, 'D', strFormaPagamento);

                            // Grava Movimento De Caixa
                            GravaMOvimentoCaixa(strFormaPagamento, dblTotalPedido, codigo);

                            iCodMesa = int.Parse(dRowPedido.ItemArray.GetValue(12).ToString());
                            if (ControlaMesas && iCodMesa != 0)
                            {
                                Utils.AtualizaMesa(iCodMesa, 1);
                            }
                            con.SinalizarPedidoConcluido("Pedido", "spSinalizarPedidoConcluido", codigo);

                        }

                    }


                    Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido);
                }


            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel selecionar o Pedido" + erro.Message);
            }

        }
        private void FinalizaCancelaPEdidos(object sender, EventArgs e)
        {
            try
            {
                bool ControlaMesas = Sessions.returnConfig.UsaControleMesa;
                int codigo, iCodMesa;
                decimal dblTotalPedido;
             
                if (Utils.MessageBoxQuestion("Deseja ** FINALIZAR ** este pedido?"))
                {
                    codigo = int.Parse(this.pedidosGridView.CurrentRow.Cells["Codigo"].Value.ToString());
                    DataSet dsPedido = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", codigo);
                    DataRow dRowPedido = dsPedido.Tables[0].Rows[0];

                    CodPedidoWS = VerificaPedidoOnline(codigo);
                    if (CodPedidoWS > 0)
                    {
                        AlteraStatusPedido(CodPedidoWS, 5);
                        MessageBox.Show("Atualização Realizada com Sucesso, pedido entregue");
                    }

                    iCodMesa = int.Parse(dRowPedido.ItemArray.GetValue(12).ToString());
                    int intCodPessoa = int.Parse(dRowPedido.ItemArray.GetValue(2).ToString());
                    dblTotalPedido = decimal.Parse(dRowPedido.ItemArray.GetValue(3).ToString());
                    string iTipo = dRowPedido.ItemArray.GetValue(8).ToString();

                    //Caso O Pedido for Entrega e o Sistema controle 
                    //os entregadores ele abre a tela pedido pra informar quem entregou
                    if (Sessions.returnConfig.ControlaEntregador && iTipo == "0 - Entrega")
                    {
                        InformaMotoboyPedido(codigo);
                    }

                    // Caso o pedido for mesa ele altera o Status da Mesa
                    if (ControlaMesas && iCodMesa != 0)
                    {
                        //  NumeroMesa = Convert.ToString(Utils.RetornaNumeroMesa(iCodMesa));
                        Utils.AtualizaMesa(iCodMesa, 1);
                    }

                    // Grava Débito caso o Tipo de Pagamento gerar financeiro 
                    string strFormaPagamento = dRowPedido.ItemArray.GetValue(5).ToString();
                    Utils.GeraHistorico(DateTime.Now, int.Parse(dRowPedido.ItemArray.GetValue(2).ToString()), dblTotalPedido, Sessions.retunrUsuario.Codigo, "Pedido Nº " + codigo, 'D', strFormaPagamento);

                    // Grava Movimento De Caixa
                    GravaMOvimentoCaixa(strFormaPagamento, dblTotalPedido, codigo);

                    // Atualiza Ticket Fidelidade
                    AtualizarFidelidade(intCodPessoa);

                    // Enfim finaliza o Pedido
                    con.SinalizarPedidoConcluido("Pedido", "spSinalizarPedidoConcluido", codigo);

                    //  Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido);
                    Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido);
                }
                else
                {
                    LimpaCampos();
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel selecionar o Pedido" + erro.Message);
            }


        }
        private void InformaMotoboyPedido(int iCodPedido)
        {
            frmInformaMotoboy frm = new frmInformaMotoboy();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                InserirMotoboyPedido MotoBoy = new InserirMotoboyPedido()
                {
                    CodMotoBoy = frm.CodMotoboy,
                    CodPedido = iCodPedido
                };
                con.Update("spInsereBoyPedido", MotoBoy);
            }
        }
        public void LimpaCampos()
        {
            txbTelefoneCliente.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtEndereco.Text = "";
            txtNome.Text = "";
            txtPontoReferencia.Text = "";
            txbTelefoneCliente.Focus();
        }
        private void AtualizarFidelidade(int iCodPessoa)
        {
            if (Sessions.returnConfig.ControlaFidelidade)
            {
                AtualizarFidelidade atlFideli = new AtualizarFidelidade()
                {
                    CodPessoa = iCodPessoa,
                    Ticket = 1
                };

                con.Update("spAlteraFidelidade", atlFideli);
            }
        }
        private void GravaMOvimentoCaixa(string iFPagamento, decimal iValor, int iCodPedido)
        {
            // Retornando o IDFpagamento
            try
            {
                int iIFormaPagamento = 1;
                DataSet dsPedido = con.SelectObterFormaPagamentoPorNOme(iFPagamento, "FormaPagamento");
                if (dsPedido.Tables[0].Rows.Count > 0)
                {
                    DataRow dRow = dsPedido.Tables[0].Rows[0];
                    iIFormaPagamento = int.Parse(dRow.ItemArray.GetValue(0).ToString());
                }
                CaixaMovimento caixa = new CaixaMovimento()
                {
                    CodCaixa = Sessions.retunrUsuario.CaixaLogado,
                    CodFormaPagamento = iIFormaPagamento,
                    Data = DateTime.Now,
                    Historico = "Pedido " + iCodPedido,
                    NumeroDocumento = iCodPedido.ToString(),
                    Tipo = 'E',
                    Valor = iValor,
                    CodUser = Sessions.retunrUsuario.Codigo

                };
                con.Insert("spInserirMovimentoCaixa", caixa);

            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }
        private void CancelarPedidos(object sender, EventArgs e)
        {
            try
            {

                string NomeCliente = (this.pedidosGridView.CurrentRow.Cells["Nome Cliente"].Value.ToString());
                int CodUser;
                DateTime dtPedido;

                if (pedidosGridView.SelectedRows.Count > 0)
                {
                    cancelPedid = new CancelarPedido();
                    if (Utils.MessageBoxQuestion("Deseja **CANCELAR** o  pedido do Cliente " + NomeCliente + "?"))
                    {
                        int Codigo = int.Parse(this.pedidosGridView.CurrentRow.Cells["Codigo"].Value.ToString());

                        DataSet dsPedido = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", Codigo);
                        dtPedido = dsPedido.Tables[0].Rows[0].Field<DateTime>("RealizadoEm");
                        DataRow dRowPedido = dsPedido.Tables[0].Rows[0];

                        if (Sessions.returnEmpresa.CNPJ == "13004606798")
                        {
                            DataSet dsItensPedido = con.SelectRegistroPorCodigo("ItemsPedido", "spObterItemsPedido", Codigo);

                            for (int i = 0; i < dsItensPedido.Tables[0].Rows.Count; i++)
                            {
                                DataRow dRow = dsItensPedido.Tables[0].Rows[i];
                                AtualizaEstoque atl = new AtualizaEstoque()
                                {
                                    CodProduto = int.Parse(dRow.ItemArray.GetValue(2).ToString()),
                                    Quantidade = -decimal.Parse(dRow.ItemArray.GetValue(4).ToString()),
                                    DataAtualizacao = Convert.ToDateTime(dtPedido.ToShortDateString()) //dsItensPedido.Tables[0].Rows[i]

                                };
                                con.Update("spAtualizaEstoque", atl);
                            }
                        }

                        CodPedidoWS = VerificaPedidoOnline(Codigo);

                        if (CodPedidoWS > 0)
                        {
                            if (Utils.MessageBoxQuestion("Este é um pedido gerado/recebido na plataforma Online , tem certeza que deseja cancela-lo?"))
                            {
                                AlteraStatusPedido(CodPedidoWS, 6);
                                // MessageBox.Show("Atualização Realizada com Sucesso, pedido entregue");
                            }
                        }


                        cancelPedid.Codigo = Codigo;
                        cancelPedid.RealizadoEm = DateTime.Now;

                        int iCodMesa = int.Parse(dRowPedido.ItemArray.GetValue(12).ToString());

                        if (ControlaMesas && iCodMesa != 0)
                        {
                            Utils.AtualizaMesa(iCodMesa, 1);
                        }
                        cancelPedid.status = "Cancelado";
                        if (Sessions.returnConfig.RegistraCancelamentos)
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
                                        CodPessoa = int.Parse(dRowPedido.ItemArray.GetValue(2).ToString()),
                                        Data = DateTime.Now,
                                        Motivo = frm.ObsCancelamento
                                    };

                                    con.Insert("spAdicionaHistoricoCancelamento", Hist);
                                }
                            }

                            finally
                            {
                                // dsPedido.Dispose();

                            }


                        }

                        con.Update("spCancelarPedido", cancelPedid);
                        Utils.ControlaEventos("CancPedido", this.Name);
                        MessageBox.Show("Pedido Cancelado com sucesso.");

                        //   Utils.PopulaGrid_Novo("Produto", parentWindow.produtosGridView, Sessions.SqlProduto);
                        Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido);
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um Pedido para **CANCELAR**");
                }
            }
            catch (Exception ER)
            {
                MessageBox.Show("Não foi possivel **CANCELAR O PEDIDO**" + ER.Message);
            }
        }
        private int VerificaPedidoOnline(int CodPedido)
        {
            int iCodWs = 0;
            DataSet dPedido = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoOnline", CodPedido);
            if (dPedido.Tables[0].Rows.Count > 0)
            {
                iCodWs = dPedido.Tables[0].Rows[0].Field<int>("CodigoPedidoWS");

            }
            return iCodWs;
        }
        private void PedidoNaCozinha(object sender, EventArgs e)
        {
            try
            {
                int intCodPedido = int.Parse(pedidosGridView.SelectedRows[rowIndex].Cells["Codigo"].Value.ToString());
                if (VerificaPedidoOnline(intCodPedido) > 0)
                {
                    AlteraStatusPedido(CodPedidoWS, 3);
                }

                con.AtualizaSitucao(intCodPedido, Sessions.retunrUsuario.Codigo, 2,pedidosGridView);

            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possivel alterar o status do Pedido " + erro.Message);
            }

        }
        private void PedidoNaEntrega(object sender, EventArgs e)
        {
            try
            {
                int intCodPedido = int.Parse(pedidosGridView.SelectedRows[rowIndex].Cells["Codigo"].Value.ToString());

                if (VerificaPedidoOnline(intCodPedido) > 0)
                {
                    AlteraStatusPedido(CodPedidoWS, 4);
                }
                con.AtualizaSitucao(intCodPedido, Sessions.retunrUsuario.Codigo, 3,pedidosGridView);
            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possivel alterar o status do Pedido " + erro.Message);
            }

        }
        private void GerarToken()
        {
            try
            {
                cUrlWs = Sessions.returnEmpresa.UrlServidor;
                iParamToken = Convert.ToString(DateTime.Now).Replace("/", "").Replace(":", "").Replace(" ", "").Substring(0, 11) + "Adminx";
                iParamToken = Utils.CriptografarArquivo(iParamToken.Trim(), false);
            }
            catch (Exception e)
            {

                MessageBox.Show("Geração do Token de validação " + e.Message);
            }

        }
        private void AlteraStatusPedido(int iCodPedidows, int iStatus)
        {
            GerarToken();
            RestClient client = new RestClient(cUrlWs);
            RestRequest request = new RestRequest("ws/pedidos/status", Method.POST);
            request.AddParameter("idPedido", iCodPedidows);
            request.AddParameter("idStatus", iStatus);
            request.AddParameter("token", iParamToken);
            RestResponse response = (RestResponse)client.Execute(request);

            if (response.Content.Contains("true"))
            {
                MessageBox.Show("Alteração realizada com sucesso");
            }
            else
            {
                MessageBox.Show("Alteração não pode ser realizada, favor tentar novamente mais tarde");
            }


        }
        private void AbrirPedido(int CodPessoa)
        {
            try
            {
                if (Sessions.returnConfig.RepeteUltimoPedido)
                {
                    ExecutaRepeticaoPedido(CodPessoa);
                }
                else
                {
                    decimal TaxaServico = Utils.RetornaTaxaPorCliente(CodPessoa, con);
                    frmCadastrarPedido frm = new frmCadastrarPedido(false, "", "", "0,00", TaxaServico, false, DateTime.Now, 0, CodPessoa,
                                                                        "", "", "", "", null, 0.00M);
                    frm.ShowDialog();
                }


                //  frm.Dispose();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void MenuAuxiliarCliente(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenu m = new ContextMenu();
                    MenuItem MontarPedido = new MenuItem("Criar Pedido");
                    MenuItem ExcluirCliente = new MenuItem("Excluir Cliente");
                    MontarPedido.Click += CriarPedido;
                    ExcluirCliente.Click += DeletarCliente;

                    m.MenuItems.Add(MontarPedido);
                    m.MenuItems.Add(ExcluirCliente);
                    // Se o caixa estiver aberto ele Libera pra criar PEdido

                    MontarPedido.Enabled = Utils.CaixaAberto(DateTime.Now, Sessions.retunrUsuario.CaixaLogado);
                    int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;

                    m.Show(dgv, new Point(e.X, e.Y));
                }

            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
            }
        }
        private void DeletarCliente(object sender, EventArgs e)
        {
            int iCodCliente = 0;
            try
            {
                iCodCliente = Convert.ToInt16(clientesGridView.SelectedCells[0].Value);
                if (iCodCliente != null || iCodCliente != 0)
                {
                    DialogResult resultado = MessageBox.Show("Deseja Excluir o Cliente " + clientesGridView.SelectedCells[1].Value, "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (resultado == DialogResult.Yes)
                    {
                        con.DeleteAll("Pessoa", "spExcluirCliente", iCodCliente);
                        Utils.ControlaEventos("Excluir", this.Name);
                        Utils.PopularGrid("Pessoas", clientesGridView);
                    }
                }
                else
                {
                    MessageBox.Show("Não foi possivel selecionar o cliente para excluir , favor selecionar novamente", "DEXAviso");
                }

            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao excluir cliente , talvez ele já tenha sido usado em algum pedido" + erro.Message, "DexAviso");
            }

        }
        private void CriarPedido(object sender, EventArgs e)
        {
            try
            {
                rowIndex = clientesGridView.CurrentRow.Index;
                int CodCliente = int.Parse(clientesGridView.Rows[rowIndex].Cells[0].Value.ToString());
                if (Sessions.returnConfig.RegistraCancelamentos)
                {
                    Utils.HistoricoCancelamentos(CodCliente);
                }
                AbrirPedido(CodCliente);
            }
            catch (Exception xx)
            {

                MessageBox.Show(xx.Message);
            }
        }

        private void EditarCliente(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (clientesGridView.SelectedCells.Count > 0)
            {

                DataSet dsPessoa = con.SelectRegistroPorCodigo("Pessoa", "spObterPessoaPorCodigo", int.Parse(clientesGridView.SelectedCells[0].Value.ToString()));
                DataRow dRowPessoa = dsPessoa.Tables["Pessoa"].Rows[0];

                frmCadastroCliente frm = new frmCadastroCliente(int.Parse(dRowPessoa.ItemArray.GetValue(0).ToString()), dRowPessoa.ItemArray.GetValue(1).ToString(), dRowPessoa.ItemArray.GetValue(10).ToString(),
                                                                  dRowPessoa.ItemArray.GetValue(11).ToString(), dRowPessoa.ItemArray.GetValue(2).ToString(), dRowPessoa.ItemArray.GetValue(3).ToString(), dRowPessoa.ItemArray.GetValue(9).ToString()
                                                                  , dRowPessoa.ItemArray.GetValue(4).ToString(), dRowPessoa.ItemArray.GetValue(5).ToString(), dRowPessoa.ItemArray.GetValue(6).ToString(), dRowPessoa.ItemArray.GetValue(7).ToString()
                                                                  , dRowPessoa.ItemArray.GetValue(8).ToString(), int.Parse(dRowPessoa.ItemArray.GetValue(14).ToString()), dRowPessoa.ItemArray.GetValue(15).ToString(), dRowPessoa.ItemArray.GetValue(12).ToString());


                Utils.PopulaGrid_Novo("Pessoa", clientesGridView, Sessions.SqlPessoa);


            }
        }

        private void BuscarCliente(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbxBuscarPor.Text != "")
                {
                    string propriedade = cbxBuscarPor.SelectedItem.ToString();
                    string valor = txtBurcarValor.Text;
                    DataSet dsResult;
                    if (!propriedade.Equals(""))
                    {
                        if (!valor.Equals(""))
                        {
                            if (propriedade.Equals("Telefone"))
                            {
                                dsResult = con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", valor);
                            }
                            else if (propriedade.Equals("Nome"))
                            {
                                dsResult = con.SelectPessoaPorNome(valor, Sessions.SqlPessoa, "Nome");
                            }
                            else
                            {
                                dsResult = con.SelectPessoaPorNome(valor, Sessions.SqlPessoa, "Endereco");
                            }

                            this.clientesGridView.DataSource = null;
                            this.clientesGridView.AutoGenerateColumns = true;
                            this.clientesGridView.DataSource = dsResult;
                            this.clientesGridView.DataMember = "Pessoa";
                        }
                        else
                        {
                            MessageBox.Show("Informe Nome ou Telefone.");
                            Utils.PopulaGrid_Novo("Pessoa", clientesGridView, Sessions.SqlPessoa);
                            // Utils.PopularGrid("Pessoas", this.clientesGridView);


                        }
                    }

                }
                else
                {
                    MessageBox.Show("Informe se a busca é por Nome ou Telefone");
                }
            }
        }
        private void VerificaRegistroASincronizar()
        {
            if (Sessions.returnEmpresa.UrlServidor != "")
            {
                DataSet dsProduto = con.SelectRegistroONline("Produto");
                DataSet dsOpcao = con.SelectRegistroONline("Opcao");
                DataSet dsProdutOpcao = con.SelectRegistroONline("Produto_Opcao");
                //  DataSet dsFormaPagamento = con.SelectRegistroONline("FormaPagamento");
                DataSet dsGrupo = con.SelectRegistroONline("Grupo");

                if (dsProduto.Tables[0].Rows.Count > 0 || dsOpcao.Tables[0].Rows.Count > 0 || dsProdutOpcao.Tables[0].Rows.Count > 0 || /*dsFormaPagamento.Tables[0].Rows.Count > 0 ||*/ dsGrupo.Tables[0].Rows.Count > 0)
                {
                    DialogResult resposta = MessageBox.Show("Há registros que precisam ser sincronizados com o servidor , deseja fazer isso agora?", "[xSistemas]", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (resposta == DialogResult.Yes)
                    {
                        frmSincronizacao frm = new frmSincronizacao();
                        frm.ShowDialog();
                    }
                }

            }


        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            string strServidor = Sessions.returnEmpresa.Servidor;
            string strBanco = Sessions.returnEmpresa.Banco;
            string strCaminhoBkp = Sessions.returnEmpresa.CaminhoBackup;
            VerificaRegistroASincronizar();
            con.BackupBanco(strServidor, strBanco, strCaminhoBkp);

            this.Dispose();
            Utils.Kill();
            con.Close();
        }

        private void frmPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12 || e.KeyCode == Keys.Enter)
            {
                if (txbTelefoneCliente.Focused)
                {
                    BuscarCliente(txbTelefoneCliente.Text);
                }

            }
            if (e.KeyCode == Keys.F11)
            {
                ImpressaRapida();
            }
        }
        private void ImpressaRapida()
        {
            int iCodPedido = int.Parse(pedidosGridView.Rows[0].Cells["Codigo"].Value.ToString());
            DataSet DsPedido = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", iCodPedido);
            DataRow DvPedido = DsPedido.Tables[0].Rows[0];

            string iTipo = DvPedido.ItemArray.GetValue(8).ToString();
            decimal iTrocoPara = decimal.Parse(DvPedido.ItemArray.GetValue(4).ToString());
            decimal iTotalPedido = decimal.Parse(DvPedido.ItemArray.GetValue(3).ToString());
            decimal iValue = 0;
            if (iTrocoPara != 0.00M && iTrocoPara != 0)
            {
                iValue = iTrocoPara - iTotalPedido;
            }

            DateTime iDataPedido = Convert.ToDateTime(DvPedido.ItemArray.GetValue(7).ToString());
            if (iTipo == "1 - Mesa")
            {

                Utils.ImpressaoFechamentoNovo(iCodPedido);
            }
            else if (iTipo == "0 - Entrega")
            {
                Utils.ImpressaoEntreganova(iCodPedido, iValue, iDataPedido.AddMinutes(Convert.ToDouble(Sessions.returnConfig.PrevisaoEntrega)).ToShortTimeString(), false, 1);
            }
            else if (iTipo == "2 - Balcao")
            {

                Utils.ImpressaoBalcao(iCodPedido);
            }
        }

        private void gruposCategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void tipoOpçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTipoOpcao frm = new frmTipoOpcao();
            frm.ShowDialog();
        }

        private void opçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadOpcao frm = new frmCadOpcao();
            frm.ShowDialog();
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastrarProduto frm = new frmCadastrarProduto(null);
            frm.ShowDialog();
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroUsuario frm = new frmCadastroUsuario();
            frm.Show();
        }

        private void entregadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastrarEntregador frm = new frmCadastrarEntregador();
            frm.Show();
        }

        private void formasDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastrarFormaPagamento frm = new frmCadastrarFormaPagamento();
            frm.Show();
        }

        private void regiãoDeEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroRegioes frm = new frmCadastroRegioes();
            frm.ShowDialog();
        }

        private void bairrosPorRegiãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBairrosRegiao frm = new frmBairrosRegiao();
            frm.ShowDialog();
        }

        private void mesasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdicionarMesa frm = new frmAdicionarMesa();
            frm.ShowDialog();
        }

        private void motivosCancelamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadMotivosCancelamento frm = new frmCadMotivosCancelamento();
            frm.ShowDialog();
        }

        private void statusPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroStatus frm = new frmCadastroStatus();
            frm.ShowDialog();
        }

        private void geralToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmReportPedidosPorPeriodo frm = new frmReportPedidosPorPeriodo();
            frm.ShowDialog();
        }

        private void entregasPorMotoboyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportEntregasMotoboy frm = new frmReportEntregasMotoboy();
            frm.ShowDialog();
        }

        private void resumidoFormaDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportFechamentoResumido frm = new frmReportFechamentoResumido();
            frm.ShowDialog();
        }

        private void ticketMédioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportTickeMedio frm = new frmReportTickeMedio();
            frm.ShowDialog();
        }

        private void geralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportProdutos frm = new frmReportProdutos();
            frm.Show();
        }

        private void maisVendidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportItensVendidos frm = new frmReportItensVendidos();
            frm.ShowDialog();
        }

        private void vendidosNoPeriodoAgrupadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportVendasPorProduto frm = new frmReportVendasPorProduto();
            frm.ShowDialog();
        }

        private void envioDeSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEnvioSms frm = new frmEnvioSms();
            frm.ShowDialog();
        }

        private void sistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfiguracoes frm = new frmConfiguracoes();
            frm.Show();
        }

        private void impressãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfigurarImpressao frm = new frmConfigurarImpressao();
            frm.ShowDialog();
        }

        private void renovarAtivarSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicencaOFFLINE frm = new frmLicencaOFFLINE();
            frm.ShowDialog();
        }

        private void produtosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAlteracaoProduto frm = new frmAlteracaoProduto();
            frm.ShowDialog();
        }

        private void opçãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAlterarOpcao frm = new frmAlterarOpcao();
            frm.Show();
        }

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAlteracaoCliente frm = new frmAlteracaoCliente();
            frm.ShowDialog();
        }

        private void lançamentoAvulsoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLancamentoCaixa frmLan = new frmLancamentoCaixa();
            frmLan.ShowDialog();
        }

        private void movimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCaixaMovimento frm = new frmCaixaMovimento();
            frm.ShowDialog();
        }

        private void cadastroCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadCaixa frm = new frmCadCaixa();
            frm.ShowDialog();
        }

        private void fecharCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCaixaFechamento frm = new frmCaixaFechamento();
            frm.ShowDialog();
        }

        private void aberturaCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAberturaCaixa frm = new frmAberturaCaixa();
            frm.ShowDialog();
        }

        private void sincronizaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSincronizacao frm = new frmSincronizacao();
            frm.ShowDialog();
        }

        private void contatoAtivaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmContato frm = new frmContato();
            frm.ShowDialog();
        }

        private void txbTelefoneCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }
        private void ImpressaoAutomatica(int iCodPedido, string iNumMesa)
        {

            DataSet itemsPedido = con.SelectRegistroPorCodigo("ItemsPedido", "spObterItemsNaoImpresso", iCodPedido);
            if (itemsPedido.Tables[0].Rows.Count > 0)
            {
                items = new List<ItemPedido>();
                ItemPedido itemPedido = new ItemPedido();
                string lRetorno = "";
                Boolean imprimirAgora = false;
                //string strNomeImpressora,strImpressoraAnterior = "";
                lRetorno = Utils.ImpressaMesaNova(iCodPedido, false, int.Parse(Sessions.returnConfig.ViasCozinha), "", imprimirAgora);
                for (int i = 0; i < itemsPedido.Tables[0].Rows.Count; i++)
                {
                    AtualizaItemsImpresso Atualiza = new AtualizaItemsImpresso();
                    Atualiza.CodPedido = iCodPedido;
                    Atualiza.CodProduto = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<int>("CodProduto");
                    Atualiza.ImpressoSN = true;
                    con.Update("spInformaItemImpresso", Atualiza);

                }
                //if (ImprimeLPT && lRetorno != "")
                //{
                //    StreamReader tempDex = new StreamReader(lRetorno);
                //    line = tempDex.ReadToEnd();

                //    string RetornoTxt = Directory.GetCurrentDirectory() + @"\" + "ConfigImpressao" + ".txt";
                //    if (System.IO.File.Exists(RetornoTxt))
                //    {
                //        tempDex = new StreamReader(RetornoTxt);
                //        // line = tempDex.ReadLine();
                //        RetornoTxt = tempDex.ReadLine();

                //        if (RetornoTxt != "")
                //        {
                //            string iPortaUSB = "", iModelo = "";
                //            string[] words = RetornoTxt.Split(';');

                //            for (int i = 0; i < words.Length; i++)
                //            {
                //                iModelo = words[0];
                //                iPortaUSB = words[1];
                //            }
                //            int iRetorno;
                //            MP2032 bema = new MP2032();
                //            try
                //            {
                //                iRetorno = MP2032.ConfiguraModeloImpressora(int.Parse(iModelo));
                //                iRetorno = MP2032.IniciaPorta(iPortaUSB);
                //                if (iRetorno == 1)
                //                {
                //                    MP2032.FormataTX(line, 2, 0, 0, 1, 0);
                //                    iRetorno = MP2032.BematechTX(line + "\r\n\r\n");
                //                    MP2032.AcionaGuilhotina(1);
                //                }
                //                else
                //                {
                //                    MessageBox.Show("Erro de  conexão impressora" + iModelo + " Porta" + iPortaUSB);
                //                }
                //            }
                //            catch (Exception erro)
                //            {

                //                MessageBox.Show(erro.Message);
                //            }



                //        }
                //    }
                //    //    }


                //}

            }
        }
        private void MudarCorLinha(int iCodPedido, DataGridView grdPedido)
        {
            DataSet dsPedido = con.SelectRegistroPorCodigo("Pedido", "spStatusPedido", iCodPedido);
            int status = dsPedido.Tables[0].Rows[0].Field<int>("CodStatus");
            switch (status)
            {
                case 1:
                    grdPedido.Rows[0].DefaultCellStyle.BackColor = Color.Red;
                    break;

                case 2:
                    grdPedido.Rows[0].DefaultCellStyle.BackColor = Color.Green;
                    break;
                case 3:
                    grdPedido.Rows[0].DefaultCellStyle.BackColor = Color.Blue;
                    break;
            }
        }
        private void AtualizaGrid_Tick(object sender, EventArgs e)
        {
            try
            {
                DataSet dsPedidosAbertos = con.SelectAll("Pedido", "spObterPedido");
                int iPedidosAberto = dsPedidosAbertos.Tables["Pedido"].Rows.Count;

                if (iPedidosAberto != pedidosGridView.Rows.Count && cbxFiltroTipo.Text == "")
                {
                    TotalizaPedidos();
                    Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido);

                }

                for (int i = 0; i < pedidosGridView.Rows.Count; i++)
                {
                  
                   // MudarCorLinha(int.Parse(pedidosGridView.Rows[i].Cells["Codigo"].Value.ToString()), pedidosGridView);
                    DataRow dRowPedido = dsPedidosAbertos.Tables[0].Rows[i];
                    Boolean iMesa = dRowPedido.ItemArray.GetValue(5).ToString() != "0";
                    if (iMesa && chkGerenciaImpressao.Checked)
                    {
                        ImpressaoAutomatica(int.Parse(dRowPedido.ItemArray.GetValue(1).ToString()), dRowPedido.ItemArray.GetValue(5).ToString());
                    }
                }
                
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao atualizar pedidos" + erro.Message);
            }
        }

        private void pedidosGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            TotalizaPedidos();
        }

        private void grupoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdicionarGrupo frm = new frmAdicionarGrupo();
            frm.ShowDialog();
        }

        private void familiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFamilia frm = new frmFamilia();
            frm.Show();
        }
    }
}