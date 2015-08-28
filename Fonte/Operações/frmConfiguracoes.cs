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
using System.Drawing.Printing;
using System.IO.Ports;
using DexComanda.Integração;
namespace DexComanda
{
    public partial class frmConfiguracoes : Form
    {
       private Configuracao config;
       private Utils util;
       private Conexao con;
       private int iNumModelo = 7;
       private string Porta = "";
       public string nomeImpressora;
        public frmConfiguracoes()
        {
            con = new Conexao();
            config = new Configuracao();
            InitializeComponent();
            this.pInfoUserDefault.Visible = false;
        }

        private void ConectarBanco(object sender, EventArgs e)
        {
            var servidor = this.txtServidor.Text;
            var banco = this.txtBanco.Text;

            Conexao con = new Conexao();
            con.OpenConection(servidor, banco);
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void SalvaConfig(object sender, EventArgs e)
        {
            //con = new Conexao(); 
            Empresa empresa = new Empresa()
            {
                Nome = txtNomeEmpresa.Text,
                CNPJ = txtCNPJ.Text,
                Telefone = txtTelefone.Text,
                Telefone2 = txtTelefone2.Text,
                Contato = txtContato.Text,
                Cep = int.Parse(txtCEP.Text),
                Endereco = txtLogradouro.Text,
                Cidade = txtCidade.Text,
                Bairro = txtBairro.Text,
                Numero = int.Parse(txtNumero.Text),
                UF = txtUF.Text,
                PontoReferencia = txtPontoReferencia.Text,
                Servidor = txtServidor.Text,
                Banco = txtBanco.Text,
                DataInicio = DateTime.Now,
                VersaoBanco= "1",
                CaminhoBackup = txtCaminhoBkp.Text
                
            };
            // Grava as configurações
                config.ImpViaCozinha = chkViaCozinha.Checked;
                config.UsaLoginSenha = chkLoginSenha.Checked ;
                config.UsaDataNascimento = chkDataNAscimento.Checked ;
                config.ControlaEntregador = chkEntregadores.Checked;
                config.ProdutoPorCodigo = chkProdutoCodigo.Checked;
                config.Usa2Telefones = chk2Telefones.Checked;
                config.UsaControleMesa = chkControlaMesas.Checked;
                config.ImprimeViaEntrega = chkImprimeViaEntrega.Checked;
                config.DescontoDiaSemana = chkDescontoDiasemana.Checked;
                config.ControlaFidelidade = chkFidelidade.Checked;
                config.EnviaSMS = chkEnviaSms.Checked;
                config.RegistraCancelamentos = chkRegCancelamentos.Checked;
               
                
                 config.RepeteUltimoPedido = chkUltPedido.Checked;
                if (chkEnviaSms.Checked)
                {
                    Utils.CriaArquivoTxt("ConfigSMS",txtLogin.Text+"-"+txtSenha.Text);
                }
                if (chkFidelidade.Checked)
                {
                   
                    config.PedidosParaFidelidade = int.Parse(txtNumeroPedidos.Text);
                }
                else
                {
                    config.PedidosParaFidelidade = 0;
                }
                
                config.PrevisaoEntregaSN = chkPrevisao.Checked;
                if (chkPrevisao.Checked)
                {
                    config.PrevisaoEntrega = txtPrevisao.Text;
                }
                else
                {
                    config.PrevisaoEntrega = "0";
                }
                if (txtTamanhoFont.Text!="")
                {
                    config.TamanhoFont = txtTamanhoFont.Text; 
                }
                else
                {
                    config.TamanhoFont = "8"; // Font Defaul
                }
                config.ImpLPT = chkImpLPT.Checked;
                if (chkImpLPT.Checked && txtPortaLPT.Text!="")
                {
                    config.PortaLPT = txtPortaLPT.Text;
                }
                else
                {
                    config.PortaLPT = "0";
                }
                //config.ImpressoraEntrega = cbxEntregas.Text;
                //config.ImpressoraCozinha = cbxCozinha.Text;
                //config.ImpressoraCopaBalcao = cbxMesas.Text;
            
            if (txtCaracterImpressora.Text !="")
            {
                config.QtdCaracteresImp = int.Parse(txtCaracterImpressora.Text.ToString());
            }
            
            if (chkLoginSenha.Checked)
            {
                string _senha = Utils.EncryptMd5(this.txtUsuarioPadrao.Text.ToString(), this.txtSenhaPadrao.Text.ToString());
                Usuario usuario = new Usuario()
                {
                    Nome = this.txtUsuarioPadrao.Text.ToString(),
                    Senha = _senha,
                    FinalizaPedidoSN=true,
                    AdministradorSN=true
                };
                con.Insert("spAdicionarUsuario", usuario);  
            }
            

            this.btnSalvar.Click -= AlterarConfig;
            this.btnSalvar.Click += SalvaConfig;
            if (empresa.Nome != "" && empresa.Contato != "" && empresa.Telefone != "")
            {
                if (txtViasBalcao.Text != "" && txtViasCozinha.Text != "" && txtViasEntrega.Text != "" && txtCaminhoBkp.Text!="")
                {
                    config.ViasEntrega = txtViasEntrega.Text;
                    config.ViasBalcao = txtViasBalcao.Text;
                    config.ViasCozinha = txtViasCozinha.Text;

                    con.Insert("spAdicionarEmpresa", empresa);
                   // MessageBox.Show("Empresa Adicionada com sucesso");
                    con.Insert("spAdicionarConfiguracao", config);
                    MessageBox.Show("Configuração adicionada com sucesso.");

                    Utils.Restart();

                }
                
            }
            else
            {
                MessageBox.Show("Campos Obrigatórios não preenchidos, favor verificar");
            }
        }

        private void AlterarConfig(object sender, EventArgs e)
        {
            Empresa empresa = new Empresa()
            {
                Nome = txtNomeEmpresa.Text,
                CNPJ = txtCNPJ.Text,
                Telefone = txtTelefone.Text,
                Telefone2 = txtTelefone2.Text,
                Contato = txtContato.Text,
                Cep = int.Parse(txtCEP.Text),
                Endereco = txtLogradouro.Text,
                Cidade = txtCidade.Text,
                Bairro = txtBairro.Text,
                Numero = int.Parse(txtNumero.Text),
                UF = txtUF.Text,
                PontoReferencia = txtPontoReferencia.Text,
                Servidor = txtServidor.Text,
                Banco = txtBanco.Text,
                DataInicio = DateTime.Now,
                VersaoBanco = "0",
                CaminhoBackup = txtCaminhoBkp.Text
            };

            config.cod = Sessions.returnConfig.cod;
           config.ImpViaCozinha = chkViaCozinha.Checked;
           config.UsaLoginSenha = chkLoginSenha.Checked;
           config.UsaDataNascimento = chkDataNAscimento.Checked ;
           config.ControlaEntregador = chkEntregadores.Checked;
           config.ProdutoPorCodigo = chkProdutoCodigo.Checked;
           config.Usa2Telefones = chk2Telefones.Checked;
           config.UsaControleMesa = chkControlaMesas.Checked;
           config.ImprimeViaEntrega = chkImprimeViaEntrega.Checked;
           config.ControlaFidelidade = chkFidelidade.Checked;
           config.DescontoDiaSemana = chkDescontoDiasemana.Checked;
           config.PedidosParaFidelidade = int.Parse(txtNumeroPedidos.Text);
           config.QtdCaracteresImp = int.Parse(txtCaracterImpressora.Text.ToString());
           config.PrevisaoEntregaSN = chkPrevisao.Checked;
           config.PrevisaoEntrega = txtPrevisao.Text;
           config.CobraTaxaGarcon = chk10Garcon.Checked;
           config.EnviaSMS = chkEnviaSms.Checked;
           config.ViasEntrega = txtViasEntrega.Text;
           config.TamanhoFont = txtTamanhoFont.Text;
           config.ViasBalcao = txtViasBalcao.Text;
           config.ViasCozinha = txtViasCozinha.Text;
           config.ImpLPT = chkImpLPT.Checked;
           config.RepeteUltimoPedido = chkUltPedido.Checked;
           config.RegistraCancelamentos = chkRegCancelamentos.Checked;

           if (chkEnviaSms.Checked)
           {
               Utils.CriaArquivoTxt("ConfigSMS", txtLogin.Text + "-" + txtSenha.Text);
           }


           if (chkImpLPT.Checked && txtPortaLPT.Text != "")
           {
               config.PortaLPT = txtPortaLPT.Text;
           }
           else
           {
               config.PortaLPT = "0";
           }

           //config.ImpressoraEntrega = cbxEntregas.Text;
           //config.ImpressoraCozinha = cbxCozinha.Text;
           //config.ImpressoraCopaBalcao = cbxMesas.Text;

           if (chkLoginSenha.Checked)
           {
               string _senha = Utils.EncryptMd5(this.txtUsuarioPadrao.Text.ToString(), this.txtSenhaPadrao.Text.ToString());
               UsuarioDefault usuario = new UsuarioDefault()
               {
                   Nome = this.txtUsuarioPadrao.Text.ToString(),
                   senha = _senha,
                   AdministradorSN=true
               };
               con.Insert("spAdicionarUsuarioDefault", usuario);
           }

           if (empresa.Nome != "" && empresa.Contato != "" && empresa.Telefone != ""&& txtCaminhoBkp.Text!="")
           {
               this.btnSalvar.Click -= AlterarConfig;
               this.btnSalvar.Click += SalvaConfig;
               con.Update("spAlterarConfiguracao", config);
               MessageBox.Show("Configuração alterada com sucesso.");
               
               con.Update("spAlterarEmpresa", empresa);
               MessageBox.Show("Empresa alterada com sucesso");
               Utils.Restart();
           }
           else
           {
               MessageBox.Show("Campos Obrigatórios não preenchidos");
           }
        }
        public string ListaImpressoras()
        {
            // Retorna as configurações da impressora
            PrinterSettings printer = new PrinterSettings();
            nomeImpressora = printer.PrinterName;

            return nomeImpressora;
        }
        private void LoadImpressoras()
        {
            // LOAD do Formulário para configuração de portas e Modelos de impressoras
            // carrega o combo com os modelos das impressoras
            cbModeloImp.Items.Add("MP 20 CI");
            cbModeloImp.Items.Add("MP 20 MI");
            cbModeloImp.Items.Add("MP 20 TH");
            cbModeloImp.Items.Add("MP 2000 CI");
            cbModeloImp.Items.Add("MP 2000 TH");
            cbModeloImp.Items.Add("MP 2100 TH");
            cbModeloImp.Items.Add("MP 2500 TH");
            cbModeloImp.Items.Add("MP 4000 TH");
            cbModeloImp.Items.Add("MP 4200 TH");

            //carrega o combo com as portas
            cbPorta.Items.Add("USB");
            cbPorta.Items.Add("COM1");
            cbPorta.Items.Add("COM2");
            cbPorta.Items.Add("COM3");
            cbPorta.Items.Add("COM4");
            cbPorta.Items.Add("LPT1");
            cbPorta.Items.Add("LPT2");
            cbPorta.Items.Add("ETHERNET");
        }

        private void frmConfiguracoes_Load(object sender, EventArgs e)
        {
            LoadImpressoras();
            
            // Utils.RetornoTxt();//cbxCozinha.Text= cbxMesas.Text= cbxEntregas.Text = ListaImpressoras();
            if (Sessions.returnConfig != null)
            {
                grpFidelidade.Visible = chkFidelidade.Checked;
                chkViaCozinha.Checked = Sessions.returnConfig.ImpViaCozinha;
                chkDataNAscimento.Checked = Sessions.returnConfig.UsaDataNascimento;
                chkLoginSenha.Checked = Sessions.returnConfig.UsaLoginSenha;
                chkEntregadores.Checked = Sessions.returnConfig.ControlaEntregador;
                chkProdutoCodigo.Checked = Sessions.returnConfig.ProdutoPorCodigo;
                chk2Telefones.Checked = Sessions.returnConfig.Usa2Telefones;
                chkControlaMesas.Checked = Sessions.returnConfig.UsaControleMesa;
                chkImprimeViaEntrega.Checked = Sessions.returnConfig.ImprimeViaEntrega;
                chkFidelidade.Checked = Sessions.returnConfig.ControlaFidelidade;
                chkDescontoDiasemana.Checked = Sessions.returnConfig.DescontoDiaSemana;
                txtNumeroPedidos.Text = Sessions.returnConfig.PedidosParaFidelidade.ToString();
                this.txtCaracterImpressora.Text = Sessions.returnConfig.QtdCaracteresImp.ToString();
                chkPrevisao.Checked = Sessions.returnConfig.PrevisaoEntregaSN;
                txtPrevisao.Text = Sessions.returnConfig.PrevisaoEntrega.ToString();
                chk10Garcon.Checked = Sessions.returnConfig.CobraTaxaGarcon;
                txtTamanhoFont.Text = Sessions.returnConfig.TamanhoFont.ToString();
                txtPortaLPT.Text = Sessions.returnConfig.PortaLPT.ToString();
                chkImpLPT.Checked = Sessions.returnConfig.ImpLPT;
                chkEnviaSms.Checked = Sessions.returnConfig.EnviaSMS;
                txtViasEntrega.Text = Sessions.returnConfig.ViasEntrega;
                txtViasCozinha.Text = Sessions.returnConfig.ViasCozinha;
                txtViasBalcao.Text = Sessions.returnConfig.ViasBalcao;
                chkUltPedido.Checked = Sessions.returnConfig.RepeteUltimoPedido;
                chkRegCancelamentos.Checked = Sessions.returnConfig.RegistraCancelamentos;

                this.btnSalvar.Text = "Alterar";
                this.btnSalvar.Click -= SalvaConfig;
                this.btnSalvar.Click += AlterarConfig;
            }
           //  Exibir DataLiberação Sistema
            var servidorLocal = con.SelectAll("Empresa", "spObterEmpresa");
            if (servidorLocal!=null)
            {
                DataRow Linha = servidorLocal.Tables["Empresa"].Rows[0];
                txtNomeEmpresa.Text = Linha.ItemArray.GetValue(1).ToString();
                txtCNPJ.Text = Linha.ItemArray.GetValue(2).ToString();
                txtTelefone.Text = Linha.ItemArray.GetValue(3).ToString();
                txtTelefone2.Text = Linha.ItemArray.GetValue(4).ToString();
                txtLogradouro.Text = Linha.ItemArray.GetValue(5).ToString();
                txtCEP.Text = Linha.ItemArray.GetValue(6).ToString();
                txtCidade.Text = Linha.ItemArray.GetValue(7).ToString();
                txtNumero.Text = Linha.ItemArray.GetValue(8).ToString();
                txtBairro.Text = Linha.ItemArray.GetValue(9).ToString();
                txtUF.Text = Linha.ItemArray.GetValue(10).ToString();
                txtPontoReferencia.Text = Linha.ItemArray.GetValue(11).ToString();
                txtBanco.Text = Linha.ItemArray.GetValue(12).ToString();
                txtContato.Text = Linha.ItemArray.GetValue(13).ToString();
                txtServidor.Text = Linha.ItemArray.GetValue(15).ToString();
                txtCaminhoBkp.Text = Linha.ItemArray.GetValue(16).ToString();

                if (con.IsConnected())
                {
                    var Dados = Utils.DadosLicenca(txtCNPJ.Text, Utils.EnderecoMAC(), Utils.RetornaNomePc());
                    DataRow LinhasLicenca = Dados.Tables["Licenca"].Rows[0];
                    lblDataExpiracao.Text = LinhasLicenca.ItemArray.GetValue(3).ToString().Substring(0, 10);
                    txtLicenca.Text = LinhasLicenca.ItemArray.GetValue(1).ToString() + LinhasLicenca.ItemArray.GetValue(9).ToString();
                }
                else
                {
                    txtLicenca.Text = txtCNPJ.Text + Utils.EnderecoMAC();
                    lblDataExpiracao.Text = DateTime.Now.AddMonths(1).ToString();
                }
                
            }
            
                

        }

        private void ExibirCamposParaLogin(object sender, EventArgs e)
        {
            if (this.chkLoginSenha.Checked == true)
            {
                this.pInfoUserDefault.Visible = true;
            }
            else
            {
                this.pInfoUserDefault.Visible = false;
            }
        }

        private void ConsultarCEP(object sender, KeyEventArgs e)
        {
           
        }

        private void BuscarCep(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCEP.Text.Length ==8)
                {
                    DataSet endereco = con.SelectEnderecoPorCep("base_cep", "spObterEnderecoPorCep", int.Parse(txtCEP.Text));

                    if (endereco.Tables["base_cep"].Rows.Count > 0)
                    {

                        DataRow dRow = endereco.Tables["base_cep"].Rows[0];

                        this.txtLogradouro.Text = dRow.ItemArray.GetValue(0).ToString();
                        this.txtBairro.Text = dRow.ItemArray.GetValue(1).ToString();
                        this.txtCidade.Text = dRow.ItemArray.GetValue(2).ToString();
                        this.txtUF.Text = dRow.ItemArray.GetValue(3).ToString();
                    }
                    else
                    {
                        MessageBox.Show("CEP não encontrado");
                    }
                }
                else
                {
                    MessageBox.Show("CEP menor que 8 caracteres , favor verificar");
                    txtCEP.Focus();
                }
                
            }
        }

