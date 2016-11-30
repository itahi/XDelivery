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
using System.Configuration;
using Newtonsoft.Json;
using DexComanda.Models.WS;
using DexComanda.Models.Operacoes;

namespace DexComanda
{
    public partial class frmConfiguracoes : Form
    {
        private Configuracao config;
        private Utils util;
        private Conexao con;
        private int iNumModelo = 7;
        private List<CidadesAtendidas> listCidades;
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
        private List<CidadesAtendidas> CidadesAtendidas()
        {
            List<CidadesAtendidas> Listcdd = new List<CidadesAtendidas>();
            Listcdd = new List<Models.WS.CidadesAtendidas>();
            foreach (Control item in grpCidades.Controls)
            {
                if (object.ReferenceEquals(item.GetType(), typeof(System.Windows.Forms.TextBox)))
                {
                    if (((System.Windows.Forms.TextBox)item).Text != "")
                    {
                        var cdd = new CidadesAtendidas()
                        {
                            Cidade = ((System.Windows.Forms.TextBox)item).Text
                        };
                        Listcdd.Add(cdd);
                    };
                }

            }
            return Listcdd;
        }
        private List<HorarioFuncionamento> HorariosFuncionamento(Control controGeral)
        {
            List<HorarioFuncionamento> listHorario = new List<HorarioFuncionamento>();
            // var precosDia = new PrecoDiaProduto();
            foreach (System.Windows.Forms.Control obj in controGeral.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(controGeral.GetType(), typeof(DateTimePicker)))
                {
                    if (((System.Windows.Forms.DateTimePicker)obj).Value.ToString() !="")
                    {
                        var listaHoras = new HorarioFuncionamento()
                        {
                            Dia = obj.Tag.ToString(),
                            Inicio = "",
                            Fim = ""
                        };
                        listHorario.Add(listaHoras);
                    }
                    //foreach (DateTimePicker item in controGeral.Controls)
                    //{
                    //    var listaHoras = new HorarioFuncionamento()
                    //    {
                    //        Dia = controGeral.Tag.ToString(),
                    //        Inicio = item.Value.ToString(),
                    //        Fim = item.Value.ToString()
                    //    };
                    //    listHorario.Add(listaHoras);
                    //}

                }
            }
              

