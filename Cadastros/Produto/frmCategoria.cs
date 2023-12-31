﻿using DexComanda.Models;
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
            pnlImpressora.Enabled = chkImprimeCozinha.Checked;
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
                    OnlineSN =chkOnline.Checked,
                    DataAlteracao = DateTime.Now
                };
                if (chkImprimeCozinha.Checked)
                {
                    grupo.NomeImpressora = cbxNomeImpressora.Text;
                }
                else
                {
                    grupo.NomeImpressora = "";
                }

                if (txbNomeGrupo.Text !="")
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
            catch
            {
                MessageBox.Show("Ocorreu um erro na gravaçao do registro","Dex Aviso");
            }
        }

        private void EditarGrupo(object sender, EventArgs e)
        {

            codigo = int.Parse(this.gruposGridView.SelectedRows[rowIndex].Cells[0].Value.ToString());
            this.txbNomeGrupo.Text = this.gruposGridView.SelectedRows[rowIndex].Cells[1].Value.ToString();
            chkImprimeCozinha.Checked =Convert.ToBoolean( this.gruposGridView.SelectedRows[rowIndex].Cells[2].Value.ToString());
            cbxNomeImpressora.Text = this.gruposGridView.SelectedRows[rowIndex].Cells["NomeImpressora"].Value.ToString();
            chkOnline.Checked = Convert.ToBoolean(this.gruposGridView.SelectedRows[rowIndex].Cells[3].Value.ToString());
            chkAtivo.Checked = Convert.ToBoolean(this.gruposGridView.SelectedRows[rowIndex].Cells[4].Value.ToString());

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
                DataAlteracao = DateTime.Now

            };
            if (chkImprimeCozinha.Checked)
            {
                grupo.NomeImpressora = cbxNomeImpressora.Text;
            }
            else
            {
                grupo.NomeImpressora = "";
            }

            if (txbNomeGrupo.Text !="")
            {
                con.Update("spAlterarGrupo", grupo);
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
            this.gruposGridView.DataSource = con.SelectAll("Grupo", "spObterGrupo");
            this.gruposGridView.DataMember = "Grupo";

        }

        private void txbNomeGrupo_TextChanged(object sender, EventArgs e)
        {

        }

        private void gruposGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int total = this.gruposGridView.SelectedRows.Count;

            for (int i = 0; i < total; i++)
            {
                if (this.gruposGridView.Rows[i].Selected)
                {
                    rowIndex = this.gruposGridView.Rows[i].Index;
                }
            }

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
            else if (btnEditarGrupo.Text =="Cancelar [ESC]" && e.KeyCode == Keys.Escape)
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
    }
}

