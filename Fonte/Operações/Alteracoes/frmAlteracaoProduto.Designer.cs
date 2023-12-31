﻿namespace DexComanda.Operações.Alteracoes
{
    partial class frmAlteracaoProduto
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label5;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAlteracaoProduto));
            this.grpFiltros = new System.Windows.Forms.GroupBox();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxGrupo = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GridViewProdutos = new System.Windows.Forms.DataGridView();
            this.btnExecutar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkAlteraPreco = new System.Windows.Forms.CheckBox();
            this.chkAtivaDesconto = new System.Windows.Forms.CheckBox();
            this.grpDesconto = new System.Windows.Forms.GroupBox();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.chkDomingo = new System.Windows.Forms.CheckBox();
            this.txtPorcentagem = new System.Windows.Forms.TextBox();
            this.ChkSexta = new System.Windows.Forms.CheckBox();
            this.chkQuinta = new System.Windows.Forms.CheckBox();
            this.ChkSabado = new System.Windows.Forms.CheckBox();
            this.ChkQuarta = new System.Windows.Forms.CheckBox();
            this.chkTerca = new System.Windows.Forms.CheckBox();
            this.chkSegunda = new System.Windows.Forms.CheckBox();
            this.grpPrecos = new System.Windows.Forms.GroupBox();
            this.richTextGrande = new System.Windows.Forms.RichTextBox();
            this.rbValor = new System.Windows.Forms.RadioButton();
            this.rbPorcentagem = new System.Windows.Forms.RadioButton();
            this.txtnewValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOpcao = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlAdicionais = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxTipoOpcao = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAdicionarOpcao = new System.Windows.Forms.Button();
            this.txtPrecoOpcao = new System.Windows.Forms.TextBox();
            this.cbxOpcao = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.AdicionaisGridView = new System.Windows.Forms.DataGridView();
            this.CodOpcao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlInsumos = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnInsumoSalvar = new System.Windows.Forms.Button();
            this.btnInsumoEditar = new System.Windows.Forms.Button();
            this.btnInsumoAdd = new System.Windows.Forms.Button();
            this.txtQtdInsumo = new System.Windows.Forms.TextBox();
            this.cbxInsumo = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.InsumoGridView = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.CodInsumo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Insumo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnInsumoVincular = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            this.grpFiltros.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewProdutos)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.grpDesconto.SuspendLayout();
            this.grpPrecos.SuspendLayout();
            this.pnlAdicionais.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AdicionaisGridView)).BeginInit();
            this.pnlInsumos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InsumoGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(3, 66);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(70, 13);
            label6.TabIndex = 24;
            label6.Text = "Porcentagem";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(8, 99);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(97, 13);
            label5.TabIndex = 35;
            label5.Text = "Validade Desconto";
            // 
            // grpFiltros
            // 
            this.grpFiltros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFiltros.Controls.Add(this.chkTodos);
            this.grpFiltros.Controls.Add(this.btnFiltrar);
            this.grpFiltros.Controls.Add(this.label1);
            this.grpFiltros.Controls.Add(this.cbxGrupo);
            this.grpFiltros.Location = new System.Drawing.Point(7, 2);
            this.grpFiltros.Name = "grpFiltros";
            this.grpFiltros.Size = new System.Drawing.Size(788, 72);
            this.grpFiltros.TabIndex = 0;
            this.grpFiltros.TabStop = false;
            this.grpFiltros.Text = "Filtro";
            // 
            // chkTodos
            // 
            this.chkTodos.AutoSize = true;
            this.chkTodos.Location = new System.Drawing.Point(301, 41);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(101, 17);
            this.chkTodos.TabIndex = 27;
            this.chkTodos.Text = "Todos Produtos";
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(231, 35);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(64, 26);
            this.btnFiltrar.TabIndex = 26;
            this.btnFiltrar.Text = "Buscar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.Filtro);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Grupos";
            // 
            // cbxGrupo
            // 
            this.cbxGrupo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbxGrupo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGrupo.FormattingEnabled = true;
            this.cbxGrupo.Location = new System.Drawing.Point(9, 35);
            this.cbxGrupo.Name = "cbxGrupo";
            this.cbxGrupo.Size = new System.Drawing.Size(216, 26);
            this.cbxGrupo.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.GridViewProdutos);
            this.groupBox1.Location = new System.Drawing.Point(4, 275);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(790, 189);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resultado - Filtro";
            // 
            // GridViewProdutos
            // 
            this.GridViewProdutos.AllowUserToAddRows = false;
            this.GridViewProdutos.AllowUserToDeleteRows = false;
            this.GridViewProdutos.AllowUserToOrderColumns = true;
            this.GridViewProdutos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridViewProdutos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GridViewProdutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewProdutos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridViewProdutos.Location = new System.Drawing.Point(3, 16);
            this.GridViewProdutos.MultiSelect = false;
            this.GridViewProdutos.Name = "GridViewProdutos";
            this.GridViewProdutos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridViewProdutos.Size = new System.Drawing.Size(784, 170);
            this.GridViewProdutos.TabIndex = 2;
            this.GridViewProdutos.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GridViewProdutos_MouseClick);
            // 
            // btnExecutar
            // 
            this.btnExecutar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExecutar.Location = new System.Drawing.Point(258, 467);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(64, 23);
            this.btnExecutar.TabIndex = 2;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecutar.UseVisualStyleBackColor = true;
            this.btnExecutar.Click += new System.EventHandler(this.ExecutarAlteracoes);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelar.Location = new System.Drawing.Point(328, 467);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkAlteraPreco);
            this.groupBox2.Controls.Add(this.chkAtivaDesconto);
            this.groupBox2.Controls.Add(this.grpDesconto);
            this.groupBox2.Controls.Add(this.grpPrecos);
            this.groupBox2.Location = new System.Drawing.Point(7, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(788, 200);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Alterações";
            // 
            // chkAlteraPreco
            // 
            this.chkAlteraPreco.AutoSize = true;
            this.chkAlteraPreco.Location = new System.Drawing.Point(9, 14);
            this.chkAlteraPreco.Name = "chkAlteraPreco";
            this.chkAlteraPreco.Size = new System.Drawing.Size(96, 17);
            this.chkAlteraPreco.TabIndex = 36;
            this.chkAlteraPreco.Text = "Alterar Preço ?";
            this.chkAlteraPreco.UseVisualStyleBackColor = true;
            this.chkAlteraPreco.CheckedChanged += new System.EventHandler(this.chkAlteraPreco_CheckedChanged);
            // 
            // chkAtivaDesconto
            // 
            this.chkAtivaDesconto.AutoSize = true;
            this.chkAtivaDesconto.Location = new System.Drawing.Point(389, 11);
            this.chkAtivaDesconto.Name = "chkAtivaDesconto";
            this.chkAtivaDesconto.Size = new System.Drawing.Size(104, 17);
            this.chkAtivaDesconto.TabIndex = 35;
            this.chkAtivaDesconto.Text = "Ativar Promoção";
            this.toolTip1.SetToolTip(this.chkAtivaDesconto, "Ativa o desconto em porcetagem no filtro de produtos ");
            this.chkAtivaDesconto.UseVisualStyleBackColor = true;
            this.chkAtivaDesconto.CheckedChanged += new System.EventHandler(this.chkAtivaDesconto_CheckedChanged);
            // 
            // grpDesconto
            // 
            this.grpDesconto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDesconto.Controls.Add(label5);
            this.grpDesconto.Controls.Add(this.dtFim);
            this.grpDesconto.Controls.Add(this.dtInicio);
            this.grpDesconto.Controls.Add(this.richTextBox1);
            this.grpDesconto.Controls.Add(this.chkDomingo);
            this.grpDesconto.Controls.Add(label6);
            this.grpDesconto.Controls.Add(this.txtPorcentagem);
            this.grpDesconto.Controls.Add(this.ChkSexta);
            this.grpDesconto.Controls.Add(this.chkQuinta);
            this.grpDesconto.Controls.Add(this.ChkSabado);
            this.grpDesconto.Controls.Add(this.ChkQuarta);
            this.grpDesconto.Controls.Add(this.chkTerca);
            this.grpDesconto.Controls.Add(this.chkSegunda);
            this.grpDesconto.Location = new System.Drawing.Point(389, 38);
            this.grpDesconto.Name = "grpDesconto";
            this.grpDesconto.Size = new System.Drawing.Size(399, 153);
            this.grpDesconto.TabIndex = 34;
            this.grpDesconto.TabStop = false;
            this.grpDesconto.Text = "Dias Desconto";
            // 
            // dtFim
            // 
            this.dtFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFim.Location = new System.Drawing.Point(121, 120);
            this.dtFim.Name = "dtFim";
            this.dtFim.Size = new System.Drawing.Size(81, 20);
            this.dtFim.TabIndex = 34;
            // 
            // dtInicio
            // 
            this.dtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtInicio.Location = new System.Drawing.Point(25, 120);
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Size = new System.Drawing.Size(81, 20);
            this.dtInicio.TabIndex = 33;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox1.Location = new System.Drawing.Point(218, 11);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(172, 101);
            this.richTextBox1.TabIndex = 26;
            this.richTextBox1.Text = "Preço desconto será:\nPreço Produto - Porcentagem definida.\nExemplo:\nPreço R$10,00" +
    " \nMargem 10%\nPreço desconto R$9,00";
            // 
            // chkDomingo
            // 
            this.chkDomingo.AutoSize = true;
            this.chkDomingo.Location = new System.Drawing.Point(112, 39);
            this.chkDomingo.Name = "chkDomingo";
            this.chkDomingo.Size = new System.Drawing.Size(51, 17);
            this.chkDomingo.TabIndex = 25;
            this.chkDomingo.Text = "Dom.";
            this.chkDomingo.UseVisualStyleBackColor = true;
            // 
            // txtPorcentagem
            // 
            this.txtPorcentagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentagem.Location = new System.Drawing.Point(79, 58);
            this.txtPorcentagem.Name = "txtPorcentagem";
            this.txtPorcentagem.Size = new System.Drawing.Size(75, 26);
            this.txtPorcentagem.TabIndex = 23;
            this.toolTip1.SetToolTip(this.txtPorcentagem, "Preencha a pocentagem de desconto que deseja dar nos produtos.");
            // 
            // ChkSexta
            // 
            this.ChkSexta.AutoSize = true;
            this.ChkSexta.Location = new System.Drawing.Point(6, 36);
            this.ChkSexta.Name = "ChkSexta";
            this.ChkSexta.Size = new System.Drawing.Size(53, 17);
            this.ChkSexta.TabIndex = 22;
            this.ChkSexta.Text = "Sexta";
            this.ChkSexta.UseVisualStyleBackColor = true;
            // 
            // chkQuinta
            // 
            this.chkQuinta.AutoSize = true;
            this.chkQuinta.Location = new System.Drawing.Point(169, 13);
            this.chkQuinta.Name = "chkQuinta";
            this.chkQuinta.Size = new System.Drawing.Size(51, 17);
            this.chkQuinta.TabIndex = 21;
            this.chkQuinta.Text = "Quin.";
            this.chkQuinta.UseVisualStyleBackColor = true;
            // 
            // ChkSabado
            // 
            this.ChkSabado.AutoSize = true;
            this.ChkSabado.Location = new System.Drawing.Point(60, 38);
            this.ChkSabado.Name = "ChkSabado";
            this.ChkSabado.Size = new System.Drawing.Size(48, 17);
            this.ChkSabado.TabIndex = 18;
            this.ChkSabado.Text = "Sab.";
            this.ChkSabado.UseVisualStyleBackColor = true;
            // 
            // ChkQuarta
            // 
            this.ChkQuarta.AutoSize = true;
            this.ChkQuarta.Location = new System.Drawing.Point(111, 13);
            this.ChkQuarta.Name = "ChkQuarta";
            this.ChkQuarta.Size = new System.Drawing.Size(52, 17);
            this.ChkQuarta.TabIndex = 20;
            this.ChkQuarta.Text = "Quar.";
            this.ChkQuarta.UseVisualStyleBackColor = true;
            // 
            // chkTerca
            // 
            this.chkTerca.AutoSize = true;
            this.chkTerca.Location = new System.Drawing.Point(60, 15);
            this.chkTerca.Name = "chkTerca";
            this.chkTerca.Size = new System.Drawing.Size(45, 17);
            this.chkTerca.TabIndex = 19;
            this.chkTerca.Text = "Ter.";
            this.chkTerca.UseVisualStyleBackColor = true;
            // 
            // chkSegunda
            // 
            this.chkSegunda.AutoSize = true;
            this.chkSegunda.Location = new System.Drawing.Point(6, 15);
            this.chkSegunda.Name = "chkSegunda";
            this.chkSegunda.Size = new System.Drawing.Size(48, 17);
            this.chkSegunda.TabIndex = 18;
            this.chkSegunda.Text = "Seg.";
            this.chkSegunda.UseVisualStyleBackColor = true;
            // 
            // grpPrecos
            // 
            this.grpPrecos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPrecos.Controls.Add(this.richTextGrande);
            this.grpPrecos.Controls.Add(this.rbValor);
            this.grpPrecos.Controls.Add(this.rbPorcentagem);
            this.grpPrecos.Controls.Add(this.txtnewValue);
            this.grpPrecos.Controls.Add(this.label2);
            this.grpPrecos.Enabled = false;
            this.grpPrecos.Location = new System.Drawing.Point(6, 38);
            this.grpPrecos.Name = "grpPrecos";
            this.grpPrecos.Size = new System.Drawing.Size(376, 153);
            this.grpPrecos.TabIndex = 0;
            this.grpPrecos.TabStop = false;
            this.grpPrecos.Text = "Preço Normal";
            // 
            // richTextGrande
            // 
            this.richTextGrande.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextGrande.Location = new System.Drawing.Point(186, 11);
            this.richTextGrande.Name = "richTextGrande";
            this.richTextGrande.Size = new System.Drawing.Size(175, 117);
            this.richTextGrande.TabIndex = 4;
            this.richTextGrande.Text = "Escolhendo essa opção os produtos da \'Grid\' abaixo \n terão o seu valor atual soma" +
    "do a porcentagem preenchida no campo \'Novo Valor\' \n Ex. Refrigerante R$10,00 + 1" +
    "0%  = Novo valor R$11,00";
            // 
            // rbValor
            // 
            this.rbValor.AutoSize = true;
            this.rbValor.Location = new System.Drawing.Point(76, 40);
            this.rbValor.Name = "rbValor";
            this.rbValor.Size = new System.Drawing.Size(49, 17);
            this.rbValor.TabIndex = 3;
            this.rbValor.TabStop = true;
            this.rbValor.Text = "Valor";
            this.rbValor.UseVisualStyleBackColor = true;
            this.rbValor.Click += new System.EventHandler(this.rbValor_Click);
            // 
            // rbPorcentagem
            // 
            this.rbPorcentagem.AutoSize = true;
            this.rbPorcentagem.Checked = true;
            this.rbPorcentagem.Location = new System.Drawing.Point(76, 17);
            this.rbPorcentagem.Name = "rbPorcentagem";
            this.rbPorcentagem.Size = new System.Drawing.Size(99, 17);
            this.rbPorcentagem.TabIndex = 2;
            this.rbPorcentagem.TabStop = true;
            this.rbPorcentagem.Text = "Porcentagem %";
            this.rbPorcentagem.UseVisualStyleBackColor = true;
            this.rbPorcentagem.Click += new System.EventHandler(this.rbPorcentagem_Click);
            // 
            // txtnewValue
            // 
            this.txtnewValue.Location = new System.Drawing.Point(10, 41);
            this.txtnewValue.Name = "txtnewValue";
            this.txtnewValue.Size = new System.Drawing.Size(60, 20);
            this.txtnewValue.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Novo Preço";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 496);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(485, 26);
            this.label3.TabIndex = 28;
            this.label3.Text = "Para remover um produto da lista de alterações basca selecionar e clicar  com bot" +
    "ão direito do mouse\r\n ";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 523);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(374, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Você pode editar o nome do Produto diretamente dentro da Grid.";
            // 
            // btnOpcao
            // 
            this.btnOpcao.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOpcao.Location = new System.Drawing.Point(18, 470);
            this.btnOpcao.Name = "btnOpcao";
            this.btnOpcao.Size = new System.Drawing.Size(93, 23);
            this.btnOpcao.TabIndex = 30;
            this.btnOpcao.Text = "Vincular Opção";
            this.btnOpcao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.btnOpcao, "Selecione as opções que serão vinculadas aos produtos do filtro");
            this.btnOpcao.UseVisualStyleBackColor = true;
            this.btnOpcao.Click += new System.EventHandler(this.btnOpcao_Click);
            // 
            // pnlAdicionais
            // 
            this.pnlAdicionais.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAdicionais.Controls.Add(this.label9);
            this.pnlAdicionais.Controls.Add(this.label8);
            this.pnlAdicionais.Controls.Add(this.cbxTipoOpcao);
            this.pnlAdicionais.Controls.Add(this.btnCancel);
            this.pnlAdicionais.Controls.Add(this.btnSalvar);
            this.pnlAdicionais.Controls.Add(this.btnEditar);
            this.pnlAdicionais.Controls.Add(this.btnAdicionarOpcao);
            this.pnlAdicionais.Controls.Add(this.txtPrecoOpcao);
            this.pnlAdicionais.Controls.Add(this.cbxOpcao);
            this.pnlAdicionais.Controls.Add(this.label7);
            this.pnlAdicionais.Controls.Add(this.AdicionaisGridView);
            this.pnlAdicionais.Location = new System.Drawing.Point(18, 238);
            this.pnlAdicionais.Name = "pnlAdicionais";
            this.pnlAdicionais.Size = new System.Drawing.Size(382, 210);
            this.pnlAdicionais.TabIndex = 32;
            this.pnlAdicionais.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 48;
            this.label9.Text = "Opção";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 47;
            this.label8.Text = "Tipo";
            // 
            // cbxTipoOpcao
            // 
            this.cbxTipoOpcao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoOpcao.FormattingEnabled = true;
            this.cbxTipoOpcao.Location = new System.Drawing.Point(46, 5);
            this.cbxTipoOpcao.Name = "cbxTipoOpcao";
            this.cbxTipoOpcao.Size = new System.Drawing.Size(130, 21);
            this.cbxTipoOpcao.TabIndex = 46;
            this.cbxTipoOpcao.DropDown += new System.EventHandler(this.cbxTipoOpcao_DropDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(309, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSalvar.Location = new System.Drawing.Point(239, 180);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(64, 23);
            this.btnSalvar.TabIndex = 44;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(334, 27);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(41, 26);
            this.btnEditar.TabIndex = 43;
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnAdicionarOpcao
            // 
            this.btnAdicionarOpcao.Image = ((System.Drawing.Image)(resources.GetObject("btnAdicionarOpcao.Image")));
            this.btnAdicionarOpcao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionarOpcao.Location = new System.Drawing.Point(291, 27);
            this.btnAdicionarOpcao.Name = "btnAdicionarOpcao";
            this.btnAdicionarOpcao.Size = new System.Drawing.Size(35, 26);
            this.btnAdicionarOpcao.TabIndex = 42;
            this.btnAdicionarOpcao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdicionarOpcao.UseVisualStyleBackColor = true;
            this.btnAdicionarOpcao.Click += new System.EventHandler(this.btnAdicionarOpcao_Click_2);
            // 
            // txtPrecoOpcao
            // 
            this.txtPrecoOpcao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoOpcao.Location = new System.Drawing.Point(210, 26);
            this.txtPrecoOpcao.Name = "txtPrecoOpcao";
            this.txtPrecoOpcao.Size = new System.Drawing.Size(77, 26);
            this.txtPrecoOpcao.TabIndex = 41;
            // 
            // cbxOpcao
            // 
            this.cbxOpcao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOpcao.FormattingEnabled = true;
            this.cbxOpcao.Location = new System.Drawing.Point(46, 30);
            this.cbxOpcao.Name = "cbxOpcao";
            this.cbxOpcao.Size = new System.Drawing.Size(158, 21);
            this.cbxOpcao.TabIndex = 40;
            this.cbxOpcao.DropDown += new System.EventHandler(this.cbxOpcao_DropDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Adicionais do Produto";
            // 
            // AdicionaisGridView
            // 
            this.AdicionaisGridView.AllowUserToAddRows = false;
            this.AdicionaisGridView.AllowUserToDeleteRows = false;
            this.AdicionaisGridView.AllowUserToOrderColumns = true;
            this.AdicionaisGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AdicionaisGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AdicionaisGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AdicionaisGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodOpcao,
            this.Valor,
            this.Nome,
            this.CodTipo});
            this.AdicionaisGridView.Location = new System.Drawing.Point(3, 71);
            this.AdicionaisGridView.MultiSelect = false;
            this.AdicionaisGridView.Name = "AdicionaisGridView";
            this.AdicionaisGridView.ReadOnly = true;
            this.AdicionaisGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AdicionaisGridView.Size = new System.Drawing.Size(370, 103);
            this.AdicionaisGridView.TabIndex = 38;
            this.AdicionaisGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuAuxiliarOpcao);
            // 
            // CodOpcao
            // 
            this.CodOpcao.HeaderText = "CodOpcao";
            this.CodOpcao.Name = "CodOpcao";
            this.CodOpcao.ReadOnly = true;
            // 
            // Valor
            // 
            this.Valor.HeaderText = "Valor";
            this.Valor.Name = "Valor";
            this.Valor.ReadOnly = true;
            // 
            // Nome
            // 
            this.Nome.HeaderText = "Nome";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            // 
            // CodTipo
            // 
            this.CodTipo.HeaderText = "CodTipo";
            this.CodTipo.Name = "CodTipo";
            this.CodTipo.ReadOnly = true;
            // 
            // pnlInsumos
            // 
            this.pnlInsumos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlInsumos.Controls.Add(this.label11);
            this.pnlInsumos.Controls.Add(this.label10);
            this.pnlInsumos.Controls.Add(this.button1);
            this.pnlInsumos.Controls.Add(this.btnInsumoSalvar);
            this.pnlInsumos.Controls.Add(this.btnInsumoEditar);
            this.pnlInsumos.Controls.Add(this.btnInsumoAdd);
            this.pnlInsumos.Controls.Add(this.txtQtdInsumo);
            this.pnlInsumos.Controls.Add(this.cbxInsumo);
            this.pnlInsumos.Controls.Add(this.label12);
            this.pnlInsumos.Controls.Add(this.InsumoGridView);
            this.pnlInsumos.Location = new System.Drawing.Point(406, 227);
            this.pnlInsumos.Name = "pnlInsumos";
            this.pnlInsumos.Size = new System.Drawing.Size(382, 220);
            this.pnlInsumos.TabIndex = 50;
            this.pnlInsumos.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 48;
            this.label10.Text = "Insumo";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(309, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 23);
            this.button1.TabIndex = 45;
            this.button1.Text = "Cancel";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnInsumoSalvar
            // 
            this.btnInsumoSalvar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnInsumoSalvar.Location = new System.Drawing.Point(239, 190);
            this.btnInsumoSalvar.Name = "btnInsumoSalvar";
            this.btnInsumoSalvar.Size = new System.Drawing.Size(64, 23);
            this.btnInsumoSalvar.TabIndex = 44;
            this.btnInsumoSalvar.Text = "Salvar";
            this.btnInsumoSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInsumoSalvar.UseVisualStyleBackColor = true;
            this.btnInsumoSalvar.Click += new System.EventHandler(this.btnInsumoSalvar_Click);
            // 
            // btnInsumoEditar
            // 
            this.btnInsumoEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnInsumoEditar.Image")));
            this.btnInsumoEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInsumoEditar.Location = new System.Drawing.Point(334, 36);
            this.btnInsumoEditar.Name = "btnInsumoEditar";
            this.btnInsumoEditar.Size = new System.Drawing.Size(41, 26);
            this.btnInsumoEditar.TabIndex = 43;
            this.btnInsumoEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInsumoEditar.UseVisualStyleBackColor = true;
            // 
            // btnInsumoAdd
            // 
            this.btnInsumoAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnInsumoAdd.Image")));
            this.btnInsumoAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInsumoAdd.Location = new System.Drawing.Point(291, 36);
            this.btnInsumoAdd.Name = "btnInsumoAdd";
            this.btnInsumoAdd.Size = new System.Drawing.Size(35, 26);
            this.btnInsumoAdd.TabIndex = 42;
            this.btnInsumoAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInsumoAdd.UseVisualStyleBackColor = true;
            this.btnInsumoAdd.Click += new System.EventHandler(this.btnInsumoAdd_Click);
            // 
            // txtQtdInsumo
            // 
            this.txtQtdInsumo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQtdInsumo.Location = new System.Drawing.Point(210, 35);
            this.txtQtdInsumo.Name = "txtQtdInsumo";
            this.txtQtdInsumo.Size = new System.Drawing.Size(77, 26);
            this.txtQtdInsumo.TabIndex = 41;
            // 
            // cbxInsumo
            // 
            this.cbxInsumo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxInsumo.FormattingEnabled = true;
            this.cbxInsumo.Location = new System.Drawing.Point(46, 39);
            this.cbxInsumo.Name = "cbxInsumo";
            this.cbxInsumo.Size = new System.Drawing.Size(158, 21);
            this.cbxInsumo.TabIndex = 40;
            this.cbxInsumo.DropDown += new System.EventHandler(this.cbxInsumo_DropDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 13);
            this.label12.TabIndex = 39;
            this.label12.Text = "Adicionais do Produto";
            // 
            // InsumoGridView
            // 
            this.InsumoGridView.AllowUserToAddRows = false;
            this.InsumoGridView.AllowUserToDeleteRows = false;
            this.InsumoGridView.AllowUserToOrderColumns = true;
            this.InsumoGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.InsumoGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.InsumoGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InsumoGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodInsumo,
            this.Insumo,
            this.Quantidade});
            this.InsumoGridView.Location = new System.Drawing.Point(3, 80);
            this.InsumoGridView.MultiSelect = false;
            this.InsumoGridView.Name = "InsumoGridView";
            this.InsumoGridView.ReadOnly = true;
            this.InsumoGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InsumoGridView.Size = new System.Drawing.Size(370, 103);
            this.InsumoGridView.TabIndex = 38;
            this.InsumoGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.InsumoGridView_MouseClick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(115, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(197, 24);
            this.label11.TabIndex = 49;
            this.label11.Text = "Insumos do Produto";
            // 
            // CodInsumo
            // 
            this.CodInsumo.HeaderText = "Column1CodInsumo";
            this.CodInsumo.Name = "CodInsumo";
            this.CodInsumo.ReadOnly = true;
            this.CodInsumo.Visible = false;
            // 
            // Insumo
            // 
            this.Insumo.HeaderText = "Insumo";
            this.Insumo.Name = "Insumo";
            this.Insumo.ReadOnly = true;
            // 
            // Quantidade
            // 
            this.Quantidade.HeaderText = "Quantidade";
            this.Quantidade.Name = "Quantidade";
            this.Quantidade.ReadOnly = true;
            // 
            // btnInsumoVincular
            // 
            this.btnInsumoVincular.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnInsumoVincular.Location = new System.Drawing.Point(701, 467);
            this.btnInsumoVincular.Name = "btnInsumoVincular";
            this.btnInsumoVincular.Size = new System.Drawing.Size(93, 23);
            this.btnInsumoVincular.TabIndex = 51;
            this.btnInsumoVincular.Text = "Vincular Insumo";
            this.btnInsumoVincular.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.btnInsumoVincular, "Selecione as opções que serão vinculadas aos produtos do filtro");
            this.btnInsumoVincular.UseVisualStyleBackColor = true;
            this.btnInsumoVincular.Click += new System.EventHandler(this.btnInsumoVincular_Click);
            // 
            // frmAlteracaoProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 544);
            this.Controls.Add(this.btnInsumoVincular);
            this.Controls.Add(this.pnlInsumos);
            this.Controls.Add(this.pnlAdicionais);
            this.Controls.Add(this.btnOpcao);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnExecutar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpFiltros);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAlteracaoProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[xDelivery] Alteração Coletiva";
            this.Load += new System.EventHandler(this.frmAlteracaoProduto_Load);
            this.grpFiltros.ResumeLayout(false);
            this.grpFiltros.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewProdutos)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpDesconto.ResumeLayout(false);
            this.grpDesconto.PerformLayout();
            this.grpPrecos.ResumeLayout(false);
            this.grpPrecos.PerformLayout();
            this.pnlAdicionais.ResumeLayout(false);
            this.pnlAdicionais.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AdicionaisGridView)).EndInit();
            this.pnlInsumos.ResumeLayout(false);
            this.pnlInsumos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InsumoGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFiltros;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExecutar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridView GridViewProdutos;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxGrupo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox grpPrecos;
        private System.Windows.Forms.TextBox txtnewValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbValor;
        private System.Windows.Forms.RadioButton rbPorcentagem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextGrande;
        private System.Windows.Forms.GroupBox grpDesconto;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox chkDomingo;
        private System.Windows.Forms.TextBox txtPorcentagem;
        private System.Windows.Forms.CheckBox ChkSexta;
        private System.Windows.Forms.CheckBox chkQuinta;
        private System.Windows.Forms.CheckBox ChkSabado;
        private System.Windows.Forms.CheckBox ChkQuarta;
        private System.Windows.Forms.CheckBox chkTerca;
        private System.Windows.Forms.CheckBox chkSegunda;
        private System.Windows.Forms.CheckBox chkAtivaDesconto;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.Button btnOpcao;
        private System.Windows.Forms.CheckBox chkTodos;
        private System.Windows.Forms.CheckBox chkAlteraPreco;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pnlAdicionais;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbxTipoOpcao;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnAdicionarOpcao;
        private System.Windows.Forms.TextBox txtPrecoOpcao;
        private System.Windows.Forms.ComboBox cbxOpcao;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView AdicionaisGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodOpcao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodTipo;
        private System.Windows.Forms.Panel pnlInsumos;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnInsumoSalvar;
        private System.Windows.Forms.Button btnInsumoEditar;
        private System.Windows.Forms.Button btnInsumoAdd;
        private System.Windows.Forms.TextBox txtQtdInsumo;
        private System.Windows.Forms.ComboBox cbxInsumo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView InsumoGridView;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodInsumo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Insumo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantidade;
        private System.Windows.Forms.Button btnInsumoVincular;
    }
}