            return listHorario;

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
                VersaoBanco = "1",
                CaminhoBackup = txtCaminhoBkp.Text,
                UrlServidor = txtURL.Text,
                HorarioFuncionamento = HorariosFuncionamento(tabPage3).ToString()


            };
            // Grava as configurações
            config.ImpViaCozinha = chkViaCozinha.Checked;
            config.UsaLoginSenha = chkLoginSenha.Checked;
            config.UsaDataNascimento = chkDataNAscimento.Checked;
            config.ControlaEntregador = chkEntregadores.Checked;
            config.ProdutoPorCodigo = chkProdutoCodigo.Checked;
            config.Usa2Telefones = chk2Telefones.Checked;
            config.UsaControleMesa = chkControlaMesas.Checked;
            config.ImprimeViaEntrega = chkImprimeViaEntrega.Checked;
            config.DescontoDiaSemana = chkDescontoDiasemana.Checked;
            config.ControlaFidelidade = chkFidelidade.Checked;
            config.EnviaSMS = chkEnviaSms.Checked;
            config.RegistraCancelamentos = chkRegCancelamentos.Checked;
            config.DadosApp = Utils.GravaJson(cbxPlataforma1.Text, txtLink2.Text);// + cbxPlataforma2.Text + txtLink2.Text);
            config.Pushapp_id = txtAPPID.Text;
            config.Pushauthorization = txtCodAutorização.Text;
            config.RepeteUltimoPedido = chkUltPedido.Checked;
            config.CidadesAtendidas = Utils.SerializaObjeto(CidadesAtendidas());
            config.ExigeVendedorSN = chkVendedor.Checked;
            config.GCM = txtGoogleProjetc.Text;
            config.ImpressoraEntrega = cbxImpressoraDelivery.Text;
            config.ImpressoraCozinha = cbxImpressoraMesa.Text;
            config.ImpressoraCopaBalcao = cbxImpressoraBalcao.Text;
            if (chkEnviaSms.Checked)
            {
                Utils.CriaArquivoTxt("ConfigSMS", txtLogin.Text + "-" + txtSenha.Text);
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
            if (txtTamanhoFont.Text != "")
            {
                config.TamanhoFont = txtTamanhoFont.Text;
            }
            else
            {
                config.TamanhoFont = "8"; // Font Defaul
            }
            config.ImpLPT = chkImpLPT.Checked;
            if (chkImpLPT.Checked && txtPortaLPT.Text != "")
            {
                config.PortaLPT = txtPortaLPT.Text;
            }
            else
            {
                config.PortaLPT = "0";
            }

            if (txtCaracterImpressora.Text != "")
            {
                config.QtdCaracteresImp = int.Parse(txtCaracterImpressora.Text.ToString());
            }

            if (chkLoginSenha.Checked && txtUsuarioPadrao.Text.Trim() != "" && txtSenhaPadrao.Text.Trim() != "")
            {
                string _senha = Utils.EncryptMd5(this.txtUsuarioPadrao.Text.ToString(), this.txtSenhaPadrao.Text.ToString());
                Usuario usuario = new Usuario()
                {
                    Nome = this.txtUsuarioPadrao.Text.ToString(),
                    Senha = _senha,
                    FinalizaPedidoSN = true,
                    AdministradorSN = true
                };
                con.Insert("spAdicionarUsuario", usuario);
            }


            this.btnSalvar.Click -= AlterarConfig;
            this.btnSalvar.Click += SalvaConfig;
            if (empresa.Nome != "" && empresa.Contato != "" && empresa.Telefone != "" && txtViasBalcao.Text != "" && txtViasCozinha.Text != "" && txtViasEntrega.Text != "" && txtCaminhoBkp.Text != "")
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
                CaminhoBackup = txtCaminhoBkp.Text,
                UrlServidor = txtURL.Text,
                HorarioFuncionamento = HorariosFuncionamento(tabControl1.TabPages[1]).ToString()
            };

            config.cod = Sessions.returnConfig.cod;
            config.ImpViaCozinha = chkViaCozinha.Checked;
            config.UsaLoginSenha = chkLoginSenha.Checked;
            config.UsaDataNascimento = chkDataNAscimento.Checked;
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
            config.DadosApp = Utils.GravaJson(cbxPlataforma1.Text, txtLink1.Text);
            config.Pushauthorization = txtCodAutorização.Text;
            config.Pushapp_id = txtAPPID.Text;
            config.RepeteUltimoPedido = chkUltPedido.Checked;
            config.CidadesAtendidas = Utils.SerializaObjeto(CidadesAtendidas());
            config.ExigeVendedorSN = chkVendedor.Checked;
            config.GCM = txtGoogleProjetc.Text;
            config.ImpressoraEntrega = cbxImpressoraDelivery.Text;
            config.ImpressoraCozinha = cbxImpressoraMesa.Text;
            config.ImpressoraCopaBalcao = cbxImpressoraBalcao.Text;
            //config.CidadesAtendidas = "";
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

            if (chkLoginSenha.Checked && txtUsuarioPadrao.Text.Trim() != "" && txtSenhaPadrao.Text.Trim() != "")
            {
                string _senha = Utils.EncryptMd5(this.txtUsuarioPadrao.Text.ToString(), this.txtSenhaPadrao.Text.ToString());
                UsuarioDefault usuario = new UsuarioDefault()
                {
                    Nome = this.txtUsuarioPadrao.Text.ToString(),
                    senha = _senha,
                    AdministradorSN = true
                };
                con.Insert("spAdicionarUsuarioDefault", usuario);
            }

