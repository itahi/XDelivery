using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DexComanda.Models;
using DexComanda.Relatorios.Delivery;
using DexComanda.Models.WS;
using System.Collections.Generic;

namespace DexComanda
{
    public partial class frmCadastroCliente : Form
    {
        private Conexao con;
        private Pessoa cliente;
        private int codigoClienteParaAlterar;
        private DataSet endereco;
        private DataRow RowsClientes;
        private int mCodRegiao;
        private int rowIndex;
        private int codigo = 0;
        private int prvCodEndereco;

        public frmCadastroCliente()
        {
            InitializeComponent();
            CarregaRegiao(0, cbxRegiao, txtTaxaEntrega);
            txtNomeCliente.Focus();
            // ObterCidadePadrao();
            //ObterCidadePadrao();
        }

        public frmCadastroCliente(Pessoa cli)
        {
            InitializeComponent();
            this.cliente = cli;
            txtDataNascimento.Text = cli.DataNascimento.ToString();
            txtDataCadastro.Text = cli.DataCadastro.ToString();
            txtNomeCliente.Focus();
            txtNomeCliente.Select();
            //ObterCidadePadrao();
            // ObterCidadePadrao();
        }

        public frmCadastroCliente(int iCodPessoa, string iNomeCliente, string iTelefone, string iTelefone2,
                              string iCEP, string iEndereco, string inumero, string iBairro, string iCidade,
                              string iEstado, string iPontoReferencia, string iObservacao, int iCodRegiao,
                              string iDataCadastro, string iDataNascimento, string iUserID, string iPJPF, int intCodEndereco,int intCodOrigem)
        {
            InitializeComponent();
            codigoClienteParaAlterar = iCodPessoa;
            prvCodEndereco = intCodEndereco;
            txtNomeCliente.Text = iNomeCliente;
            txtTelefone.Text = iTelefone;
            txtTelefone2.Text = iTelefone2;
            txtCEP.Text = iCEP;
            txtEndereco.Text = iEndereco;
            txtNumero.Text = inumero;
            txtBairro.Text = iBairro;
            txtCidade.Text = iCidade;
            txtEstado.Text = iEstado;
            txtPontoReferencia.Text = iPontoReferencia;
            txtObservacaoCliente.Text = iObservacao;
            txtDataCadastro.Text = iDataCadastro;
            txtDataNascimento.Text = iDataNascimento;
            txtUserID.Text = iUserID;
            txtPJPF.Text = iPJPF;
            CarregaRegiao(iCodRegiao, cbxRegiao, txtTaxaEntrega);
            CarregaOrigem(intCodOrigem);
            this.btnAdicionarCliente.Text = "Alterar [F12]";
            this.btnAdicionarCliente.Click -= AdicionarCliente;
            this.btnAdicionarCliente.Click += AlterarCliente;

            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowDialog();
        }
        private void AtualizaEndereco()
        {
            try
            {
                Pessoa_Endereco pessEnd = new Pessoa_Endereco()
                {
                    Codigo = prvCodEndereco,
                    Bairro = txtBairro.Text,
                    Cep = txtCEP.Text,
                    Cidade = txtCidade.Text,
                    CodPessoa = codigoClienteParaAlterar,
                    CodRegiao = mCodRegiao,
                    Complemento = txtObservacaoCliente.Text,
                    Endereco = txtEndereco.Text,
                    NomeEndereco = "Principal",
                    Numero = txtNumero.Text,
                    PontoReferencia = txtPontoReferencia.Text,
                    UF = txtEstado.Text
                };
                con.Update("spAlterarEndereco", pessEnd);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
        private void ListaEnderecos()
        {
            if (codigoClienteParaAlterar == 0)
            {
                return;
            }
            DataSet dsEnderecos = con.SelectRegistroPorCodigo("Pessoa_Endereco", "spObterEnderecoPessoa", codigoClienteParaAlterar);
            if (dsEnderecos.Tables[0].Rows.Count > 0)
            {

            }
        }
        private void frmCadastroCliente_Load(object sender, EventArgs e)
        {

            con = new Conexao();
            dtLancamento.Value = dtFim.Value = dtInicio.Value = DateTime.Now;
            txtNomeCliente.Focus();
            txtNomeCliente.Select();
            ObterCidadePadrao();
            ListaEnderecosPessoa();

            if (Sessions.returnConfig != null)
            {
                lblDataNascimento.Enabled = txtDataNascimento.Enabled = Sessions.returnConfig.UsaDataNascimento;
                lblTelefone2.Enabled = txtTelefone2.Visible = Sessions.returnConfig.Usa2Telefones;
            }
            if (cliente != null)
            {
                Utils.ExibirDadosForm(tbPrincipal, !Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "VisualizaDadosClienteSN"));
                this.txtDataNascimento.Enabled = Sessions.returnConfig.UsaDataNascimento;
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
                    int intCodOrigem = int.Parse(RowsClientes.ItemArray.GetValue(21).ToString());
                    if (txtTelefone2.Visible == true)
                    {
                        txtTelefone2.Text = RowsClientes.ItemArray.GetValue(11).ToString();
                    }

                    //Carrega as Regioes de Entrega previamente cadastradas // 
                    CarregaRegiao(int.Parse(RowsClientes.ItemArray.GetValue(0).ToString()), cbxRegiao, txtTaxaEntrega);
                    CarregaOrigem(intCodOrigem);

                }

            }
        }


