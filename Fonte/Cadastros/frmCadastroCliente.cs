using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DexComanda.Models;
using DexComanda.Integração;

namespace DexComanda
{
    public partial class frmCadastroCliente : Form
    {
        private Conexao con;
        private Main parentMain;
        private Pessoa cliente;
        private int codigoClienteParaAlterar;
        private DataSet endereco;
        private string CidadePadrao;
        private string EstadoPadrao;
        private DataRow RowsClientes;
        private int mCodRegiao;


        public frmCadastroCliente(Main parent)
        {
            InitializeComponent();
            this.parentMain = parent;
            CarregaRegiao("");

            // ObterCidadePadrao();
            //ObterCidadePadrao();
        }

        public frmCadastroCliente(Pessoa cli, Main parent)
        {
            InitializeComponent();
            this.parentMain = parent;
            this.cliente = cli;
            txtDataNascimento.Text = cli.DataNascimento.ToString();
            txtDataCadastro.Text = cli.DataCadastro.ToString();
            txtNomeCliente.Focus();
            txtNomeCliente.Select();
            //ObterCidadePadrao();
            // ObterCidadePadrao();
        }

        private void frmCadastroCliente_Load(object sender, EventArgs e)
        {

            con = new Conexao();
            dtLancamento.Value = dtFim.Value = dtInicio.Value = DateTime.Now;
            txtNomeCliente.Focus();
            txtNomeCliente.Select();
            ObterCidadePadrao();


            if (Sessions.returnConfig != null)
            {
                lblDataNascimento.Enabled = txtDataNascimento.Enabled = Sessions.returnConfig.UsaDataNascimento;
                lblTelefone2.Enabled = txtTelefone2.Visible = Sessions.returnConfig.Usa2Telefones;
            }
            if (cliente != null)
            {
                if (Sessions.returnConfig != null)
                {
                    if (Sessions.returnConfig.UsaDataNascimento)
                    {
                        this.txtDataNascimento.Enabled = true;

                    }
                    else
                    {
                        this.txtDataNascimento.Enabled = false;
                    }

                }

                this.btnAdicionarCliente.Text = "Alterar [F12]";
                this.btnAdicionarCliente.Click -= AdicionarCliente;
                this.btnAdicionarCliente.Click += AlterarCliente;

                codigoClienteParaAlterar = cliente.Codigo;
                DataSet pessoa = con.SelectPessoaPorCodigo("Pessoa", "spObterPessoaPorCodigo", codigoClienteParaAlterar);
                if (pessoa.Tables["Pessoa"].Rows.Count > 0)
                {
                    RowsClientes = pessoa.Tables["Pessoa"].Rows[0];

                    this.txtNomeCliente.Text = RowsClientes.ItemArray.GetValue(1).ToString();
                    this.txtCEP.Text = RowsClientes.ItemArray.GetValue(2).ToString();
                    this.txtEndereco.Text = RowsClientes.ItemArray.GetValue(3).ToString();
                    this.txtBairro.Text = RowsClientes.ItemArray.GetValue(4).ToString();
                    this.txtCidade.Text = RowsClientes.ItemArray.GetValue(5).ToString();
                    this.txtEstado.Text = RowsClientes.ItemArray.GetValue(6).ToString();
                    this.txtPontoReferencia.Text = RowsClientes.ItemArray.GetValue(7).ToString();
                    this.txtObservacaoCliente.Text = RowsClientes.ItemArray.GetValue(8).ToString();
                    this.txtNumero.Text = RowsClientes.ItemArray.GetValue(9).ToString();
                    this.txtTelefone.Text = Convert.ToString(RowsClientes.ItemArray.GetValue(10).ToString());
                    this.cbxRegiao.Text = RowsClientes.ItemArray.GetValue(14).ToString();

                    if (txtTelefone2.Visible == true)
                    {
                        txtTelefone2.Text = RowsClientes.ItemArray.GetValue(11).ToString();
                    }
                    //Carrega as Regioes de Entrega previamente cadastradas // 
                    CarregaRegiao(cbxRegiao.Text);
                }

            }
        }