            if (empresa.Nome != "" && empresa.Contato != "" && empresa.Telefone != "" && txtCaminhoBkp.Text != "")
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


        private  void LoadImpressoras(ComboBox icbx)
        {
            icbx.Items.Clear();
            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                icbx.Items.Add(item);
            }

        }

        private void frmConfiguracoes_Load(object sender, EventArgs e)
        {
            // Utils.RetornoTxt();//cbxCozinha.Text= cbxMesas.Text= cbxEntregas.Text = ListaImpressoras();
            if (Sessions.returnConfig != null)
            {
                grpFidelidade.Enabled = chkFidelidade.Checked;
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
                txtAPPID.Text = Sessions.returnConfig.Pushapp_id;
                txtCodAutorização.Text = Sessions.returnConfig.Pushauthorization;
                chkVendedor.Checked = Sessions.returnConfig.ExigeVendedorSN;
                PreencheCidades(Sessions.returnConfig.CidadesAtendidas);
                txtGoogleProjetc.Text = Sessions.returnConfig.GCM;

                cbxImpressoraDelivery.Text = Sessions.returnConfig.ImpressoraEntrega;
                cbxImpressoraMesa.Text = Sessions.returnConfig.ImpressoraCozinha;
                cbxImpressoraBalcao.Text = Sessions.returnConfig.ImpressoraCopaBalcao;
                this.btnSalvar.Text = "Alterar";
                this.btnSalvar.Click -= SalvaConfig;
                this.btnSalvar.Click += AlterarConfig;
            }
            //  Exibir DataLiberação Sistema
            var servidorLocal = con.SelectAll("Empresa", "spObterEmpresa");
            if (servidorLocal != null)
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
                txtURL.Text = Sessions.returnEmpresa.UrlServidor;


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
        private void PreencheCidades(string iListaJson)
        {
            try
            {
                List<CidadesAtendidas> cidades = new List<CidadesAtendidas>();
                if (iListaJson == "" || iListaJson==null)
                {
                    return;
                }
                cidades = Utils.DeserializaObjeto2(iListaJson);
                if (cidades.Count > 0)
                {
                    foreach (var item in cidades)
                    {
                        foreach (System.Windows.Forms.Control obj in grpCidades.Controls)
                        {
                            if (object.ReferenceEquals(obj.GetType(), typeof(System.Windows.Forms.TextBox)))
                            {
                                if (((System.Windows.Forms.TextBox)obj).Text == "")
                                {
                                    ((System.Windows.Forms.TextBox)obj).Text = item.Cidade;
                                    break;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception erros)
            {
                MessageBox.Show(Bibliotecas.cException + erros.Message);
            }
            
        }

        private void BuscarCep(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCEP.Text.Length == 8)
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

        //private void cbModeloImp_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string modeloImp = cbModeloImp.SelectedItem.ToString(); //Pega a seleção do Combo
        //    int iRetorno = 7;
        //    //testes para definir o código do modelo da impressora
        //    if (modeloImp == "MP 20 CI")
        //    {
        //        iRetorno = MP2032.ConfiguraModeloImpressora(1);
        //        iNumModelo = 1;
        //    }
        //    else if (modeloImp == "MP 20 MI")
        //    {
        //        iRetorno = MP2032.ConfiguraModeloImpressora(1);
        //    }
        //    else if (modeloImp == "MP 20 TH")
        //    {
        //        iRetorno = MP2032.ConfiguraModeloImpressora(0);
        //        iNumModelo = 1;
        //    }
        //    else if (modeloImp == "MP 2000 CI")
        //    {
        //        iRetorno = MP2032.ConfiguraModeloImpressora(0);
        //        iNumModelo = 0;
        //    }
        //    else if (modeloImp == "MP 2000 TH")
        //    {
        //        iNumModelo = MP2032.ConfiguraModeloImpressora(0);
        //        iNumModelo = 0;
        //    }
        //    else if (modeloImp == "MP 2100 TH")
        //    {
        //        iRetorno = MP2032.ConfiguraModeloImpressora(0);
        //        iNumModelo = 0;
        //    }
        //    else if (modeloImp == "MP 2500 TH")
        //    {
        //        iRetorno = MP2032.ConfiguraModeloImpressora(8);
        //        iNumModelo = 0;
        //    }
        //    else if (modeloImp == "MP 4000 TH")
        //    {
        //        iRetorno = MP2032.ConfiguraModeloImpressora(5);
        //        iNumModelo = 5;
        //    }
        //    else if (modeloImp == "MP 4200 TH")
        //    {
        //        iRetorno = MP2032.ConfiguraModeloImpressora(7);
        //        iNumModelo = 7;
        //    }


        //}

        //private void cbPorta_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    txtIpImpressora.Visible = cbPorta.SelectedItem.ToString() == "ETHERNET";
        //    txtIpImpressora.Focus();

        //}


        //private void button1_Click_1(object sender, EventArgs e)
        //{

        //    //MP2032.ConfiguraModeloImpressora(iNumModelo);
        //    if (txtIpImpressora.Visible)
        //    {
        //        Porta = txtIpImpressora.Text;
        //    }
        //    else
        //    {
        //        Porta = cbPorta.SelectedItem.ToString();
        //    }

        //    if (MP2032.IniciaPorta(Porta) <= 0)
        //    {
        //        MessageBox.Show("Impressora não Configurada");
        //        string iArquivo = Utils.CriaArquivoTxt("ConfigImpressao", Convert.ToString(iNumModelo) + ";" + Porta);
        //    }
        //    else
        //    {
        //        string iArquivo = Utils.CriaArquivoTxt("ConfigImpressao", Convert.ToString(iNumModelo) + ";" + Porta);
        //    }

        //}

        private void chkViaCozinha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkViaCozinha.Checked)
            {
                txtViasCozinha.Text = "1";
            }
            else
            {
                txtViasCozinha.Text = "0";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SalvarConfigProduto(object sender, EventArgs e)
        {
            string strConfiProduto = "Codigo";

            if (chkNomeProd.Checked)
            {
                strConfiProduto = strConfiProduto + ",NomeProduto";
            }
            if (chkDescricao.Checked)
            {
                strConfiProduto = strConfiProduto + ",DescricaoProduto";
            }
            if (chkPreco.Checked)
            {
                strConfiProduto = strConfiProduto + ",PrecoProduto";
            }
            if (chkPrDesconto.Checked)
            {
                strConfiProduto = strConfiProduto + ",PrecoDesconto";
            }
            if (chkGrupo.Checked)
            {
                strConfiProduto = strConfiProduto + ",GrupoProduto";
            }
            if (chkAtivo.Checked)
            {
                strConfiProduto = strConfiProduto + ",AtivoSN";
            }
            if (chkDtAlteracao.Checked)
            {
                strConfiProduto = strConfiProduto + ",DataAlteracao";
            }
            if (chkDtSincronismo.Checked)
            {
                strConfiProduto = strConfiProduto + ",DataSincronismo";
            }

            Utils.SalvarConfiguracao("GridProduto", strConfiProduto);

        }

        private void SalvarConfigPedido(object sender, EventArgs e)
        {
            string strConfigPedido = "Pd.Codigo,";
            if (chkNomeCliente.Checked)
            {
                strConfigPedido = strConfigPedido + "(select Nome from Pessoa P where P.Codigo = Pd.CodPessoa) as 'Nome Cliente'";
            }
            if (chkFinalizado.Checked)
            {
                strConfigPedido = strConfigPedido + ",Finalizado";
            }
            if (chkTotal.Checked)
            {
                strConfigPedido = strConfigPedido + ",TotalPedido";
            }

            if (chkTrocoPara.Checked)
            {
                strConfigPedido = strConfigPedido + ",TrocoPara";
            }
            if (chkFormaPagamento.Checked)
            {
                strConfigPedido = strConfigPedido + ",FormaPagamento";
            }

            if (chkDataPedido.Checked)
            {
                strConfigPedido = strConfigPedido + ",RealizadoEM";
            }
            if (chkTipo.Checked)
            {
                strConfigPedido = strConfigPedido + ",Pd.Tipo";
            }
            if (chkNumeroMesa.Checked)
            {
                strConfigPedido = strConfigPedido + ",NumeroMesa";
            }
            if (chkstatus.Checked)
            {
                strConfigPedido = strConfigPedido + ",(select top 1 PS.Nome from PedidoStatusMovimento PSM join "
                   + " PedidoStatus PS on Status = PSM.CodStatus"
                   + " where PSM.CodPedido=PD.Codigo"
                   + " order by PSM.DataAlteracao desc)"
                   + " as 'Situacao Pedido' ";
            }
            if (chkOrigem.Checked)
            {
                strConfigPedido = strConfigPedido + ",PedidoOrigem";
            }
            if (chkDesconto.Checked)
            {
                strConfigPedido = strConfigPedido + ",DescontoValor";
            }
            if (chkEntregador.Checked)
            {
                strConfigPedido = strConfigPedido + ",(select Nome from Entregador where Codigo=PD.CodMotoboy) as 'Entregador'";
            }
            if (chkAtendente.Checked)
            {
                strConfigPedido = strConfigPedido + ",(Select Nome from Usuario where Cod = PD.CodUsuario) as 'Atendente'";
            }

            Utils.SalvarConfiguracao("GridPedido", strConfigPedido);
        }

        private void btnSalvarConfigPessoas_Click(object sender, EventArgs e)
        {
            string strConfiPessoa = "Codigo";

            if (chkNomePessoa.Checked)
            {
                strConfiPessoa = strConfiPessoa + ",Nome";
            }
            if (chkEndereco.Checked)
            {
                strConfiPessoa = strConfiPessoa + ",Endereco";
            }
            if (chkBairro.Checked)
            {
                strConfiPessoa = strConfiPessoa + ",Bairro";
            }
            if (chkCidade.Checked)
            {
                strConfiPessoa = strConfiPessoa + ",Cidade";
            }
            if (chkUF.Checked)
            {
                strConfiPessoa = strConfiPessoa + ",UF";
            }
            if (chkPreferencia.Checked)
            {
                strConfiPessoa = strConfiPessoa + ",PontoReferencia";
            }
            if (chkTicket.Checked)
            {
                strConfiPessoa = strConfiPessoa + ",TicketFidelidade";
            }
            if (chkNUmero.Checked)
            {
                strConfiPessoa = strConfiPessoa + ",Numero";
            }
            if (chkCep.Checked)
            {
                strConfiPessoa = strConfiPessoa + ",Cep";
            }
            if (chkTelefone.Checked)
            {
                strConfiPessoa = strConfiPessoa + ",Telefone";
            }
            if (chkTelefone2.Checked)
            {
                strConfiPessoa = strConfiPessoa + ",Telefone2";
            }

            Utils.SalvarConfiguracao("GridPessoa", strConfiPessoa);
        }

        private void btnSalvarSMS_Click(object sender, EventArgs e)
        {
            Utils.SalvarConfiguracao("ConfigSMS", txtLogin.Text + "," + txtSenha.Text);
            MessageBox.Show("Senha gravada", "[xSistemas]");
        }

        private void txtPrevisao_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }

        private void cbxImpressoraDelivery_DropDown(object sender, EventArgs e)
        {
            LoadImpressoras(cbxImpressoraDelivery);
        }

        private void cbxImpressoraBalcao_DropDown(object sender, EventArgs e)
        {
            LoadImpressoras(cbxImpressoraBalcao);
        }

        private void cbxImpressoraMesa_DropDown(object sender, EventArgs e)
        {
            LoadImpressoras(cbxImpressoraMesa);
        }
    }
}