        // Utils.MontaCombox(cbxOrigemCadastro, "Nome", "Codigo", "Pessoa_OrigemCadastro", "spObterPessoa_OrigemCadastro");

        private void CarregaOrigem(int iCodOrigem = 0)
        {
            if (iCodOrigem == 0)
            {
                Utils.MontaCombox(cbxOrigemCadastro, "Nome", "Codigo", "Pessoa_OrigemCadastro", "spObterPessoa_OrigemCadastro");
                // cbxOrigemCadastro.DataSource = con.SelectAll("Pessoa_OrigemCadastro", "spObterPessoa_OrigemCadastro").Tables["Pessoa_OrigemCadastro"];
            }
            else
            {
                Utils.MontaCombox(cbxOrigemCadastro, "Nome", "Codigo", "Pessoa_OrigemCadastro", "spObterPessoa_OrigemCadastroPorCodigo", iCodOrigem);
            }
          
        }
        private void CarregaRegiao(int iCodRegiao, ComboBox iCbx, TextBox itext)
        {
            if (iCodRegiao == 0)
            {
                Conexao con = new Conexao();

                this.cbxRegiao.DataSource = con.SelectAll("RegiaoEntrega", "spObterRegioes").Tables["RegiaoEntrega"];
                this.cbxRegiao.DisplayMember = "NomeRegiao";
                this.cbxRegiao.ValueMember = "Codigo";
            }
            else
            {
                Conexao con = new Conexao();
                DataSet Regiao = con.SelectRegistroPorCodigo("RegiaoEntrega", "spObterRegioesPorCodigo", iCodRegiao);
                if (Regiao.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                mCodRegiao = Regiao.Tables["RegiaoEntrega"].Rows[0].Field<int>("Codigo");
                itext.Text = Regiao.Tables["RegiaoEntrega"].Rows[0].Field<decimal>("TaxaServico").ToString();
                cbxRegiao.Text = Regiao.Tables["RegiaoEntrega"].Rows[0].Field<string>("NomeRegiao");
            }
        }
        
        /// <summary>
        /// Busca o CEP no banco de dados local caso não encontre ele busca nos correios e insere no banco local
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ConsultarEnderecoPorCep(object sender, KeyEventArgs e)
        {
            CepUtil _cepCorreios;
            if (txtCEP.Text.Trim().Length == 8 && e.KeyCode != Keys.Back)
            {
                ObterCidadePadrao();
                if (this.txtCEP.Text.Equals("") && e.KeyCode == Keys.Enter)
                {
                    MessageBox.Show("Informe o Cep.");
                    return;
                }
                else
                {
                    int cep = int.Parse(this.txtCEP.Text);
                    endereco = con.SelectEnderecoPorCep("base_cep", "spObterEnderecoPorCep", cep);

                    if (endereco.Tables["base_cep"].Rows.Count > 0)
                    {
                        DataRow dRow = endereco.Tables["base_cep"].Rows[0];
                        this.txtEndereco.Text = dRow.ItemArray.GetValue(0).ToString();
                        this.txtBairro.Text = dRow.ItemArray.GetValue(1).ToString();
                        PreencheRegiao(txtBairro.Text, cbxRegiao, txtTaxaEntrega);
                        this.txtNumero.Focus();
                    }
                    else
                    {
                        // MessageBox.Show("Cep não encontrado");
                        if (!con.IsConnected())
                        {
                            return;
                        }
                        pnConsultaCEp.Visible = true;
                        _cepCorreios = Utils.BuscaCEPOnline(txtCEP.Text);
                        if (_cepCorreios != null)
                        {
                            txtEndereco.Text = _cepCorreios.Logradouro;
                            txtBairro.Text = _cepCorreios.Bairro;
                            PreencheRegiao(txtBairro.Text, cbxRegiao, txtTaxaEntrega);
                            this.txtNumero.Focus();
                        }
                        pnConsultaCEp.Visible = false;
                    }
                }
            }

        }
        
        private void PreencheRegiao(string iBairro, ComboBox iCbxName, TextBox iTextBox)
        {
            DataSet ds = con.RetornarTaxaPorBairro(iBairro);
            if (ds.Tables[0].Rows.Count== 0)
            {
                MessageBox.Show("Bairro não pertence a nenhuma região cadastrada", "[xSistemas] Aviso");
                return;
            }
            Utils.MontaCombox(iCbxName, "NomeRegiao", "Codigo", "RegiaoEntrega", "spObterRegioesPorCodigo", ds.Tables[0].Rows[0].Field<int>("Codigo"));
            txtTaxaEntregaEnd.Text = ds.Tables[0].Rows[0].Field<decimal>("TaxaServico").ToString();
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
                    user_id = "",
                    DataCadastro = DateTime.Now,
                    Sexo = "1",
                    DDD = "",
                    PFPJ = 'F'
                };
                if (cbxOrigemCadastro.SelectedIndex>0)
                {
                    pessoa.CodOrigemCadastro = int.Parse(cbxOrigemCadastro.SelectedValue.ToString());
                }
                else
                {
                    pessoa.CodOrigemCadastro = 1;
                }
                if (cbxRegiao.SelectedValue.ToString() != "")
                {
                    pessoa.CodRegiao = int.Parse(this.cbxRegiao.SelectedValue.ToString());
                }
                else
                {
                    MessageBox.Show("Região de entrega não selecionada, favor vericicar", "[XSistemas]");
                    return;
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
                    pessoa.DataNascimento = Convert.ToDateTime("01/01/1950" + " " + "23:58:00");
                }

                if (txtNumero.Text != "")
                {
                    pessoa.Numero = txtNumero.Text;
                    con.Insert("spAdicionarClienteDelivery", pessoa);
                    Pessoa_Endereco pessEnd = new Pessoa_Endereco()
                    {
                        Codigo = codigo,
                        CodPessoa = con.getLastCodigo(),
                        Bairro = txtBairro.Text,
                        Cep = txtCEP.Text,
                        Cidade = txtCidade.Text,
                        Endereco = txtEndereco.Text,
                        CodRegiao = int.Parse(cbxRegiao.SelectedValue.ToString()),
                        NomeEndereco = "Principal",
                        Numero = txtNumero.Text,
                        Complemento = txtObservacaoCliente.Text,
                        PontoReferencia = txtPontoReferencia.Text,
                        UF = "ES"
                    };
                    Utils.ControlaEventos("Inserir", this.Name);
                    MessageBox.Show("Cliente cadastrado com sucesso.", "[xSistemas] Aviso", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    this.Close();
                    con.Insert("spAdicionarEndereco", pessEnd);

                    if (Utils.CaixaAberto(DateTime.Now, Sessions.retunrUsuario.CaixaLogado, Sessions.retunrUsuario.Turno))
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
                this.Close();
                //   DialogResult resultado = MessageBox.Show("Deseja realizar um Pedido para pessoa cadastrada?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Utils.MessageBoxQuestion("Deseja realizar um Pedido para pessoa cadastrada?"))
                {
                    DBExpertDataSet dbExpert = new DBExpertDataSet();
                    DataSet pessoaTelefone = con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", telefone);
                    if ((pessoaTelefone.Tables["Pessoa"].Rows.Count == 0))
                    {
                        RealizarPedidoAgora(telefone);
                    }
                    else
                    {
                        DataSet Pessoa = con.SelectPessoaPorTelefone("Pessoa", "spObterPessoaPorTelefone", telefone);
                        DataRow dRow = Pessoa.Tables["Pessoa"].Rows[0];

                        int iCodPessoa = int.Parse(dRow.ItemArray.GetValue(0).ToString());
                        int iCodEndereco = int.Parse(dRow.ItemArray.GetValue(16).ToString());
                        //this.parentMain.txtNome.Text = dRow.ItemArray.GetValue(1).ToString();
                        //this.parentMain.txtEndereco.Text = dRow.ItemArray.GetValue(2).ToString();
                        //this.parentMain.txtBairro.Text = dRow.ItemArray.GetValue(3).ToString();
                        //this.parentMain.txtCidade.Text = dRow.ItemArray.GetValue(4).ToString();
                        //this.parentMain.txtPontoReferencia.Text = dRow.ItemArray.GetValue(5).ToString();

                        var TaxaEntrega = Utils.RetornaTaxaPorCliente(iCodPessoa, 0);
                        frmCadastrarPedido frmCadastrarPedido = new frmCadastrarPedido(false, "0,00", 0, "", TaxaEntrega, false, DateTime.Now, 0, int.Parse(dRow.ItemArray.GetValue(0).ToString()),
                                                                                       "", "", "", "",0, 0, 0, "", iCodEndereco);
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
                if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AlteraDadosClienteSN"))
                {
                    return;
                }
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
                    DataCadastro = Convert.ToDateTime(txtDataCadastro.Text),
                    user_id = txtUserID.Text,
                    PFPJ = 'F',
                    Sexo = "1",
                    DDD = "",
                    CodOrigemCadastro = int.Parse(cbxOrigemCadastro.SelectedValue.ToString())
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
                AtualizaEndereco();
                Utils.ControlaEventos("Altera", this.Name);
                MessageBox.Show("Cliente alterado com sucesso.");

                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Registro não foi gravado , favor verificar " + ex.Message);
            }
        }
        private void this_FormClosing()
        {
          //  Utils.PopulaGrid_Novo("Pessoa", Sessions.SqlPessoa);
        }
        //private void CadastraCEP()
        //{
        //    DataSet Cep = con.SelectAll("base_cep", "spObterMaiorCEP");
        //    DataRow dRwo = Cep.Tables[0].Rows[0];
        //    int NovoCep = int.Parse(dRwo.ItemArray.GetValue(0).ToString()) + 1;

        //    baseCEP baseCep = new baseCEP()
        //    {
        //        Id = NovoCep,
        //        Cep = int.Parse(txtCEP.Text),
        //        Logradouro = this.txtEndereco.Text,
        //        Bairro = txtBairro.Text,
        //        Cidade = txtBairro.Text,
        //        Estado = txtBairro.Text
        //    };

        //    con.Insert("spAdicionarCep", baseCep);
        //}
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

            if (txtTelefone.Text != "" && txtTelefone2.Visible)
            {
                txtTelefone2.Focus();
            }
            else
            {
                txtCEP.Focus();
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
                if (codigoClienteParaAlterar != 0)
                {
                    return;
                }
                txtCidade.Text = Sessions.returnEmpresa.Cidade.ToString();
                txtEstado.Text = Sessions.returnEmpresa.UF.ToString();

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

        }

        private void AlteraRegiao(object sender, EventArgs e)
        {
            if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AlteraDadosClienteSN"))
            {
                return;
            }
            CarregaRegiao(0, cbxRegiao, txtTaxaEntrega);
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
            if (dtLancamento.Value < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                if (!Utils.MessageBoxQuestion("Data do lançamento é menor que data atual, tem certeza que deseja continuar?"))
                {
                    return;
                }

            }
            if (txtValor.Text.Trim() == "" || txtHistorico.Text.Trim() == "")
            {
                MessageBox.Show("Campos obrigatórios não podem ser vazios");
                return;
            }
            double dblTotalCredito = 0.00;
            double dblTotalDebito = 0.00;

            HistoricoPessoa histPessoa = new HistoricoPessoa()
            {
                CodPessoa = codigoClienteParaAlterar,
                CodUsuario = Sessions.retunrUsuario.Codigo,
                Data = dtLancamento.Value,
                Historico = txtHistorico.Text,
                Valor = decimal.Parse(txtValor.Text)

            };
            if (rbCredito.Checked)
            {
                histPessoa.Tipo = 'C';
            }
            else
            {
                histPessoa.Tipo = 'D';
                histPessoa.Valor = -histPessoa.Valor;
            }
            con.Insert("spAdicionaHistorico", histPessoa);
            Utils.PopularGrid_SP("HistoricoPessoa", HistoricoGridView, "spObterHistoricoPorPessoa", histPessoa.CodPessoa);
            // Utils.PopularGrid("HistoricoPessoa", HistoricoGridView, "spObterHistoricoPorPessoa", histPessoa.CodPessoa);
            CalculaValores();
        }
        private void CalculaValores()
        {
            Decimal dblTotalCredito = 0.00M;
            Decimal dblTotalDebito = 0.00M;
            for (int i = 0; i < HistoricoGridView.Rows.Count; i++)
            {

                dblTotalDebito = decimal.Parse(HistoricoGridView.Rows[i].Cells["Valor"].Value.ToString()) + dblTotalDebito;
            }
            lblTotal.Text = dblTotalDebito.ToString();
            txtValor.Text = txtHistorico.Text = "";
        }

        private void btnCons_Click(object sender, EventArgs e)
        {
            DataSet dsPedidos = con.SelectRegistroPorCodigoPeriodo("HistoricoPessoa", "spObterHistoricoPorPessoaPorData", Convert.ToString(codigoClienteParaAlterar), dataInicio.Value, dataFim.Value);
            if (dsPedidos.Tables[0].Rows.Count > 0)
            {
                HistoricoGridView.DataSource = null;
                HistoricoGridView.AutoGenerateColumns = true;
                HistoricoGridView.DataSource = dsPedidos;
                HistoricoGridView.DataMember = "HistoricoPessoa";
                CalculaValores();
                btnImprimir.Enabled = true;
            }


        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Utils.ImprimirHistoricoCliente(codigoClienteParaAlterar, dataInicio.Value, dataFim.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RelDelivery report;
            int iCodigo = 4441;
            try
            {
                report = new RelDelivery();
                report.SetDatabaseLogon("sa", "1001");
                report.SetParameterValue("@Codigo", iCodigo);
                report.PrintToPrinter(0, false, 0, 0);
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.InnerException.Message);
            }

        }

        private void MenuAuxiliar(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AuxiliarMenu(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem ReeimpressaoPedido = new MenuItem(" 0 - Reeimprimir Pedido");

                ReeimpressaoPedido.Click += ReeimpressaoPedidoGrid;
                m.MenuItems.Add(ReeimpressaoPedido);
                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }

        private void ReeimpressaoPedidoGrid(object sender, EventArgs e)
        {
            int codPedido = int.Parse(this.PedidosGridView.CurrentRow.Cells[0].Value.ToString());
            DataSet dsPedido = con.SelectRegistroPorCodigo("Pedido", "spObterPedidoFinalizadoPorCodigo", codPedido);

            if (dsPedido.Tables[0].Rows.Count > 0)
            {
                DataRow dRowPedido = dsPedido.Tables[0].Rows[0];
                DateTime dtPedido = Convert.ToDateTime(dRowPedido.ItemArray.GetValue(7).ToString());
                string iTipo = dRowPedido.ItemArray.GetValue(8).ToString();
                decimal iTrocoPara = decimal.Parse(dRowPedido.ItemArray.GetValue(4).ToString());
                decimal iTotalPedido = decimal.Parse(dRowPedido.ItemArray.GetValue(3).ToString());
                decimal iValue = 0;
                if (iTrocoPara != 0.00M && iTrocoPara != 0)
                {
                    iValue = iTrocoPara - iTotalPedido;
                }

                Utils.ImpressaoEntreganova(codPedido, iValue,int.Parse(Utils.RetornaConfiguracaoDelivery().ViaDelivery));
            }

        }

        private void PedidosGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowIndex = e.RowIndex;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel selecionar a linha " + erro.Message);
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }

        private void txtCEP_KeyUp(object sender, KeyEventArgs e)
        {
            ConsultarEnderecoPorCep(sender, e);
        }

        private void cbxRegiao_DropDown(object sender, EventArgs e)
        {
            CarregaRegiao(0, cbxRegiao, txtTaxaEntrega);
        }

        private void AdicionarEndereco(object sender, EventArgs e)
        {
            try
            {

                if (codigoClienteParaAlterar == 0)
                {
                    return;
                }
                if (cbxRegiaoEnd.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("Selecione a Região");
                    cbxRegiaoEnd.Focus();
                    return;
                }
                Pessoa_Endereco pessEnd = new Pessoa_Endereco()
                {
                    Codigo = codigo,
                    CodPessoa = codigoClienteParaAlterar,
                    Bairro = txtBairroEnd.Text,
                    Cep = txtCEPEnd.Text,
                    Cidade = txtCidadeEnd.Text,
                    Endereco = txtLogradouro.Text,
                    CodRegiao = int.Parse(cbxRegiaoEnd.SelectedValue.ToString()),
                    NomeEndereco = txtNomeEnd.Text,
                    Numero = txtNumEnd.Text,
                    Complemento = txtComplementoEnd.Text,
                    PontoReferencia = txtPontoREnd.Text,
                    UF = "ES"
                };
                if (codigo == 0)
                {
                    con.Insert("spAdicionarEndereco", pessEnd);
                }
                else
                {
                    con.Update("spAlterarEndereco", pessEnd);
                }
                codigo = 0;
                Utils.LimpaForm(groupBox2);
                ListaEnderecosPessoa();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);

            }

        }
        private void ListaEnderecosPessoa()
        {
            DataSet dsEnderecos = con.SelectRegistroPorCodigo("Pessoa_Endereco", "spObterEnderecoPessoa", codigoClienteParaAlterar);
            if (dsEnderecos.Tables[0].Rows.Count > 0)
            {
                gridViewEndereco.DataSource = null;
                gridViewEndereco.AutoGenerateColumns = true;
                gridViewEndereco.DataSource = dsEnderecos;
                gridViewEndereco.DataMember = "Pessoa_Endereco";


            }
        }

        private void cbxRegiaoEnd_DropDown(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxRegiaoEnd, "NomeRegiao", "Codigo", "Regiao_Entrega", "spObterRegioes");
        }

        private void txtCEPEnd_KeyDown(object sender, KeyEventArgs e)
        {
            CepUtil _cepCorreios;
            if (txtCEPEnd.Text.Trim().Length == 8 && e.KeyCode != Keys.Back)
            {
                // ObterCidadePadrao();
                if (this.txtCEPEnd.Text.Equals("") && e.KeyCode == Keys.Enter)
                {
                    MessageBox.Show("Informe o Cep.");
                    return;
                }
                else
                {
                    int cep = int.Parse(this.txtCEPEnd.Text);
                    endereco = con.SelectEnderecoPorCep("base_cep", "spObterEnderecoPorCep", cep);

                    if (endereco.Tables["base_cep"].Rows.Count > 0)
                    {
                        DataRow dRow = endereco.Tables["base_cep"].Rows[0];
                        this.txtLogradouro.Text = dRow.ItemArray.GetValue(0).ToString();
                        this.txtBairroEnd.Text = dRow.ItemArray.GetValue(1).ToString();
                        txtCidadeEnd.Text = dRow.ItemArray.GetValue(2).ToString();
                        PreencheRegiao(txtBairroEnd.Text, cbxRegiaoEnd, txtTaxaEntregaEnd);
                        this.txtNumEnd.Focus();
                        //CarregaRegiao(0, cbxRegiaoEnd, txtTaxaEntregaEnd);
                    }
                    else
                    {
                        if (!con.IsConnected())
                        {
                            return;
                        }
                        _cepCorreios = Utils.BuscaCEPOnline(txtCEPEnd.Text);
                        if (_cepCorreios != null)
                        {
                            txtLogradouro.Text = _cepCorreios.Logradouro;
                            txtBairroEnd.Text = _cepCorreios.Bairro;
                            txtCidadeEnd.Text = _cepCorreios.Cidade;
                            PreencheRegiao(txtBairroEnd.Text, cbxRegiaoEnd, txtTaxaEntregaEnd);
                            this.txtNumEnd.Focus();
                        }
                        // pnConsultaCEp.Visible = false;
                    }
                }
            }

        }

        private void txtCEPEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void txtNumEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void txtCEPEnd_KeyUp(object sender, KeyEventArgs e)
        {
            txtCEPEnd_KeyDown(sender, e);
        }

        private void SelecionaRegistro(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (gridViewEndereco.CurrentRow.Cells["CodPessoa"].Value.ToString() != "")
            {
                codigo = int.Parse(gridViewEndereco.CurrentRow.Cells["Codigo"].Value.ToString());
                txtCEPEnd.Text = gridViewEndereco.CurrentRow.Cells["CEP"].Value.ToString();
                txtBairroEnd.Text = gridViewEndereco.CurrentRow.Cells["Bairro"].Value.ToString();
                txtNumEnd.Text = gridViewEndereco.CurrentRow.Cells["Numero"].Value.ToString();
                txtCidadeEnd.Text = gridViewEndereco.CurrentRow.Cells["Cidade"].Value.ToString();
                txtNomeEnd.Text = gridViewEndereco.CurrentRow.Cells["NomeEndereco"].Value.ToString();
                txtPontoREnd.Text = gridViewEndereco.CurrentRow.Cells["PontoReferencia"].Value.ToString();
                txtComplementoEnd.Text = gridViewEndereco.CurrentRow.Cells["Complemento"].Value.ToString();
                txtLogradouro.Text = gridViewEndereco.CurrentRow.Cells["Endereco"].Value.ToString();

                int intCodRegiao = int.Parse(gridViewEndereco.CurrentRow.Cells["CodRegiao"].Value.ToString());
                Utils.MontaCombox(cbxRegiaoEnd, "NomeRegiao", "Codigo", "RegiaoEntrega", "spObterRegioesPorCodigo", intCodRegiao);
            }
        }

        private void ListaOrigens(object sender, EventArgs e)
        {
            CarregaOrigem(0);
        }

        private void cbxRegiaoEnd_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CarregaRegiao(int.Parse(cbxRegiaoEnd.SelectedValue.ToString()), cbxRegiaoEnd, txtTaxaEntregaEnd);
        }