        private void chkFidelidade_CheckedChanged(object sender, EventArgs e)
        {
            grpFidelidade.Enabled = chkFidelidade.Checked;
        }

        private void chkEnviaSms_CheckedChanged(object sender, EventArgs e)
        {
            grpSms.Enabled = chkEnviaSms.Checked;
        }

        private void chkDescontoDiasemana_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void ControlaPrevisao(object sender, EventArgs e)
        {
            lbltempo.Visible = txtPrevisao.Visible = chkPrevisao.Checked;
            if (chkPrevisao.Checked)
            {
                lbltempo.Focus();
            } 
        }

        private void ImpressoaMatricial(object sender, EventArgs e)
        {
            txtPortaLPT.Enabled = chkImpLPT.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPortaLPT.Text != "")
            {
                SerialPort porta = new SerialPort(txtPortaLPT.Text);
                porta.Open();
                try
                {

                }
                catch (Exception)
                {
                    
                    throw;
                }
                if (porta.IsOpen)
                {
                    MessageBox.Show("Comunicação Realizada com sucesso");
                }
                else
                {
                 
                }

            }
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void txtCNPJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void txtCEP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void txtCaracterImpressora_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void SelecionaLocal(object sender, EventArgs e)
        {
            FolderBrowserDialog OpenFolder = new FolderBrowserDialog();
            DialogResult result = OpenFolder.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCaminhoBkp.Text = OpenFolder.SelectedPath.ToString();
            }
        }

        private void cbModeloImp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string modeloImp = cbModeloImp.SelectedItem.ToString(); //Pega a seleção do Combo
            int iRetorno = 7;
            //testes para definir o código do modelo da impressora
            if (modeloImp == "MP 20 CI")
            {
                iRetorno = MP2032.ConfiguraModeloImpressora(1);
                iNumModelo = 1;
            }
            else if (modeloImp == "MP 20 MI")
            {
                iRetorno = MP2032.ConfiguraModeloImpressora(1);
            }
            else if (modeloImp == "MP 20 TH")
            {
                iRetorno = MP2032.ConfiguraModeloImpressora(0);
                iNumModelo = 1;
            }
            else if (modeloImp == "MP 2000 CI")
            {
                iRetorno = MP2032.ConfiguraModeloImpressora(0);
                iNumModelo = 0;
            }
            else if (modeloImp == "MP 2000 TH")
            {
                iNumModelo = MP2032.ConfiguraModeloImpressora(0);
                iNumModelo = 0;
            }
            else if (modeloImp == "MP 2100 TH")
            {
                iRetorno = MP2032.ConfiguraModeloImpressora(0);
                iNumModelo = 0;
            }
            else if (modeloImp == "MP 2500 TH")
            {
                iRetorno = MP2032.ConfiguraModeloImpressora(8);
                iNumModelo = 0;
            }
            else if (modeloImp == "MP 4000 TH")
            {
                iRetorno = MP2032.ConfiguraModeloImpressora(5);
                iNumModelo = 5;
            }
            else if (modeloImp == "MP 4200 TH")
            {
                iRetorno = MP2032.ConfiguraModeloImpressora(7);
                iNumModelo = 7;
            }

          
        }

        private void cbPorta_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtIpImpressora.Visible = cbPorta.SelectedItem.ToString() == "ETHERNET";
            txtIpImpressora.Focus();
        
        }

       
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            //MP2032.ConfiguraModeloImpressora(iNumModelo);
            if (txtIpImpressora.Visible)
	        {
		     Porta = txtIpImpressora.Text;
	        }
            else
	        {
                Porta = cbPorta.SelectedItem.ToString();
	        }

            if (MP2032.IniciaPorta(Porta) <= 0)
            {
                MessageBox.Show("Impressora não Configurada");
                string iArquivo = Utils.CriaArquivoTxt("ConfigImpressao", Convert.ToString(iNumModelo) + ";" + Porta);
            }
            else
            {
                string iArquivo = Utils.CriaArquivoTxt("ConfigImpressao", Convert.ToString(iNumModelo)+";"+ Porta);
            }
            
        }

        

        

    }
}