        private void CarregaRegiao(string iCodRegiacao)
        {
            if (iCodRegiacao == "")
            {
                Conexao con = new Conexao();

                this.cbxRegiao.DataSource = con.SelectAll("RegiaoEntrega", "spObterRegioes").Tables["RegiaoEntrega"];
                this.cbxRegiao.DisplayMember = "NomeRegiao";
                this.cbxRegiao.ValueMember = "Codigo";
            }
            else
            {
                var Regiao = con.SelectRegistroPorCodigo("RegiaoEntrega", "spObterRegioesPorCodigo", int.Parse(iCodRegiacao));
                DataRow Lista = Regiao.Tables["RegiaoEntrega"].Rows[0];

                mCodRegiao = int.Parse(Lista.ItemArray.GetValue(0).ToString());
                txtTaxaEntrega.Text = Lista.ItemArray.GetValue(2).ToString();

                cbxRegiao.Text = Lista.ItemArray.GetValue(3).ToString();
            }




        }

        private void ConsultarEnderecoPorCep(object sender, KeyEventArgs e)
        {


            if (txtCEP.Text.Length == 8)
            {
                ObterCidadePadrao();
                if (this.txtCEP.Text.Equals(""))
                {
                    MessageBox.Show("Informe o Cep.");
                }
                else
                {

                    int cep = int.Parse(this.txtCEP.Text);
                    endereco = con.SelectEnderecoPorCep("base_cep", "spObterEnderecoPorCep", cep);

                    if (endereco.Tables["base_cep"].Rows.Count > 0)
                    {
                        // ObterCidadePadrao();
                        DataRow dRow = endereco.Tables["base_cep"].Rows[0];

                        this.txtEndereco.Text = dRow.ItemArray.GetValue(0).ToString();
                        this.txtBairro.Text = dRow.ItemArray.GetValue(1).ToString();
                        //  this.txtCidade.Text = CidadePadrao;//dRow.ItemArray.GetValue(2).ToString();
                        //this.txtEstado.Text = EstadoPadrao;//dRow.ItemArray.GetValue(3).ToString();
                        this.txtNumero.Focus();
                    }
                    else
                    {
                        // ObterCidadePadrao();
                        MessageBox.Show("Cep não encontrado");

                        this.txtEndereco.Focus();
                    }
                }
            }

        }

