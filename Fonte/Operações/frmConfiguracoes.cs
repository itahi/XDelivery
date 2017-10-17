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
using DexComanda.Models.Configuracoes;
using DexComanda.Operações;

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
          //  this.pInfoUserDefault.Visible = false;
        }

        private void ConectarBanco(object sender, EventArgs e)
        {
            var servidor = this.txtServidor.Text;
            var banco = this.txtBanco.Text;

            Conexao con = new Conexao();
            con.OpenConection(servidor, banco);

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
        private string HorariosFuncionamento()
        {
            List<HorarioFuncionamento> listHorario = new List<HorarioFuncionamento>();
            //try
            //{
            //    var listaHoras = new HorarioFuncionamento();
            //    for (int i = 0; i < gridHorarios.Columns.Count; i++)
            //    {
            //        listaHoras.Dia = gridHorarios.Columns[i].Name.ToString();
            //        for (int intFor = 0; intFor < gridHorarios.Rows.Count; intFor++)
            //        {
            //            listaHoras.Inicio = gridHorarios.Rows[intFor].Cells[0].Value.ToString();
            //            listaHoras.Fim = gridHorarios.Rows[intFor].Cells[0].Value.ToString();
            //        }
            //    }

            //    listHorario.Add(listaHoras);

            //}
            //catch (Exception erro)
            //{
            //    MessageBox.Show(erro.Message);
            //}



            return JsonConvert.SerializeObject(listHorario, Formatting.None);

        }
        //private string LeConfiguracaoSMS(string strJson)
        //{
        //    try
        //    {
        //        foreach (var item in Utils.DeserializaObjetoSMS(strJson))
        //        {
        //            foreach (var item in collection)
        //            {

        //            }
        //        }
               
        //    }
        //    catch (Exception erro)
        //    {
        //        MessageBox.Show(erro.Message);
        //    }
        //}
        private string GravaConfiguracaoSMS()
        {
            string strReturn = "";
            try
            {
                DadosSMS sms = new DadosSMS();
                if (rbTww.Checked)
                {
                    sms = new DadosSMS
                    {
                        api = "tww",
                        login = txtLoginTww.Text,
                        senha = txtSenhaTww.Text
                    };
                }
                else if (rbZenvia.Checked)
                {
                     sms = new DadosSMS()
                    {
                        api = "zenvia",
                        login = txtIDZenvia.Text,
                        senha = txtAgrZenvia.Text
                    };
                }
                strReturn = JsonConvert.SerializeObject(sms, Formatting.None);
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return strReturn;
        }
        private void MarcaDias(string iValores)
        {
            try
            {
                if (iValores == "")
                {
                    return;
                }
                List<FidelidadeDias> fidelidade = Utils.DeserializaObjetoFidelidade(iValores);
                foreach (var item in fidelidade)
                {
                    foreach (Control check in grpControleFidelidade.Controls)
                    {
                        if (object.ReferenceEquals(check.GetType(), typeof(System.Windows.Forms.CheckBox)))
                        {
                            if (((System.Windows.Forms.CheckBox)check).Tag.ToString() == item.DiaSemana)
                            {
                                ((System.Windows.Forms.CheckBox)check).Checked = true;
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }


        }
        private void MarcaConfiguracao(string iConfig)
        {
            try
            {
                if (iConfig == "" || iConfig == null)
                {
                    chkFidelidade.CheckState = CheckState.Unchecked;
                    return;
                }
                Fidelidade fidelida = new Fidelidade();
                fidelida = Utils.DeserializaObjeto5(iConfig);

                MarcaDias(fidelida.Dias);
                chkFidelidade.Checked = fidelida.AtivoSN == true;
                rbPorPonto.Checked = fidelida.Tipo == "Por Ponto";
                rbPorValor.Checked = fidelida.Tipo == "Por Valor";
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
        private List<FidelidadeDias> DiasFidelidadeMarcados()
        {
            List<FidelidadeDias> listFidelidade = new List<FidelidadeDias>();
            try
            {
                foreach (Control item in grpControleFidelidade.Controls)
                {
                    if (object.ReferenceEquals(item.GetType(), typeof(System.Windows.Forms.CheckBox)))
                    {
                        //Check to see if it's a textbox 
                        if (((System.Windows.Forms.CheckBox)item).Checked)
                        {
                            var fidelidade = new FidelidadeDias()
                            {
                                DiaSemana = (((System.Windows.Forms.CheckBox)item).Tag.ToString()),
                            };
                            listFidelidade.Add(fidelidade);
                        }

                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return listFidelidade;
        }
        private string ConfigFidelidade()
        {
            string iReturn = "";
            if (!chkFidelidade.Checked)
            {
                return iReturn;
            }

            Fidelidade fidelidade = new Fidelidade();
            fidelidade.AtivoSN = true;
            fidelidade.Multiplicador = int.Parse(txtMultiplicador.Text);
            fidelidade.Dias = Utils.SerializaObjeto(DiasFidelidadeMarcados());
            if (rbPorPonto.Checked)
            {
                fidelidade.Tipo = "Por Ponto";
            }
            else
            {
                fidelidade.Tipo = "Por Valor";
            }

            return Utils.SerializaObjeto(fidelidade);
        }
        private string RetornaTipoBuscaProduto()
        {
            ConfiguracaoBuscaPorCodigo conf = new ConfiguracaoBuscaPorCodigo();
            try
            {
                conf = new ConfiguracaoBuscaPorCodigo()
                {
                    PorCodigo = chkProdutoCodigo.Checked,
                    TipoCodigo = cbxTipoCodigo.Text
                };
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return Utils.SerializaObjeto(conf);
        }
        public string ConfiguracaoCozinha()
        {
            ImpressaoMesa impressora = new ImpressaoMesa();
            try
            {
                impressora = new ImpressaoMesa()
                {
                    ImprimeSN = chkViaCozinha.Checked,
                    TipoAgrupamento = cbxAgrupamentoCozinha.Text,
                    ViaCozinha = txtViasCozinha.Text
                };
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return Utils.SerializaObjeto(impressora);
        }
        public string ConfiguracaoDelivery()
        {
            ImpressaoDelivery impressora = new ImpressaoDelivery();
            try
            {
                impressora = new ImpressaoDelivery()
                {
                    ImprimeSN = chkViaEntrega.Checked,
                    TipoAgrupamento = cbxAgrupamentoDelivery.Text,
                    ViaDelivery = txtViasEntrega.Text
                };
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return Utils.SerializaObjeto(impressora);
        }
        public string ConfiguracaoBalcao()
        {
            ImpressaoBalcao impressora = new ImpressaoBalcao();
            try
            {
                impressora = new ImpressaoBalcao()
                {
                    ImprimeSN = chkViaBalcao.Checked,
                    TipoAgrupamento = cbxAgrupamentoBalcao.Text,
                    ViaBalcao = txtViasBalcao.Text
                };
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
            return Utils.SerializaObjeto(impressora);
        }
        private string GravaConfiguracaoImpressora()
        {
            DadosImpressoras impressoras = new DadosImpressoras();
            try
            {
                impressoras = new DadosImpressoras()
                {
                    ImpressoraBalcao = cbxImpressoraBalcao.Text,
                    ImpressoraContaMesa = cbxImpressoraMesa.Text,
                    ImpressoraDelivery = cbxImpressoraDelivery.Text,
                };
            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }

            return Utils.SerializaObjeto(impressoras);
        }
        private string GravaDadosPush()
        {
            DadosPush dadosPush = new DadosPush();
            try
            {
                dadosPush = new DadosPush()
                {
                    GCM = txtGoogleProjetc.Text,
                    Pushapp_id = txtAPPID.Text,
                    Pushauthorization = txtCodAutorização.Text
                };

            }
            catch (Exception)
            {
                //  MessageBox.Show()
            }

            return Utils.SerializaObjetoDadosPush(dadosPush);
        }
        private void SalvaConfig(object sender, EventArgs e)
        {
            con = new Conexao();
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
                HorarioFuncionamento = HorariosFuncionamento(),
                ConfiguracaoSMS = GravaConfiguracaoSMS(),
                Id_loja = int.Parse(txt_IdLoja.Text)
            };

            // Grava as configurações
            config.ImprimeViaBalcao = ConfiguracaoBalcao();
            config.ImpViaCozinha = ConfiguracaoCozinha();
            config.ImprimeViaEntrega = ConfiguracaoDelivery();
            config.UsaLoginSenha = true;//chkLoginSenha.Checked;
            config.UsaDataNascimento = chkDataNAscimento.Checked;
            config.ControlaEntregador = chkEntregadores.Checked;
            config.ProdutoPorCodigo = RetornaTipoBuscaProduto();
            config.Usa2Telefones = chk2Telefones.Checked;
            config.UsaControleMesa = chkControlaMesas.Checked;
            
            config.DescontoDiaSemana = chkDescontoDiasemana.Checked;
            config.ControlaFidelidade = ConfigFidelidade();
            config.EnviaSMS = chkEnviaSms.Checked;
            config.RegistraCancelamentos = chkRegCancelamentos.Checked;
            config.DadosApp = Utils.GravaJson(cbxPlataforma1.Text, txtLink2.Text);
            config.RepeteUltimoPedido = chkUltPedido.Checked;
            config.CidadesAtendidas = Utils.SerializaObjeto(CidadesAtendidas());
            config.ExigeVendedorSN = chkVendedor.Checked;
            config.CobrancaProporcionalSN = chkProporcional.Checked;
            config.QtdCaracteresImp = int.Parse(cbxColunas.Text);
            config.Impressoras = GravaConfiguracaoImpressora();
            config.DadosPush = GravaDadosPush();
            //if (chkLoginSenha.Checked && txtUsuarioPadrao.Text.Trim() != "" && txtSenhaPadrao.Text.Trim() != "")
            //{
            //    string _senha = Utils.EncryptMd5(this.txtUsuarioPadrao.Text.ToString(), this.txtSenhaPadrao.Text.ToString());
            //    Usuario usuario = new Usuario()
            //    {
            //        Nome = this.txtUsuarioPadrao.Text.ToString(),
            //        Senha = _senha,
            //        FinalizaPedidoSN = true,
            //        AdministradorSN = true,
            //        AbreFechaCaixaSN = true
            //    };
            //    con.Insert("spAdicionarUsuario", usuario);
            //}


            this.btnSalvar.Click -= AlterarConfig;
            this.btnSalvar.Click += SalvaConfig;
            if (empresa.Nome != "" && empresa.Contato != "" && empresa.Telefone != "" && txtViasBalcao.Text != "" && txtViasCozinha.Text != "" && txtViasEntrega.Text != "" && txtCaminhoBkp.Text != "")
            {
                con.Insert("spAdicionarEmpresa", empresa);
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
                HorarioFuncionamento = HorariosFuncionamento(),
                ConfiguracaoSMS = GravaConfiguracaoSMS(),
                Id_loja = int.Parse(txt_IdLoja.Text)
            };

            config.cod = Sessions.returnConfig.cod;
            config.ImpViaCozinha = ConfiguracaoCozinha();
            config.UsaLoginSenha = true;
            config.UsaDataNascimento = chkDataNAscimento.Checked;
            config.ControlaEntregador = chkEntregadores.Checked;
            config.ProdutoPorCodigo = RetornaTipoBuscaProduto();
            config.Usa2Telefones = chk2Telefones.Checked;
            config.UsaControleMesa = chkControlaMesas.Checked;
            config.ImprimeViaEntrega = ConfiguracaoDelivery();
            config.ImprimeViaBalcao = ConfiguracaoBalcao();
            config.ControlaFidelidade = ConfigFidelidade();
            config.DescontoDiaSemana = chkDescontoDiasemana.Checked;
            config.QtdCaracteresImp = int.Parse(cbxColunas.Text);
            config.CobraTaxaGarcon = chk10Garcon.Checked;
            config.EnviaSMS = chkEnviaSms.Checked;
            config.RepeteUltimoPedido = chkUltPedido.Checked;
            config.RegistraCancelamentos = chkRegCancelamentos.Checked;
            config.DadosApp = Utils.GravaJson(cbxPlataforma1.Text, txtLink1.Text);
            config.RepeteUltimoPedido = chkUltPedido.Checked;
            config.CidadesAtendidas = Utils.SerializaObjeto(CidadesAtendidas());
            config.ExigeVendedorSN = chkVendedor.Checked;
            config.CobrancaProporcionalSN = chkProporcional.Checked;
            config.Impressoras = GravaConfiguracaoImpressora();
            config.DadosPush = GravaDadosPush();
            if (chkEnviaSms.Checked)
            {
                Utils.CriaArquivoTxt("ConfigSMS", txtLoginTww.Text + "-" + txtSenhaTww.Text);
            }

            //if (chkLoginSenha.Checked && txtUsuarioPadrao.Text.Trim() != "" && txtSenhaPadrao.Text.Trim() != "")
            //{
            //    string _senha = Utils.EncryptMd5(this.txtUsuarioPadrao.Text.ToString(), this.txtSenhaPadrao.Text.ToString());
            //    UsuarioDefault usuario = new UsuarioDefault()
            //    {
            //        Nome = this.txtUsuarioPadrao.Text.ToString(),
            //        senha = _senha,
            //        AdministradorSN = true
            //        ,
            //        AbreFechaCaixaSN = true
            //    };
            //    con.Insert("spAdicionarUsuarioDefault", usuario);
            //}

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


        private void LoadImpressoras(ComboBox icbx)
        {
            icbx.Items.Clear();
            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                icbx.Items.Add(item);
            }

        }
        private void CarregaConfigSMS(string iValores)
        {
            if (!Sessions.returnConfig.EnviaSMS)
            {
                return;
            }
            DadosSMS sms = new DadosSMS();
            sms = Utils.DeserializaObjetoSMS(iValores);
            chkEnviaSms.Checked = true;
            if (iValores.Contains("zenvia"))
            {

                txtAgrZenvia.Text = sms.api;
                txtIDZenvia.Text = sms.login;
                rbZenvia.Checked = true;
            }
            else
            {
                txtLoginTww.Text = sms.login;
                txtSenhaTww.Text = sms.senha;
                rbTww.Checked = true;
            }

        }
        private void frmConfiguracoes_Load(object sender, EventArgs e)
        {
            if (con.statusConexao != ConnectionState.Open)
            {
                return;
            }
            MarcaConfiguracaoExibicao();
            if (Sessions.returnConfig != null)
            {
              //  grpFidelidade.Enabled = chkFidelidade.Checked;
                chkDataNAscimento.Checked = Sessions.returnConfig.UsaDataNascimento;
                chkEntregadores.Checked = Sessions.returnConfig.ControlaEntregador;
                chkProdutoCodigo.Checked = Utils.MarcaTipoConfiguracaoProduto().PorCodigo;
                cbxTipoCodigo.Text = Utils.MarcaTipoConfiguracaoProduto().TipoCodigo;
                chk2Telefones.Checked = Sessions.returnConfig.Usa2Telefones;
                chkControlaMesas.Checked = Sessions.returnConfig.UsaControleMesa;
                MarcaConfiguracao(Sessions.returnConfig.ControlaFidelidade);
                chkDescontoDiasemana.Checked = Sessions.returnConfig.DescontoDiaSemana;
                cbxColunas.Text = Sessions.returnConfig.QtdCaracteresImp.ToString();
                chk10Garcon.Checked = Sessions.returnConfig.CobraTaxaGarcon;
                chkEnviaSms.Checked = Sessions.returnConfig.EnviaSMS;
                chkUltPedido.Checked = Sessions.returnConfig.RepeteUltimoPedido;
                chkRegCancelamentos.Checked = Sessions.returnConfig.RegistraCancelamentos;
                txtAPPID.Text = Utils.RetornaConfiguracaoPush().Pushapp_id;
                txtCodAutorização.Text = Utils.RetornaConfiguracaoPush().Pushauthorization;
                chkVendedor.Checked = Sessions.returnConfig.ExigeVendedorSN;
                PreencheCidades(Sessions.returnConfig.CidadesAtendidas);
                txtGoogleProjetc.Text = Utils.RetornaConfiguracaoPush().GCM;
                cbxPlataforma1.Text = Utils.RetornaDadosApp().plataforma;
                txtLink1.Text = Utils.RetornaDadosApp().url;
                chkProporcional.Checked = Sessions.returnConfig.CobrancaProporcionalSN;
                grpSms.Enabled = Sessions.returnConfig.EnviaSMS;


                chkViaBalcao.Checked = Utils.RetornaConfiguracaoBalcao().ImprimeSN;
                cbxAgrupamentoBalcao.Text = Utils.RetornaConfiguracaoBalcao().TipoAgrupamento;
                txtViasBalcao.Text = Utils.RetornaConfiguracaoBalcao().ViaBalcao;
                cbxImpressoraBalcao.Text = Utils.RetornaImpressoras().ImpressoraBalcao;

                chkViaCozinha.Checked = Utils.RetornaConfiguracaoMesa().ImprimeSN;
                cbxAgrupamentoCozinha.Text = Utils.RetornaConfiguracaoMesa().TipoAgrupamento;
                txtViasCozinha.Text = Utils.RetornaConfiguracaoMesa().ViaCozinha;
                cbxImpressoraMesa.Text = Utils.RetornaImpressoras().ImpressoraContaMesa;

                chkViaEntrega.Checked = Utils.RetornaConfiguracaoDelivery().ImprimeSN;
                cbxAgrupamentoDelivery.Text = Utils.RetornaConfiguracaoDelivery().TipoAgrupamento;
                txtViasEntrega.Text = Utils.RetornaConfiguracaoDelivery().ViaDelivery;
                cbxImpressoraDelivery.Text = Utils.RetornaImpressoras().ImpressoraDelivery;

                this.btnSalvar.Text = "Alterar";
                this.btnSalvar.Click -= SalvaConfig;
                this.btnSalvar.Click += AlterarConfig;

            }

            //  Exibir DataLiberação Sistema
            DataSet servidorLocal = con.SelectAll("Empresa", "spObterEmpresa");
            if (servidorLocal.Tables[0].Rows.Count > 0)
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
                txtURL.Text = servidorLocal.Tables[0].Rows[0].Field<string>("UrlServidor");//Sessions.returnEmpresa.UrlServidor;
                CarregaConfigSMS(servidorLocal.Tables[0].Rows[0].Field<string>("ConfiguracaoSMS"));
                txt_IdLoja.Text = servidorLocal.Tables[0].Rows[0].Field<int>("Id_Loja").ToString();
            }

        }

        private void ConsultarCEP(object sender, KeyEventArgs e)
        {
            if (txtCEP.Text.Trim().Length == 8 && e.KeyCode != Keys.Back)
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
        }
        private void PreencheCidades(string iListaJson)
        {
            try
            {
                List<CidadesAtendidas> cidades = new List<CidadesAtendidas>();
                if (iListaJson == "" || iListaJson == null)
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
        private void chkEnviaSms_CheckedChanged(object sender, EventArgs e)
        {
            grpSms.Enabled = chkEnviaSms.Checked;
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
                Application.DoEvents();
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
            foreach (Control item in grpProdutos.Controls)
            {

                if (object.ReferenceEquals(item.GetType(), typeof(System.Windows.Forms.CheckBox)))
                {
                    //Check to see if it's a checkbox e Checked 
                    if (((System.Windows.Forms.CheckBox)item).Checked)
                    {
                        strConfiProduto = strConfiProduto + "," + ((System.Windows.Forms.CheckBox)item).Tag;
                    }
                }
            }
            Utils.SalvarConfiguracao("GridProduto", strConfiProduto);

        }

        private void SalvarConfigPedido(object sender, EventArgs e)
        {
            string strConfigPedido = "Pd.Codigo,";
            if (chkNomeCliente.Checked)
            {
                strConfigPedido = strConfigPedido + " case PD.Tipo when '1 - Mesa' then 'Mesa' + ' - ' + PD.NumeroMesa when '2 - Balcao' then 'Cliente Balcao ' + PD.Senha +' '+Pd.Observacao when '0 - Entrega' then P.Nome end as  'Nome Cliente'";
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
            if (chkSenha.Checked)
            {
                strConfigPedido = strConfigPedido + ",Senha";
            }

            Utils.SalvarConfiguracao("GridPedido", strConfigPedido);
        }
        /// <summary>
        /// Marca os checkBox de configuração da tela de Exibição
        /// </summary>
        private void MarcaConfiguracaoExibicao()
        {
            string strProduto = Sessions.SqlProduto;
            string strSqlPessoa = Sessions.SqlPessoa;

            if (strProduto == null || strSqlPessoa == null)
            {
                return;
            }
            string[] strListaProduto = strProduto.Split(new char[] { ',' });
            string[] strListaPessoa = strSqlPessoa.Split(new char[] { ',' });
            foreach (var item in strListaProduto)
            {
                foreach (Control controlProduto in grpProdutos.Controls)
                {
                    if (object.ReferenceEquals(controlProduto.GetType(), typeof(System.Windows.Forms.CheckBox)))
                    {
                        if (((System.Windows.Forms.CheckBox)controlProduto).Tag.ToString() == item)
                        {
                            ((System.Windows.Forms.CheckBox)controlProduto).Checked = true;
                        }
                    }

                }
            }
            foreach (var item in strListaPessoa)
            {
                foreach (Control controlPessoa in grpPessoas.Controls)
                {
                    if (object.ReferenceEquals(controlPessoa.GetType(), typeof(System.Windows.Forms.CheckBox)))
                    {
                        if (item == ((System.Windows.Forms.CheckBox)controlPessoa).Tag.ToString())
                        {
                            ((System.Windows.Forms.CheckBox)controlPessoa).Checked = true;
                        }
                    }

                }
            }
        }

        private void btnSalvarConfigPessoas_Click(object sender, EventArgs e)
        {
            string strConfiPessoa = "Codigo";

            foreach (Control controPessoa in grpPessoas.Controls)
            {
                if (object.ReferenceEquals(controPessoa.GetType(), typeof(System.Windows.Forms.CheckBox)))
                {
                    //Check to see if it's a checkbox e Checked 
                    if (((System.Windows.Forms.CheckBox)controPessoa).Checked)
                    {
                        strConfiPessoa += "," + ((System.Windows.Forms.CheckBox)controPessoa).Tag;
                    }
                }
            }

            Utils.SalvarConfiguracao("GridPessoa", strConfiPessoa);
        }

        private void btnSalvarSMS_Click(object sender, EventArgs e)
        {
            Utils.SalvarConfiguracao("ConfigSMS", txtLoginTww.Text + "," + txtSenhaTww.Text);
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
        private void chkImprimeViaEntrega_CheckedChanged(object sender, EventArgs e)
        {
            if (chkViaEntrega.Checked)
            {
                txtViasEntrega.Text = "1";
            }
            else
            {
                txtViasEntrega.Text = "0";
            }
        }

        private void SalvaLocaSMS(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbZenvia_CheckedChanged(object sender, EventArgs e)
        {
            grpTww.Enabled = rbZenvia.Checked;
        }

        private void rbLocaSMS_CheckedChanged(object sender, EventArgs e)
        {
            grpLocaTww.Enabled = rbTww.Checked;
        }

        private void chkProdutoCodigo_CheckStateChanged(object sender, EventArgs e)
        {
            cbxTipoCodigo.Enabled = chkProdutoCodigo.Checked;
        }

        private void chkViaBalcao_CheckedChanged(object sender, EventArgs e)
        {
            txtViasBalcao.Text = "1";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmConectaBanco frm = new frmConectaBanco();
            frm.Show();
        }

        private void txt_IdLoja_DoubleClick(object sender, EventArgs e)
        {
            if (!Utils.ImputStringQuestion())
            {
                return;
            }
            txt_IdLoja.Enabled = true;
        }

        private void txt_IdLoja_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SoPermiteNumeros(e);
        }
        private void txtCNPJ_KeyDown(object sender, KeyEventArgs e)
        {
            //if (txtCNPJ.Text.Trim().Length==14 && (e.KeyCode != Keys.Back && e.KeyCode !=Keys.Delete))
            //{
            //  e.Handled =!  Utils.ValidaCNPJ(txtCNPJ.Text);
            //}
        }

        private void chkFidelidade_CheckedChanged(object sender, EventArgs e)
        {
            grpControleFidelidade.Enabled = chkFidelidade.CheckState == CheckState.Checked;
        }

        private void chkFidelidade_CheckStateChanged(object sender, EventArgs e)
        {

        }
    }
}
