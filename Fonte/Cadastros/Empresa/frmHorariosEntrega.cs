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

namespace DexComanda.Cadastros.Empresa
{
    public partial class frmHorariosEntrega : Form
    {
        private Conexao con;
        private int codigo;
        private int rowIndex;
        public frmHorariosEntrega()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private Boolean ValidaHorarios()
        {
            Boolean iRetur = true;
            if (dtLimite.Value> dtEntregaInicio.Value )
            {
                MessageBox.Show("Hora limite não pode ser maior que hora inicio da entrega");
                iRetur = false;
            }
            if (dtLimite.Value> dtEntregaFim.Value)
            {
                MessageBox.Show("Hora limite não pode ser maior que hora fim da entrega");
                iRetur = false;
            }
            if (dtEntregaInicio.Value>dtEntregaFim.Value)
            {
                MessageBox.Show("Hora inicio não pode ser maior que hora fim da entrega");
                iRetur = false;
            }


            return iRetur;
        }
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (!ValidaHorarios())
            {
                return;
            }
            HorarioEntrega horario = new HorarioEntrega()
            {
                Horario_entrega = dtEntregaInicio.Value.ToShortTimeString().ToString() + " às " + dtEntregaFim.Value.ToShortTimeString(),
                Limite_horario_pedido = dtLimite.Value.ToShortTimeString().ToString(),
                OnlineSN = chkOnlineSN.Checked
            };

            con.Insert("spAdicionarEmpresa_HorarioEntrega", horario);
            Utils.PopularGrid("Empresa_HorarioEntrega", gridViewHorarios);
        }

        private void frmHorariosEntrega_Load(object sender, EventArgs e)
        {
            Utils.PopularGrid("Empresa_HorarioEntrega", gridViewHorarios);
        }
        private void Salvar(object sender, EventArgs e)
        {
            if (!ValidaHorarios())
            {
                return;
            }
            HorarioEntrega horario = new HorarioEntrega()
            {
                Codigo = codigo,
                Horario_entrega = dtEntregaInicio.Value.ToShortTimeString().ToString() + " às " + dtEntregaFim.Value.ToShortTimeString(),
                Limite_horario_pedido = dtLimite.Value.ToShortTimeString().ToString(),
                OnlineSN = chkOnlineSN.Checked
            };
            con.Update("spAlterarEmpresa_HorarioEntrega", horario);
            Utils.PopularGrid("Empresa_HorarioEntrega", gridViewHorarios);
            Utils.LimpaForm(this);
            Utils.ControlaEventos("Alterar", this.Name);
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            this.btnAdicionar.Click -= new System.EventHandler(this.Salvar);

            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);

        }
        private void Cancelar(object sender, EventArgs e)
        {

            Button iButton = (Button)sender;
            Utils.LimpaForm(this);
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            this.btnAdicionar.Click -= new System.EventHandler(this.Salvar);

            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
            this.btnEditar.Click -= new System.EventHandler(this.Cancelar);
        }
        private void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                if (gridViewHorarios.SelectedRows.Count > 0)
                {
                    codigo = int.Parse(gridViewHorarios.CurrentRow.Cells[0].Value.ToString());
                    dtLimite.Value = Convert.ToDateTime(gridViewHorarios.CurrentRow.Cells[1].Value.ToString());


                    string text = gridViewHorarios.CurrentRow.Cells[2].Value.ToString().Substring(0, 5);
                    string text2 = gridViewHorarios.CurrentRow.Cells[2].Value.ToString().Substring(9, 5);
                    dtEntregaInicio.Value = DateTime.Parse(gridViewHorarios.CurrentRow.Cells[2].Value.ToString().Substring(0, 5));
                    dtEntregaFim.Value = DateTime.Parse(gridViewHorarios.CurrentRow.Cells[2].Value.ToString().Substring(9, 5));
                    chkOnlineSN.Checked = Boolean.Parse(gridViewHorarios.CurrentRow.Cells[3].Value.ToString());
                    this.btnAdicionar.Text = "Salvar [F12]";
                    this.btnAdicionar.Click += new System.EventHandler(this.Salvar);
                    this.btnAdicionar.Click -= new System.EventHandler(this.btnAdicionar_Click);

                    this.btnEditar.Text = "Cancelar";
                    this.btnEditar.Click += new System.EventHandler(this.Cancelar);
                    this.btnEditar.Click -= new System.EventHandler(this.EditarRegistro);
                }
                else
                {
                    MessageBox.Show(Bibliotecas.cSelecioneRegistro);
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            

            
        }

        private void BuscaRowIndex(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void frmHorariosEntrega_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12 && btnAdicionar.Text == "Adicionar [F12]")
            {
                btnAdicionar_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F12 && btnAdicionar.Text == "Salvar [F12]")
            {
                Salvar(sender, e);
            }
            else if (e.KeyCode == Keys.F11 && btnEditar.Text == "Editar [F11]")
            {
                Salvar(sender, e);
            }
        }
    }
}
