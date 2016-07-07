
using DexComanda.Cadastros;
using DexComanda.Models;
using DexComanda.Models.Operacoes;
using DexComanda.Relatorios.Delivery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DexComanda
{
    public partial class frmCadastrarPedido : Form
    {
        private Conexao con;
        private List<Grupo> grupos;
        private int codPessoa;
        private int codPedido;
        private string trocoPara;
        private string formaPagamento;
        private string TipoPedido;
        private string MesaBalcao;
        public decimal ValorTotal;
        public decimal ValorTroco;
        private List<ItemPedido> items;
        private Pedido pedido;
        private string iTotalItem;
        public int iCodPedido;
        private string iNomeProd;
        private Main parentWindow;
        private frmCadastrarPedido parentWindowPedido;
        private int codigoItemParaAlterar;
        private int rowIndex;
        private Font printFont;
        private Font printFontCozinha;
        private Pessoa pCliente;
        private bool debug = true;//Sessions.returnConfig.ImpViaCozinha;
        private string itemNome, gNUmeroMesa;
        private bool ContraMesas = Sessions.returnConfig.UsaControleMesa;
        private string NomeEmpresa, Telefone, Telefone2;
        private bool ControlaFidelidade = Sessions.returnConfig.ControlaFidelidade;
        private int NumeroPedidos; //= Sessions.returnPessoa.TicketFidelidade; 
        private int PedidosParaFidelidade = Sessions.returnConfig.PedidosParaFidelidade;
        private string DiaDaSema = DateTime.Now.DayOfWeek.ToString();
        private string DiaDaPromocao = null;
        private bool PromocaoDiasSemana = Sessions.returnConfig.DescontoDiaSemana;
        private string[] lol;
        private bool ImprimeViaEntrega = Sessions.returnConfig.ImprimeViaEntrega;
        private bool ImprimeViaCozinha = Sessions.returnConfig.ImpViaCozinha;
        private bool ControlaPrevisao = Sessions.returnConfig.PrevisaoEntregaSN;
        private int TempPrevisto = int.Parse(Sessions.returnConfig.PrevisaoEntrega);
        private DateTime DataPed;
        private string CNPJRETORNO = Sessions.returnEmpresa.CNPJ;
        private string TamanhoFont = Sessions.returnConfig.TamanhoFont;
        private string PortaImpressa = Sessions.returnConfig.PortaLPT;
        private bool ImprimeLPT = Sessions.returnConfig.ImpLPT;
        private string line = null;
        private int QtdViasCozinha = int.Parse(Sessions.returnConfig.ViasCozinha);
        private int QtdViasBalcao = int.Parse(Sessions.returnConfig.ViasBalcao);
        private int QtViasEntrega = int.Parse(Sessions.returnConfig.ViasEntrega);
        private bool ProdutosPorCodigo = Sessions.returnConfig.ProdutoPorCodigo;
        private decimal TaxaEntrega;
        private bool PedidoRepetio;
        private string TrocoPagar;
        private bool FPPermiteDesconto;
        private decimal dTotalPedido;
        private decimal DMargemGarco = 0.00M;
        private string lnome;
        private string lNomeOpcao;
        private int lTipo;
        private decimal lPreco;
        private decimal dcTaxaEntrega;
        private int gMaximoOpcaoProduto;

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
        //private int HoraPedido = Sessions.returnPedido.RealizadoEm.Minute;
        public frmCadastrarPedido(Boolean iPedidoRepetio, string iDescontoPedido, string iNumeMesa, string iTroco, decimal iTaxaEntrega, Boolean IniciaTempo,
            DateTime DataPedido, int CodigoPedido, int CodPessoa, string tPara, string fPagamento, string TipoPedido, string MesaBalcao,
            Main parent = null, decimal iTotalPedido = 0.00M, decimal MargeGarcon = 0.00M,int iCodVendedor=0)
        {
            try
            {
                InitializeComponent();
                con = new Conexao();
                items = new List<ItemPedido>();
                grupos = new List<Grupo>();
                cbxTipoPedido.Visible = ContraMesas;
                txtDesconto.Text = iDescontoPedido;
                parentWindow = parent;
                codPessoa = CodPessoa;
                codPedido = CodigoPedido;
                trocoPara = tPara;
                txtTrocoPara.Text = tPara;
                formaPagamento = fPagamento;

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

                cbxListaMesas.Text = iNumeMesa;
                gNUmeroMesa = iNumeMesa;
                cbxTipoPedido.Text = TipoPedido;
                cbxListaMesas.Items.Add(MesaBalcao);
                Utils.MontaCombox(cbxVendedor, "Nome", "Codigo", "Usuario", "spObterUsuarioPorCodigo", iCodVendedor);

            }
            catch (Exception mx)
            {

                MessageBox.Show(mx.Message);
            }
        }


        private void itemsPedidoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.itemsPedidoBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dBExpertDataSet);
        }
        private void CarregaMesas()
        {
            this.cbxListaMesas.DataSource = con.SelectAll("Mesas", "spObterMesasAbertas").Tables["Mesas"];
            this.cbxListaMesas.DisplayMember = "NumeroMesa";
            this.cbxListaMesas.ValueMember = "Codigo";

        }
        private void CarregaOpcoesProduto(int iCodProduto)
        {
            con.SelectRegistroPorCodigo("Produto_Opcao", "spObterOpcoesProduto", iCodProduto);
        }

        public void frmCadastrarPedido_Load(object sender, EventArgs e)
        {
            //    lblEntrega.Text = Utils.RetornaTaxaPorCliente(codPessoa, con).ToString();
            //    AtualizaTotalPedido();

            grpVendedor.Enabled = Sessions.returnConfig.ExigeVendedorSN;
            if (gNUmeroMesa == "")
            {
                CarregaMesas();
            }

            cbxTipoPedido.Visible = ContraMesas;
            if (ControlaFidelidade)
            {
                DataSet pessoa = con.SelectPessoaPorCodigo("Pessoa", "spObterPessoaPorCodigo", codPessoa);
                DataRow dRow = pessoa.Tables["Pessoa"].Rows[0];
                NumeroPedidos = int.Parse(dRow.ItemArray.GetValue(13).ToString());
                if (NumeroPedidos == PedidosParaFidelidade)
                {
                    lblFidelidade.Visible = true;
                }
            }

            if (ProdutosPorCodigo)
            {
                cbxTipoProduto.Visible = false;
                lblGrupo.Text = "Código:";
                txtCodProduto1.Visible = true;
            }
            else
            {
                cbxTipoProduto.Visible = true;
                lblGrupo.Text = "Grupo";
                txtCodProduto1.Visible = false;
                // Utils.MontaCombox(cbxTipoPedido, "NomeGrupo", "Codigo", "Grupo", "spObterGrupoAtivo");
                this.cbxTipoProduto.DataSource = con.SelectAll("Grupo", "spObterGrupoAtivo").Tables["Grupo"];
                this.cbxTipoProduto.DisplayMember = "NomeGrupo";
                this.cbxTipoProduto.ValueMember = "Codigo";
            }

            Utils.MontaCombox(cmbFPagamento, "Descricao", "Codigo", "FormaPagamento", "spObterFormaPagamento");
            //this.cmbFPagamento.DataSource = con.SelectAll("FormaPagamento", "spObterFormaPagamento").Tables["FormaPagamento"];
            //this.cmbFPagamento.DisplayMember = "Descricao";
            //this.cmbFPagamento.ValueMember = "Codigo";

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
                DataSet pessoa = con.SelectPessoaPorCodigo("Pessoa", "spObterPessoaPorCodigo", codPessoa);
                DataRow dRow = pessoa.Tables["Pessoa"].Rows[0];
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
                        Quantidade = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<int>("Quantidade"),
                        PrecoUnitario = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<decimal>("PrecoItem"),
                        PrecoTotal = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<decimal>("PrecoTotalItem"),
                        Item = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<string>("Item"),
                        ImpressoSN = Convert.ToBoolean(itemsPedido.Tables["ItemsPedido"].Rows[i].Field<Boolean>("ImpressoSN"))
                    };

                    //if (PromocaoDiasSemana)
                    //{
                    //    List<PrecoDiaProduto> listPreco = new List<PrecoDiaProduto>();
                    //    var produto = con.SelectRegistroPorCodigo("Produto", "spObterProdutoPorCodigo", itemPedido.CodProduto, true).Tables["Produto"];
                    //    listPreco = Utils.DeserializaObjeto(produto.Rows[0]["DiaSemana"].ToString());

                    //    if (listPreco != null && listPreco.Count > 0)
                    //    {
                    //        if (con.RetornaOpcoesProduto(itemPedido.CodProduto).Tables[0].Rows.Count == 0)
                    //        {
                    //            if (listPreco.Count> 0)
                    //            {
                    //                itemPedido.PrecoUnitario = decimal.Parse(produto.Rows[0]["PrecoDesconto"].ToString());
                    //            }
                    //            else
                    //            {
                    //                itemPedido.PrecoUnitario = decimal.Parse(produto.Rows[0]["PrecoProduto"].ToString());
                    //            }

                    //        }
                    //        else
                    //        {
                    //            itemPedido.PrecoUnitario = decimal.Parse(produto.Rows[0]["PrecoProduto"].ToString());
                    //            // itemPedido.PrecoUnitario = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<decimal>("PrecoItem");
                    //        }

                    //        itemPedido.PrecoTotal = itemPedido.Quantidade * itemPedido.PrecoUnitario;
                    //    }

                    //}
                    this.txtTrocoPara.Text = trocoPara;
                    this.cmbFPagamento.Text = formaPagamento;

                    items.Add(itemPedido);
                    atualizarGrid(itemPedido);

                }
                if (DMargemGarco != 0.00M)
                {
                    lbTotal.Text = Convert.ToString(decimal.Parse(lbTotal.Text.Replace("R$", "")) + DMargemGarco);
                }
                AtualizaClienteTela(this);
            }
            else
            {
                DataSet pessoa = con.SelectPessoaPorCodigo("Pessoa", "spObterPessoaPorCodigo", codPessoa);
                DataRow dRow = pessoa.Tables["Pessoa"].Rows[0];

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

                AtualizaClienteTela(this);
            }
            this.gridViewItemsPedido.CurrentCell = null;
        }
        public void AtualizaClienteTela(frmCadastrarPedido frm)
        {
            DataSet dsPessoa = con.SelectRegistroPorCodigo("Pessoa", "spObterPessoaPorCodigo", codPessoa);

            lblNomeCliente.Text = dsPessoa.Tables[0].Rows[0].Field<string>("Nome") + " - " + dsPessoa.Tables[0].Rows[0].Field<string>("Telefone");
            lblEndereco.Text = dsPessoa.Tables[0].Rows[0].Field<string>("Endereco") + "," + dsPessoa.Tables[0].Rows[0].Field<string>("Numero") + "-" + dsPessoa.Tables[0].Rows[0].Field<string>("Bairro") + " " + dsPessoa.Tables[0].Rows[0].Field<string>("Cidade");
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

                MontaMenuOpcoes(int.Parse(this.cbxProdutosGrid.SelectedValue.ToString()));

                this.txtQuantidade.Text = "1";
                var ValorProduto = "";
                if (PromocaoDiasSemana)
                {
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


                this.txtPrecoUnitario.Text = ValorProduto;
                CalcularTotalItem();

                if (this.cbxSabor.Focused)
                {
                    var valorProduto = decimal.Parse("");
                    var valorSabor = decimal.Parse("");
                    var _produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];
                    var sabor = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxSabor.SelectedValue.ToString())).Tables["Produto"];

                    txtQuantidade.Text = "1";
                    if (PromocaoDiasSemana)
                    {
                        List<PrecoDiaProduto> listPreco = new List<PrecoDiaProduto>();
                        listPreco = Utils.DeserializaObjeto(produto.Rows[0]["DiaSemana"].ToString());
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
                        if (valorProduto > valorSabor)
                        {
                            this.txtPrecoUnitario.Text = valorProduto.ToString();
                        }
                        else
                        {
                            this.txtPrecoUnitario.Text = valorSabor.ToString();
                        }
                    }


                }
                // Pega o Preço unitário selecionado

            }
            catch (Exception erro)
            {

                throw;
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
                        lblStatusPedido.Text = dsPedido.Tables[0].Rows[0].Field<string>("Nome");
                        lblStatusPedido.ForeColor = Color.Red;
                        break;

                    case 2:
                        lblStatusPedido.Text = dsPedido.Tables[0].Rows[0].Field<string>("Nome");
                        lblStatusPedido.ForeColor = Color.Green;
                        break;
                    case 3:
                        lblStatusPedido.Text = dsPedido.Tables[0].Rows[0].Field<string>("Nome");
                        lblStatusPedido.ForeColor = Color.Blue;
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
        }
        private void cbxTipoProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string grupo = this.cbxTipoProduto.Text;
                this.cbxProdutosGrid.DataSource = con.SelectRegistroPorNome("@GrupoProduto", "Produto", "spObterProdutoPorGrupo", grupo).Tables["Produto"];
                this.cbxProdutosGrid.DisplayMember = "NomeProduto";
                this.cbxProdutosGrid.ValueMember = "Codigo";
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
            if (Utils.SoPermiteNumeros(e))
            {
                if (e.KeyChar == 13 || e.KeyChar == (char)Keys.Tab || e.KeyChar == 11 || e.KeyChar == 9)
                {
                    var precoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.ToString().Replace("R$ ", ""));
                    var quantidade = int.Parse(this.txtQuantidade.Text);
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
                itemNome = "Meia " + this.cbxProdutosGrid.Text + " / Meia " + this.cbxSabor.Text;
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
        private void MontaMenuOpcoes(int iCodProduto, int iCodProduto2 = 0)
        {
            try
            {
                DataSet dsOpcoes = con.RetornaOpcoesProduto(iCodProduto);
                DataRow dRowOpcoes;
                DataRow dRoOpcoes2;
                DataSet dsOpcoes2 = con.RetornaOpcoesProduto(iCodProduto2);
                EscondeTamanhos();
                chkListAdicionais.Items.Clear();
                if (dsOpcoes.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsOpcoes.Tables[0].Rows.Count; i++)
                    {
                        if (iCodProduto2 != 0 && lTipo == 1)
                        {
                            lPreco = RetornaMaiorValor(dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<decimal>("Preco"), dsOpcoes2.Tables["Produto_Opcao"].Rows[i].Field<decimal>("Preco"));
                        }
                        else
                        {
                            lPreco = dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<decimal>("Preco");
                        }
                        gMaximoOpcaoProduto = dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<int>("MaximoAdicionais");
                        lnome = dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<string>("Nome").Trim();//.Substring(1, 10);
                        lTipo = int.Parse(dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<string>("Tipo"));
                        // lPreco = dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<decimal>("Preco");

                        if (lTipo == 1)
                        {
                            lNomeOpcao = dsOpcoes.Tables["Produto_Opcao"].Rows[i].Field<string>("Nome").Trim();
                            if (!radioButton1.Visible)
                            {
                                radioButton1.Text = lnome;
                                radioButton1.Tag = lPreco;
                                radioButton1.Visible = true;

                                // grpBoxTamanhos.Controls.Add(rb);
                            }
                            else if (!radioButton2.Visible)
                            {
                                radioButton2.Text = lnome;
                                radioButton2.Tag = lPreco;
                                radioButton2.Visible = true;
                            }
                            else if (!radioButton3.Visible)
                            {
                                radioButton3.Text = lnome;
                                radioButton3.Tag = lPreco;
                                radioButton3.Visible = true;

                            }
                            else if (!radioButton4.Visible)
                            {
                                radioButton4.Text = lnome;
                                radioButton4.Tag = lPreco;
                                radioButton4.Visible = true;
                            }
                            else if (!radioButton5.Visible)
                            {
                                radioButton5.Text = lnome;
                                radioButton5.Tag = lPreco;
                                radioButton5.Visible = true;
                            }
                            else if (!radioButton6.Visible)
                            {
                                radioButton6.Text = lnome;
                                radioButton6.Tag = lPreco;
                                radioButton6.Visible = true;
                            }
                            else if (!rb7.Visible)
                            {
                                rb7.Text = lnome;
                                rb7.Tag = lPreco;
                                rb7.Visible = true;
                            }
                            else if (!rb8.Visible)
                            {
                                rb8.Text = lnome;
                                rb8.Tag = lPreco;
                                rb8.Visible = true;
                            }
                            else if (!rb9.Visible)
                            {
                                rb9.Text = lnome;
                                rb9.Tag = lPreco;
                                rb9.Visible = true;
                            }
                            else if (!rb10.Visible)
                            {
                                rb10.Text = lnome;
                                rb10.Tag = lPreco;
                                rb10.Visible = true;
                            }
                            else if (!rb11.Visible)
                            {
                                rb11.Text = lnome;
                                rb11.Tag = lPreco;
                                rb11.Visible = true;
                            }
                            else if (!rb12.Visible)
                            {
                                rb12.Text = lnome;
                                rb12.Tag = lPreco;
                                rb12.Visible = true;
                            }

                        }
                        if (lTipo == 2)
                        {
                            chkListAdicionais.Items.Add(lnome + "(+" + lPreco + ")", false);
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

        private void btnAdicionarItemNoPedido_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Visible && !radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked
                    && !radioButton4.Checked && !radioButton5.Checked && !radioButton6.Checked && !rb7.Checked
                     && !rb8.Checked && !rb9.Checked && !rb10.Checked && !rb11.Checked && !rb12.Checked)
                {
                    MessageBox.Show("É obrigatório selecionar o tamanho ", "[xSistemas]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                MeiaPizza();


                if (txtQuantidade.Text != "")
                {
                    var quantidade = int.Parse(this.txtQuantidade.Text.ToString());

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
                            TrocoPara = "R$" + this.txtTrocoPara.Text,
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
                                item = new ItemPedido()
                                {
                                    CodPedido = codPedido,
                                    NomeProduto = itemNome,
                                    Quantidade = int.Parse(this.txtQuantidade.Text),
                                    PrecoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.Replace("R$ ", "")),
                                    PrecoTotal = decimal.Parse(this.txtPrecoTotal.Text.Replace("R$ ", "")),
                                    Item = txtItemDescricao.Text
                                };

                                if (!Sessions.returnConfig.ProdutoPorCodigo)
                                {
                                    item.CodProduto = int.Parse(this.cbxProdutosGrid.SelectedValue.ToString());
                                }
                                else
                                {
                                    item.CodProduto = int.Parse(this.txtCodProduto1.Text);
                                }

                                item.DataAtualizacao = DateTime.Now;

                                pedido.TotalPedido = pedido.TotalPedido + item.PrecoTotal;
                                pedido.Codigo = codPedido;
                                con.Insert("spAdicionarItemAoPedido", item);

                                con.Update("spAlterarTotalPedido", pedido);
                                items.Add(item);
                                atualizarGrid(item);
                                SemMeiaPizza();
                                // Utils.PopulaGrid_Novo("Pedido", parentWindow.pedidosGridView, Sessions.SqlPedido);
                            }

                        }
                        else
                        {

                            item = new ItemPedido()
                            {
                                CodPedido = 0,
                                NomeProduto = itemNome,
                                Quantidade = int.Parse(this.txtQuantidade.Text),
                                PrecoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.Replace("R$", "")),
                                PrecoTotal = decimal.Parse(this.txtPrecoTotal.Text.Replace("R$", "")),
                                Item = txtItemDescricao.Text
                            };
                            if (!Sessions.returnConfig.ProdutoPorCodigo)
                            {
                                item.CodProduto = int.Parse(this.cbxProdutosGrid.SelectedValue.ToString());
                            }
                            else
                            {
                                item.CodProduto = int.Parse(this.txtCodProduto1.Text);
                            }

                            items.Add(item);

                            atualizarGrid(item);

                        }
                        //this.cbxProdutosGrid.Text = "";
                        //this.txtPrecoUnitario.Text = "";
                        this.txtQuantidade.Text = "1";
                        //this.txtPrecoTotal.Text = "";
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
                    MessageBox.Show("Não foi possível adicionar o produto, verifique a quantidade digitada", "Aviso Dex");

                }
            }
            catch (Exception ss)
            {

                MessageBox.Show(ss.Message);
            }

        }
        private void btnCancelarPedido_Click(object sender, EventArgs e)
        {
            this.Close();
            //EXCLUIR ITEMS DO PEDIDO E DEPOIS REMOVER PEDIDO
        }
        private Boolean ValidaMaximoDesconto()
        {
            Boolean iReturn = false;
           // bool PermiteDesconto = Sessions.retunrUsuario.DescontoPedidoSN;
            double DescMAxPermitido = Sessions.retunrUsuario.DescontoMax;
            //MessageBox.Show(lbTotal.Text + "- "+ lbTotalPedido.ToString() + " 2 "+ lbTotal.ToString());
            double TotalPedido = double.Parse(lbTotal.Text.Replace("R$", ""));

            double Cal = 100;
            double DescCalculado = Double.Parse(txtDesconto.Text) * Cal / TotalPedido;

            return iReturn = DescCalculado < DescMAxPermitido;
        }
        private int RetornaCodVendedor()
        {
            int iReturn = 0;
            try
            {
                
                if (!Sessions.returnConfig.ExigeVendedorSN)
                {
                    return 0;
                }
                if (cbxVendedor.SelectedValue != null)
                {
                    iReturn = int.Parse(cbxVendedor.SelectedValue.ToString());
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cErroGravacao);
            }
           

            return iReturn;
        }
        public void btnGerarPedido_Click(object sender, EventArgs e)
        {
            int iRetur = 0;
            try
            {
                if (cmbFPagamento.ValueMember == null)
                {
                    MessageBox.Show("Formaga de Pagamento não selecionada", "[XSistemas] Aviso");
                    cmbFPagamento.Focus();
                    return;
                }
                else
                {
                    //ds System.Windows.Forms.KeyEventArgs = new Windows.Forms.KeyEventArgs();

                    //   Boolean ivalida = object.ReferenceEquals(e,System.Windows.Forms.KeyEventArgs.Equals(Keys.F5));
                    if (AtualizaTroco(false))
                    {
                        // DBExpertDataSet dbExpert = new DBExpertDataSet();
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
                                RealizadoEm = DateTime.Now,
                                Status = "Aberto",
                                PedidoOrigem = "Balcao",
                                CodigoPedidoWS = 0,
                                CodUsuario = RetornaCodVendedor()

                            };
                            if (txtTrocoPara.Text != "")
                            {
                                pedido.TrocoPara = this.txtTrocoPara.Text;
                            }
                            else
                            {
                                pedido.TrocoPara = "0.00";
                            }

                            // Validar o Desconto Máximo Por Usuario

                            if (txtDesconto.Text != "")
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
                                    pedido.DescontoValor = decimal.Parse("0.00");
                                    return;
                                }
                                //else
                                //{
                                //    if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "DescontoPedidoSN"))
                                //    {
                                //        return;
                                //    }
                                //    else
                                //    {
                                //        pedido.DescontoValor = decimal.Parse(txtDesconto.Text);
                                //    }
                                //   // return;
                                //}



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

                            if (Sessions.returnConfig.ExigeVendedorSN && pedido.CodUsuario==0)
                            {
                                MessageBox.Show("Selecione o vendedor/atendente para continuar");
                                txtCodVendedor.Focus();
                                txtCodVendedor.BackColor = Color.Red;
                                return;
                                
                            }
                            con.Insert("spAdicionarPedido", pedido);
                            //  DataEntrada = DateTime.Now;

                            for (int i = 0; i < gridViewItemsPedido.Rows.Count; i++)
                            {
                                var itemDoPedido = new ItemPedido()
                                {
                                    CodPedido = con.getLastCodigo(),
                                    CodProduto = int.Parse(gridViewItemsPedido.Rows[i].Cells["CodProduto"].Value.ToString()),
                                    NomeProduto = gridViewItemsPedido.Rows[i].Cells[2].Value.ToString(),
                                    Quantidade = int.Parse(gridViewItemsPedido.Rows[i].Cells[3].Value.ToString()),
                                    PrecoUnitario = decimal.Parse(gridViewItemsPedido.Rows[i].Cells[4].Value.ToString().Replace("R$", "")),
                                    PrecoTotal = decimal.Parse(gridViewItemsPedido.Rows[i].Cells[3].Value.ToString()) * decimal.Parse(gridViewItemsPedido.Rows[i].Cells[4].Value.ToString().Replace("R$", "")),
                                    ImpressoSN = false,
                                    Item = items[i].Item.ToUpper()
                                };
                                itemDoPedido.DataAtualizacao = DateTime.Now;
                                con.Insert("spCriarPedido", itemDoPedido);
                                Utils.ControlaEventos("Inserir", this.Name);

                            }

                            if (ContraMesas && cbxListaMesas.Visible && pedido.NumeroMesa != "0")
                            {
                                int CodigoMesa = Utils.RetornaCodigoMesa(cbxListaMesas.Text);

                                Utils.AtualizaMesa(CodigoMesa, 2);
                            }

                            iCodPedido = con.getLastCodigo();
                            con.AtualizaSitucao(iCodPedido, Sessions.retunrUsuario.Codigo, 1);
                            MessageBox.Show("Pedido gerado com sucesso.");

                            if (ContraMesas && cbxTipoPedido.Text != "1 - Mesa")
                            {
                                prepareToPrint();
                            }
                            else if (!ContraMesas)
                            {
                                prepareToPrint();
                            }

                            if (!Utils.bMult)
                            {
                                FinalizaPedido finaliza = new FinalizaPedido()
                                {
                                    CodPedido = iCodPedido,
                                    CodPagamento = int.Parse(cmbFPagamento.SelectedValue.ToString()),
                                    ValorPagamento = pedido.TotalPedido
                                };
                                con.Insert("spAdicionarFinalizaPedido_Pedido", finaliza);
                            }

                            // Fecha o Formulario 
                            this.Close();


                        }
                    }
                }

            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possivel gravar o pedido " + erro.Message);
            }
        }
        private void AtualizaTotalPedido()
        {
            
            pedido = new Pedido()
            {
                Codigo = codPedido,
                TrocoPara = this.txtTrocoPara.Text,
                FormaPagamento = this.cmbFPagamento.Text,
                RealizadoEm = DateTime.Now,
                Tipo = cbxTipoPedido.Text,

            };
            if (DMargemGarco != 0.00M)
            {
                decimal dTotalPedido = decimal.Parse(lbTotal.Text.Replace("R$", ""));
                pedido.TotalPedido = dTotalPedido + DMargemGarco;
            }
            else
            {
                pedido.TotalPedido = decimal.Parse(lbTotal.Text.Replace("R$", ""));
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
                pedido.Tipo = "0 - Entrega";
                pedido.NumeroMesa = "";
            }
            pedido.CodUsuario = RetornaCodVendedor();
            if (Sessions.returnConfig.ExigeVendedorSN && pedido.CodUsuario==0)
            {
                MessageBox.Show("Selecione o vendedor/atendente para continuar");
                txtCodVendedor.Focus();
                txtCodVendedor.BackColor = Color.Red;
                return;
            }
            con.Update("spAlterarTotalPedido", pedido);
            // Utils.PopularGrid("Pedido", parentWindow.pedidosGridView);
        }
        private void AlterarItem(object sender, EventArgs e)
        {
            if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "EditaPedidoSN"))
            {
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
                        var itemPedido = new ItemPedido()
                        {
                            CodProduto = codigoItemParaAlterar,
                            CodPedido = codPedido,
                            NomeProduto = itemNome,
                            Quantidade = int.Parse(this.txtQuantidade.Text),
                            PrecoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.Replace("R$ ", "")),
                            PrecoTotal = decimal.Parse(this.txtPrecoTotal.Text.Replace("R$ ", "")),
                            Item = this.txtItemDescricao.Text.ToString()

                        };

                        itemPedido.DataAtualizacao = DateTime.Now;
                        itemPedido.Codigo = int.Parse(gridViewItemsPedido.Rows[rowIndex].Cells["Codigo"].Value.ToString());
                        this.gridViewItemsPedido.Rows[rowIndex].Cells[2].Value = itemPedido.NomeProduto;
                        this.gridViewItemsPedido.Rows[rowIndex].Cells[3].Value = itemPedido.Quantidade;
                        this.gridViewItemsPedido.Rows[rowIndex].Cells[4].Value = "R$ " + itemPedido.PrecoUnitario.ToString();
                        this.gridViewItemsPedido.Rows[rowIndex].Cells[5].Value = "R$ " + itemPedido.PrecoTotal.ToString();
                        this.gridViewItemsPedido.Rows[rowIndex].Cells[6].Value = itemPedido.Item.ToString();

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
                        this.btnAdicionarItemNoPedido.Text = "Adicionar";
                        this.btnAdicionarItemNoPedido.Click += new System.EventHandler(this.btnAdicionarItemNoPedido_Click);
                        this.btnAdicionarItemNoPedido.Click -= new System.EventHandler(this.AlterarItem);
                    }
                }
            }
        }
        private void gridViewItemsPedido_MouseClick(object sender, MouseEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem excluirItemPedido = new MenuItem("Excluir");
                excluirItemPedido.Click += ExcluirItem;
                m.MenuItems.Add(excluirItemPedido);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;

                m.Show(dgv, new Point(e.X, e.Y));
                AtualizaTroco();
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
                        var itemPedido = new ItemPedido()
                        {
                            CodProduto = codigoItemParaAlterar,
                            CodPedido = codPedido,
                            NomeProduto = itemNome,
                            Quantidade = int.Parse(this.txtQuantidade.Text),
                            PrecoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.Replace("R$ ", "")),
                            PrecoTotal = decimal.Parse(this.txtPrecoTotal.Text.Replace("R$ ", "")),
                            Item = this.txtItemDescricao.Text.ToString()

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
                    string iNomeProduto = gridViewItemsPedido.Rows[rowIndex].Cells["Nome do Produto"].Value.ToString();
                    codigoItemParaAlterar = int.Parse(gridViewItemsPedido.Rows[rowIndex].Cells["CodProduto"].Value.ToString());
                    int CodigoLinha = int.Parse(gridViewItemsPedido.Rows[rowIndex].Cells["Codigo"].Value.ToString());
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
                        Codigo = CodigoLinha

                    };

                    ValorTotal = ValorTotal - decimal.Parse(((this.gridViewItemsPedido.Rows[rowIndex].Cells["Preço Total"].Value.ToString()).Replace("R$ ", "")));
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
                    con.Delete("spExcluirItemPedido", itemPedido);
                    con.Update("spAlterarTotalPedido", pedido);
                    Utils.ControlaEventos("Excluir", this.Name);
                    MessageBox.Show("Item excluído com sucesso.", "DexPedido");
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
                dt.Rows.Add(row);
            }

            this.gridViewItemsPedido.AutoGenerateColumns = true;
            this.gridViewItemsPedido.DataSource = dt;
            this.lbTotal.Text = "R$ " + Convert.ToString(ValorTotal + Convert.ToDecimal(lblEntrega.Text.Replace("R$", "")));
            this.lblTroco.Text = Convert.ToString(lblTroco.Text);
        }
        private void prepareToPrint()
        {
            try
            {
                if (ContraMesas && cbxTipoPedido.Text == "1 - Mesa")
                {
                    int iCodigo;
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
                        iRetorno = Utils.ImpressaoFechamentoNovo_20(iCodigo, ImprimeLPT, QtdViasBalcao);
                    }
                    else if (Sessions.returnConfig.QtdCaracteresImp >= 30)
                    {
                        iRetorno = Utils.ImpressaoFechamentoNovo(iCodigo, ImprimeLPT, QtdViasBalcao);
                    }


                }
                // Impressão de Venda Balcão
                if (cbxTipoPedido.Text == "2 - Balcao")
                {
                    int iCodigo;
                    if (con.getLastCodigo() != 0)
                    {
                        iCodigo = con.getLastCodigo();
                    }
                    else
                    {
                        iCodigo = codPedido;
                    }

                    string iRetorno = Utils.ImpressaoBalcao(iCodigo, ImprimeLPT, QtdViasBalcao);

                    if (ImprimeLPT && iRetorno != "")
                    {
                        StreamReader tempDex = new StreamReader(iRetorno);
                        string sLine = "";
                        sLine = tempDex.ReadToEnd();
                        Utils.ImpressaoSerial(sLine, PortaImpressa, 115200);
                    }
                }

                // Imprimindo via Entrega
                if (ImprimeViaEntrega && cbxTipoPedido.Text == "0 - Entrega")
                {
                    int iCodigo;
                    string iRetorno;
                    string dblPRevisao = DataPed.AddMinutes(Convert.ToDouble(Sessions.returnConfig.PrevisaoEntrega)).ToShortTimeString();

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
                        iRetorno = Utils.ImpressaoEntreganova_20(iCodigo, decimal.Parse(lblTroco.Text.Replace("R$", "")), ImprimeLPT, QtViasEntrega);
                    }
                    else if (Sessions.returnConfig.QtdCaracteresImp >= 40 && !ImprimeLPT)
                    {
                        if (Sessions.returnEmpresa.CNPJ == "13004606798")
                        {
                            iRetorno = Utils.ImpressaoEntre_Epson(iCodigo, decimal.Parse(lblTroco.Text.Replace("R$", "")), dblPRevisao, ImprimeLPT, QtViasEntrega);
                        }
                        else
                        {
                            iRetorno = Utils.ImpressaoEntreganova(iCodigo, decimal.Parse(lblTroco.Text.Replace("R$", "")), dblPRevisao, ImprimeLPT, QtViasEntrega);
                        }

                    }
                    else if (ImprimeLPT)
                    {
                        Utils.ImpressaoEntreganova_Matricial(iCodigo, decimal.Parse(lblTroco.Text.Replace("R$", "")), dblPRevisao, false, 1);
                    }

                    //  }

                }
                if (ImprimeViaCozinha && cbxTipoPedido.Text.Contains("0 - Entrega"))
                {
                    int iCodigo;
                    if (con.getLastCodigo() != 0)
                    {
                        iCodigo = con.getLastCodigo();
                    }
                    else
                    {
                        iCodigo = codPedido;
                    }

                    string iRetorno = Utils.ImpressaoCozihanova(iCodigo, false, QtdViasCozinha);



                }
            }
            catch (Exception E)
            {

                MessageBox.Show("Não foi possivel imprimir " + E.Message, "Avisoss Dex");
            }

        }
        public void ImprimirPedidoMesa(object sender, PrintPageEventArgs ev)
        {
            if (!ImprimeLPT)
            {
                ev.Graphics.DrawString(line, printFont, Brushes.Black, 0, 0);
                ev.HasMorePages = false;
            }

        }
        private void imprimirViaCozinha(object sender, PrintPageEventArgs ev)
        {
            //  ImpressaoCozinha();
            if (!ImprimeLPT)
            {
                ev.Graphics.DrawString(line, printFont, Brushes.Black, 0, 0);
                ev.HasMorePages = false;

            }
        }
        
        private void imprimirViaEntrega(object sender, PrintPageEventArgs ev)
        {
            if (!ImprimeLPT)
            {
                ev.Graphics.DrawString(line, printFont, Brushes.Black, 0, 0);
                ev.HasMorePages = false;
            }

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
        private void btnReimprimir_Click(object sender, EventArgs e)
        {
            prepareToPrint();

        }
        private void LimpaTamanhosSabores()
        {

            // Percorre o GroupBox dos tamanhos e e  desmarca todos pra obrigar o usuario marcar o tamanho depois de selecionar os tamanhos
            foreach (System.Windows.Forms.Control ctrControl in grpBoxTamanhos.Controls)
            {
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    //Unselect all RadioButtons
                    ((System.Windows.Forms.RadioButton)ctrControl).Checked = false;
                }
            }
        }

        private void cbxMeiaPizza_CheckedChanged(object sender, EventArgs e)
        {
            LimpaTamanhosSabores();

            if (cbxMeiaPizza.Checked)
            {
                this.cbxSabor.Enabled = true;
                if (Sessions.returnConfig.ProdutoPorCodigo == true)
                {
                    this.txtCodProduto2.Visible = true;
                }
                else
                {
                    this.txtCodProduto2.Visible = false;
                    this.cbxSabor.DataSource = con.SelectRegistroPorNome("@GrupoProduto", "Produto", "spObterProdutoPorGrupo", this.cbxTipoProduto.Text).Tables["Produto"];
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
        public decimal ComparaValores()
        {
            var valorProduto = decimal.Parse("0.00");
            decimal[] valores = new decimal[2];
            var valorSabor = decimal.Parse("0.00");
            decimal iValue = 0.00M;
            if (cbxProdutosGrid.Text != null)
            {

                if (Sessions.returnConfig.ProdutoPorCodigo)
                {
                    DataTable produto;
                    DataTable sabor;

                    if (txtCodProduto1.Text != "")
                    {
                        produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(txtCodProduto1.Text)).Tables["Produto"];
                        MontaMenuOpcoes(int.Parse(txtCodProduto1.Text));
                    }
                    else
                    {
                        produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];
                        MontaMenuOpcoes(int.Parse(this.cbxProdutosGrid.SelectedValue.ToString()));
                    }

                    if (txtCodProduto2.Text != "")
                    {
                        sabor = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(txtCodProduto2.Text)).Tables["Produto"];
                        MontaMenuOpcoes(int.Parse(txtCodProduto2.Text));
                    }


                    if (PromocaoDiasSemana)
                    {
                        DiaDaPromocao = produto.Rows[0]["DiaSemana"].ToString();
                        lol = DiaDaPromocao.Split(new char[] { ';' });
                        List<PrecoDiaProduto> listPreco = new List<PrecoDiaProduto>();

                        listPreco = Utils.DeserializaObjeto(produto.Rows[0]["DiaSemana"].ToString());
                        if (listPreco != null)
                        {
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
                            valorProduto = decimal.Parse(produto.Rows[0]["PrecoProduto"].ToString());
                            //  valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
                        }

                        CalcularTotalItem();

                    }
                    else
                    {
                        valorProduto = decimal.Parse(produto.Rows[0]["PrecoProduto"].ToString());
                        // valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
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
                else if (!Sessions.returnConfig.ProdutoPorCodigo && this.cbxSabor.Text != " ")
                {
                    var produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];
                    MontaMenuOpcoes(int.Parse(cbxProdutosGrid.SelectedValue.ToString()));
                    DataTable sabor = null;
                    if (cbxSabor.Enabled)
                    {
                        sabor = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxSabor.SelectedValue.ToString())).Tables["Produto"];
                        MontaMenuOpcoes(int.Parse(cbxSabor.SelectedValue.ToString()));
                    }

                    // DiaDaPromocao = produto.Rows[0]["DiaSemana"].ToString();
                    // lol = DiaDaPromocao.Split(new char[] { ';' });

                    List<PrecoDiaProduto> listPreco = new List<PrecoDiaProduto>();

                    listPreco = Utils.DeserializaObjeto(produto.Rows[0]["DiaSemana"].ToString());
                    if (listPreco != null && listPreco.Count > 0)
                    {
                        foreach (var item in listPreco)
                        {
                            if (item.Dia == DiaDaSema)
                            {
                                txtPrecoUnitario.Text = item.Preco.ToString();
                                valorProduto = decimal.Parse(txtPrecoUnitario.Text.Replace("R$", ""));
                                break;
                            }
                            else
                            {
                                txtPrecoUnitario.Text = produto.Rows[0]["PrecoProduto"].ToString();
                            }
                            valorProduto = decimal.Parse(txtPrecoUnitario.Text.Replace("R$", ""));
                        }
                    }
                    else
                    {
                        valorProduto = decimal.Parse(produto.Rows[0]["PrecoProduto"].ToString());
                        if (cbxSabor.Enabled)
                        {
                            valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
                        }

                    }

                }

                valores[0] = valorProduto;
                valores[1] = valorSabor;

                txtPrecoUnitario.Text = valores.Max().ToString();
                // Calcula o preço total forçando a multiplicação
                txtPrecoTotal.Text = Convert.ToString(decimal.Parse(txtQuantidade.Text) * valores.Max());


            }
            return valores.Max();
        }
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
                        TrocoPara = this.txtTrocoPara.Text,
                        FormaPagamento = this.cmbFPagamento.Text
                    };

                    if (cbxTrocoParaOK.Checked == false && !troco.Equals("0,00"))
                    {
                        ValorTroco = decimal.Parse(this.txtTrocoPara.Text.ToString()) - TotalPedido;
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
                return true;
                this.txtTrocoPara.Text = "0,00";
                this.lblTroco.Text = "R$ 0,00";
            }
            else
            {
                return false;
            }
        }
        
        private void BuscaProduto1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCodProduto1.Text != "")
                {
                    var produto = con.SelectProdutoCompleto("Produto", "spObterProdutoPorCodigo", int.Parse(txtCodProduto1.Text)).Tables["Produto"];

                    if (produto.Rows.Count > 0)
                    {
                        MontaMenuOpcoes(int.Parse(txtCodProduto1.Text));
                        cbxProdutosGrid.Text = produto.Rows[0]["NomeProduto"].ToString();
                        if (PromocaoDiasSemana)
                        {
                            List<PrecoDiaProduto> listPreco = new List<PrecoDiaProduto>();
                            listPreco = Utils.DeserializaObjeto(produto.Rows[0]["DiaSemana"].ToString());

                            foreach (var item in listPreco)
                            {
                                if (item.Dia == DiaDaSema)
                                {
                                    txtPrecoUnitario.Text = item.Preco.ToString();
                                }
                                else
                                {
                                    txtPrecoUnitario.Text = produto.Rows[0]["PrecoProduto"].ToString();
                                }
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
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCodProduto2.Text != "")
                {
                    var produto = con.SelectProdutoCompleto("Produto", "spObterProdutoPorCodigo", int.Parse(txtCodProduto2.Text)).Tables["Produto"];
                    if (produto.Rows.Count > 0)
                    {
                        MontaMenuOpcoes(int.Parse(txtCodProduto2.Text));
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
                        ComparaValores();

                        //this.txtPrecoUnitario.Text = "R$ " + produto.Rows[0]["PrecoProduto"].ToString();

                    }
                    else
                    {
                        MessageBox.Show("Produto não encontrado");
                    }
                }
            }

        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastrarPedido));
            this.dBExpertDataSet = new DexComanda.DBExpertDataSet();
            this.itemsPedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.itemsPedidoTableAdapter = new DexComanda.DBExpertDataSetTableAdapters.ItemsPedidoTableAdapter();
            this.tableAdapterManager = new DexComanda.DBExpertDataSetTableAdapters.TableAdapterManager();
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
            this.btnAtlCadastro = new System.Windows.Forms.Button();
            this.lblTempo = new System.Windows.Forms.Label();
            this.lblFidelidade = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.dBExpertDataSet)).BeginInit();
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
            // dBExpertDataSet
            // 
            this.dBExpertDataSet.DataSetName = "DBExpertDataSet";
            this.dBExpertDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // itemsPedidoBindingSource
            // 
            this.itemsPedidoBindingSource.DataMember = "ItemsPedido";
            this.itemsPedidoBindingSource.DataSource = this.dBExpertDataSet;
            // 
            // itemsPedidoTableAdapter
            // 
            this.itemsPedidoTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.base_cepTableAdapter = null;
            this.tableAdapterManager.GrupoTableAdapter = null;
            this.tableAdapterManager.ItemsPedidoTableAdapter = this.itemsPedidoTableAdapter;
            this.tableAdapterManager.PedidoTableAdapter = null;
            this.tableAdapterManager.PessoaTableAdapter = null;
            this.tableAdapterManager.ProdutoTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = DexComanda.DBExpertDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
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
            this.cbxTipoProduto.SelectionChangeCommitted += new System.EventHandler(this.cbxTipoProduto_SelectionChangeCommitted);
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
            this.txtQuantidade.Size = new System.Drawing.Size(149, 26);
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
            this.label2.Location = new System.Drawing.Point(231, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Preço Unitário:";
            // 
            // txtPrecoUnitario
            // 
            this.txtPrecoUnitario.Enabled = false;
            this.txtPrecoUnitario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoUnitario.Location = new System.Drawing.Point(314, 152);
            this.txtPrecoUnitario.Name = "txtPrecoUnitario";
            this.txtPrecoUnitario.Size = new System.Drawing.Size(149, 26);
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
            this.txtPrecoTotal.Location = new System.Drawing.Point(547, 151);
            this.txtPrecoTotal.Name = "txtPrecoTotal";
            this.txtPrecoTotal.Size = new System.Drawing.Size(105, 26);
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
            this.panel1.Controls.Add(this.btnAtlCadastro);
            this.panel1.Controls.Add(this.lblTempo);
            this.panel1.Controls.Add(this.lblFidelidade);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1051, 43);
            this.panel1.TabIndex = 41;
            // 
            // btnAtlCadastro
            // 
            this.btnAtlCadastro.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAtlCadastro.Location = new System.Drawing.Point(567, 4);
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
            this.lblTempo.Location = new System.Drawing.Point(645, 11);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(79, 20);
            this.lblTempo.TabIndex = 11;
            this.lblTempo.Text = "00:00:00";
            // 
            // lblFidelidade
            // 
            this.lblFidelidade.AutoSize = true;
            this.lblFidelidade.BackColor = System.Drawing.Color.Red;
            this.lblFidelidade.Font = new System.Drawing.Font("Marlett", 20.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFidelidade.Location = new System.Drawing.Point(749, 6);
            this.lblFidelidade.Name = "lblFidelidade";
            this.lblFidelidade.Size = new System.Drawing.Size(241, 32);
            this.lblFidelidade.TabIndex = 10;
            this.lblFidelidade.Text = "Pedido Fidelidade";
            this.lblFidelidade.Visible = false;
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
            this.cbxProdutosGrid.SelectionChangeCommitted += new System.EventHandler(this.cbxProdutosGrid_SelectionChangeCommitted);
            // 
            // gridViewItemsPedido
            // 
            this.gridViewItemsPedido.AllowUserToAddRows = false;
            this.gridViewItemsPedido.AllowUserToDeleteRows = false;
            this.gridViewItemsPedido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridViewItemsPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewItemsPedido.Location = new System.Drawing.Point(12, 230);
            this.gridViewItemsPedido.Name = "gridViewItemsPedido";
            this.gridViewItemsPedido.ReadOnly = true;
            this.gridViewItemsPedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridViewItemsPedido.Size = new System.Drawing.Size(640, 113);
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
            this.panel2.Location = new System.Drawing.Point(10, 449);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(647, 116);
            this.panel2.TabIndex = 42;
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
            this.btnMultiploPagamento.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMultiploPagamento.FlatAppearance.BorderSize = 5;
            this.btnMultiploPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnMultiploPagamento.Location = new System.Drawing.Point(275, 82);
            this.btnMultiploPagamento.Name = "btnMultiploPagamento";
            this.btnMultiploPagamento.Size = new System.Drawing.Size(178, 31);
            this.btnMultiploPagamento.TabIndex = 61;
            this.btnMultiploPagamento.Text = "+ F. Pagamento [F10]";
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
            this.cbxMeiaPizza.Location = new System.Drawing.Point(580, 124);
            this.cbxMeiaPizza.Name = "cbxMeiaPizza";
            this.cbxMeiaPizza.Size = new System.Drawing.Size(72, 17);
            this.cbxMeiaPizza.TabIndex = 54;
            this.cbxMeiaPizza.Text = "2 sabores";
            this.cbxMeiaPizza.UseVisualStyleBackColor = true;
            this.cbxMeiaPizza.CheckedChanged += new System.EventHandler(this.cbxMeiaPizza_CheckedChanged);
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
            this.panel3.Location = new System.Drawing.Point(12, 348);
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
            this.label6.Location = new System.Drawing.Point(669, 19);
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
            this.lblEndereco.Location = new System.Drawing.Point(106, 19);
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
            this.lblNomeCliente.Location = new System.Drawing.Point(12, 3);
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
            this.panel5.Location = new System.Drawing.Point(671, 89);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(368, 467);
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
            this.chkListAdicionais.Location = new System.Drawing.Point(3, 199);
            this.chkListAdicionais.Name = "chkListAdicionais";
            this.chkListAdicionais.Size = new System.Drawing.Size(353, 259);
            this.chkListAdicionais.TabIndex = 0;
            this.chkListAdicionais.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkListAdicionais_ItemCheck);
            // 
            // grpVendedor
            // 
            this.grpVendedor.Controls.Add(this.cbxVendedor);
            this.grpVendedor.Controls.Add(this.txtCodVendedor);
            this.grpVendedor.Location = new System.Drawing.Point(12, 404);
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
            // frmCadastrarPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1051, 568);
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
            this.Activated += new System.EventHandler(this.frmCadastrarPedido_Activated);
            this.Load += new System.EventHandler(this.frmCadastrarPedido_Load);
            this.Shown += new System.EventHandler(this.frmCadastrarPedido_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCadastrarPedido_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dBExpertDataSet)).EndInit();
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
            if (txtQuantidade.Text != "")
            {
                CalcularTotalItem();
            }


        }

        private void CalcularTotalItem()
        {
            if (txtQuantidade.Text != "" && txtPrecoUnitario.Text != "" || cbxProdutosGrid.SelectedItem != null)
            {
                var precoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.ToString().Replace("R$ ", ""));
                var quantidade = int.Parse(this.txtQuantidade.Text);
                var total = precoUnitario * quantidade;
                this.txtPrecoTotal.Text = total.ToString();
                this.btnAdicionarItemNoPedido.Focus();
            }
        }
        private void CalculaTempPedido()
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = DataPed.Hour;
            relogio.Tick += delegate
            {
                tempo -= DateTime.Now.Hour;
                lblTempo.Text = tempo.ToString();
                if (tempo == TempPrevisto)
                {
                    relogio.Stop();
                }
            };
            relogio.Start();
        }

        private void cbxTipoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCalGarcon.Enabled = cbxTipoPedido.Text == "1 - Mesa";

            if (cbxTipoPedido.Text == "1 - Mesa")
            {
                cbxListaMesas.Visible = true;
                cbxListaMesas.Focus();
            }
            else
            {
                cbxListaMesas.Visible = false;
            }
            CalculaTaxaEntrega(cbxTipoPedido.Text == "0 - Entrega");

        }
        private void CalculaTaxaEntrega(Boolean iCalcula)
        {
            if (!iCalcula)
            {
                // decimal iValorSemTaxa = decimal.Parse(lbTotal.Text) - lblEntrega.Text);
                this.lbTotal.Text = "R$ " + Convert.ToString(decimal.Parse(lbTotal.Text.Replace("R$", "")) - decimal.Parse(lblEntrega.Text.Replace("R$", "")));
                lblEntrega.Text = "R$ 0,00 ";
            }
            else
            {
                this.lbTotal.Text = "R$ " + Convert.ToString(ValorTotal + dcTaxaEntrega);
                lblEntrega.Text = "R$ " + dcTaxaEntrega;
            }

            AlteraTotalPedido(gNUmeroMesa, dTotalPedido);
        }

        private void AlteraTotalPedido(string iNumMesa, decimal iTotalPedido)
        {
            NovoTotalPedido pedi = new NovoTotalPedido()
            {
                Codigo = codPedido,
                NumeroMesa = gNUmeroMesa,
                TotalPedido = dTotalPedido,
                Tipo = cbxTipoPedido.Text,
                CodUsuario = int.Parse(txtCodVendedor.Text)
            };
            con.Update("spAlterarTotalPedido", pedi);
        }

        private void cbxTrocoParaOK_CheckedChanged(object sender, EventArgs e)
        {
            txtTrocoPara.Enabled = !cbxTrocoParaOK.Checked;
            txtTrocoPara.Text = "0,00";
        }

        private void cbxSabor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LimpaTamanhosSabores();
            ComparaValores();
            MontaMenuOpcoes(int.Parse(cbxSabor.SelectedValue.ToString()), int.Parse(cbxSabor.SelectedValue.ToString()));
        }

        private void frmCadastrarPedido_KeyDown(object sender, KeyEventArgs e)
        {
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
            Timer HoraAtual;
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
            if(e.KeyChar != Convert.ToChar(Keys.Enter))
            {
                return;
            }

            decimal TotalPedido = ValorTotal + Convert.ToDecimal(lblEntrega.Text.Replace("R$",""));
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
                        gridViewItemsPedido.Rows[0].Cells[4].Value = decimal.Parse(gridViewItemsPedido.Rows[0].Cells[4].Value.ToString().Replace("R$", "")) - decimal.Parse(txtDesconto.Text);

                        gridViewItemsPedido.Rows[0].Cells[5].Value = decimal.Parse(gridViewItemsPedido.Rows[0].Cells[4].Value.ToString().Replace("R$", "")) * decimal.Parse(gridViewItemsPedido.Rows[0].Cells[3].Value.ToString());

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

            iReturn = Regex.Replace(istring, "[^a-zA-Z áéíóú]+", "");


            return iReturn;
        }
        private string ObterSomenteNumerosReais(string iValue)
        {
            string ire;
            ire = Regex.Replace(iValue, "[^0-9 ,]+", "");
            return ire;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                decimal lPreco = decimal.Parse(radioButton1.Tag.ToString());
                txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = cbxSabor.Text + " " + radioButton1.Text;
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
                decimal lPreco = decimal.Parse(radioButton2.Tag.ToString());
                txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = cbxSabor.Text + " " + radioButton2.Text;
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
                decimal lPreco = decimal.Parse(radioButton3.Tag.ToString());
                txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = cbxSabor.Text + " " + radioButton3.Text;
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
                decimal lPreco = decimal.Parse(radioButton4.Tag.ToString());
                txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                //cbxProdutosGrid.Text = iNomeProd + " " + radioButton4.Text;
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = cbxSabor.Text + " " + radioButton4.Text;
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
                decimal lPreco = decimal.Parse(radioButton5.Tag.ToString());
                txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                // cbxProdutosGrid.Text = iNomeProd + " " + radioButton5.Text;
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = cbxSabor.Text + " " + radioButton5.Text;
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
                decimal lPreco = decimal.Parse(radioButton6.Tag.ToString());
                txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                if (cbxMeiaPizza.Checked)
                {
                    cbxSabor.Text = cbxSabor.Text + " " + radioButton6.Text;
                }
                else
                {
                    cbxProdutosGrid.Text = iNomeProd + " " + radioButton6.Text;
                }
                //cbxProdutosGrid.Text = iNomeProd + " " + radioButton6.Text;
                CalcularTotalItem();
            }
        }


        private void chkListAdicionais_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (chkListAdicionais.CheckedItems.Count + 1 > gMaximoOpcaoProduto)
            {
                // chkListAdicionais.SetItemCheckState(chkListAdicionais.SelectedIndex, CheckState.Unchecked);
                MessageBox.Show("O Produto " + cbxProdutosGrid.Text + " só permite " + gMaximoOpcaoProduto + " Adicionais");
                return;
            }


            if (e.CurrentValue == CheckState.Unchecked)
            {
                txtItemDescricao.Text = txtItemDescricao.Text + " + " + ObterSomenteLetras(chkListAdicionais.SelectedItem.ToString());
                decimal iValorItem = decimal.Parse(txtPrecoUnitario.Text.Replace("R$", ""));
                decimal iValorAdicional = decimal.Parse(ObterSomenteNumerosReais(chkListAdicionais.SelectedItem.ToString()));
                decimal iValor = iValorItem + iValorAdicional;
                txtPrecoUnitario.Text = Convert.ToString(iValor);

                CalcularTotalItem();
            }
            else
            {
                txtItemDescricao.Text = txtItemDescricao.Text.Replace(" + " + ObterSomenteLetras(chkListAdicionais.SelectedItem.ToString()), string.Empty);
                // txtItemDescricao.Text = txtItemDescricao.Text.Replace("+", string.Empty);
                decimal iValorItem = decimal.Parse(txtPrecoUnitario.Text.Replace("R$", ""));
                decimal iValorAdicional = decimal.Parse(ObterSomenteNumerosReais(chkListAdicionais.SelectedItem.ToString()));
                decimal iValor = iValorItem - iValorAdicional;

                txtPrecoUnitario.Text = Convert.ToString(iValor);

                CalcularTotalItem();
            }
        }

        private void MultiplaFormasPagamento(object sender, EventArgs e)
        {
            if (ValidaTroco())
            {
                Boolean iInsereAtualiza = this.btnGerarPedido.Text != "Alterar";
                frmFinalizacaoPedido frm = new frmFinalizacaoPedido(decimal.Parse(lbTotal.Text.Replace("R$", "")), this, iInsereAtualiza, codPedido);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show(" Troco é menor que total do Pedido");
            }



        }

        private void rb7_Click(object sender, EventArgs e)
        {
            if (rb7.Checked)
            {
                decimal lPreco = decimal.Parse(rb7.Tag.ToString());
                txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
                cbxProdutosGrid.Text = iNomeProd + " " + rb7.Text;
                CalcularTotalItem();
            }
        }

        private void rb8_Click(object sender, EventArgs e)
        {
            if (rb8.Checked)
            {
                decimal lPreco = decimal.Parse(rb8.Tag.ToString());
                txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
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
                decimal lPreco = decimal.Parse(rb9.Tag.ToString());
                txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
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
                decimal lPreco = decimal.Parse(rb10.Tag.ToString());
                txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
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
                decimal lPreco = decimal.Parse(rb11.Tag.ToString());
                txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
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
                decimal lPreco = decimal.Parse(rb12.Tag.ToString());
                txtPrecoUnitario.Text = Convert.ToString(lPreco + ComparaValores());
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

                    frmCadastroCliente frm = new frmCadastroCliente(int.Parse(dRowPessoa.ItemArray.GetValue(0).ToString()), dRowPessoa.ItemArray.GetValue(1).ToString(), dRowPessoa.ItemArray.GetValue(10).ToString(),
                                                                      dRowPessoa.ItemArray.GetValue(11).ToString(), dRowPessoa.ItemArray.GetValue(2).ToString(), dRowPessoa.ItemArray.GetValue(3).ToString(), dRowPessoa.ItemArray.GetValue(9).ToString()
                                                                      , dRowPessoa.ItemArray.GetValue(4).ToString(), dRowPessoa.ItemArray.GetValue(5).ToString(), dRowPessoa.ItemArray.GetValue(6).ToString(), dRowPessoa.ItemArray.GetValue(7).ToString()
                                                                  , dRowPessoa.ItemArray.GetValue(8).ToString(), int.Parse(dRowPessoa.ItemArray.GetValue(14).ToString()), dRowPessoa.ItemArray.GetValue(15).ToString(), dRowPessoa.ItemArray.GetValue(12).ToString());
                    
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
                    MontaMenuOpcoes(codItem);
                    var itemCompleto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", codItem);
                    string itemNome = this.gridViewItemsPedido.Rows[rowIndex].Cells[2].Value.ToString();

                    string[] sabores = (this.gridViewItemsPedido.Rows[rowIndex].Cells[3].Value.ToString()).Split('/');
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

                    codigoItemParaAlterar = int.Parse(this.gridViewItemsPedido.Rows[rowIndex].Cells["CodProduto"].Value.ToString());
                    txtCodProduto1.Text = codItem.ToString();
                    this.txtPrecoUnitario.Text = this.gridViewItemsPedido.Rows[rowIndex].Cells[4].Value.ToString();
                    this.txtQuantidade.Text = this.gridViewItemsPedido.Rows[rowIndex].Cells[3].Value.ToString();
                    this.txtPrecoTotal.Text = this.gridViewItemsPedido.Rows[rowIndex].Cells[5].Value.ToString();
                    this.txtItemDescricao.Text = this.gridViewItemsPedido.Rows[rowIndex].Cells[6].Value.ToString();
                    //   btnAdicionarItemNoPedido.

                    this.btnAdicionarItemNoPedido.Text = "Alterar Item";
                    this.btnAdicionarItemNoPedido.Click += new System.EventHandler(this.AlterarItem);
                    this.btnAdicionarItemNoPedido.Click -= new System.EventHandler(this.btnAdicionarItemNoPedido_Click);

                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel selecionar o Item para edição " + erro.Message);
            }
            // DataGridView dgv = sender as DataGridView;

        }

        private void gridViewItemsPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void frmCadastrarPedido_Shown(object sender, EventArgs e)
        {
           
        }

        private void frmCadastrarPedido_Activated(object sender, EventArgs e)
        {
         
        }

        private void BuscaVendedor(object sender, EventArgs e)
        {
            if (codPedido!=0 && !PedidoRepetio)
            {
                if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "EditaPedidoSN"))
                {
                    return;
                }
            }
            if (txtCodVendedor.Text!="")
            {
                Utils.MontaCombox(cbxVendedor, "Nome","Codigo","Usuario", "spObterUsuarioPorCodigo", int.Parse(txtCodVendedor.Text));
            }
        }

        private void txtCodVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void cbxSabor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void cbxTipoProduto_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //this.cbxProdutosGrid.DataSource = con.SelectRegistroPorNome("@GrupoProduto", "Produto", "spObterProdutoPorGrupo", this.cbxTipoProduto.Text).Tables["Produto"];
            //this.cbxProdutosGrid.DisplayMember = "NomeProduto";
            //this.cbxProdutosGrid.ValueMember = "Codigo";
            //this.txtQuantidade.Text = "1";
            //SemMeiaPizza();
        }

        private void cbxProdutosGrid_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //var produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];

            //MontaMenuOpcoes(int.Parse(this.cbxProdutosGrid.SelectedValue.ToString()));

            //this.txtQuantidade.Text = "1";
            //var ValorProduto = "";
            //if (PromocaoDiasSemana)
            //{
            //    DiaDaPromocao = produto.Rows[0]["DiaSemana"].ToString();
            //    lol = DiaDaPromocao.Split(new char[] { ';' });
            //    if (DiaDaPromocao.IndexOf(DiaDaSema) > 0)
            //    {
            //        ValorProduto = "R$ " + produto.Rows[0]["PrecoDesconto"].ToString();
            //    }
            //    else
            //    {
            //        ValorProduto = "R$ " + produto.Rows[0]["PrecoProduto"].ToString();
            //    }
            //}
            //else
            //{
            //    ValorProduto = "R$ " + produto.Rows[0]["PrecoProduto"].ToString();
            //}


            //this.txtPrecoUnitario.Text = ValorProduto;
            //CalcularTotalItem();

            //if (this.cbxSabor.Focused)
            //{
            //    var valorProduto = decimal.Parse("");
            //    var valorSabor = decimal.Parse("");
            //    var _produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];
            //    var sabor = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxSabor.SelectedValue.ToString())).Tables["Produto"];

            //    txtQuantidade.Text = "1";
            //    if (PromocaoDiasSemana)
            //    {
            //        DiaDaPromocao = _produto.Rows[0]["DiaSemana"].ToString();
            //        lol = DiaDaPromocao.Split(new char[] { ';' });
            //        if (lol.Contains(DiaDaSema))
            //        {
            //            valorProduto = decimal.Parse(_produto.Rows[0]["PrecoDesconto"].ToString());
            //            valorSabor = decimal.Parse(sabor.Rows[0]["PrecoDesconto"].ToString());
            //        }
            //        else
            //        {
            //            valorProduto = decimal.Parse(_produto.Rows[0]["PrecoProduto"].ToString());
            //            valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
            //        }

            //        if (valorProduto > valorSabor)
            //        {
            //            this.txtPrecoUnitario.Text = "R$ " + valorProduto;
            //        }
            //        else
            //        {
            //            this.txtPrecoUnitario.Text = "R$ " + valorSabor;
            //        }
            //    }
            //    else
            //    {
            //        valorProduto = decimal.Parse(_produto.Rows[0]["PrecoProduto"].ToString());
            //        valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
            //        if (valorProduto > valorSabor)
            //        {
            //            this.txtPrecoUnitario.Text = "R$ " + valorProduto;
            //        }
            //        else
            //        {
            //            this.txtPrecoUnitario.Text = "R$ " + valorSabor;
            //        }
            //    }


            //}
            //// Pega o Preço unitário selecionado
            //iTotalItem = txtPrecoUnitario.Text;
            //iNomeProd = cbxProdutosGrid.Text;
        }



    }
}
