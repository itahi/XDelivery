﻿namespace DexComanda
{
    partial class frmCadastroCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroCliente));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnAlteraRegiao = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTaxaEntrega = new System.Windows.Forms.TextBox();
            this.txtDataCadastro = new System.Windows.Forms.MaskedTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cbxRegiao = new System.Windows.Forms.ComboBox();
            this.txtDataNascimento = new System.Windows.Forms.MaskedTextBox();
            this.txtTelefone2 = new System.Windows.Forms.TextBox();
            this.lblTelefone2 = new System.Windows.Forms.Label();
            this.lblDataNascimento = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNomeCliente = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtObservacaoCliente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdicionarCliente = new System.Windows.Forms.Button();
            this.txtPontoReferencia = new System.Windows.Forms.TextBox();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.txtEndereco = new System.Windows.Forms.TextBox();
            this.txtCEP = new System.Windows.Forms.TextBox();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbHistorico = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ItemsPedidoGridView = new System.Windows.Forms.DataGridView();
            this.PedidosGridView = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.lblTotalPedido = new System.Windows.Forms.Label();
            this.lblMedia = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblQtd = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tbHistorico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsPedidoGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PedidosGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tbHistorico);
            this.tabControl1.Location = new System.Drawing.Point(-2, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(593, 448);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnAlteraRegiao);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.txtTaxaEntrega);
            this.tabPage1.Controls.Add(this.txtDataCadastro);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.cbxRegiao);
            this.tabPage1.Controls.Add(this.txtDataNascimento);
            this.tabPage1.Controls.Add(this.txtTelefone2);
            this.tabPage1.Controls.Add(this.lblTelefone2);
            this.tabPage1.Controls.Add(this.lblDataNascimento);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.txtNumero);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.txtEstado);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.txtNomeCliente);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.txtObservacaoCliente);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btnAdicionarCliente);
            this.tabPage1.Controls.Add(this.txtPontoReferencia);
            this.tabPage1.Controls.Add(this.txtCidade);
            this.tabPage1.Controls.Add(this.txtBairro);
            this.tabPage1.Controls.Add(this.txtEndereco);
            this.tabPage1.Controls.Add(this.txtCEP);
            this.tabPage1.Controls.Add(this.txtTelefone);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(585, 422);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dados do Cliente";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnAlteraRegiao
            // 
            this.btnAlteraRegiao.Location = new System.Drawing.Point(389, 327);
            this.btnAlteraRegiao.Name = "btnAlteraRegiao";
            this.btnAlteraRegiao.Size = new System.Drawing.Size(83, 25);
            this.btnAlteraRegiao.TabIndex = 58;
            this.btnAlteraRegiao.Text = "Altera Regiao";
            this.btnAlteraRegiao.UseVisualStyleBackColor = true;
            this.btnAlteraRegiao.Click += new System.EventHandler(this.AlteraRegiao);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(293, 314);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 13);
            this.label16.TabIndex = 57;
            this.label16.Text = "Valor Entrega";
            // 
            // txtTaxaEntrega
            // 
            this.txtTaxaEntrega.Enabled = false;
            this.txtTaxaEntrega.Location = new System.Drawing.Point(297, 330);
            this.txtTaxaEntrega.Name = "txtTaxaEntrega";
            this.txtTaxaEntrega.Size = new System.Drawing.Size(86, 20);
            this.txtTaxaEntrega.TabIndex = 56;
            // 
            // txtDataCadastro
            // 
            this.txtDataCadastro.Enabled = false;
            this.txtDataCadastro.Location = new System.Drawing.Point(15, 392);
            this.txtDataCadastro.Mask = "00/00/0000";
            this.txtDataCadastro.Name = "txtDataCadastro";
            this.txtDataCadastro.Size = new System.Drawing.Size(84, 20);
            this.txtDataCadastro.TabIndex = 55;
            this.txtDataCadastro.ValidatingType = typeof(System.DateTime);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 376);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 13);
            this.label15.TabIndex = 54;
            this.label15.Text = "Data Cadastro";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(137, 313);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 13);
            this.label14.TabIndex = 53;
            this.label14.Text = "Região de Entrega";
            // 
            // cbxRegiao
            // 
            this.cbxRegiao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxRegiao.DisplayMember = "NomeRegiao";
            this.cbxRegiao.FormattingEnabled = true;
            this.cbxRegiao.Location = new System.Drawing.Point(140, 329);
            this.cbxRegiao.Name = "cbxRegiao";
            this.cbxRegiao.Size = new System.Drawing.Size(148, 21);
            this.cbxRegiao.TabIndex = 52;
            this.cbxRegiao.ValueMember = "Codigo";
            this.cbxRegiao.SelectedIndexChanged += new System.EventHandler(this.cbxRegiao_SelectedIndexChanged);
            // 
            // txtDataNascimento
            // 
            this.txtDataNascimento.Location = new System.Drawing.Point(17, 329);
            this.txtDataNascimento.Mask = "00/00/0000";
            this.txtDataNascimento.Name = "txtDataNascimento";
            this.txtDataNascimento.Size = new System.Drawing.Size(84, 20);
            this.txtDataNascimento.TabIndex = 51;
            this.txtDataNascimento.ValidatingType = typeof(System.DateTime);
            // 
            // txtTelefone2
            // 
            this.txtTelefone2.Cursor = System.Windows.Forms.Cursors.No;
            this.txtTelefone2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefone2.Location = new System.Drawing.Point(440, 25);
            this.txtTelefone2.Name = "txtTelefone2";
            this.txtTelefone2.Size = new System.Drawing.Size(124, 29);
            this.txtTelefone2.TabIndex = 50;
            this.txtTelefone2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefone2_KeyPress);
            // 
            // lblTelefone2
            // 
            this.lblTelefone2.AutoSize = true;
            this.lblTelefone2.Location = new System.Drawing.Point(437, 2);
            this.lblTelefone2.Name = "lblTelefone2";
            this.lblTelefone2.Size = new System.Drawing.Size(61, 13);
            this.lblTelefone2.TabIndex = 49;
            this.lblTelefone2.Text = "Telefone 2:";
            // 
            // lblDataNascimento
            // 
            this.lblDataNascimento.AutoSize = true;
            this.lblDataNascimento.Location = new System.Drawing.Point(14, 313);
            this.lblDataNascimento.Name = "lblDataNascimento";
            this.lblDataNascimento.Size = new System.Drawing.Size(107, 13);
            this.lblDataNascimento.TabIndex = 48;
            this.lblDataNascimento.Text = "Data de Nascimento:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(450, 392);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 29);
            this.button1.TabIndex = 47;
            this.button1.Text = "Sair [ESC]";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtNumero
            // 
            this.txtNumero.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumero.Location = new System.Drawing.Point(502, 89);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(71, 29);
            this.txtNumero.TabIndex = 34;
            this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumero_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(499, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 13);
            this.label10.TabIndex = 46;
            this.label10.Text = "N.º:";
            // 
            // txtEstado
            // 
            this.txtEstado.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstado.Location = new System.Drawing.Point(478, 146);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Size = new System.Drawing.Size(95, 29);
            this.txtEstado.TabIndex = 39;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(475, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 45;
            this.label9.Text = "Estado:";
            // 
            // txtNomeCliente
            // 
            this.txtNomeCliente.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeCliente.Location = new System.Drawing.Point(15, 25);
            this.txtNomeCliente.Name = "txtNomeCliente";
            this.txtNomeCliente.Size = new System.Drawing.Size(276, 29);
            this.txtNomeCliente.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 44;
            this.label8.Text = "Nome:";
            // 
            // txtObservacaoCliente
            // 
            this.txtObservacaoCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservacaoCliente.Location = new System.Drawing.Point(297, 209);
            this.txtObservacaoCliente.Multiline = true;
            this.txtObservacaoCliente.Name = "txtObservacaoCliente";
            this.txtObservacaoCliente.Size = new System.Drawing.Size(272, 82);
            this.txtObservacaoCliente.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(300, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Observação:";
            // 
            // btnAdicionarCliente
            // 
            this.btnAdicionarCliente.Location = new System.Drawing.Point(301, 392);
            this.btnAdicionarCliente.Name = "btnAdicionarCliente";
            this.btnAdicionarCliente.Size = new System.Drawing.Size(143, 29);
            this.btnAdicionarCliente.TabIndex = 42;
            this.btnAdicionarCliente.Text = "Salvar [F12]";
            this.btnAdicionarCliente.UseVisualStyleBackColor = true;
            this.btnAdicionarCliente.Click += new System.EventHandler(this.AdicionarCliente);
            // 
            // txtPontoReferencia
            // 
            this.txtPontoReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPontoReferencia.Location = new System.Drawing.Point(16, 209);
            this.txtPontoReferencia.Multiline = true;
            this.txtPontoReferencia.Name = "txtPontoReferencia";
            this.txtPontoReferencia.Size = new System.Drawing.Size(272, 82);
            this.txtPontoReferencia.TabIndex = 41;
            // 
            // txtCidade
            // 
            this.txtCidade.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCidade.Location = new System.Drawing.Point(301, 146);
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.Size = new System.Drawing.Size(171, 29);
            this.txtCidade.TabIndex = 38;
            // 
            // txtBairro
            // 
            this.txtBairro.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBairro.Location = new System.Drawing.Point(15, 146);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(276, 29);
            this.txtBairro.TabIndex = 37;
            // 
            // txtEndereco
            // 
            this.txtEndereco.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndereco.Location = new System.Drawing.Point(140, 88);
            this.txtEndereco.Multiline = true;
            this.txtEndereco.Name = "txtEndereco";
            this.txtEndereco.Size = new System.Drawing.Size(356, 30);
            this.txtEndereco.TabIndex = 33;
            // 
            // txtCEP
            // 
            this.txtCEP.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCEP.Location = new System.Drawing.Point(15, 88);
            this.txtCEP.MaxLength = 8;
            this.txtCEP.Name = "txtCEP";
            this.txtCEP.Size = new System.Drawing.Size(119, 29);
            this.txtCEP.TabIndex = 30;
            this.txtCEP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConsultarEnderecoPorCep);
            this.txtCEP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCEP_KeyPress);
            // 
            // txtTelefone
            // 
            this.txtTelefone.Cursor = System.Windows.Forms.Cursors.No;
            this.txtTelefone.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefone.Location = new System.Drawing.Point(301, 25);
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(124, 29);
            this.txtTelefone.TabIndex = 29;
            this.txtTelefone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefone_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(137, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "Endereço:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Bairro:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(300, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Cidade:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Ponto de Referencia:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "CEP:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Telefone:";
            // 
            // tbHistorico
            // 
            this.tbHistorico.Controls.Add(this.lblQtd);
            this.tbHistorico.Controls.Add(this.label19);
            this.tbHistorico.Controls.Add(this.lblData);
            this.tbHistorico.Controls.Add(this.label22);
            this.tbHistorico.Controls.Add(this.lblMedia);
            this.tbHistorico.Controls.Add(this.label20);
            this.tbHistorico.Controls.Add(this.lblTotalPedido);
            this.tbHistorico.Controls.Add(this.label17);
            this.tbHistorico.Controls.Add(this.label13);
            this.tbHistorico.Controls.Add(this.label12);
            this.tbHistorico.Controls.Add(this.ItemsPedidoGridView);
            this.tbHistorico.Controls.Add(this.PedidosGridView);
            this.tbHistorico.Controls.Add(this.label11);
            this.tbHistorico.Controls.Add(this.btnConsultar);
            this.tbHistorico.Controls.Add(this.dtFim);
            this.tbHistorico.Controls.Add(this.dtInicio);
            this.tbHistorico.Location = new System.Drawing.Point(4, 22);
            this.tbHistorico.Name = "tbHistorico";
            this.tbHistorico.Padding = new System.Windows.Forms.Padding(3);
            this.tbHistorico.Size = new System.Drawing.Size(585, 422);
            this.tbHistorico.TabIndex = 1;
            this.tbHistorico.Text = "Histórico de Pedidos";
            this.tbHistorico.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(21, 196);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(181, 24);
            this.label13.TabIndex = 7;
            this.label13.Text = "Items dos Pedidos";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(21, 53);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 24);
            this.label12.TabIndex = 6;
            this.label12.Text = "Pedidos";
            // 
            // ItemsPedidoGridView
            // 
            this.ItemsPedidoGridView.AllowUserToAddRows = false;
            this.ItemsPedidoGridView.AllowUserToDeleteRows = false;
            this.ItemsPedidoGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ItemsPedidoGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ItemsPedidoGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemsPedidoGridView.Location = new System.Drawing.Point(25, 223);
            this.ItemsPedidoGridView.MultiSelect = false;
            this.ItemsPedidoGridView.Name = "ItemsPedidoGridView";
            this.ItemsPedidoGridView.ReadOnly = true;
            this.ItemsPedidoGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ItemsPedidoGridView.Size = new System.Drawing.Size(553, 181);
            this.ItemsPedidoGridView.TabIndex = 5;
            // 
            // PedidosGridView
            // 
            this.PedidosGridView.AllowUserToAddRows = false;
            this.PedidosGridView.AllowUserToDeleteRows = false;
            this.PedidosGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PedidosGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PedidosGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PedidosGridView.Location = new System.Drawing.Point(25, 80);
            this.PedidosGridView.MultiSelect = false;
            this.PedidosGridView.Name = "PedidosGridView";
            this.PedidosGridView.ReadOnly = true;
            this.PedidosGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PedidosGridView.Size = new System.Drawing.Size(553, 106);
            this.PedidosGridView.TabIndex = 4;
            this.PedidosGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MostraItems);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(75, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Pedidos entre";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(219, 26);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 2;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.ConsultarPedidos);
            // 
            // dtFim
            // 
            this.dtFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFim.Location = new System.Drawing.Point(115, 26);
            this.dtFim.Name = "dtFim";
            this.dtFim.Size = new System.Drawing.Size(87, 20);
            this.dtFim.TabIndex = 1;
            // 
            // dtInicio
            // 
            this.dtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtInicio.Location = new System.Drawing.Point(25, 26);
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Size = new System.Drawing.Size(82, 20);
            this.dtInicio.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(466, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 13);
            this.label17.TabIndex = 8;
            this.label17.Text = "Total R$";
            // 
            // lblTotalPedido
            // 
            this.lblTotalPedido.AutoSize = true;
            this.lblTotalPedido.Location = new System.Drawing.Point(515, 26);
            this.lblTotalPedido.Name = "lblTotalPedido";
            this.lblTotalPedido.Size = new System.Drawing.Size(28, 13);
            this.lblTotalPedido.TabIndex = 9;
            this.lblTotalPedido.Text = "0,00";
            // 
            // lblMedia
            // 
            this.lblMedia.AutoSize = true;
            this.lblMedia.Location = new System.Drawing.Point(515, 42);
            this.lblMedia.Name = "lblMedia";
            this.lblMedia.Size = new System.Drawing.Size(28, 13);
            this.lblMedia.TabIndex = 11;
            this.lblMedia.Text = "0,00";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(475, 42);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(39, 13);
            this.label20.TabIndex = 10;
            this.label20.Text = "Média ";
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(515, 62);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(65, 13);
            this.lblData.TabIndex = 13;
            this.lblData.Text = "01/01/2015";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(455, 62);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 13);
            this.label22.TabIndex = 12;
            this.label22.Text = "Ult. Pedido";
            // 
            // lblQtd
            // 
            this.lblQtd.AutoSize = true;
            this.lblQtd.Location = new System.Drawing.Point(515, 11);
            this.lblQtd.Name = "lblQtd";
            this.lblQtd.Size = new System.Drawing.Size(13, 13);
            this.lblQtd.TabIndex = 15;
            this.lblQtd.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(429, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 13);
            this.label19.TabIndex = 14;
            this.label19.Text = "Quant. Pedidos";
            // 
            // frmCadastroCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 450);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmCadastroCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro Cliente - DexComanda";
            this.Load += new System.EventHandler(this.frmCadastroCliente_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCadastroCliente_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tbHistorico.ResumeLayout(false);
            this.tbHistorico.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsPedidoGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PedidosGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.MaskedTextBox txtDataNascimento;
        public System.Windows.Forms.TextBox txtTelefone2;
        private System.Windows.Forms.Label lblTelefone2;
        private System.Windows.Forms.Label lblDataNascimento;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNomeCliente;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtObservacaoCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdicionarCliente;
        private System.Windows.Forms.TextBox txtPontoReferencia;
        private System.Windows.Forms.TextBox txtCidade;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.TextBox txtEndereco;
        private System.Windows.Forms.TextBox txtCEP;
        public System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tbHistorico;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.DataGridView PedidosGridView;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView ItemsPedidoGridView;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbxRegiao;
        private System.Windows.Forms.MaskedTextBox txtDataCadastro;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtTaxaEntrega;
        private System.Windows.Forms.Button btnAlteraRegiao;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblMedia;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblTotalPedido;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblQtd;
        private System.Windows.Forms.Label label19;

    }
}