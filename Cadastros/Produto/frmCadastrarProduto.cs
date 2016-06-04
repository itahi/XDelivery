using DexComanda.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda
{
    public partial class frmCadastrarProduto : Form
    {
        private Conexao con;
        private Main parentMain;
        private int rowIndex;
        int codigoOpcao;
        private Produto produto;
        private int codigoProdutoParaAlterar;
        private bool DescontoPordia = Sessions.returnConfig.DescontoDiaSemana;
        private List<string> DiasSelecionados;
        private string Marcados;
        private bool PromocaoDiasSemana = Sessions.returnConfig.DescontoDiaSemana;

        public frmCadastrarProduto(Main parent)
        {
            InitializeComponent();
            this.parentMain = parent;
            //  parentMain = new Main();
            DiasSelecionados = new List<string>();
            grpDesconto.Visible = DescontoPordia;

        }
        public frmCadastrarProduto(int CodProduto, string iNomeProduto, string iGrupo, decimal iPreco, string iDescricao, bool iVendaOnline,
                                   decimal iPrecoPromocao, string iDiasPromocao, string iMaximoAdicionais, string iUrlImagem, DateTime idtInicioPromo, DateTime idtFimPromo)
        {
            InitializeComponent();
            codigoProdutoParaAlterar = CodProduto;
            if (iDiasPromocao != "")
            {
                grpDesconto.Visible = DescontoPordia;
                txtPrecoDesconto.Text = iPrecoPromocao.ToString();

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

            }
            nomeProdutoTextBox.Text = iNomeProduto;
            precoProdutoTextBox.Text = iPreco.ToString();
            cbxGrupoProduto.Text = iGrupo;
            descricaoProdutoTextBox.Text = iDescricao;
            chkAtivo.Checked = true;
            chkOnline.Checked = iVendaOnline;
            txtMaxAdicionais.Text = iMaximoAdicionais;
            txtcaminhoImage.Text = iUrlImagem;
            dtInicio.Value = idtInicioPromo;
            dtFim.Value = idtFimPromo;
            this.btnDoProduto.Enabled = Sessions.retunrUsuario.AlteraProdutosSN;
            this.btnDoProduto.Text = "Alterar [F12]";
            this.btnDoProduto.Click -= AdicionarProduto;
            this.btnDoProduto.Click += AlterarProduto;
        }

        public frmCadastrarProduto(Produto prod, Main parent)
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

        private void frmCadastrarProduto_Load(object sender, EventArgs e)
        {
            con = new Conexao();

            btnEditar.Enabled = codigoProdutoParaAlterar != 0;
            DiasSelecionados = new List<string>();
            tabPage2.IsAccessible = btnDoProduto.Text == "Alterar [F12]";
            //    List<Produtos_Adicionais> ProdAdicionais = new List<Produtos_Adicionais>();
            grpDesconto.Visible = DescontoPordia;
            List<Grupo> grupos = new List<Grupo>();
            
            if (produto != null)
            {
                if (Sessions.retunrUsuario != null)
                {
                    this.btnDoProduto.Enabled = Sessions.retunrUsuario.AlteraProdutosSN;
                }
                else
                {
                    this.btnDoProduto.Enabled = true;
                }

                this.btnDoProduto.Text = "Alterar [F12]";
                this.btnDoProduto.Click -= AdicionarProduto;
                this.btnDoProduto.Click += AlterarProduto;

                codigoProdutoParaAlterar = produto.Codigo;
                this.nomeProdutoTextBox.Text = produto.Nome;
                this.cbxGrupoProduto.Text = produto.GrupoProduto;
                this.precoProdutoTextBox.Text = produto.Preco.ToString();
                this.descricaoProdutoTextBox.Text = produto.Descricao.ToString();
                this.chkAtivo.Checked = produto.AtivoSN;
            }

        }


        private void AdicionarProduto(object sender, EventArgs e)
        {
            try
            {
                if (nomeProdutoTextBox.Text.Trim() == "" || precoProdutoTextBox.Text.Trim() == "" || cbxGrupoProduto.Text.Trim() == "")
                {
                    MessageBox.Show("Campos obrigátórios não preenchidos");
                    return;
                }
                Produto produto = new Produto()
                {
                    Codigo = 0,
                    Nome = this.nomeProdutoTextBox.Text,
                    AtivoSN = this.chkAtivo.Checked,
                    Descricao = this.descricaoProdutoTextBox.Text,
                    Preco = Convert.ToDecimal(this.precoProdutoTextBox.Text.Replace(".", ",")),
                    GrupoProduto = this.cbxGrupoProduto.Text,
                    OnlineSN = chkOnline.Checked,
                    DataInicioPromocao = Convert.ToDateTime(dtInicio.Value.ToShortDateString()),
                    DataFimPromocao = Convert.ToDateTime(dtFim.Value.ToShortDateString()),
                    DataAlteracao = DateTime.Now
                };
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
                if (DescontoPordia)
                {
                    produto.DiaSemana = DiasSelecinado();
                    if (txtPrecoDesconto.Text != "")
                    {
                        produto.PrecoDesconto = decimal.Parse(txtPrecoDesconto.Text.Replace(".", ","));
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
                Utils.PopulaGrid_Novo("Produto", parentMain.produtosGridView, Sessions.SqlProduto);
                Utils.ControlaEventos("Inserir", this.Name);
                nomeProdutoTextBox.Text = "";
                txtPrecoDesconto.Text = "";
                this.nomeProdutoTextBox.Focus();

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
                        DataAlteracao = DateTime.Now
                    };
                    con.Insert("spAdicionarOpcaProduto", prod);
                }

                AdicionaisGridView.DataSource = null;
                AdicionaisGridView.DataMember = null;
                AdicionaisGridView.AutoGenerateColumns = false;
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
                if (nomeProdutoTextBox.Text.Trim() == "" || precoProdutoTextBox.Text.Trim() == "" || cbxGrupoProduto.Text.Trim() == "")
                {
                    MessageBox.Show("Campos obrigátórios não preenchidos");
                    return;
                }
                Produto produto = new Produto()
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
                };
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
                if (DescontoPordia)
                {
                    produto.DiaSemana = DiasSelecinado();
                    if (txtPrecoDesconto.Text != "")
                    {
                        produto.PrecoDesconto = decimal.Parse(txtPrecoDesconto.Text.Replace(".", ","));
                    }

                    con.Update("spAlterarProduto", produto);
                }
                else
                {
                    produto.DiaSemana = "";
                    produto.PrecoDesconto = decimal.Parse("0");

                    con.Update("spAlterarProduto", produto);
                }

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
            ClearForm(this);
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

            this.cbxOpcao.DataSource = con.SelectAll("Opcao", "spObterOpcao").Tables["Opcao"];
            this.cbxOpcao.DisplayMember = "Nome";
            this.cbxOpcao.ValueMember = "Codigo";
        }

        private void AdicionarOpcao(object sender, EventArgs e)
        {
            try
            {
                int iCountLinhas = AdicionaisGridView.Rows.Count;
                if (codigoProdutoParaAlterar != 0)
                {
                    if (txtPrecoOpcao.Text.Trim() != "")
                    {
                        Produto_Opcao prodOpcao = new Produto_Opcao()
                        {
                            CodProduto = codigoProdutoParaAlterar,
                            CodOpcao = int.Parse(cbxOpcao.SelectedValue.ToString()),
                            Preco = decimal.Parse(txtPrecoOpcao.Text),
                            DataAlteracao = DateTime.Now
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
                    AdicionaisGridView.Rows[iCountLinhas].Cells[0].Value = int.Parse(cbxOpcao.SelectedValue.ToString());
                    AdicionaisGridView.Rows[iCountLinhas].Cells[1].Value = decimal.Parse(txtPrecoOpcao.Text);
                    AdicionaisGridView.Rows[iCountLinhas].Cells[2].Value = cbxOpcao.Text.ToString();
                    iCountLinhas = AdicionaisGridView.Rows.Count;
                }
            }
            catch (Exception erro)
            {

                throw;
            }






        }

        private void ListaOpcaoProduto()
        {
            if (codigoProdutoParaAlterar!=0)
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
            //    try
            //    {
            //        rowIndex = e.RowIndex;
            //        //  rowIndex = this.AdicionaisGridView.SelectedRows[e.RowIndex].Index;
            //    }
            //    catch (Exception erro)
            //    {
            //        MessageBox.Show("Não foi possivel selecionar a linha "+ erro.Message);
            //    }


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            codigoOpcao = int.Parse(this.AdicionaisGridView.SelectedRows[rowIndex].Cells[0].Value.ToString());
            txtPrecoOpcao.Text = AdicionaisGridView.SelectedRows[rowIndex].Cells[1].Value.ToString();
            this.cbxOpcao.SelectedText = this.AdicionaisGridView.SelectedRows[rowIndex].Cells[2].Value.ToString();


            this.btnAdicionarOpcao.Text = "Salvar";
            this.btnAdicionarOpcao.Click += new System.EventHandler(this.SalvarRegistro);
            this.btnAdicionarOpcao.Click -= new System.EventHandler(this.AdicionarOpcao);

            this.btnEditar.Text = "Cancelar";
            this.btnEditar.Click += new System.EventHandler(this.Cancelar);
            this.btnEditar.Click -= new System.EventHandler(this.SalvarRegistro);
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
                this.btnEditar.Click += new System.EventHandler(this.SalvarRegistro);
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
            this.cbxGrupoProduto.DataSource = con.SelectAll("Grupo", "spObterGrupoAtivo").Tables["Grupo"];
            this.cbxGrupoProduto.DisplayMember = "NomeGrupo";
            this.cbxGrupoProduto.ValueMember = "Codigo";
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

                imgProduto.Load(txtcaminhoImage.Text);
                con.AtualizaDataSincronismo("Produto", codigoProdutoParaAlterar, "DataFoto");
            }
        }

        private void txtcaminhoImage_TextChanged(object sender, EventArgs e)
        {
            if (txtcaminhoImage.Text != "")
            {
                imgProduto.Load(txtcaminhoImage.Text);
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

        private void AddGrupo(object sender, EventArgs e)
        {
            frmAdicionarGrupo frm = new frmAdicionarGrupo();
            frm.Show();
        }
    }
}
