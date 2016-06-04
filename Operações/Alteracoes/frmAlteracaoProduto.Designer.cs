namespace DexComanda.Operações.Alteracoes
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
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label5;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAlteracaoProduto));
            this.grpFiltros = new System.Windows.Forms.GroupBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxGrupo = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlAdicionais = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAdicionarOpcao = new System.Windows.Forms.Button();
            this.txtPrecoOpcao = new System.Windows.Forms.TextBox();
            this.cbxOpcao = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.AdicionaisGridView = new System.Windows.Forms.DataGridView();
            this.GridViewProdutos = new System.Windows.Forms.DataGridView();
            this.btnExecutar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.richTextGrande = new System.Windows.Forms.RichTextBox();
            this.rbValor = new System.Windows.Forms.RadioButton();
            this.rbPorcentagem = new System.Windows.Forms.RadioButton();
            this.txtnewValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOpcao = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            this.grpFiltros.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlAdicionais.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AdicionaisGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewProdutos)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.grpDesconto.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(231, 35);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(64, 26);
            this.btnFiltrar.TabIndex = 26;
            this.btnFiltrar.Text = "Filtros";
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
            this.groupBox1.Controls.Add(this.pnlAdicionais);
            this.groupBox1.Controls.Add(this.GridViewProdutos);
            this.groupBox1.Location = new System.Drawing.Point(4, 275);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(790, 189);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resultado - Filtro";
            // 
            // pnlAdicionais
            // 
            this.pnlAdicionais.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAdicionais.Controls.Add(this.btnCancel);
            this.pnlAdicionais.Controls.Add(this.btnSalvar);
            this.pnlAdicionais.Controls.Add(this.btnEditar);
            this.pnlAdicionais.Controls.Add(this.btnAdicionarOpcao);
            this.pnlAdicionais.Controls.Add(this.txtPrecoOpcao);
            this.pnlAdicionais.Controls.Add(this.cbxOpcao);
            this.pnlAdicionais.Controls.Add(this.label7);
            this.pnlAdicionais.Controls.Add(this.AdicionaisGridView);
            this.pnlAdicionais.Location = new System.Drawing.Point(180, 0);
            this.pnlAdicionais.Name = "pnlAdicionais";
            this.pnlAdicionais.Size = new System.Drawing.Size(346, 183);
            this.pnlAdicionais.TabIndex = 3;
            this.pnlAdicionais.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(270, 153);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSalvar.Location = new System.Drawing.Point(200, 153);
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
            this.btnEditar.Location = new System.Drawing.Point(270, 4);
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
            this.btnAdicionarOpcao.Location = new System.Drawing.Point(229, 4);
            this.btnAdicionarOpcao.Name = "btnAdicionarOpcao";
            this.btnAdicionarOpcao.Size = new System.Drawing.Size(35, 26);
            this.btnAdicionarOpcao.TabIndex = 42;
            this.btnAdicionarOpcao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdicionarOpcao.UseVisualStyleBackColor = true;
            this.btnAdicionarOpcao.Click += new System.EventHandler(this.btnAdicionarOpcao_Click);
            // 
            // txtPrecoOpcao
            // 
            this.txtPrecoOpcao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoOpcao.Location = new System.Drawing.Point(146, 3);
            this.txtPrecoOpcao.Name = "txtPrecoOpcao";
            this.txtPrecoOpcao.Size = new System.Drawing.Size(77, 26);
            this.txtPrecoOpcao.TabIndex = 41;
            // 
            // cbxOpcao
            // 
            this.cbxOpcao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOpcao.FormattingEnabled = true;
            this.cbxOpcao.Location = new System.Drawing.Point(3, 6);
            this.cbxOpcao.Name = "cbxOpcao";
            this.cbxOpcao.Size = new System.Drawing.Size(137, 21);
            this.cbxOpcao.TabIndex = 40;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 30);
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
            this.AdicionaisGridView.Location = new System.Drawing.Point(3, 46);
            this.AdicionaisGridView.MultiSelect = false;
            this.AdicionaisGridView.Name = "AdicionaisGridView";
            this.AdicionaisGridView.ReadOnly = true;
            this.AdicionaisGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AdicionaisGridView.Size = new System.Drawing.Size(336, 103);
            this.AdicionaisGridView.TabIndex = 38;
            this.AdicionaisGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Excluir);
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
            this.GridViewProdutos.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuAuxiliar);
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
            this.groupBox2.Controls.Add(this.chkAtivaDesconto);
            this.groupBox2.Controls.Add(this.grpDesconto);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(7, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(788, 200);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Alterações";
            // 
            // chkAtivaDesconto
            // 
            this.chkAtivaDesconto.AutoSize = true;
            this.chkAtivaDesconto.Location = new System.Drawing.Point(389, 11);
            this.chkAtivaDesconto.Name = "chkAtivaDesconto";
            this.chkAtivaDesconto.Size = new System.Drawing.Size(105, 17);
            this.chkAtivaDesconto.TabIndex = 35;
            this.chkAtivaDesconto.Text = "Ativa Desconto?";
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
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.richTextGrande);
            this.groupBox3.Controls.Add(this.rbValor);
            this.groupBox3.Controls.Add(this.rbPorcentagem);
            this.groupBox3.Controls.Add(this.txtnewValue);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(6, 38);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(376, 153);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Preço Normal";
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
            this.btnOpcao.Location = new System.Drawing.Point(694, 467);
            this.btnOpcao.Name = "btnOpcao";
            this.btnOpcao.Size = new System.Drawing.Size(93, 23);
            this.btnOpcao.TabIndex = 30;
            this.btnOpcao.Text = "Vincular Opção";
            this.btnOpcao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpcao.UseVisualStyleBackColor = true;
            this.btnOpcao.Click += new System.EventHandler(this.btnOpcao_Click);
            // 
            // frmAlteracaoProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 544);
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
            this.pnlAdicionais.ResumeLayout(false);
            this.pnlAdicionais.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AdicionaisGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewProdutos)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpDesconto.ResumeLayout(false);
            this.grpDesconto.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox3;
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
        private System.Windows.Forms.Panel pnlAdicionais;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView AdicionaisGridView;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnAdicionarOpcao;
        private System.Windows.Forms.TextBox txtPrecoOpcao;
        private System.Windows.Forms.ComboBox cbxOpcao;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSalvar;
    }
}