        private void AdicionarCliente(object sender, EventArgs e)
        {
            try
            {
                string _cep = "0";
                if (this.txtCEP.Text.ToString() != "")
                {
                    _cep = this.txtCEP.Text;
                }
                else
                {
                    _cep = "290";
                }

                Pessoa pessoa = new Pessoa()
                {
                    Codigo = 0,
                    Nome = this.txtNomeCliente.Text,
                    Endereco = this.txtEndereco.Text,
                    Bairro = this.txtBairro.Text,
                    Cidade = this.txtCidade.Text,
                    Cep = _cep,
                    PontoReferencia = this.txtPontoReferencia.Text,
                    Observacao = this.txtObservacaoCliente.Text,
                    Telefone = this.txtTelefone.Text,
                    Telefone2 = txtTelefone2.Text,
                    UF = this.txtEstado.Text,
                    TicketFidelidade = 0,
                    CodRegiao = int.Parse(this.cbxRegiao.SelectedValue.ToString()),
                    DataCadastro = DateTime.Now

                };
                if (txtDataNascimento.Visible && txtDataNascimento.Text == "  /  /")
                {
                    //pessoa.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text);
                    pessoa.DataNascimento = Convert.ToDateTime("01/01/1950" + " " + "23:58:00");
                }
                else if (txtDataNascimento.Visible && txtDataNascimento.Text != "  /  /")
                {
                    pessoa.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text + " " + "23:58:00");
                }
                else if (!txtDataNascimento.Visible)
                {
                    pessoa.DataNascimento = Convert.ToDateTime("01/01/1950" + " " + "23:58:00");
                }

                if (txtNumero.Text != "")
                {
                    pessoa.Numero = txtNumero.Text;
                    con.Insert("spAdicionarClienteDelivery", pessoa);
                    Utils.ControlaEventos("Inserir", this.Name);
                    MessageBox.Show("Cliente cadastrado com sucesso.", "Dex Aviso", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    this_FormClosing();
                    if (Utils.CaixaAberto(DateTime.Now, Sessions.retunrUsuario.CaixaLogado))
                    {
                        RealizarPedidoAgora(Convert.ToString(pessoa.Telefone));
                    }

                }
                else
                {
                    MessageBox.Show("Numero não pode ser vazio");
                    txtNumero.Focus();
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("Registro não foi gravado , favor verificar os campos digitados " + err.Message);
            }

        }

        private void RealizarPedidoAgora(string telefone)
        {
            try
            {
                DialogResult resultado = MessageBox.Show("Deseja realizar um Pedido para pessoa cadastrada?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    DBExpertDataSet dbExpert = new DBExpertDataSet();
                    DataSet pessoaTelefone = con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", telefone);
                    if ((pessoaTelefone.Tables["Pessoa"].Rows.Count == 0))
                    {

                    }
                    else
                    {
                        DataSet Pessoa = con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", telefone);
                        DataRow dRow = Pessoa.Tables["Pessoa"].Rows[0];

                        int iCodPessoa = int.Parse(dRow.ItemArray.GetValue(0).ToString());
                        this.parentMain.txtNome.Text = dRow.ItemArray.GetValue(1).ToString();
                        this.parentMain.txtEndereco.Text = dRow.ItemArray.GetValue(2).ToString();
                        this.parentMain.txtBairro.Text = dRow.ItemArray.GetValue(3).ToString();
                        this.parentMain.txtCidade.Text = dRow.ItemArray.GetValue(4).ToString();
                        this.parentMain.txtPontoReferencia.Text = dRow.ItemArray.GetValue(5).ToString();

                        var TaxaEntrega = Utils.RetornaTaxaPorCliente(iCodPessoa, con);
                        frmCadastrarPedido frmCadastrarPedido = new frmCadastrarPedido(false, "0,00", "", "", TaxaEntrega, false, DateTime.Now, 0, int.Parse(dRow.ItemArray.GetValue(0).ToString()),
                                                                                       "", "", "", "", this.parentMain, 0.00M);
                        frmCadastrarPedido.ShowDialog();
                    }
                }
            }
            catch (Exception tr)
            {

                MessageBox.Show(tr.Message, "DexCommanda");
            }
        }

        private void AlterarCliente(object sender, EventArgs e)
        {
            try
            {
                //int _cep = int.Parse(this.txtCEP.Text);
                string _cep = "0";
                if (this.txtCEP.Text.ToString() != "")
                {
                    _cep = this.txtCEP.Text.ToString();
                }
                else
                {
                    _cep = "29000000";
                }

                Pessoa pessoa = new Pessoa()
                {
                    Codigo = codigoClienteParaAlterar,
                    Nome = this.txtNomeCliente.Text,
                    Endereco = this.txtEndereco.Text,
                    Numero = this.txtNumero.Text,
                    Bairro = this.txtBairro.Text,
                    Cidade = this.txtCidade.Text,
                    Cep = _cep,
                    PontoReferencia = this.txtPontoReferencia.Text,
                    Observacao = this.txtObservacaoCliente.Text,
                    Telefone = this.txtTelefone.Text,
                    UF = this.txtEstado.Text,
                    TicketFidelidade = 0,
                    CodRegiao = mCodRegiao,
                    DataCadastro = Convert.ToDateTime(txtDataCadastro.Text)
                };
                if (txtTelefone2.Visible == true)
                {
                    pessoa.Telefone2 = txtTelefone2.Text;
                }
                else
                {
                    pessoa.Telefone2 = "0";
                }
                if (txtDataNascimento.Visible && txtDataNascimento.Text == "  /  /")
                {
                    //pessoa.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text);
                    pessoa.DataNascimento = Convert.ToDateTime("01/01/1950" + " " + "23:58:00");
                }
                else if (txtDataNascimento.Visible && txtDataNascimento.Text != "  /  /")
                {
                    pessoa.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text + " " + "23:58:00");
                }
                else if (!txtDataNascimento.Visible)
                {
                    pessoa.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text);
                }

                con.Update("spAlterarPessoa", pessoa);
                Utils.ControlaEventos("Altera", this.Name);
                MessageBox.Show("Cliente alterado com sucesso.");

                this_FormClosing();
                //ClearForm(this);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Registro não foi gravado , favor verificar " + ex.Message);
            }
        }

