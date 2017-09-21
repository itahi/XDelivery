
using DexComanda.Cadastros;
using DexComanda.Cadastros.Pedido;
using DexComanda.Models;
using DexComanda.Models.Operacoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;


namespace DexComanda
{
    public partial class frmCadastrarPedido : Form
    {
        private Conexao con;
        private List<Grupo> grupos;
        private int codPessoa;
        private int codPedido;
        private string formaPagamento;
        public decimal ValorTotal;
        public decimal ValorTroco;
        private List<ItemPedido> items;
        private Pedido pedido;
        private string iTotalItem;
        public int iCodPedido;
        private string iNomeProd;
        private int codigoItemParaAlterar;
        private int rowIndex;
        private int iRowSelecionada;
        private Pessoa pCliente;
        private string itemNome, gNUmeroMesa;
        private bool ContraMesas = Sessions.returnConfig.UsaControleMesa;
        private string DiaDaSema = DateTime.Now.DayOfWeek.ToString();
        private bool PromocaoDiasSemana = Sessions.returnConfig.DescontoDiaSemana;
        private DateTime DataPed;
        private string CNPJRETORNO = Sessions.returnEmpresa.CNPJ;
        private bool ProdutosPorCodigo = Utils.MarcaTipoConfiguracaoProduto().PorCodigo;
        private string TipoCodigo = Utils.MarcaTipoConfiguracaoProduto().TipoCodigo;
        private bool PedidoRepetio;
        private string TrocoPagar;
        private decimal dTotalPedido;
        private decimal DMargemGarco = 0.00M;
        private string lnome;
        private string lNomeOpcao;
        private int lTipo;
        private decimal lPrecoOpcao;
        private decimal lPrecoProdutoMaisOpcao;
        private decimal dcTaxaEntrega;
        private int gMaximoOpcaoProduto;
        private int prvCodEndecoSelecionado = 0;
        private bool ImprimeViaCozinha = Utils.RetornaConfiguracaoMesa().ImprimeSN;
        private int QtdViasCozinha = int.Parse(Utils.RetornaConfiguracaoMesa().ViaCozinha);
        private string TipoAgrupamentoCozinha = Utils.RetornaConfiguracaoMesa().TipoAgrupamento;
        private string TipoAgrupamentoDelivery = Utils.RetornaConfiguracaoDelivery().TipoAgrupamento;
        private bool ImprimeViaDelivery = Utils.RetornaConfiguracaoDelivery().ImprimeSN;
        private int QtdViasDelivery = int.Parse(Utils.RetornaConfiguracaoDelivery().ViaDelivery);
        private bool ImprimeViaBalcao = Utils.RetornaConfiguracaoBalcao().ImprimeSN;
        private int QtdViasBalcao = int.Parse(Utils.RetornaConfiguracaoBalcao().ViaBalcao);
        private string strNomeImpressoraDelivery = Utils.RetornaImpressoras().ImpressoraDelivery;
        private string strNomeImpressoraBalcao = Utils.RetornaImpressoras().ImpressoraBalcao;
        private string strNomeImpressoraContaMesa = Utils.RetornaImpressoras().ImpressoraContaMesa;
        private int intCodProduto1Buscado, intCodProduto2Buscado = 0;
        private List<string> prvListProdutosFidelidade;
        public frmCadastrarPedido()
        {
            try
            {
                InitializeComponent();
                con = new Conexao();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public frmCadastrarPedido(Boolean iPedidoRepetio, string iDescontoPedido, int iCodMesa, string iTroco, decimal iTaxaEntrega, Boolean IniciaTempo,
            DateTime DataPedido, int CodigoPedido, int CodPessoa, string itrocoPara, string fPagamento, string TipoPedido, string MesaBalcao,
            decimal iTotalPedido = 0.00M, decimal MargeGarcon = 0.00M, int iCodVendedor = 0,
            string iObservacaoPedido = "", int iIntEnderecoSelecionado = 0, string strSenha = "", List<string> iCodProdutosFidelidade = null)
        {
            try
            {
                InitializeComponent();
                con = new Conexao();
                items = new List<ItemPedido>();
                grupos = new List<Grupo>();
                cbxTipoPedido.Visible = ContraMesas;
                txtDesconto.Text = iDescontoPedido;
                prvCodEndecoSelecionado = iIntEnderecoSelecionado;
                prvListProdutosFidelidade = iCodProdutosFidelidade;
                codPessoa = CodPessoa;
                txtSenha.Text = strSenha;
                codPedido = CodigoPedido;
                txtTrocoPara.Text = itrocoPara;
                formaPagamento = fPagamento;
                txtObsPedido.Text = iObservacaoPedido;
                DataPed = DataPedido;
                PedidoRepetio = iPedidoRepetio;
                dTotalPedido = iTotalPedido;
                DMargemGarco = MargeGarcon;
                if (MargeGarcon != 0.00M && codPedido != 0)
                {
                    btnCalGarcon.Text = "Remove 10%";
                }
                else
                {
                    btnCalGarcon.Text = "Calcula 10%";
                }

                timer1.Enabled = IniciaTempo;
                dcTaxaEntrega = iTaxaEntrega;

                lblEntrega.Text = Convert.ToString(iTaxaEntrega);
                if (iTroco != "0,00")
                {
                    lblTroco.Text = iTroco;
                    TrocoPagar = iTroco;
                }
                else
                {
                    lblTroco.Text = "0,00";
                    TrocoPagar = "0,00";
                }
                cbxListaMesas.Enabled = TipoPedido != "1 - Mesa";
                CarregaMesas(iCodMesa);
                gNUmeroMesa = Convert.ToString(iCodMesa);
                cbxTipoPedido.Text = TipoPedido;
                Utils.MontaCombox(cbxVendedor, "Nome", "Codigo", "Usuario", "spObterUsuarioPorCodigo", iCodVendedor);
                PreencheListaProdutosFidelidade(iCodProdutosFidelidade);
            }
            catch (Exception mx)
            {

                MessageBox.Show(Bibliotecas.cException + mx.Message);
            }
        }
        private void PreencheListaProdutosFidelidade(List<string> listProdutos)
        {
            try
            {
                if (listProdutos.Count == 0 || listProdutos == null)
                {
                    return;
                }
                DataSet itemsPedido = con.SelectRegistroPorCodigo("Produto", "spObterListaProdutoPorCodigo", listProdutos);
                for (int i = 0; i < itemsPedido.Tables["Produto"].Rows.Count; i++)
                {
                    var itemPedido = new ItemPedido()
                    {
                        Codigo = 0,
                        CodProduto = itemsPedido.Tables["Produto"].Rows[i].Field<int>("Codigo"),
                        NomeProduto = itemsPedido.Tables["Produto"].Rows[i].Field<string>("NomeProduto"),
                        Quantidade = 1.00M,
                        PrecoUnitario = 0.00M,
                        PrecoTotal = 0.00M,
                        Item = "",
                        ImpressoSN = false,
                        FidelidadeSN = true
                    };

                    items.Add(itemPedido);
                    atualizarGrid(itemPedido);
                }
            }
            catch (Exception erros)
            {
                MessageBox.Show("PreencheListaProdutosFidelidade " + erros.Message);
            }
        }

        private void itemsPedidoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.itemsPedidoBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dBExpertDataSet);
        }
        private void CarregaMesas(int iCodMesa = 0)
        {
            try
            {
                if (iCodMesa == 0)
                {
                    cbxListaMesas.DataSource = con.SelectAll("Mesas", "spObterMesasAbertas").Tables["Mesas"];
                    cbxListaMesas.DisplayMember = "NumeroMesa";
                    cbxListaMesas.ValueMember = "Codigo";
                }
                else
                {
                    cbxListaMesas.DataSource = con.SelectRegistroPorCodigo("Mesas", "spObterMesasPorCodigo", iCodMesa).Tables[0];
                    cbxListaMesas.DisplayMember = "NumeroMesa";
                    cbxListaMesas.ValueMember = "Codigo";
                }


            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        private void CarregaOpcoesProduto(int iCodProduto)
        {
            con.SelectRegistroPorCodigo("Produto_Opcao", "spObterOpcoesProduto", iCodProduto);
        }

        public void frmCadastrarPedido_Load(object sender, EventArgs e)
        {
            dtPedido.Value = DateTime.Now;
            grpVendedor.Enabled = Sessions.returnConfig.ExigeVendedorSN;
            cbxTipoPedido.Visible = ContraMesas;
            DataSet pessoa = con.SelectPessoaPorCodigo("Pessoa", "spObterPessoaPorCodigo", codPessoa);
            DataRow dRow = pessoa.Tables["Pessoa"].Rows[0];

            if (ProdutosPorCodigo)
            {
                cbxTipoProduto.Visible = false;
                lblGrupo.Text = "Código:";
                txtCodProduto1.Visible = true;
                chkCodPersonalizado.Visible = true;
                chkCodPersonalizado.Checked = Utils.MarcaTipoConfiguracaoProduto().TipoCodigo == "Personalizado";
            }
            else
            {
                cbxTipoProduto.Visible = true;
                lblGrupo.Text = "Grupo";
                txtCodProduto1.Visible = false;
                this.cbxTipoProduto.DataSource = con.SelectAll("Grupo", "spObterGrupoAtivo").Tables[0];
                this.cbxTipoProduto.DisplayMember = "NomeGrupo";
                this.cbxTipoProduto.ValueMember = "Codigo";
            }

            Utils.MontaCombox(cmbFPagamento, "Descricao", "Codigo", "FormaPagamento", "spObterFormaPagamentoAtivo");

            if (codPedido != 0 || PedidoRepetio)
            {
                ConsultaStatusPedido(codPedido);
                if (!PedidoRepetio)
                {
                    this.label3.Text = "Alterar Pedido ( N:" + codPedido + ")";
                    this.btnGerarPedido.Text = "Atualizar Pedido";
                    this.btnGerarPedido.Click -= btnGerarPedido_Click;
                    this.btnGerarPedido.Click += btnAtualizar_Click;
                    this.btnReimprimir.Visible = true;
                }
                pCliente = new Pessoa()
                {
                    Nome = dRow["Nome"].ToString(),
                    Endereco = dRow["Endereco"].ToString(),
                    Numero = dRow["Numero"].ToString(),
                    Observacao = dRow["Observacao"].ToString(),
                    Bairro = dRow["Bairro"].ToString(),
                    Cidade = dRow["Cidade"].ToString(),
                    PontoReferencia = dRow["PontoReferencia"].ToString(),
                    Telefone = dRow["Telefone"].ToString(),
                    Telefone2 = dRow["Telefone2"].ToString()
                };
                DataSet itemsPedido = con.SelectRegistroPorCodigo("ItemsPedido", "spObterItemsPedido", codPedido);
                for (int i = 0; i < itemsPedido.Tables["ItemsPedido"].Rows.Count; i++)
                {
                    var itemPedido = new ItemPedido()
                    {
                        Codigo = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<int>("Codigo"),
                        CodProduto = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<int>("CodProduto"),
                        NomeProduto = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<string>("NomeProduto"),
                        Quantidade = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<decimal>("Quantidade"),
                        PrecoUnitario = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<decimal>("PrecoItem"),
                        PrecoTotal = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<decimal>("PrecoTotalItem"),
                        Item = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<string>("Item"),
                        ImpressoSN = Convert.ToBoolean(itemsPedido.Tables["ItemsPedido"].Rows[i].Field<Boolean>("ImpressoSN")),
                       
                        DescontoPorcetagem = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<decimal>("DescontoPorcetagem")
                    };

                    this.cmbFPagamento.Text = formaPagamento;
                    items.Add(itemPedido);
                    atualizarGrid(itemPedido);
                    if (PedidoRepetio)
                    {
                        itemPedido.FidelidadeSN = false;
                    }
                    else
                    {
                        itemPedido.FidelidadeSN = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<Boolean>("FidelidadeSN");
                    }
                }
               
                if (DMargemGarco != 0.00M)
                {
                    lbTotal.Text = Convert.ToString(decimal.Parse(lbTotal.Text.Replace("R$", "")) + DMargemGarco);
                }
            }
            else
            {
                pCliente = new Pessoa()
                {
                    Nome = dRow["Nome"].ToString(),
                    Endereco = dRow["Endereco"].ToString(),
                    Numero = dRow["Numero"].ToString(),
                    Observacao = dRow["Observacao"].ToString(),
                    Bairro = dRow["Bairro"].ToString(),
                    Cidade = dRow["Cidade"].ToString(),
                    PontoReferencia = dRow["PontoReferencia"].ToString(),
                    Telefone = dRow["Telefone"].ToString(),
                    Telefone2 = dRow["Telefone2"].ToString()
                };

            }
            AtualizaClienteTela(prvCodEndecoSelecionado);
            this.gridViewItemsPedido.CurrentCell = null;
            CalculaTaxaEntrega(cbxTipoPedido.Text == "0 - Entrega");
        }
        private void ExibiFiado(int intCodPessoa)
        {
            decimal dDébito = 0;
            DataSet dsHis = con.SelectRegistroPorCodigo("HistoricoPessoa", "spObterHistoricoPorPessoa", intCodPessoa);
            for (int i = 0; i < dsHis.Tables[0].Rows.Count; i++)
            {
                dDébito += dsHis.Tables[0].Rows[i].Field<Decimal>("Valor");
            }

            if (dDébito < 0)
            {

                lblDébito.Visible = true;
                lblDébito.Text = "Débito R$: " + dDébito.ToString();
            }
        }
        public void AtualizaClienteTela(int iCodEndereco = 0)
        {
            DataSet dsPessoa;
            try
            {
                if (iCodEndereco == 0)
                {
                    dsPessoa = con.SelectRegistroPorCodigo("Pessoa", "spObterPessoaPorCodigo", codPessoa);
                    lblNomeCliente.Text = dsPessoa.Tables[0].Rows[0].Field<string>("Nome");
                    lblEndereco.Text = dsPessoa.Tables[0].Rows[0].Field<string>("Endereco") + "," +
                        dsPessoa.Tables[0].Rows[0].Field<string>("Numero")
                        + "-" + dsPessoa.Tables[0].Rows[0].Field<string>("Bairro") + " " +
                        dsPessoa.Tables[0].Rows[0].Field<string>("Cidade");
                    lblEntrega.Text = Convert.ToString(Utils.RetornaTaxaPorCliente(codPessoa, iCodEndereco));
                }
                else
                {
                    dsPessoa = con.SelectRegistroPorCodigo("Pessoa_Endereco", "spObterEnderecoPorCodigo", iCodEndereco);
                    lblNomeCliente.Text = dsPessoa.Tables[0].Rows[0].Field<string>("Nome");
                    lblEndereco.Text = dsPessoa.Tables[0].Rows[0].Field<string>("Endereco") + "," +
                        dsPessoa.Tables[0].Rows[0].Field<string>("Numero")
                        + "-" + dsPessoa.Tables[0].Rows[0].Field<string>("Bairro") + " " +
                        dsPessoa.Tables[0].Rows[0].Field<string>("Cidade");
                    lblEntrega.Text = Convert.ToString(Utils.RetornaTaxaPorCliente(codPessoa, dsPessoa.Tables[0].Rows[0].Field<int>("Codigo")));
                }

                AtualizaTotalPedido();
                ExibiFiado(codPessoa);
            }
            catch (Exception erros)
            {
                MessageBox.Show(Bibliotecas.cException + erros.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxProdutosGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LimpaTamanhosSabores();
                var produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];

                intCodProduto1Buscado = int.Parse(produto.Rows[0]["Codigo"].ToString());
                MontaMenuOpcoes(intCodProduto1Buscado);

                this.txtQuantidade.Text = "1";
                var ValorProduto = "";
                //if (PromocaoDiasSemana)
                //{
                List<PrecoDiaProduto> listPreco = new List<PrecoDiaProduto>();
                listPreco = Utils.DeserializaObjeto(produto.Rows[0]["DiaSemana"].ToString());
                if (listPreco != null && listPreco.Count != 0)
                {
                    foreach (var item in listPreco)
                    {
                        if (item.Dia == DiaDaSema)
                        {
                            ValorProduto = item.Preco.ToString();
                            this.txtPrecoUnitario.Text = ValorProduto;
                            iTotalItem = txtPrecoUnitario.Text;
                            if (cbxMeiaPizza.Checked)
                            {
                                iNomeProd = cbxSabor.Text;
                            }
                            else
                            {
                                iNomeProd = cbxProdutosGrid.Text;
                            }

                            return;
                        }
                        else
                        {
                            ValorProduto = produto.Rows[0]["PrecoProduto"].ToString();
                            iTotalItem = txtPrecoUnitario.Text;
                            if (cbxMeiaPizza.Checked)
                            {
                                iNomeProd = cbxSabor.Text;
                            }
                            else
                            {
                                iNomeProd = cbxProdutosGrid.Text;
                            }
                        }
                    }
                }
                else
                {
                    ValorProduto = produto.Rows[0]["PrecoProduto"].ToString();
                    iTotalItem = txtPrecoUnitario.Text;
                    if (cbxMeiaPizza.Checked)
                    {
                        iNomeProd = cbxSabor.Text;
                    }
                    else
                    {
                        iNomeProd = cbxProdutosGrid.Text;
                    }
                }
                //}
                // DAqui
                //else
                //{
                //    ValorProduto = produto.Rows[0]["PrecoProduto"].ToString();
                //    iTotalItem = txtPrecoUnitario.Text;
                //    if (cbxMeiaPizza.Checked && cbxSabor.Focused)
                //    {
                //        iNomeProd = cbxSabor.Text;
                //    }
                //    else
                //    {
                //        iNomeProd = cbxProdutosGrid.Text;
                //    }
                //}

                this.txtPrecoUnitario.Text = ValorProduto;
                CalcularTotalItem();

                if (this.cbxSabor.Focused)
                {
                    var valorProduto = decimal.Parse("");
                    var valorSabor = decimal.Parse("");
                    var _produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];
                    var sabor = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxSabor.SelectedValue.ToString())).Tables["Produto"];

                    txtQuantidade.Text = "1";
                    //if (PromocaoDiasSemana)
                    //{
                    List<PrecoDiaProduto> newListPreco = new List<PrecoDiaProduto>();
                    newListPreco = Utils.DeserializaObjeto(produto.Rows[0]["DiaSemana"].ToString());
                    if (listPreco != null)
                    {
                        foreach (var item in listPreco)
                        {
                            if (item.Dia == DiaDaSema)
                            {
                                txtPrecoUnitario.Text = item.Preco.ToString();
                                iTotalItem = txtPrecoUnitario.Text;
                                if (cbxMeiaPizza.Checked)
                                {
                                    iNomeProd = cbxSabor.Text;
                                }
                                else
                                {
                                    iNomeProd = cbxProdutosGrid.Text;
                                }
                                return;
                            }
                            else
                            {
                                txtPrecoUnitario.Text = produto.Rows[0]["PrecoProduto"].ToString();
                                iTotalItem = txtPrecoUnitario.Text;
                                if (cbxMeiaPizza.Checked)
                                {
                                    iNomeProd = cbxSabor.Text;
                                }
                                else
                                {
                                    iNomeProd = cbxProdutosGrid.Text;
                                }
                            }
                        }
                    }
                    else
                    {
                        valorProduto = decimal.Parse(_produto.Rows[0]["PrecoProduto"].ToString());
                        valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
                    }

                    if (valorProduto > valorSabor)
                    {
                        this.txtPrecoUnitario.Text = valorProduto.ToString();
                    }
                    else
                    {
                        this.txtPrecoUnitario.Text = valorSabor.ToString();
                    }
                    //}
                    //else
                    //{
                    //    valorProduto = decimal.Parse(_produto.Rows[0]["PrecoProduto"].ToString());
                    //    valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());

                    //    txtPrecoUnitario.Text = Convert.ToString(RetornaMaiorValor(valorProduto, valorSabor));
                    //    //if (valorProduto > valorSabor)
                    //    //{
                    //    //    this.txtPrecoUnitario.Text = valorProduto.ToString();
                    //    //}
                    //    //else
                    //    //{
                    //    //    this.txtPrecoUnitario.Text = valorSabor.ToString();
                    //    //}
                    //}


                }
                // Pega o Preço unitário selecionado

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }



