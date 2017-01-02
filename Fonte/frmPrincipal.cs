﻿using DexComanda.Cadastros;
using DexComanda.Cadastros.Empresa;
using DexComanda.Cadastros.Pedido;
using DexComanda.Cadastros.Pessoa;
using DexComanda.Cadastros.Produto;
using DexComanda.Integração;
using DexComanda.Models;
using DexComanda.Models.WS;
using DexComanda.Operações;
using DexComanda.Operações.Alteracoes;
using DexComanda.Operações.Consultas;
using DexComanda.Operações.Financeiro;
using DexComanda.Operações.Funções;
using DexComanda.Push;
using DexComanda.Relatorios;
using DexComanda.Relatorios.Clientes;
using DexComanda.Relatorios.Fechamentos.Novos;
using DexComanda.Relatorios.Fechamentos.Novos.Impressao_Termica;
using DexComanda.Relatorios.Gerenciais;
using DexComanda.Relatorios.Impressao_Termica;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        private DataSet dsPedidosAbertos;
        private DataSet itemsPedido;
        private int intCodEndereco;
        public frmPrincipal()
        {
            con = new Conexao();
            InitializeComponent();
            this.Text = Text + RetornaVersao();
        }

        private void ConsultarCliente(object sender, EventArgs e)
        {
            BuscarCliente(txbTelefoneCliente.Text);

        }
        private void ExecutaRepeticaoPedido(int iCodPessoa, int codEnde)
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
                    Utils.RepetirUltimoPedido(iCodPessoa, codEnde);
                }
                else
                {
                    IniciaPedido(iCodPessoa, codEnde);
                }

            }
            else
            {
                IniciaPedido(iCodPessoa, codEnde);
            }
        }
        private void IniciaPedido(int CodPessoa, int iCodEndeco = 0)
        {
            var TaxaEntrega = Utils.RetornaTaxaPorCliente(CodPessoa, iCodEndeco);

            frmCadastrarPedido CadPedido = new frmCadastrarPedido(false, "0,00", "0,00", "0,00", TaxaEntrega,
                                                                        false, DateTime.Now, 0, CodPessoa,
                                                                        "", "", "", "", null, 0.00M, 0.00M, 0, "", iCodEndeco);
            CadPedido.ShowDialog();
            Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido);
            TotalizaPedidos();
        }

        private void AbreTelaPedido(string iTelefone)
        {

            if (iTelefone != "")
            {
                var telefone = iTelefone;
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
                    if (Utils.CaixaAberto(DateTime.Now, Sessions.retunrUsuario.CaixaLogado, Sessions.retunrUsuario.Turno))
                    {

                        DataSet Pessoa = con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", telefone);
                        DataRow dRow = Pessoa.Tables["Pessoa"].Rows[0];


                        int CodigoPessoa = int.Parse(dRow.ItemArray.GetValue(0).ToString());
                        // intCodEndereco =  Utils.MaisEnderecos(CodigoPessoa);
                        intCodEndereco = int.Parse(dRow.ItemArray.GetValue(16).ToString());
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
                            ExecutaRepeticaoPedido(CodigoPessoa, intCodEndereco);
                        }
                        else
                        {
                            IniciaPedido(CodigoPessoa, intCodEndereco);
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
        private void ExecutaCancelamento(int CodUserCancelamento)
        {
            string NomeCliente = (this.pedidosGridView.CurrentRow.Cells["Nome Cliente"].Value.ToString());
            int CodUser;
            DateTime dtPedido;

            if (pedidosGridView.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Deseja **CANCELAR** o  pedido do Cliente " + NomeCliente + "?", "Cancelamento de Pedido !!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    cancelPedid = new CancelarPedido();
                    int Codigo = int.Parse(this.pedidosGridView.CurrentRow.Cells["Codigo"].Value.ToString());

                    DataSet dsPedido = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", Codigo);

                    dtPedido = dsPedido.Tables[0].Rows[0].Field<DateTime>("RealizadoEm");
                    DataRow dRowPedido = dsPedido.Tables[0].Rows[0];
                    int iCodPessoa = int.Parse(dRowPedido.ItemArray.GetValue(2).ToString());

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
                            AlteraStatusPedido(CodPedidoWS, StatusPedido.cPedidoCancelado, iCodPessoa);
                            // MessageBox.Show("Atualização Realizada com Sucesso, pedido entregue");
                        }
                    }


                    cancelPedid.Codigo = Codigo;
                    cancelPedid.RealizadoEm = DateTime.Now;

                    int iCodMesa = int.Parse(dRowPedido.ItemArray.GetValue(12).ToString());

                    AtualizaStatusMesa(iCodMesa, Bibliotecas.cStatuMesaLiberada);
                    //if (ControlaMesas && iCodMesa !=0)
                    //{
                    //    Utils.AtualizaMesa(iCodMesa, 1);
                    //}
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
                                    Motivo = frm.ObsCancelamento,
                                    CodPedido = Codigo
                                };

                                con.Insert("spAdicionaHistoricoCancelamento", Hist);
                            }

                        }

                        finally
                        {
                            // dsPedido.Dispose();

                        }

                    }
                    RemoveFinalizacao rem = new RemoveFinalizacao()
                    {
                        CodPedido = cancelPedid.Codigo
                    };
                    con.Delete("spExcluirFinalizaPedido_Pedido", rem);
                    cancelPedid.CodUsuario = CodUserCancelamento;
                    //Utils.ControlaEventos("CancPedido", this.Name);
                    con.Update("spCancelarPedido", cancelPedid);

                    Utils.ControlaEventos("CancPedido", this.Name);
                    MessageBox.Show("Pedido Cancelado com sucesso.");
                    Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido);
                }
            }
            else
            {
                MessageBox.Show("Selecione um Pedido para **CANCELAR**");
            }
        }
        private void AtualizaStatusMesa(int iCodMesa, int iStatus)
        {
            if (ControlaMesas && iCodMesa != 0)
            {
                Utils.AtualizaMesa(iCodMesa, iStatus);
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //
        }
        private string RetornaVersao()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return Text = String.Format(" versão {0}", version);
        }
        private void frmPrincipal_Load(object sender, EventArgs e)
        {

            Utils.MontaCombox(cbxGrupoProduto, "NomeGrupo", "Codigo", "Grupo", "spObterGrupoAtivo");
            int iNumeroCaixa = Sessions.retunrUsuario.CaixaLogado;
            DataSet dsCaixa = con.RetornaCaixaPorTurno(iNumeroCaixa, Sessions.retunrUsuario.Turno, DateTime.Now);
            iCaixaAberto = dsCaixa.Tables[0].Rows.Count;
            if (iCaixaAberto > 0)
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
            if (Sessions.returnEmpresa.CNPJ != Bibliotecas.cCasteloPlus &&
                Sessions.returnEmpresa.CNPJ != Bibliotecas.cTopsAcai &&
                Sessions.returnEmpresa.CNPJ != Bibliotecas.cElShaday &&
                Sessions.returnEmpresa.CNPJ != Bibliotecas.cCarangoVix)
            {
                aberturaCaixaToolStripMenuItem.Enabled = true;
                controleDeEstoqueToolStripMenuItem.Enabled = true;
            }

            // Menu Visivel
            //relatórioToolStripMenuItem.Enabled = Sessions.retunrUsuario.AcessaRelatoriosSN;
            entregadorToolStripMenuItem.Visible = Sessions.returnConfig.ControlaEntregador;
            envioDeSMSToolStripMenuItem.Enabled = Sessions.returnConfig.EnviaSMS;
            alterarSenhaToolStripMenuItem.Visible = Sessions.returnConfig.UsaLoginSenha;
            usuáriosToolStripMenuItem.Visible = Sessions.returnConfig.UsaLoginSenha;

            this.txtUsuarioLogado.Text = Sessions.retunrUsuario.Nome;
            //usuáriosToolStripMenuItem.Enabled = Sessions.retunrUsuario.AdministradorSN;
            //relatórioToolStripMenuItem.Enabled = Sessions.retunrUsuario.AcessaRelatoriosSN;
            configuraçãoToolStripMenuItem.Enabled = Sessions.retunrUsuario.AdministradorSN;
            usuáriosToolStripMenuItem.Visible = Sessions.returnConfig.UsaLoginSenha;
            // FinanceiroToolStripMenuItem.Enabled = Sessions.retunrUsuario.AbreFechaCaixaSN;
            // alteracaoStatusToolStripMenuItem.Enabled = Sessions.retunrUsuario.AlteraProdutosSN;
            //sincronizaçãoToolStripMenuItem.Enabled = Sessions.retunrUsuario.AlteraProdutosSN;
            //notificaçõesToolStripMenuItem.Enabled = Sessions.retunrUsuario.AdministradorSN;
            lançamentoAvulsoToolStripMenuItem.Enabled = iCaixaAberto > 0;

        }

        private void FiltraGrupo(object sender, EventArgs e)
        {

        }

        private void BuscaProduto(object sender, KeyEventArgs e)
        {
            if (txtNomeProd.Focused)
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
            int index = rowIndex;
            DataSet dsProduto = con.SelectRegistroPorCodigo("Produto", "spObterProdutoPorCodigo", iCodProduto, !chkProdutosInativos.Checked);

            DataRow dRowProduto = dsProduto.Tables["Produto"].Rows[0];

            frmCadastrarProduto frm = new frmCadastrarProduto(int.Parse(dRowProduto.ItemArray.GetValue(7).ToString()), dRowProduto.ItemArray.GetValue(0).ToString(), dRowProduto.ItemArray.GetValue(12).ToString(), dRowProduto.ItemArray.GetValue(3).ToString(),
                                                              decimal.Parse(dRowProduto.ItemArray.GetValue(1).ToString()), dRowProduto.ItemArray.GetValue(2).ToString(),
                                                              Convert.ToBoolean(dRowProduto.ItemArray.GetValue(6).ToString()), decimal.Parse(dRowProduto.ItemArray.GetValue(5).ToString()),
                                                              dRowProduto.ItemArray.GetValue(4).ToString(), dRowProduto.ItemArray.GetValue(8).ToString(), dRowProduto.ItemArray.GetValue(9).ToString(),
                                                              Convert.ToDateTime(dRowProduto.ItemArray.GetValue(10).ToString()), Convert.ToDateTime(dRowProduto.ItemArray.GetValue(11).ToString()),
                                                              Convert.ToBoolean(dRowProduto.ItemArray.GetValue(13).ToString()), dRowProduto.ItemArray.GetValue(14).ToString());
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            if (cbxGrupoProduto.SelectedValue.ToString() != "0")
            {
                Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto, !chkProdutosInativos.Checked, " and CodGrupo=" + cbxGrupoProduto.SelectedValue.ToString(), rowIndex);
            }
            else
            {
                Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto, !chkProdutosInativos.Checked, "", rowIndex);
            }

        }
        private void DeletarProduto(object sender, EventArgs e)
        {
            try
            {
                if (produtosGridView.SelectedRows.Count > 0)
                {
                    if (Utils.MessageBoxQuestion("Deseja Excluir o Produto " + produtosGridView.SelectedCells[1].Value))
                    {
                        int Codigo = int.Parse(this.produtosGridView.CurrentRow.Cells["Codigo"].Value.ToString());
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
            catch (Exception erro)
            {
                if (erro.Message.Contains("A instrução DELETE conflitou com a restrição do REFERENCE"))
                {
                    MessageBox.Show(Bibliotecas.cException + "Produto já foi vendido e não pode ser excluído,você pode desativa-lo");
                    return;
                }
                MessageBox.Show(Bibliotecas.cException + erro.Message);
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
        private void TotalizaPedidos()
        {
            if (Sessions.returnEmpresa.CNPJ == Bibliotecas.cBuris)
            {
                return;
            }
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
                TaxaServico = Utils.RetornaTaxaPorCliente(int.Parse(DvPedido.ItemArray.GetValue(2).ToString()), 0);
            }

            string strTrocoPara = DvPedido.ItemArray.GetValue(4).ToString();
            string strTotalPedido = DvPedido.ItemArray.GetValue(3).ToString();
            string strDescPedido = DvPedido.ItemArray.GetValue(14).ToString();
            string strTroco = "0,00";
            decimal MargemGarcon = decimal.Parse(DvPedido.ItemArray.GetValue(16).ToString());
            int intCodVendedor = int.Parse(DvPedido.ItemArray.GetValue(17).ToString());
            string iObservacao = DvPedido.ItemArray.GetValue(18).ToString();
            if (strTrocoPara != "0,00" && strTrocoPara != "0")
            {
                strTroco = Convert.ToString(decimal.Parse(strTrocoPara) - decimal.Parse(strTotalPedido));
            }
            int intCodEndereco = int.Parse(DvPedido.ItemArray.GetValue(20).ToString());
            frmCadastrarPedido frm = new frmCadastrarPedido(false, strDescPedido, DvPedido.ItemArray.GetValue(9).ToString(),
                                      strTroco, TaxaServico, true, Convert.ToDateTime(DvPedido.ItemArray.GetValue(7).ToString()),
                                      int.Parse(DvPedido.ItemArray.GetValue(1).ToString()), int.Parse(DvPedido.ItemArray.GetValue(2).ToString()), DvPedido.ItemArray.GetValue(4).ToString(),
                                      DvPedido.ItemArray.GetValue(5).ToString(), DvPedido.ItemArray.GetValue(8).ToString(), DvPedido.ItemArray.GetValue(9).ToString(), null,
                                      decimal.Parse(strTotalPedido), MargemGarcon, intCodVendedor, iObservacao, intCodEndereco);
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
                MenuItem InformaEntregador = new MenuItem(" 3 - Informar Entregador");
                MenuItem StatusConcluido = new MenuItem(" 4 - Concluído/Entregue");
                MenuItem ImprimeConferenciaMesa = new MenuItem(" Imprimir Conferencia desta Mesa");
                MenuItem PedidoONline = new MenuItem(" X - Status Pedido");
                MenuItem TransMesa = new MenuItem("5 - Transferir Mesa");

                if (pedidosGridView.Rows.Count > 0)
                {
                    PedidoONline = new MenuItem(" X - Status Pedido");

                    DataSet dsStatus = con.SelectAll("PedidoStatus", "", "select * from PedidoStatus");
                    MenuItem StatusNaCozinha = new MenuItem(" 0 - Pedido na Cozinha");
                    MenuItem StatusNaEntrega = new MenuItem(" 1 - Saiu pra entrega");
                    MenuItem StatusCancelado = new MenuItem(" 2 - Cancelado");

                    PedidoONline.MenuItems.Add(StatusNaCozinha);
                    PedidoONline.MenuItems.Add(StatusNaEntrega);
                    PedidoONline.MenuItems.Add(StatusCancelado);

                    StatusCancelado.Enabled = Sessions.retunrUsuario.CancelaPedidosSN;
                    StatusNaCozinha.Click += PedidoNaCozinha;
                    StatusNaEntrega.Click += PedidoNaEntrega;
                }


                CancPedido.Click += CancelarPedidos;
                FinalizarPed.Click += FinalizaCancelaPEdidos;
                FinalizaSelecionados.Click += FinalizaTodos;
                InformaEntregador.Click += SelecionaBoy;
                StatusConcluido.Click += PEdidoConcluido;
                

                m.MenuItems.Add(CancPedido);
                m.MenuItems.Add(FinalizarPed);
                m.MenuItems.Add(FinalizaSelecionados);
                m.MenuItems.Add(InformaEntregador);
                m.MenuItems.Add(StatusConcluido);

                if (Utils.VerificaSeEmesa(int.Parse(pedidosGridView.CurrentRow.Cells[0].Value.ToString())))
                {
                    m.MenuItems.Add(TransMesa);
                    TransMesa.Click += TransfereMesa;
                }
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
                            int iCodPessoa = int.Parse(dRowPedido.ItemArray.GetValue(2).ToString());
                            CodPedidoWS = VerificaPedidoOnline(codigo);
                            if (CodPedidoWS > 0)
                            {
                                AlteraStatusPedido(CodPedidoWS, 5, iCodPessoa);
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
                            Utils.ControlaEventos("BaixaPed", this.Name);
                            con.SinalizarPedidoConcluido("Pedido", "spSinalizarPedidoConcluido", codigo, 1);

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
        private void ExecutaFinalizacao()
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
                    int intCodPessoa = int.Parse(dRowPedido.ItemArray.GetValue(2).ToString());
                    CodPedidoWS = VerificaPedidoOnline(codigo);
                    if (CodPedidoWS > 0)
                    {
                        AlteraStatusPedido(CodPedidoWS, StatusPedido.cPedidoConcluido, intCodPessoa);
                        MessageBox.Show("Atualização realizada com sucesso, pedido entregue");
                    }

                    iCodMesa = int.Parse(dRowPedido.ItemArray.GetValue(12).ToString());

                    dblTotalPedido = decimal.Parse(dRowPedido.ItemArray.GetValue(3).ToString());
                    string iTipo = dRowPedido.ItemArray.GetValue(8).ToString();

                    //Caso O Pedido for Entrega e o Sistema controle 
                    //os entregadores ele abre a tela pedido pra informar quem entregou
                    if (Sessions.returnConfig.ControlaEntregador && iTipo == "0 - Entrega")
                    {
                        InformaMotoboyPedido(codigo);
                    }

                    // Caso o pedido for mesa ele altera o Status da Mesa
                    Boolean bFinalizouViaMesa = false;
                    if (ControlaMesas && iCodMesa != 0)
                    {
                        frmFinalizacaoPedido frm = new frmFinalizacaoPedido(dblTotalPedido, false, codigo, iCodMesa);
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.ShowDialog();

                        if (frm.boolFinalizou)
                        {
                            bFinalizouViaMesa = true;
                            AtualizaStatusMesa(iCodMesa, Bibliotecas.cStatuMesaLiberada);
                        }
                        else
                        {
                            return;
                        }

                    }

                    // Grava Débito caso o Tipo de Pagamento gerar financeiro 
                    string strFormaPagamento = dRowPedido.ItemArray.GetValue(5).ToString();
                    Utils.GeraHistorico(DateTime.Now, int.Parse(dRowPedido.ItemArray.GetValue(2).ToString()), dblTotalPedido, Sessions.retunrUsuario.Codigo, "Pedido Nº " + codigo, 'D', strFormaPagamento);

                    // Grava Movimento De Caixa
                    GravaMOvimentoCaixa(strFormaPagamento, dblTotalPedido, codigo);

                    // Atualiza Ticket Fidelidade
                    AtualizarFidelidade(intCodPessoa);

                    // Enfim finaliza o Pedido
                    Utils.ControlaEventos("BaixaPed", this.Name);
                    if (!bFinalizouViaMesa)
                    {
                        con.SinalizarPedidoConcluido("Pedido", "spSinalizarPedidoConcluido", codigo, 1);
                    }

                    //Atualiza Grid
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
        private void TransfereMesa(object sender, EventArgs e)
        {
            frmTransfereMesa frmMe = new frmTransfereMesa();
            frmMe.ShowDialog();
        }

        private void PEdidoConcluido(object sender, EventArgs e)
        {
            int intCodPedido = int.Parse(pedidosGridView.CurrentRow.Cells["Codigo"].Value.ToString());
            con.AtualizaSituacao(intCodPedido, Sessions.retunrUsuario.Codigo, StatusPedido.cPedidoConcluido, pedidosGridView);

        }
        private void FinalizaCancelaPEdidos(object sender, EventArgs e)
        {

            if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "FinalizaPedidoSN"))
            {
                if (Utils.MessageBoxQuestion(Bibliotecas.cSolicitarPermissao))
                {
                    frmLiberação frm = new frmLiberação("FinalizaPedidoSN");
                    //frm.ShowDialog();
                    if (frm.Autorizacao)
                    {
                        ExecutaFinalizacao();
                        frm.Close();
                    }
                    else
                    {
                        MessageBox.Show(Bibliotecas.cUsuarioSemPermissao);
                    }
                }
            }
            else
            {
                ExecutaFinalizacao();
            }
        }
        private Boolean PermiteEntregador(int iCodPedido)
        {
            Boolean iRetur = false;
            DataRow dROw = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", iCodPedido).Tables[0].Rows[0];
            if (dROw.ItemArray.GetValue(8).ToString() == "0 - Entrega")
            {
                iRetur = true;
            }

            return iRetur;
        }

        private void SelecionaBoy(object sender, EventArgs e)
        {
            int intCodPedido = int.Parse(pedidosGridView.CurrentRow.Cells["Codigo"].Value.ToString());


            InformaMotoboyPedido(intCodPedido);

        }
        private Boolean VerificaSeMotoboyFoiInformado(int iCodPedido)
        {
            Boolean iReturn = false;
            int teste = int.Parse(con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", iCodPedido).
                Tables[0].Rows[0].ItemArray.GetValue(15).ToString());
            if (teste > 0)
            {
                iReturn = true;
            }

            if (iReturn)
            {
                iReturn = Utils.MessageBoxQuestion("Motoboy já foi informado nesse pedido deseja alterar?");
            }
            return iReturn;
        }
        private void InformaMotoboyPedido(int iCodPedido)
        {
            try
            {
                if (!PermiteEntregador(iCodPedido))
                {
                    MessageBox.Show("Este pedido não permite entregadores");
                    return;
                }

                if (VerificaSeMotoboyFoiInformado(iCodPedido))
                {
                    return;
                }
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
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
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
                    CodUser = Sessions.retunrUsuario.Codigo,
                    Turno = Sessions.retunrUsuario.Turno

                };
                con.Insert("spInserirMovimentoCaixa", caixa);

            }
            catch (Exception erro)
            {

                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        //private void CancelarPedidos(object sender, EventArgs e)
        //{
        //    try
        //    {


        //    }
        //    catch (Exception ER)
        //    {
        //        MessageBox.Show("Não foi possivel **CANCELAR O PEDIDO**" + ER.Message);
        //    }
        //}
        private void CancelarPedidos(object sender, EventArgs e)
        {
            try
            {
                if (Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "CancelaPedidosSN"))
                {
                    ExecutaCancelamento(Utils.intCodUserAutorizador);
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
                CodPedidoWS = VerificaPedidoOnline(intCodPedido);
                if (CodPedidoWS > 0)
                {
                    AlteraStatusPedido(CodPedidoWS, StatusPedido.cPedidoNaCozinha, RetornaPessoa(intCodPedido));
                }

                con.AtualizaSituacao(intCodPedido, Sessions.retunrUsuario.Codigo, StatusPedido.cPedidoNaCozinha, pedidosGridView);

            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possivel alterar o status do Pedido " + erro.Message);
            }

        }
        private int RetornaPessoa(int iCodPedido)
        {
            return int.Parse(con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", iCodPedido).Tables[0].Rows[0].ItemArray.GetValue(2).ToString());

        }
        private void PedidoNaEntrega(object sender, EventArgs e)
        {
            try
            {
                int intCodPedido = int.Parse(pedidosGridView.SelectedRows[rowIndex].Cells["Codigo"].Value.ToString());
                CodPedidoWS = VerificaPedidoOnline(intCodPedido);
                if (CodPedidoWS > 0)
                {

                    AlteraStatusPedido(CodPedidoWS, StatusPedido.cPedidoNaEntrega, RetornaPessoa(intCodPedido));
                }
                if (Sessions.returnConfig.ControlaEntregador)
                {
                    InformaMotoboyPedido(intCodPedido);
                }

                con.AtualizaSituacao(intCodPedido, Sessions.retunrUsuario.Codigo, StatusPedido.cPedidoNaEntrega, pedidosGridView);
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
                iParamToken = Utils.CriptografarArquivo("xsistemas", false);
            }
            catch (Exception e)
            {

                MessageBox.Show("Geração do Token de validação " + e.Message);
            }

        }

        private void AlteraStatusPedido(int iCodPedidows, int iStatus, int iIdCliente = 0)
        {
            GerarToken();
            RestClient client = new RestClient(cUrlWs);
            RestRequest request = new RestRequest("ws/pedidos/status", Method.POST);
            request.AddParameter("idPedido", iCodPedidows);
            request.AddParameter("idStatus", iStatus);
            request.AddParameter("token", iParamToken);
            RestResponse response = (RestResponse)client.Execute(request);
            OneSignal on = new OneSignal();

            on.BuscaCliente(iIdCliente, iStatus);
            if (response.Content.Contains("true"))
            {
                MessageBox.Show("Alteração realizada com sucesso");

            }
            else
            {
                MessageBox.Show("Alteração não pode ser realizada, favor tentar novamente mais tarde");
            }


        }
        private void AbrirPedido(int CodPessoa, int CodEnde = 0)
        {
            try
            {
                int intCodEndereco = int.Parse(con.SelectRegistroPorCodigo("Pessoa", "spObterPessoaPorCodigo", CodPessoa).Tables[0].Rows[0].ItemArray.GetValue(20).ToString());
                if (Sessions.returnConfig.RepeteUltimoPedido)
                {
                    ExecutaRepeticaoPedido(CodPessoa, intCodEndereco);
                }
                else
                {
                    decimal TaxaServico = Utils.RetornaTaxaPorCliente(CodPessoa, intCodEndereco);
                    frmCadastrarPedido frm = new frmCadastrarPedido(false, "0,00", "0,00", "0,00",
                                                                    TaxaServico, false, DateTime.Now, 0,
                                                                    CodPessoa,
                                                                        "0,00", "", "", "", null, 0.00M, 0, 0, "", intCodEndereco);
                    frm.ShowDialog();
                }

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
                if (!Utils.CaixaAberto(DateTime.Now, Sessions.retunrUsuario.CaixaLogado, Sessions.retunrUsuario.Turno))
                {
                    return;
                }
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
            if (Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "VisualizaDadosClienteSN"))
            {
                if (clientesGridView.SelectedCells.Count > 0)
                {
                    DataSet dsPessoa = con.SelectRegistroPorCodigo("Pessoa", "spObterPessoaPorCodigo", int.Parse(clientesGridView.SelectedCells[0].Value.ToString()));
                    DataRow dRowPessoa = dsPessoa.Tables["Pessoa"].Rows[0];

                    frmCadastroCliente frm = new frmCadastroCliente(int.Parse(dRowPessoa.ItemArray.GetValue(0).ToString()), dRowPessoa.ItemArray.GetValue(1).ToString(), dRowPessoa.ItemArray.GetValue(10).ToString(),
                                                                      dRowPessoa.ItemArray.GetValue(11).ToString(), dRowPessoa.ItemArray.GetValue(2).ToString(), dRowPessoa.ItemArray.GetValue(3).ToString(), dRowPessoa.ItemArray.GetValue(9).ToString()
                                                                      , dRowPessoa.ItemArray.GetValue(4).ToString(), dRowPessoa.ItemArray.GetValue(5).ToString(), dRowPessoa.ItemArray.GetValue(6).ToString(), dRowPessoa.ItemArray.GetValue(7).ToString()
                                                                      , dRowPessoa.ItemArray.GetValue(8).ToString(), int.Parse(dRowPessoa.ItemArray.GetValue(14).ToString()), dRowPessoa.ItemArray.GetValue(15).ToString(), dRowPessoa.ItemArray.GetValue(12).ToString(),
                                                                      dRowPessoa.ItemArray.GetValue(16).ToString(), dRowPessoa.ItemArray.GetValue(19).ToString());


                    Utils.PopulaGrid_Novo("Pessoa", clientesGridView, Sessions.SqlPessoa);
                }

            }
        }

        private void BuscarCliente(object sender, KeyEventArgs e)
        {
            string propriedade = cbxBuscarPor.Text.ToString();
            string valor = txtBurcarValor.Text;
            DataSet dsResult = null;
            if (propriedade.Equals("Telefone") && e.KeyData == Keys.Enter)
            {
                dsResult = con.SelectPessoaPorNome(valor, Sessions.SqlPessoa, "Telefone");
            }
            else if (propriedade.Equals("Nome") || propriedade.Equals(""))
            {
                dsResult = con.SelectPessoaPorNome(valor, Sessions.SqlPessoa, "Nome");
            }
            else if (propriedade.Equals("Endereço"))
            {
                dsResult = con.SelectPessoaPorNome(valor, Sessions.SqlPessoa, "Endereco");
            }


            this.clientesGridView.DataSource = null;
            this.clientesGridView.AutoGenerateColumns = true;
            this.clientesGridView.DataSource = dsResult;
            this.clientesGridView.DataMember = "Pessoa";
            //}
            //else
            //{
            //    MessageBox.Show("Informe Nome ou Telefone.");
            //    Utils.PopulaGrid_Novo("Pessoa", clientesGridView, Sessions.SqlPessoa);
            //    // Utils.PopularGrid("Pessoas", this.clientesGridView);


            //}
            // }

            //}
            //else
            //{
            //    MessageBox.Show("Informe se a busca é por Nome ou Telefone");
            //}
            //  }
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

                if (dsProduto.Tables[0].Rows.Count > 0 || dsOpcao.Tables[0].Rows.Count > 0 || dsProdutOpcao.Tables[0].Rows.Count > 0 || dsGrupo.Tables[0].Rows.Count > 0)
                {
                    //    DialogResult resposta = MessageBox.Show("Há registros que precisam ser sincronizados com o servidor , deseja fazer isso agora?", "[xSistemas]", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (Utils.MessageBoxQuestion("Há registros que precisam ser sincronizados com o servidor, deseja fazer isso agora ? "))
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
            // VerificaRegistroASincronizar();
            con.BackupBanco(strServidor, strBanco, strCaminhoBkp);

            this.Dispose();
            con.Close();
            Utils.Kill();

        }

        private void frmPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12 || e.KeyCode == Keys.Enter)
            {
                if (txbTelefoneCliente.Focused)
                {
                    BuscarCliente(txbTelefoneCliente.Text);
                }
                else
                {
                    txtBurcarValor.Focus();
                }

            }
            else
            if (e.KeyCode == Keys.F11)
            {
                ImpressaRapida();
            }
            else
            if (produtosGridView.SelectedRows.Count > 0 && e.KeyCode == Keys.Enter)
            {
                CarregaProduto(int.Parse(produtosGridView.SelectedCells[0].Value.ToString()));
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
            Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto);
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AdministradorSN"))
            {
                frmCadastroUsuario frm = new frmCadastroUsuario();
                frm.ShowDialog();
            }

        }

        private void entregadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastrarEntregador frm = new frmCadastrarEntregador();
            frm.ShowDialog();
        }

        private void formasDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastrarFormaPagamento frm = new frmCadastrarFormaPagamento();
            frm.ShowDialog();
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
            frm.ShowDialog();
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
            if (Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AdministradorSN"))
            {
                frmConfiguracoes frm = new frmConfiguracoes();
                frm.ShowDialog();
            }
        }

        private void impressãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AdministradorSN"))
            {
                frmConfigurarImpressao frm = new frmConfigurarImpressao();
                frm.ShowDialog();
            }
        }

        private void renovarAtivarSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicencaOFFLINE frm = new frmLicencaOFFLINE();
            frm.ShowDialog();
        }

        private void produtosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AlteraProdutosSN"))
            {
                frmAlteracaoProduto frm = new frmAlteracaoProduto();
                frm.ShowDialog();
            }

        }

        private void opçãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AlteraProdutosSN"))
            {
                frmAlterarOpcao frm = new frmAlterarOpcao();
                frm.ShowDialog();
            }
        }

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAlteracaoCliente frm = new frmAlteracaoCliente();
            frm.ShowDialog();
        }

        private void lançamentoAvulsoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AbreFechaCaixaSN"))
            {
                frmLancamentoCaixa frmLan = new frmLancamentoCaixa();
                frmLan.ShowDialog();
            }
        }

        private void movimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AbreFechaCaixaSN"))
            {
                frmCaixaMovimento frm = new frmCaixaMovimento();
                frm.ShowDialog();
            }
        }

        private void cadastroCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadCaixa frm = new frmCadCaixa();
            frm.ShowDialog();
        }

        private void fecharCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AbreFechaCaixaSN"))
            {
                frmCaixaFechamento frm = new frmCaixaFechamento();
                frm.ShowDialog();
            }
        }

        private void aberturaCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AbreFechaCaixaSN"))
            {
                frmAberturaCaixa frm = new frmAberturaCaixa();
                frm.ShowDialog();
            }
        }

        private void sincronizaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AlteraProdutosSN"))
            {
                frmSincronizacao frm = new frmSincronizacao();
                frm.ShowDialog();
            }
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
        private async void ImpressaoAutomatica(int iCodPedido, int iCodGrupo, string iNomeImpressora)
        {
            try
            {
                itemsPedido = Utils.ItensSelect(iCodPedido);
                for (int i = 0; i < itemsPedido.Tables[0].Rows.Count; i++)
                {
                    string lRetorno = "";
                    Boolean imprimirAgora = false;
                    lRetorno = Utils.ImpressaMesaNova(iCodPedido, iCodGrupo, false, 1, iNomeImpressora, imprimirAgora);
                    for (int intFor = 0; intFor < itemsPedido.Tables[0].Rows.Count; intFor++)
                    {
                        AtualizaItemsImpresso Atualiza = new AtualizaItemsImpresso();
                        Atualiza.CodPedido = iCodPedido;
                        Atualiza.CodProduto = itemsPedido.Tables["ItemsPedido"].Rows[intFor].Field<int>("CodProduto");
                        Atualiza.ImpressoSN = true;
                        con.Update("spInformaItemImpresso", Atualiza);
                    }
                    itemsPedido = Utils.ItensSelect(iCodPedido);
                }



            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel imprimir o Item da Mesa verificar a impressora" + erro.Message);
            }
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


        //}
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
                string iSql = " select distinct (IT.CodProduto) ,PE.*,  " +
                              " CodGrupo, " +
                              " NomeImpressora " +
                              " from Pedido PE " +
                              " join ItemsPedido IT ON PE.Codigo = IT.CodPedido and IT.IMPRESSOSN = 0 " +
                              " left join Produto P on P.Codigo = It.CodProduto " +
                              " LEFT JOIN GRUPO G ON G.Codigo = P.CodGrupo " +
                              " where PE.CodigoMesa > 0 and Finalizado = 0 ";


                DataSet dsItemsNaoImpresso = con.SelectAll("ItemsPedido", "", iSql);

                if (dsItemsNaoImpresso.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                DataRow dRowPedido = dsItemsNaoImpresso.Tables[0].Rows[0];
                Boolean iMesa = dRowPedido.ItemArray.GetValue(8).ToString() == "1 - Mesa";
                if (iMesa && chkGerenciaImpressao.Checked && dsItemsNaoImpresso.Tables[0].Rows.Count > 0)
                {
                    int CodPedido = int.Parse(dRowPedido.ItemArray.GetValue(1).ToString());
                    int CodGrupo = int.Parse(dRowPedido.ItemArray.GetValue(20).ToString());
                    string iNomeImpressora = dRowPedido.ItemArray.GetValue(21).ToString();
                    ImpressaoAutomatica(CodPedido, CodGrupo, iNomeImpressora);
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
            frm.ShowDialog();
        }

        private void cbxGrupoProduto_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbxGrupoProduto.SelectedValue.ToString() != "0")
            {

                Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto, !chkProdutosInativos.Checked, " and CodGrupo=" + cbxGrupoProduto.SelectedValue.ToString());
            }
        }

        private void produtosGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void chkSEmFotos_CheckedChanged(object sender, EventArgs e)
        {
            Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto, !chkSEmFotos.Checked);
        }

        private void notificaçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNotificacao frm = new frmNotificacao();
            frm.Show();
        }

        private void alterarSenhaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAlteraSenha frm = new frmAlteraSenha();
            frm.Show();
        }

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //    relatórioToolStripMenuItem.Enabled = Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AcessaRelatoriosSN");

        }

        private void relatórioToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            //   relatórioToolStripMenuItem = Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AcessaRelatoriosSN");
        }

        private void vendasPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportVendasPorVendedor frm = new frmReportVendasPorVendedor();
            frm.Show();
        }

        private void rotasDeEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHorariosEntrega frm = new frmHorariosEntrega();
            frm.Show();
        }

        private void cancelamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportCancelamentos frm = new frmReportCancelamentos();
            frm.ShowDialog();
        }

        private void porRegiãoComTaxaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEntregasPorRegiao frm = new frmEntregasPorRegiao();
            frm.ShowDialog();
        }

        private void clientesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbxGrupoProduto_DropDown(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxGrupoProduto, "NomeGrupo", "Codigo", "Grupo", "spObterGrupoAtivo");

        }

        private void crediárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDebitosPessoa frm = new frmDebitosPessoa();
            frm.Show();
        }

        private void porMotoboyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportEntregasMotoboy frm = new frmReportEntregasMotoboy();
            frm.Show();
        }

        private void porPessoaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void melhoresClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReporVendasPorCliente frm = new frmReporVendasPorCliente();
            frm.Show();
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AdministradorSN"))
            {
                return;
            }
            frmConsultaPedido frmCons = new frmConsultaPedido();
            frmCons.Show();
        }

        private void relatórioToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            relatórioToolStripMenuItem.DropDown.Enabled = Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AcessaRelatoriosSN");
        }

        private void vendasPorAtendenteVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReporVendasPorVendedor frmVe = new frmReporVendasPorVendedor();
            frmVe.Show();
        }
    }
}
