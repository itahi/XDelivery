using DexComanda.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DexComanda.Relatorios;
using DexComanda.Cadastros;
using System.Drawing.Printing;
using DexComanda.Operações;
using DexComanda.Operações.Financeiro;
using System.Configuration;
using DexComanda.Integração;
using System.IO;
//using DexComanda.Relatorios.Fechamentos;

namespace DexComanda
{

    public partial class Main : Form
    {
        private Conexao con;
        private string line = null;
        private CancelarPedido cancelPedid;
        private Font printFont;
        private List<ItemPedido> items;
        private int rowIndex;
        private List<Produto> Produtos;
        private Main parentWindow;
        private Main parentMain;
        private bool DescontoDia = Sessions.returnConfig.DescontoDiaSemana;
        private bool ControlaMesas = Sessions.returnConfig.UsaControleMesa;
        public DateTime DataPedido;
        private bool ControlaLoginSenha = Sessions.returnConfig.UsaLoginSenha;
        private int QtdPedidosAnterior;
        private int lTotalPedidosNaGrid;
        private DataSet DadosRetorno;
        private DataRow LinhaRetorno;
        private bool RepeteUltimoPedido = Sessions.returnConfig.RepeteUltimoPedido;
        private int iPosicao = 0;
        private int PedidosEmAberto = 0;
        private int PedidosContados;
        private DataSet ListaPedidos;
        private int iCaixaAberto;
        // Permissões de Usuarios
        //private bool UserAdmin = Sessions.returnUsuario.AdministradorSN;
        //private bool UserCancelaPedido = Sessions.returnUsuario.CancelaPedidosSN;
        //private int UserLogado = Sessions.returnUsuario.Codigo;
        //private bool UserAlteraProdutos = Sessions.returnUsuario.AlteraProdutosSN;
        //private bool UserDescPedido = Sessions.returnUsuario.DescontoPedidoSN;


        public Main()
        {

            InitializeComponent();
            cancelPedid = new CancelarPedido();
            //  ListaClientes();
            //cbxFiltroTipo.Visible = Sessions.returnConfig.UsaControleMesa;
        }