            //  ComparaValores();
        }
        private void ConsultaStatusPedido(int iCodPedido)
        {
            int status;
            DataSet dsPedido = con.SelectRegistroPorCodigo("Pedido", "spStatusPedido", iCodPedido);
            if (dsPedido.Tables[0].Rows.Count > 0)
            {
                status = dsPedido.Tables[0].Rows[0].Field<int>("CodStatus");
                switch (status)
                {
                    case 1:
                        //Aberto
                        lblStatusPedido.Text = dsPedido.Tables[0].Rows[0].Field<string>("Nome");
                        lblStatusPedido.ForeColor = Color.Red;
                        break;

                    case 2:
                        // Confirmado
                        lblStatusPedido.Text = dsPedido.Tables[0].Rows[0].Field<string>("Nome");
                        lblStatusPedido.ForeColor = Color.Green;
                        break;
                    case 3:
                        //Na Cozinha
                        lblStatusPedido.Text = dsPedido.Tables[0].Rows[0].Field<string>("Nome");
                        lblStatusPedido.ForeColor = Color.Green;
                        break;
                    case 4:
                        //Na Entrega
                        lblStatusPedido.Text = dsPedido.Tables[0].Rows[0].Field<string>("Nome");
                        lblStatusPedido.ForeColor = Color.Blue;
                        break;
                    case 5:
                        //Concluido
                        lblStatusPedido.Text = dsPedido.Tables[0].Rows[0].Field<string>("Nome");
                        lblStatusPedido.ForeColor = Color.Black;
                        break;
                }

            }
            else
            {
                lblStatusPedido.Visible = false;
            }
        }
        private void SemMeiaPizza()
        {
            this.cbxSabor.Enabled = false;
            this.cbxSabor.Text = "";
            this.cbxMeiaPizza.Checked = false;
            chkSabores.Checked = false;

        }
        private void cbxTipoProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string grupo = this.cbxTipoProduto.Text;

                this.cbxProdutosGrid.DataSource = con.SelectRegistroPorNome("@GrupoProduto", "Produto", "spObterProdutoPorGrupo", grupo).Tables["Produto"];
                this.cbxProdutosGrid.DisplayMember = "NomeProduto";
                this.cbxProdutosGrid.ValueMember = "Codigo";

                this.cbxSabor.DataSource = con.SelectRegistroPorNome("@GrupoProduto", "Produto", "spObterProdutoPorGrupo", grupo).Tables["Produto"];
                this.cbxSabor.DisplayMember = "NomeProduto";
                this.cbxSabor.ValueMember = "Codigo";

