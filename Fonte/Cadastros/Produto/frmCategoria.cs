using DexComanda.Models;
using DexComanda.Models.WS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DexComanda
{
    public partial class frmAdicionarGrupo : Form
    {

        Conexao con;
        int rowIndex;
        int codigo;

        public frmAdicionarGrupo()
        {
            InitializeComponent();
            con = new Conexao();
            ExibirGrupos();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            grpMultiplo.Enabled = chkMultiSabores.Checked;
            Utils.MontaCombox(cbxFamilia, "NomeGrupo", "Codigo", "Grupo", "spObterFamilia");
            pnlImpressora.Enabled = chkImprimeCozinha.Checked;
        }

        private string MultiploSabores()
        {
            string iRetur="";
            if (chkMultiSabores.Checked)
            {
                MultiSabores mult = new MultiSabores()
                {
                    Permite = true,
                    QuantidadePermitida = cbxNumeroSabores.Text.ToString()
                };
                iRetur = Utils.SerializaObjeto(mult);
            }
            return iRetur;
        }
        private void AdicionarGrupo(object sender, EventArgs e)
        {
            try
            {
                Grupo grupo = new Grupo()
                {
                    NomeGrupo = this.txbNomeGrupo.Text,
                    ImprimeCozinhaSN = chkImprimeCozinha.Checked,
                    AtivoSN = chkAtivo.Checked,
                    OnlineSN = chkOnline.Checked,
                    DataAlteracao = DateTime.Now,
                    MultiploSabores = chkMultiSabores.Checked
                };
                if (chkImprimeCozinha.Checked)
                {
                    grupo.NomeImpressora = cbxNomeImpressora.Text;
                }
                else
                {
                    grupo.NomeImpressora = "";
                }
                if (cbxFamilia.SelectedValue != null)
                {
                    grupo.CodFamilia = int.Parse(cbxFamilia.SelectedValue.ToString());
                }
                else
                {
                    grupo.CodFamilia = 0;
                }

                if (txbNomeGrupo.Text != "")
                {
                    con.Insert("spAdicionarGrupo", grupo);
                    Utils.ControlaEventos("Inserir", this.Name);
                    Utils.LimpaForm(this);
                    ExibirGrupos();
                }
                else
                {
                    MessageBox.Show("Preenchar o Nome do Grupo para prosseguir", "Dex Aviso");
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show("Ocorreu um erro na gravaçao do registro " + erro.Message, "xSistemas Aviso");
            }
        }
        
        private void EditarGrupo(object sender, EventArgs e)
        {

            codigo = int.Parse(this.gruposGridView.Rows[rowIndex].Cells[0].Value.ToString());
            this.txbNomeGrupo.Text = this.gruposGridView.Rows[rowIndex].Cells[1].Value.ToString();
            chkImprimeCozinha.Checked = Convert.ToBoolean(this.gruposGridView.Rows[rowIndex].Cells[2].Value.ToString());
            cbxNomeImpressora.Text = this.gruposGridView.Rows[rowIndex].Cells["NomeImpressora"].Value.ToString();
            chkOnline.Checked = Convert.ToBoolean(this.gruposGridView.Rows[rowIndex].Cells[3].Value.ToString());
            chkAtivo.Checked = Convert.ToBoolean(this.gruposGridView.Rows[rowIndex].Cells[4].Value.ToString());

            chkMultiSabores.Checked = gruposGridView.Rows[rowIndex].Cells["MultiploSabores"].Value.ToString() == "1";
            //MultiSabores multi = new MultiSabores();
            //multi = Utils.DeserializaObjeto4(gruposGridView.Rows[rowIndex].Cells["MultiploSabores"].Value.ToString());
            //if (multi!=null)
            //{
            //    chkMultiSabores.Checked = multi.Permite;
            //    cbxNumeroSabores.Text = multi.QuantidadePermitida;
            //}

            if (gruposGridView.Rows[rowIndex].Cells["CodFamilia"].Value.ToString() !="0")
            {
                Utils.MontaCombox(cbxFamilia, "NomeGrupo", "Codigo", "Grupo", "spObterFamiliaPorCodigo", int.Parse(gruposGridView.Rows[rowIndex].Cells["CodFamilia"].Value.ToString()));
            }
            else
            {
                Utils.MontaCombox(cbxFamilia, "NomeGrupo", "Codigo", "Grupo", "spObterFamilia");
                cbxFamilia.SelectedIndex = -1;
            }

            this.btnAdicionarGrupo.Text = "Salvar [F12]";
            this.btnAdicionarGrupo.Click += new System.EventHandler(this.SalvarGrupo);
            this.btnAdicionarGrupo.Click -= new System.EventHandler(this.AdicionarGrupo);

            this.btnEditarGrupo.Text = "Cancelar [ESC]";
            this.btnEditarGrupo.Click += new System.EventHandler(this.Cancelar);
            this.btnEditarGrupo.Click -= new System.EventHandler(this.EditarGrupo);

        }
        private void Cancelar(object sender, EventArgs e)
        {

            Button iButton = (Button)sender;

            if (iButton.Name == "btnEditarGrupo")
            {
                this.txbNomeGrupo.Text = "";
            }
            this.btnAdicionarGrupo.Text = "Adicionar";
            this.btnAdicionarGrupo.Click += new System.EventHandler(this.AdicionarGrupo);
            this.btnAdicionarGrupo.Click -= new System.EventHandler(this.SalvarGrupo);

            this.btnEditarGrupo.Text = "Editar";
            this.btnEditarGrupo.Click += new System.EventHandler(this.EditarGrupo);
            this.btnEditarGrupo.Click -= new System.EventHandler(this.Cancelar);
        }

        private void SalvarGrupo(object sender, EventArgs e)
        {
            Grupo grupo = new Grupo()
            {
                Codigo = codigo,
                NomeGrupo = this.txbNomeGrupo.Text,
                ImprimeCozinhaSN = chkImprimeCozinha.Checked,
                OnlineSN = chkOnline.Checked,
                AtivoSN = chkAtivo.Checked,
                DataAlteracao = DateTime.Now,
                MultiploSabores = chkMultiSabores.Checked

            };
            if (chkImprimeCozinha.Checked)
            {
                grupo.NomeImpressora = cbxNomeImpressora.Text;
            }
            else
            {
                grupo.NomeImpressora = "";
            }
            if (cbxFamilia.SelectedValue != null)
            {
                grupo.CodFamilia = int.Parse(cbxFamilia.SelectedValue.ToString());
            }
            else
            {
                grupo.CodFamilia = 0;
            }

            if (txbNomeGrupo.Text != "")
            {
                con.Update("spAlterarGrupo", grupo);

                ProdutoGrupo prod = new ProdutoGrupo()
                {
                    AtivoSN = chkAtivo.Checked,
                    OnlineSN = chkOnline.Checked,
                    CodGrupo = grupo.Codigo
                };
                //   con.Update("spAlterarProdutoPorGrupo", prod);
                Utils.ControlaEventos("Alterar", this.Name);
                this.btnAdicionarGrupo.Text = "Adicionar [F12]";
                this.btnAdicionarGrupo.Click += new System.EventHandler(this.AdicionarGrupo);
                this.btnAdicionarGrupo.Click -= new System.EventHandler(this.SalvarGrupo);

                this.btnEditarGrupo.Text = "Editar [F11]";
                this.btnEditarGrupo.Click += new System.EventHandler(this.EditarGrupo);
                this.btnEditarGrupo.Click -= new System.EventHandler(this.Cancelar);

                this.txbNomeGrupo.Text = "";
                ExibirGrupos();
            }
            else
            {
                MessageBox.Show("Preencha o nome para continuar", "Dex Aviso");
            }


        }

        private void ExibirGrupos()
        {
            this.gruposGridView.DataSource = null;
            this.gruposGridView.AutoGenerateColumns = true;
            this.gruposGridView.DataSource = Utils.PopularGrid_SP("Grupo", gruposGridView, "spObterGrupo");
            this.gruposGridView.DataMember = "Grupo";
        }

        private void txbNomeGrupo_TextChanged(object sender, EventArgs e)
        {

        }

        private void gruposGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            rowIndex = e.RowIndex;

        }

        private void DeletaGrupo(object sender, EventArgs e)
        {
            if (gruposGridView.SelectedRows.Count > 0)
            {
                int CodGrupo = int.Parse(this.gruposGridView.SelectedCells[0].Value.ToString());
                con.DeleteAll("Grupo", "spExcluirGrupo", CodGrupo);
                Utils.ControlaEventos("Excluir", this.Name);
                MessageBox.Show("Item excluído com sucesso.");
                ExibirGrupos();

            }
            else
            {
                MessageBox.Show("Selecione o grupo para excluir");
            }

        }

        private void gruposGridView_MouseClick_1(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem ExcluirProduto = new MenuItem("Excluir Grupo");
                ExcluirProduto.Click += DeletaGrupo;
                m.MenuItems.Add(ExcluirProduto);

                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));

            }

        }

        private void frmAdicionarGrupo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12 && btnAdicionarGrupo.Text == "Adicionar [F12]")
            {
                AdicionarGrupo(sender, e);
            }
            else if (btnAdicionarGrupo.Text == "Salvar [F12]" && e.KeyCode == Keys.F12)
            {
                SalvarGrupo(sender, e);
            }

            else if (e.KeyCode == Keys.F11 && btnEditarGrupo.Text == "Editar [F11]")
            {
                EditarGrupo(sender, e);
            }
            else if (btnEditarGrupo.Text == "Cancelar [ESC]" && e.KeyCode == Keys.Escape)
            {
                Cancelar(sender, e);
            }
        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            foreach (string impressora in PrinterSettings.InstalledPrinters)
            {
                cbxNomeImpressora.Items.Add(impressora);
            }
        }

        private void chkImprimeCozinha_CheckedChanged(object sender, EventArgs e)
        {
            //  cbxNomeImpressora.Enabled = chkImprimeCozinha.Checked;
            //btnLista.Enabled = chkImprimeCozinha.Checked;
        }

        private void chkImprimeCozinha_CheckedChanged_1(object sender, EventArgs e)
        {
            pnlImpressora.Enabled = chkImprimeCozinha.Checked;
        }

        private void EditarFamilia(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxFamilia, "NomeGrupo", "Codigo", "Grupo", "spObterFamilia");
        }

        private void BuscaFamilia(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Utils.MontaCombox(cbxFamilia, "NomeGrupo", "Codigo", "Grupo", "spObterFamiliaPorCodFamilia", int.Parse(txtCodFamilia.Text));
            }
        }

        private void ListaTodos(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxFamilia, "NomeGrupo", "Codigo", "Grupo", "spObterFamilia");
        }

        private void ListaImpressoras(object sender, EventArgs e)
        {
            cbxNomeImpressora.Items.Clear();
            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                cbxNomeImpressora.Items.Add(item);
            }
        }

        private void chkMultiSabores_CheckedChanged(object sender, EventArgs e)
        {
            grpMultiplo.Enabled = chkMultiSabores.Checked;
        }
    }
}

