using DexComanda.Models.Produto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações.Alteracoes
{
    public partial class frmAlterarMultOpcao : Form
    {
        private Conexao con;
        public frmAlterarMultOpcao()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void frmAlterarMultOpcao_Load(object sender, EventArgs e)
        {
            try
            {
                Utils.MontaCombox(cbxTipo, "Nome", "Tipo", "Produto_OpcaoTipo", "spObterTipoOpcao");
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            MarcaTodosDias(checkBox1.Checked);
        }
        private void MarcaTodosDias(bool iMarca = true)
        {
            foreach (System.Windows.Forms.Control ctrControl in grpDiasDisponivel.Controls)
            {
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.CheckBox)))
                {
                    //select all RadioButtons
                    ((System.Windows.Forms.CheckBox)ctrControl).Checked = iMarca;
                }
            }
        }
        private string RetornaDiasMarcados()
        {
            List<OpcaoDia> listDias = new List<OpcaoDia>();
            // var precosDia = new PrecoDiaProduto();
            foreach (System.Windows.Forms.Control TEXT in grpDiasDisponivel.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(TEXT.GetType(), typeof(System.Windows.Forms.CheckBox)))
                {

                    var diasDisponiel = new Models.Produto.OpcaoDia()
                    {
                        Dia = (((System.Windows.Forms.CheckBox)TEXT).Tag.ToString().Substring(0, 3)),
                        AtivoSN = Convert.ToInt16((((System.Windows.Forms.CheckBox)TEXT).Checked)),
                    };
                    listDias.Add(diasDisponiel);
                }
            }

            return Utils.SerializaObjeto(listDias);
        }

        private void AdicionaItemGrid(object sender, EventArgs e)
        {
            try
            {
                if (cbxOpcao.SelectedIndex < 0)
                {
                    MessageBox.Show("Selecione uma opção para adicionar");
                    return;
                }

                int iCountLinhas = GridView.Rows.Count;
                if (GridView.DataSource != null)
                {
                    GridView.AutoGenerateColumns = false;
                    GridView.DataSource = null;
                    GridView.DataMember = null;
                }

                GridView.Rows.Add();
                GridView.Rows[iCountLinhas].Cells["Codigo"].Value = int.Parse(cbxOpcao.SelectedValue.ToString());
                GridView.Rows[iCountLinhas].Cells["Nome"].Value = cbxOpcao.Text;
                iCountLinhas = GridView.Rows.Count;

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void cbxTipo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbxTipo.SelectedIndex < 0)
            {
                return;
            }

            Utils.MontaCombox(cbxOpcao, "Nome", "Codigo", "Opcao", "spObterOpcaoPorTipo", int.Parse(cbxTipo.SelectedValue.ToString()));
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            if (GridView.Rows.Count==0)
            {
                MessageBox.Show(Bibliotecas.cFiltroRetornaVazio);
                return;
            }
            for (int i = 0; i < GridView.Rows.Count; i++)
            {
                OpcaoMult opcao = new OpcaoMult()
                {
                    Codigo = int.Parse(GridView.Rows[i].Cells["Codigo"].Value.ToString()),
                    AtivoSN = chkAtivoSN.Checked,
                    OnlineSN = chkOnlineSN.Checked,
                    DiasDisponivel = RetornaDiasMarcados()
                };
                con.Update("spAlterarMultiplaOpcao", opcao);
            }
            GridView.DataSource = null;
            GridView.DataMember = null;
        }
    }
}