                this.txtQuantidade.Text = "1";
                SemMeiaPizza();

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

        }
        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Utils.SoDecimais(e))
            {
                if (e.KeyChar == 13 || e.KeyChar == (char)Keys.Tab || e.KeyChar == 11 || e.KeyChar == 9)
                {
                    var precoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.ToString().Replace("R$ ", ""));
                    var quantidade = decimal.Parse(this.txtQuantidade.Text);
                    var total = precoUnitario * quantidade;
                    this.txtPrecoTotal.Text = total.ToString();
                    this.btnAdicionarItemNoPedido.Focus();
                }
            }



        }
        private bool MeiaPizza()
        {
            if (this.cbxSabor.Enabled == true)
            {
                if (Sessions.returnEmpresa.CNPJ == "10470600000177")
                {
                    cbxProdutosGrid.Text = cbxProdutosGrid.Text + " 50%";
                    itemNome = cbxProdutosGrid.Text.Insert(cbxProdutosGrid.Text.Length, Environment.NewLine) + this.cbxSabor.Text + " 50%";
                }
                else
                {
                    itemNome = "Meio " + cbxProdutosGrid.Text.Insert(cbxProdutosGrid.Text.Length, Environment.NewLine) + " / Meio " + this.cbxSabor.Text;
                }

                return true;
            }
            else
            {
                itemNome = this.cbxProdutosGrid.Text;
                return false;
            }
        }

        private void EscondeTamanhos()
        {
            foreach (System.Windows.Forms.Control ctrControl in grpBoxTamanhos.Controls)
            {
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    //Unselect all RadioButtons
                    ((System.Windows.Forms.RadioButton)ctrControl).Visible = false;
                }
            }
        }
        private decimal RetornaMaiorValor(decimal iValue1, decimal iValue2)
        {
            decimal[] valores = new decimal[2];
            valores[0] = iValue1;
            valores[1] = iValue2;

            return valores.Max();
        }

        private void MontaMenuOpcoes(int intCodProduto = 0, int intCodProduto2 = 0)
        {
            grpBoxTamanhos.Enabled = false;
            int intCodProduto1 = intCodProduto;
            DataSet dsOpcoes = new DataSet();
            decimal dPrecoProduto, dPrecoSoOpcao;
            try
            {
                dsOpcoes = con.RetornaOpcoesProduto(intCodProduto);
                EscondeTamanhos();
                chkListAdicionais.Items.Clear();
                if (dsOpcoes.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsOpcoes.Tables[0].Rows.Count; i++)
                    {
                        DataSet dsMaiorPreco = con.RetornaMaioresPrecos(intCodProduto, intCodProduto2, "", "", dsOpcoes.Tables[0].Rows[i].Field<int>("CodOpcao").ToString(), Sessions.returnConfig.CobrancaProporcionalSN);
                        lTipo = int.Parse(dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<string>("Tipo"));
                        lPrecoOpcao = dsMaiorPreco.Tables[0].Rows[0].Field<Decimal>("PrecoOpcao");
                        dPrecoProduto = dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<decimal>("PrecoProduto");
                        gMaximoOpcaoProduto = dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<int>("MaximoAdicionais");
                        dPrecoSoOpcao = dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<decimal>("PrecoSoOpcao");
                        lnome = dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<string>("Nome").Trim();//.Substring(1, 10);
                        lTipo = int.Parse(dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<string>("Tipo"));
                        if (lTipo == 1)
                        {

                            List<PrecoDiaProduto> listPreco = new List<PrecoDiaProduto>();
                            listPreco = Utils.DeserializaObjeto(dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<string>("DiaSemana"));
                            if (listPreco != null && listPreco.Count != 0)
                            {
                                foreach (var item in listPreco)
                                {
                                    if (item.Dia == DiaDaSema)
                                    {
                                        dPrecoProduto = item.Preco;
                                        break;
                                    }
                                }
                            }
                            grpBoxTamanhos.Enabled = true;
                            grpBoxTamanhos.Text = dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<string>("NomeTipo");
                            lNomeOpcao = dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<string>("Nome").Trim();
                            lPrecoProdutoMaisOpcao = dPrecoProduto + dPrecoSoOpcao;
                            if (!radioButton1.Visible)
                            {
                                radioButton1.Text = lnome;
                                radioButton1.Tag = lPrecoProdutoMaisOpcao;
                                radioButton1.Visible = true;

                            }
                            else if (!radioButton2.Visible)
                            {
                                radioButton2.Text = lnome;
                                radioButton2.Tag = lPrecoProdutoMaisOpcao;
                                radioButton2.Visible = true;
                            }
                            else if (!radioButton3.Visible)
                            {
                                radioButton3.Text = lnome;
                                radioButton3.Tag = lPrecoProdutoMaisOpcao;
                                radioButton3.Visible = true;

                            }
                            else if (!radioButton4.Visible)
                            {
                                radioButton4.Text = lnome;
                                radioButton4.Tag = lPrecoProdutoMaisOpcao;
                                radioButton4.Visible = true;
                            }
                            else if (!radioButton5.Visible)
                            {
                                radioButton5.Text = lnome;
                                radioButton5.Tag = lPrecoProdutoMaisOpcao;
                                radioButton5.Visible = true;
                            }
                            else if (!radioButton6.Visible)
                            {
                                radioButton6.Text = lnome;
                                radioButton6.Tag = lPrecoProdutoMaisOpcao;
                                radioButton6.Visible = true;
                            }
                            else if (!rb7.Visible)
                            {
                                rb7.Text = lnome;
                                rb7.Tag = lPrecoProdutoMaisOpcao;
                                rb7.Visible = true;
                            }
                            else if (!rb8.Visible)
                            {
                                rb8.Text = lnome;
                                rb8.Tag = lPrecoProdutoMaisOpcao;
                                rb8.Visible = true;
                            }
                            else if (!rb9.Visible)
                            {
                                rb9.Text = lnome;
                                rb9.Tag = lPrecoProdutoMaisOpcao;
                                rb9.Visible = true;
                            }
                            else if (!rb10.Visible)
                            {
                                rb10.Text = lnome;
                                rb10.Tag = lPrecoProdutoMaisOpcao;
                                rb10.Visible = true;
                            }
                            else if (!rb11.Visible)
                            {
                                rb11.Text = lnome;
                                rb11.Tag = lPrecoProdutoMaisOpcao;
                                rb11.Visible = true;
                            }
                            else if (!rb12.Visible)
                            {
                                rb12.Text = lnome;
                                rb12.Tag = lPrecoProdutoMaisOpcao;
                                rb12.Visible = true;
                            }



                            ////Varre todos controles dentro do groupBox
                            //foreach (Control radiobutons in grpBoxTamanhos.Controls)
                            //{
                            //    if (object.ReferenceEquals(radiobutons.GetType(), typeof(System.Windows.Forms.RadioButton)))
                            //    {
                            //        //Check to see if  
                            //        if (!((System.Windows.Forms.RadioButton)radiobutons).Visible)
                            //        {
                            //            ((System.Windows.Forms.RadioButton)radiobutons).Tag = lPreco;
                            //            ((System.Windows.Forms.RadioButton)radiobutons).Text = lnome;
                            //            ((System.Windows.Forms.RadioButton)radiobutons).Visible =true;
                            //        }
                            //    }
                            //}


                        }
                        if (lTipo == 2)
                        {
                            //  NumericUpDown newNumeric = new NumericUpDown();
                            //newNumeric.Tag = lPreco;
                            if (dPrecoSoOpcao > 0)
                            {
                                chkListAdicionais.Items.Add(lnome + "(+" + dPrecoSoOpcao + ")", false);
                            }
                            else
                            {
                                chkListAdicionais.Items.Add(lnome + "(" + dPrecoSoOpcao + ")", false);
                            }

                            //  listView1.Items.Add(newNumeric, 0)
                            //while (lTipo==2)
                            //{
                            //}


                        }

                    }

                }
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro na montagem de opções " + erro.Message + erro.InnerException);
            }


            //if (cbxSabor.SelectedIndex>0)
            //{
            //    MontaMenuOpcoes(int.Parse(cbxSabor.SelectedValue.ToString()));
            //}

            //ComparaValores();
        }
        private void InsereOpcaoItems(int iCodPedido, int iCodProduto, int iCodOpcao, string iObservacao)
        {
            OpcaoItem opcao = new OpcaoItem()
            {
                CodOpcao = iCodOpcao,
                CodPedido = iCodPedido,
                CodProduto = iCodProduto,
                Observacao = iObservacao
            };
            con.Insert("spAdicionarOpcaoPedido", opcao);

        }

        /// <summary>
        /// Pega o nome de cada item marcado no checkbox 'adicionais' e preenche as observações do produto
        /// </summary>
        /// <returns></returns>
        private string PegaCheckBoxMarcado()
        {

            string strMarcado = string.Empty;
            try
            {
                txtItemDescricao.Text = txtItemDescricao.Text.Replace("+ ", string.Empty);
                string[] strObs = txtItemDescricao.Text.Split(Environment.NewLine.ToCharArray());
                for (int i = 0; i < chkListAdicionais.Items.Count; i++)
                {
                    if (chkListAdicionais.GetItemCheckState(i) == CheckState.Checked
                        && !txtItemDescricao.Text.Contains(ObterSomenteLetras(chkListAdicionais.Items[i].ToString())))
                    {
                        strMarcado += "+ " + ObterSomenteLetras(chkListAdicionais.Items[i].ToString());
                        strMarcado = strMarcado.Insert(strMarcado.Length, Environment.NewLine);
                        decimal dValorProduto = decimal.Parse(txtPrecoUnitario.Text.Replace("R$", ""));
                        decimal dValorAdicional = decimal.Parse(ObterSomenteNumerosReais(chkListAdicionais.Items[i].ToString()));
                        txtPrecoUnitario.Text = Convert.ToString(dValorProduto + dValorAdicional);
                        CalcularTotalItem();
                    }
                    else if (txtItemDescricao.Text.Contains(ObterSomenteLetras(chkListAdicionais.Items[i].ToString()))
                        && chkListAdicionais.GetItemCheckState(i) == CheckState.Unchecked)
                    {
                        decimal dValorProduto = decimal.Parse(txtPrecoUnitario.Text.Replace("R$", ""));
                        decimal dValorAdicional = decimal.Parse(ObterSomenteNumerosReais(chkListAdicionais.Items[i].ToString()));
                        for (int intFor = 0; intFor < strObs.Length; intFor++)
                        {
                            if (strObs[intFor].ToString() == ObterSomenteLetras(chkListAdicionais.Items[i].ToString()))
                            {
                                txtItemDescricao.Text = txtItemDescricao.Text.Replace(ObterSomenteLetras(chkListAdicionais.Items[i].ToString()), "").Replace(Environment.NewLine, string.Empty);
                                strMarcado = txtItemDescricao.Text;
                                txtPrecoUnitario.Text = Convert.ToString(dValorProduto - dValorAdicional);
                                CalcularTotalItem();
                            }

                        }

                    }


                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return strMarcado;
        }
        private void btnAdicionarItemNoPedido_Click(object sender, EventArgs e)
        {
            int intCodigoProdutoBusca = 0;
            string strItemsMarcados = "";
            try
            {
                if (!ProdutosPorCodigo)
                {
                    intCodigoProdutoBusca = int.Parse(this.cbxProdutosGrid.SelectedValue.ToString());
                }
                else
                {
                    intCodigoProdutoBusca = intCodProduto1Buscado;
                }

                if (con.ControlaEstoque(intCodigoProdutoBusca))
                {
                    if (con.ContaEstoque(cbxProdutosGrid.Text).Tables[0].Rows[0].Field<decimal>("EstoqueAtual") == 0)
                    {
                        MessageBox.Show("Produto selecionado não possui estoque");
                        return;
                    }
                }
                if (!Utils.CaixaAberto(DateTime.Now, Sessions.retunrUsuario.CaixaLogado, Sessions.retunrUsuario.Turno))
                {
                    MessageBox.Show(Bibliotecas.cCaixaFechado);
                    return;
                }
                if (grpBoxTamanhos.Enabled)
                {

                    if (radioButton1.Visible && !radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked
                        && !radioButton4.Checked && !radioButton5.Checked && !radioButton6.Checked && !rb7.Checked
                         && !rb8.Checked && !rb9.Checked && !rb10.Checked && !rb11.Checked && !rb12.Checked)
                    {
                        MessageBox.Show("É obrigatório selecionar o " + grpBoxTamanhos.Text, " [xSistemas] Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        grpBoxTamanhos.Focus();
                        return;
                    }
                }
                chkSabores.Checked = false;
                grpBoxTamanhos.Enabled = true;
                MeiaPizza();

                if (txtQuantidade.Text != "")
                {
                    var quantidade = decimal.Parse(this.txtQuantidade.Text.ToString());
                    if (!quantidade.Equals(""))
                    {
                        ItemPedido item = null;

                        ValorTotal = 0;

                        foreach (ItemPedido _item in items)
                        {
                            ValorTotal += _item.PrecoTotal;
                        }
                        var pedido = new Pedido()
                        {
                            TotalPedido = ValorTotal + decimal.Parse(lblEntrega.Text.Replace("R$", "")),
                            TrocoPara = decimal.Parse(txtTrocoPara.Text),
                            FormaPagamento = this.cmbFPagamento.Text,
                            RealizadoEm = DateTime.Now
                        };

                        pedido.Tipo = cbxTipoPedido.Text;
                        if (ContraMesas && cbxTipoPedido.Text == "1 - Mesa")
                        {
                            pedido.NumeroMesa = cbxListaMesas.Text;
                        }
                        else
                        {
                            pedido.NumeroMesa = "";
                        }

                        if (codPedido != 0)
                        {
                            if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "EditaPedidoSN"))
                            {
                                return;
                            }
                            if (this.txtQuantidade.Text == "" || this.txtPrecoUnitario.Text == "" && this.txtPrecoTotal.Text == "")
                            {
                                MessageBox.Show("Informe a Quantidade.");
                            }
                            else
                            {
                                strItemsMarcados = PegaCheckBoxMarcado();
                                item = new ItemPedido()
                                {
                                    CodPedido = codPedido,
                                    NomeProduto = itemNome,
                                    Quantidade = decimal.Parse(this.txtQuantidade.Text),
                                    PrecoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.Replace("R$ ", "")),
                                    PrecoTotal = decimal.Parse(this.txtPrecoTotal.Text.Replace("R$ ", "")),
                                    Item = txtItemDescricao.Text.Insert(txtItemDescricao.Text.Length, Environment.NewLine) + strItemsMarcados,
                                    CodProduto = intCodigoProdutoBusca,
                                    FidelidadeSN = false,

                                };
                                if (decimal.Parse(txtPorcentagemDesconto.Text) > 0)
                                {
                                    item.DescontoPorcetagem = decimal.Parse(txtPorcentagemDesconto.Text);
                                }
                                pedido.HorarioEntrega = "";
                                if (cbxHorarioEntrega.Text != "")
                                {
                                    pedido.HorarioEntrega = cbxHorarioEntrega.Text;
                                }

                                item.DataAtualizacao = DateTime.Now;
                                pedido.Observacao = txtObsPedido.Text;
                                pedido.TotalPedido = pedido.TotalPedido + item.PrecoTotal;
                                pedido.Codigo = codPedido;
                                pedido.CodEndereco = prvCodEndecoSelecionado;
                                con.Insert("spAdicionarItemAoPedido", item);

                                AlteraTotalPedido();
                                items.Add(item);
                                atualizarGrid(item);
                                SemMeiaPizza();
                                iNomeProd = "";
                                txtPorcentagemDesconto.Text = "0";
                            }
                            AtualizaTotalPedido();
                        }
                        else
                        {

                            strItemsMarcados = PegaCheckBoxMarcado();
                            item = new ItemPedido()
                            {
                                CodPedido = 0,
                                NomeProduto = itemNome,
                                Quantidade = decimal.Parse(this.txtQuantidade.Text),
                                PrecoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.Replace("R$", "")),
                                PrecoTotal = decimal.Parse(this.txtPrecoTotal.Text.Replace("R$", "")),
                                Item = txtItemDescricao.Text.Insert(txtItemDescricao.Text.Length, Environment.NewLine) + strItemsMarcados,
                                CodProduto = intCodigoProdutoBusca

                            };
                            if (decimal.Parse(txtPorcentagemDesconto.Text) > 0)
                            {
                                item.DescontoPorcetagem = decimal.Parse(txtPorcentagemDesconto.Text);
                            }
                            items.Add(item);
                            atualizarGrid(item);

                        }
                        txtQuantidade.Text = "1";
                        this.txtItemDescricao.Text = "";
                        this.txtCodProduto1.Text = "";
                        if (ProdutosPorCodigo)
                        {
                            txtCodProduto1.Focus();
                        }
                        else
                        {
                            cbxTipoProduto.Focus();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Não foi possível adicionar o produto, verifique a quantidade digitada", "[xSistemas] Aviso");
                    txtQuantidade.Focus();

                }
            }
            catch (Exception ss)
            {

                MessageBox.Show(ss.Message);
            }
            txtPorcentagemDesconto.Text = "0";
            LimpaTamanhosSabores();
            chkSabores.Checked = false;
            cbxMeiaPizza.Checked = false;
            intCodProduto1Buscado = 0;
            intCodProduto2Buscado = 0;
            //MeiaPizzaMarcado(sender, e);
        }
        private void btnCancelarPedido_Click(object sender, EventArgs e)
        {
            gridViewItemsPedido.Rows.Clear();
            this.Close();
            //EXCLUIR ITEMS DO PEDIDO E DEPOIS REMOVER PEDIDO
        }

        private Boolean ValidaMaximoDesconto()
        {
            Boolean iReturn = false;
            double TotalPedido = Convert.ToDouble(SomaItensPedido()) + double.Parse(lblEntrega.Text.Replace("R$", ""));//double.Parse(lbTotal.Text.Replace("R$", ""));
            double DescontoSolicitado = double.Parse(txtDesconto.Text.Replace("R$", ""));
            double DescMAxPermitido = Sessions.retunrUsuario.DescontoMax;
            if (TotalPedido == 0 || DescontoSolicitado == 0)
            {
                return true;
            }

            double DescCalculado = (100 * DescontoSolicitado) / TotalPedido;
            return iReturn = DescCalculado <= DescMAxPermitido;
        }
        private int RetornaCodVendedor()
        {
            int iReturn = 0;
            try
            {

                if (!Sessions.returnConfig.ExigeVendedorSN)
                {
                    return Sessions.retunrUsuario.Codigo;
                }
                if (cbxVendedor.SelectedValue != null)
                {
                    iReturn = int.Parse(cbxVendedor.SelectedValue.ToString());
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cErroGravacao + erro.Message);
            }


            return iReturn;
        }
        public void btnGerarPedido_Click(object sender, EventArgs e)
        {
            btnGerarPedido.Enabled = false;
            try
            {
                if (cmbFPagamento.ValueMember == null)
                {
                    MessageBox.Show("Formaga de Pagamento não selecionada", "[xSistemas] Aviso");
                    cmbFPagamento.Focus();
                    return;
                }
                else
                {
                    if (txtSenha.Text != "" && cbxTipoPedido.Text == "2 - Balcao")
                    {
                        if (con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorSenha", 0, txtSenha.Text).Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show(Bibliotecas.cSenhaEmUso);
                            btnGerarPedido.Enabled = true;
                            return;
                        }

                    }

                    if (AtualizaTroco(false))
                    {
                        int totalDeItems = this.gridViewItemsPedido.RowCount;
                        if (totalDeItems == 0)
                        {
                            MessageBox.Show("O Pedido deve contem no mínimo um item.");
                        }
                        else
                        {
                            var pedido = new Pedido
                            {
                                CodPessoa = codPessoa,
                                TotalPedido = decimal.Parse(lbTotal.Text.Replace("R$", "")),
                                FormaPagamento = this.cmbFPagamento.Text,
                                RealizadoEm = dtPedido.Value,
                                Status = "Aberto",
                                PedidoOrigem = "Balcao",
                                CodigoPedidoWS = 0,
                                CodUsuario = RetornaCodVendedor(),
                                Observacao = txtObsPedido.Text,
                                CodEndereco = prvCodEndecoSelecionado,
                            };
                            if (txtSenha.Text != "")
                            {
                                pedido.Senha = txtSenha.Text;
                            }
                            if (txtTrocoPara.Text != "")
                            {
                                pedido.TrocoPara = decimal.Parse(txtTrocoPara.Text);
                            }
                            else
                            {
                                pedido.TrocoPara = decimal.Parse("0,00");
                            }

                            // Validar o Desconto Máximo Por Usuario
                            if (decimal.Parse(txtDesconto.Text) > 0)
                            {
                                if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "DescontoPedidoSN"))
                                {
                                    return;
                                }

                                if (ValidaMaximoDesconto())
                                {
                                    pedido.DescontoValor = decimal.Parse(txtDesconto.Text);
                                }
                                else
                                {
                                    MessageBox.Show("Desconto máximo superado ");
                                    pedido.DescontoValor = decimal.Parse("0,00");
                                    return;
                                }

                            }
                            //Finaliza Validação 

                            // DataEntrada = pedido.RealizadoEm;
                            if (ContraMesas)
                            {
                                if (cbxTipoPedido.Text != "")
                                {
                                    pedido.Tipo = this.cbxTipoPedido.Text;
                                }
                                else
                                {
                                    MessageBox.Show("Tipo de Pedido não pode ser vazio");
                                    cbxTipoPedido.Focus();

                                }

                            }
                            else
                            {
                                pedido.Tipo = "0 - Entrega";
                            }
                            if (cbxListaMesas.Visible)
                            {
                                pedido.NumeroMesa = cbxListaMesas.Text;
                                int intCodMesa = Utils.RetornaCodigoMesa(cbxListaMesas.Text);
                                pedido.CodigoMesa = intCodMesa;
                            }
                            else
                            {
                                pedido.NumeroMesa = "0";
                                //  pedido.CodigoMesa
                            }

                            if (Sessions.returnConfig.ExigeVendedorSN && pedido.CodUsuario == 0)
                            {
                                MessageBox.Show("Selecione o vendedor/atendente para continuar");
                                txtCodVendedor.Focus();
                                txtCodVendedor.BackColor = Color.Red;
                                btnGerarPedido.Enabled = true;

                                return;
                            }
                            pedido.HorarioEntrega = "";
                            if (cbxHorarioEntrega.Text != "")
                            {
                                pedido.HorarioEntrega = cbxHorarioEntrega.Text;
                            }
                            con.Insert("spAdicionarPedido", pedido);

                            for (int i = 0; i < gridViewItemsPedido.Rows.Count; i++)
                            {
                                var itemDoPedido = new ItemPedido()
                                {
                                    CodPedido = con.getLastCodigo(),
                                    CodProduto = int.Parse(gridViewItemsPedido.Rows[i].Cells["CodProduto"].Value.ToString()),
                                    NomeProduto = gridViewItemsPedido.Rows[i].Cells[2].Value.ToString(),
                                    Quantidade = decimal.Parse(gridViewItemsPedido.Rows[i].Cells[3].Value.ToString()),
                                    PrecoUnitario = decimal.Parse(gridViewItemsPedido.Rows[i].Cells[4].Value.ToString().Replace("R$", "")),
                                    PrecoTotal = decimal.Parse(gridViewItemsPedido.Rows[i].Cells[5].Value.ToString().Replace("R$", "")),
                                    ImpressoSN = false,
                                    Item = gridViewItemsPedido.Rows[i].Cells["Obs"].Value.ToString().ToUpper(),
                                    FidelidadeSN = bool.Parse(gridViewItemsPedido.Rows[i].Cells["FidelidadeSN"].Value.ToString())
                                };
                                itemDoPedido.DataAtualizacao = DateTime.Now;
                                con.Insert("spCriarPedido", itemDoPedido);
                                con.ConsultaInsumos(itemDoPedido.CodProduto, itemDoPedido.Quantidade);
                                Utils.ControlaEventos("Inserir", this.Name);


                            }

                            iCodPedido = con.getLastCodigo();
                            con.AtualizaSituacao(iCodPedido, Sessions.retunrUsuario.Codigo, 1);

                            if (ContraMesas && cbxTipoPedido.Text != "1 - Mesa")
                            {
                                prepareToPrint();
                            }
                            else if (!ContraMesas)
                            {
                                prepareToPrint();
                            }

                            this.Close();


                        }
                    }
                }

            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possivel gravar o pedido " + erro.Message);
            }
            btnGerarPedido.Enabled = true;
        }
        private Decimal SomaItensPedido()
        {
            decimal dcTotalPedido = 0;
            for (int i = 0; i < gridViewItemsPedido.Rows.Count; i++)
            {
                dcTotalPedido = dcTotalPedido + decimal.Parse(gridViewItemsPedido.Rows[i].Cells["Preço Total"].Value.ToString().Replace("R$ ", ""));
            }

            return dcTotalPedido; //+ decimal.Parse(lblEntrega.Text.Replace("R$", ""));
        }
        /// <summary>
        ///  Metodo para agrupar itens quando quando forem exatamente iguais
        /// </summary>
        /// <param name="intCodProduto">
        /// Codigo do Produto</param>
        /// <param name="iDescricao">
        /// Descrição adicional do produto ( Observação, adicional) </param>
        /// <returns></returns>
        private Boolean AgrupaItens(int intCodProduto, string iDescricao)
        {
            try
            {


            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return true;
        }
        private void AtualizaTotalPedido()
        {
            if (!ValidaMaximoDesconto())
            {
                MessageBox.Show("Desconto superior ao permitido no pedido");
                btnGerarPedido.Enabled = true;
                return;
            }
            //if (codPedido > 0)
            //{
            //    FinalizaPedido finaliza = new FinalizaPedido()
            //    {
            //        CodPedido = codPedido,
            //        CodPagamento = int.Parse(cmbFPagamento.SelectedValue.ToString()),
            //        ValorPagamento = SomaItensPedido() - Convert.ToDecimal
            //        (txtDesconto.Text) + DMargemGarco + decimal.Parse(lblEntrega.Text.Replace("R$", ""))

            //    };
            //    con.Update("spAlteraFinalizaPedido_Pedido", finaliza);
            //}
            if (txtTrocoPara.Text == "")
            {
                txtTrocoPara.Text = "0,00";
            }


            pedido = new Pedido()
            {
                Codigo = codPedido,
                TrocoPara = decimal.Parse(txtTrocoPara.Text),
                FormaPagamento = this.cmbFPagamento.Text,
                RealizadoEm = DateTime.Now,
                Tipo = cbxTipoPedido.Text,
                Observacao = txtObsPedido.Text
            };
            if (DMargemGarco != 0.00M)
            {
                decimal dTotalPedido = decimal.Parse(lbTotal.Text.Replace("R$", ""));
                pedido.TotalPedido = SomaItensPedido() - decimal.Parse(txtDesconto.Text) + DMargemGarco
                    + decimal.Parse(lblEntrega.Text.Replace("R$", ""));
            }
            else
            {
                pedido.TotalPedido = SomaItensPedido() - Convert.ToDecimal
                    (txtDesconto.Text) + decimal.Parse(lblEntrega.Text.Replace("R$", ""));
            }

            if (ContraMesas && cbxTipoPedido.Text == "1 - Mesa")
            {
                pedido.Tipo = cbxTipoPedido.Text;
                pedido.NumeroMesa = cbxListaMesas.Text;
                //  Utils.RetornaCodigoMesa(cbxListaMesas.Text);
                pedido.CodigoMesa = Utils.RetornaCodigoMesa(cbxListaMesas.Text);
                pedido.PedidoOrigem = pedido.PedidoOrigem;
            }
            else
            {
                pedido.Tipo = cbxTipoPedido.Text;
                pedido.NumeroMesa = "";
            }
            pedido.CodUsuario = RetornaCodVendedor();
            pedido.DescontoValor = decimal.Parse(txtDesconto.Text);
            if (Sessions.returnConfig.ExigeVendedorSN && pedido.CodUsuario == 0)
            {
                MessageBox.Show("Selecione o vendedor/atendente para continuar");
                txtCodVendedor.Focus();
                txtCodVendedor.BackColor = Color.Red;
                return;
            }
            pedido.HorarioEntrega = "";
            if (cbxHorarioEntrega.Text != "")
            {
                pedido.HorarioEntrega = cbxHorarioEntrega.Text;
            }
            pedido.Observacao = txtObsPedido.Text;
            pedido.CodEndereco = prvCodEndecoSelecionado;
            con.Update("spAlterarTotalPedido", pedido);
            lbTotal.Text = pedido.TotalPedido.ToString();
            // Utils.PopularGrid("Pedido", parentWindow.pedidosGridView);
        }
        private void AlterarItem(object sender, EventArgs e)
        {
            try
            {
                if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "EditaPedidoSN"))
                {
                    return;
                }

                if (!Utils.CaixaAberto(DateTime.Now, Sessions.retunrUsuario.CaixaLogado, Sessions.retunrUsuario.Turno))
                {
                    MessageBox.Show(Bibliotecas.cCaixaFechado);
                    return;
                }
                ValorTotal = 0;
                ValorTroco = 0;
                if (radioButton1.Visible && !radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked
                         && !radioButton4.Checked && !radioButton5.Checked && !radioButton6.Checked)
                {
                    MessageBox.Show("É obrigatório selecionar o tamanho ", "[xSistemas]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (!this.txtQuantidade.Text.Equals("")
                        && !this.txtPrecoUnitario.Text.Equals("") && !this.txtPrecoTotal.Text.Equals(""))
                {
                    foreach (ItemPedido item in items)
                    {
                        if (item.CodProduto == codigoItemParaAlterar)
                        {
                            item.PrecoTotal = decimal.Parse(this.txtPrecoTotal.Text.Replace("R$ ", ""));
                            item.Item = this.txtItemDescricao.Text.ToString();
                        }
                    }

                    foreach (ItemPedido item in items)
                    {
                        ValorTotal = ValorTotal + item.PrecoTotal;
                        if (txtTrocoPara.Text.ToString() != "")
                        {
                            ValorTroco = decimal.Parse(txtTrocoPara.Text.Replace("R$ ", "")) - ValorTotal;
                        }
                        else
                        {
                            ValorTroco = 0.00M;
                        }

                    }

                    MeiaPizza();
                    if (!itemNome.Equals("") && !this.txtQuantidade.Text.Equals("")
                        && !this.txtPrecoUnitario.Text.Equals("") && !this.txtPrecoTotal.Text.Equals(""))
                    {
                        int totalDeItems = this.gridViewItemsPedido.RowCount;
                        if (totalDeItems == 0)
                        {
                            MessageBox.Show("O Pedido deve contem no mínimo um item.");
                            return;
                        }
                        else
                        {
                            string strItens;
                            if (PegaCheckBoxMarcado() != "")
                            {
                                strItens = txtItemDescricao.Text + PegaCheckBoxMarcado();
                            }
                            else
                            {
                                strItens = txtItemDescricao.Text;
                            }

                            var itemPedido = new ItemPedido()
                            {
                                CodProduto = codigoItemParaAlterar,
                                CodPedido = codPedido,
                                NomeProduto = itemNome,
                                Quantidade = decimal.Parse(this.txtQuantidade.Text),
                                PrecoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.Replace("R$ ", "")),
                                PrecoTotal = decimal.Parse(this.txtPrecoTotal.Text.Replace("R$ ", "")),
                                Item = strItens

                            };

                            itemPedido.DataAtualizacao = DateTime.Now;
                            if (int.Parse(gridViewItemsPedido.CurrentRow.Cells["Codigo"].Value.ToString()) > 0)
                            {
                                itemPedido.Codigo = int.Parse(gridViewItemsPedido.CurrentRow.Cells["Codigo"].Value.ToString());
                            }
                            else
                            {
                                itemPedido.Codigo = iRowSelecionada;
                            }

                            this.gridViewItemsPedido.Rows[rowIndex].Cells[2].Value = itemPedido.NomeProduto;
                            this.gridViewItemsPedido.Rows[rowIndex].Cells[3].Value = itemPedido.Quantidade;
                            this.gridViewItemsPedido.Rows[rowIndex].Cells[4].Value = "R$ " + itemPedido.PrecoUnitario.ToString();
                            this.gridViewItemsPedido.Rows[rowIndex].Cells[5].Value = "R$ " + itemPedido.PrecoTotal.ToString();
                            this.gridViewItemsPedido.Rows[rowIndex].Cells[6].Value = itemPedido.Item.ToString();

                            var ValorPedidoTotal = ValorTotal + decimal.Parse(lblEntrega.Text.Replace("R$ ", ""));

                            if (DMargemGarco != 0.00M)
                            {
                                decimal TotalMesaGeral = DMargemGarco + ValorPedidoTotal;
                                this.lbTotal.Text = "R$ " + TotalMesaGeral.ToString();
                            }
                            else
                            {
                                this.lbTotal.Text = "R$ " + ValorPedidoTotal;
                            }

                            this.lblTroco.Text = "R$ " + TrocoPagar;
                            AtualizaTotalPedido();

                            if (codPedido != 0)
                            {
                                con.Update("spAlterarItemPedido", itemPedido);
                                Utils.ControlaEventos("Alterar", this.Name);
                            }


                            this.cbxProdutosGrid.Text = "";
                            this.txtPrecoUnitario.Text = "";
                            this.txtQuantidade.Text = "";
                            this.txtPrecoTotal.Text = "";
                            this.txtItemDescricao.Text = "";

                            // MessageBox.Show("Item alterado com sucesso.", "[xSistemas]");

                            Utils.MontaCombox(cbxTipoProduto, "NomeGrupo", "Codigo", "Grupo", "spObterGrupoAtivo");
                            txtPorcentagemDesconto.Text = "0";
                            this.btnAdicionarItemNoPedido.Text = "Adicionar";
                            this.btnAdicionarItemNoPedido.Click += new System.EventHandler(this.btnAdicionarItemNoPedido_Click);
                            this.btnAdicionarItemNoPedido.Click -= new System.EventHandler(this.AlterarItem);
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        private void gridViewItemsPedido_MouseClick(object sender, MouseEventArgs e)
        {
            if (!btnGerarPedido.Enabled || !btnReimprimir.Enabled)
            {
                // MessageBox.Show("Não é possivel executar essa ação para esse pedido");
                return;
            }
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem excluirItemPedido = new MenuItem("Excluir");
                excluirItemPedido.Click += ExcluirItem;
                m.MenuItems.Add(excluirItemPedido);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;

                m.Show(dgv, new Point(e.X, e.Y));

            }
        }
        private void AlterarItemPedido(object sender, EventArgs e)
        {
            //   if (ValidaTroco())
            //{
            if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "EditaPedidoSN"))
            {
                return;
            }

            if (!Utils.CaixaAberto(DateTime.Now, Sessions.retunrUsuario.CaixaLogado, Sessions.retunrUsuario.Turno))
            {
                MessageBox.Show(Bibliotecas.cCaixaFechado);
                return;
            }
            ValorTotal = 0;
            ValorTroco = 0;
            if (radioButton1.Visible && !radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked
                     && !radioButton4.Checked && !radioButton5.Checked && !radioButton6.Checked)
            {
                MessageBox.Show("É obrigatório selecionar o " + grpBoxTamanhos.Text, "[xSistemas]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!this.txtQuantidade.Text.Equals("")
                    && !this.txtPrecoUnitario.Text.Equals("") && !this.txtPrecoTotal.Text.Equals(""))
            {
                foreach (ItemPedido item in items)
                {
                    if (item.CodProduto == codigoItemParaAlterar)
                    {
                        item.PrecoTotal = decimal.Parse(this.txtPrecoTotal.Text.Replace("R$ ", ""));
                        item.Item = this.txtItemDescricao.Text.ToString();
                    }
                }

                foreach (ItemPedido item in items)
                {
                    ValorTotal = ValorTotal + item.PrecoTotal;
                    if (txtTrocoPara.Text.ToString() != "")
                    {
                        ValorTroco = decimal.Parse(txtTrocoPara.Text.Replace("R$ ", "")) - ValorTotal;
                    }
                    else
                    {
                        ValorTroco = 0.00M;
                    }

                }

                // Valida Se esta adicionando Meia Pizza
                MeiaPizza();

                if (!itemNome.Equals("") && !this.txtQuantidade.Text.Equals("")
                    && !this.txtPrecoUnitario.Text.Equals("") && !this.txtPrecoTotal.Text.Equals(""))
                {
                    int totalDeItems = this.gridViewItemsPedido.RowCount;
                    if (totalDeItems == 0)
                    {
                        MessageBox.Show("O Pedido deve contem no mínimo um item.");
                        return;
                    }
                    else
                    {
                        string strItens = PegaCheckBoxMarcado();
                        var itemPedido = new ItemPedido()
                        {
                            CodProduto = codigoItemParaAlterar,
                            CodPedido = codPedido,
                            NomeProduto = itemNome,
                            Quantidade = decimal.Parse(this.txtQuantidade.Text),
                            PrecoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.Replace("R$ ", "")),
                            PrecoTotal = decimal.Parse(this.txtPrecoTotal.Text.Replace("R$ ", "")),
                            Item = txtItemDescricao.Text.Insert(txtItemDescricao.Text.Length, Environment.NewLine) + strItens

                        };

                        itemPedido.DataAtualizacao = DateTime.Now;

                        this.gridViewItemsPedido.Rows[rowIndex].Cells[1].Value = itemPedido.NomeProduto;
                        this.gridViewItemsPedido.Rows[rowIndex].Cells[2].Value = itemPedido.Quantidade;
                        this.gridViewItemsPedido.Rows[rowIndex].Cells[3].Value = "R$ " + itemPedido.PrecoUnitario.ToString();
                        this.gridViewItemsPedido.Rows[rowIndex].Cells[4].Value = "R$ " + itemPedido.PrecoTotal.ToString();
                        this.gridViewItemsPedido.Rows[rowIndex].Cells[5].Value = itemPedido.Item.ToString();

                        var ValorPedidoTotal = ValorTotal + decimal.Parse(lblEntrega.Text);

                        if (DMargemGarco != 0.00M)
                        {
                            decimal TotalMesaGeral = DMargemGarco + ValorPedidoTotal;
                            this.lbTotal.Text = "R$ " + TotalMesaGeral.ToString();
                        }
                        else
                        {
                            this.lbTotal.Text = "R$ " + ValorPedidoTotal;
                        }

                        this.lblTroco.Text = "R$ " + TrocoPagar;
                        AtualizaTroco();
                        AtualizaTotalPedido();
                        FinalizaPedido finaliza = new FinalizaPedido()
                        {
                            CodPedido = iCodPedido,
                            CodPagamento = int.Parse(cmbFPagamento.SelectedValue.ToString()),
                            ValorPagamento = pedido.TotalPedido
                        };

                        con.Update("spAlteraFinalizaPedido_Pedido", finaliza);

                        con.Update("spAlterarItemPedido", itemPedido);

                        Utils.ControlaEventos("Alterar", this.Name);

                        this.cbxProdutosGrid.Text = "";
                        this.txtPrecoUnitario.Text = "";
                        this.txtQuantidade.Text = "";
                        this.txtPrecoTotal.Text = "";
                        this.txtItemDescricao.Text = "";

                        if (ContraMesas && cbxListaMesas.Visible)
                        {
                            int CodigoMesa = Utils.RetornaCodigoMesa(cbxListaMesas.Text);
                            Utils.AtualizaMesa(CodigoMesa, 2);
                        }

                        MessageBox.Show("Item alterado com sucesso.", "[xSistemas]");
                        cbxTipoProduto.DataSource = con.SelectAll("Grupo", "spObterGrupoAtivo").Tables[0];
                        cbxTipoProduto.DisplayMember = "NomeGrupo";
                        cbxTipoProduto.ValueMember = "Codigo";
                    }
                }
            }
            else
            {
                return;
            }
            //  }
        }
        private void ExcluirItem(object sender, EventArgs e)
        {
            try
            {
                if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "EditaPedidoSN"))
                {
                    return;
                }
                if (gridViewItemsPedido.SelectedRows.Count > 0)
                {
                    string iNomeProduto = gridViewItemsPedido.CurrentRow.Cells["Nome do Produto"].Value.ToString();
                    codigoItemParaAlterar = int.Parse(gridViewItemsPedido.CurrentRow.Cells["CodProduto"].Value.ToString());
                    int CodigoLinha = int.Parse(gridViewItemsPedido.CurrentRow.Cells["Codigo"].Value.ToString());
                    if (!Utils.MessageBoxQuestion("Deseja excluir o item " + iNomeProduto
                        + " Do Pedido?"))
                    {
                        return;
                    }
                    if (gridViewItemsPedido.Rows.Count == 1 && (codPedido != 0 && !PedidoRepetio))
                    {
                        MessageBox.Show("Item não pode ser excluído pois o pedido deve conter 1 ou mais itens");
                        return;
                    }
                    //  rowIndex = this.gridViewItemsPedido.CurrentRow.Index;

                    var itemPedido = new ItemPedido()
                    {
                        CodProduto = codigoItemParaAlterar,
                        CodPedido = codPedido,
                        Codigo = CodigoLinha,
                        NomeProduto = iNomeProduto

                    };

                    ValorTotal = ValorTotal - decimal.Parse(((this.gridViewItemsPedido.CurrentRow.Cells["Preço Total"].Value.ToString()).Replace("R$ ", "")));
                    this.lbTotal.Text = "R$ " + Convert.ToString(ValorTotal + decimal.Parse(lblEntrega.Text.Replace("R$ ", "")));
                    this.gridViewItemsPedido.Rows.RemoveAt(rowIndex);
                    items.RemoveAt(rowIndex);

                    this.txtPrecoUnitario.Text = "";
                    this.txtQuantidade.Text = "1";
                    this.txtPrecoTotal.Text = "";

                    ValorTotal = 0;

                    foreach (ItemPedido _item in items)
                    {
                        ValorTotal = ValorTotal + _item.PrecoTotal;
                    }

                    var pedido = new Pedido()
                    {
                        Codigo = codPedido,
                        TotalPedido = ValorTotal,
                        Tipo = cbxTipoPedido.Text

                    };
                    if (ContraMesas && cbxListaMesas.Visible)
                    {
                        int CodMesa = Utils.RetornaCodigoMesa(cbxListaMesas.Text);
                        pedido.NumeroMesa = cbxListaMesas.Text;
                        pedido.CodigoMesa = CodMesa;
                        Utils.AtualizaMesa(CodMesa, 2);
                    }
                    else
                    {
                        pedido.NumeroMesa = "";
                    }
                    pedido.HorarioEntrega = "";
                    if (cbxHorarioEntrega.Text != "")
                    {
                        pedido.HorarioEntrega = cbxHorarioEntrega.Text;
                    }
                    pedido.Observacao = txtObsPedido.Text;
                    pedido.CodEndereco = prvCodEndecoSelecionado;

                    con.Delete("spExcluirItemPedido", itemPedido);
                    AtualizaTotalPedido();
                    con.Update("spAlterarTotalPedido", pedido);
                    Utils.ControlaEventos("Excluir", this.Name);
                    AtualizaTroco();
                    //  MessageBox.Show("Item excluído com sucesso.", "xSistemas");
                }
                else
                {
                    MessageBox.Show("Selecione o produto para alterar", "Aviso");
                }
            }
            catch (Exception err)
            {

                MessageBox.Show(Bibliotecas.cException + err.Message);
            }


        }
        public void atualizarGrid(ItemPedido itemDoPedido)
        {
            var troco = this.txtTrocoPara.Text.ToString();
            ValorTotal = 0;
            ValorTroco = 0;

            foreach (ItemPedido item in items)
            {
                ValorTotal += item.PrecoTotal;
            }


            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Codigo", typeof(string)));
            dt.Columns.Add(new DataColumn("CodProduto", typeof(string)));
            dt.Columns.Add(new DataColumn("Nome do Produto", typeof(string)));
            dt.Columns.Add(new DataColumn("Quantidade", typeof(string)));
            dt.Columns.Add(new DataColumn("Preço Unitário", typeof(string)));
            dt.Columns.Add(new DataColumn("Preço Total", typeof(string)));
            dt.Columns.Add(new DataColumn("Obs", typeof(string)));
            dt.Columns.Add(new DataColumn("ImpressoSN", typeof(bool)));
            dt.Columns.Add(new DataColumn("FidelidadeSN", typeof(bool)));
            dt.Columns.Add(new DataColumn("DescontoPorcetagem", typeof(decimal)));
            DataRow row;

            for (int i = 0; i < items.Count; i++)
            {
                row = dt.NewRow();

                row["Codigo"] = items[i].Codigo;
                row["CodProduto"] = items[i].CodProduto;
                row["Nome do Produto"] = items[i].NomeProduto;
                row["Quantidade"] = items[i].Quantidade;
                row["Preço Unitário"] = "R$ " + items[i].PrecoUnitario;
                row["Preço Total"] = "R$ " + items[i].PrecoTotal;
                row["Obs"] = items[i].Item;
                row["ImpressoSN"] = items[i].ImpressoSN;
                row["FidelidadeSN"] = items[i].FidelidadeSN;
                row["DescontoPorcetagem"] = items[i].DescontoPorcetagem;
                dt.Rows.Add(row);
            }

            this.gridViewItemsPedido.AutoGenerateColumns = true;
            this.gridViewItemsPedido.DataSource = dt;
            gridViewItemsPedido.Columns["DescontoPorcetagem"].Visible = false;
            gridViewItemsPedido.Columns["FidelidadeSN"].Visible = false;
            this.lbTotal.Text = "R$ " + Convert.ToString(ValorTotal - decimal.Parse(txtDesconto.Text) + Convert.ToDecimal(lblEntrega.Text.Replace("R$", "")));
            this.lblTroco.Text = Convert.ToString(lblTroco.Text);
        }
        private void prepareToPrint()
        {
            try
            {
                int iCodigo;
                AtualizaTotalPedido();
                if (ContraMesas && cbxTipoPedido.Text == "1 - Mesa")
                {
                    string iRetorno;
                    if (con.getLastCodigo() != 0)
                    {
                        iCodigo = con.getLastCodigo();
                    }
                    else
                    {
                        iCodigo = codPedido;
                    }
                    if (Sessions.returnConfig.QtdCaracteresImp <= 30)
                    {
                        iRetorno = Utils.ImpressaoFechamentoNovo_20(iCodigo, QtdViasBalcao);
                    }
                    else if (Sessions.returnConfig.QtdCaracteresImp >= 30)
                    {
                        iRetorno = Utils.ImpressaoFechamentoNovo(iCodigo, QtdViasBalcao, strNomeImpressoraContaMesa);
                    }


                }
                // Impressão de Venda Balcão
                if (cbxTipoPedido.Text == "2 - Balcao" && ImprimeViaBalcao)
                {

                    if (con.getLastCodigo() != 0)
                    {
                        iCodigo = con.getLastCodigo();
                    }
                    else
                    {
                        iCodigo = codPedido;
                    }

                    string iRetorno = Utils.ImpressaoBalcao(iCodigo, QtdViasBalcao, strNomeImpressoraBalcao);
                }

                // Imprimindo via Entrega
                if (ImprimeViaDelivery && cbxTipoPedido.Text == "0 - Entrega")
                {
                    string iRetorno;
                    if (con.getLastCodigo() != 0)
                    {
                        iCodigo = con.getLastCodigo();
                    }
                    else
                    {
                        iCodigo = codPedido;
                    }

                    if (Sessions.returnConfig.QtdCaracteresImp <= 30)
                    {
                        iRetorno = Utils.ImpressaoEntreganova_20(iCodigo, decimal.Parse(lblTroco.Text.Replace("R$", "")), QtdViasDelivery);
                    }
                    else if (Sessions.returnConfig.QtdCaracteresImp >= 40)
                    {
                        decimal dbTotalReceber = decimal.Parse(txtTrocoPara.Text);
                        if (dbTotalReceber == 0)
                        {
                            dbTotalReceber = decimal.Parse(lbTotal.Text.Replace("R$", ""));
                        }
                        decimal dblTroco = decimal.Parse(txtTrocoPara.Text) - decimal.Parse(lbTotal.Text.Replace("R$", ""));
                        if (dblTroco < 0)
                        {
                            dblTroco = 0;
                        }
                        Utils.ImpressaoEntreganova(iCodigo, dbTotalReceber,
                            QtdViasDelivery, strNomeImpressoraDelivery, prvCodEndecoSelecionado, dblTroco);
                    }
                }

                if (ImprimeViaCozinha)
                {
                    if (con.getLastCodigo() != 0)
                    {
                        iCodigo = con.getLastCodigo();
                    }
                    else
                    {
                        iCodigo = codPedido;
                    }
                    if (TipoAgrupamentoCozinha == "Sem Agrupamento")
                    {
                        Utils.ImpressaoCozihanova(iCodigo);
                    }
                    else
                    {
                        ImpressaoPorCozinha(iCodigo);
                    }

                }
            }
            catch (Exception E)
            {

                MessageBox.Show("Não foi possivel imprimir " + E.Message, " [xSistemas] ");
            }

        }
        /// <summary>
        /// Impressão separada do ticket de cozinha de acordo com o tipo de agrupamento
        /// </summary>
        /// <param name="iCodPedido"> Código do pedido a ser impresso
        /// </param>
        private void ImpressaoPorCozinha(int iCodPedido)
        {
            try
            {
                DataSet itemsPedido, dsItemsNaoImpresso;
                DataSet dsI = new DataSet();
                dsItemsNaoImpresso = Utils.CarregaItens(iCodPedido);

                if (dsItemsNaoImpresso.Tables.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < dsItemsNaoImpresso.Tables["ItemsPedido"].Rows.Count; i++)
                {
                    int CodGrupo = dsItemsNaoImpresso.Tables[0].Rows[i].Field<int>("CodGrupo");
                    string iNomeImpressora = dsItemsNaoImpresso.Tables[0].Rows[i].Field<string>("NomeImpressora");

                    if (TipoAgrupamentoDelivery == "Por Cozinha/Grupo")
                    {
                        //if (!ProdutosPorCodigo && TipoCodigo != "Personalizado")
                        //{
                        itemsPedido = con.SelectRegistroPorCodigo("ItemsPedido", "spObterItemsNaoImpressoPorGrupo", iCodPedido, "", CodGrupo);
                        if (itemsPedido.Tables[0].Rows.Count > 0)
                        {
                            Utils.ImpressaoDelivery_CozinhaPorGrupo(iCodPedido, iNomeImpressora, CodGrupo);
                        }
                    }
                    else
                    {
                        itemsPedido = con.SelectItensPorImpressora(iCodPedido, iNomeImpressora);
                        if (itemsPedido.Tables[0].Rows.Count == 0)
                        {
                            return;
                        }
                        Utils.ImpressaoDeliveryCoziha_SeparadoPorImpressora(iCodPedido, iNomeImpressora);
                    }
                    for (int intfor = 0; intfor < itemsPedido.Tables["ItemsPedido"].Rows.Count; intfor++)
                    {
                        AtualizaItemsImpresso Atualiza = new AtualizaItemsImpresso();
                        Atualiza.CodPedido = iCodPedido;
                        Atualiza.CodProduto = itemsPedido.Tables["ItemsPedido"].Rows[intfor].Field<int>("CodProduto");
                        Atualiza.ImpressoSN = true;
                        con.Update("spInformaItemImpresso", Atualiza);
                    }

                }


            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel imprimir o Item da Mesa verificar a impressora" + erro.Message);
            }
        }

        private string QuebrarString(string texto)
        {
            int totalDeCaracters = texto.Length - 1;
            var TamanhoCaracterImpressao = Sessions.returnConfig.QtdCaracteresImp.ToString();
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
        private void btnReimprimir_Click(object sender, EventArgs e)
        {
            prepareToPrint();

        }
        private void LimpaTamanhosSabores()
        {
            grpBoxTamanhos.Enabled = false;
            // Percorre o GroupBox dos tamanhos e e  desmarca todos pra obrigar o usuario marcar o tamanho depois de selecionar os tamanhos
            foreach (System.Windows.Forms.Control ctrControl in grpBoxTamanhos.Controls)
            {
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    //Unselect all RadioButtons
                    ((System.Windows.Forms.RadioButton)ctrControl).Checked = false;
                }
            }
            // Percorre o List para limpar as selecões
            for (int i = 0; i < chkListAdicionais.Items.Count; i++)
            {
                chkListAdicionais.SetItemChecked(i, false);
            }
        }

        private void cbxMeiaPizza_CheckedChanged(object sender, EventArgs e)
        {
            LimpaTamanhosSabores();

            if (cbxMeiaPizza.Checked)
            {
                this.cbxSabor.Enabled = true;
                if (ProdutosPorCodigo == true)
                {
                    this.txtCodProduto2.Visible = true;
                }
                else
                {
                    this.txtCodProduto2.Visible = false;
                    int CodPai = con.BuscaPaiGrupo(int.Parse(cbxTipoProduto.SelectedValue.ToString()));
                    if (CodPai != 0)
                    {
                        this.cbxSabor.DataSource = con.SelectRegistroPorCodigo("Produto", "spObterProdutoPorCodPai", CodPai).Tables[0];
                    }
                    else
                    {
                        this.cbxSabor.DataSource = con.SelectRegistroPorNome("@GrupoProduto", "Produto", "spObterProdutoPorGrupo", this.cbxTipoProduto.Text).Tables["Produto"];
                    }

                    this.cbxSabor.DisplayMember = Convert.ToString("NomeProduto");
                    this.cbxSabor.ValueMember = "Codigo";
                }

            }
            else
            {
                this.cbxSabor.Enabled = false;
                this.cbxSabor.Text = "";
                this.txtCodProduto2.Visible = false;
            }
        }
        //public decimal ComparaValores()
        //{
        //    var valorProduto = decimal.Parse("0.00");
        //    decimal[] valores = new decimal[2];
        //    var valorSabor = decimal.Parse("0.00");
        //    decimal iValue = 0.00M;
        //    int intCodInterno = 0;
        //    if (cbxProdutosGrid.Text != null)
        //    {
        //        if (ProdutosPorCodigo)
        //        {
        //            DataTable produto;
        //            DataTable sabor;

        //            if (txtCodProduto1.Text != "")
        //            {
        //                if (chkCodPersonalizado.Checked)
        //                {
        //                    intCodInterno = int.Parse(txtCodProduto1.Text);
        //                    produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompletoPorCodPersonalizado", intCodInterno).Tables["Produto"];
        //                }
        //                else
        //                {
        //                    produto = con.SelectProdutoCompleto("Produto", "spObterProdutoPorCodigo", int.Parse(txtCodProduto1.Text)).Tables["Produto"];
        //                }
        //                //produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(txtCodProduto1.Text)).Tables["Produto"];
        //            }
        //            else
        //            {
        //                produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];
        //            }





        //            if (txtCodProduto2.Text != "")
        //            {
        //                if (chkCodPersonalizado.Checked)
        //                {
        //                    intCodInterno = int.Parse(txtCodProduto1.Text);
        //                    sabor = con.SelectProdutoCompleto("Produto", "spObterProdutoCompletoPorCodPersonalizado", intCodInterno).Tables["Produto"];
        //                }
        //                else
        //                {
        //                    sabor = con.SelectProdutoCompleto("Produto", "spObterProdutoPorCodigo", int.Parse(txtCodProduto1.Text)).Tables["Produto"];
        //                }
        //                // sabor = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(txtCodProduto2.Text)).Tables["Produto"];
        //            }

        //            if (PromocaoDiasSemana)
        //            {
        //                DiaDaPromocao = produto.Rows[0]["DiaSemana"].ToString();
        //                lol = DiaDaPromocao.Split(new char[] { ';' });
        //                List<PrecoDiaProduto> listPreco = new List<PrecoDiaProduto>();

        //                listPreco = Utils.DeserializaObjeto(produto.Rows[0]["DiaSemana"].ToString());
        //                if (listPreco != null && listPreco.Count > 0)
        //                {
        //                    foreach (var item in listPreco)
        //                    {
        //                        if (item.Dia == DiaDaSema && item.Preco != 0 && item.Preco != null)
        //                        {
        //                            txtPrecoUnitario.Text = item.Preco.ToString();
        //                            valorProduto = item.Preco;
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            txtPrecoUnitario.Text = produto.Rows[0]["PrecoProduto"].ToString();
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    valorProduto = decimal.Parse(produto.Rows[0]["PrecoProduto"].ToString());
        //                    //  valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
        //                }

        //                CalcularTotalItem();

        //            }
        //            else
        //            {
        //                valorProduto = decimal.Parse(produto.Rows[0]["PrecoProduto"].ToString());
        //                // valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
        //            }

        //            txtPrecoUnitario.Text = Convert.ToString(RetornaMaiorValor(valorProduto, valorSabor));
        //            //if (valorProduto > valorSabor)
        //            //{
        //            //    this.txtPrecoUnitario.Text = valorProduto.ToString();
        //            //}
        //            //else
        //            //{
        //            //    this.txtPrecoUnitario.Text = valorSabor.ToString();
        //            //}



        //        }
        //        else if (!ProdutosPorCodigo && this.cbxSabor.Text != " ")
        //        {
        //            var produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];
        //            // MontaMenuOpcoes(int.Parse(cbxProdutosGrid.SelectedValue.ToString()));
        //            DataTable sabor = null;
        //            if (cbxSabor.Enabled)
        //            {
        //                sabor = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxSabor.SelectedValue.ToString())).Tables["Produto"];
        //                //  MontaMenuOpcoes(int.Parse(cbxProdutosGrid.SelectedValue.ToString()), int.Parse(cbxSabor.SelectedValue.ToString()));
        //            }

        //            List<PrecoDiaProduto> listPreco = new List<PrecoDiaProduto>();
        //            listPreco = Utils.DeserializaObjeto(produto.Rows[0]["DiaSemana"].ToString());
        //            if (listPreco != null && listPreco.Count > 0)
        //            {
        //                foreach (var item in listPreco)
        //                {
        //                    if (item.Dia == DiaDaSema && item.Preco != 0 && item.Preco != null)
        //                    {
        //                        txtPrecoUnitario.Text = item.Preco.ToString();
        //                        valorProduto = decimal.Parse(txtPrecoUnitario.Text.Replace("R$", ""));
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        txtPrecoUnitario.Text = produto.Rows[0]["PrecoProduto"].ToString();
        //                    }
        //                    valorProduto = decimal.Parse(txtPrecoUnitario.Text.Replace("R$", ""));
        //                }
        //            }
        //            else
        //            {
        //                valorProduto = decimal.Parse(produto.Rows[0]["PrecoProduto"].ToString());
        //                if (cbxSabor.Enabled)
        //                {
        //                    valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
        //                }

        //            }

        //        }

        //        valores[0] = valorProduto;
        //        valores[1] = valorSabor;

        //        txtPrecoUnitario.Text = valores.Max().ToString();
        //        // Calcula o preço total forçando a multiplicação
        //        txtPrecoTotal.Text = Convert.ToString(decimal.Parse(txtQuantidade.Text) * valores.Max());


        //    }
        //    return valores.Max();
        //}
        internal void changeValorTotal(decimal p)
        {
            this.lbTotal.Text = p.ToString() + lblEntrega.Text;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizaTroco();
            AtualizaTotalPedido();
        }

        private Boolean AtualizaTroco(Boolean SemTroco = false)
        {
            bool Ok;
            if (!SemTroco)
            {
                decimal troco = 0.00M;

                decimal TotalPedido = decimal.Parse(lbTotal.Text.Replace("R$", ""));

                if (this.txtTrocoPara.Text != "")
                {
                    troco = decimal.Parse(txtTrocoPara.Text);
                }
                if (cbxTipoPedido.Text != "1 - Mesa" && TotalPedido > troco && cbxTrocoParaOK.Checked == false)
                {
                    MessageBox.Show("TROCO é menor que o Total do Pedido.");
                    txtTrocoPara.Focus();
                    txtTrocoPara.BackColor = Color.Aqua;
                    // txtTrocoPara.Font.Style = Font.Bold;
                    Ok = false;
                }
                else
                {
                    var pedido = new Pedido()
                    {
                        Codigo = codPedido,
                        TotalPedido = TotalPedido,
                        TrocoPara = decimal.Parse("0,00"),
                        FormaPagamento = this.cmbFPagamento.Text
                    };
                    if (cbxTrocoParaOK.Checked == false && !troco.Equals("0.00"))
                    {
                        ValorTroco = decimal.Parse(txtTrocoPara.Text.ToString()) - TotalPedido;
                        this.lblTroco.Text = "R$ " + Convert.ToString(ValorTroco);
                    }
                    else
                    {
                        ValorTroco = 0.00M;
                        this.lblTroco.Text = "R$ " + Convert.ToString(ValorTroco);
                    }

                    con.Update("spAlterarTrocoParaFormaPagamento", pedido);
                    Ok = true;
                }
            }
            else
            {
                Ok = true;
            }

            return Ok;
        }

        private void ExibirReceita(object sender, EventArgs e)
        {
            if (cbxProdutosGrid.Text != "")
            {
                frmReceita frm = new frmReceita(int.Parse(cbxProdutosGrid.SelectedValue.ToString()));
                frm.Show();
            }
        }
        private bool ValidaTroco()
        {
            if (cbxTrocoParaOK.Checked == true)
            {

                txtTrocoPara.Text = "0,00";
                lblTroco.Text = "R$ 0,00";
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuscaProduto1(object sender, KeyEventArgs e)
        {
            int intCodInterno = 0;
            DataTable produto;
            if (e.KeyCode == Keys.Enter)
            {

                if (txtCodProduto1.Text != "")
                {
                    if (chkCodPersonalizado.Checked)
                    {
                        intCodInterno = int.Parse(txtCodProduto1.Text);
                        produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompletoPorCodPersonalizado", intCodInterno).Tables["Produto"];
                    }
                    else
                    {
                        produto = con.SelectProdutoCompleto("Produto", "spObterProdutoPorCodigo", int.Parse(txtCodProduto1.Text)).Tables["Produto"];
                    }



                    if (produto.Rows.Count > 0)
                    {
                        cbxProdutosGrid.Text = produto.Rows[0]["NomeProduto"].ToString();
                        intCodProduto1Buscado = int.Parse(produto.Rows[0]["Codigo"].ToString());
                        MontaMenuOpcoes(intCodProduto1Buscado, intCodProduto2Buscado);
                        if (PromocaoDiasSemana)
                        {
                            List<PrecoDiaProduto> listPreco = new List<PrecoDiaProduto>();
                            listPreco = Utils.DeserializaObjeto(produto.Rows[0]["DiaSemana"].ToString());

                            if (listPreco.Count > 0 && listPreco != null)
                            {
                                foreach (var item in listPreco)
                                {
                                    if (item.Dia == DiaDaSema && item.Preco != null && item.Preco != 0)
                                    {
                                        txtPrecoUnitario.Text = item.Preco.ToString();
                                        break;
                                    }
                                    else
                                    {
                                        txtPrecoUnitario.Text = produto.Rows[0]["PrecoProduto"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                txtPrecoUnitario.Text = produto.Rows[0]["PrecoProduto"].ToString();
                            }

                            //if (DiaDaPromocao.IndexOf(DiaDaSema) > 0)
                            //{
                            //    txtPrecoUnitario.Text = produto.Rows[0]["PrecoDesconto"].ToString();
                            //}
                            //else
                            //{
                            //    txtPrecoUnitario.Text =produto.Rows[0]["PrecoProduto"].ToString();
                            //}
                        }
                        else
                        {
                            txtPrecoUnitario.Text = produto.Rows[0]["PrecoProduto"].ToString();
                        }

                        this.txtQuantidade.Text = "1";
                        CalcularTotalItem();
                        this.txtQuantidade.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Produto não encontrado");
                    }

                    iNomeProd = cbxProdutosGrid.Text;
                }
            }
        }

        private void BuscaProduto2(object sender, KeyEventArgs e)
        {
            int intCodInterno = 0;
            DataTable produto;
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCodProduto2.Text != "")
                {
                    if (chkCodPersonalizado.Checked)
                    {
                        intCodInterno = int.Parse(txtCodProduto2.Text);
                        produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompletoPorCodPersonalizado", intCodInterno).Tables["Produto"];
                    }
                    else
                    {
                        produto = con.SelectProdutoCompleto("Produto", "spObterProdutoPorCodigo", int.Parse(txtCodProduto1.Text)).Tables["Produto"];
                    }

                    // produto = con.SelectProdutoCompleto("Produto", "spObterProdutoPorCodigo", int.Parse(txtCodProduto2.Text)).Tables["Produto"];
                    if (produto.Rows.Count > 0)
                    {
                        intCodProduto2Buscado = int.Parse(produto.Rows[0]["Codigo"].ToString());
                        MontaMenuOpcoes(intCodProduto1Buscado, intCodProduto2Buscado);
                        cbxSabor.Text = produto.Rows[0]["NomeProduto"].ToString();
                        if (PromocaoDiasSemana)
                        {
                            List<PrecoDiaProduto> listPreco = new List<PrecoDiaProduto>();
                            listPreco = Utils.DeserializaObjeto(produto.Rows[0]["DiaSemana"].ToString());

                            foreach (var item in listPreco)
                            {
                                if (item.Dia == DiaDaSema)
                                {
                                    txtPrecoUnitario.Text = item.Preco.ToString();
                                    break;
                                }
                                else
                                {
                                    txtPrecoUnitario.Text = produto.Rows[0]["PrecoProduto"].ToString();
                                }
                            }
                        }
                        else
                        {
                            txtPrecoUnitario.Text = produto.Rows[0]["PrecoProduto"].ToString();
                        }

                        this.txtQuantidade.Text = "1";
                        CalcularTotalItem();
                        this.txtQuantidade.Focus();
                        txtQuantidade.Text = "1";
                        // ComparaValores();

                    }
                    else
                    {
                        MessageBox.Show("Produto não encontrado");
                    }
                }
                iNomeProd = cbxSabor.Text;
            }

        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastrarPedido));
            this.itemsPedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.cbxTipoProduto = new System.Windows.Forms.ComboBox();
            this.lblGrupo = new System.Windows.Forms.Label();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrecoUnitario = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrecoTotal = new System.Windows.Forms.TextBox();
            this.btnAdicionarItemNoPedido = new System.Windows.Forms.Button();
            this.btnCancelarPedido = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.lblDébito = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAtlCadastro = new System.Windows.Forms.Button();
            this.lblTempo = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            this.lbTotalPedido = new System.Windows.Forms.Label();
            this.btnGerarPedido = new System.Windows.Forms.Button();
            this.txbProduto = new System.Windows.Forms.Label();
            this.cbxProdutosGrid = new System.Windows.Forms.ComboBox();
            this.gridViewItemsPedido = new System.Windows.Forms.DataGridView();
            this.lblTrocoPara = new System.Windows.Forms.Label();
            this.txtTrocoPara = new System.Windows.Forms.TextBox();
            this.lblFormaDePagamento = new System.Windows.Forms.Label();
            this.txtItemDescricao = new System.Windows.Forms.TextBox();
            this.lblDescricaoDoItem = new System.Windows.Forms.Label();
            this.btnReimprimir = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtPedido = new System.Windows.Forms.DateTimePicker();
            this.lblStatusPedido = new System.Windows.Forms.Label();
            this.btnMultiploPagamento = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCalGarcon = new System.Windows.Forms.Button();
            this.lblEntrega = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTroco = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxMeiaPizza = new System.Windows.Forms.CheckBox();
            this.cbxSabor = new System.Windows.Forms.ComboBox();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDesconto = new System.Windows.Forms.TextBox();
            this.cmbFPagamento = new System.Windows.Forms.ComboBox();
            this.cbxTrocoParaOK = new System.Windows.Forms.CheckBox();
            this.btnReceita = new System.Windows.Forms.Button();
            this.txtCodProduto1 = new System.Windows.Forms.TextBox();
            this.txtCodProduto2 = new System.Windows.Forms.TextBox();
            this.cbxTipoPedido = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbxListaMesas = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.lblNomeCliente = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.grpBoxTamanhos = new System.Windows.Forms.GroupBox();
            this.rb12 = new System.Windows.Forms.RadioButton();
            this.rb11 = new System.Windows.Forms.RadioButton();
            this.rb10 = new System.Windows.Forms.RadioButton();
            this.rb9 = new System.Windows.Forms.RadioButton();
            this.rb8 = new System.Windows.Forms.RadioButton();
            this.rb7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.chkListAdicionais = new System.Windows.Forms.CheckedListBox();
            this.grpVendedor = new System.Windows.Forms.GroupBox();
            this.cbxVendedor = new System.Windows.Forms.ComboBox();
            this.txtCodVendedor = new System.Windows.Forms.TextBox();
            this.chkSabores = new System.Windows.Forms.CheckBox();
            this.cbxHorarioEntrega = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPorcentagemDesconto = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtObsPedido = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chkCodPersonalizado = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lblSenha = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.itemsPedidoBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemsPedido)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.grpBoxTamanhos.SuspendLayout();
            this.grpVendedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemsPedidoBindingSource
            // 
            this.itemsPedidoBindingSource.DataMember = "ItemsPedido";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(12, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Cadastrar Pedido";
            // 
            // cbxTipoProduto
            // 
            this.cbxTipoProduto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxTipoProduto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoProduto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxTipoProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipoProduto.FormattingEnabled = true;
            this.cbxTipoProduto.Location = new System.Drawing.Point(54, 89);
            this.cbxTipoProduto.Name = "cbxTipoProduto";
            this.cbxTipoProduto.Size = new System.Drawing.Size(232, 26);
            this.cbxTipoProduto.TabIndex = 0;
            this.cbxTipoProduto.SelectedIndexChanged += new System.EventHandler(this.cbxTipoProduto_SelectedIndexChanged);
            // 
            // lblGrupo
            // 
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.Location = new System.Drawing.Point(9, 95);
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(39, 13);
            this.lblGrupo.TabIndex = 32;
            this.lblGrupo.Text = "Grupo:";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidade.Location = new System.Drawing.Point(55, 152);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(96, 26);
            this.txtQuantidade.TabIndex = 2;
            this.txtQuantidade.Text = "1";
            this.txtQuantidade.TextChanged += new System.EventHandler(this.txtQuantidade_TextChanged);
            this.txtQuantidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantidade_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Qtd.:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Preço Unitário:";
            // 
            // txtPrecoUnitario
            // 
            this.txtPrecoUnitario.Enabled = false;
            this.txtPrecoUnitario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoUnitario.Location = new System.Drawing.Point(240, 152);
            this.txtPrecoUnitario.Name = "txtPrecoUnitario";
            this.txtPrecoUnitario.Size = new System.Drawing.Size(72, 26);
            this.txtPrecoUnitario.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(477, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Preço Total:";
            // 
            // txtPrecoTotal
            // 
            this.txtPrecoTotal.Enabled = false;
            this.txtPrecoTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoTotal.Location = new System.Drawing.Point(548, 151);
            this.txtPrecoTotal.Name = "txtPrecoTotal";
            this.txtPrecoTotal.Size = new System.Drawing.Size(104, 26);
            this.txtPrecoTotal.TabIndex = 5;
            // 
            // btnAdicionarItemNoPedido
            // 
            this.btnAdicionarItemNoPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAdicionarItemNoPedido.Location = new System.Drawing.Point(546, 184);
            this.btnAdicionarItemNoPedido.Name = "btnAdicionarItemNoPedido";
            this.btnAdicionarItemNoPedido.Size = new System.Drawing.Size(106, 43);
            this.btnAdicionarItemNoPedido.TabIndex = 4;
            this.btnAdicionarItemNoPedido.Text = "Adicionar";
            this.btnAdicionarItemNoPedido.UseVisualStyleBackColor = true;
            this.btnAdicionarItemNoPedido.Click += new System.EventHandler(this.btnAdicionarItemNoPedido_Click);
            // 
            // btnCancelarPedido
            // 
            this.btnCancelarPedido.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCancelarPedido.FlatAppearance.BorderSize = 5;
            this.btnCancelarPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelarPedido.Location = new System.Drawing.Point(409, 44);
            this.btnCancelarPedido.Name = "btnCancelarPedido";
            this.btnCancelarPedido.Size = new System.Drawing.Size(104, 37);
            this.btnCancelarPedido.TabIndex = 9;
            this.btnCancelarPedido.Text = "Canc. [ESC]";
            this.btnCancelarPedido.UseVisualStyleBackColor = true;
            this.btnCancelarPedido.Click += new System.EventHandler(this.btnCancelarPedido_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.lblDébito);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnAtlCadastro);
            this.panel1.Controls.Add(this.lblTempo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1051, 43);
            this.panel1.TabIndex = 41;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(744, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 20);
            this.label15.TabIndex = 69;
            this.label15.Text = "Tempo Ab.";
            // 
            // lblDébito
            // 
            this.lblDébito.AutoSize = true;
            this.lblDébito.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDébito.ForeColor = System.Drawing.Color.Red;
            this.lblDébito.Location = new System.Drawing.Point(344, 11);
            this.lblDébito.Name = "lblDébito";
            this.lblDébito.Size = new System.Drawing.Size(0, 20);
            this.lblDébito.TabIndex = 68;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Location = new System.Drawing.Point(558, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 31);
            this.button1.TabIndex = 67;
            this.button1.Text = "Trocar Endereço";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAtlCadastro
            // 
            this.btnAtlCadastro.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAtlCadastro.Location = new System.Drawing.Point(636, 4);
            this.btnAtlCadastro.Name = "btnAtlCadastro";
            this.btnAtlCadastro.Size = new System.Drawing.Size(72, 31);
            this.btnAtlCadastro.TabIndex = 66;
            this.btnAtlCadastro.Text = "Atualizar Cliente";
            this.btnAtlCadastro.UseVisualStyleBackColor = true;
            this.btnAtlCadastro.Click += new System.EventHandler(this.AtualizarCadastro);
            // 
            // lblTempo
            // 
            this.lblTempo.AutoSize = true;
            this.lblTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTempo.Location = new System.Drawing.Point(843, 11);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(79, 20);
            this.lblTempo.TabIndex = 11;
            this.lblTempo.Text = "00:00:00";
            this.toolTip1.SetToolTip(this.lblTempo, "Tempo que o pedido esta aberto/gerado");
            // 
            // lbTotal
            // 
            this.lbTotal.AutoSize = true;
            this.lbTotal.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lbTotal.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lbTotal.Location = new System.Drawing.Point(322, 8);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(71, 31);
            this.lbTotal.TabIndex = 11;
            this.lbTotal.Text = "0,00";
            // 
            // lbTotalPedido
            // 
            this.lbTotalPedido.AutoSize = true;
            this.lbTotalPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbTotalPedido.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbTotalPedido.Location = new System.Drawing.Point(271, 15);
            this.lbTotalPedido.Name = "lbTotalPedido";
            this.lbTotalPedido.Size = new System.Drawing.Size(54, 20);
            this.lbTotalPedido.TabIndex = 10;
            this.lbTotalPedido.Text = "Total:";
            // 
            // btnGerarPedido
            // 
            this.btnGerarPedido.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnGerarPedido.FlatAppearance.BorderSize = 5;
            this.btnGerarPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnGerarPedido.Location = new System.Drawing.Point(152, 42);
            this.btnGerarPedido.Name = "btnGerarPedido";
            this.btnGerarPedido.Size = new System.Drawing.Size(126, 38);
            this.btnGerarPedido.TabIndex = 8;
            this.btnGerarPedido.Text = "Gerar [F12]";
            this.btnGerarPedido.UseVisualStyleBackColor = true;
            this.btnGerarPedido.Click += new System.EventHandler(this.btnGerarPedido_Click);
            // 
            // txbProduto
            // 
            this.txbProduto.AutoSize = true;
            this.txbProduto.Location = new System.Drawing.Point(9, 128);
            this.txbProduto.Name = "txbProduto";
            this.txbProduto.Size = new System.Drawing.Size(29, 13);
            this.txbProduto.TabIndex = 44;
            this.txbProduto.Text = "Prod";
            // 
            // cbxProdutosGrid
            // 
            this.cbxProdutosGrid.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxProdutosGrid.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxProdutosGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxProdutosGrid.FormattingEnabled = true;
            this.cbxProdutosGrid.Location = new System.Drawing.Point(54, 121);
            this.cbxProdutosGrid.Name = "cbxProdutosGrid";
            this.cbxProdutosGrid.Size = new System.Drawing.Size(232, 26);
            this.cbxProdutosGrid.TabIndex = 1;
            this.cbxProdutosGrid.SelectedIndexChanged += new System.EventHandler(this.cbxProdutosGrid_SelectedIndexChanged);
            // 
            // gridViewItemsPedido
            // 
            this.gridViewItemsPedido.AllowUserToAddRows = false;
            this.gridViewItemsPedido.AllowUserToDeleteRows = false;
            this.gridViewItemsPedido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridViewItemsPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewItemsPedido.Location = new System.Drawing.Point(12, 230);
            this.gridViewItemsPedido.Name = "gridViewItemsPedido";
            this.gridViewItemsPedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridViewItemsPedido.Size = new System.Drawing.Size(640, 142);
            this.gridViewItemsPedido.TabIndex = 47;
            this.gridViewItemsPedido.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewItemsPedido_CellClick);
            this.gridViewItemsPedido.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.EditarItem);
            this.gridViewItemsPedido.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridViewItemsPedido_MouseClick);
            // 
            // lblTrocoPara
            // 
            this.lblTrocoPara.AutoSize = true;
            this.lblTrocoPara.Location = new System.Drawing.Point(3, 32);
            this.lblTrocoPara.Name = "lblTrocoPara";
            this.lblTrocoPara.Size = new System.Drawing.Size(53, 13);
            this.lblTrocoPara.TabIndex = 48;
            this.lblTrocoPara.Text = "Troco P/:";
            // 
            // txtTrocoPara
            // 
            this.txtTrocoPara.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtTrocoPara.Location = new System.Drawing.Point(62, 24);
            this.txtTrocoPara.Name = "txtTrocoPara";
            this.txtTrocoPara.Size = new System.Drawing.Size(57, 26);
            this.txtTrocoPara.TabIndex = 6;
            this.txtTrocoPara.Text = "0,00";
            this.txtTrocoPara.TextChanged += new System.EventHandler(this.txtTrocoPara_TextChanged);
            this.txtTrocoPara.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTrocoPara_KeyPress);
            // 
            // lblFormaDePagamento
            // 
            this.lblFormaDePagamento.AutoSize = true;
            this.lblFormaDePagamento.Location = new System.Drawing.Point(300, 0);
            this.lblFormaDePagamento.Name = "lblFormaDePagamento";
            this.lblFormaDePagamento.Size = new System.Drawing.Size(111, 13);
            this.lblFormaDePagamento.TabIndex = 50;
            this.lblFormaDePagamento.Text = "Forma de Pagamento:";
            // 
            // txtItemDescricao
            // 
            this.txtItemDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtItemDescricao.Location = new System.Drawing.Point(54, 184);
            this.txtItemDescricao.Multiline = true;
            this.txtItemDescricao.Name = "txtItemDescricao";
            this.txtItemDescricao.Size = new System.Drawing.Size(480, 43);
            this.txtItemDescricao.TabIndex = 3;
            // 
            // lblDescricaoDoItem
            // 
            this.lblDescricaoDoItem.AutoSize = true;
            this.lblDescricaoDoItem.Location = new System.Drawing.Point(9, 192);
            this.lblDescricaoDoItem.Name = "lblDescricaoDoItem";
            this.lblDescricaoDoItem.Size = new System.Drawing.Size(29, 13);
            this.lblDescricaoDoItem.TabIndex = 53;
            this.lblDescricaoDoItem.Text = "Obs:";
            // 
            // btnReimprimir
            // 
            this.btnReimprimir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnReimprimir.FlatAppearance.BorderSize = 5;
            this.btnReimprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnReimprimir.Location = new System.Drawing.Point(284, 43);
            this.btnReimprimir.Name = "btnReimprimir";
            this.btnReimprimir.Size = new System.Drawing.Size(121, 37);
            this.btnReimprimir.TabIndex = 54;
            this.btnReimprimir.Text = "Imprimir [F11]";
            this.btnReimprimir.UseVisualStyleBackColor = true;
            this.btnReimprimir.Visible = false;
            this.btnReimprimir.Click += new System.EventHandler(this.btnReimprimir_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel2.Controls.Add(this.dtPedido);
            this.panel2.Controls.Add(this.lblStatusPedido);
            this.panel2.Controls.Add(this.btnMultiploPagamento);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.btnCalGarcon);
            this.panel2.Controls.Add(this.lblEntrega);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.lblTroco);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.lbTotal);
            this.panel2.Controls.Add(this.btnCancelarPedido);
            this.panel2.Controls.Add(this.lbTotalPedido);
            this.panel2.Controls.Add(this.btnReimprimir);
            this.panel2.Controls.Add(this.btnGerarPedido);
            this.panel2.Location = new System.Drawing.Point(12, 498);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(647, 116);
            this.panel2.TabIndex = 42;
            // 
            // dtPedido
            // 
            this.dtPedido.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPedido.Location = new System.Drawing.Point(174, 90);
            this.dtPedido.Name = "dtPedido";
            this.dtPedido.Size = new System.Drawing.Size(95, 20);
            this.dtPedido.TabIndex = 67;
            this.toolTip1.SetToolTip(this.dtPedido, "Data do pedido \"Usado para lançar pedido com data futura\"");
            // 
            // lblStatusPedido
            // 
            this.lblStatusPedido.AutoSize = true;
            this.lblStatusPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusPedido.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblStatusPedido.Location = new System.Drawing.Point(7, 90);
            this.lblStatusPedido.Name = "lblStatusPedido";
            this.lblStatusPedido.Size = new System.Drawing.Size(169, 29);
            this.lblStatusPedido.TabIndex = 66;
            this.lblStatusPedido.Text = "StatusPedido";
            // 
            // btnMultiploPagamento
            // 
            this.btnMultiploPagamento.Enabled = false;
            this.btnMultiploPagamento.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMultiploPagamento.FlatAppearance.BorderSize = 5;
            this.btnMultiploPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnMultiploPagamento.Location = new System.Drawing.Point(313, 82);
            this.btnMultiploPagamento.Name = "btnMultiploPagamento";
            this.btnMultiploPagamento.Size = new System.Drawing.Size(178, 31);
            this.btnMultiploPagamento.TabIndex = 61;
            this.btnMultiploPagamento.Text = "Multiplos Pagamentos";
            this.toolTip1.SetToolTip(this.btnMultiploPagamento, "Caso o pedido tenha multilpas formas de pagamento use esse fechamento");
            this.btnMultiploPagamento.UseVisualStyleBackColor = true;
            this.btnMultiploPagamento.Click += new System.EventHandler(this.MultiplaFormasPagamento);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label10.Location = new System.Drawing.Point(3, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 16);
            this.label10.TabIndex = 60;
            this.label10.Text = "[F5] Gerar S/Troco";
            this.label10.Visible = false;
            // 
            // btnCalGarcon
            // 
            this.btnCalGarcon.Enabled = false;
            this.btnCalGarcon.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCalGarcon.FlatAppearance.BorderSize = 5;
            this.btnCalGarcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalGarcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnCalGarcon.Location = new System.Drawing.Point(519, 44);
            this.btnCalGarcon.Name = "btnCalGarcon";
            this.btnCalGarcon.Size = new System.Drawing.Size(117, 37);
            this.btnCalGarcon.TabIndex = 59;
            this.btnCalGarcon.Text = "Calcula 10%";
            this.btnCalGarcon.UseVisualStyleBackColor = true;
            this.btnCalGarcon.Click += new System.EventHandler(this.CalculaGarcon);
            // 
            // lblEntrega
            // 
            this.lblEntrega.AutoSize = true;
            this.lblEntrega.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblEntrega.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblEntrega.Location = new System.Drawing.Point(137, 8);
            this.lblEntrega.Name = "lblEntrega";
            this.lblEntrega.Size = new System.Drawing.Size(71, 31);
            this.lblEntrega.TabIndex = 58;
            this.lblEntrega.Text = "0,00";
            this.lblEntrega.TextChanged += new System.EventHandler(this.lblEntrega_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(12, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 20);
            this.label8.TabIndex = 57;
            this.label8.Text = "Taxa Entrega";
            // 
            // lblTroco
            // 
            this.lblTroco.AutoSize = true;
            this.lblTroco.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTroco.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblTroco.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblTroco.Location = new System.Drawing.Point(519, 8);
            this.lblTroco.Name = "lblTroco";
            this.lblTroco.Size = new System.Drawing.Size(71, 31);
            this.lblTroco.TabIndex = 56;
            this.lblTroco.Text = "0,00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(464, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 20);
            this.label5.TabIndex = 55;
            this.label5.Text = "Troco:";
            // 
            // cbxMeiaPizza
            // 
            this.cbxMeiaPizza.AutoSize = true;
            this.cbxMeiaPizza.Location = new System.Drawing.Point(547, 124);
            this.cbxMeiaPizza.Name = "cbxMeiaPizza";
            this.cbxMeiaPizza.Size = new System.Drawing.Size(52, 17);
            this.cbxMeiaPizza.TabIndex = 54;
            this.cbxMeiaPizza.Text = "2º Sb";
            this.cbxMeiaPizza.UseVisualStyleBackColor = true;
            this.cbxMeiaPizza.CheckedChanged += new System.EventHandler(this.cbxMeiaPizza_CheckedChanged);
            this.cbxMeiaPizza.CheckStateChanged += new System.EventHandler(this.MeiaPizzaMarcado);
            // 
            // cbxSabor
            // 
            this.cbxSabor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxSabor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxSabor.Enabled = false;
            this.cbxSabor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.cbxSabor.FormattingEnabled = true;
            this.cbxSabor.Location = new System.Drawing.Point(315, 121);
            this.cbxSabor.Name = "cbxSabor";
            this.cbxSabor.Size = new System.Drawing.Size(225, 26);
            this.cbxSabor.TabIndex = 55;
            this.cbxSabor.SelectedIndexChanged += new System.EventHandler(this.cbxSabor_SelectedIndexChanged);
            this.cbxSabor.SelectionChangeCommitted += new System.EventHandler(this.cbxSabor_SelectionChangeCommitted);
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAtualizar.Location = new System.Drawing.Point(524, 18);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(61, 31);
            this.btnAtualizar.TabIndex = 56;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Cornsilk;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.txtDesconto);
            this.panel3.Controls.Add(this.cmbFPagamento);
            this.panel3.Controls.Add(this.cbxTrocoParaOK);
            this.panel3.Controls.Add(this.btnAtualizar);
            this.panel3.Controls.Add(this.lblTrocoPara);
            this.panel3.Controls.Add(this.txtTrocoPara);
            this.panel3.Controls.Add(this.lblFormaDePagamento);
            this.panel3.Location = new System.Drawing.Point(12, 378);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(640, 55);
            this.panel3.TabIndex = 57;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(175, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "Desconto R$";
            // 
            // txtDesconto
            // 
            this.txtDesconto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtDesconto.Location = new System.Drawing.Point(178, 23);
            this.txtDesconto.Name = "txtDesconto";
            this.txtDesconto.Size = new System.Drawing.Size(73, 26);
            this.txtDesconto.TabIndex = 57;
            this.txtDesconto.Text = "0,00";
            this.txtDesconto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CalculaDesconto);
            this.txtDesconto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDesconto_KeyUp);
            // 
            // cmbFPagamento
            // 
            this.cmbFPagamento.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbFPagamento.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFPagamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.cmbFPagamento.FormattingEnabled = true;
            this.cmbFPagamento.Location = new System.Drawing.Point(303, 23);
            this.cmbFPagamento.Name = "cmbFPagamento";
            this.cmbFPagamento.Size = new System.Drawing.Size(208, 26);
            this.cmbFPagamento.TabIndex = 99;
            this.cmbFPagamento.DropDown += new System.EventHandler(this.ListaFormasPagamento);
            this.cmbFPagamento.SelectionChangeCommitted += new System.EventHandler(this.cmbFPagamento_SelectionChangeCommitted);
            // 
            // cbxTrocoParaOK
            // 
            this.cbxTrocoParaOK.AutoSize = true;
            this.cbxTrocoParaOK.Location = new System.Drawing.Point(7, 5);
            this.cbxTrocoParaOK.Name = "cbxTrocoParaOK";
            this.cbxTrocoParaOK.Size = new System.Drawing.Size(65, 17);
            this.cbxTrocoParaOK.TabIndex = 7;
            this.cbxTrocoParaOK.Text = "S/ troco";
            this.cbxTrocoParaOK.UseVisualStyleBackColor = true;
            this.cbxTrocoParaOK.CheckedChanged += new System.EventHandler(this.cbxTrocoParaOK_CheckedChanged);
            // 
            // btnReceita
            // 
            this.btnReceita.Location = new System.Drawing.Point(292, 123);
            this.btnReceita.Name = "btnReceita";
            this.btnReceita.Size = new System.Drawing.Size(17, 23);
            this.btnReceita.TabIndex = 58;
            this.btnReceita.Text = "Receita";
            this.btnReceita.UseVisualStyleBackColor = true;
            this.btnReceita.Click += new System.EventHandler(this.ExibirReceita);
            // 
            // txtCodProduto1
            // 
            this.txtCodProduto1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodProduto1.Location = new System.Drawing.Point(54, 89);
            this.txtCodProduto1.Name = "txtCodProduto1";
            this.txtCodProduto1.Size = new System.Drawing.Size(71, 26);
            this.txtCodProduto1.TabIndex = 0;
            this.txtCodProduto1.Visible = false;
            this.txtCodProduto1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BuscaProduto1);
            this.txtCodProduto1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodProduto1_KeyPress);
            // 
            // txtCodProduto2
            // 
            this.txtCodProduto2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodProduto2.Location = new System.Drawing.Point(315, 88);
            this.txtCodProduto2.Name = "txtCodProduto2";
            this.txtCodProduto2.Size = new System.Drawing.Size(71, 26);
            this.txtCodProduto2.TabIndex = 60;
            this.txtCodProduto2.Visible = false;
            this.txtCodProduto2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BuscaProduto2);
            // 
            // cbxTipoPedido
            // 
            this.cbxTipoPedido.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxTipoPedido.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipoPedido.FormattingEnabled = true;
            this.cbxTipoPedido.Items.AddRange(new object[] {
            "0 - Entrega",
            "1 - Mesa",
            "2 - Balcao"});
            this.cbxTipoPedido.Location = new System.Drawing.Point(409, 88);
            this.cbxTipoPedido.Name = "cbxTipoPedido";
            this.cbxTipoPedido.Size = new System.Drawing.Size(132, 26);
            this.cbxTipoPedido.TabIndex = 61;
            this.cbxTipoPedido.Text = "0 - Entrega";
            this.cbxTipoPedido.SelectedIndexChanged += new System.EventHandler(this.cbxTipoPedido_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbxListaMesas
            // 
            this.cbxListaMesas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxListaMesas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxListaMesas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxListaMesas.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxListaMesas.FormattingEnabled = true;
            this.cbxListaMesas.Location = new System.Drawing.Point(546, 88);
            this.cbxListaMesas.Name = "cbxListaMesas";
            this.cbxListaMesas.Size = new System.Drawing.Size(106, 26);
            this.cbxListaMesas.TabIndex = 62;
            this.cbxListaMesas.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.lblEndereco);
            this.panel4.Controls.Add(this.lblNomeCliente);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 43);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1051, 43);
            this.panel4.TabIndex = 63;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(741, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 16);
            this.label6.TabIndex = 66;
            this.label6.Text = "Opções do Produto:";
            // 
            // lblEndereco
            // 
            this.lblEndereco.AutoSize = true;
            this.lblEndereco.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndereco.ForeColor = System.Drawing.Color.Blue;
            this.lblEndereco.Location = new System.Drawing.Point(74, 19);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Size = new System.Drawing.Size(13, 18);
            this.lblEndereco.TabIndex = 11;
            this.lblEndereco.Text = ".";
            // 
            // lblNomeCliente
            // 
            this.lblNomeCliente.AutoSize = true;
            this.lblNomeCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeCliente.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblNomeCliente.Location = new System.Drawing.Point(4, 3);
            this.lblNomeCliente.Name = "lblNomeCliente";
            this.lblNomeCliente.Size = new System.Drawing.Size(12, 16);
            this.lblNomeCliente.TabIndex = 10;
            this.lblNomeCliente.Text = ".";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.grpBoxTamanhos);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.chkListAdicionais);
            this.panel5.Location = new System.Drawing.Point(671, 121);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(368, 490);
            this.panel5.TabIndex = 64;
            // 
            // grpBoxTamanhos
            // 
            this.grpBoxTamanhos.Controls.Add(this.rb12);
            this.grpBoxTamanhos.Controls.Add(this.rb11);
            this.grpBoxTamanhos.Controls.Add(this.rb10);
            this.grpBoxTamanhos.Controls.Add(this.rb9);
            this.grpBoxTamanhos.Controls.Add(this.rb8);
            this.grpBoxTamanhos.Controls.Add(this.rb7);
            this.grpBoxTamanhos.Controls.Add(this.radioButton6);
            this.grpBoxTamanhos.Controls.Add(this.radioButton5);
            this.grpBoxTamanhos.Controls.Add(this.radioButton4);
            this.grpBoxTamanhos.Controls.Add(this.radioButton3);
            this.grpBoxTamanhos.Controls.Add(this.radioButton2);
            this.grpBoxTamanhos.Controls.Add(this.radioButton1);
            this.grpBoxTamanhos.Enabled = false;
            this.grpBoxTamanhos.Location = new System.Drawing.Point(6, 3);
            this.grpBoxTamanhos.Name = "grpBoxTamanhos";
            this.grpBoxTamanhos.Size = new System.Drawing.Size(350, 177);
            this.grpBoxTamanhos.TabIndex = 34;
            this.grpBoxTamanhos.TabStop = false;
            this.grpBoxTamanhos.Text = "Tamanhos";
            // 
            // rb12
            // 
            this.rb12.AutoSize = true;
            this.rb12.Location = new System.Drawing.Point(235, 137);
            this.rb12.Name = "rb12";
            this.rb12.Size = new System.Drawing.Size(46, 17);
            this.rb12.TabIndex = 83;
            this.rb12.TabStop = true;
            this.rb12.Text = "rb12";
            this.rb12.UseVisualStyleBackColor = true;
            this.rb12.Visible = false;
            this.rb12.Click += new System.EventHandler(this.rb12_Click);
            // 
            // rb11
            // 
            this.rb11.AutoSize = true;
            this.rb11.Location = new System.Drawing.Point(130, 140);
            this.rb11.Name = "rb11";
            this.rb11.Size = new System.Drawing.Size(46, 17);
            this.rb11.TabIndex = 82;
            this.rb11.TabStop = true;
            this.rb11.Text = "rb11";
            this.rb11.UseVisualStyleBackColor = true;
            this.rb11.Visible = false;
            this.rb11.Click += new System.EventHandler(this.rb11_Click);
            // 
            // rb10
            // 
            this.rb10.AutoSize = true;
            this.rb10.Location = new System.Drawing.Point(6, 140);
            this.rb10.Name = "rb10";
            this.rb10.Size = new System.Drawing.Size(46, 17);
            this.rb10.TabIndex = 81;
            this.rb10.TabStop = true;
            this.rb10.Text = "rb10";
            this.rb10.UseVisualStyleBackColor = true;
            this.rb10.Visible = false;
            this.rb10.Click += new System.EventHandler(this.rb10_Click);
            // 
            // rb9
            // 
            this.rb9.AutoSize = true;
            this.rb9.Location = new System.Drawing.Point(235, 102);
            this.rb9.Name = "rb9";
            this.rb9.Size = new System.Drawing.Size(40, 17);
            this.rb9.TabIndex = 80;
            this.rb9.TabStop = true;
            this.rb9.Text = "rb6";
            this.rb9.UseVisualStyleBackColor = true;
            this.rb9.Visible = false;
            this.rb9.Click += new System.EventHandler(this.rb9_Click);
            // 
            // rb8
            // 
            this.rb8.AutoSize = true;
            this.rb8.Location = new System.Drawing.Point(235, 60);
            this.rb8.Name = "rb8";
            this.rb8.Size = new System.Drawing.Size(40, 17);
            this.rb8.TabIndex = 79;
            this.rb8.TabStop = true;
            this.rb8.Text = "rb5";
            this.rb8.UseVisualStyleBackColor = true;
            this.rb8.Visible = false;
            this.rb8.Click += new System.EventHandler(this.rb8_Click);
            // 
            // rb7
            // 
            this.rb7.AutoSize = true;
            this.rb7.Location = new System.Drawing.Point(235, 19);
            this.rb7.Name = "rb7";
            this.rb7.Size = new System.Drawing.Size(40, 17);
            this.rb7.TabIndex = 78;
            this.rb7.TabStop = true;
            this.rb7.Text = "rb7";
            this.rb7.UseVisualStyleBackColor = true;
            this.rb7.Visible = false;
            this.rb7.Click += new System.EventHandler(this.rb7_Click);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(130, 102);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(40, 17);
            this.radioButton6.TabIndex = 77;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "rb6";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.Visible = false;
            this.radioButton6.Click += new System.EventHandler(this.radioButton6_Click);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(130, 60);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(40, 17);
            this.radioButton5.TabIndex = 76;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "rb5";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.Visible = false;
            this.radioButton5.Click += new System.EventHandler(this.radioButton5_Click);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(130, 19);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(40, 17);
            this.radioButton4.TabIndex = 75;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "rb4";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.Visible = false;
            this.radioButton4.Click += new System.EventHandler(this.radioButton4_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 102);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(40, 17);
            this.radioButton3.TabIndex = 74;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "rb3";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Visible = false;
            this.radioButton3.Click += new System.EventHandler(this.radioButton3_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 60);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(40, 17);
            this.radioButton2.TabIndex = 73;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "rb2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Visible = false;
            this.radioButton2.Click += new System.EventHandler(this.radioButton2_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(40, 17);
            this.radioButton1.TabIndex = 72;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "rb1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Visible = false;
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 183);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Adicionais";
            // 
            // chkListAdicionais
            // 
            this.chkListAdicionais.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkListAdicionais.CheckOnClick = true;
            this.chkListAdicionais.FormattingEnabled = true;
            this.chkListAdicionais.HorizontalScrollbar = true;
            this.chkListAdicionais.Location = new System.Drawing.Point(3, 199);
            this.chkListAdicionais.Name = "chkListAdicionais";
            this.chkListAdicionais.ScrollAlwaysVisible = true;
            this.chkListAdicionais.Size = new System.Drawing.Size(353, 274);
            this.chkListAdicionais.TabIndex = 0;
            this.chkListAdicionais.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkListAdicionais_ItemCheck);
            this.chkListAdicionais.SelectedValueChanged += new System.EventHandler(this.chkListAdicionais_SelectedValueChanged);
            // 
            // grpVendedor
            // 
            this.grpVendedor.Controls.Add(this.cbxVendedor);
            this.grpVendedor.Controls.Add(this.txtCodVendedor);
            this.grpVendedor.Location = new System.Drawing.Point(12, 448);
            this.grpVendedor.Name = "grpVendedor";
            this.grpVendedor.Size = new System.Drawing.Size(218, 44);
            this.grpVendedor.TabIndex = 69;
            this.grpVendedor.TabStop = false;
            this.grpVendedor.Text = "Vendedor / Atendente";
            // 
            // cbxVendedor
            // 
            this.cbxVendedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxVendedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVendedor.Enabled = false;
            this.cbxVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.cbxVendedor.FormattingEnabled = true;
            this.cbxVendedor.Location = new System.Drawing.Point(59, 12);
            this.cbxVendedor.Name = "cbxVendedor";
            this.cbxVendedor.Size = new System.Drawing.Size(137, 26);
            this.cbxVendedor.TabIndex = 69;
            // 
            // txtCodVendedor
            // 
            this.txtCodVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodVendedor.Location = new System.Drawing.Point(5, 13);
            this.txtCodVendedor.Name = "txtCodVendedor";
            this.txtCodVendedor.Size = new System.Drawing.Size(40, 26);
            this.txtCodVendedor.TabIndex = 68;
            this.txtCodVendedor.TextChanged += new System.EventHandler(this.BuscaVendedor);
            this.txtCodVendedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodVendedor_KeyPress);
            // 
            // chkSabores
            // 
            this.chkSabores.AutoSize = true;
            this.chkSabores.Location = new System.Drawing.Point(601, 124);
            this.chkSabores.Name = "chkSabores";
            this.chkSabores.Size = new System.Drawing.Size(67, 17);
            this.chkSabores.TabIndex = 70;
            this.chkSabores.Text = "3°/4º Sb";
            this.chkSabores.UseVisualStyleBackColor = true;
            this.chkSabores.CheckStateChanged += new System.EventHandler(this.chkSabores_CheckStateChanged);
            // 
            // cbxHorarioEntrega
            // 
            this.cbxHorarioEntrega.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxHorarioEntrega.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxHorarioEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxHorarioEntrega.FormattingEnabled = true;
            this.cbxHorarioEntrega.Location = new System.Drawing.Point(757, 88);
            this.cbxHorarioEntrega.Name = "cbxHorarioEntrega";
            this.cbxHorarioEntrega.Size = new System.Drawing.Size(167, 26);
            this.cbxHorarioEntrega.TabIndex = 71;
            this.cbxHorarioEntrega.DropDown += new System.EventHandler(this.ListaHorarios);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(671, 96);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 72;
            this.label11.Text = "Horário Entrega";
            // 
            // txtPorcentagemDesconto
            // 
            this.txtPorcentagemDesconto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentagemDesconto.Location = new System.Drawing.Point(399, 151);
            this.txtPorcentagemDesconto.Name = "txtPorcentagemDesconto";
            this.txtPorcentagemDesconto.Size = new System.Drawing.Size(72, 26);
            this.txtPorcentagemDesconto.TabIndex = 73;
            this.txtPorcentagemDesconto.Text = "0";
            this.txtPorcentagemDesconto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPorcentagemDesconto_KeyDown);
            this.txtPorcentagemDesconto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorcentagemDesconto_KeyPress);
            this.txtPorcentagemDesconto.Leave += new System.EventHandler(this.txtPorcentagemDesconto_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(322, 159);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 13);
            this.label12.TabIndex = 74;
            this.label12.Text = "%Desconto:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 210);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 75;
            this.label13.Text = "Item";
            // 
            // txtObsPedido
            // 
            this.txtObsPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtObsPedido.Location = new System.Drawing.Point(287, 439);
            this.txtObsPedido.Multiline = true;
            this.txtObsPedido.Name = "txtObsPedido";
            this.txtObsPedido.Size = new System.Drawing.Size(365, 52);
            this.txtObsPedido.TabIndex = 76;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(246, 447);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 39);
            this.label14.TabIndex = 77;
            this.label14.Text = "Obs \r\ndo \r\nPedido";
            // 
            // chkCodPersonalizado
            // 
            this.chkCodPersonalizado.AutoSize = true;
            this.chkCodPersonalizado.Location = new System.Drawing.Point(154, 93);
            this.chkCodPersonalizado.Name = "chkCodPersonalizado";
            this.chkCodPersonalizado.Size = new System.Drawing.Size(127, 17);
            this.chkCodPersonalizado.TabIndex = 78;
            this.chkCodPersonalizado.Text = "Código personalizado";
            this.toolTip1.SetToolTip(this.chkCodPersonalizado, "Busca produto usando código personalizado na loja");
            this.chkCodPersonalizado.UseVisualStyleBackColor = true;
            this.chkCodPersonalizado.Visible = false;
            // 
            // txtSenha
            // 
            this.txtSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha.Location = new System.Drawing.Point(601, 88);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(58, 26);
            this.txtSenha.TabIndex = 79;
            this.txtSenha.Visible = false;
            this.txtSenha.Leave += new System.EventHandler(this.txtSenha_Leave);
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenha.Location = new System.Drawing.Point(547, 92);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(52, 15);
            this.lblSenha.TabIndex = 80;
            this.lblSenha.Text = "Senha:";
            this.lblSenha.Visible = false;
            // 
            // frmCadastrarPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1051, 626);
            this.Controls.Add(this.lblSenha);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.chkCodPersonalizado);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtObsPedido);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtPorcentagemDesconto);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbxHorarioEntrega);
            this.Controls.Add(this.chkSabores);
            this.Controls.Add(this.grpVendedor);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.cbxListaMesas);
            this.Controls.Add(this.cbxTipoPedido);
            this.Controls.Add(this.txtCodProduto2);
            this.Controls.Add(this.txtCodProduto1);
            this.Controls.Add(this.btnReceita);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.cbxSabor);
            this.Controls.Add(this.cbxMeiaPizza);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblDescricaoDoItem);
            this.Controls.Add(this.txtItemDescricao);
            this.Controls.Add(this.gridViewItemsPedido);
            this.Controls.Add(this.cbxProdutosGrid);
            this.Controls.Add(this.txbProduto);
            this.Controls.Add(this.btnAdicionarItemNoPedido);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPrecoTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPrecoUnitario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtQuantidade);
            this.Controls.Add(this.lblGrupo);
            this.Controls.Add(this.cbxTipoProduto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmCadastrarPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[XDelivery ] Cadastrar Pedido";
            this.Load += new System.EventHandler(this.frmCadastrarPedido_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCadastrarPedido_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.itemsPedidoBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemsPedido)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.grpBoxTamanhos.ResumeLayout(false);
            this.grpBoxTamanhos.PerformLayout();
            this.grpVendedor.ResumeLayout(false);
            this.grpVendedor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void txtQuantidade_TextChanged(object sender, EventArgs e)
        {
            if (txtQuantidade.Text != "" && txtQuantidade.Text != "0," && txtQuantidade.Text != "0.")
            {
                CalcularTotalItem();
            }


        }

        private void CalcularTotalItem()
        {
            if (txtQuantidade.Text != "" && txtPrecoUnitario.Text != "" || cbxProdutosGrid.SelectedItem != null)
            {
                var precoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.ToString().Replace("R$ ", ""));
                var quantidade = decimal.Parse(this.txtQuantidade.Text);
                var total = Math.Round(precoUnitario * quantidade, 2);
                this.txtPrecoTotal.Text = total.ToString();
                CalculaPorcentagemDesconto();
            }
        }
        //private void CalculaTempPedido()
        //{
        //    Timer relogio = new Timer();
        //    relogio.Interval = 1000;
        //    int tempo = DataPed.Hour;
        //    relogio.Tick += delegate
        //    {
        //        tempo -= DateTime.Now.Hour;
        //        lblTempo.Text = tempo.ToString();
        //        //if (tempo == TempPrevisto)
        //        //{
        //        //    relogio.Stop();
        //        //}
        //    };
        //    relogio.Start();
        //}

        private void cbxTipoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCalGarcon.Enabled = cbxTipoPedido.Text == "1 - Mesa";

            if (cbxTipoPedido.Text == "1 - Mesa")
            {
                cbxListaMesas.Visible = true;
                cbxListaMesas.Focus();
                lblSenha.Visible = false;
                txtSenha.Visible = false;
            }
            else if (cbxTipoPedido.Text == "2 - Balcao")
            {
                lblSenha.Visible = true;
                txtSenha.Visible = true;
                txtSenha.Focus();
            }
            else
            {
                cbxListaMesas.Visible = false;
                lblSenha.Visible = false;
                txtSenha.Visible = false;
            }
            CalculaTaxaEntrega(cbxTipoPedido.Text == "0 - Entrega");

        }
        private void CalculaTaxaEntrega(Boolean iCalcula)
        {
            if (!iCalcula)
            {
                this.lbTotal.Text = "R$ " + Convert.ToString(decimal.Parse(lbTotal.Text.Replace("R$", "")) - decimal.Parse(lblEntrega.Text.Replace("R$", "")));
                lblEntrega.Text = "R$ 0,00 ";
            }
            else
            {
                this.lbTotal.Text = "R$ " + Convert.ToString(SomaItensPedido() + decimal.Parse(lblEntrega.Text.Replace("R$", "")) - decimal.Parse(txtDesconto.Text.Replace("R$", "")));
                lblEntrega.Text = "R$ " + Utils.RetornaTaxaPorCliente(codPessoa, prvCodEndecoSelecionado).ToString();
            }
            AlteraTotalPedido();
        }

        private void AlteraTotalPedido()
        {
            try
            {
                if (!Utils.CaixaAberto(DateTime.Now, Sessions.retunrUsuario.CaixaLogado, Sessions.retunrUsuario.Turno))
                {
                    MessageBox.Show(Bibliotecas.cCaixaFechado);
                    return;
                }
                NovoTotalPedido pedi = new NovoTotalPedido()
                {
                    Codigo = codPedido,
                    NumeroMesa = gNUmeroMesa,
                    TotalPedido = SomaItensPedido() + decimal.Parse(lblEntrega.Text.Replace("R$", "")) - decimal.Parse(txtDesconto.Text.Replace("R$", "")),
                    Tipo = cbxTipoPedido.Text,
                    CodEndereco = prvCodEndecoSelecionado
                };
                if (txtCodVendedor.Text != "")
                {
                    pedi.CodUsuario = int.Parse(txtCodVendedor.Text);
                }
                else
                {
                    pedi.CodUsuario = 0;
                }
                pedi.HorarioEntrega = "";
                pedi.DescontoValor = decimal.Parse(txtDesconto.Text);
                if (cbxHorarioEntrega.Text != "")
                {
                    pedido.HorarioEntrega = cbxHorarioEntrega.Text;
                }
                pedi.Observacao = txtObsPedido.Text;
                con.Update("spAlterarTotalPedido", pedi);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }

        private void cbxTrocoParaOK_CheckedChanged(object sender, EventArgs e)
        {
            txtTrocoPara.Enabled = !cbxTrocoParaOK.Checked;
            txtTrocoPara.Text = "0,00";
        }

        private void cbxSabor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LimpaTamanhosSabores();
            //MontaMenuOpcoes(int.Parse(cbxProdutosGrid.SelectedValue.ToString()), int.Parse(cbxSabor.SelectedValue.ToString()));
            // ComparaValores();
        }

        private void frmCadastrarPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (!btnGerarPedido.Enabled || !btnReimprimir.Enabled)
            {
                MessageBox.Show("Não é possivel executar essa ação para esse pedido");
                return;
            }
            if (e.KeyCode == Keys.F12 || e.KeyCode == Keys.F5 && btnGerarPedido.Text == "Gerar [F12]")
            {
                btnGerarPedido_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F11)
            {
                prepareToPrint();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //   Timer HoraAtual;
            lblTempo.Text = Convert.ToString(DateTime.Now - DataPed).Substring(0, 8);
        }

        private void txtCodProduto1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void txtTrocoPara_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Utils.SoDecimais(e))
            {
                if ((txtTrocoPara.Text != "") && (e.KeyChar == Convert.ToChar(Keys.Enter)))
                {
                    txtTrocoPara.Text = string.Format("{0:#,##0.00}", decimal.Parse(txtTrocoPara.Text));
                    AtualizaTroco();

                }
            }

        }

        private void txtTrocoPara_TextChanged(object sender, EventArgs e)
        {

        }

        private void CalculaDesconto(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(Keys.Enter))
            {
                return;
            }

            decimal TotalPedido = ValorTotal + Convert.ToDecimal(lblEntrega.Text.Replace("R$", ""));
            if (Utils.SoDecimais(e))
            {
                if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "DescontoPedidoSN"))
                {
                    return;
                }
                if (!ValidaMaximoDesconto())
                {
                    MessageBox.Show("Desconto máximo superado");
                    return;
                }
                if ((txtDesconto.Text != ""))
                {
                    if (decimal.Parse(txtDesconto.Text) < TotalPedido)
                    {
                        TotalPedido = TotalPedido - decimal.Parse(txtDesconto.Text);
                        txtDesconto.Text = string.Format("{0:#,##0.00}", decimal.Parse(txtDesconto.Text));
                        lbTotal.Text = "R$" + TotalPedido.ToString();

                        //gridViewItemsPedido.Rows[0].Cells[4].Value = decimal.Parse(gridViewItemsPedido.Rows[0].Cells[4].Value.ToString().Replace("R$", "")) - decimal.Parse(txtDesconto.Text);
                        //gridViewItemsPedido.Rows[0].Cells[5].Value = decimal.Parse(gridViewItemsPedido.Rows[0].Cells[4].Value.ToString().Replace("R$", "")) * decimal.Parse(gridViewItemsPedido.Rows[0].Cells[3].Value.ToString());

                        AtualizaTroco();
                        AtualizaTotalPedido();


                    }
                    else
                    {
                        MessageBox.Show("Desconto não pode ser maior que o total do pedido", "Aviso");
                        return;
                    }

                }
            }
        }

        private void cmbFPagamento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Decimal ValorProduto = 0.00M;
            //// DiaDaPromocao = produto.Rows[0]["DiaSemana"].ToString();
            ////   lol = DiaDaPromocao.Split(new char[] { ';' });
            ////  if (DiaDaPromocao.IndexOf(DiaDaSema) > 0)
            //if (PromocaoDiasSemana)
            //{
            //    DataRow rows;
            //    DataSet DsFP;
            //    int CodFP = int.Parse(cmbFPagamento.SelectedValue.ToString());
            //    DsFP = new DataSet();
            //    DsFP = con.SelectRegistroPorCodigo("FormaPagamento", "spObterFPPorCodigo", CodFP);
            //    rows = DsFP.Tables[0].Rows[0];
            //    FPPermiteDesconto = Convert.ToBoolean(rows.ItemArray.GetValue(2).ToString());

            //    DataSet dsProduto;
            //    DataRow RowsProduto;

            //    Decimal TotalAtualizado = 0;
            //    int iCodProduto = 0;

            //    if (!FPPermiteDesconto)
            //    {

            //        for (int i = 0; i < gridViewItemsPedido.Rows.Count; i++)
            //        {
            //            iCodProduto = int.Parse(gridViewItemsPedido.Rows[i].Cells[0].Value.ToString());
            //            dsProduto = con.SelectRegistroPorCodigo("Produto", "spObterProdutoPorCodigo", iCodProduto, true);
            //            RowsProduto = dsProduto.Tables[0].Rows[0];
            //            if (RowsProduto.ItemArray.GetValue(4).ToString().IndexOf(DiaDaSema) > 0 && FPPermiteDesconto)
            //            {
            //                ValorProduto = decimal.Parse(RowsProduto.ItemArray.GetValue(5).ToString());
            //            }
            //            else
            //            {
            //                ValorProduto = decimal.Parse(RowsProduto.ItemArray.GetValue(1).ToString());
            //            }


            //            gridViewItemsPedido.Rows[i].Cells[3].Value = ValorProduto;
            //            gridViewItemsPedido.Rows[i].Cells[4].Value = decimal.Parse(gridViewItemsPedido.Rows[i].Cells[2].Value.ToString()) * ValorProduto;
            //            decimal TotalLinha = decimal.Parse(gridViewItemsPedido.Rows[i].Cells[4].Value.ToString());

            //            TotalAtualizado += TotalLinha;

            //        }

            //    }
            //    else
            //    {
            //        for (int intFor = 0; intFor < gridViewItemsPedido.Rows.Count; intFor++)
            //        {
            //            iCodProduto = int.Parse(gridViewItemsPedido.Rows[intFor].Cells[0].Value.ToString());
            //            dsProduto = con.SelectRegistroPorCodigo("Produto", "spObterProdutoPorCodigo", iCodProduto, true);

            //            RowsProduto = dsProduto.Tables[0].Rows[0];

            //            if (RowsProduto.ItemArray.GetValue(4).ToString().IndexOf(DiaDaSema) > 0)
            //            {
            //                if (RowsProduto.ItemArray.GetValue(5).ToString() != "0,00")
            //                {
            //                    ValorProduto = decimal.Parse(RowsProduto.ItemArray.GetValue(5).ToString());
            //                }
            //                else
            //                {
            //                    ValorProduto = decimal.Parse(RowsProduto.ItemArray.GetValue(1).ToString());
            //                }

            //            }
            //            else
            //            {

            //                ValorProduto = decimal.Parse(RowsProduto.ItemArray.GetValue(1).ToString());
            //            }
            //            gridViewItemsPedido.Rows[intFor].Cells[3].Value = ValorProduto;
            //            gridViewItemsPedido.Rows[intFor].Cells[4].Value = decimal.Parse(gridViewItemsPedido.Rows[intFor].Cells[2].Value.ToString()) * ValorProduto;
            //            decimal TotalLinha = decimal.Parse(gridViewItemsPedido.Rows[intFor].Cells[4].Value.ToString());
            //            TotalAtualizado += TotalLinha;

            //        }

            //    }

            //    lbTotal.Text = "R$ " + Convert.ToString(TotalAtualizado + Convert.ToDecimal(lblEntrega.Text));
            //}

        }

        private void CalculaGarcon(object sender, EventArgs e)
        {
            CultureInfo culture;
            culture = new CultureInfo("pt-BR");

            double vlrPedido = double.Parse(lbTotal.Text.Replace("R$", ""), culture.NumberFormat);
            if (btnCalGarcon.Text == "Calcula 10%")
            {
                CalculaDezPorCento CalcDezPorcento;
                CalcDezPorcento = new CalculaDezPorCento()
                {
                    Codigo = codPedido,
                    MargemGarcon = vlrPedido * 0.1
                };
                con.Update("spMargemGarcon", CalcDezPorcento);
                decimal TotalPedido = decimal.Parse(lbTotal.Text.Replace("R$", ""));
                lbTotal.Text = Convert.ToString(vlrPedido + vlrPedido * 0.1);
                DMargemGarco = Decimal.Parse(vlrPedido.ToString()) * 0.1M;
                btnCalGarcon.Text = "Remove 10%";
            }
            else
            {
                RemoveDezPorcento removeDez;
                double vlrPedidoOriginal = 0.00;
                for (int i = 0; i < gridViewItemsPedido.Rows.Count; i++)
                {
                    vlrPedidoOriginal = vlrPedidoOriginal + double.Parse(gridViewItemsPedido.Rows[i].Cells[4].Value.ToString().Replace("R$", ""));
                }
                removeDez = new RemoveDezPorcento()
                {
                    Codigo = codPedido,
                    MargemGarcon = 0,
                    TotalPedido = vlrPedidoOriginal
                };
                con.Update("spRemoveDezPorCento", removeDez);
                lbTotal.Text = vlrPedidoOriginal.ToString();
                btnCalGarcon.Text = "Calcula 10%";
            }

        }
        private string ObterSomenteLetras(string istring)
        {
            string iReturn;
            var inicioPalavra = istring.IndexOf('(', istring.IndexOf('(') + 1);
            istring = istring.Substring(inicioPalavra + 1, istring.IndexOf('(', inicioPalavra + 1));
            iReturn = Regex.Replace(istring, "[^a-zA-Z çãõÇÁáéÉÍíÓóÚú 0-9]+", "");
            return iReturn;
        }
        /// <summary>
        /// Retorna o Valor do Item Selecionado , considera apenas oque está entre parentes
        /// </summary>
        /// <param name="iValue">
        /// </param>
        /// <returns></returns>
        private string ObterSomenteNumerosReais(string iValue)
        {
            iValue += ")";
            iValue = iValue.Substring(iValue.IndexOf("(") + 1);
            string ire;
            ire = Regex.Replace(iValue, "[^0-9 ,]+", "");
            return ire;
        }


        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                txtPrecoUnitario.Text = radioButton1.Tag.ToString();
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = iNomeProd + " " + radioButton1.Text;
                }
                else
                {
                    cbxProdutosGrid.Text = iNomeProd + " " + radioButton1.Text;
                }
                CalcularTotalItem();
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                txtPrecoUnitario.Text = radioButton2.Tag.ToString();
                // txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = iNomeProd + " " + radioButton2.Text;
                }
                else
                {
                    cbxProdutosGrid.Text = iNomeProd + " " + radioButton2.Text;
                }

                CalcularTotalItem();
            }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                txtPrecoUnitario.Text = radioButton3.Tag.ToString(); ;
                // txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = iNomeProd + " " + radioButton3.Text;
                }
                else
                {
                    cbxProdutosGrid.Text = iNomeProd + " " + radioButton3.Text;
                }
                // cbxProdutosGrid.Text = iNomeProd + " " + radioButton3.Text;
                CalcularTotalItem();
            }
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                txtPrecoUnitario.Text = radioButton4.Tag.ToString();
                //  txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                //cbxProdutosGrid.Text = iNomeProd + " " + radioButton4.Text;
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = iNomeProd + " " + radioButton4.Text;
                }
                else
                {
                    cbxProdutosGrid.Text = iNomeProd + " " + radioButton4.Text;
                }
                CalcularTotalItem();
            }
        }

        private void radioButton5_Click(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                txtPrecoUnitario.Text = radioButton5.Tag.ToString();
                // txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                // cbxProdutosGrid.Text = iNomeProd + " " + radioButton5.Text;
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = iNomeProd + " " + radioButton5.Text;
                }
                else
                {
                    cbxProdutosGrid.Text = iNomeProd + " " + radioButton5.Text;
                }
                CalcularTotalItem();
            }
        }

        private void radioButton6_Click(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                txtPrecoUnitario.Text = radioButton6.Tag.ToString();
                // txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = iNomeProd + " " + radioButton6.Text;
                }
                else
                {
                    cbxProdutosGrid.Text = iNomeProd + " " + radioButton6.Text;
                }
                //cbxProdutosGrid.Text = iNomeProd + " " + radioButton6.Text;
                CalcularTotalItem();
            }
        }

        //Varre todos controles dentro do groupBox
        private Boolean RadiosMarcos()
        {
            Boolean iRetur = false;
            foreach (RadioButton radiobutons in grpBoxTamanhos.Controls)
            {
                if (object.ReferenceEquals(radiobutons.GetType(), typeof(System.Windows.Forms.RadioButton))
                    && ((System.Windows.Forms.RadioButton)radiobutons).Visible)
                {
                    if (((System.Windows.Forms.RadioButton)radiobutons).Checked)
                    {
                        iRetur = true;
                        break;
                    }
                }
            }

            return iRetur;
        }

        private void chkListAdicionais_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (grpBoxTamanhos.Enabled && !RadiosMarcos())
                {
                    MessageBox.Show("Selecione o " + grpBoxTamanhos.Text + " para continuar");
                    chkListAdicionais.SetSelected(e.Index, false);
                    return;
                }

                if (chkListAdicionais.CheckedItems.Count + 1 > gMaximoOpcaoProduto)
                {
                    MessageBox.Show("O Produto " + cbxProdutosGrid.Text + " só permite " + gMaximoOpcaoProduto + " Adicionais");
                    return;
                }

                //if (e.CurrentValue == CheckState.Unchecked)
                //{
                //    decimal iValorItem = decimal.Parse(txtPrecoUnitario.Text.Replace("R$", ""));
                //    decimal iValorAdicional = decimal.Parse(ObterSomenteNumerosReais(chkListAdicionais.SelectedItem.ToString()));
                //    decimal iValor = iValorItem + iValorAdicional;
                //    txtPrecoUnitario.Text = Convert.ToString(iValor);
                //    CalcularTotalItem();
                //}
                //else
                //{
                //    decimal iValorItem;
                //    decimal iValorAdicional;
                //    iValorItem = decimal.Parse(txtPrecoUnitario.Text.Replace("R$", ""));
                //    iValorAdicional = decimal.Parse(ObterSomenteNumerosReais(chkListAdicionais.SelectedItem.ToString()));
                //    if (txtItemDescricao.Text.Contains(" + " + ObterSomenteLetras(chkListAdicionais.SelectedItem.ToString())))
                //    {
                //        decimal iValor = iValorItem - iValorAdicional;
                //        txtPrecoUnitario.Text = Convert.ToString(iValor);
                //        CalcularTotalItem();
                //    }
                //    txtItemDescricao.Text = txtItemDescricao.Text.Replace(" + " + ObterSomenteLetras(chkListAdicionais.SelectedItem.ToString()), string.Empty);

                //}
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

        }

        private void MultiplaFormasPagamento(object sender, EventArgs e)
        {
            try
            {
                if (!Utils.MessageBoxQuestion("Deseja incluir multiplas formas de pagamento nesse pedido? "))
                {
                    return;
                }
                frmFinalizaDelivery frm = new frmFinalizaDelivery(codPedido);
                frm.ShowDialog();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }

        private void rb7_Click(object sender, EventArgs e)
        {
            if (rb7.Checked)
            {
                txtPrecoUnitario.Text = rb7.Tag.ToString();
                // txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                cbxProdutosGrid.Text = iNomeProd + " " + rb7.Text;
                CalcularTotalItem();
            }
        }

        private void rb8_Click(object sender, EventArgs e)
        {
            if (rb8.Checked)
            {
                txtPrecoUnitario.Text = rb8.Tag.ToString();
                //  txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = iNomeProd + " " + rb8.Text;
                }
                else
                {
                    cbxProdutosGrid.Text = iNomeProd + " " + rb8.Text;
                }
                //  cbxProdutosGrid.Text = iNomeProd + " " + rb8.Text;
                CalcularTotalItem();
            }
        }

        private void rb9_Click(object sender, EventArgs e)
        {

            if (rb9.Checked)
            {
                txtPrecoUnitario.Text = rb9.Tag.ToString();
                //  txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = iNomeProd + " " + rb9.Text;
                }
                else
                {
                    cbxProdutosGrid.Text = iNomeProd + " " + rb9.Text;
                }
                //   cbxProdutosGrid.Text = iNomeProd + " " + rb9.Text;
                CalcularTotalItem();
            }
        }

        private void rb10_Click(object sender, EventArgs e)
        {
            if (rb10.Checked)
            {
                txtPrecoUnitario.Text = rb10.Tag.ToString();
                //  txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = iNomeProd + " " + rb10.Text;
                }
                else
                {
                    cbxProdutosGrid.Text = iNomeProd + " " + rb10.Text;
                }
                //  cbxProdutosGrid.Text = iNomeProd + " " + rb10.Text;
                CalcularTotalItem();
            }
        }

        private void rb11_Click(object sender, EventArgs e)
        {
            if (rb11.Checked)
            {
                txtPrecoUnitario.Text = rb11.Tag.ToString();
                //  txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = iNomeProd + " " + rb11.Text;
                }
                else
                {
                    cbxProdutosGrid.Text = iNomeProd + " " + rb11.Text;
                }
                //cbxProdutosGrid.Text = iNomeProd + " " + rb11.Text;
                CalcularTotalItem();
            }
        }

        private void rb12_Click(object sender, EventArgs e)
        {
            if (rb12.Checked)
            {
                txtPrecoUnitario.Text = rb12.Tag.ToString();
                // txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = iNomeProd + " " + rb12.Text;
                }
                else
                {
                    cbxProdutosGrid.Text = iNomeProd + " " + rb12.Text;
                }
                //  cbxProdutosGrid.Text = iNomeProd + " " + rb12.Text;
                CalcularTotalItem();
            }
        }

        private void AtualizarCadastro(object sender, EventArgs e)
        {
            try
            {
                if (Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "VisualizaDadosClienteSN"))
                {
                    DataSet dsPessoa = con.SelectRegistroPorCodigo("Pessoa", "spObterPessoaPorCodigo", codPessoa);
                    DataRow dRowPessoa = dsPessoa.Tables["Pessoa"].Rows[0];
                    int iCodEnd = int.Parse(dRowPessoa.ItemArray.GetValue(20).ToString());
                    frmCadastroCliente frm = new frmCadastroCliente(int.Parse(dRowPessoa.ItemArray.GetValue(0).ToString()), dRowPessoa.ItemArray.GetValue(1).ToString(), dRowPessoa.ItemArray.GetValue(10).ToString(),
                                                                      dRowPessoa.ItemArray.GetValue(11).ToString(), dRowPessoa.ItemArray.GetValue(2).ToString(), dRowPessoa.ItemArray.GetValue(3).ToString(), dRowPessoa.ItemArray.GetValue(9).ToString()
                                                                      , dRowPessoa.ItemArray.GetValue(4).ToString(), dRowPessoa.ItemArray.GetValue(5).ToString(), dRowPessoa.ItemArray.GetValue(6).ToString(), dRowPessoa.ItemArray.GetValue(7).ToString()
                                                                  , dRowPessoa.ItemArray.GetValue(8).ToString(), int.Parse(dRowPessoa.ItemArray.GetValue(14).ToString()), dRowPessoa.ItemArray.GetValue(15).ToString(), dRowPessoa.ItemArray.GetValue(12).ToString(),
                                                                      dRowPessoa.ItemArray.GetValue(16).ToString(), dRowPessoa.ItemArray.GetValue(19).ToString(), iCodEnd, int.Parse(dRowPessoa.ItemArray.GetValue(21).ToString()));

                    AtualizaClienteTela();
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message + erro.InnerException);
            }

        }

        private void txtDesconto_KeyUp(object sender, KeyEventArgs e)
        {
            //decimal TotalPedido = ValorTotal;
            ////if (Utils.SoDecimais(e.KeyValue.GetTypeCode(KeyPressEventArgs))
            ////{
            //if (decimal.Parse(txtDesconto.Text) < TotalPedido)
            //{
            //    if ((txtDesconto.Text != ""))
            //    {
            //        TotalPedido = TotalPedido + decimal.Parse(lblEntrega.Text);
            //        TotalPedido = TotalPedido - decimal.Parse(txtDesconto.Text);
            //        txtDesconto.Text = string.Format("{0:#,##0.00}", decimal.Parse(txtDesconto.Text));
            //        lbTotal.Text = "R$" + TotalPedido.ToString();
            //        AtualizaTroco();
            //        AtualizaTotalPedido();
            //    }



            //}
            //else
            //{
            //    MessageBox.Show("Desconto não pode ser maior que o total do pedido", "Aviso");
            //    return;
            //}

            //}
        }

        private void EditarItem(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (gridViewItemsPedido.SelectedRows.Count > 0)
                {
                    int codItem = int.Parse(this.gridViewItemsPedido.Rows[rowIndex].Cells["CodProduto"].Value.ToString());
                    // txtPorcentagemDesconto.Text = gridViewItemsPedido.Rows[rowIndex].Cells[""]
                    DataSet dsItemCompleto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", codItem);
                    string itemNome = this.gridViewItemsPedido.Rows[rowIndex].Cells[2].Value.ToString();

                    Utils.MontaCombox(cbxTipoProduto, "Nome", "Codigo", "Grupo", "spObterGrupoPOrCodigo", dsItemCompleto.Tables[0].Rows[0].Field<int>("CodGrupo"));
                    string[] sabores = itemNome.Split('/');
                    List<string> list = new List<string>();

                    foreach (string sabor in sabores)
                    {
                        list.Add(sabor);
                    }

                    if (list.Count > 1)
                    {
                        this.cbxMeiaPizza.Checked = true;
                        this.cbxSabor.Enabled = true;
                        this.cbxProdutosGrid.Text = sabores[0];
                        this.cbxSabor.Text = sabores[1];
                    }
                    else
                    {
                        this.cbxMeiaPizza.Checked = false;
                        this.cbxSabor.Enabled = false;
                        this.cbxSabor.Text = "";
                        this.cbxProdutosGrid.Text = gridViewItemsPedido.Rows[rowIndex].Cells[2].Value.ToString();
                    }


                    MontaMenuOpcoes(codItem);
                    MarcaRadioButon(itemNome);
                    if (codPedido == 0)
                    {
                        iRowSelecionada = rowIndex;
                    }

                    codigoItemParaAlterar = int.Parse(this.gridViewItemsPedido.Rows[rowIndex].Cells["CodProduto"].Value.ToString());
                    txtCodProduto1.Text = codItem.ToString();
                    this.txtPrecoUnitario.Text = this.gridViewItemsPedido.Rows[rowIndex].Cells[4].Value.ToString();

                    this.txtQuantidade.Text = this.gridViewItemsPedido.Rows[rowIndex].Cells[3].Value.ToString();
                    this.txtPrecoTotal.Text = this.gridViewItemsPedido.Rows[rowIndex].Cells[5].Value.ToString();
                    this.txtItemDescricao.Text = this.gridViewItemsPedido.Rows[rowIndex].Cells[6].Value.ToString();
                    txtPorcentagemDesconto.Text = gridViewItemsPedido.Rows[rowIndex].Cells["DescontoPorcetagem"].Value.ToString();
                    MarcaListBoxMarcados(txtItemDescricao.Text);
                    this.btnAdicionarItemNoPedido.Text = "Alterar Item";
                    this.btnAdicionarItemNoPedido.Click += new System.EventHandler(this.AlterarItem);
                    this.btnAdicionarItemNoPedido.Click -= new System.EventHandler(this.btnAdicionarItemNoPedido_Click);

                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel selecionar o Item para edição " + erro.Message);
            }
        }
        private void MarcaRadioButon(string strNomeProduto)
        {
            foreach (var item in grpBoxTamanhos.Controls)
            {
                if (object.ReferenceEquals(item.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    if (strNomeProduto.Contains((((System.Windows.Forms.RadioButton)item).Text)))
                    {
                        (((System.Windows.Forms.RadioButton)item).Checked) = true;
                    }
                }
            }


        }
        /// <summary>
        /// Marca os checkbox de acordo com a lista
        /// </summary>
        /// <param name="strAdicionais">
        /// String de observações</param>
        private void MarcaListBoxMarcados(string strAdicionais)
        {
            try
            {
                string[] list = strAdicionais.Replace("+ ", string.Empty).Trim().Split(Environment.NewLine.ToCharArray());
                for (int i = 0; i < list.Length; i++)
                {
                    for (int intfor = 0; intfor < chkListAdicionais.Items.Count; intfor++)
                    {
                        if (ObterSomenteLetras(chkListAdicionais.Items[intfor].ToString()) == list[i].ToString())
                        {
                            chkListAdicionais.SetItemChecked(intfor, true);
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

        }


        private void gridViewItemsPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }
        private void BuscaVendedor(object sender, EventArgs e)
        {
            if (codPedido != 0 && !PedidoRepetio)
            {
                if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "EditaPedidoSN"))
                {
                    return;
                }
            }
            if (txtCodVendedor.Text != "")
            {
                Utils.MontaCombox(cbxVendedor, "Nome", "Codigo", "Usuario", "spObterUsuarioPorCodigo", int.Parse(txtCodVendedor.Text));
            }
        }

        private void txtCodVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void chkSabores_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkSabores.Checked)
            {
                int CodPai = con.BuscaPaiGrupo(int.Parse(cbxTipoProduto.SelectedValue.ToString()));
                grpBoxTamanhos.Enabled = !chkSabores.Checked;
                frmSabores frm = new frmSabores(cbxTipoProduto.Text, CodPai);
                frm.ShowDialog();
                if (frm.boolConfirmado)
                {
                    cbxProdutosGrid.Text = cbxTipoProduto.Text + " " + frm.strTamanho;
                    txtItemDescricao.Text = frm.strNomeProduto;
                    txtPrecoUnitario.Text = frm.strPreco;
                    CalcularTotalItem();
                }
                else
                {
                    grpBoxTamanhos.Enabled = true;
                }

            }

        }

        private void ListaHorarios(object sender, EventArgs e)
        {
            try
            {
                Utils.MontaCombox(cbxHorarioEntrega, "Horario_Entrega", "Codigo", "Empresa_HorarioEntrega", "spObterEmpresa_HorarioEntrega");
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        private void ListaFormasPagamento(object sender, EventArgs e)
        {
            Utils.MontaCombox(cmbFPagamento, "Descricao", "Codigo", "FormaPagamento", "spObterFormaPagamentoAtivo");
        }

        private void txtPorcentagemDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }

        private void cbxSabor_SelectedIndexChanged(object sender, EventArgs e)
        {


            //  iNomeProd = cbxSabor.Text;

            try
            {
                LimpaTamanhosSabores();
                var produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxSabor.SelectedValue.ToString())).Tables["Produto"];
                this.txtQuantidade.Text = "1";
                var ValorProduto = "";
                intCodProduto2Buscado = produto.Rows[0].Field<int>("Codigo");
                MontaMenuOpcoes(intCodProduto1Buscado, intCodProduto2Buscado);
                //if (PromocaoDiasSemana)
                //{
                List<PrecoDiaProduto> listPreco = new List<PrecoDiaProduto>();
                listPreco = Utils.DeserializaObjeto(produto.Rows[0]["DiaSemana"].ToString());
                if (listPreco != null && listPreco.Count != 0)
                {
                    foreach (var item in listPreco)
                    {
                        if (item.Dia == DiaDaSema)
                        {
                            ValorProduto = item.Preco.ToString();
                            this.txtPrecoUnitario.Text = ValorProduto;
                            iTotalItem = txtPrecoUnitario.Text;
                            if (cbxMeiaPizza.Checked)
                            {
                                iNomeProd = cbxSabor.Text;
                            }
                            else
                            {
                                iNomeProd = cbxProdutosGrid.Text;
                            }

                            return;
                        }
                        else
                        {
                            ValorProduto = produto.Rows[0]["PrecoProduto"].ToString();
                            iTotalItem = txtPrecoUnitario.Text;
                            if (cbxMeiaPizza.Checked)
                            {
                                iNomeProd = cbxSabor.Text;
                            }
                            else
                            {
                                iNomeProd = cbxProdutosGrid.Text;
                            }
                        }
                    }
                }
                else
                {
                    ValorProduto = produto.Rows[0]["PrecoProduto"].ToString();
                    iTotalItem = txtPrecoUnitario.Text;
                    if (cbxMeiaPizza.Checked)
                    {
                        iNomeProd = cbxSabor.Text;
                    }
                    else
                    {
                        iNomeProd = cbxProdutosGrid.Text;
                    }
                }
                //}
                //// DAqui
                //else
                //{
                //    ValorProduto = produto.Rows[0]["PrecoProduto"].ToString();
                //    iTotalItem = txtPrecoUnitario.Text;
                //    if (cbxMeiaPizza.Checked && cbxSabor.Focused)
                //    {
                //        iNomeProd = cbxSabor.Text;
                //    }
                //    else
                //    {
                //        iNomeProd = cbxProdutosGrid.Text;
                //    }
                //}

                this.txtPrecoUnitario.Text = ValorProduto;
                CalcularTotalItem();

                if (this.cbxSabor.Focused)
                {
                    decimal valorProduto = 0;
                    decimal valorSabor = 0;
                    var _produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];
                    var sabor = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxSabor.SelectedValue.ToString())).Tables["Produto"];

                    txtQuantidade.Text = "1";
                    if (PromocaoDiasSemana)
                    {
                        List<PrecoDiaProduto> newlistPreco = new List<PrecoDiaProduto>();
                        newlistPreco = Utils.DeserializaObjeto(produto.Rows[0]["DiaSemana"].ToString());
                        if (listPreco != null)
                        {
                            foreach (var item in listPreco)
                            {
                                if (item.Dia == DiaDaSema)
                                {
                                    txtPrecoUnitario.Text = item.Preco.ToString();
                                    iTotalItem = txtPrecoUnitario.Text;
                                    if (cbxMeiaPizza.Checked)
                                    {
                                        iNomeProd = cbxSabor.Text;
                                    }
                                    else
                                    {
                                        iNomeProd = cbxProdutosGrid.Text;
                                    }
                                    return;
                                }
                                else
                                {
                                    txtPrecoUnitario.Text = produto.Rows[0]["PrecoProduto"].ToString();
                                    iTotalItem = txtPrecoUnitario.Text;
                                    if (cbxMeiaPizza.Checked)
                                    {
                                        iNomeProd = cbxSabor.Text;
                                    }
                                    else
                                    {
                                        iNomeProd = cbxProdutosGrid.Text;
                                    }
                                }
                            }
                        }
                        else
                        {
                            valorProduto = decimal.Parse(_produto.Rows[0]["PrecoProduto"].ToString());
                            valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
                        }

                        if (valorProduto > valorSabor)
                        {
                            this.txtPrecoUnitario.Text = valorProduto.ToString();
                        }
                        else
                        {
                            this.txtPrecoUnitario.Text = valorSabor.ToString();
                        }
                    }
                    else
                    {
                        valorProduto = decimal.Parse(_produto.Rows[0]["PrecoProduto"].ToString());
                        valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());

                        txtPrecoUnitario.Text = Convert.ToString(RetornaMaiorValor(valorProduto, valorSabor));
                        //if (valorProduto > valorSabor)
                        //{
                        //    this.txtPrecoUnitario.Text = valorProduto.ToString();
                        //}
                        //else
                        //{
                        //    this.txtPrecoUnitario.Text = valorSabor.ToString();
                        //}
                    }


                }
                // Pega o Preço unitário selecionado

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }


        private void lblEntrega_TextChanged(object sender, EventArgs e)
        {
            if (cbxTipoPedido.Text != "0 - Entrega")
            {
                lblEntrega.Text = "0,00";
            }
            //CalculaTaxaEntrega(cbxTipoPedido.Text == "0 - Entrega");
        }

        private void txtPorcentagemDesconto_Leave(object sender, EventArgs e)
        {
            CalculaPorcentagemDesconto();
            //Passei aqui
        }

        private void MeiaPizzaMarcado(object sender, EventArgs e)
        {



            try
            {
                LimpaTamanhosSabores();

                if (cbxMeiaPizza.Checked)
                {
                    this.cbxSabor.Enabled = true;
                    if (ProdutosPorCodigo)
                    {
                        this.txtCodProduto2.Visible = true;
                    }
                    else
                    {
                        this.txtCodProduto2.Visible = false;
                        int CodPai = con.BuscaPaiGrupo(int.Parse(cbxTipoProduto.SelectedValue.ToString()));
                        if (CodPai != 0)
                        {
                            this.cbxSabor.DataSource = con.SelectRegistroPorCodigo("Produto", "spObterProdutoPorCodPai", CodPai).Tables[0];
                        }
                        else
                        {
                            this.cbxSabor.DataSource = con.SelectRegistroPorNome("@GrupoProduto", "Produto", "spObterProdutoPorGrupo", this.cbxTipoProduto.Text).Tables["Produto"];
                        }

                        this.cbxSabor.DisplayMember = Convert.ToString("NomeProduto");
                        this.cbxSabor.ValueMember = "Codigo";
                    }

                }
                else
                {
                    this.cbxSabor.Enabled = false;
                    this.cbxSabor.Text = "";
                    this.txtCodProduto2.Visible = false;
                }
                //string grupo = this.cbxTipoProduto.Text;
                //this.cbxSabor.DataSource = con.SelectRegistroPorNome("@GrupoProduto", "Produto", "spObterProdutoPorGrupo", grupo).Tables["Produto"];
                //this.cbxSabor.DisplayMember = "NomeProduto";
                //this.cbxSabor.ValueMember = "Codigo";
                //this.txtQuantidade.Text = "1";
                //SemMeiaPizza();

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }



        }

        private void txtSenha_Leave(object sender, EventArgs e)
        {
            if (con.SelectRegistroPorCodigo("Pedido", "spObterPedidoPorSenha", 0, txtSenha.Text).Tables[0].Rows.Count > 0)
            {
                MessageBox.Show(Bibliotecas.cSenhaEmUso);
            }
        }
        private void chkListAdicionais_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            prvCodEndecoSelecionado = Utils.MaisEnderecos(codPessoa);
            if (prvCodEndecoSelecionado == 0)
            {
                btnAtlCadastro.Focus();
            }
            else
            {
                AtualizaClienteTela(prvCodEndecoSelecionado);
            }
        }

        private void txtPorcentagemDesconto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtPorcentagemDesconto.Text != "")
            {
                CalculaPorcentagemDesconto();
            }
        }
        private void CalculaPorcentagemDesconto()
        {
            if (txtPorcentagemDesconto.Text == "")
            {
                MessageBox.Show("Preencha o campo porcentagem de desconto");
                txtPorcentagemDesconto.Focus();
                return;
            }

            decimal qtdProduto = decimal.Parse(txtQuantidade.Text);
            decimal prUnitario = Convert.ToDecimal(txtPrecoUnitario.Text.Replace("R$ ", ""));
            decimal vlrDesconto = prUnitario * qtdProduto * (decimal.Parse(txtPorcentagemDesconto.Text)) / 100;
            var Calc = prUnitario * qtdProduto - vlrDesconto;
            txtPrecoTotal.Text = Calc.ToString();

            // Validar o Desconto Máximo Por Usuario
            //if (txtDesconto.Text != "0,00" || txtDesconto.Text.Trim() != "")
            //{
            //    if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "DescontoPedidoSN"))
            //    {
            //        return;
            //    }

            //}
            //txtPorcentagemDesconto.Text = "0";
        }
    }
}
