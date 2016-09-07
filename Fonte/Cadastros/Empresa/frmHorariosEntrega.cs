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
        private int rowIndex;
        public frmHorariosEntrega()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
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

        private void EditarRegistro(object sender, EventArgs e)
        {
            int intCodRegistro = 0;
            if (gridViewHorarios.SelectedRows.Count>0)
            {
                intCodRegistro = int.Parse(gridViewHorarios.Rows[rowIndex].Cells[0].Value.ToString());
                dtLimite.Value = Convert.ToDateTime(gridViewHorarios.Rows[rowIndex].Cells[1].Value.ToString());
                string text = gridViewHorarios.Rows[rowIndex].Cells[2].Value.ToString().Substring(0, 5);
                string text2 = gridViewHorarios.Rows[rowIndex].Cells[2].Value.ToString().Substring(9, 5);
                dtEntregaInicio.Value = DateTime.Parse(gridViewHorarios.Rows[rowIndex].Cells[2].Value.ToString().Substring(0,5));
                dtEntregaFim.Value = DateTime.Parse(gridViewHorarios.Rows[rowIndex].Cells[2].Value.ToString().Substring(9,5));

            }
        }

        private void BuscaRowIndex(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }
    }
}