        private void this_FormClosing()
        {
            Utils.PopularGrid("Pessoa", this.parentMain.clientesGridView, "spObterPessoas");
            this.Dispose();
        }
        private void CadastraCEP()
        {
            DataSet Cep = con.SelectAll("base_cep", "spObterMaiorCEP");
            DataRow dRwo = Cep.Tables[0].Rows[0];
            int NovoCep = int.Parse(dRwo.ItemArray.GetValue(0).ToString()) + 1;

            baseCEP baseCep = new baseCEP()
            {
                Id = NovoCep,
                Cep = int.Parse(txtCEP.Text),
                Logradouro = this.txtEndereco.Text,
                Bairro = txtBairro.Text,
                Cidade = txtBairro.Text,
                Estado = txtBairro.Text
            };

            con.Insert("spAdicionarCep", baseCep);
        }
        private static void ClearForm(System.Windows.Forms.Control parent)
        {
            foreach (System.Windows.Forms.Control ctrControl in parent.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.TextBox)))
                {
                    //Check to see if it's a textbox 
                    ((System.Windows.Forms.TextBox)ctrControl).Text = string.Empty;
                    //If it is then set the text to String.Empty (empty textbox) 
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RichTextBox)))
                {
                    //If its a RichTextBox clear the text
                    ((System.Windows.Forms.RichTextBox)ctrControl).Text = string.Empty;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.ComboBox)))
                {
                    //Next check if it's a dropdown list 
                    ((System.Windows.Forms.ComboBox)ctrControl).SelectedIndex = -1;
                    //If it is then set its SelectedIndex to 0 
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.CheckBox)))
                {
                    //Next uncheck all checkboxes
                    ((System.Windows.Forms.CheckBox)ctrControl).Checked = false;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    //Unselect all RadioButtons
                    ((System.Windows.Forms.RadioButton)ctrControl).Checked = false;
                }
                if (ctrControl.Controls.Count > 0)
                {
                    //Call itself to get all other controls in other containers 
                    ClearForm(ctrControl);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNomeCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNomeCliente.Text != "")
                {
                    txtTelefone.Focus();
                }
            }

        }

        private void txtTelefone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtTelefone.Text != "" && txtTelefone2.Visible == true)
                {
                    txtTelefone2.Focus();
                }
                else
                {
                    txtCEP.Focus();
                }
            }
        }

        private void txtEndereco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtEndereco.Text != "")
                {
                    txtNumero.Focus();
                }
            }
        }

        private void txtNumero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNumero.Text != "")
                {
                    txtBairro.Focus();
                }
            }
        }

        private void txtBairro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBairro.Text != "")
                {
                    txtCidade.Focus();
                }
            }
        }
        private void ObterCidadePadrao()
        {
            try
            {

                //DataRow Linha = Empresa.Tables["Empresa"].Rows[0];
                txtCidade.Text = Sessions.returnEmpresa.Cidade.ToString();
                txtEstado.Text = Sessions.returnEmpresa.UF.ToString();

                //var Empresa = con.SelectAll("Empresa", "spObterEmpresa");
                //if (Empresa != null)
                //{
                //    DataRow Linha = Empresa.Tables["Empresa"].Rows[0];
                //    txtCidade.Text = Linha.ItemArray.GetValue(7).ToString();
                //    txtEstado.Text = Linha.ItemArray.GetValue(10).ToString();
                //}
            }
            catch (Exception Erro)
            {

                MessageBox.Show(Erro.Message);
            }

        }

        private void txtCidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCidade.Text != "")
                {
                    txtEstado.Focus();
                }
            }
        }

        private void txtEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtEstado.Text != "")
                {
                    if (txtDataNascimento.Visible == true)
                    {
                        txtDataNascimento.Focus();
                    }
                    else
                    {
                        txtPontoReferencia.Focus();
                    }

                }

            }
        }

        private void txtObservacaoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtObservacaoCliente.Text != "")
                {
                    btnAdicionarCliente.Focus();
                }
            }
        }

        private void txtPontoReferencia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtPontoReferencia.Text != "")
                {
                    txtObservacaoCliente.Focus();
                }
            }
        }

        private void txtTelefone2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCEP.Focus();
            }
        }

        private void txtDataNascimento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPontoReferencia.Focus();
            }
        }

        private void ConsultarPedidos(object sender, EventArgs e)
        {
            DateTime dataInicio, dataFim;
            DataSet dsPedidos;
            decimal strTotalPedido = 0.00M;
            int intNumeroPedidos;
            decimal strMedia = 0.00M;
            string strDataPedido = DateTime.Now.ToShortDateString();
            dataInicio = Convert.ToDateTime(dtInicio.Value.ToShortDateString() + " 00:00:00");
            dataFim = Convert.ToDateTime(dtFim.Value.ToShortDateString() + " 23:59:59");
            this.PedidosGridView.DataSource = null;
            this.PedidosGridView.AutoGenerateColumns = true;
            dsPedidos = con.SelectRegistroPorCodigoPeriodo("Pedido", "spObterPedidosPessoaPorData", Convert.ToString(codigoClienteParaAlterar), dataInicio, dataFim);
            if (dsPedidos.Tables[0].Rows.Count > 0)
            {
                this.PedidosGridView.DataSource = dsPedidos;
                this.PedidosGridView.DataMember = "Pedido";
                intNumeroPedidos = PedidosGridView.Rows.Count;

                for (int i = 0; i < PedidosGridView.Rows.Count; i++)
                {
                    strTotalPedido = strTotalPedido + decimal.Parse(PedidosGridView.Rows[i].Cells[2].Value.ToString());
                    strDataPedido = PedidosGridView.Rows[i].Cells[1].Value.ToString();

                    // Pinta  linda de Vermelha para Pedidos Cancelados
                    if (PedidosGridView.Rows[i].Cells[4].Value.ToString() == "Cancelado")
                    {
                        PedidosGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }

                lblTotalPedido.Text = strTotalPedido.ToString();
                lblMedia.Text = Convert.ToString(strTotalPedido / intNumeroPedidos);
                lblQtd.Text = intNumeroPedidos.ToString();
                lblData.Text = strDataPedido;
            }
            else
            {
                MessageBox.Show("Não há registros no periodo informado", "Aviso");
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCadastroCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F12) && (btnAdicionarCliente.Text == "Salvar [F12]"))
            {
                AdicionarCliente(sender, e);
            }
            else if (((e.KeyCode == Keys.F12) && (btnAdicionarCliente.Text == "Alterar [F12]")))
            {
                AlterarCliente(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void txtTelefone2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void txtCEP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void cbxRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            // var TaxaEntrega;
            var Regiao = con.SelectRegistroPorCodigo("RegiaoEntrega", "spObterRegioesPorCodigo", int.Parse(this.cbxRegiao.SelectedValue.ToString())).Tables["RegiaoEntrega"];
            mCodRegiao = int.Parse(Regiao.Rows[0]["Codigo"].ToString());
            txtTaxaEntrega.Text = Convert.ToString(Regiao.Rows[0]["TaxaServico"].ToString());
            // txtTaxaEntrega.Text =Convert.ToString(TaxaEntrega);

        }

        private void AlteraRegiao(object sender, EventArgs e)
        {
            CarregaRegiao("");
        }

        private void MostraItems(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (PedidosGridView.SelectedRows.Count > 0)
            {
                int CodPedido = int.Parse(PedidosGridView.SelectedCells[0].Value.ToString());
                this.ItemsPedidoGridView.DataSource = null;
                this.ItemsPedidoGridView.AutoGenerateColumns = true;
                this.ItemsPedidoGridView.DataSource = con.SelectRegistroPorCodigo("ItemsPedido", "spObterItemsPedido", CodPedido);
                this.ItemsPedidoGridView.DataMember = "ItemsPedido";


                for (int i = 0; i < ItemsPedidoGridView.Columns.Count; i++)
                {
                    if (ItemsPedidoGridView.Columns[i].HeaderText != "NomeProduto")
                    {
                        ItemsPedidoGridView.Columns.RemoveAt(i);
                    }
                }
                ItemsPedidoGridView.Refresh();

            }
        }

        private void LancarHistorico(object sender, EventArgs e)
        {
            double dblTotalCredito = 0.00;
            double dblTotalDebito = 0.00;

            HistoricoPessoa histPessoa = new HistoricoPessoa()
            {
                CodPessoa = codigoClienteParaAlterar,
                CodUsuario = Sessions.retunrUsuario.Codigo,
                Data = dtLancamento.Value,
                Historico = txtHistorico.Text,
                Valor = double.Parse(txtValor.Text.Replace(",", "."))

            };
            if (rbCredito.Checked)
            {
                histPessoa.Tipo = 'C';
            }
            else
            {
                histPessoa.Tipo = 'D';
            }
            con.Insert("spAdicionaHistorico", histPessoa);
           // Utils.PopularGrid("HistoricoPessoa", HistoricoGridView, "spObterHistoricoPorPessoa", histPessoa.CodPessoa);
            CalculaValores();
        }
        private void CalculaValores()
        {
            double dblTotalCredito = 0.00;
            double dblTotalDebito = 0.00;
            for (int i = 0; i < HistoricoGridView.Rows.Count; i++)
            {
                if (HistoricoGridView.Rows[i].Cells["Tipo"].Value.ToString() == "D")
                {
                    dblTotalDebito = dblTotalDebito + double.Parse(HistoricoGridView.Rows[i].Cells["Valor"].Value.ToString());
                }
                else
                {
                    dblTotalCredito = dblTotalCredito + double.Parse(HistoricoGridView.Rows[i].Cells["Valor"].Value.ToString());
                }
            }
            lblTotal.Text = Convert.ToString(dblTotalCredito - dblTotalDebito);
            txtValor.Text = txtHistorico.Text = "";
        }

        private void btnCons_Click(object sender, EventArgs e)
        {
            DataSet dsPedidos = con.SelectRegistroPorCodigoPeriodo("HistoricoPessoa", "spObterHistoricoPorPessoa", Convert.ToString(codigoClienteParaAlterar), dataInicio.Value, dataFim.Value);
            HistoricoGridView.DataSource = null;
            HistoricoGridView.AutoGenerateColumns = true;
            HistoricoGridView.DataSource = dsPedidos;
            HistoricoGridView.DataMember = "HistoricoPessoa";
            
            CalculaValores();

        }
    }

}
