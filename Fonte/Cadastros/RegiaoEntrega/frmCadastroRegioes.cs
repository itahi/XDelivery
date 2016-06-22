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
    public partial class frmCadastroRegioes : Form
    {
        Conexao con;
        int rowIndex;
        int codigo;

        public frmCadastroRegioes()
        {
            InitializeComponent();
            con = new Conexao();
            ListaRegioes();
        }

        private void Acao(object sender, EventArgs e)
        {

        }
        private void ListaRegioes()
        {
            this.RegioesGridView.DataSource = null;
            this.RegioesGridView.AutoGenerateColumns = true;
            this.RegioesGridView.DataSource = con.SelectAll("RegiaoEntrega", "spObterRegioes");
            this.RegioesGridView.DataMember = "RegiaoEntrega";

        }

        private void ConsultarCEP(object sender, KeyEventArgs e)
        {

        }

        private void RegioesGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowIndex = e.RowIndex;
            }
            catch (Exception)
            {

                throw;
            }

            

        }

        private void Editar(object sender, EventArgs e)
        {
            codigo = int.Parse(this.RegioesGridView.Rows[rowIndex].Cells["Codigo"].Value.ToString());
            txtEntrega.Text = (this.RegioesGridView.Rows[rowIndex].Cells["TaxaServico"].Value.ToString());
            txtRegiao.Text = (this.RegioesGridView.Rows[rowIndex].Cells["NomeRegiao"].Value.ToString());
            chkAtivo.Checked =Convert.ToBoolean(RegioesGridView.Rows[rowIndex].Cells["AtivoSN"].Value.ToString());
            chkOnline.Checked = Convert.ToBoolean(RegioesGridView.Rows[rowIndex].Cells["OnlineSN"].Value.ToString());
            txtTaxaGratis.Text = RegioesGridView.Rows[rowIndex].Cells["valorMinimoFreteGratis"].Value.ToString();

            this.btnSalvar.Text = "Salvar [F12]";
            this.btnSalvar.Click += new System.EventHandler(this.SalvarRegiao);
            this.btnSalvar.Click -= new System.EventHandler(this.AdicionarRegiao);

            this.btnEditar.Text = "Cancelar [ESC]";
            this.btnEditar.Click += new System.EventHandler(this.Cancelar);
            this.btnEditar.Click -= new System.EventHandler(this.Editar);
        }

        private void SalvarRegiao(object sender, EventArgs e)
        {
            try
            {
                double valorFrete = 0;
                if (txtRegiao.Text.Trim() != "" || txtEntrega.Text.Trim() != "")
                {
                    RegioesEntrega regioes = new RegioesEntrega()
                    {
                        Codigo = codigo,
                        NomeRegiao = txtRegiao.Text,
                        TaxaServico = Convert.ToDecimal(this.txtEntrega.Text.Replace(".", ",")),
                        DataAlteracao = DateTime.Now,
                        OnlineSN = chkOnline.Checked,
                        AtivoSN = chkAtivo.Checked
                        
                    };
                    if (txtTaxaGratis.Text != "")
                    {
                        valorFrete = double.Parse(txtTaxaGratis.Text);
                    }
                    regioes.valorMinimoFreteGratis = valorFrete;

                    con.Update("spAlteraRegiao", regioes);
                    
                    Utils.ControlaEventos("Altera", this.Name);
                    this.btnSalvar.Text = "Adicionar [F12]";
                    this.btnSalvar.Click += new System.EventHandler(this.AdicionarRegiao);
                    this.btnSalvar.Click -= new System.EventHandler(this.SalvarRegiao);

                    this.btnEditar.Text = "Editar [F11]";
                    this.btnEditar.Click += new System.EventHandler(this.Editar);
                    this.btnEditar.Click -= new System.EventHandler(this.Cancelar);

                    Utils.LimpaForm(this);
                    ListaRegioes();
                }
                else
                {
                    MessageBox.Show("Preencha corretamento os campos para continuar", "Dex Aviso");
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.InnerException.Message);
            }


        }
        private void Cancelar(object sender, EventArgs e)
        {

            Button iButton = (Button)sender;

            if (iButton.Name == "btnEditar")
            {
                Utils.LimpaForm(this);
            }
            this.btnSalvar.Text = "Adicionar [F12]";
            this.btnSalvar.Click += new System.EventHandler(this.AdicionarRegiao);
            this.btnSalvar.Click -= new System.EventHandler(this.SalvarRegiao);

            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.Click += new System.EventHandler(this.Editar);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
        }

        private void frmCadastroRegioes_Load(object sender, EventArgs e)
        {

        }

        private void AdicionarRegiao(object sender, EventArgs e)
        {
            try
            {
                double valorFrete = 0;
                if (txtRegiao.Text.Trim() != "" || txtEntrega.Text.Trim() != "")
                {
                    RegioesEntrega regioes = new RegioesEntrega()
                    {
                        NomeRegiao = txtRegiao.Text,
                        TaxaServico = Convert.ToDecimal(this.txtEntrega.Text.Replace(".", ",")),
                        DataAlteracao = DateTime.Now,
                        OnlineSN = chkOnline.Checked,
                        AtivoSN = chkAtivo.Checked,
                      //  valorMinimoFreteGratis = valorFrete

                    };
                    if (txtTaxaGratis.Text!="")
                    {
                        valorFrete = double.Parse(txtTaxaGratis.Text);
                    }
                    regioes.valorMinimoFreteGratis = valorFrete;

                    con.Insert("spAdicionaRegiao", regioes);
                    Utils.ControlaEventos("Inserir", this.Name);
                    Utils.LimpaForm(this);
                    txtRegiao.Focus();
                    ListaRegioes();
                }
                else
                {
                    MessageBox.Show("Preencha corretamento os campos para continuar", "Dex Aviso");
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message, "Dex Aviso");
            }
        }

        private void frmCadastroRegioes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12 && btnSalvar.Text == "Adicionar [F12]")
            {
                AdicionarRegiao(sender, e);
            }
            else if (e.KeyCode == Keys.F12 && btnSalvar.Text == "Salvar [F12]")
            {
                SalvarRegiao(sender, e);
            }
            else if (e.KeyCode == Keys.F11 && btnEditar.Text == "Editar [F11]")
            {
                Editar(sender, e);
            }
        }
    }
}
