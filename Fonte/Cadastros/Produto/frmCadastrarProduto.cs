using DexComanda.Models;
using DexComanda.Models.Produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DexComanda
{
    public partial class frmCadastrarProduto : Form
    {
        private Conexao con;
        private Main parentMain;
        private int rowIndex;
        int codigoOpcao;
        int codTipo;
        private string strNomeProd = "";
        private string intCodGrupo;
        private Produtos produto;
        private int codigoProdutoParaAlterar;
        private bool DescontoPordia = Sessions.returnConfig.DescontoDiaSemana;
        private List<string> DiasSelecionados;
        private string Marcados;
        private bool PromocaoDiasSemana = Sessions.returnConfig.DescontoDiaSemana;
        private List<PrecoDiaProduto> listPrecos;
        private int codigoinsumo;
        public frmCadastrarProduto(Main parent)
        {
            InitializeComponent();
            this.parentMain = parent;
            //  parentMain = new Main();
            DiasSelecionados = new List<string>();
            grpDesconto.Visible = DescontoPordia;

        }
        public frmCadastrarProduto(int CodProduto, string iNomeProduto, string iCodGrupo, string iGrupo, decimal iPreco, string iDescricao, bool iVendaOnline,
                                   decimal iPrecoPromocao, string iDiasPromocao, string iMaximoAdicionais, string iUrlImagem, DateTime idtInicioPromo,
                                   DateTime idtFimPromo, bool iAtivoSN, string iCodInterno,string iMarkup,string iPrecoSugerido)
        {
            try
            {
                InitializeComponent();
                codigoProdutoParaAlterar = CodProduto;
                grpDesconto.Visible = DescontoPordia;
                txtPrecoDesconto.Text = iPrecoPromocao.ToString();
                intCodGrupo = iCodGrupo;
                string[] lol = iDiasPromocao.Split(new char[] { ';' });

                if (lol.Contains("Monday"))
                {
                    chkSegunda.Checked = true;
                }
                if (lol.Contains("Tuesday"))
                {
                    chkTerca.Checked = true;
                }
                if (lol.Contains("Wednesday "))
                {
                    ChkQuarta.Checked = true;
                }
                if (lol.Contains("Thursday"))
                {
                    chkQuinta.Checked = true;
                }
                if (lol.Contains("Friday"))
                {
                    ChkSexta.Checked = true;
                }
                if (lol.Contains("Sunday"))
                {
                    ChkSabado.Checked = true;
                }
                if (lol.Contains("Saturday"))
                {
                    chkDomingo.Checked = true;
                }

                txtMarkup.Text = iMarkup;
                txtPrecoSugerido.Text = iPrecoSugerido;
                MontaListPrecos(iDiasPromocao);
                nomeProdutoTextBox.Text = iNomeProduto;
                precoProdutoTextBox.Text = iPreco.ToString();
                Utils.MontaCombox(cbxGrupoProduto, "NomeGrupo", "Codigo", "Grupo", "spObterGrupoPorCodigo", int.Parse(iCodGrupo));
                descricaoProdutoTextBox.Text = iDescricao;
                con = new Conexao();
                txtPrecoCusto.Text = Convert.ToString(con.CalculaPrecoInsumo(CodProduto));
                chkAtivo.Checked = iAtivoSN;
                chkOnline.Checked = iVendaOnline;
                txtMaxAdicionais.Text = iMaximoAdicionais;
                txtcaminhoImage.Text = iUrlImagem;
                dtInicio.Value = idtInicioPromo;
                dtFim.Value = idtFimPromo;
                txtCodInterno.Text = iCodInterno;
                //     this.btnDoProduto.Enabled = Sessions.retunrUsuario.AlteraProdutosSN;
                this.btnDoProduto.Text = "Alterar [F12]";
                this.btnDoProduto.Click -= AdicionarProduto;
                this.btnDoProduto.Click += AlterarProduto;
            }
            catch (Exception erro)
            {

                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

        }
     
        public frmCadastrarProduto(Produtos prod, Main parent)
        {
            InitializeComponent();
            this.parentMain = parent;
            DiasSelecionados = new List<string>();
            this.produto = prod;
            if (DescontoPordia)
            {
                grpDesconto.Visible = DescontoPordia;
                txtPrecoDesconto.Text = produto.PrecoDesconto.ToString();

                if (produto.DiaSemana != "")
                {
                    string[] lol = produto.DiaSemana.Split(new char[] { ';' });

                    if (lol.Contains("Monday"))
                    {
                        chkSegunda.Checked = true;
                    }
                    if (lol.Contains("Tuesday"))
                    {
                        chkTerca.Checked = true;
                    }
                    if (lol.Contains("Wednesday "))
                    {
                        ChkQuarta.Checked = true;
                    }
                    if (lol.Contains("Thursday"))
                    {
                        chkQuinta.Checked = true;
                    }
                    if (lol.Contains("Friday"))
                    {
                        ChkSexta.Checked = true;
                    }
                    if (lol.Contains("Sunday"))
                    {
                        ChkSabado.Checked = true;
                    }
                    if (lol.Contains("Sunday"))
                    {
                        chkDomingo.Checked = true;
                    }
                }


            }

        }
        private List<PrecoDiaProduto> RetornaDiasMarcados()
        {
            
            listPrecos = new List<PrecoDiaProduto>();
            // var precosDia = new PrecoDiaProduto();
            foreach (System.Windows.Forms.Control TEXT in grpPrecosDia.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(TEXT.GetType(), typeof(System.Windows.Forms.TextBox)))
                {
                    //Check to see if  
                    if (((System.Windows.Forms.TextBox)TEXT).Text != "")
                    {
                        var precosDia = new PrecoDiaProduto()
                        {
                            Dia = (((System.Windows.Forms.TextBox)TEXT).Tag.ToString()),
                            Preco = decimal.Parse((((System.Windows.Forms.TextBox)TEXT).Text.ToString()))

                        };

                        listPrecos.Add(precosDia);
                    }

                    
                }

            }

            return listPrecos;
        }
        private void MontaListPrecos(string ivalor)
        {
            try
            {
                List<PrecoDiaProduto> pr = new List<PrecoDiaProduto>();
                if (ivalor == "")
                {
                    return;
                }
                pr = Utils.DeserializaObjeto(ivalor);
                if (pr.Count > 0)
                {
                    foreach (var item in pr)
                    {
                        foreach (System.Windows.Forms.Control obj in grpPrecosDia.Controls)
                        {
                            if (object.ReferenceEquals(obj.GetType(), typeof(System.Windows.Forms.TextBox)))
                            {
                                if (item.Dia == ((System.Windows.Forms.TextBox)obj).Tag.ToString())
                                {
                                    ((System.Windows.Forms.TextBox)obj).Text = item.Preco.ToString();
                                }
                            }

                        }
                    }
                }

            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }

       
        private void frmCadastrarProduto_Load(object sender, EventArgs e)
        {
            con = new Conexao();
            ListaInsumos();
            btnEditar.Enabled = codigoProdutoParaAlterar != 0;
            DiasSelecionados = new List<string>();
            tabPage2.IsAccessible = btnDoProduto.Text == "Alterar [F12]";
            //    List<Produtos_Adicionais> ProdAdicionais = new List<Produtos_Adicionais>();
            grpDesconto.Visible = DescontoPordia;
            List<Grupo> grupos = new List<Grupo>();

            if (produto != null)
            {
                this.btnDoProduto.Text = "Alterar [F12]";
                this.btnDoProduto.Click -= AdicionarProduto;
                this.btnDoProduto.Click += AlterarProduto;

                codigoProdutoParaAlterar = produto.Codigo;
                this.nomeProdutoTextBox.Text = produto.Nome;
                //  MontaListPrecos(produto.DiaSemana);
                txtPrecoCusto.Text = con.CalculaPrecoInsumo(codigoProdutoParaAlterar).ToString();
                cbxGrupoProduto.ValueMember = produto.CodGrupo.ToString();
                this.cbxGrupoProduto.Text = produto.GrupoProduto;
                this.precoProdutoTextBox.Text = produto.Preco.ToString();
                this.descricaoProdutoTextBox.Text = produto.Descricao.ToString();
                this.chkAtivo.Checked = produto.AtivoSN;
            }

        }
        private bool ValidaCodigoPersonalizado(int iCodigoPersonalizado, string iCodProduto)
        {
            Boolean retur = false;
            DataSet dsProduto = con.SelectRegistroPorCodigo("Produto", "spObterProdutoCodigoInterno", iCodigoPersonalizado, iCodProduto);
            if (dsProduto.Tables[0].Rows.Count > 0)
            {
                retur = true;
                strNomeProd = dsProduto.Tables[0].Rows[0].Field<string>("NomeProduto");
            }

            return retur;
        }

        private void AdicionarProduto(object sender, EventArgs e)
        {
            try
            {
                if (txtCodInterno.Text != "0" && txtCodInterno.Text != "")
                {
                    if (ValidaCodigoPersonalizado(int.Parse(txtCodInterno.Text), "0"))
                    {
                        MessageBox.Show("Código já esta sendo usado no produto " + strNomeProd);
                        return;
                    }
                }

                if (nomeProdutoTextBox.Text.Trim() == "" || precoProdutoTextBox.Text.Trim() == "" || cbxGrupoProduto.Text.Trim() == "")
                {
                    MessageBox.Show("Campos obrigátórios não preenchidos");
                    return;
                }
                Produtos produto = new Produtos()
                {
                    Codigo = 0,
                    Nome = this.nomeProdutoTextBox.Text,
                    AtivoSN = this.chkAtivo.Checked,
                    Descricao = this.descricaoProdutoTextBox.Text,
                    Preco = Convert.ToDecimal(this.precoProdutoTextBox.Text.Replace(".", ",")),
                    GrupoProduto = this.cbxGrupoProduto.Text,
                    CodGrupo = int.Parse(cbxGrupoProduto.SelectedValue.ToString()),
                    OnlineSN = chkOnline.Checked,
                    DataInicioPromocao = Convert.ToDateTime(dtInicio.Value.ToShortDateString()),
                    DataFimPromocao = Convert.ToDateTime(dtFim.Value.ToShortDateString()),
                    DataAlteracao = DateTime.Now,
                   
                };
                if (txtPrecoCusto.Text!="")
                {
                    produto.PrecoCusto = decimal.Parse(txtPrecoCusto.Text);
                }
                if (txtMarkup.Text!="")
                {
                    produto.Markup = decimal.Parse(txtMarkup.Text);
                }
                if (txtPrecoSugerido.Text!="")
                {
                    produto.PrecoSugerido = decimal.Parse(txtPrecoSugerido.Text);
                }
                produto.UrlImagem = "";
                if (txtCodInterno.Text != "0")
                {
                    produto.CodigoPersonalizado = txtCodInterno.Text;
                }
                else
                {
                    produto.CodigoPersonalizado = "0";
                }
                if (txtcaminhoImage.Text.Trim() != "")
                {
                    produto.UrlImagem = txtcaminhoImage.Text;
                }
                if (txtMaxAdicionais.Text.Trim() != "")
                {
                    produto.MaximoAdicionais = int.Parse(txtMaxAdicionais.Text);
                }
                else
                {
                    produto.MaximoAdicionais = 0;
                }
                if (DescontoPordia)
                {
                    produto.DiaSemana = Utils.SerializaObjeto(RetornaDiasMarcados());
                    if (txtPrecoDesconto.Text != "")
                    {
                        produto.PrecoDesconto = decimal.Parse(txtPrecoDesconto.Text);
                    }

                    con.Insert("spAdicionarProduto", produto);
                }
                else
                {
                    produto.DiaSemana = "";
                    produto.PrecoDesconto = decimal.Parse("0");

                    con.Insert("spAdicionarProduto", produto);
                }
                ClearForm(this);
                // this_FormClosing();
                MessageBox.Show("Produto cadastrado com sucesso.");
                SalvarAdicionais(con.getLastCodigo());
                SalvarInsumo(con.getLastCodigo());
                Utils.ControlaEventos("Inserir", this.Name);
                nomeProdutoTextBox.Focus();

            }
            catch (Exception errro)
            {
                MessageBox.Show("Registro não foi gravado " + errro.Message);
            }
        }
        private void SalvarAdicionais(int iCodProduto)
        {
            if (AdicionaisGridView.Rows.Count > 0)
            {
                for (int i = 0; i < AdicionaisGridView.Rows.Count; i++)
                {
                    Produto_Opcao prod = new Produto_Opcao()
                    {
                        CodProduto = iCodProduto,
                        CodOpcao = int.Parse(AdicionaisGridView.Rows[i].Cells["CodOpcao"].Value.ToString()),
                        Preco = decimal.Parse(AdicionaisGridView.Rows[i].Cells["Preco"].Value.ToString()),
                        CodTipo = int.Parse(AdicionaisGridView.Rows[i].Cells["CodTipo"].Value.ToString()),
                        DataAlteracao = DateTime.Now
                    };
                    con.Insert("spAdicionarOpcaProduto", prod);
                }

                AdicionaisGridView.DataSource = null;
                AdicionaisGridView.DataMember = null;
                AdicionaisGridView.AutoGenerateColumns = false;
            }
        }
        /// <summary>
        /// Salva os insumos do produto para compor o preço
        /// </summary>
        /// <param name="iCodProduto">
        /// Código do Produto</param>
        private void SalvarInsumo(int iCodProduto)
        {
            if (gridInsumo.Rows.Count > 0)
            {
                for (int i = 0; i < gridInsumo.Rows.Count; i++)
                {
                    InsumoProduto insumo = new InsumoProduto()
                    {
                        CodProduto = iCodProduto,
                        CodInsumo = int.Parse(gridInsumo.Rows[i].Cells["CodInsumo"].Value.ToString()),
                        Quantidade = decimal.Parse(gridInsumo.Rows[i].Cells["Preco"].Value.ToString()),
                    };
                    con.Insert("spAdicionarProdutoInsumo", insumo);
                }

                gridInsumo.DataSource = null;
                gridInsumo.DataMember = null;
                gridInsumo.AutoGenerateColumns = false;
            }
        }
        public string DiasSelecinado()
        {
            Marcados = "";
            if (chkSegunda.Checked)
            {
                Marcados = "Monday";
            }
            if (chkTerca.Checked)
            {
                Marcados = Marcados + ";Tuesday";
            }
            if (ChkQuarta.Checked)
            {
                Marcados = Marcados + ";Wednesday ";
            }
            if (chkQuinta.Checked)
            {
                Marcados = Marcados + ";Thursday";
            }
            if (ChkSexta.Checked)
            {
                Marcados = Marcados + ";Friday";
            }
            if (ChkSabado.Checked)
            {
                Marcados = Marcados + ";Sunday";
            }
            if (chkDomingo.Checked)
            {
                Marcados = Marcados + ";Saturday";
            }

            return Marcados;
        }

        public void AlterarProduto(object sender, EventArgs e)
        {
            try
            {

                if (!Utils.ValidaPermissao(Sessions.retunrUsuario.Codigo, "AlteraProdutosSN"))
                {
                    return;
                }
                if (txtCodInterno.Text != "0" && txtCodInterno.Text != "")
                {
                    if (ValidaCodigoPersonalizado(int.Parse(txtCodInterno.Text), Convert.ToString(codigoProdutoParaAlterar)))
                    {
                        MessageBox.Show("Código já esta sendo usado no produto " + strNomeProd);
                        return;
                    }
                }
                if (nomeProdutoTextBox.Text.Trim() == "" || precoProdutoTextBox.Text.Trim() == "" || cbxGrupoProduto.Text.Trim() == "")
                {
                    MessageBox.Show("Campos obrigátórios não preenchidos");
                    return;
                }
                Produtos produto = new Produtos()
                {
                    Codigo = codigoProdutoParaAlterar,
                    Nome = nomeProdutoTextBox.Text,
                    Descricao = descricaoProdutoTextBox.Text,
                    Preco = Convert.ToDecimal(this.precoProdutoTextBox.Text.Replace(".", ",")),
                    GrupoProduto = cbxGrupoProduto.Text,
                    AtivoSN = chkAtivo.Checked,
                    OnlineSN = chkOnline.Checked,
                    DataAlteracao = DateTime.Now,
                    DataInicioPromocao = Convert.ToDateTime(dtInicio.Value.ToShortDateString()),
                    DataFimPromocao = Convert.ToDateTime(dtFim.Value.ToShortDateString()),
                    //Markup = decimal.Parse(txtMarkup.Text),
                    //PrecoCusto = decimal.Parse(txtPrecoCusto.Text),
                    //PrecoSugerido = decimal.Parse(txtPrecoSugerido.Text),
                };
                if (txtPrecoCusto.Text != "")
                {
                    produto.PrecoCusto = decimal.Parse(txtPrecoCusto.Text);
                }
                if (txtMarkup.Text != "")
                {
                    produto.Markup = decimal.Parse(txtMarkup.Text);
                }
                if (txtPrecoSugerido.Text != "")
                {
                    produto.PrecoSugerido = decimal.Parse(txtPrecoSugerido.Text);
                }
                produto.CodigoPersonalizado = "0";
                if (txtCodInterno.Text != "" && txtCodInterno.Text != "0")
                {
                    produto.CodigoPersonalizado = txtCodInterno.Text;
                }
                if (cbxGrupoProduto.SelectedValue != null)
                {
                    produto.CodGrupo = int.Parse(cbxGrupoProduto.SelectedValue.ToString());
                }
                else
                {
                    produto.CodGrupo = int.Parse(intCodGrupo);
                }

                produto.UrlImagem = "";
                if (txtcaminhoImage.Text.Trim() != "")
                {
                    produto.UrlImagem = txtcaminhoImage.Text;
                }
                if (txtMaxAdicionais.Text.Trim() != "")
                {
                    produto.MaximoAdicionais = int.Parse(txtMaxAdicionais.Text);
                }
                else
                {
                    produto.MaximoAdicionais = 0;
                }

                produto.DiaSemana = Utils.SerializaObjeto(RetornaDiasMarcados());
                //  produto.DiaSemana = DiasSelecinado();
                if (txtPrecoDesconto.Text != "")
                {
                    produto.PrecoDesconto = decimal.Parse(txtPrecoDesconto.Text.Replace(".", ","));
                }
                else
                {
                    produto.PrecoDesconto = 0;
                }

                con.Update("spAlterarProduto", produto);
                this.btnDoProduto.Text = "Cadastrar [F12]";
                this.btnDoProduto.Click -= AlterarProduto;
                this.btnDoProduto.Click += AdicionarProduto;

                Utils.ControlaEventos("Alterar", this.Name);
                MessageBox.Show("Produto alterado com sucesso.");
                this.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Registro não foi alterado , favor verificar os campos digitados em: " + erro.Message);
            }
        }

        private void Cancelar(object sender, EventArgs e)
        {
            this.btnAdicionarOpcao.Text = "Salvar [F12]";
            this.btnAdicionarOpcao.Click += new System.EventHandler(this.AdicionarOpcao);
            this.btnAdicionarOpcao.Click -= new System.EventHandler(this.SalvarRegistro);

            this.btnEditar.Text = "Cancelar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
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
                //else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.CheckBox)))
                //{
                //    //Next uncheck all checkboxes
                //    ((System.Windows.Forms.CheckBox)ctrControl).Checked = false;
                //}
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

        private void frmCadastrarProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F12) && (btnDoProduto.Text == "Cadastrar [F12]"))
            {
                AdicionarProduto(sender, e);
            }
            else if ((e.KeyCode == Keys.F12) && (btnDoProduto.Text == "Alterar [F12]"))
            {
                AlterarProduto(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            MudaAbas(e);
        }
        private void MudaAbas(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad1 && e.Alt)
            {
                tabControl1.SelectedTab = tabPage1;
            }
            else if (e.KeyCode == Keys.NumPad2 && e.Alt)
            {
                tabControl1.SelectedTab = tabPage2;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BuscaItem(object sender, KeyEventArgs e)
        {


        }

        private void ListaOpcao(object sender, EventArgs e)
        {

            if (cbxTipoOpcao.SelectedIndex >= 0)
            {
                Utils.MontaCombox(cbxOpcao, "Nome", "Codigo", "Opcao", "spObterOpcaoPorTipo", int.Parse(cbxTipoOpcao.SelectedValue.ToString()));
            }
            else
            {
                Utils.MontaCombox(cbxOpcao, "Nome", "Codigo", "Opcao", "spObterOpcao");
            }



            //this.cbxOpcao.DataSource = con.SelectAll("Opcao", "spObterOpcao").Tables["Opcao"];
            //this.cbxOpcao.DisplayMember = "Nome";
            //this.cbxOpcao.ValueMember = "Codigo";
        }

        private void AdicionarOpcao(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < AdicionaisGridView.Rows.Count; i++)
                {
                    if (cbxOpcao.SelectedValue.ToString() == AdicionaisGridView.Rows[i].Cells["CodOpcao"].Value.ToString())
                    {
                        if (!Utils.MessageBoxQuestion("Essa opção já esta vinculada a esse produto , deseja adicionar novamente?"))
                        {
                            return;
                        }
                    }
                }
                int iCountLinhas = AdicionaisGridView.Rows.Count;
                if (codigoProdutoParaAlterar != 0)
                {
                    if (txtPrecoOpcao.Text.Trim() != "" && cbxTipoOpcao.SelectedValue.ToString() != ""
                        && cbxOpcao.SelectedValue.ToString() != "")
                    {
                        Produto_Opcao prodOpcao = new Produto_Opcao()
                        {
                            CodProduto = codigoProdutoParaAlterar,
                            CodOpcao = int.Parse(cbxOpcao.SelectedValue.ToString()),
                            Preco = decimal.Parse(txtPrecoOpcao.Text),
                            DataAlteracao = DateTime.Now,
                            CodTipo = int.Parse(cbxTipoOpcao.SelectedValue.ToString())
                        };
                        con.Insert("spAdicionarOpcaProduto", prodOpcao);
                        // con.AtualizaDataSincronismo("Produto", codigoProdutoParaAlterar, "DataAlteracao");
                        txtPrecoOpcao.Text = "";
                        cbxOpcao.Text = "";
                        ListaOpcaoProduto();
                    }
                    else
                    {
                        MessageBox.Show("Campos obrigatórios não podem ser vazios");
                        return;
                    }
                }
                else
                {

                    if (AdicionaisGridView.DataSource != null)
                    {
                        AdicionaisGridView.AutoGenerateColumns = false;
                        AdicionaisGridView.DataSource = null;
                        AdicionaisGridView.DataMember = null;
                    }

                    AdicionaisGridView.Rows.Add();
                    AdicionaisGridView.Rows[iCountLinhas].Cells["CodOpcao"].Value = int.Parse(cbxOpcao.SelectedValue.ToString());
                    AdicionaisGridView.Rows[iCountLinhas].Cells["Preco"].Value = decimal.Parse(txtPrecoOpcao.Text);
                    AdicionaisGridView.Rows[iCountLinhas].Cells["Nome"].Value = cbxOpcao.Text.ToString();
                    AdicionaisGridView.Rows[iCountLinhas].Cells["CodTipo"].Value = cbxTipoOpcao.SelectedValue.ToString();
                    iCountLinhas = AdicionaisGridView.Rows.Count;
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao adicionar linha " + erro.Message);
            }


        }

        private void ListaOpcaoProduto()
        {
            if (codigoProdutoParaAlterar != 0)
            {
                AdicionaisGridView.DataSource = con.SelectOpcaoProduto(Convert.ToString(codigoProdutoParaAlterar));
                AdicionaisGridView.AutoGenerateColumns = true;
                AdicionaisGridView.DataMember = "Produto_Opcao";
            }
            else if (!AdicionaisGridView.Columns.Contains("CodOpcao"))
            {
                AdicionaisGridView.Columns.Add("CodOpcao", "CodOpcao");
                AdicionaisGridView.Columns.Add("Preco", "Preco");
                AdicionaisGridView.Columns.Add("Nome", "Nome");
                AdicionaisGridView.Columns.Add("Tipo", "Tipo");
                AdicionaisGridView.Columns.Add("CodTipo", "CodTipo");
            }

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            ListaOpcaoProduto();
        }

        private void EditarLinha(object sender, MouseEventArgs e)
        {

        }

        private void AdicionaisGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowIndex = e.RowIndex;
                //  rowIndex = this.AdicionaisGridView.SelectedRows[e.RowIndex].Index;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possivel selecionar a linha " + erro.Message);
            }


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                codigoOpcao = int.Parse(this.AdicionaisGridView.Rows[rowIndex].Cells[0].Value.ToString());
                txtPrecoOpcao.Text = AdicionaisGridView.Rows[rowIndex].Cells[1].Value.ToString();
                this.cbxOpcao.SelectedText = this.AdicionaisGridView.Rows[rowIndex].Cells[2].Value.ToString();
                codTipo = int.Parse(AdicionaisGridView.Rows[rowIndex].Cells["Tipo"].Value.ToString());

                this.btnAdicionarOpcao.Text = "Salvar";
                this.btnAdicionarOpcao.Click += new System.EventHandler(this.SalvarRegistro);
                this.btnAdicionarOpcao.Click -= new System.EventHandler(this.AdicionarOpcao);

                this.btnEditar.Text = "Cancelar";
                this.btnEditar.Click += new System.EventHandler(this.Cancelar);
                this.btnEditar.Click -= new System.EventHandler(this.btnEditar_Click);

            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }
        private void SalvarRegistroInsumo(object sender, EventArgs e)
        {
            try
            {
                InsumoProduto ins = new InsumoProduto()
                {
                    Codigo = codigoinsumo,
                    CodInsumo = int.Parse(cbxInsumo.SelectedValue.ToString()),
                    CodProduto = codigoProdutoParaAlterar,
                    Quantidade = decimal.Parse(txtQtd.Text)
                };
                con.Update("spAlterarProdutoInsumo", ins);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.Click += new System.EventHandler(this.AdicionarInsumoProduto);
            this.btnAdicionar.Click -= new System.EventHandler(this.SalvarRegistroInsumo);

            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
        }
        private void SalvarRegistro(object sender, EventArgs e)
        {
            Produto_Opcao opcaoProd = new Produto_Opcao()
            {
                CodOpcao = codigoOpcao,
                CodProduto = codigoProdutoParaAlterar,
                DataAlteracao = DateTime.Now,
                Preco = decimal.Parse(txtPrecoOpcao.Text),
                PrecoProcomocao = 0,
                CodTipo = codTipo
            };
            if (txtPrecoOpcao.Text != "")
            {
                con.Update("spAlterarOpcaoProduto", opcaoProd);
                con.AtualizaDataSincronismo("Produto", codigoProdutoParaAlterar, "DataAlteracao");
                txtPrecoOpcao.Text = "";
                cbxOpcao.Text = "";
                Utils.ControlaEventos("Alterar", this.Name);
                this.btnAdicionarOpcao.Text = "Adicionar";
                this.btnAdicionarOpcao.Click += new System.EventHandler(this.AdicionarOpcao);
                this.btnAdicionarOpcao.Click -= new System.EventHandler(this.SalvarRegistro);

                this.btnEditar.Text = "Editar";
                this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
                this.btnEditar.Click -= new System.EventHandler(this.Cancelar);

                Utils.LimpaForm(this);
                ListaOpcaoProduto();
            }
            else
            {
                MessageBox.Show("Campos Obrigatórios não preenchidos", "[xSistemas] Aviso");
            }


        }

        private void AdicionaisGridView_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem Excluir = new MenuItem(" 0 - Excluir Opcao ");

                Excluir.Click += DeletarRegistro;
                m.MenuItems.Add(Excluir);
                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

                ListaOpcaoProduto();

            }



        }
        private void ExcluirOpcaoProduto(int iCodProduto, int iCodOpcao)
        {

        }
        private void DeletarRegistro(object sender, EventArgs e)
        {
            try
            {
                if (!Utils.MessageBoxQuestion("Deseja excluir a Opção " + AdicionaisGridView.Rows[rowIndex].Cells["Nome"].Value + " Do Produto"))
                {
                    return;
                }
                if (codigoProdutoParaAlterar != 0)
                {
                    int intCodOpcao = Convert.ToInt16(AdicionaisGridView.Rows[rowIndex].Cells["CodOpcao"].Value);
                    con.Delete("Produto_Opcao", "spExcluirOpcaoProduto", codigoProdutoParaAlterar, intCodOpcao);
                    ListaOpcaoProduto();
                }
                else
                {
                    AdicionaisGridView.Rows.RemoveAt(rowIndex);

                }
            }
            catch (Exception ERRO)
            {
                MessageBox.Show("Não foi possivel excluir a linha " + ERRO.Message);
            }


        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            //ListaOpcaoProduto();
        }

        private void cbxGrupoProduto_Click(object sender, EventArgs e)
        {

            //this.cbxGrupoProduto.DataSource = con.SelectAll("Grupo", "spObterGrupoAtivo").Tables["Grupo"];
            //this.cbxGrupoProduto.DisplayMember = "NomeGrupo";
            //this.cbxGrupoProduto.ValueMember = "Codigo";
        }

        private void SelecionarImagem(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            opn.Title = "Selecione a imagem pro site";
            opn.CheckFileExists = true;
            opn.Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" + "All files (*.*)|*.*";

            if (opn.ShowDialog() == DialogResult.OK)
            {
                txtcaminhoImage.Text = opn.FileName.ToString();

                if (File.Exists(txtcaminhoImage.Text))
                {
                    imgProduto.Load(txtcaminhoImage.Text);
                }

                con.AtualizaDataSincronismo("Produto", codigoProdutoParaAlterar, "DataFoto");
            }
        }

        private void txtcaminhoImage_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(txtcaminhoImage.Text))
            {
                imgProduto.Load(txtcaminhoImage.Text);
            }
            else
            {
                MessageBox.Show("Arquivo de imagem não existe no caminho " + txtcaminhoImage.Text + " informado, favor verificar");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtcaminhoImage.Text = "";
            imgProduto.Dispose();
            con.AtualizaDataSincronismo("Produto", codigoProdutoParaAlterar, "DataFoto");
        }

        private void btnSair_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdicionaisGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListaOpcaoProduto();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAdicionarGrupo frm = new frmAdicionarGrupo();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //string strValor = Interaction.InputBox("Informe a senha mestre", "[xSistemas]", "", 100, 200);
            //if (strValor == "xAdminx77")
            if (Utils.ImputStringQuestion())
            {
                DataSet dsProduto = con.SelectAll("Produto", "", "select * from Produto");
                for (int i = 0; i < dsProduto.Tables[0].Rows.Count; i++)
                {
                    Produtos prod = new Produtos()
                    {
                        Codigo = dsProduto.Tables[0].Rows[i].Field<int>("Codigo"),
                        AtivoSN = dsProduto.Tables[0].Rows[i].Field<Boolean>("AtivoSN"),
                        CodGrupo = con.RetornaIDCategoria(dsProduto.Tables[0].Rows[i].Field<string>("GrupoProduto")),
                        DataAlteracao = DateTime.Now,
                        DataFimPromocao = dsProduto.Tables[0].Rows[i].Field<DateTime>("DataFimPromocao"),
                        DataInicioPromocao = dsProduto.Tables[0].Rows[i].Field<DateTime>("DataInicioPromocao"),
                        Descricao = dsProduto.Tables[0].Rows[i].Field<string>("DescricaoProduto"),
                        DiaSemana = dsProduto.Tables[0].Rows[i].Field<string>("DiaSemana"),
                        GrupoProduto = dsProduto.Tables[0].Rows[i].Field<string>("GrupoProduto"),
                        MaximoAdicionais = dsProduto.Tables[0].Rows[i].Field<int>("MaximoAdicionais"),
                        Nome = dsProduto.Tables[0].Rows[i].Field<string>("NomeProduto"),
                        OnlineSN = dsProduto.Tables[0].Rows[i].Field<Boolean>("OnlineSN"),
                        Preco = dsProduto.Tables[0].Rows[i].Field<decimal>("PrecoProduto"),
                        PrecoDesconto = dsProduto.Tables[0].Rows[i].Field<decimal>("PrecoDesconto"),

                        //UrlImagem = dsProduto.Tables[0].Rows[i].Field<string>("UrlImagem"),
                    };
                    if (dsProduto.Tables[0].Rows[i].Field<string>("UrlImagem") != "")
                    {
                        prod.UrlImagem = dsProduto.Tables[0].Rows[i].Field<string>("UrlImagem");
                    }
                    else
                    {
                        prod.UrlImagem = "";
                    }

                    con.Update("spAlterarProduto", prod);
                }
            }
            else
            {
                MessageBox.Show("Senha mestra errada");
            }
        }

        private void cbxGrupoProduto_DropDown(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxGrupoProduto, "NomeGrupo", "Codigo", "Grupo", "spObterGrupoAtivo");
        }

        private void cbxTipoOpcao_DropDown(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxTipoOpcao, "Nome", "Codigo", "Produto_OpcaoTipo", "spObterTipoOpcao");

        }
        private void ListaInsumos(object sender, EventArgs e)
        {
            try
            {
                Utils.MontaCombox(cbxInsumo, "Nome", "Codigo", "Insumo", "spObterInsumo");
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        private void ListaInsumos()
        {
            if (codigoProdutoParaAlterar != 0)
            {
                gridInsumo.DataSource = con.SelectInsumoProduto(codigoProdutoParaAlterar);
                gridInsumo.AutoGenerateColumns = true;
                gridInsumo.DataMember = "Produto_Insumo";

                txtPrecoCusto.Text = con.CalculaPrecoInsumo(codigoProdutoParaAlterar).ToString();
            }
            else if (!gridInsumo.Columns.Contains("Codigo"))
            {
                gridInsumo.Columns.Add("Codigo", "Codigo");
                gridInsumo.Columns.Add("Nome", "Nome");
                gridInsumo.Columns.Add("Quantidade", "Quantidade");
                gridInsumo.Columns.Add("Preco", "Preço");
            }
           
        }
        private void AdicionarInsumoProduto(object sender, EventArgs e)
        {
            try
            {
                if (cbxInsumo.SelectedIndex < 0)
                {
                    MessageBox.Show("Selecione o insumo");
                    cbxInsumo.Focus();
                    return;
                }
                if (txtQtd.Text == "")
                {
                    MessageBox.Show("Informe a quantidade do insumo");
                    txtQtd.Focus();
                    return;
                }
            

                for (int i = 0; i < gridInsumo.Rows.Count; i++)
                {
                    if (cbxInsumo.SelectedValue.ToString() == gridInsumo.Rows[i].Cells["Codigo"].Value.ToString())
                    {
                        if (!Utils.MessageBoxQuestion("Esse insumo já esta vinculada a esse produto , deseja adicionar novamente?"))
                        {
                            return;
                        }
                    }
                }
                if (codigoProdutoParaAlterar != 0)
                {
                    InsumoProduto insPro = new InsumoProduto()
                    {
                        CodInsumo = int.Parse(cbxInsumo.SelectedValue.ToString()),
                        CodProduto = codigoProdutoParaAlterar,
                        Quantidade = int.Parse(txtQtd.Text)
                    };
                    con.Insert("spAdicionarProdutoInsumo", insPro);
                    ListaInsumos();
                    txtPrecoCusto.Text = con.CalculaPrecoInsumo(codigoProdutoParaAlterar).ToString();
                }
                else
                {
                    int iCountLinhas = 0;
                    if (gridInsumo.DataSource != null)
                    {
                        gridInsumo.AutoGenerateColumns = false;
                        gridInsumo.DataSource = null;
                        gridInsumo.DataMember = null;
                    }

                    gridInsumo.Rows.Add();
                    gridInsumo.Rows[iCountLinhas].Cells["CodInsumo"].Value = int.Parse(cbxInsumo.SelectedValue.ToString());
                    gridInsumo.Rows[iCountLinhas].Cells["Quantidade"].Value = decimal.Parse(txtQtd.Text);
                    gridInsumo.Rows[iCountLinhas].Cells["Nome"].Value = cbxInsumo.Text.ToString();
                    iCountLinhas = gridInsumo.Rows.Count;
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void txtPrecoInsumo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }
        private void DeletarInsumo(object sender, EventArgs e)
        {
            string iNomeInsumo = gridInsumo.CurrentRow.Cells["Nome"].Value.ToString();
            if (!Utils.MessageBoxQuestion("Deseja excluir o " + iNomeInsumo + " da lista de insumo?"))
            {
                return;
            }
            
            if (gridInsumo.SelectedRows.Count > 0)
            {
                int codRegistro = int.Parse(this.gridInsumo.CurrentRow.Cells["Codigo"].Value.ToString());
                con.DeleteAll("Produto_Insumo", "spExcluirProdutoInsumo", codRegistro);
                Utils.ControlaEventos("Excluir", this.Name);
                MessageBox.Show("Item excluído com sucesso.");
                ListaInsumos();

            }
            else
            {
                MessageBox.Show("Selecione o grupo para excluir");
            }

        }
        private void MenuAuxiliar(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem ExcluirRegistro = new MenuItem("Excluir Registro");
                ExcluirRegistro.Click += DeletarInsumo;
                m.MenuItems.Add(ExcluirRegistro);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }
        }

        private void txtQtd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void EditarRegistro(object sender, MouseEventArgs e)
        {
            try
            {
                if (gridInsumo.CurrentRow.Cells[0].Value.ToString()==null)
                {
                    MessageBox.Show("Selecione o registro para editar");
                    return; 
                }
                codigoinsumo = int.Parse(gridInsumo.CurrentRow.Cells["Codigo"].Value.ToString());
                cbxInsumo.Text = gridInsumo.CurrentRow.Cells["Nome"].Value.ToString();
                txtQtd.Text = gridInsumo.CurrentRow.Cells["Quantidade"].Value.ToString();


                this.btnAdicionar.Text = "Salvar";
                this.btnAdicionar.Click += new System.EventHandler(this.SalvarRegistroInsumo);
                this.btnAdicionar.Click -= new System.EventHandler(this.AdicionarInsumoProduto);

                this.btnEditarIns.Text = "Cancelar";
                this.btnEditarIns.Click += new System.EventHandler(this.Cancelar);
                this.btnEditarIns.Click -= new System.EventHandler(this.btnEditarIns_Click);

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btnEditarIns_Click(object sender, EventArgs e)
        {
            btnEditar_Click(sender, e);
        }

        private void precoProdutoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }

        private void txtPrecoCusto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }

        private void txtMarkup_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }

        private void txtMaxAdicionais_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrecoDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoDecimais(e);
        }

        private void txtMarkup_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPrecoCusto.Text == "")
                {
                    return;
                }
                decimal dPrecoCusto = decimal.Parse(txtPrecoCusto.Text);
                decimal dPrecoSugerido = dPrecoCusto + decimal.Parse(txtPrecoCusto.Text)* (decimal.Parse(txtMarkup.Text)/100);
                txtPrecoSugerido.Text = dPrecoSugerido.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void txtPrecoCusto_Leave(object sender, EventArgs e)
        {
            txtMarkup_Leave(sender, e);
        }

   
    }
}