        private void txtBairro_KeyUp(object sender, KeyEventArgs e)
        {
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            if (txtBairro.Text.Length<=3)
            {
                return;
            }
            DataSet dsBairros = con.ListaBairro();
           
            for (int i = 0; i < dsBairros.Tables[0].Rows.Count; i++)
            {
               // lista = new AutoCompleteStringCollection();
                lista.Add(dsBairros.Tables[0].Rows[i].Field<string>("bairro"));
            }
            txtBairro.AutoCompleteCustomSource = lista;
            if (e.KeyCode == Keys.Enter)
            {
                DataSet ds = con.RetornarTaxaPorBairro(txtBairro.Text);
                if (ds.Tables[0].Rows.Count==0)
                {
                    return;
                }
                Utils.MontaCombox(cbxRegiao, "NomeRegiao", "Codigo", "RegiaoEntrega", "spObterRegioesPorCodigo", ds.Tables[0].Rows[0].Field<int>("Codigo"));
                txtTaxaEntrega.Text = ds.Tables[0].Rows[0].Field<decimal>("TaxaServico").ToString();
            }
        }

        private void txtBairro_Leave(object sender, EventArgs e)
        {
            DataSet ds = con.RetornarTaxaPorBairro(txtBairro.Text);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return;
            }
            Utils.MontaCombox(cbxRegiao, "NomeRegiao", "Codigo", "RegiaoEntrega", "spObterRegioesPorCodigo", ds.Tables[0].Rows[0].Field<int>("Codigo"));
            txtBairro.Text = ds.Tables[0].Rows[0].Field<string>("Nome").ToString();
            txtTaxaEntrega.Text = ds.Tables[0].Rows[0].Field<decimal>("TaxaServico").ToString();
        }

        private void txtBairro_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