        public Main(Main parent)
        {
            InitializeComponent();
            cancelPedid = new CancelarPedido();
            this.parentMain = parent;

        }
        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdicionarGrupo frm = new frmAdicionarGrupo();
            frm.ShowDialog();
        }
        private void HistoricoCancelamentos(int iCodPessoa)
        {
            int intQuantidadeCancelamento = con.SelectRegistroPorCodigo("HistoricoCancelamentos", "spObterCancelamentoPorPessoa", iCodPessoa).Tables[0].Rows.Count;
            if (intQuantidadeCancelamento > 0)
            {
                DialogResult resultado = MessageBox.Show("Cliente possui " + intQuantidadeCancelamento + "  Cancelamento(s) Deseja visualizar ?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    frmExibeCancelamentos frm = new frmExibeCancelamentos(iCodPessoa);
                    frm.ShowDialog();
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
       
            if (Sessions.returnUsuario != null)
            {
                con = new Conexao();

                int iNumeroCaixa = Sessions.returnUsuario.CaixaLogado;
                iCaixaAberto = con.SelectRegistroPorDataCodigo("Caixa", "spObterDadosCaixa", DateTime.Now, iNumeroCaixa).Tables["Caixa"].Rows.Count;

                if (Utils.CaixaAberto(DateTime.Now, iNumeroCaixa))
                {
                    btnConsultarTelefone.Enabled = true;
                    aberturaCaixaToolStripMenuItem.Enabled = false;
                    lblCaixa.Text = "Caixa Aberto";
                }

            }
            else
            {
                iCaixaAberto = 1;
                btnConsultarTelefone.Enabled = true;
                lblCaixa.Text = "Caixa Aberto";
            }


            this.txbTelefoneCliente.Focus();
            con = new Conexao();
            if (!DescontoDia)
            {
                Utils.PopularGrid("Produto", this.produtosGridView, "spObterProdutoSemDesconto");

            }
            else
            {
                Utils.PopularGrid("Produto", this.produtosGridView);
            }

            Utils.PopularGrid("Pedido", this.pedidosGridView);
            // Altera a cor da linha pra identificar o pedido
            // AlteraCorLinhas();

            Utils.PopularGrid("Pessoas", this.clientesGridView);

            MontaMenu();

        }


        private void MontaMenu() // Monta o menu de opções
        {
            // Menu Visivel
            //relatórioToolStripMenuItem.Enabled = Sessions.returnUsuario.AcessaRelatoriosSN;
            entregadorToolStripMenuItem.Visible = Sessions.returnConfig.ControlaEntregador;
            envioDeSMSToolStripMenuItem.Enabled = Sessions.returnConfig.EnviaSMS;
            alterarSenhaToolStripMenuItem.Visible = Sessions.returnConfig.UsaLoginSenha;
            usuáriosToolStripMenuItem.Visible = Sessions.returnConfig.UsaLoginSenha;
            entregasPorMotoboyToolStripMenuItem.Visible = Sessions.returnConfig.ControlaEntregador;
            entregadorToolStripMenuItem.Visible = Sessions.returnConfig.ControlaEntregador;

            if (Sessions.returnUsuario != null)
            {
                this.txtUsuarioLogado.Text = Sessions.returnUsuario.Nome;
                usuáriosToolStripMenuItem.Enabled = Sessions.retunrUsuario.AdministradorSN;
                relatórioToolStripMenuItem.Enabled = Sessions.retunrUsuario.AcessaRelatoriosSN;
                configuraçãoToolStripMenuItem.Enabled = Sessions.retunrUsuario.AdministradorSN;
                usuáriosToolStripMenuItem.Visible = Sessions.returnConfig.UsaLoginSenha;
                operaçõesToolStripMenuItem.Enabled = Sessions.retunrUsuario.AdministradorSN;
                FinanceiroToolStripMenuItem.Enabled = iCaixaAberto > 0;
            }

        }
        private void CarregaPedido(int iCodPedido)
        {
            DataSet DsPedido;
            DataRow DvPedido;
            DsPedido = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", iCodPedido);
            DvPedido = DsPedido.Tables[0].Rows[0];

            decimal TaxaServico = Utils.RetornaTaxaPorCliente(int.Parse(DvPedido.ItemArray.GetValue(2).ToString()), con);

            //frmCadastrarPedido(Boolean iPedidoRepetio, string iNumeMesa, string iTroco, decimal iTaxaEntrega, Boolean IniciaTempo, 
            //DateTime DataPedido, int CodigoPedido, int CodPessoa, string tPara,
            //string fPagamento, string TipoPedido, string MesaBalcao, Main parent)
            string strTrocoPara = DvPedido.ItemArray.GetValue(4).ToString();
            string strTotalPedido = DvPedido.ItemArray.GetValue(3).ToString();
            string strDescPedido = DvPedido.ItemArray.GetValue(14).ToString();
            string strTroco = "0,00";
            if (strTrocoPara != "0.00")
            {
                strTroco = Convert.ToString(decimal.Parse(strTrocoPara) - decimal.Parse(strTotalPedido));
            }

            frmCadastrarPedido frm = new frmCadastrarPedido(false, strDescPedido, DvPedido.ItemArray.GetValue(9).ToString(),
                                      strTroco, TaxaServico, true, Convert.ToDateTime(DvPedido.ItemArray.GetValue(7).ToString()),
                                      int.Parse(DvPedido.ItemArray.GetValue(1).ToString()), int.Parse(DvPedido.ItemArray.GetValue(2).ToString()), DvPedido.ItemArray.GetValue(4).ToString(),
                                      DvPedido.ItemArray.GetValue(5).ToString(), DvPedido.ItemArray.GetValue(8).ToString(), DvPedido.ItemArray.GetValue(9).ToString(), this, decimal.Parse(strTotalPedido));
            frm.ShowDialog();
        }
        private void cadastrarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCadastroCliente frm = new frmCadastroCliente(this);
            frm.ShowDialog();
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastrarProduto frm = new frmCadastrarProduto(this);
            frm.ShowDialog();
        }

        public void PopularGrid(Boolean iFechandoRegistro, string table, DataGridView gridView)
        {

            DadosRetorno = con.SelectAll(table, "spObter" + table);

            if (DadosRetorno.Tables[table].Rows.Count > 0 || iFechandoRegistro)
            {
                gridView.DataSource = null;
                gridView.AutoGenerateColumns = true;
                gridView.DataSource = DadosRetorno;
                gridView.DataMember = table;
            }


            //txbTelefoneCliente.Text = Convert.ToString(0); 
            con.Close();
        }

        public DataSet PopularGrid(Boolean iFechandoRegistro, string table, DataGridView gridView, string spName)
        {

            DadosRetorno = con.SelectAll(table, spName);

            if (DadosRetorno.Tables[table].Rows.Count > 0 || iFechandoRegistro)
            {
                gridView.DataSource = null;
                gridView.AutoGenerateColumns = true;
                gridView.DataSource = DadosRetorno;
                gridView.DataMember = table;
            }

            return DadosRetorno;

        }


        public void ConsultarCliente(object sender, EventArgs e)
        {
            // Integração.EnviaSMS_LOCASMS Enviar = new Integração.EnviaSMS_LOCASMS();
            // Enviar.EnviaSMSLista(clientesGridView, "27981667827", "549636", "MEnsage");
            BuscarCliente();
        }

        public void ConsultarClienteParaEdicao(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbxBuscarPor.Text != "")
                {
                    string propriedade = cbxBuscarPor.SelectedItem.ToString();
                    string valor = txtBurcarValor.Text;

                    if (!propriedade.Equals(""))
                    {
                        if (!valor.Equals(""))
                        {
                            string query = null;
                            DBExpertDataSet dbExpert = new DBExpertDataSet();
                            DataSet result = con.SelectAll("Pessoa", "spObterPessoas");


                            if (propriedade.Equals("Nome"))
                            {
                                query = "Nome LIKE '%" + valor + "%'";
                            }
                            else
                            {
                                query = "Telefone ='" + valor + "'";
                            }

                            var dv = result.Tables[0].DefaultView;
                            dv.RowFilter = query;

                            var newDS = new DataSet("Pessoa");
                            var newDT = dv.ToTable();

                            newDS.Tables.Add(newDT);

                            this.clientesGridView.DataSource = null;
                            this.clientesGridView.AutoGenerateColumns = true;
                            this.clientesGridView.DataSource = newDS;
                            this.clientesGridView.DataMember = "Pessoa";
                        }
                        else
                        {
                            MessageBox.Show("Informe Nome ou Telefone.");
                            Utils.PopularGrid("Pessoas", this.clientesGridView);


                        }
                    }

                }
                else
                {
                    MessageBox.Show("Informe se a busca é por Nome ou Telefone");
                }
            }

        }


        private void BuscarCliente()
        {
            try
            {
                if (Sessions.retunrUsuario != null)
                {
                    if (iCaixaAberto > 0)
                    {
                        AbreTelaPedido();
                    }
                    else
                    {
                        MessageBox.Show("Caixa precisa estar aberto", "[XSistemas] Aviso");
                    }

                }
                else
                {
                    AbreTelaPedido();
                }


            }

            catch (Exception errobusca)
            {

                MessageBox.Show(errobusca.Message, "Erro");
            }
        }

        private void AbreTelaPedido()
        {

            if (txbTelefoneCliente.Text != "")
            {
                var telefone = this.txbTelefoneCliente.Text;
                //  DBExpertDataSet dbExpert = new DBExpertDataSet();

                DataSet pessoaTelefone = con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", telefone);

                if ((pessoaTelefone.Tables["Pessoa"].Rows.Count == 0))
                {
                    DialogResult resultado = MessageBox.Show("Telefone não encontrado! Deseja cadastrar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        frmCadastroCliente frm = new frmCadastroCliente(this);
                        frm.txtTelefone.Text = telefone;


                        frm.ShowDialog();
                    }
                }
                else if ((pessoaTelefone.Tables["Pessoa"].Rows.Count == 1))
                {

                    DataSet Pessoa = con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", telefone);
                    DataRow dRow = Pessoa.Tables["Pessoa"].Rows[0];

                    int CodigoPessoa = int.Parse(dRow.ItemArray.GetValue(0).ToString());

                    if (Sessions.returnConfig.RegistraCancelamentos)
                    {
                        HistoricoCancelamentos(CodigoPessoa);
                    }
                    this.txtNome.Text = dRow.ItemArray.GetValue(1).ToString();
                    this.txtEndereco.Text = dRow.ItemArray.GetValue(2).ToString();


                    if (dRow.ItemArray.GetValue(7).ToString() != "")
                    {
                        this.txtEndereco.Text = this.txtEndereco.Text + ", Nº " + dRow.ItemArray.GetValue(7).ToString();
                    }

                    this.txtBairro.Text = dRow.ItemArray.GetValue(3).ToString();
                    this.txtCidade.Text = dRow.ItemArray.GetValue(4).ToString();
                    this.txtPontoReferencia.Text = dRow.ItemArray.GetValue(5).ToString();


                    if (RepeteUltimoPedido)
                    {
                        ExecutaRepeticaoPedido(CodigoPessoa);

                    }
                    else
                    {
                        IniciaPedido(CodigoPessoa);
                    }


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
            LimpaCampos();


        }

        private void ExecutaRepeticaoPedido(int iCodPessoa)
        {
            DataRow LinhasPedido;
            int QuantidadePedidos = 0;
            ListaPedidos = con.SelectRegistroPorCodigo("Pedido", "spContaPedidosPorCliente", iCodPessoa);
            LinhasPedido = ListaPedidos.Tables["Pedido"].Rows[0];
            if (LinhasPedido.ItemArray.GetValue(0).ToString() != "0")
            {
                QuantidadePedidos = int.Parse(LinhasPedido.ItemArray.GetValue(0).ToString());
            }

            if (QuantidadePedidos > 0)
            {
                DialogResult resultado = MessageBox.Show("Deseja Repetir o Ultimo Pedido ?", "Dex Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (resultado == DialogResult.Yes)
                {
                    Utils.RepetirUltimoPedido(iCodPessoa, this);
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
        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEnvioEmail frmCadClieForn = new frmEnvioEmail();
            frmCadClieForn.ShowDialog();
        }

        private void IniciaPedido(int CodPessoa)
        {
            var TaxaEntrega = Utils.RetornaTaxaPorCliente(CodPessoa, con);

            frmCadastrarPedido CadPedido = new frmCadastrarPedido(false, "0,00", "", "0.00", TaxaEntrega, false, DateTime.Now, 0, CodPessoa,
                                                                    "0.00", "", "", "", this, 0.00M);
            CadPedido.ShowDialog();
            LimpaCampos();
        }

        public void DeletarGrupo(Object sender, EventArgs e)
        {
            try
            {

                if (produtosGridView.SelectedRows.Count > 0)
                {
                    rowIndex = int.Parse(produtosGridView.SelectedCells[0].Value.ToString());
                    this.produtosGridView.Rows.RemoveAt(rowIndex);

                    con.Delete("spExcluirProduto", rowIndex);
                    Utils.PopularGrid("Produto", parentWindow.pedidosGridView);
                    MessageBox.Show("Item excluído com sucesso.");
                }
                else
                {
                    MessageBox.Show("Selecione o produto para alterar");
                }
            }
            catch (Exception ErroDelete)
            {

                MessageBox.Show("Grupo não pode ser deletado entre em contato com o suporte e informa a seguinte Mensagem " + ErroDelete.Message);
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

        private void empresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClieForn frm = new frmClieForn();
            frm.Show();
        }

        private void ListaClientes(string table, DataGridView gridView)
        {
            this.clientesGridView.DataSource = null;
            this.clientesGridView.AutoGenerateColumns = true;
            this.clientesGridView.DataSource = con.SelectAll("Pessoa", "spObterPessoas");
            this.clientesGridView.DataMember = "Pessoa";
        }

        private void EditarProduto(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (produtosGridView.SelectedCells.Count > 0)
                {
                    var produto = new Produto()
                    {
                        Codigo = int.Parse(produtosGridView.SelectedCells[0].Value.ToString()),
                        Nome = (produtosGridView.SelectedCells[1].Value.ToString()),
                        Descricao = (produtosGridView.SelectedCells[2].Value.ToString()),
                        Preco = decimal.Parse(produtosGridView.SelectedCells[3].Value.ToString()),
                        GrupoProduto = (produtosGridView.SelectedCells[4].Value.ToString()),

                    };
                    if (DescontoDia)
                    {
                        produto.AtivoSN = Convert.ToBoolean(produtosGridView.SelectedCells[8].Value.ToString());
                        if (produto.PrecoDesconto.ToString() != "" || produto.PrecoDesconto != null)
                        {
                            produto.PrecoDesconto = decimal.Parse(produtosGridView.SelectedCells[5].Value.ToString());
                        }
                        if (produto.DiaSemana != "" || produto.DiaSemana != null)
                        {
                            produto.DiaSemana = (produtosGridView.SelectedCells[6].Value.ToString());
                        }
                    }
                    else
                    {
                        produto.AtivoSN = Convert.ToBoolean(produtosGridView.SelectedCells[5].Value.ToString());
                    }

                    frmCadastrarProduto frm = new frmCadastrarProduto(produto, this);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.Show();
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }

        private void EditarCliente(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (clientesGridView.SelectedCells.Count > 0)
            {
                var pessoa = new Pessoa()
                {
                    Codigo = int.Parse(clientesGridView.SelectedCells[0].Value.ToString()),
                    DataNascimento = Convert.ToDateTime(clientesGridView.SelectedCells[11].Value.ToString()),
                    DataCadastro = Convert.ToDateTime(clientesGridView.SelectedCells[13].Value.ToString()),
                };

                frmCadastroCliente frm = new frmCadastroCliente(pessoa, this);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.Show();
            }
        }



        private void txbTelefoneCliente_KeyDown_1(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                BuscarCliente();
            }

        }

        private void ObterClienteParaRelatorio(object sender, EventArgs e)
        {

        }

        private void ReportProdutos(object sender, EventArgs e)
        {
            frmReportProdutos frm = new frmReportProdutos();
            frm.Show();
            // frm.Dispose();
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroUsuario frm = new frmCadastroUsuario();
            frm.Show();
            //frm.Dispose();
        }

        private void sistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfiguracoes frm = new frmConfiguracoes();
            frm.Show();
            // frm.Dispose();
        }

        private void entregadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastrarEntregador frm = new frmCadastrarEntregador();
            frm.Show();
            //frm.Dispose();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            string strServidor = Sessions.returnEmpresa.Servidor;
            string strBanco = Sessions.returnEmpresa.Banco;
            string strCaminhoBkp = Sessions.returnEmpresa.CaminhoBackup;
            con.BackupBanco(strServidor, strBanco, strCaminhoBkp);

            this.Dispose();
            Utils.Kill();
            con.Close();
        }

        private void formasDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastrarFormaPagamento frm = new frmCadastrarFormaPagamento();
            frm.Show();
        }
        private void DeletarProduto(object sender, EventArgs e)
        {
            try
            {
                if (produtosGridView.SelectedRows.Count > 0)
                {
                    DialogResult resultado = MessageBox.Show("Deseja Excluir o Produto " + produtosGridView.SelectedCells[1].Value, "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (resultado == DialogResult.Yes)
                    {
                        int Codigo = int.Parse(this.produtosGridView.SelectedCells[0].Value.ToString());
                        con.DeleteAll("Produto", "spExcluirProduto", Codigo);
                        MessageBox.Show("Item excluído com sucesso.");
                        Utils.PopularGrid("Produto", produtosGridView);
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

        private void MenuAuxiliarProduto(object sender, MouseEventArgs e)
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
        private void CancelarPedidos(object sender, EventArgs e)
        {
            try
            {
                string NomeCliente = (this.pedidosGridView.SelectedCells[0].Value.ToString());
                int CodUser;

                if (pedidosGridView.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Deseja **CANCELAR** o  pedido do Cliente " + NomeCliente + "?", "Cancelamento de Pedido !!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        int Codigo = int.Parse(this.pedidosGridView.SelectedCells[1].Value.ToString());
                        cancelPedid.Codigo = Codigo;
                        cancelPedid.RealizadoEm = DateTime.Now;
                        string iCodMesa = pedidosGridView.SelectedCells[5].Value.ToString();

                        if (ControlaMesas && iCodMesa != "0")
                        {
                            //   string NumeroMesa = Convert.ToString(Utils.RetornaNumeroMesa(iCodMesa));
                            Utils.AtualizaMesa(iCodMesa, 1);
                        }
                        cancelPedid.status = "Cancelado";
                        if (Sessions.returnConfig.RegistraCancelamentos)
                        {
                            DataSet dsPedido = null;
                            DataRow dRow;
                            int CodPessoa;
                            try
                            {
                                dsPedido = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", Codigo);
                                dRow = dsPedido.Tables[0].Rows[0];
                                CodPessoa = int.Parse(dRow.ItemArray.GetValue(2).ToString());
                            }

                            finally
                            {
                                dsPedido.Dispose();

                            }

                            frmHistoricoCancelamento frm = new frmHistoricoCancelamento();
                            frm.ShowDialog();

                            if (frm.DialogResult == DialogResult.OK)
                            {
                                HistoricoCancelamento Hist = new HistoricoCancelamento()
                                {
                                    CodMotivo = frm.CodMotivo,
                                    CodPessoa = CodPessoa,
                                    Data = DateTime.Now,
                                    Motivo = frm.ObsCancelamento
                                };

                                con.Insert("spAdicionaHistoricoCancelamento", Hist);
                            }
                        }

                        con.Update("spCancelarPedido", cancelPedid);
                        Utils.ControlaEventos("CancPedido", this.Name);
                        MessageBox.Show("Pedido Cancelado com sucesso.");
                        Utils.PopularGrid("Pedido", pedidosGridView, "spObterPedido");
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
            //}
            //else
            //{
            //    MessageBox.Show("Usuário não possui permissão para **CANCELAR** pedidos, deseja solicitar acesso do administrador?");
            //}
        }

        private void FinalizaTodos(object sender, EventArgs e)
        {
            try
            {
                bool ControlaMesas = Sessions.returnConfig.UsaControleMesa;
                int codigo;
                bool Marcado;
                string NumeroMesa, iCodMesa;

                if (MessageBox.Show("Deseja ** FINALIZAR ** Todos Pedidos Selecionado?", "Cuidado !!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    for (int i = 0; i < pedidosGridView.Rows.Count; i++)
                    {
                        Marcado = Convert.ToBoolean(pedidosGridView.Rows[i].Cells[2].Value);

                        if (Marcado)
                        {
                            codigo = int.Parse(pedidosGridView.Rows[i].Cells[1].Value.ToString());
                            iCodMesa = pedidosGridView.Rows[i].Cells[5].Value.ToString();
                            // string TipoPedido = pedidosGridView.Rows[i].Cells["PedidoOrigem"].Value.ToString();
                            if (ControlaMesas && iCodMesa != "0")
                            {
                                //NumeroMesa = Convert.ToString(Utils.RetornaNumeroMesa(iCodMesa));
                                Utils.AtualizaMesa(iCodMesa, 1);
                            }
                            con.SinalizarPedidoConcluido("Pedido", "spSinalizarPedidoConcluido", codigo);

                        }

                    }

                    Utils.PopularGrid("Pedido", pedidosGridView, "spObterPedido");
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
        private void FinalizaCancelaPEdidos(object sender, EventArgs e)
        {
            try
            {
                bool ControlaMesas = Sessions.returnConfig.UsaControleMesa;
                int codigo, iCodMesa;
                string NumeroMesa;
                this.pedidosGridView.SelectedCells[2].Value = true;

                if (MessageBox.Show("Deseja ** FINALIZAR ** este pedido?", "Cuidado !!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.pedidosGridView.SelectedCells[2].Value = true;
                    codigo = int.Parse(pedidosGridView.SelectedCells[1].Value.ToString());
                    iCodMesa = int.Parse(pedidosGridView.SelectedCells[5].Value.ToString());

                    if (Sessions.returnConfig.ControlaEntregador)
                    {
                        InformaMotoboyPedido(codigo);
                    }

                    // string TipoPedido = pedidosGridView.Rows[i].Cells["PedidoOrigem"].Value.ToString();
                    if (ControlaMesas && iCodMesa != 0)
                    {
                        //  NumeroMesa = Convert.ToString(Utils.RetornaNumeroMesa(iCodMesa));
                        Utils.AtualizaMesa(Convert.ToString(iCodMesa), 1);
                    }

                    GravaMOvimentoCaixa();
                    con.SinalizarPedidoConcluido("Pedido", "spSinalizarPedidoConcluido", codigo);

                   

                    Utils.PopularGrid("Pedido", pedidosGridView, "spObterPedido");

                }
                else
                {
                    this.pedidosGridView.SelectedCells[6].Value = false;
                    LimpaCampos();
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel selecionar o Pedido" + erro.Message);
            }


        }
        private void GravaMOvimentoCaixa()
        {
            // Retornando o IDFpagamento
            DataSet dsPedido = con.SelectObterRegistroPorString(pedidosGridView.SelectedCells[3].Value.ToString(), "FormaPagamento");
            DataRow dRow = dsPedido.Tables[0].Rows[0];
            int iIFormaPagamento = int.Parse(dRow.ItemArray.GetValue(0).ToString());

            CaixaMovimento caixa = new CaixaMovimento()
            {
                CodCaixa = Sessions.retunrUsuario.CaixaLogado,
                CodFormaPagamento = iIFormaPagamento,
                Data = DateTime.Now,
                Historico = "Pedido " + pedidosGridView.SelectedCells[1].Value.ToString(),
                NumeroDocumento = pedidosGridView.SelectedCells[1].Value.ToString(),
                Tipo = 'E',
                Valor = decimal.Parse(pedidosGridView.SelectedCells[4].Value.ToString()),
                CodUser = Sessions.retunrUsuario.Codigo
            };
            con.Insert("spInserirMovimentoCaixa", caixa);
        }

        private void renovarAtivarSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicencaOFFLINE frm = new frmLicencaOFFLINE();
            frm.ShowDialog();
            // frm.Dispose();
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
                    MontarPedido.Enabled = iCaixaAberto > 0;
                    int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;

                    m.Show(dgv, new Point(e.X, e.Y));
                }

            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
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
                                                                        "", "", "", "", this, 0.00M);
                    frm.ShowDialog();
                }


                //  frm.Dispose();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
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
                    HistoricoCancelamentos(CodCliente);
                }
                AbrirPedido(CodCliente);
            }
            catch (Exception xx)
            {

                MessageBox.Show(xx.Message);
            }
        }

        private void envioDeSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEnvioSms frm = new frmEnvioSms();
            frm.ShowDialog();
            // frm.Dispose();
        }

        private void FiltrarPedidos(object sender, EventArgs e)
        {
            try
            {
                if (cbxFiltroTipo.Text != " ")
                {
                    string Consulta = null;
                    DataSet result = con.SelectAll("Pedido", "spObterPedido");
                    Consulta = cbxFiltroTipo.Text;

                    Consulta = "Tipo LIKE '%" + cbxFiltroTipo.Text + "%'";

                    var Linhas = result.Tables[0].DefaultView;
                    Linhas.RowFilter = Consulta;

                    var newDS = new DataSet("Pedido");
                    var newDT = Linhas.ToTable();

                    newDS.Tables.Add(newDT);

                    this.pedidosGridView.DataSource = null;
                    this.pedidosGridView.AutoGenerateColumns = true;
                    this.pedidosGridView.DataSource = newDS;
                    this.pedidosGridView.DataMember = "Pedido";
                }
                else
                {
                    Utils.PopularGrid("Pedido", this.pedidosGridView, "spObterPedido");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao buscar clientes" + ex.Message);
            }

        }

        private void impressãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfigurarImpressao frm = new frmConfigurarImpressao();
            frm.ShowDialog();
            // frm.Dispose();
        }

        private void geralToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //frmReportPessoas frm = new frmReportPessoas();
            //frm.ShowDialog();
            //  frm.Dispose();
        }

        private void maisVendidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportVendasPorProduto frm = new frmReportVendasPorProduto();
            frm.ShowDialog();
            // frm.Dispose();
        }

        private void CadastroSMSEMAIL(object sender, EventArgs e)
        {
            frmCadastroEmailSMS frm = new frmCadastroEmailSMS();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void exportarDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExportacao frm = new frmExportacao();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void contatoAtivaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmContato frm = new frmContato();
            frm.ShowDialog();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                BuscarCliente();
            }
        }

        private void regiõesDeEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroRegioes frm = new frmCadastroRegioes();
            frm.ShowDialog();
        }

        private void ListaProdInativos(object sender, EventArgs e)
        {
            if (chkProdutosInativos.Checked && !DescontoDia)
            {
                // Carrega produtos Inativos e Sem Desconto
                Utils.PopularGrid("Produto", this.produtosGridView, "spObterProdutosInativosSemDesconto");
            }

            else if (!chkProdutosInativos.Checked && DescontoDia)
            {
                // Carrega Produtos Inativos Com Desconto
                Utils.PopularGrid("Produto", this.produtosGridView);
            }


            else if (chkProdutosInativos.Checked && !DescontoDia)
            {
                // Carrega Produtos Ativos e Com Desconto
                Utils.PopularGrid("Produto", this.produtosGridView, "spObterProdutosInativosSemDesconto");
            }

            else if (!chkProdutosInativos.Checked && !DescontoDia)
            {
                Utils.PopularGrid("Produto", this.produtosGridView, "spObterProdutosAtivosSemDesconto");
            }

            else if (chkProdutosInativos.Checked && DescontoDia)
            {
                Utils.PopularGrid("Produto", this.produtosGridView, "spObterProdutosInativos");
            }


        }

        private void txbTelefoneCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);

        }

        private void mesasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdicionarMesa frm = new frmAdicionarMesa();
            frm.ShowDialog();
        }

        private void ImpressaoAutomatica(int iCodPedido ,string iNumMesa)
        {

            DataSet itemsPedido = con.SelectRegistroPorCodigo("ItemsPedido", "spObterItemsNaoImpresso", iCodPedido);
            if (itemsPedido.Tables[0].Rows.Count>0)
            {
                items = new List<ItemPedido>();
                ItemPedido itemPedido = new ItemPedido();

                for (int i = 0; i < itemsPedido.Tables[0].Rows.Count; i++)
                {

                    itemPedido = new ItemPedido()
                    {
                        CodProduto = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<int>("CodProduto"),
                        NomeProduto = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<string>("NomeProduto"),
                        Quantidade = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<int>("Quantidade"),
                        PrecoUnitario = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<decimal>("PrecoItem"),
                        PrecoTotal = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<decimal>("PrecoTotalItem"),
                        Item = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<string>("Item"),
                        ImpressoSN = Convert.ToBoolean(itemsPedido.Tables["ItemsPedido"].Rows[i].Field<Boolean>("ImpressoSN"))
                    };

                    items.Add(itemPedido);

                }



                int iParam = 0;
                if (!itemPedido.ImpressoSN)
                {
                    foreach (ItemPedido item in items)
                    {

                        if (iParam == 0)
                        {

                            line = "MESA  - Nº " + iNumMesa + "\r\n";

                            //         line += QuebrarString("Pedido tipo:" + item.Text);
                            line += QuebrarString("------------------------------");
                            line += "Emitido em " + DateTime.Now.ToShortDateString() + "\r\n";
                            line += " às " + DateTime.Now.ToShortTimeString() + "\r\n";
                        }
                        AtualizaItemsImpresso Atualiza = new AtualizaItemsImpresso();
                        if (!item.ImpressoSN)
                        {
                            line += item.NomeProduto.ToString() + ", Quant.:" + item.Quantidade.ToString() + "\r\n";
                            if (item.Item != "" && item.Item != null)
                            {
                                line += "Obs:" + QuebrarString(item.Item.ToString().ToUpper()) + "\r\n";
                            }

                            Atualiza.CodPedido = iCodPedido;
                            Atualiza.CodProduto = item.CodProduto;
                            Atualiza.ImpressoSN = true;

                            con.Update("spInformaItemImpresso", Atualiza);
                        }

                        iParam++;
                    }
                }

                if (line != null)
                {
                    EnviaImpressora();
                }
            }
           
            
        }
        private void EnviaImpressora()
        {

        //    string temp = Directory.GetCurrentDirectory() + @"\" + "ConfigImpressao "+ ".txt";
            string RetornoTxt = Utils.CriaArquivoTxt("ConfigImpressao", "");

            if (RetornoTxt!="")
            {
                string iPortaUSB="", iModelo="";

                
                string[] words = RetornoTxt.Split(';');

                for (int i = 0; i < words.Length; i++)
                {
                    iModelo = words[0];
                    iPortaUSB = words[1];
                }
                int iRetorno;
                MP2032 bema = new MP2032();

                iRetorno = MP2032.ConfiguraModeloImpressora(int.Parse(iModelo));
                iRetorno = MP2032.IniciaPorta(iPortaUSB);
                iRetorno = MP2032.BematechTX(line+"\r\n\r\n");
                MP2032.AcionaGuilhotina(1);
            }
            else
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(this.ImprimirPedidoMesa);
                pd.Print();

            }
           

            
        }
        public void ImprimirPedidoMesa(object sender, PrintPageEventArgs ev)
        {
            printFont = new Font("Arial", int.Parse(Sessions.returnConfig.TamanhoFont));
            ev.Graphics.DrawString(line, printFont, Brushes.Black, 0, 0);
            ev.HasMorePages = false;

        }


        private void AtualizaGrid_Tick(object sender, EventArgs e)
        {

            DataSet Dados, PedidosAberto;

       
            int iPedidosAberto = con.SelectAll("Pedido", "spObterPedido").Tables[0].Rows.Count;

            while (iPedidosAberto != pedidosGridView.Rows.Count)
            {
                PopularGrid(false, "Pedido", pedidosGridView);

                for (int i = 0; i < pedidosGridView.Rows.Count; i++)
                {
                    if (pedidosGridView.Rows[i].Cells["PedidoOrigem"].Value.ToString() == "Aplicativo")
                    {
                        pedidosGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;

                    }
                }
  
            }

            for (int intFor = 0; intFor < pedidosGridView.Rows.Count; intFor++)
            {
                ImpressaoAutomatica(int.Parse(pedidosGridView.Rows[intFor].Cells["Codigo"].Value.ToString()), pedidosGridView.Rows[intFor].Cells["NumeroMesa"].Value.ToString()); 
            }
            




            //if (PedidosAberto != null)
            //{
            //    PedidosEmAberto = PedidosAberto.Tables["Pedido"].Rows.Count;

            //    while (PedidosEmAberto != PedidosContados)
            //    {
            //        Dados = Utils.PopularGrid("Pedido", this.pedidosGridView, "spObterPedido");
            //        PedidosContados = Dados.Tables["Pedido"].Rows.Count;

            //        if (Dados.Tables["Pedido"].Rows.Count > 0)
            //        {
            //            string TipoPedido = pedidosGridView.Rows[iPosicao].Cells["PedidoOrigem"].Value.ToString();

            //            if (TipoPedido == "Aplicativo")
            //            {
            //                pedidosGridView.Rows[iPosicao].DefaultCellStyle.BackColor = Color.Red;
            //            }

            //            QtdPedidosAnterior = QtdPedidosAnterior + 1;
            //            iPosicao = iPosicao + 1;

            //        }
            //    }



            //}




        }
         private string QuebrarString(string texto)
        {
            int totalDeCaracters = texto.Length - 1;
            var TamanhoCaracterImpressao = Sessions.returnConfig.QtdCaracteresImp.ToString();
            // string textoTratado = "";
            if (totalDeCaracters > int.Parse(TamanhoCaracterImpressao) - 1)
            {
                for (int i = 0, count = 0; i < totalDeCaracters; i++)
                {
                    if (!texto[i].Equals(' ') && count >= int.Parse(Sessions.returnConfig.QtdCaracteresImp.ToString()))
                    {
                        //texto = texto.Replace(texto, Environment.NewLine);
                        texto = texto.Insert(i, Environment.NewLine);
                        count = 0;
                    }
                    count++;
                }
            }
            return texto += "\r\r\r\n";
        }

        private void AlteraCorLinhas()
        {
            int iPosicao = 0;

            string TipoPedido = pedidosGridView.Rows[iPosicao].Cells["PedidoOrigem"].Value.ToString();

            if (TipoPedido == "Aplicativo")
            {
                pedidosGridView.Rows[iPosicao].DefaultCellStyle.BackColor = Color.Red;
            }

            QtdPedidosAnterior = QtdPedidosAnterior + 1;
            iPosicao = iPosicao + 1;

        }
        private bool AtualizaTela(Boolean iAtualizada)
        {
            if (!iAtualizada)
            {
                Utils.PopularGrid("Pedido", this.pedidosGridView);
                iAtualizada = true;
            }
            return iAtualizada;
        }

        private void EditarPedido(object sender, DataGridViewCellMouseEventArgs e)
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
                if (pedidosGridView.SelectedCells.Count > 0 && e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    codigo = int.Parse(pedidosGridView.SelectedCells[1].Value.ToString());
                    CarregaPedido(codigo);

                }

            }
            catch (Exception es)
            {

                MessageBox.Show(es.Message);
            }
        }

        private void MenuAuxiliarPedidos(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem CancPedido = new MenuItem(" 0 - Cancelar Pedidos");
                //    MenuItem EnviaTab = new MenuItem(" 1 - Enviar Garçon");
                MenuItem FinalizarPed = new MenuItem(" 1 - Finalizar Este Pedido?");
                MenuItem FinalizaSelecionados = new MenuItem(" 2 - Finalizar Todos Selecionado?");
                MenuItem ImprimeConferenciaMesa = new MenuItem(" Imprimir Conferencia desta Mesa");

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
                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }

        private void alteraSenhaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void alterarSenhaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAlteraSenha frm = new frmAlteraSenha();
            frm.ShowDialog();
        }


        private void MarcaPEdidos(object sender, DataGridViewCellEventArgs e)
        {
            bool blSelecionado = Convert.ToBoolean(pedidosGridView.SelectedCells[2].Value);
            //if (pedidosGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            if (pedidosGridView.Rows[e.RowIndex].Cells[2].Value != null)
            {
                if (blSelecionado)
                {
                    pedidosGridView.SelectedCells[2].Value = false;
                }
                else
                {
                    pedidosGridView.SelectedCells[2].Value = true;
                }
            }
        }

        private void entregasPorMotoboyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmReportPedidosPorMotoboy frm = new frmReportPedidosPorMotoboy();
            //frm.ShowDialog();
        }

        private void txtEndereco_TextChanged(object sender, EventArgs e)
        {

        }

        private void geralToolStripMenuItem2_Click(object sender, EventArgs e)
        {

            frmReportPedidosPorPeriodo frm = new frmReportPedidosPorPeriodo();
            frm.ShowDialog();
        }

        private void motivosCancelamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadMotivosCancelamento frm = new frmCadMotivosCancelamento();
            frm.ShowDialog();
        }

        private void caixaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aberturaCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAberturaCaixa frm = new frmAberturaCaixa();
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


    }
}



