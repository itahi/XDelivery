using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Cadastros.Pedido
{
    public partial class frmSabores : Form
    {
        private Conexao con;
        private string strGrupo;
        private string iCod1 = "0";
        private string iCod2 = "0";
        private string iCod3 = "0";
        private string iCod4 = "0";
        private string iCodOpcao;
        public frmSabores()
        {

            InitializeComponent();
        }
        public frmSabores(string iGrupo)
        {
            strGrupo = iGrupo;
            InitializeComponent();
        }

        private void frmSabores_Load(object sender, EventArgs e)
        {
            try
            {
                con = new Conexao();
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void ListaSabor1(object sender, EventArgs e)
        {
            Utils.MontaCombox(comboBox1, "NomeProduto", "Codigo", strGrupo);
            
        }

        private void ListaSabor2(object sender, EventArgs e)
        {
            Utils.MontaCombox(comboBox2, "NomeProduto", "Codigo", strGrupo);
        }

        private void ListaSabor3(object sender, EventArgs e)
        {
            Utils.MontaCombox(comboBox3, "NomeProduto", "Codigo", strGrupo);
        }

        private void ListaSabor4(object sender, EventArgs e)
        {
            Utils.MontaCombox(comboBox4, "NomeProduto", "Codigo", strGrupo);
        }

        private void EscondeTamanhos()
        {
            foreach (System.Windows.Forms.Control ctrControl in grpTamanhos.Controls)
            {
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    //Unselect all RadioButtons
                    ((System.Windows.Forms.RadioButton)ctrControl).Visible = false;
                }
            }
        }
        private void MontaTamanhos(object sender, EventArgs e)
        {
            try
            {
                EscondeTamanhos();
                //foreach (System.Windows.Forms.Control ctrControl in grpSabores.Controls)
                //{
                //    //Loop through all controls 
                //    if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.ComboBox)))
                //    {
                        if (comboBox1.SelectedIndex!=-1)
                        {
                            iCod1 = comboBox1.SelectedValue.ToString();
                        }
                        if (comboBox2.SelectedIndex != -1)
                        {
                            iCod2 = comboBox2.SelectedValue.ToString();
                        }
                        if (comboBox3.SelectedIndex != -1)
                        {
                            iCod3 = comboBox3.SelectedValue.ToString();
                        }
                        if (comboBox4.SelectedIndex != -1)
                        {
                            iCod4 = comboBox4.SelectedValue.ToString();
                        }
                //    }
                //}

                DataSet dsOpcoesProduto = con.RetornaOpcoesProduto(int.Parse(iCod1));
                for (int i = 0; i < dsOpcoesProduto.Tables[0].Rows.Count; i++)
                {
                    string iCodOpcao = dsOpcoesProduto.Tables[0].Rows[i].ItemArray.GetValue(5).ToString();
                    DataSet dsMaiorPreco = con.RetornaMaioresPrecos(iCod1, iCod2, iCod3, iCod4, iCodOpcao);
                    string strPreco = dsMaiorPreco.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    string strNome = dsOpcoesProduto.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    if (dsMaiorPreco.Tables[0].Rows.Count>0)
                    {
                        if (!radioButton1.Visible)
                        {
                            radioButton1.Visible = true;
                            radioButton1.Tag = strPreco;
                            radioButton1.Text = strNome + " " + strPreco;
                        }
                        else if (!radioButton2.Visible)
                        {
                            radioButton2.Visible = true;
                            radioButton2.Tag = strPreco;
                            radioButton2.Text = strNome + " " + strPreco;
                        }
                        else if (!radioButton3.Visible)
                        {
                            radioButton3.Visible = true;
                            radioButton3.Tag = strPreco;
                            radioButton3.Text = strNome + " " + strPreco;
                        }
                       else if (!radioButton4.Visible)
                        {
                            radioButton4.Visible = true;
                            radioButton4.Tag = strPreco;
                            radioButton4.Text = strNome + " " + strPreco;
                        }
                        else if (!radioButton5.Visible)
                        {
                            radioButton5.Visible = true;
                            radioButton5.Tag = strPreco;
                            radioButton5.Text = strNome + " " + strPreco;
                        }
                    }
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
       
    }
}
