
using DexComanda.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
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
        private Main parentWindow;
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
       
        //private int HoraPedido = Sessions.returnPedido.RealizadoEm.Minute;
        public frmCadastrarPedido(Boolean iPedidoRepetio, string iDescontoPedido ,string iNumeMesa, string iTroco, decimal iTaxaEntrega, Boolean IniciaTempo,
            DateTime DataPedido, int CodigoPedido, int CodPessoa, string tPara, string fPagamento, string TipoPedido, string MesaBalcao,
            Main parent, decimal iTotalPedido)
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
                cbxTipoPedido.Text = TipoPedido;
                cbxListaMesas.Items.Add( MesaBalcao);
                DataPed = DataPedido;
                PedidoRepetio = iPedidoRepetio;
                dTotalPedido = iTotalPedido;

                timer1.Enabled = IniciaTempo;
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

        private void frmCadastrarPedido_Load(object sender, EventArgs e)
        {
            if (Sessions.returnUsuario != null)
            {
                // Define se o Usuario pode ou não dar Desconto
                if (Sessions.retunrUsuario.DescontoPedidoSN)
                {
                    txtDesconto.Enabled = Sessions.returnUsuario.DescontoPedidoSN;
                }
                
            }

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
            }
            this.cbxTipoProduto.DataSource = con.SelectAll("Grupo", "spObterGrupo").Tables["Grupo"];
            this.cbxTipoProduto.DisplayMember = "NomeGrupo";
            this.cbxTipoProduto.ValueMember = "Codigo";

            this.cmbFPagamento.DataSource = con.SelectAll("FormaPagamento", "spObterFormaPagamento").Tables["FormaPagamento"];
            this.cmbFPagamento.DisplayMember = "Descricao";
            this.cmbFPagamento.ValueMember = "Codigo";

            if (codPedido != 0 || PedidoRepetio)
            {
                if (!PedidoRepetio)
                {
                    this.label3.Text = "Editar Pedido ( N:" + codPedido + ")";
                    this.btnGerarPedido.Text = "Alterar";
                    this.btnGerarPedido.Click -= btnGerarPedido_Click;
                    this.btnGerarPedido.Click += AlterarItemPedido;
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

                for (int i = 0; i < itemsPedido.Tables[0].Rows.Count; i++)
                {
                    var itemPedido = new ItemPedido()
                    {
                        CodProduto = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<int>("CodProduto"),
                        NomeProduto = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<string>("NomeProduto"),
                        Quantidade = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<int>("Quantidade"),
                        PrecoUnitario = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<decimal>("PrecoItem"),
                        PrecoTotal = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<decimal>("PrecoTotalItem"),
                        Item = itemsPedido.Tables["ItemsPedido"].Rows[i].Field<string>("Item"),
                        ImpressoSN = Convert.ToBoolean(itemsPedido.Tables["ItemsPedido"].Rows[i].Field<Boolean>("ImpressoSN"))
                    };

                    if (PromocaoDiasSemana)
                    {
                        var produto = con.SelectRegistroPorCodigo("Produto", "spObterProdutoPorCodigo", itemPedido.CodProduto).Tables["Produto"];
                        if (produto != null)
                        {
                            DiaDaPromocao = produto.Rows[0]["DiaSemana"].ToString();
                            //lol = DiaDaPromocao.Split(new char[] { ';' });
                            if (DiaDaPromocao.IndexOf(DiaDaSema) > 0)
                            {
                                itemPedido.PrecoUnitario = decimal.Parse(produto.Rows[0]["PrecoDesconto"].ToString());
                            }
                            else
                            {
                                itemPedido.PrecoUnitario = decimal.Parse(produto.Rows[0]["PrecoProduto"].ToString());
                            }

                            itemPedido.PrecoTotal = itemPedido.Quantidade * itemPedido.PrecoUnitario;
                        }

                    }
                    this.txtTrocoPara.Text = trocoPara;
                    this.cmbFPagamento.Text = formaPagamento;

                    items.Add(itemPedido);
                    atualizarGrid(itemPedido);

                }
                lblNomeCliente.Text = pCliente.Nome + "-" + pCliente.Telefone;
                lblEndereco.Text = pCliente.Endereco + "," + pCliente.Numero + "-" + pCliente.Bairro +" "+ pCliente.Cidade;
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

                lblNomeCliente.Text = pCliente.Nome +"-"+  pCliente.Telefone;
                lblEndereco.Text = pCliente.Endereco + "," + pCliente.Numero + "-" + pCliente.Bairro +" " +pCliente.Cidade;
            }
            this.gridViewItemsPedido.CurrentCell = null;
        }

        private void cbxProdutosGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            var produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];
            this.txtQuantidade.Text = "1";
            var ValorProduto = "";
            if (PromocaoDiasSemana)
            {
                DiaDaPromocao = produto.Rows[0]["DiaSemana"].ToString();
                lol = DiaDaPromocao.Split(new char[] { ';' });
                if (DiaDaPromocao.IndexOf(DiaDaSema) > 0)
                {
                    ValorProduto = "R$ " + produto.Rows[0]["PrecoDesconto"].ToString();
                }
                else
                {
                    ValorProduto = "R$ " + produto.Rows[0]["PrecoProduto"].ToString();
                }
            }
            else
            {
                ValorProduto = "R$ " + produto.Rows[0]["PrecoProduto"].ToString();
            }


            this.txtPrecoUnitario.Text = ValorProduto;
            CalcularTotalItem();
            //if (this.cbxSabor.Enabled == true) 
            //{
            if (this.cbxSabor.Focused == true)
            {
                var valorProduto = decimal.Parse("");
                var valorSabor = decimal.Parse("");
                var _produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];
                var sabor = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxSabor.SelectedValue.ToString())).Tables["Produto"];

                txtQuantidade.Text = "1";
                if (PromocaoDiasSemana)
                {
                    DiaDaPromocao = _produto.Rows[0]["DiaSemana"].ToString();
                    lol = DiaDaPromocao.Split(new char[] { ';' });
                    if (lol.Contains(DiaDaSema))
                    {
                        valorProduto = decimal.Parse(_produto.Rows[0]["PrecoDesconto"].ToString());
                        valorSabor = decimal.Parse(sabor.Rows[0]["PrecoDesconto"].ToString());
                    }
                    else
                    {
                        valorProduto = decimal.Parse(_produto.Rows[0]["PrecoProduto"].ToString());
                        valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
                    }

                    if (valorProduto > valorSabor)
                    {
                        this.txtPrecoUnitario.Text = "R$ " + valorProduto;
                    }
                    else
                    {
                        this.txtPrecoUnitario.Text = "R$ " + valorSabor;
                    }
                }
                else
                {
                    valorProduto = decimal.Parse(_produto.Rows[0]["PrecoProduto"].ToString());
                    valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
                    if (valorProduto > valorSabor)
                    {
                        this.txtPrecoUnitario.Text = "R$ " + valorProduto;
                    }
                    else
                    {
                        this.txtPrecoUnitario.Text = "R$ " + valorSabor;
                    }
                }


            }
            //  }
        }
        private void SemMeiaPizza()
        {
            this.cbxSabor.Enabled = false;
            this.cbxSabor.Text = "";
            this.cbxMeiaPizza.Checked = false;
        }

        private void cbxTipoProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxProdutosGrid.DataSource = con.SelectRegistroPorNome("@GrupoProduto", "Produto", "spObterProdutoPorGrupo", this.cbxTipoProduto.Text).Tables["Produto"];
            this.cbxProdutosGrid.DisplayMember = "NomeProduto";
            this.cbxProdutosGrid.ValueMember = "Codigo";
            this.txtQuantidade.Text = "1";
            SemMeiaPizza();
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
                    this.txtPrecoTotal.Text = "R$ " + total.ToString();
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

        private void btnAdicionarItemNoPedido_Click(object sender, EventArgs e)
        {
            try
            {
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
                            TotalPedido = ValorTotal + decimal.Parse(lblEntrega.Text),
                            TrocoPara = "R$" + this.txtTrocoPara.Text,
                            FormaPagamento = this.cmbFPagamento.Text,
                            RealizadoEm = DateTime.Now

                        };

                        pedido.Tipo = cbxTipoPedido.Text;
                        if (ContraMesas && cbxTipoPedido.Text == "1 - Mesa/Balcao")
                        {
                            pedido.Tipo = cbxTipoPedido.Text;
                            pedido.NumeroMesa = int.Parse(cbxListaMesas.Text);
                        }

                        if (codPedido != 0)
                        {
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

                                if (Sessions.returnConfig.ProdutoPorCodigo == false)
                                {
                                    item.CodProduto = int.Parse(this.cbxProdutosGrid.SelectedValue.ToString());
                                }
                                else
                                {
                                    item.CodProduto = int.Parse(this.txtCodProduto1.Text);
                                }

                                pedido.TotalPedido = pedido.TotalPedido + item.PrecoTotal;
                                con.Insert("spAdicionarItemAoPedido", item);
                                con.Update("spAlterarTotalPedido", pedido);
                                items.Add(item);
                                atualizarGrid(item);
                                SemMeiaPizza();
                            }

                        }
                        else
                        {

                            item = new ItemPedido()
                            {
                                CodPedido = 0,
                                CodProduto = int.Parse(this.cbxProdutosGrid.SelectedValue.ToString()),
                                NomeProduto = itemNome,
                                Quantidade = int.Parse(this.txtQuantidade.Text),
                                PrecoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.Replace("R$ ", "")),
                                PrecoTotal = decimal.Parse(this.txtPrecoTotal.Text.Replace("R$ ", "")),
                                Item = txtItemDescricao.Text
                            };

                            items.Add(item);

                            atualizarGrid(item);

                        }
                        if (ProdutosPorCodigo)
                        {
                            txtCodProduto1.Focus();
                        }
                        else
                        {
                            cbxTipoProduto.Focus();
                        }
                        this.cbxProdutosGrid.Text = "";
                        this.txtPrecoUnitario.Text = "";
                        this.txtQuantidade.Text = "";
                        this.txtPrecoTotal.Text = "";
                        this.txtItemDescricao.Text = "";
                        this.txtCodProduto1.Text = "";

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

        private void btnGerarPedido_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (AtualizaTroco())
                {
                    DBExpertDataSet dbExpert = new DBExpertDataSet();
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
                           
                        };
                        if (txtTrocoPara.Text!="")
                        {
                            pedido.TrocoPara = this.txtTrocoPara.Text;
                        }
                        else
                        {
                            pedido.TrocoPara = "0.00";
                        }

                        // Validar o Desconto Máximo Por Usuario
                        if (Sessions.returnUsuario!=null)
                        {
                            bool PermiteDesconto = Sessions.returnUsuario.DescontoPedidoSN;
                            double DescMAxPermitido = Sessions.returnUsuario.DescontoMax;
                            double TotalPedido = double.Parse(lbTotal.Text.Replace("R$", ""));

                            if (txtDesconto.Text != "" && PermiteDesconto)
                            {
                                double Cal = 100;

                                double DescCalculado = Double.Parse(txtDesconto.Text) * Cal / TotalPedido;

                            if (DescCalculado < DescMAxPermitido)
                            {
                                pedido.DescontoValor = decimal.Parse(txtDesconto.Text);
                            }
                            else
                            {
                                MessageBox.Show("Desconto máximo do usuário superado , favor verificar", "Aviso ");
                                return;
                            }

                            
                            }
                            else
                            {
                                pedido.DescontoValor = decimal.Parse("0.00");
                            }
                        }
                        
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
                            pedido.NumeroMesa = int.Parse(cbxListaMesas.SelectedValue.ToString());
                            pedido.CodigoMesa = pedido.NumeroMesa;
                        }
                        else
                        {
                            pedido.NumeroMesa = 0;
                            //  pedido.CodigoMesa
                        }

                        con.Insert("spAdicionarPedido", pedido);
                        //  DataEntrada = DateTime.Now;

                        for (int i = 0; i < items.Count; i++)
                        {
                            var itemDoPedido = new ItemPedido()
                            {
                                CodPedido = con.getLastCodigo(),
                                CodProduto = items[i].CodProduto,
                                NomeProduto = items[i].NomeProduto,
                                Quantidade = items[i].Quantidade,
                                PrecoUnitario = items[i].PrecoUnitario,
                                PrecoTotal = items[i].PrecoTotal,
                                ImpressoSN = false,
                                Item = items[i].Item
                            };
                            con.Insert("spCriarPedido", itemDoPedido);
                            Utils.ControlaEventos("Inserir", this.Name);
                        }

                        Utils.PopularGrid("Pedido", parentWindow.pedidosGridView);

                        if (ContraMesas && cbxListaMesas.Visible && pedido.NumeroMesa != 0)
                        {
                            int CodigoMesa = Utils.RetornaCodigoMesa(cbxListaMesas.Text);

                            Utils.AtualizaMesa( cbxListaMesas.Text, 2);
                        }


                        MessageBox.Show("Pedido gerado com sucesso.");

                        if (ContraMesas && cbxTipoPedido.Text == "0 - Entrega")
                        {
                            prepareToPrint();
                        }
                        else if (!ContraMesas)
                        {
                            prepareToPrint();
                        }
                        // Fecha o Formulario 
                        this.Close();
                    }
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show("Não foi possivel gravar o pedido " + erro.Message);
            }
        }


        private void gridViewItemsPedido_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.SelectedRows.Count > 0)
            {
                rowIndex = dgv.CurrentRow.Index;

                int codItem = int.Parse(this.gridViewItemsPedido.Rows[rowIndex].Cells[0].Value.ToString());
                var itemCompleto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", codigoItemParaAlterar);
                string itemNome = this.gridViewItemsPedido.Rows[rowIndex].Cells[1].Value.ToString();

                string[] sabores = (this.gridViewItemsPedido.Rows[rowIndex].Cells[1].Value.ToString()).Split('/');
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
                    this.cbxProdutosGrid.Text = this.gridViewItemsPedido.Rows[rowIndex].Cells[1].Value.ToString();
                }

                codigoItemParaAlterar = int.Parse(this.gridViewItemsPedido.Rows[rowIndex].Cells[0].Value.ToString());

                this.txtQuantidade.Text = this.gridViewItemsPedido.Rows[rowIndex].Cells[2].Value.ToString();
                this.txtPrecoUnitario.Text = this.gridViewItemsPedido.Rows[rowIndex].Cells[3].Value.ToString();
                this.txtPrecoTotal.Text = this.gridViewItemsPedido.Rows[rowIndex].Cells[4].Value.ToString();
                this.txtItemDescricao.Text = this.gridViewItemsPedido.Rows[rowIndex].Cells[5].Value.ToString();
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
            ValorTotal = 0;
            ValorTroco = 0;
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

                        var pedido = new Pedido()
                        {
                            Codigo = codPedido,
                            TotalPedido = decimal.Parse(lbTotal.Text.Replace("R$", "")),
                            TrocoPara = this.txtTrocoPara.Text,
                            FormaPagamento = this.cmbFPagamento.Text,
                            RealizadoEm = DateTime.Now,
                            Tipo = cbxTipoPedido.Text,

                        };
                        if (ContraMesas)
                        {
                            pedido.Tipo = cbxTipoPedido.Text;
                            pedido.NumeroMesa = int.Parse(cbxListaMesas.Text);
                            pedido.PedidoOrigem = pedido.PedidoOrigem;
                        }
                        else
                        {
                            pedido.Tipo = "Entrega";
                            //   pedido.NumeroMesa = null;
                        }

                        this.gridViewItemsPedido.Rows[rowIndex].Cells[1].Value = itemPedido.NomeProduto;
                        this.gridViewItemsPedido.Rows[rowIndex].Cells[2].Value = itemPedido.Quantidade;
                        this.gridViewItemsPedido.Rows[rowIndex].Cells[3].Value = "R$ " + itemPedido.PrecoUnitario.ToString();
                        this.gridViewItemsPedido.Rows[rowIndex].Cells[4].Value = "R$ " + itemPedido.PrecoTotal.ToString();
                        this.gridViewItemsPedido.Rows[rowIndex].Cells[5].Value = itemPedido.Item.ToString();

                        var ValorPedidoTotal = ValorTotal + decimal.Parse(lblEntrega.Text);

                        this.lbTotal.Text = "R$ " + ValorPedidoTotal;
                        this.lblTroco.Text = "R$ " + TrocoPagar;

                        con.Update("spAlterarItemPedido", itemPedido);
                        con.Update("spAlterarTotalPedido", pedido);

                        Utils.PopularGrid("Pedido", parentWindow.pedidosGridView);
                        Utils.ControlaEventos("Alterar", this.Name);

                        this.cbxProdutosGrid.Text = "";
                        this.txtPrecoUnitario.Text = "";
                        this.txtQuantidade.Text = "";
                        this.txtPrecoTotal.Text = "";
                        this.txtItemDescricao.Text = "";

                        if (ContraMesas && cbxListaMesas.Visible)
                        {
                            int CodigoMesa = Utils.RetornaCodigoMesa(cbxListaMesas.Text);
                            Utils.AtualizaMesa( cbxListaMesas.Text, 2);
                        }

                        MessageBox.Show("Pedido alterado com sucesso.", "DexPedidos");
                    }
                }
            }
            else
            {
                ItemPedido ItemPedido = new ItemPedido();
                Pedido pedido = new Pedido();
                foreach (ItemPedido  item in items)
                {
                    ValorTotal = item.PrecoTotal;
                }
                con.Update("spAlterarItemPedido", ItemPedido);
                con.Update("spAlterarTotalPedido", pedido);

                Utils.PopularGrid("Pedido", parentWindow.pedidosGridView);
                Utils.ControlaEventos("Alterar", this.Name);
            }
            //  }
        }

        private void ExcluirItem(object sender, EventArgs e)
        {
            if (gridViewItemsPedido.SelectedRows.Count > 0)
            {
                rowIndex = this.gridViewItemsPedido.CurrentRow.Index;

                var itemPedido = new ItemPedido()
                {
                    CodProduto = codigoItemParaAlterar,
                    CodPedido = codPedido
                };

                ValorTotal = ValorTotal - decimal.Parse(((this.gridViewItemsPedido.Rows[rowIndex].Cells[4].Value.ToString()).Replace("R$ ", "")));
                this.lbTotal.Text = "R$ " + Convert.ToString(ValorTotal + decimal.Parse(lblEntrega.Text));
                this.gridViewItemsPedido.Rows.RemoveAt(rowIndex);
                items.RemoveAt(rowIndex);

                this.txtPrecoUnitario.Text = "";
                this.txtQuantidade.Text = "";
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
                    pedido.NumeroMesa = CodMesa;
                    Utils.AtualizaMesa( cbxListaMesas.Text, 2);
                }
                con.Delete("spExcluirItemPedido", itemPedido);
                con.Update("spAlterarTotalPedido", pedido);
                Utils.PopularGrid("Pedido", parentWindow.pedidosGridView);
                Utils.ControlaEventos("Excluir", this.Name);
                MessageBox.Show("Item excluído com sucesso.", "DexPedido");
            }
            else
            {
                MessageBox.Show("Selecione o produto para alterar", "Aviso");
            }

        }

        public void atualizarGrid(ItemPedido itemDoPedido)
        {

            var troco = this.txtTrocoPara.Text.ToString();

            ValorTotal = 0;
            ValorTroco = 0;

            if (txtDesconto.Text !="0,00")
            {
                ValorTotal = dTotalPedido;
            }
            else
            {

                foreach (ItemPedido item in items)
                {
                    ValorTotal += item.PrecoTotal;
                }
            }
          

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Codigo", typeof(string)));
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

                row["Codigo"] = items[i].CodProduto;
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
            this.lbTotal.Text = "R$ " + Convert.ToString(ValorTotal + Convert.ToDecimal(lblEntrega.Text));
            this.lblTroco.Text = Convert.ToString(lblTroco.Text);
        }

        private void prepareToPrint()
        {
            try
            {
                printFont = new Font("Arial", int.Parse(TamanhoFont));
                printFontCozinha = new Font("Arial", int.Parse(TamanhoFont));
                PrintDocument pd = new PrintDocument();


                if (ContraMesas && cbxTipoPedido.Text == "1 - Mesa" || cbxTipoPedido.Text =="2 - Balcao" )
                {
                    for (int i = 0; i < QtdViasBalcao; i++)
                    {
                        ImprimePedidoMesa();
                        // ImpressaoCozinha();


                        // Configuração para imprimir em Impressoras LPT1
                        if (ImprimeLPT)
                        {
                            SerialPort porta = new SerialPort(PortaImpressa);
                            porta.Open();
                            if (porta.IsOpen)
                            {
                                porta.WriteLine(line);
                                porta.Close();
                            }
                            else
                            {
                                // Tenta novamente
                                prepareToPrint();
                            }
                        }
                        else
                        {
                            pd.PrintPage += new PrintPageEventHandler(this.ImprimirPedidoMesa);
                            pd.Print();
                            pd.PrintPage -= new PrintPageEventHandler(this.ImprimirPedidoMesa);
                        }
                    }


                }


                // Imprimindo via Entrega
                if (ImprimeViaEntrega && cbxTipoPedido.Text == "0 - Entrega")
                {
                    for (int i = 0; i < QtViasEntrega; i++)
                    {
                        ImpressaoEntrega();
                        if (ImprimeLPT)
                        {
                            Utils.ImpressaoSerial(line, PortaImpressa, 115200);
                        }
                        else
                        {
                            pd.PrintPage += new PrintPageEventHandler(this.imprimirViaEntrega);
                            pd.Print();
                            pd.PrintPage -= new PrintPageEventHandler(this.imprimirViaEntrega);
                        }
                    }

                }
                if (ImprimeViaCozinha)
                {
                    for (int i = 0; i < QtdViasCozinha; i++)
                    {
                        ImpressaoCozinha();
                        if (ImprimeLPT)
                        {
                            SerialPort porta = new SerialPort(PortaImpressa);
                            porta.Open();
                            if (porta.IsOpen)
                            {
                                porta.WriteLine(line);
                                porta.Close();
                            }
                            //Utils.ImpressaoLPT1(line, PortaImpressa);
                        }
                        else
                        {
                            pd.PrintPage += new PrintPageEventHandler(this.imprimirViaCozinha);
                            pd.Print();
                            pd.PrintPage -= new PrintPageEventHandler(this.imprimirViaCozinha);
                        }
                    }

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
        private void ImprimePedidoMesa()
        {
            int i = 0;

            int TempoPermanencia = DateTime.Now.Hour - parentWindow.DataPedido.Hour;
            ValorTotal = 0;
            foreach (ItemPedido item in items)
            {
                if (i == 0)
                {
                    line = QuebrarString(Sessions.returnEmpresa.Nome);
                    line += QuebrarString(Sessions.returnEmpresa.Telefone);
                    line += QuebrarString(Sessions.returnEmpresa.Telefone2);
                    line += QuebrarString(Sessions.returnEmpresa.Endereco + ", " + Sessions.returnEmpresa.Cidade + " - " + Sessions.returnEmpresa.Bairro);
                    line += QuebrarString("------------------------------------------");
                    line += QuebrarString(" ******** NÃO VALE COMO DOCUMENTO FISCAL **********");

                    line += QuebrarString(" Pedido gerado ás: " + DateTime.Now.ToShortTimeString());
                    if (codPedido.ToString() == "0")
                    {
                        line += "Documento Nº " + con.getLastCodigo() + "\r\n";
                    }
                    else
                    {
                        line += "Documento Nº " + codPedido + "\r\n";
                    }
                    line += QuebrarString("Mesa:" + cbxListaMesas.Text);
                    line += QuebrarString("-------------------------------------");
                    line += QuebrarString("PRODUTOS DO PEDIDO");

                    foreach (ItemPedido ItemsPedi in items)
                    {
                        line += QuebrarString(ItemsPedi.NomeProduto.ToString());
                        line += QuebrarString(ItemsPedi.PrecoUnitario.ToString());
                        line += QuebrarString(ItemsPedi.Quantidade.ToString());

                        if (ItemsPedi.Item != null && ItemsPedi.Item !="")
                        {
                            line += QuebrarString(ItemsPedi.Item.ToString());
                        }

                        ValorTotal = ValorTotal + ItemsPedi.PrecoTotal;

                    }
                    if (txtDesconto.Text!="0,00")
                    {
                      line += QuebrarString("Sub Total:" + ValorTotal);
                      line += QuebrarString("Desconto :" + txtDesconto.Text);
                      line += QuebrarString("Total Pagar:" + lbTotal.Text); 
                    }
                    else
                    {
                        line += QuebrarString("Total Pedido:" + ValorTotal);
                    }
                    
                    if (ImprimeLPT)
                    {
                        Utils.CriaArquivoTxt("Impressao", line);
                    }

                    i++;

                }
            }
        }

        private void ImpressaoCozinha()
        {
            line = null;
            int i = 0;

            foreach (ItemPedido item in items)
            {

                if (i == 0)
                {
                    if (codPedido.ToString() == "0")
                    {
                        line = "X COMMANDA - COZINHA - Nº " + con.getLastCodigo() + "\r\n";
                    }
                    else
                    {
                        line = "X COMMANDA - COZINHA - Nº " + codPedido + "\r\n";
                    }
                    line += QuebrarString("Pedido tipo:" + cbxTipoPedido.Text);
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

                    Atualiza.CodPedido = codPedido;
                    Atualiza.CodProduto = item.CodProduto;
                    Atualiza.ImpressoSN = true;

                    con.Update("spInformaItemImpresso", Atualiza);
                }


                //ev.Graphics.DrawString(line, printFontCozinha, Brushes.Black, 0, 0);
                //ev.HasMorePages = false;
                i++;
                if (ImprimeLPT)
                {
                    Utils.CriaArquivoTxt("Impressao", line);
                }

                // MP2032.BMP2032.AcionaGuilhotina(1);
            }
            //ev.HasMorePages = false;

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

        private void ImpressaoEntrega()
        {
            int i = 0;
          
            foreach (ItemPedido item in items)
            {
                string strLinhaCodigo, strLinhaDataHora;
                if (i == 0)
                {
                    strLinhaDataHora = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "\r\n\n";

                    if (Sessions.returnEmpresa.CNPJ == "21207218000191")
                    {
                        line = QuebrarString(Sessions.returnEmpresa.Nome);
                    }
                    else
                    {
                        line = QuebrarString(Sessions.returnEmpresa.Nome);
                        line += QuebrarString(Sessions.returnEmpresa.Telefone + " / " + Sessions.returnEmpresa.Telefone2);
                        line += QuebrarString(Sessions.returnEmpresa.Endereco + ", " + Sessions.returnEmpresa.Cidade + " - " + Sessions.returnEmpresa.Bairro);

                    }

                    if (con.getLastCodigo().ToString() == "0")
                    {
                        strLinhaCodigo = "PEDIDO:" + codPedido;
                    }
                    else
                    {
                        strLinhaCodigo = "PEDIDO:" + con.getLastCodigo();
                    }
                    line += strLinhaCodigo + "   " + strLinhaDataHora;
                    if (ControlaFidelidade)
                    {
                        line += QuebrarString("Tick Fidelidade -" + NumeroPedidos + "/" + PedidosParaFidelidade);
                    }

                    line += "-----------------------------------------------------------\r\n";
                    line += "Nome  :";
                    line += QuebrarString(pCliente.Nome);
                    if (QuebrarString(pCliente.Telefone2.ToString()) != "0")
                    {
                        line += QuebrarString("Tel  : " + pCliente.Telefone.ToString());
                        line += QuebrarString("Tel  :" + pCliente.Telefone2.ToString());
                    }
                    else
                    {
                        line += "Tel :";
                        line += pCliente.Telefone.ToString();
                    }
                    line += QuebrarString("End.: " + pCliente.Endereco + QuebrarString("," + pCliente.Numero.ToString()));
                    if (pCliente.Observacao.ToString() != "")
                    {
                        line += "Obs:";
                        line += QuebrarString(pCliente.Observacao) + "\r\n";
                    }
                    line += QuebrarString("Bairro:" + pCliente.Bairro);
                    if (pCliente.PontoReferencia != "")
                    {
                        line += QuebrarString("REF.:");
                        line += QuebrarString(pCliente.PontoReferencia) + "\r\n";
                    }
                    if (pCliente.Observacao != "")
                    {
                        line += QuebrarString("Observa.:");
                        line += QuebrarString(pCliente.Observacao);
                    }

                    line += "-----------------------------------------------------------\r\n";

                }
                
                if (PromocaoDiasSemana && DiaDaPromocao.IndexOf(DiaDaSema) > 0 && FPPermiteDesconto)
                {
                    line += QuebrarString("**Preço Promocional**");
                    line += QuebrarString(item.NomeProduto.ToString() + "        " + "Quant." + item.Quantidade.ToString());
                    line += " R$: " + item.PrecoTotal + "\r\n";
                    if (item.Item != "")
                    {
                        line += " Obs:" + QuebrarString(item.Item.ToString()) + "\r\n";
                    }
                }
                else
                {
                    if (CNPJRETORNO == "18367525000125")
                    {
                        line += QuebrarString(item.NomeProduto.ToString());
                        line += "Quant." + QuebrarString(item.Quantidade.ToString());
                        line += " R$  : " + item.PrecoTotal + "\r\n";
                        if (item.Item != "")
                        {
                            line += " Obs:" + QuebrarString(item.Item.ToString());
                        }
                    }
                    else
                    {
                        ItemPedido itemPedidos = new ItemPedido();

                            item.PrecoTotal = decimal.Parse(gridViewItemsPedido.Rows[i].Cells[3].Value.ToString().Replace("R$", ""));
                            
                            line += QuebrarString(item.NomeProduto.ToString() + "        " + "Quant." + item.Quantidade.ToString());
                            line += " R$  : " + item.PrecoTotal + "\r\n";

                            if (item.Item != "")
                            {
                                line += " Obs:" + QuebrarString(item.Item.ToString());
                            }

                    }

                }
               
                // ev.HasMorePages = true;
                i++;
            }
            ValorTotal = 0;
            foreach (ItemPedido _item in items)
            {
                ValorTotal += _item.PrecoTotal*_item.Quantidade;
            }
            //decimal TotaItems = decimal.Parse(lbTotal.Text.Replace("R$", ""));
           // decimal TotalPago = TotaItems + ValorTroco;
            var TotalPedido = ValorTotal + decimal.Parse(lblEntrega.Text);
            line += "-----------------------------------------------------------\r\n";
            line += QuebrarString("Tx. Serv     :" + lblEntrega.Text);
            line += QuebrarString("Total Itens  :" + ValorTotal);
            line += QuebrarString("F. Pagamento :" + cmbFPagamento.Text);
            if (txtDesconto.Text!="0,00")
            {
              line += QuebrarString("Desconto   :" + txtDesconto.Text); 
            }
            line += QuebrarString("Total Pedido :" + TotalPedido);
            line += QuebrarString("Troco Para   :" + txtTrocoPara.Text + " - Troco:" + lblTroco.Text);
            if (ControlaFidelidade && lblFidelidade.Visible)
            {
                line += "-----------------------------------------------------------\r\n";
                line += QuebrarString(" Parabens você é nosso cliente Fiel , este e seu pedido N.:" + NumeroPedidos.ToString()) + "\r\n";
            }
            if (ControlaPrevisao)
            {
                line += QuebrarString("Previsão de Entrega:  " + DataPed.AddMinutes(TempPrevisto).ToShortTimeString()) + "\r\n";
            }
            line += "Obrigado Pela Preferencia \r\n\n";

            //ev.Graphics.DrawString(line, printFont, Brushes.Black, 0, 0);
            //ev.HasMorePages = false;
            if (ImprimeLPT)
            {
                Utils.CriaArquivoTxt("Impressao", line);
            }

            i++;

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
            if (debug == true)
            {
                prepareToPrint();
            }
        }

        private void cbxMeiaPizza_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbxMeiaPizza.Checked == true)
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
        public void ComparaValores()
        {
            if (Sessions.returnConfig.ProdutoPorCodigo == true)
            {
          
                var valorProduto = decimal.Parse("0.00");
                var valorSabor = decimal.Parse("0.00");
                DataTable produto;
                DataTable sabor;
               
                if (txtCodProduto1.Text!="")
                {
                     produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(txtCodProduto1.Text)).Tables["Produto"];
                }
                else
                {
                     produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];
                }

                if (txtCodProduto2.Text!="")
                {
                  sabor= con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(txtCodProduto2.Text)).Tables["Produto"]; 
                }
                else
                {
                    sabor = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxSabor.SelectedValue.ToString())).Tables["Produto"];
                }

   
                if (PromocaoDiasSemana)
                {
                    DiaDaPromocao = produto.Rows[0]["DiaSemana"].ToString();
                    lol = DiaDaPromocao.Split(new char[] { ';' });
                    if (DiaDaPromocao.IndexOf(DiaDaSema) > 0)
                    {
                        valorProduto = decimal.Parse(produto.Rows[0]["PrecoDesconto"].ToString());
                        valorSabor = decimal.Parse(produto.Rows[0]["PrecoDesconto"].ToString());
                    }
                    else
                    {
                        valorProduto = decimal.Parse(produto.Rows[0]["PrecoProduto"].ToString());
                        valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
                    }
                    
                    CalcularTotalItem();
                   
                }
                else
                {
                    valorProduto = decimal.Parse(produto.Rows[0]["PrecoProduto"].ToString());
                    valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
                }


                if (valorProduto > valorSabor)
                {
                    this.txtPrecoUnitario.Text = "R$ " + valorProduto;
                }
                else
                {
                    this.txtPrecoUnitario.Text = "R$ " + valorSabor;
                }
                
            }
            else if (Sessions.returnConfig.ProdutoPorCodigo == false && this.cbxSabor.Text != " ")
            {
                var valorProduto = decimal.Parse("0.00");
                var valorSabor = decimal.Parse("0.00");
                var produto = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxProdutosGrid.SelectedValue.ToString())).Tables["Produto"];
                var sabor = con.SelectProdutoCompleto("Produto", "spObterProdutoCompleto", int.Parse(this.cbxSabor.SelectedValue.ToString())).Tables["Produto"];

                DiaDaPromocao = produto.Rows[0]["DiaSemana"].ToString();
                lol = DiaDaPromocao.Split(new char[] { ';' });

                if (PromocaoDiasSemana && lol.Contains(DiaDaSema))
                {
                    valorProduto = decimal.Parse(produto.Rows[0]["PrecoProduto"].ToString());
                    valorSabor = decimal.Parse(sabor.Rows[0]["PrecoDesconto"].ToString());
                }
                else
                {
                    valorProduto = decimal.Parse(produto.Rows[0]["PrecoProduto"].ToString());
                    valorSabor = decimal.Parse(sabor.Rows[0]["PrecoProduto"].ToString());
                }

                if (valorProduto > valorSabor)
                {
                    this.txtPrecoUnitario.Text = "R$ " + valorProduto;
                }
                else
                {
                    this.txtPrecoUnitario.Text = "R$ " + valorSabor;
                }
               
            }
            // Calcula o preço total forçando a multiplicação
            txtPrecoTotal.Text = Convert.ToString(decimal.Parse(txtQuantidade.Text) * decimal.Parse(txtPrecoUnitario.Text.Replace("R$", "")));
        }

        internal void changeValorTotal(decimal p)
        {
            this.lbTotal.Text = p.ToString() + lblEntrega.Text;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizaTroco();
        }

        private Boolean AtualizaTroco()
        {
            decimal troco = 0.00M;
            bool Ok;
            decimal TotalPedido = decimal.Parse(lbTotal.Text.Replace("R$", ""));
            if (this.txtTrocoPara.Text != "")
            {
                troco = decimal.Parse(txtTrocoPara.Text);
            }
            if (cbxTipoPedido.Text!="1 - Mesa" && TotalPedido > troco && cbxTrocoParaOK.Checked == false)
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
                Utils.PopularGrid("Pedido", parentWindow.pedidosGridView);
                //   MessageBox.Show("Troco e/ou Forma de pagamento atualizados");
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

        private void frmCadastrarPedido_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void cmbFPagamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbFPagamento.Focused == true)
            {
                var fPagamento = con.SelectAll("FormaPagamento", "spObterFormaPagamento").Tables["FormaPagamento"];

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
                        cbxProdutosGrid.Text = produto.Rows[0]["NomeProduto"].ToString();
                        if (PromocaoDiasSemana)
                        {
                            DiaDaPromocao = produto.Rows[0]["DiaSemana"].ToString();
                            lol = DiaDaPromocao.Split(new char[] { ';' });
                            if (DiaDaPromocao.IndexOf(DiaDaSema) > 0)
                            {
                                txtPrecoUnitario.Text = "R$ " + produto.Rows[0]["PrecoDesconto"].ToString();
                            }
                            else
                            {
                                txtPrecoUnitario.Text = "R$ " + produto.Rows[0]["PrecoProduto"].ToString();
                            }
                        }
                        else
                        {
                            txtPrecoUnitario.Text = "R$ " + produto.Rows[0]["PrecoProduto"].ToString();
                        }
                      
                        this.txtQuantidade.Text = "1";
                        CalcularTotalItem();
                        this.txtQuantidade.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Produto não encontrado");
                    }
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
                        cbxSabor.Text = produto.Rows[0]["NomeProduto"].ToString();
                        if (PromocaoDiasSemana)
                        {
                            DiaDaPromocao = produto.Rows[0]["DiaSemana"].ToString();
                            lol = DiaDaPromocao.Split(new char[] { ';' });
                            if (DiaDaPromocao.IndexOf(DiaDaSema) > 0)
                            {
                                txtPrecoUnitario.Text = "R$ " + produto.Rows[0]["PrecoDesconto"].ToString();
                            }
                            else
                            {
                                txtPrecoUnitario.Text = "R$ " + produto.Rows[0]["PrecoProduto"].ToString();
                            }
                        }
                        else
                        {
                            txtPrecoUnitario.Text = "R$ " + produto.Rows[0]["PrecoProduto"].ToString();
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
            this.lblEndereco = new System.Windows.Forms.Label();
            this.lblNomeCliente = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dBExpertDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemsPedidoBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemsPedido)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
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
            this.cbxTipoProduto.Location = new System.Drawing.Point(74, 101);
            this.cbxTipoProduto.Name = "cbxTipoProduto";
            this.cbxTipoProduto.Size = new System.Drawing.Size(232, 26);
            this.cbxTipoProduto.TabIndex = 0;
            this.cbxTipoProduto.SelectedIndexChanged += new System.EventHandler(this.cbxTipoProduto_SelectedIndexChanged);
            // 
            // lblGrupo
            // 
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.Location = new System.Drawing.Point(11, 108);
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(39, 13);
            this.lblGrupo.TabIndex = 32;
            this.lblGrupo.Text = "Grupo:";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidade.Location = new System.Drawing.Point(75, 164);
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
            this.label1.Location = new System.Drawing.Point(4, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Quantidade:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Preço Unitário:";
            // 
            // txtPrecoUnitario
            // 
            this.txtPrecoUnitario.Enabled = false;
            this.txtPrecoUnitario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoUnitario.Location = new System.Drawing.Point(334, 164);
            this.txtPrecoUnitario.Name = "txtPrecoUnitario";
            this.txtPrecoUnitario.Size = new System.Drawing.Size(149, 26);
            this.txtPrecoUnitario.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(496, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Preço Total:";
            // 
            // txtPrecoTotal
            // 
            this.txtPrecoTotal.Enabled = false;
            this.txtPrecoTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoTotal.Location = new System.Drawing.Point(567, 163);
            this.txtPrecoTotal.Name = "txtPrecoTotal";
            this.txtPrecoTotal.Size = new System.Drawing.Size(142, 26);
            this.txtPrecoTotal.TabIndex = 5;
            // 
            // btnAdicionarItemNoPedido
            // 
            this.btnAdicionarItemNoPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAdicionarItemNoPedido.Location = new System.Drawing.Point(566, 196);
            this.btnAdicionarItemNoPedido.Name = "btnAdicionarItemNoPedido";
            this.btnAdicionarItemNoPedido.Size = new System.Drawing.Size(143, 43);
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
            this.btnCancelarPedido.Location = new System.Drawing.Point(411, 42);
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
            this.panel1.Controls.Add(this.lblTempo);
            this.panel1.Controls.Add(this.lblFidelidade);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 43);
            this.panel1.TabIndex = 41;
            // 
            // lblTempo
            // 
            this.lblTempo.AutoSize = true;
            this.lblTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTempo.Location = new System.Drawing.Point(357, 9);
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
            this.lblFidelidade.Location = new System.Drawing.Point(451, 5);
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
            this.lbTotal.Location = new System.Drawing.Point(331, 8);
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
            this.txbProduto.Location = new System.Drawing.Point(4, 141);
            this.txbProduto.Name = "txbProduto";
            this.txbProduto.Size = new System.Drawing.Size(47, 13);
            this.txbProduto.TabIndex = 44;
            this.txbProduto.Text = "Produto:";
            // 
            // cbxProdutosGrid
            // 
            this.cbxProdutosGrid.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxProdutosGrid.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxProdutosGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxProdutosGrid.FormattingEnabled = true;
            this.cbxProdutosGrid.Location = new System.Drawing.Point(74, 133);
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
            this.gridViewItemsPedido.Location = new System.Drawing.Point(74, 250);
            this.gridViewItemsPedido.Name = "gridViewItemsPedido";
            this.gridViewItemsPedido.ReadOnly = true;
            this.gridViewItemsPedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridViewItemsPedido.Size = new System.Drawing.Size(675, 124);
            this.gridViewItemsPedido.TabIndex = 47;
            this.gridViewItemsPedido.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridViewItemsPedido_CellMouseClick);
            this.gridViewItemsPedido.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridViewItemsPedido_MouseClick);
            // 
            // lblTrocoPara
            // 
            this.lblTrocoPara.AutoSize = true;
            this.lblTrocoPara.Location = new System.Drawing.Point(92, 7);
            this.lblTrocoPara.Name = "lblTrocoPara";
            this.lblTrocoPara.Size = new System.Drawing.Size(62, 13);
            this.lblTrocoPara.TabIndex = 48;
            this.lblTrocoPara.Text = "Troco para:";
            // 
            // txtTrocoPara
            // 
            this.txtTrocoPara.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtTrocoPara.Location = new System.Drawing.Point(92, 23);
            this.txtTrocoPara.Name = "txtTrocoPara";
            this.txtTrocoPara.Size = new System.Drawing.Size(73, 26);
            this.txtTrocoPara.TabIndex = 6;
            this.txtTrocoPara.Text = "0,00";
            this.txtTrocoPara.TextChanged += new System.EventHandler(this.txtTrocoPara_TextChanged);
            this.txtTrocoPara.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTrocoPara_KeyPress);
            // 
            // lblFormaDePagamento
            // 
            this.lblFormaDePagamento.AutoSize = true;
            this.lblFormaDePagamento.Location = new System.Drawing.Point(422, 5);
            this.lblFormaDePagamento.Name = "lblFormaDePagamento";
            this.lblFormaDePagamento.Size = new System.Drawing.Size(111, 13);
            this.lblFormaDePagamento.TabIndex = 50;
            this.lblFormaDePagamento.Text = "Forma de Pagamento:";
            // 
            // txtItemDescricao
            // 
            this.txtItemDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtItemDescricao.Location = new System.Drawing.Point(74, 196);
            this.txtItemDescricao.Multiline = true;
            this.txtItemDescricao.Name = "txtItemDescricao";
            this.txtItemDescricao.Size = new System.Drawing.Size(480, 43);
            this.txtItemDescricao.TabIndex = 3;
            // 
            // lblDescricaoDoItem
            // 
            this.lblDescricaoDoItem.AutoSize = true;
            this.lblDescricaoDoItem.Location = new System.Drawing.Point(40, 204);
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
            this.btnReimprimir.Location = new System.Drawing.Point(284, 42);
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
            this.panel2.Controls.Add(this.lblEntrega);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.lblTroco);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.lbTotal);
            this.panel2.Controls.Add(this.btnCancelarPedido);
            this.panel2.Controls.Add(this.lbTotalPedido);
            this.panel2.Controls.Add(this.btnReimprimir);
            this.panel2.Controls.Add(this.btnGerarPedido);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 440);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(754, 82);
            this.panel2.TabIndex = 42;
            // 
            // lblEntrega
            // 
            this.lblEntrega.AutoSize = true;
            this.lblEntrega.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblEntrega.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblEntrega.Location = new System.Drawing.Point(146, 8);
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
            this.lblTroco.Location = new System.Drawing.Point(607, 8);
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
            this.label5.Location = new System.Drawing.Point(542, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 20);
            this.label5.TabIndex = 55;
            this.label5.Text = "Troco:";
            // 
            // cbxMeiaPizza
            // 
            this.cbxMeiaPizza.AutoSize = true;
            this.cbxMeiaPizza.Location = new System.Drawing.Point(570, 137);
            this.cbxMeiaPizza.Name = "cbxMeiaPizza";
            this.cbxMeiaPizza.Size = new System.Drawing.Size(115, 17);
            this.cbxMeiaPizza.TabIndex = 54;
            this.cbxMeiaPizza.Text = "Pizza de 2 sabores";
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
            this.cbxSabor.Location = new System.Drawing.Point(335, 133);
            this.cbxSabor.Name = "cbxSabor";
            this.cbxSabor.Size = new System.Drawing.Size(225, 26);
            this.cbxSabor.TabIndex = 55;
            this.cbxSabor.SelectionChangeCommitted += new System.EventHandler(this.cbxSabor_SelectionChangeCommitted);
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAtualizar.Location = new System.Drawing.Point(609, 19);
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
            this.panel3.Location = new System.Drawing.Point(74, 380);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(675, 55);
            this.panel3.TabIndex = 57;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(249, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "Desconto R$";
            // 
            // txtDesconto
            // 
            this.txtDesconto.Enabled = false;
            this.txtDesconto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtDesconto.Location = new System.Drawing.Point(252, 23);
            this.txtDesconto.Name = "txtDesconto";
            this.txtDesconto.Size = new System.Drawing.Size(73, 26);
            this.txtDesconto.TabIndex = 57;
            this.txtDesconto.Text = "0,00";
            this.txtDesconto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CalculaDesconto);
            // 
            // cmbFPagamento
            // 
            this.cmbFPagamento.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbFPagamento.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.cmbFPagamento.FormattingEnabled = true;
            this.cmbFPagamento.Location = new System.Drawing.Point(424, 24);
            this.cmbFPagamento.Name = "cmbFPagamento";
            this.cmbFPagamento.Size = new System.Drawing.Size(175, 26);
            this.cmbFPagamento.TabIndex = 7;
            this.cmbFPagamento.SelectedIndexChanged += new System.EventHandler(this.cmbFPagamento_SelectedIndexChanged);
            this.cmbFPagamento.SelectionChangeCommitted += new System.EventHandler(this.cmbFPagamento_SelectionChangeCommitted);
            // 
            // cbxTrocoParaOK
            // 
            this.cbxTrocoParaOK.AutoSize = true;
            this.cbxTrocoParaOK.Location = new System.Drawing.Point(18, 26);
            this.cbxTrocoParaOK.Name = "cbxTrocoParaOK";
            this.cbxTrocoParaOK.Size = new System.Drawing.Size(65, 17);
            this.cbxTrocoParaOK.TabIndex = 5;
            this.cbxTrocoParaOK.Text = "S/ troco";
            this.cbxTrocoParaOK.UseVisualStyleBackColor = true;
            this.cbxTrocoParaOK.CheckedChanged += new System.EventHandler(this.cbxTrocoParaOK_CheckedChanged);
            // 
            // btnReceita
            // 
            this.btnReceita.Location = new System.Drawing.Point(312, 135);
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
            this.txtCodProduto1.Location = new System.Drawing.Point(74, 101);
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
            this.txtCodProduto2.Location = new System.Drawing.Point(335, 100);
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
            this.cbxTipoPedido.Location = new System.Drawing.Point(429, 100);
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
            this.cbxListaMesas.Location = new System.Drawing.Point(566, 100);
            this.cbxListaMesas.Name = "cbxListaMesas";
            this.cbxListaMesas.Size = new System.Drawing.Size(42, 26);
            this.cbxListaMesas.TabIndex = 62;
            this.cbxListaMesas.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.lblEndereco);
            this.panel4.Controls.Add(this.lblNomeCliente);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 43);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(754, 43);
            this.panel4.TabIndex = 63;
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
            // frmCadastrarPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(754, 522);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmCadastrarPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[XDelivery ] Cadastrar Pedido";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCadastrarPedido_FormClosed);
            this.Load += new System.EventHandler(this.frmCadastrarPedido_Load);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void txtQuantidade_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalItem();

        }

        private void CalcularTotalItem()
        {
            if (txtQuantidade.Text != "" && txtPrecoUnitario.Text != "")
            {
                var precoUnitario = decimal.Parse(this.txtPrecoUnitario.Text.ToString().Replace("R$ ", ""));
                var quantidade = int.Parse(this.txtQuantidade.Text);
                var total = precoUnitario * quantidade;
                this.txtPrecoTotal.Text = "R$ " + total.ToString();
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
            if (cbxTipoPedido.Text == "1 - Mesa")
            {
                cbxListaMesas.Visible = true;
                cbxListaMesas.Focus();
            }
            else
            {
                cbxListaMesas.Visible = false;
            }
        }

        private void cbxTrocoParaOK_CheckedChanged(object sender, EventArgs e)
        {
            txtTrocoPara.Enabled = !cbxTrocoParaOK.Checked;
        }

        private void cbxSabor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComparaValores();
        }

        private void frmCadastrarPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12 && btnGerarPedido.Text == "Gerar [F12]")
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
            decimal TotalPedido = ValorTotal;
            if (Utils.SoDecimais(e))
            {
                if (decimal.Parse(txtDesconto.Text) < TotalPedido)
                {
                    if ((txtDesconto.Text != "") && (e.KeyChar == Convert.ToChar(Keys.Enter)))
                    {
                        TotalPedido = TotalPedido - decimal.Parse(txtDesconto.Text);
                        txtDesconto.Text = string.Format("{0:#,##0.00}", decimal.Parse(txtDesconto.Text));
                        lbTotal.Text = "R$" + TotalPedido.ToString();
                        AtualizaTroco();

                    }
                    
                }
                else
                {
                    MessageBox.Show("Desconto não pode ser maior que o total do pedido", "Aviso");
                    return;
                }
                
            }
        }

        private void cmbFPagamento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Decimal ValorProduto = 0.00M;
          
            if (PromocaoDiasSemana)
            {
                DataRow rows;
                DataSet DsFP;
                int CodFP = int.Parse(cmbFPagamento.SelectedValue.ToString());
                DsFP = new DataSet();
                DsFP = con.SelectRegistroPorCodigo("FormaPagamento", "spObterFPPorCodigo", CodFP);
                rows = DsFP.Tables[0].Rows[0];
                FPPermiteDesconto = Convert.ToBoolean(rows.ItemArray.GetValue(2).ToString());

                DataSet dsProduto;
                DataRow RowsProduto;
               
                Decimal TotalAtualizado = 0;
                int iCodProduto = 0;

                if (!FPPermiteDesconto)
                {
                   
                    for (int i = 0; i < gridViewItemsPedido.Rows.Count; i++)
                    {
                        iCodProduto = int.Parse(gridViewItemsPedido.Rows[i].Cells[0].Value.ToString());
                        dsProduto = con.SelectRegistroPorCodigo("Produto", "spObterProdutoPorCodigo", iCodProduto);
                        RowsProduto = dsProduto.Tables[0].Rows[0];
                        ValorProduto = decimal.Parse(RowsProduto.ItemArray.GetValue(1).ToString());

                        gridViewItemsPedido.Rows[i].Cells[3].Value = ValorProduto;
                        gridViewItemsPedido.Rows[i].Cells[4].Value = decimal.Parse(gridViewItemsPedido.Rows[i].Cells[2].Value.ToString()) * ValorProduto;
                        decimal TotalLinha = decimal.Parse(gridViewItemsPedido.Rows[i].Cells[4].Value.ToString());

                        TotalAtualizado += TotalLinha;
  
                    }

                }
                else 
                {
                    for (int intFor = 0; intFor < gridViewItemsPedido.Rows.Count; intFor++)
                    {
                        iCodProduto = int.Parse(gridViewItemsPedido.Rows[intFor].Cells[0].Value.ToString());
                        dsProduto = con.SelectRegistroPorCodigo("Produto", "spObterProdutoPorCodigo", iCodProduto);
                        RowsProduto = dsProduto.Tables[0].Rows[0];
                        if (RowsProduto.ItemArray.GetValue(5).ToString()!="0,00")
                        {
                           ValorProduto = decimal.Parse(RowsProduto.ItemArray.GetValue(5).ToString());
                        }
                        else
                        {
                            ValorProduto = decimal.Parse(RowsProduto.ItemArray.GetValue(1).ToString());
                        }
                        

                        gridViewItemsPedido.Rows[intFor].Cells[3].Value = ValorProduto;
                        gridViewItemsPedido.Rows[intFor].Cells[4].Value = decimal.Parse(gridViewItemsPedido.Rows[intFor].Cells[2].Value.ToString()) * ValorProduto;
                        decimal TotalLinha = decimal.Parse(gridViewItemsPedido.Rows[intFor].Cells[4].Value.ToString());
                        TotalAtualizado += TotalLinha;
                    }

                }

                lbTotal.Text = "R$ " + Convert.ToString(TotalAtualizado + Convert.ToDecimal(lblEntrega.Text));
            }
            
        }

    }
}
