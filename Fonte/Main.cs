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
using DexComanda.Relatorios.Fechamentos;
using Microsoft.VisualBasic;
using DexComanda.Cadastros.Produto;
using DexComanda.Relatorios.Fechamentos.Novos;
using DexComanda.Operações.Alteracoes;
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
        private bool ImprimeLPT = Sessions.returnConfig.ImpLPT;
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
        // private string Mo
        private void Main_Load(object sender, EventArgs e)
        {


            if (Sessions.returnUsuario != null)
            {
                con = new Conexao();

                int iNumeroCaixa = Sessions.returnUsuario.CaixaLogado;
                iCaixaAberto = con.SelectRegistroPorDataCodigo("Caixa", "spObterDadosCaixa", DateTime.Now, iNumeroCaixa).Tables["Caixa"].Rows.Count;
                if (Sessions.returnEmpresa.CNPJ == "22695578000142" || Sessions.returnEmpresa.CNPJ== "22678091000151")
                {
                    return;
                }
                if (Utils.CaixaAberto(DateTime.Now, iNumeroCaixa))
                {
                    aberturaCaixaToolStripMenuItem.Enabled = false;
                    lblCaixa.Text = "Caixa Aberto";
                    lblCaixa.ForeColor = Color.Red;
                }

            }
            else
            {
                btnConsultarTelefone.Enabled = true;
                lblCaixa.Text = "Caixa Aberto";
            }


            this.txbTelefoneCliente.Focus();
            con = new Conexao();

            Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto);
            Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido);
            Utils.PopulaGrid_Novo("Pessoa", clientesGridView, Sessions.SqlPessoa);
            MontaMenu();

        }


        private void MontaMenu() // Monta o menu de opções
        {
            if (Sessions.returnEmpresa.CNPJ== "22695578000142" || Sessions.returnEmpresa.CNPJ == "22678091000151")
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
            decimal MargemGarcon = decimal.Parse(DvPedido.ItemArray.GetValue(16).ToString());
            if (strTrocoPara != "0.00")
            {
                strTroco = Convert.ToString(decimal.Parse(strTrocoPara) - decimal.Parse(strTotalPedido));
            }

            frmCadastrarPedido frm = new frmCadastrarPedido(false, strDescPedido, DvPedido.ItemArray.GetValue(9).ToString(),
                                      strTroco, TaxaServico, true, Convert.ToDateTime(DvPedido.ItemArray.GetValue(7).ToString()),
                                      int.Parse(DvPedido.ItemArray.GetValue(1).ToString()), int.Parse(DvPedido.ItemArray.GetValue(2).ToString()), DvPedido.ItemArray.GetValue(4).ToString(),
                                      DvPedido.ItemArray.GetValue(5).ToString(), DvPedido.ItemArray.GetValue(8).ToString(), DvPedido.ItemArray.GetValue(9).ToString(), this, decimal.Parse(strTotalPedido), MargemGarcon);
            frm.ShowDialog();
        }
        private void cadastrarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCadastroCliente frm = new frmCadastroCliente(this);
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
            BuscarCliente(txbTelefoneCliente.Text);
        }

        public void ConsultarClienteParaEdicao(object sender, KeyEventArgs e)
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
                                dsResult= con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", valor);
                            }
                            else if (propriedade.Equals("Nome"))
                            {
                                dsResult = con.SelectPessoaPorNome(valor, Sessions.SqlPessoa,"Nome");
                            }
                            else
                            {
                                dsResult=  con.SelectPessoaPorNome(valor, Sessions.SqlPessoa, "Endereco");
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


        private void BuscarCliente(string iTelefone)
        {
            try
            {
                if (Sessions.retunrUsuario != null)
                {
                    if (iCaixaAberto > 0)
                    {
                        AbreTelaPedido(iTelefone);
                    }
                    else
                    {
                        MessageBox.Show("Caixa precisa estar aberto", "[XSistemas] Aviso");
                    }

                }
                else
                {
                    AbreTelaPedido(iTelefone);
                }


            }

            catch (Exception errobusca)
            {

                MessageBox.Show(errobusca.Message, "Erro");
            }
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
                        frmCadastroCliente frm = new frmCadastroCliente(this);
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
                    else
                    {
                        MessageBox.Show("Caixa Precisa esta aberto", "XSistemas");
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
            string strValor = Interaction.InputBox("Informe o Telefone do cliente a ser cadastrado, sem pontos ou traços", "[xSistemas]", "", 100, 200);

            if (strValor != "")
            {
                BuscarCliente(strValor);
            }
            else
            {
                MessageBox.Show("[xSistemas]", "o Preenchimento do campo é obrigatório");
                return;
            }

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
        private void CarregaProduto(int iCodProduto)
        {
            //Carrega Produto
            DataSet dsProduto = con.SelectRegistroPorCodigo("Produto", "spObterProdutoPorCodigo", iCodProduto,!chkProdutosInativos.Checked);

            DataRow dRowProduto = dsProduto.Tables["Produto"].Rows[0];

            frmCadastrarProduto frm = new frmCadastrarProduto(int.Parse(dRowProduto.ItemArray.GetValue(7).ToString()), dRowProduto.ItemArray.GetValue(0).ToString(), dRowProduto.ItemArray.GetValue(3).ToString(),
                                                              decimal.Parse(dRowProduto.ItemArray.GetValue(1).ToString()), dRowProduto.ItemArray.GetValue(2).ToString(),
                                                              Convert.ToBoolean(dRowProduto.ItemArray.GetValue(6).ToString()), decimal.Parse(dRowProduto.ItemArray.GetValue(5).ToString()),
                                                              dRowProduto.ItemArray.GetValue(4).ToString(), dRowProduto.ItemArray.GetValue(8).ToString(), dRowProduto.ItemArray.GetValue(9).ToString());
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto);

        }

        private void EditarProduto(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                CarregaProduto(int.Parse(produtosGridView.SelectedCells[0].Value.ToString()));
                //if (produtosGridView.SelectedCells.Count > 0)
                //{
                //    var produto = new Produto()
                //    {
                //        Codigo = int.Parse(produtosGridView.SelectedCells[0].Value.ToString()),
                //        Nome = (produtosGridView.SelectedCells[1].Value.ToString()),
                //        Descricao = (produtosGridView.SelectedCells[2].Value.ToString()),
                //        Preco = decimal.Parse(produtosGridView.SelectedCells[3].Value.ToString()),
                //        GrupoProduto = (produtosGridView.SelectedCells[4].Value.ToString()),

                //    };
                //    if (DescontoDia)
                //    {
                //        produto.AtivoSN = Convert.ToBoolean(produtosGridView.SelectedCells[7].Value.ToString());
                //        if (produto.PrecoDesconto.ToString() != "" || produto.PrecoDesconto != null)
                //        {
                //            produto.PrecoDesconto = decimal.Parse(produtosGridView.SelectedCells[5].Value.ToString());
                //        }
                //        if (produto.DiaSemana != "" || produto.DiaSemana != null)
                //        {
                //            produto.DiaSemana = (produtosGridView.SelectedCells[6].Value.ToString());
                //        }
                //    }
                //    else
                //    {
                //        produto.AtivoSN = Convert.ToBoolean(produtosGridView.SelectedCells[5].Value.ToString());
                //    }

                //    frmCadastrarProduto frm = new frmCadastrarProduto(produto, this);
                //    frm.StartPosition = FormStartPosition.CenterParent;
                //    frm.Show();
                //}
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

                DataSet dsPessoa = con.SelectRegistroPorCodigo("Pessoa", "spObterPessoaPorCodigo", int.Parse(clientesGridView.SelectedCells[0].Value.ToString()));
                DataRow dRowPessoa = dsPessoa.Tables["Pessoa"].Rows[0];

                frmCadastroCliente frm = new frmCadastroCliente(int.Parse(dRowPessoa.ItemArray.GetValue(0).ToString()), dRowPessoa.ItemArray.GetValue(1).ToString(), dRowPessoa.ItemArray.GetValue(10).ToString(),
                                                                  dRowPessoa.ItemArray.GetValue(11).ToString(), dRowPessoa.ItemArray.GetValue(2).ToString(), dRowPessoa.ItemArray.GetValue(3).ToString(), dRowPessoa.ItemArray.GetValue(9).ToString()
                                                                  , dRowPessoa.ItemArray.GetValue(4).ToString(), dRowPessoa.ItemArray.GetValue(5).ToString(), dRowPessoa.ItemArray.GetValue(6).ToString(), dRowPessoa.ItemArray.GetValue(7).ToString()
                                                                  , dRowPessoa.ItemArray.GetValue(8).ToString(), int.Parse(dRowPessoa.ItemArray.GetValue(14).ToString()), dRowPessoa.ItemArray.GetValue(15).ToString(), dRowPessoa.ItemArray.GetValue(12).ToString());


                
                Utils.PopulaGrid_Novo("Pessoa", clientesGridView, Sessions.SqlPessoa);
            }
        }




        private void txbTelefoneCliente_KeyDown_1(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                BuscarCliente(txbTelefoneCliente.Text);
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
            //  VerificaRegistroASincronizar();
            con.BackupBanco(strServidor, strBanco, strCaminhoBkp);

            this.Dispose();
            Utils.Kill();
            con.Close();
        }

        private void VerificaRegistroASincronizar()
        {
            DataSet dsProduto = con.SelectRegistroONline("Produto");
            DataSet dsOpcao = con.SelectRegistroONline("Opcao");
            DataSet dsProdutOpcao = con.SelectRegistroONline("Produto_Opcao");
            DataSet dsFormaPagamento = con.SelectRegistroONline("FormaPagamento");
            DataSet dsGrupo = con.SelectRegistroONline("Grupo");
            //  DataSet dsRegiaoEntrega = con.SelectRegistroONline("RegiaoEntrega");


            if (dsProduto.Tables[0].Rows.Count > 0 || dsOpcao.Tables[0].Rows.Count > 0 || dsProdutOpcao.Tables[0].Rows.Count > 0 || dsFormaPagamento.Tables[0].Rows.Count > 0 || dsGrupo.Tables[0].Rows.Count > 0)
            {
                DialogResult resposta = MessageBox.Show("Há registros que precisam ser sincronizados com o servidor , deseja fazer isso agora?", "[xSistemas]", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resposta == DialogResult.Yes)
                {
                    frmSincronizacao frm = new frmSincronizacao();
                    frm.ShowDialog();
                }
            }

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
                string NomeCliente = (this.pedidosGridView.CurrentRow.Cells["Nome Cliente"].Value.ToString());
                int CodUser;

                if (pedidosGridView.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Deseja **CANCELAR** o  pedido do Cliente " + NomeCliente + "?", "Cancelamento de Pedido !!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        int Codigo = int.Parse(this.pedidosGridView.CurrentRow.Cells["Codigo"].Value.ToString());

                        DataSet dsPedido = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", Codigo);
                        DataRow dRowPedido = dsPedido.Tables[0].Rows[0];

                        cancelPedid.Codigo = Codigo;
                        cancelPedid.RealizadoEm = DateTime.Now;

                        string iCodMesa = dRowPedido.ItemArray.GetValue(9).ToString();

                        if (ControlaMesas && iCodMesa != "0")
                        {
                            //   string NumeroMesa = Convert.ToString(Utils.RetornaNumeroMesa(iCodMesa));
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
                                dsPedido.Dispose();

                            }


                        }

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
            catch (Exception ER)
            {
                MessageBox.Show("Não foi possivel **CANCELAR O PEDIDO**" + ER.Message);
            }
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
                        Marcado = Convert.ToBoolean(this.pedidosGridView.Rows[i].Cells["Finalizado"].Value.ToString());
                        decimal dblTotalPedido;
                        if (Marcado)
                        {
                            codigo = int.Parse(pedidosGridView.Rows[i].Cells["Codigo"].Value.ToString());
                                //int.Parse(pedidosGridView.CurrentRow[i].Cells["Codigo"].Value.ToString());

                            DataSet dsPedido = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", codigo);
                            DataRow dRowPedido = dsPedido.Tables[0].Rows[0];

                            int intCodPessoa = int.Parse(dRowPedido.ItemArray.GetValue(2).ToString());
                            dblTotalPedido = decimal.Parse(dRowPedido.ItemArray.GetValue(3).ToString());
                            
                            // Atualiza Ticket Fidelidade
                            AtualizarFidelidade(intCodPessoa);

                            // Grava Débito caso o Tipo de Pagamento gerar financeiro 
                            string strFormaPagamento = dRowPedido.ItemArray.GetValue(5).ToString();
                            Utils.GeraHistorico(DateTime.Now, int.Parse(dRowPedido.ItemArray.GetValue(2).ToString()), dblTotalPedido, Sessions.retunrUsuario.Codigo, "Pedido Nº " + codigo, 'D', strFormaPagamento);

                            // Grava Movimento De Caixa
                            GravaMOvimentoCaixa(strFormaPagamento, dblTotalPedido, codigo);

                            iCodMesa = dRowPedido.ItemArray.GetValue(9).ToString();
                            if (ControlaMesas && iCodMesa != "0")
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
                decimal dblTotalPedido;
                string NumeroMesa;
                // this.pedidosGridView.SelectedCells[2].Value = true;

                if (MessageBox.Show("Deseja ** FINALIZAR ** este pedido?", "Cuidado !!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    codigo = int.Parse(this.pedidosGridView.CurrentRow.Cells["Codigo"].Value.ToString());
                    DataSet dsPedido = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorCodigo", codigo);
                    DataRow dRowPedido = dsPedido.Tables[0].Rows[0];

                    iCodMesa = int.Parse(dRowPedido.ItemArray.GetValue(9).ToString());
                    int intCodPessoa = int.Parse(dRowPedido.ItemArray.GetValue(2).ToString());
                    dblTotalPedido = decimal.Parse(dRowPedido.ItemArray.GetValue(3).ToString());

                    if (Sessions.returnConfig.ControlaEntregador && iCodMesa == 0)
                    {
                        InformaMotoboyPedido(codigo);
                    }

                    // string TipoPedido = pedidosGridView.Rows[i].Cells["PedidoOrigem"].Value.ToString();
                    if (ControlaMesas && iCodMesa != 0)
                    {
                        //  NumeroMesa = Convert.ToString(Utils.RetornaNumeroMesa(iCodMesa));
                        Utils.AtualizaMesa(Convert.ToString(iCodMesa), 1);
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
                DataSet dsPedido = con.SelectObterFormaPagamentoPorNOme(iFPagamento, "FormaPagamento");
                DataRow dRow = dsPedido.Tables[0].Rows[0];
                int iIFormaPagamento = int.Parse(dRow.ItemArray.GetValue(0).ToString());

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
                    Utils.PopularGrid("Pedido", pedidosGridView, Sessions.SqlPedido);
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
                BuscarCliente(txbTelefoneCliente.Text);
            }
        }

        private void regiõesDeEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void ListaProdInativos(object sender, EventArgs e)
        {
            Utils.PopulaGrid_Novo("Produto", produtosGridView, Sessions.SqlProduto, !chkProdutosInativos.Checked);
            //if (chkProdutosInativos.Checked && !DescontoDia)
            //{
            //    // Carrega produtos Inativos e Sem Desconto
            //    Utils.PopularGrid("Produto", this.produtosGridView, "spObterProdutosInativosSemDesconto");
            //}

            //else if (!chkProdutosInativos.Checked && DescontoDia)
            //{
            //    // Carrega Produtos Inativos Com Desconto
            //    Utils.PopularGrid("Produto", this.produtosGridView);
            //}


            //else if (chkProdutosInativos.Checked && !DescontoDia)
            //{
            //    // Carrega Produtos Ativos e Com Desconto
            //    Utils.PopularGrid("Produto", this.produtosGridView, "spObterProdutosInativosSemDesconto");
            //}

            //else if (!chkProdutosInativos.Checked && !DescontoDia)
            //{
            //    Utils.PopularGrid("Produto", this.produtosGridView, "spObterProdutosAtivosSemDesconto");
            //}

            //else if (chkProdutosInativos.Checked && DescontoDia)
            //{
            //    Utils.PopularGrid("Produto", this.produtosGridView, "spObterProdutosInativos");
            //}


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

        private void ImpressaoAutomatica(int iCodPedido, string iNumMesa)
        {

            DataSet itemsPedido = con.SelectRegistroPorCodigo("ItemsPedido", "spObterItemsNaoImpresso", iCodPedido);
            if (itemsPedido.Tables[0].Rows.Count > 0)
            {
               
                items = new List<ItemPedido>();
                ItemPedido itemPedido = new ItemPedido();
                string lRetorno = Utils.ImpressaMesaNova(iCodPedido, ImprimeLPT, 0);
                for (int i = 0; i < itemsPedido.Tables[0].Rows.Count; i++)
                {
                    AtualizaItemsImpresso Atualiza = new AtualizaItemsImpresso();
                    Atualiza.CodPedido = iCodPedido;
                    Atualiza.CodProduto = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<int>("CodProduto");
                    Atualiza.ImpressoSN = true;
                    con.Update("spInformaItemImpresso", Atualiza);
                   
                }


                if (ImprimeLPT && lRetorno != "")
                {
                    StreamReader tempDex = new StreamReader(lRetorno);
                    line = tempDex.ReadToEnd();

                    string RetornoTxt = Directory.GetCurrentDirectory() + @"\" + "ConfigImpressao" + ".txt";
                    if (System.IO.File.Exists(RetornoTxt))
                    {
                        tempDex = new StreamReader(RetornoTxt);
                        // line = tempDex.ReadLine();
                        RetornoTxt = tempDex.ReadLine();

                        if (RetornoTxt != "")
                        {
                            string iPortaUSB = "", iModelo = "";
                            string[] words = RetornoTxt.Split(';');

                            for (int i = 0; i < words.Length; i++)
                            {
                                iModelo = words[0];
                                iPortaUSB = words[1];
                            }
                            int iRetorno;
                            MP2032 bema = new MP2032();
                            try
                            {
                                iRetorno = MP2032.ConfiguraModeloImpressora(int.Parse(iModelo));
                                iRetorno = MP2032.IniciaPorta(iPortaUSB);
                                if (iRetorno == 1)
                                {
                                    MP2032.FormataTX(line, 2, 0, 0, 1, 0);
                                    iRetorno = MP2032.BematechTX(line + "\r\n\r\n");
                                    MP2032.AcionaGuilhotina(1);
                                }
                                else
                                {
                                    MessageBox.Show("Erro de  conexão impressora" + iModelo + " Porta" + iPortaUSB);
                                }
                            }
                            catch (Exception erro)
                            {

                                MessageBox.Show(erro.Message);
                            }



                        }
                    }
                    //    }


                }

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
            try
            {
                DataSet dsPedidosAbertos = con.SelectAll("Pedido", "spObterPedido");
                int iPedidosAberto = dsPedidosAbertos.Tables["Pedido"].Rows.Count;

                if (iPedidosAberto != pedidosGridView.Rows.Count && cbxFiltroTipo.Text == "")
                {
                    Utils.PopulaGrid_Novo("Pedido", pedidosGridView, Sessions.SqlPedido);
                    
                }
                if (Sessions.returnConfig.ImpViaCozinha)
                {

                    for (int i = 0; i < pedidosGridView.Rows.Count; i++)
                    {

                        DataRow dRowPedido = dsPedidosAbertos.Tables[0].Rows[i];
                        //    Boolean iOrigemExterna = dRowPedido.ItemArray.GetValue(6).ToString() == "Aplicativo";
                        Boolean iMesa = dRowPedido.ItemArray.GetValue(5).ToString() != "0";
                        if (iMesa)
                        {
                            ImpressaoAutomatica(int.Parse(dRowPedido.ItemArray.GetValue(1).ToString()), dRowPedido.ItemArray.GetValue(5).ToString());
                        }
                    }

                }

            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao atualizar pedidos" + erro.Message);
            }
            //DataSet Dados, PedidosAberto;

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
                    codigo = int.Parse(pedidosGridView.SelectedCells[0].Value.ToString());
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

        private void entregasPorMotoboyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportEntregasPorMotoboy frm = new frmReportEntregasPorMotoboy();
            frm.ShowDialog();
        }

        private void txtEndereco_TextChanged(object sender, EventArgs e)
        {

        }

        private void geralToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // Utils.ReportFechamento_Novo(DateTime.Now.AddDays(-2), DateTime.Now);
            //RelFechamento_Novo rel = new RelFechamento_Novo();

            //rel.SetParameterValue("DataInicio", DateTime.Now);
            //rel.SetParameterValue("DataFim", DateTime.Now);
            //rel.PrintToPrinter(0, true, 0, 0);
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

        private void gruposCategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdicionarGrupo frm = new frmAdicionarGrupo();
            frm.ShowDialog();
        }


        private void sincronizaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSincronizacao frm = new frmSincronizacao();
            frm.ShowDialog();
        }

        private void opçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void bairrosPorRegiãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBairrosRegiao frm = new frmBairrosRegiao();
            frm.ShowDialog();
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastrarProduto frm = new frmCadastrarProduto(this);
            frm.ShowDialog();
        }

        private void regiãoDeEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroRegioes frm = new frmCadastroRegioes();
            frm.ShowDialog();
        }

        private void lançarMovimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLancaEstoque frm = new frmLancaEstoque();
            frm.ShowDialog();
        }

        private void opçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadOpcao frm = new frmCadOpcao();
            frm.ShowDialog();
        }

        private void produtosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAlteracaoProduto frm = new frmAlteracaoProduto();
            frm.ShowDialog();
        }

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAlteracaoCliente frm = new frmAlteracaoCliente();
            frm.ShowDialog();
        }
    }
}



