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
            DiasSelecionados = new List<string>();
            grpDesconto.Visible = DescontoPordia;

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
                        chkDomingo.Checked = true;
                    }
                }


            }

        }

        private void frmCadastrarProduto_Load(object sender, EventArgs e)
        {
            con = new Conexao();
            DiasSelecionados = new List<string>();
            grpDesconto.Visible = DescontoPordia;
            List<Grupo> grupos = new List<Grupo>();

            this.cbxGrupoProduto.DataSource = con.SelectAll("Grupo", "spObterGrupo").Tables["Grupo"];
            this.cbxGrupoProduto.DisplayMember = "NomeGrupo";
            this.cbxGrupoProduto.ValueMember = "Codigo";

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
                Produto produto = new Produto()
                {
                    Nome = this.nomeProdutoTextBox.Text,
                    AtivoSN = this.chkAtivo.Checked,
                    Descricao = this.descricaoProdutoTextBox.Text,
                    Preco = Convert.ToDecimal(this.precoProdutoTextBox.Text.Replace(".", ",")),
                    GrupoProduto = this.cbxGrupoProduto.Text
                };

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
                this_FormClosing();
                MessageBox.Show("Produto cadastrado com sucesso.");
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
                Marcados = Marcados + ";Saturday";
            }
            if (chkDomingo.Checked)
            {
                Marcados = Marcados + ";Sunday";
            }

            return Marcados;
        }

        public void AlterarProduto(object sender, EventArgs e)
        {
            try
            {
                var nome = this.nomeProdutoTextBox.Text;
                var descricao = this.descricaoProdutoTextBox.Text;
                var preco = Convert.ToDecimal(this.precoProdutoTextBox.Text.Replace(".", ","));
                var grupoProduto = this.cbxGrupoProduto.Text;
                var AtivoS = this.chkAtivo.Checked;

                Produto produto = new Produto()
                {
                    Codigo = codigoProdutoParaAlterar,
                    Nome = nome,
                    Descricao = descricao,
                    Preco = preco,
                    GrupoProduto = grupoProduto,
                    AtivoSN = AtivoS
                };
                if (DescontoPordia)
                {
                    produto.DiaSemana = DiasSelecinado();
                    produto.PrecoDesconto = decimal.Parse(txtPrecoDesconto.Text.Replace(".", ","));

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
                ClearForm(this);
                this_FormClosing();
                Utils.ControlaEventos("Alterar", this.Name);
                MessageBox.Show("Produto alterado com sucesso.");

                nomeProdutoTextBox.Text = "";
                txtPrecoDesconto.Text = "";
                this.nomeProdutoTextBox.Focus();
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
        private void this_FormClosing()
        {
            if (PromocaoDiasSemana)
            {
                Utils.PopularGrid("Produto", this.parentMain.produtosGridView);
            }
            else
            {
                Utils.PopularGrid("Produto", this.parentMain.produtosGridView, "spObterProdutoSemDesconto");
            }
            //this.parentMain.PopularGrid("Produto", this.parentMain.produtosGridView);
            //this.Dispose();
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
    }
}
