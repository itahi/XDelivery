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
        private int intCodPai;
        private string iCod1 = "0";
        private string iCod2 = "0";
        private string iCod3 = "0";
        private string iCod4 = "0";
        public string strNomeProduto, strTamanho, strPreco,strNome = "";
        public Boolean boolConfirmado = false;
        private string iCodOpcao;
        public frmSabores()
        {

            InitializeComponent();
        }
        public frmSabores(string iGrupo,int iCodPai)
        {
            strGrupo = iGrupo;
            intCodPai = iCodPai;
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
            if (intCodPai!=0)
            {
                MOntaCombo(comboBox1, intCodPai);
                return;
            }
            Utils.MontaCombox(comboBox1, "NomeProduto", "Codigo", strGrupo);
        }
        private void ListaSabor2(object sender, EventArgs e)
        {
            if (intCodPai != 0)
            {
                MOntaCombo(comboBox2, intCodPai);
                return;
            }
            Utils.MontaCombox(comboBox2, "NomeProduto", "Codigo", strGrupo);
        }

        private void ListaSabor3(object sender, EventArgs e)
        {
            if (intCodPai != 0)
            {
                MOntaCombo(comboBox3, intCodPai);
                return;
            }
            Utils.MontaCombox(comboBox3, "NomeProduto", "Codigo", strGrupo);
        }

        private void ListaSabor4(object sender, EventArgs e)
        {
            if (intCodPai != 0)
            {
                MOntaCombo(comboBox4, intCodPai);
                return;
            }
            Utils.MontaCombox(comboBox4, "NomeProduto", "Codigo", strGrupo);
        }
        private void MOntaCombo(ComboBox icbxName,int iCodPai)
        {
            DataSet ds = con.SelectRegistroPorCodigo("Produto", "spObterProdutoPorCodPai", iCodPai);
            icbxName.DataSource = ds.Tables[0];
            icbxName.DisplayMember = "NomeProduto";
            icbxName.ValueMember = "Codigo";
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
        //private string RetornaProporcaoSabor(string iNomeSabor)
        //{

        //}
        private void MontaTamanhos(object sender, EventArgs e)
        {
            try
            {
                EscondeTamanhos();
                if (comboBox1.SelectedIndex != -1)
                {
                    if (cbxPorcentagem1.SelectedIndex==-1)
                    {
                        MessageBox.Show("Selecione a proporção");
                        cbxPorcentagem1.Focus();
                        return;
                    }
                    iCod1 = comboBox1.SelectedValue.ToString();
                    strNomeProduto = strNomeProduto + comboBox1.Text +" " +cbxPorcentagem1.Text;
                    strNomeProduto = strNomeProduto.Insert(strNomeProduto.Length, Environment.NewLine);
                }
                if (comboBox2.SelectedIndex != -1)
                {
                    if (cbxPorcentagem2.SelectedIndex == -1)
                    {
                        MessageBox.Show("Selecione a proporção");
                        cbxPorcentagem2.Focus();
                        return;
                    }
                    iCod2 = comboBox2.SelectedValue.ToString();
                    strNomeProduto = strNomeProduto + comboBox2.Text + " " + cbxPorcentagem2.Text;
                    strNomeProduto = strNomeProduto.Insert(strNomeProduto.Length, Environment.NewLine);
                }
                if (comboBox3.SelectedIndex != -1)
                {
                    if (cbxPorcentagem3.SelectedIndex == -1)
                    {
                        MessageBox.Show("Selecione a proporção");
                        cbxPorcentagem3.Focus();
                        return;
                    }
                    iCod3 = comboBox3.SelectedValue.ToString();
                    strNomeProduto = strNomeProduto + comboBox3.Text + " " + cbxPorcentagem3.Text;
                    strNomeProduto = strNomeProduto.Insert(strNomeProduto.Length, Environment.NewLine);
                }
                if (comboBox4.SelectedIndex != -1)
                {
                    if (cbxPorcentagem4.SelectedIndex == -1)
                    {
                        MessageBox.Show("Selecione a proporção");
                        cbxPorcentagem4.Focus();
                        return;
                    }
                    iCod4 = comboBox4.SelectedValue.ToString();
                    strNomeProduto = strNomeProduto + comboBox4.Text + " " + cbxPorcentagem4.Text;
                    strNomeProduto = strNomeProduto.Insert(strNomeProduto.Length, Environment.NewLine);
                }

                DataSet dsOpcoesProduto = con.RetornaOpcoesProduto2(int.Parse(iCod1));
                for (int i = 0; i < dsOpcoesProduto.Tables[0].Rows.Count; i++)
                {
                    string iCodOpcao = dsOpcoesProduto.Tables[0].Rows[i].ItemArray.GetValue(5).ToString();
                    DataSet dsMaiorPreco = con.RetornaMaioresPrecos(iCod1, iCod2, iCod3, iCod4, iCodOpcao,Sessions.returnConfig.CobrancaProporcionalSN);
                    string strPreco =dsMaiorPreco.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    strNome = dsOpcoesProduto.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    if (dsMaiorPreco.Tables[0].Rows.Count > 0)
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
                        else if (!radioButton6.Visible)
                        {
                            radioButton6.Visible = true;
                            radioButton6.Tag = strPreco;
                            radioButton6.Text = strNome + " " + strPreco;
                        }
                    }
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }

        private void ConfirmaSelecao(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Control ctrControl in grpTamanhos.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    if (((System.Windows.Forms.RadioButton)ctrControl).Checked)
                    {
                        // strNomeProduto = strNomeProduto + ((System.Windows.Forms.RadioButton)ctrControl).Text;
                        strPreco = ((System.Windows.Forms.RadioButton)ctrControl).Tag.ToString();
                        strTamanho = ((System.Windows.Forms.RadioButton)ctrControl).Text.ToString().Replace(strPreco,""); 
                    }
                }
            }
            if (strPreco=="0" || strPreco=="")
            {
                MessageBox.Show("Selecione o Tamanho para continuar");
                return;
            }
            boolConfirmado = true;
            this.Close();
        }
    }
}
