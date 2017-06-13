namespace DexComanda
{
    partial class frmCadastrarProduto
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
            System.Windows.Forms.Label grupoProdutoLabel;
            System.Windows.Forms.Label precoProdutoLabel;
            System.Windows.Forms.Label descricaoProdutoLabel;
            System.Windows.Forms.Label nomeProdutoLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label17;
            System.Windows.Forms.Label label19;
            System.Windows.Forms.Label label21;
            System.Windows.Forms.Label label22;
            System.Windows.Forms.Label label25;
            System.Windows.Forms.Label label26;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastrarProduto));
            this.dBExpertDataSet = new DexComanda.DBExpertDataSet();
            this.produtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.produtoTableAdapter = new DexComanda.DBExpertDataSetTableAdapters.ProdutoTableAdapter();
            this.tableAdapterManager = new DexComanda.DBExpertDataSetTableAdapters.TableAdapterManager();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnDoProduto = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.btnImg = new System.Windows.Forms.Button();
            this.txtcaminhoImage = new System.Windows.Forms.TextBox();
            this.imgProduto = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.cbxTipoOpcao = new System.Windows.Forms.ComboBox();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAdicionarOpcao = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrecoOpcao = new System.Windows.Forms.TextBox();
            this.cbxOpcao = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AdicionaisGridView = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grpEstoque = new System.Windows.Forms.GroupBox();
            this.txtEstMinimo = new System.Windows.Forms.TextBox();
            this.chkControleEstoque = new System.Windows.Forms.CheckBox();
            this.txtEstoqueAtual = new System.Windows.Forms.TextBox();
            this.txtPrecoSugerido = new System.Windows.Forms.TextBox();
            this.txtMarkup = new System.Windows.Forms.TextBox();
            this.txtPrecoCusto = new System.Windows.Forms.TextBox();
            this.txtCodInterno = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtMaxAdicionais = new System.Windows.Forms.TextBox();
            this.nomeProdutoTextBox = new System.Windows.Forms.TextBox();
            this.descricaoProdutoTextBox = new System.Windows.Forms.TextBox();
            this.precoProdutoTextBox = new System.Windows.Forms.TextBox();
            this.chkOnline = new System.Windows.Forms.CheckBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.grpDesconto = new System.Windows.Forms.GroupBox();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.chkDomingo = new System.Windows.Forms.CheckBox();
            this.txtPrecoDesconto = new System.Windows.Forms.TextBox();
            this.ChkSexta = new System.Windows.Forms.CheckBox();
            this.chkQuinta = new System.Windows.Forms.CheckBox();
            this.ChkSabado = new System.Windows.Forms.CheckBox();
            this.ChkQuarta = new System.Windows.Forms.CheckBox();
            this.chkTerca = new System.Windows.Forms.CheckBox();
            this.chkSegunda = new System.Windows.Forms.CheckBox();
            this.cbxGrupoProduto = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label27 = new System.Windows.Forms.Label();
            this.txtPalavrasChave = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtPontosTroca = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtPontosFidelidade = new System.Windows.Forms.TextBox();
            this.grpPrecosDia = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPrecoDomingo = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPrecoSabado = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPrecoSexta = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPrecoQuinta = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPrecoQuarta = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPrecoTerca = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPrecoSegunda = new System.Windows.Forms.TextBox();
            this.chkDom = new System.Windows.Forms.CheckBox();
            this.chkSex = new System.Windows.Forms.CheckBox();
            this.chkQui = new System.Windows.Forms.CheckBox();
            this.chkSab = new System.Windows.Forms.CheckBox();
            this.chkQua = new System.Windows.Forms.CheckBox();
            this.chkTer = new System.Windows.Forms.CheckBox();
            this.chkSeg = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.horaInicio = new System.Windows.Forms.DateTimePicker();
            this.HoraFim = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.tbInsumo = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.txtQtd = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridInsumo = new System.Windows.Forms.DataGridView();
            this.btnEditarIns = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.cbxInsumo = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            grupoProdutoLabel = new System.Windows.Forms.Label();
            precoProdutoLabel = new System.Windows.Forms.Label();
            descricaoProdutoLabel = new System.Windows.Forms.Label();
            nomeProdutoLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label19 = new System.Windows.Forms.Label();
            label21 = new System.Windows.Forms.Label();
            label22 = new System.Windows.Forms.Label();
            label25 = new System.Windows.Forms.Label();
            label26 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dBExpertDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.produtoBindingSource)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgProduto)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AdicionaisGridView)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.grpEstoque.SuspendLayout();
            this.grpDesconto.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpPrecosDia.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tbInsumo.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInsumo)).BeginInit();
            this.SuspendLayout();
            // 
            // grupoProdutoLabel
            // 
            grupoProdutoLabel.AutoSize = true;
            grupoProdutoLabel.Location = new System.Drawing.Point(8, 123);
            grupoProdutoLabel.Name = "grupoProdutoLabel";
            grupoProdutoLabel.Size = new System.Drawing.Size(79, 13);
            grupoProdutoLabel.TabIndex = 31;
            grupoProdutoLabel.Text = "Grupo Produto:";
            // 
            // precoProdutoLabel
            // 
            precoProdutoLabel.AutoSize = true;
            precoProdutoLabel.Location = new System.Drawing.Point(254, 69);
            precoProdutoLabel.Name = "precoProdutoLabel";
            precoProdutoLabel.Size = new System.Drawing.Size(72, 13);
            precoProdutoLabel.TabIndex = 30;
            precoProdutoLabel.Text = "Preço Venda:";
            // 
            // descricaoProdutoLabel
            // 
            descricaoProdutoLabel.AutoSize = true;
            descricaoProdutoLabel.Location = new System.Drawing.Point(8, 170);
            descricaoProdutoLabel.Name = "descricaoProdutoLabel";
            descricaoProdutoLabel.Size = new System.Drawing.Size(98, 13);
            descricaoProdutoLabel.TabIndex = 29;
            descricaoProdutoLabel.Text = "Descricao Produto:";
            // 
            // nomeProdutoLabel
            // 
            nomeProdutoLabel.AutoSize = true;
            nomeProdutoLabel.Location = new System.Drawing.Point(8, 3);
            nomeProdutoLabel.Name = "nomeProdutoLabel";
            nomeProdutoLabel.Size = new System.Drawing.Size(78, 13);
            nomeProdutoLabel.TabIndex = 25;
            nomeProdutoLabel.Text = "Nome Produto:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(152, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(74, 13);
            label1.TabIndex = 24;
            label1.Text = " C/ Desconto:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(331, 123);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(81, 13);
            label5.TabIndex = 37;
            label5.Text = "Max. Adicionais";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(6, 76);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(97, 13);
            label6.TabIndex = 32;
            label6.Text = "Validade Desconto";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(343, 66);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(65, 13);
            label17.TabIndex = 40;
            label17.Text = "Cod. Interno";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new System.Drawing.Point(8, 69);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(65, 13);
            label19.TabIndex = 42;
            label19.Text = "Preço Custo";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new System.Drawing.Point(81, 69);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(54, 13);
            label21.TabIndex = 44;
            label21.Text = "Markup %";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new System.Drawing.Point(166, 66);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(65, 13);
            label22.TabIndex = 46;
            label22.Text = "Pr. Sugerido";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new System.Drawing.Point(6, 48);
            label25.Name = "label25";
            label25.Size = new System.Drawing.Size(55, 13);
            label25.TabIndex = 32;
            label25.Text = "Est. Atual:";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new System.Drawing.Point(97, 48);
            label26.Name = "label26";
            label26.Size = new System.Drawing.Size(63, 13);
            label26.TabIndex = 35;
            label26.Text = "Est. Mínimo";
            // 
            // dBExpertDataSet
            // 
            this.dBExpertDataSet.DataSetName = "DBExpertDataSet";
            this.dBExpertDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // produtoBindingSource
            // 
            this.produtoBindingSource.DataMember = "Produto";
            this.produtoBindingSource.DataSource = this.dBExpertDataSet;
            // 
            // produtoTableAdapter
            // 
            this.produtoTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.base_cepTableAdapter = null;
            this.tableAdapterManager.GrupoTableAdapter = null;
            this.tableAdapterManager.ItemsPedidoTableAdapter = null;
            this.tableAdapterManager.PedidoTableAdapter = null;
            this.tableAdapterManager.PessoaTableAdapter = null;
            this.tableAdapterManager.ProdutoTableAdapter = this.produtoTableAdapter;
            this.tableAdapterManager.UpdateOrder = DexComanda.DBExpertDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(522, 304);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(150, 43);
            this.btnSair.TabIndex = 7;
            this.btnSair.Text = "Sair [ESC]";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click_1);
            // 
            // btnDoProduto
            // 
            this.btnDoProduto.Location = new System.Drawing.Point(362, 304);
            this.btnDoProduto.Name = "btnDoProduto";
            this.btnDoProduto.Size = new System.Drawing.Size(150, 43);
            this.btnDoProduto.TabIndex = 6;
            this.btnDoProduto.Text = "Cadastrar [F12]";
            this.btnDoProduto.UseVisualStyleBackColor = true;
            this.btnDoProduto.Click += new System.EventHandler(this.AdicionarProduto);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.btnImg);
            this.tabPage3.Controls.Add(this.txtcaminhoImage);
            this.tabPage3.Controls.Add(this.imgProduto);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(673, 274);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "4 -Imagem";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(618, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Limpar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnImg
            // 
            this.btnImg.Location = new System.Drawing.Point(557, 14);
            this.btnImg.Name = "btnImg";
            this.btnImg.Size = new System.Drawing.Size(55, 23);
            this.btnImg.TabIndex = 13;
            this.btnImg.Text = "Buscar";
            this.btnImg.UseVisualStyleBackColor = true;
            this.btnImg.Click += new System.EventHandler(this.SelecionarImagem);
            // 
            // txtcaminhoImage
            // 
            this.txtcaminhoImage.Location = new System.Drawing.Point(8, 16);
            this.txtcaminhoImage.Name = "txtcaminhoImage";
            this.txtcaminhoImage.Size = new System.Drawing.Size(543, 20);
            this.txtcaminhoImage.TabIndex = 14;
            this.txtcaminhoImage.TextChanged += new System.EventHandler(this.txtcaminhoImage_TextChanged);
            // 
            // imgProduto
            // 
            this.imgProduto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imgProduto.Location = new System.Drawing.Point(8, 42);
            this.imgProduto.Name = "imgProduto";
            this.imgProduto.Size = new System.Drawing.Size(659, 226);
            this.imgProduto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgProduto.TabIndex = 0;
            this.imgProduto.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.cbxTipoOpcao);
            this.tabPage2.Controls.Add(this.btnEditar);
            this.tabPage2.Controls.Add(this.btnAdicionarOpcao);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtPrecoOpcao);
            this.tabPage2.Controls.Add(this.cbxOpcao);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(673, 274);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "2 -Adicionais/Opcionais";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 27);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(28, 13);
            this.label16.TabIndex = 32;
            this.label16.Text = "Tipo";
            // 
            // cbxTipoOpcao
            // 
            this.cbxTipoOpcao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoOpcao.FormattingEnabled = true;
            this.cbxTipoOpcao.Location = new System.Drawing.Point(52, 21);
            this.cbxTipoOpcao.Name = "cbxTipoOpcao";
            this.cbxTipoOpcao.Size = new System.Drawing.Size(107, 21);
            this.cbxTipoOpcao.TabIndex = 31;
            this.cbxTipoOpcao.DropDown += new System.EventHandler(this.cbxTipoOpcao_DropDown);
            // 
            // btnEditar
            // 
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(516, 43);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(63, 26);
            this.btnEditar.TabIndex = 30;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnAdicionarOpcao
            // 
            this.btnAdicionarOpcao.Image = ((System.Drawing.Image)(resources.GetObject("btnAdicionarOpcao.Image")));
            this.btnAdicionarOpcao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionarOpcao.Location = new System.Drawing.Point(425, 43);
            this.btnAdicionarOpcao.Name = "btnAdicionarOpcao";
            this.btnAdicionarOpcao.Size = new System.Drawing.Size(75, 26);
            this.btnAdicionarOpcao.TabIndex = 29;
            this.btnAdicionarOpcao.Text = "Adicionar";
            this.btnAdicionarOpcao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdicionarOpcao.UseVisualStyleBackColor = true;
            this.btnAdicionarOpcao.Click += new System.EventHandler(this.AdicionarOpcao);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(324, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Preço";
            // 
            // txtPrecoOpcao
            // 
            this.txtPrecoOpcao.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPrecoOpcao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoOpcao.Location = new System.Drawing.Point(327, 43);
            this.txtPrecoOpcao.Name = "txtPrecoOpcao";
            this.txtPrecoOpcao.Size = new System.Drawing.Size(92, 26);
            this.txtPrecoOpcao.TabIndex = 27;
            // 
            // cbxOpcao
            // 
            this.cbxOpcao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOpcao.FormattingEnabled = true;
            this.cbxOpcao.Location = new System.Drawing.Point(52, 48);
            this.cbxOpcao.Name = "cbxOpcao";
            this.cbxOpcao.Size = new System.Drawing.Size(246, 21);
            this.cbxOpcao.TabIndex = 6;
            this.cbxOpcao.Click += new System.EventHandler(this.ListaOpcao);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Adicionais do Produto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nome ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AdicionaisGridView);
            this.panel1.Location = new System.Drawing.Point(3, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(647, 178);
            this.panel1.TabIndex = 0;
            // 
            // AdicionaisGridView
            // 
            this.AdicionaisGridView.AllowUserToAddRows = false;
            this.AdicionaisGridView.AllowUserToDeleteRows = false;
            this.AdicionaisGridView.AllowUserToOrderColumns = true;
            this.AdicionaisGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AdicionaisGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AdicionaisGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AdicionaisGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AdicionaisGridView.Location = new System.Drawing.Point(0, 0);
            this.AdicionaisGridView.MultiSelect = false;
            this.AdicionaisGridView.Name = "AdicionaisGridView";
            this.AdicionaisGridView.ReadOnly = true;
            this.AdicionaisGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AdicionaisGridView.Size = new System.Drawing.Size(647, 178);
            this.AdicionaisGridView.TabIndex = 2;
            this.AdicionaisGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AdicionaisGridView_CellClick);
            this.AdicionaisGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AdicionaisGridView_CellContentClick);
            this.AdicionaisGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AdicionaisGridView_MouseClick);
            this.AdicionaisGridView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.EditarLinha);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grpEstoque);
            this.tabPage1.Controls.Add(this.txtPrecoSugerido);
            this.tabPage1.Controls.Add(label22);
            this.tabPage1.Controls.Add(this.txtMarkup);
            this.tabPage1.Controls.Add(label21);
            this.tabPage1.Controls.Add(this.txtPrecoCusto);
            this.tabPage1.Controls.Add(label19);
            this.tabPage1.Controls.Add(this.txtCodInterno);
            this.tabPage1.Controls.Add(label17);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(label5);
            this.tabPage1.Controls.Add(this.txtMaxAdicionais);
            this.tabPage1.Controls.Add(this.nomeProdutoTextBox);
            this.tabPage1.Controls.Add(this.descricaoProdutoTextBox);
            this.tabPage1.Controls.Add(this.precoProdutoTextBox);
            this.tabPage1.Controls.Add(this.chkOnline);
            this.tabPage1.Controls.Add(this.chkAtivo);
            this.tabPage1.Controls.Add(this.grpDesconto);
            this.tabPage1.Controls.Add(this.cbxGrupoProduto);
            this.tabPage1.Controls.Add(nomeProdutoLabel);
            this.tabPage1.Controls.Add(descricaoProdutoLabel);
            this.tabPage1.Controls.Add(precoProdutoLabel);
            this.tabPage1.Controls.Add(grupoProdutoLabel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(673, 274);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "1 -Produto";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grpEstoque
            // 
            this.grpEstoque.Controls.Add(this.txtEstMinimo);
            this.grpEstoque.Controls.Add(label26);
            this.grpEstoque.Controls.Add(this.chkControleEstoque);
            this.grpEstoque.Controls.Add(this.txtEstoqueAtual);
            this.grpEstoque.Controls.Add(label25);
            this.grpEstoque.Location = new System.Drawing.Point(420, 139);
            this.grpEstoque.Name = "grpEstoque";
            this.grpEstoque.Size = new System.Drawing.Size(232, 123);
            this.grpEstoque.TabIndex = 33;
            this.grpEstoque.TabStop = false;
            this.grpEstoque.Text = "Controle Estoque";
            // 
            // txtEstMinimo
            // 
            this.txtEstMinimo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtEstMinimo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstMinimo.Location = new System.Drawing.Point(100, 65);
            this.txtEstMinimo.Name = "txtEstMinimo";
            this.txtEstMinimo.Size = new System.Drawing.Size(69, 26);
            this.txtEstMinimo.TabIndex = 34;
            this.txtEstMinimo.Text = "0";
            this.toolTip1.SetToolTip(this.txtEstMinimo, "Estoque minimo do produto , quando o valor do estoque atingir esse numero o siste" +
        "ma avisará no pedido");
            this.txtEstMinimo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEstMinimo_KeyPress);
            // 
            // chkControleEstoque
            // 
            this.chkControleEstoque.AutoSize = true;
            this.chkControleEstoque.Location = new System.Drawing.Point(9, 21);
            this.chkControleEstoque.Name = "chkControleEstoque";
            this.chkControleEstoque.Size = new System.Drawing.Size(106, 17);
            this.chkControleEstoque.TabIndex = 33;
            this.chkControleEstoque.Text = "Controla estoque";
            this.toolTip1.SetToolTip(this.chkControleEstoque, "Marque essa opção caso deseje controlar estoque esse produto ");
            this.chkControleEstoque.UseVisualStyleBackColor = true;
            // 
            // txtEstoqueAtual
            // 
            this.txtEstoqueAtual.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtEstoqueAtual.Enabled = false;
            this.txtEstoqueAtual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstoqueAtual.Location = new System.Drawing.Point(9, 65);
            this.txtEstoqueAtual.Name = "txtEstoqueAtual";
            this.txtEstoqueAtual.Size = new System.Drawing.Size(69, 26);
            this.txtEstoqueAtual.TabIndex = 31;
            this.toolTip1.SetToolTip(this.txtEstoqueAtual, "Posição atual do estoque do produto");
            // 
            // txtPrecoSugerido
            // 
            this.txtPrecoSugerido.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPrecoSugerido.Enabled = false;
            this.txtPrecoSugerido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoSugerido.Location = new System.Drawing.Point(162, 86);
            this.txtPrecoSugerido.Name = "txtPrecoSugerido";
            this.txtPrecoSugerido.Size = new System.Drawing.Size(69, 26);
            this.txtPrecoSugerido.TabIndex = 45;
            this.toolTip1.SetToolTip(this.txtPrecoSugerido, "Preço de venda sugerido pelo sistema");
            // 
            // txtMarkup
            // 
            this.txtMarkup.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtMarkup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarkup.Location = new System.Drawing.Point(84, 86);
            this.txtMarkup.Name = "txtMarkup";
            this.txtMarkup.Size = new System.Drawing.Size(56, 26);
            this.txtMarkup.TabIndex = 43;
            this.toolTip1.SetToolTip(this.txtMarkup, "Informe a margem de lucro desejada no produto");
            this.txtMarkup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMarkup_KeyPress);
            this.txtMarkup.Leave += new System.EventHandler(this.txtMarkup_Leave);
            // 
            // txtPrecoCusto
            // 
            this.txtPrecoCusto.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPrecoCusto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoCusto.Location = new System.Drawing.Point(11, 86);
            this.txtPrecoCusto.Name = "txtPrecoCusto";
            this.txtPrecoCusto.Size = new System.Drawing.Size(54, 26);
            this.txtPrecoCusto.TabIndex = 41;
            this.toolTip1.SetToolTip(this.txtPrecoCusto, "Preço de custo calculado pelo sistema");
            this.txtPrecoCusto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecoCusto_KeyPress);
            this.txtPrecoCusto.Leave += new System.EventHandler(this.txtPrecoCusto_Leave);
            // 
            // txtCodInterno
            // 
            this.txtCodInterno.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtCodInterno.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodInterno.Location = new System.Drawing.Point(346, 86);
            this.txtCodInterno.Name = "txtCodInterno";
            this.txtCodInterno.Size = new System.Drawing.Size(66, 26);
            this.txtCodInterno.TabIndex = 3;
            this.toolTip1.SetToolTip(this.txtCodInterno, "Crie um Código Personalizado para esse produto");
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(237, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 26);
            this.button2.TabIndex = 38;
            this.button2.Text = "Novo Grupo";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtMaxAdicionais
            // 
            this.txtMaxAdicionais.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtMaxAdicionais.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxAdicionais.Location = new System.Drawing.Point(329, 141);
            this.txtMaxAdicionais.Name = "txtMaxAdicionais";
            this.txtMaxAdicionais.Size = new System.Drawing.Size(83, 26);
            this.txtMaxAdicionais.TabIndex = 1;
            this.txtMaxAdicionais.TextChanged += new System.EventHandler(this.txtMaxAdicionais_TextChanged);
            // 
            // nomeProdutoTextBox
            // 
            this.nomeProdutoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "NomeProduto", true));
            this.nomeProdutoTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nomeProdutoTextBox.Location = new System.Drawing.Point(11, 25);
            this.nomeProdutoTextBox.Name = "nomeProdutoTextBox";
            this.nomeProdutoTextBox.Size = new System.Drawing.Size(401, 26);
            this.nomeProdutoTextBox.TabIndex = 0;
            // 
            // descricaoProdutoTextBox
            // 
            this.descricaoProdutoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "DescricaoProduto", true));
            this.descricaoProdutoTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descricaoProdutoTextBox.Location = new System.Drawing.Point(6, 186);
            this.descricaoProdutoTextBox.Multiline = true;
            this.descricaoProdutoTextBox.Name = "descricaoProdutoTextBox";
            this.descricaoProdutoTextBox.Size = new System.Drawing.Size(406, 82);
            this.descricaoProdutoTextBox.TabIndex = 5;
            // 
            // precoProdutoTextBox
            // 
            this.precoProdutoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.precoProdutoTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.precoProdutoTextBox.Location = new System.Drawing.Point(257, 86);
            this.precoProdutoTextBox.Name = "precoProdutoTextBox";
            this.precoProdutoTextBox.Size = new System.Drawing.Size(69, 26);
            this.precoProdutoTextBox.TabIndex = 3;
            this.precoProdutoTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.precoProdutoTextBox_KeyPress);
            // 
            // chkOnline
            // 
            this.chkOnline.AutoSize = true;
            this.chkOnline.Location = new System.Drawing.Point(302, 6);
            this.chkOnline.Name = "chkOnline";
            this.chkOnline.Size = new System.Drawing.Size(96, 17);
            this.chkOnline.TabIndex = 2;
            this.chkOnline.Text = "Venda Online?";
            this.chkOnline.UseVisualStyleBackColor = true;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Checked = true;
            this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivo.Location = new System.Drawing.Point(237, 6);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(59, 17);
            this.chkAtivo.TabIndex = 1;
            this.chkAtivo.Text = "Ativo ?";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // grpDesconto
            // 
            this.grpDesconto.Controls.Add(label6);
            this.grpDesconto.Controls.Add(this.dtFim);
            this.grpDesconto.Controls.Add(this.dtInicio);
            this.grpDesconto.Controls.Add(this.chkDomingo);
            this.grpDesconto.Controls.Add(label1);
            this.grpDesconto.Controls.Add(this.txtPrecoDesconto);
            this.grpDesconto.Controls.Add(this.ChkSexta);
            this.grpDesconto.Controls.Add(this.chkQuinta);
            this.grpDesconto.Controls.Add(this.ChkSabado);
            this.grpDesconto.Controls.Add(this.ChkQuarta);
            this.grpDesconto.Controls.Add(this.chkTerca);
            this.grpDesconto.Controls.Add(this.chkSegunda);
            this.grpDesconto.Location = new System.Drawing.Point(418, 6);
            this.grpDesconto.Name = "grpDesconto";
            this.grpDesconto.Size = new System.Drawing.Size(232, 123);
            this.grpDesconto.TabIndex = 32;
            this.grpDesconto.TabStop = false;
            this.grpDesconto.Text = "Dias Desconto";
            // 
            // dtFim
            // 
            this.dtFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFim.Location = new System.Drawing.Point(119, 97);
            this.dtFim.Name = "dtFim";
            this.dtFim.Size = new System.Drawing.Size(81, 20);
            this.dtFim.TabIndex = 27;
            // 
            // dtInicio
            // 
            this.dtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtInicio.Location = new System.Drawing.Point(23, 97);
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Size = new System.Drawing.Size(81, 20);
            this.dtInicio.TabIndex = 26;
            // 
            // chkDomingo
            // 
            this.chkDomingo.AutoSize = true;
            this.chkDomingo.Location = new System.Drawing.Point(106, 14);
            this.chkDomingo.Name = "chkDomingo";
            this.chkDomingo.Size = new System.Drawing.Size(51, 17);
            this.chkDomingo.TabIndex = 25;
            this.chkDomingo.Text = "Dom.";
            this.chkDomingo.UseVisualStyleBackColor = true;
            // 
            // txtPrecoDesconto
            // 
            this.txtPrecoDesconto.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPrecoDesconto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoDesconto.Location = new System.Drawing.Point(154, 38);
            this.txtPrecoDesconto.Name = "txtPrecoDesconto";
            this.txtPrecoDesconto.Size = new System.Drawing.Size(72, 26);
            this.txtPrecoDesconto.TabIndex = 23;
            this.toolTip1.SetToolTip(this.txtPrecoDesconto, "Informe o preço do produto com DESCONTO");
            this.txtPrecoDesconto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecoDesconto_KeyPress);
            // 
            // ChkSexta
            // 
            this.ChkSexta.AutoSize = true;
            this.ChkSexta.Location = new System.Drawing.Point(56, 37);
            this.ChkSexta.Name = "ChkSexta";
            this.ChkSexta.Size = new System.Drawing.Size(53, 17);
            this.ChkSexta.TabIndex = 22;
            this.ChkSexta.Text = "Sexta";
            this.ChkSexta.UseVisualStyleBackColor = true;
            // 
            // chkQuinta
            // 
            this.chkQuinta.AutoSize = true;
            this.chkQuinta.Location = new System.Drawing.Point(55, 15);
            this.chkQuinta.Name = "chkQuinta";
            this.chkQuinta.Size = new System.Drawing.Size(51, 17);
            this.chkQuinta.TabIndex = 21;
            this.chkQuinta.Text = "Quin.";
            this.chkQuinta.UseVisualStyleBackColor = true;
            // 
            // ChkSabado
            // 
            this.ChkSabado.AutoSize = true;
            this.ChkSabado.Location = new System.Drawing.Point(56, 56);
            this.ChkSabado.Name = "ChkSabado";
            this.ChkSabado.Size = new System.Drawing.Size(48, 17);
            this.ChkSabado.TabIndex = 18;
            this.ChkSabado.Text = "Sab.";
            this.ChkSabado.UseVisualStyleBackColor = true;
            // 
            // ChkQuarta
            // 
            this.ChkQuarta.AutoSize = true;
            this.ChkQuarta.Location = new System.Drawing.Point(6, 56);
            this.ChkQuarta.Name = "ChkQuarta";
            this.ChkQuarta.Size = new System.Drawing.Size(52, 17);
            this.ChkQuarta.TabIndex = 20;
            this.ChkQuarta.Text = "Quar.";
            this.ChkQuarta.UseVisualStyleBackColor = true;
            // 
            // chkTerca
            // 
            this.chkTerca.AutoSize = true;
            this.chkTerca.Location = new System.Drawing.Point(6, 36);
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
            // cbxGrupoProduto
            // 
            this.cbxGrupoProduto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbxGrupoProduto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxGrupoProduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGrupoProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGrupoProduto.FormattingEnabled = true;
            this.cbxGrupoProduto.Location = new System.Drawing.Point(8, 141);
            this.cbxGrupoProduto.Name = "cbxGrupoProduto";
            this.cbxGrupoProduto.Size = new System.Drawing.Size(223, 26);
            this.cbxGrupoProduto.TabIndex = 4;
            this.cbxGrupoProduto.DropDown += new System.EventHandler(this.cbxGrupoProduto_DropDown);
            this.cbxGrupoProduto.Click += new System.EventHandler(this.cbxGrupoProduto_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tbInsumo);
            this.tabControl1.Location = new System.Drawing.Point(0, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(681, 300);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label27);
            this.tabPage4.Controls.Add(this.txtPalavrasChave);
            this.tabPage4.Controls.Add(this.groupBox3);
            this.tabPage4.Controls.Add(this.grpPrecosDia);
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(673, 274);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "3 - Preços e Promoções";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(394, 57);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(140, 13);
            this.label27.TabIndex = 47;
            this.label27.Text = "Palavras Chave (KeyWords)";
            // 
            // txtPalavrasChave
            // 
            this.txtPalavrasChave.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "DescricaoProduto", true));
            this.txtPalavrasChave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPalavrasChave.Location = new System.Drawing.Point(390, 76);
            this.txtPalavrasChave.Multiline = true;
            this.txtPalavrasChave.Name = "txtPalavrasChave";
            this.txtPalavrasChave.Size = new System.Drawing.Size(207, 64);
            this.txtPalavrasChave.TabIndex = 46;
            this.toolTip1.SetToolTip(this.txtPalavrasChave, resources.GetString("txtPalavrasChave.ToolTip"));
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.txtPontosTroca);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.txtPontosFidelidade);
            this.groupBox3.Location = new System.Drawing.Point(217, 68);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(167, 72);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pontos Fidelidade";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(88, 16);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(71, 13);
            this.label24.TabIndex = 48;
            this.label24.Text = "Pontos Troca";
            // 
            // txtPontosTroca
            // 
            this.txtPontosTroca.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPontosTroca.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPontosTroca.Location = new System.Drawing.Point(86, 37);
            this.txtPontosTroca.Name = "txtPontosTroca";
            this.txtPontosTroca.Size = new System.Drawing.Size(69, 26);
            this.txtPontosTroca.TabIndex = 47;
            this.txtPontosTroca.Tag = "Friday";
            this.toolTip1.SetToolTip(this.txtPontosTroca, "Quantos pontos terá que junta para troca\r\n");
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 18);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(74, 13);
            this.label23.TabIndex = 46;
            this.label23.Text = "Pontos Venda";
            // 
            // txtPontosFidelidade
            // 
            this.txtPontosFidelidade.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPontosFidelidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPontosFidelidade.Location = new System.Drawing.Point(11, 37);
            this.txtPontosFidelidade.Name = "txtPontosFidelidade";
            this.txtPontosFidelidade.Size = new System.Drawing.Size(69, 26);
            this.txtPontosFidelidade.TabIndex = 45;
            this.txtPontosFidelidade.Tag = "Friday";
            this.toolTip1.SetToolTip(this.txtPontosFidelidade, "Defina quantos pontos vale o produto no programa de fidelidade");
            // 
            // grpPrecosDia
            // 
            this.grpPrecosDia.BackColor = System.Drawing.Color.Transparent;
            this.grpPrecosDia.Controls.Add(this.label15);
            this.grpPrecosDia.Controls.Add(this.txtPrecoDomingo);
            this.grpPrecosDia.Controls.Add(this.label14);
            this.grpPrecosDia.Controls.Add(this.txtPrecoSabado);
            this.grpPrecosDia.Controls.Add(this.label13);
            this.grpPrecosDia.Controls.Add(this.txtPrecoSexta);
            this.grpPrecosDia.Controls.Add(this.label12);
            this.grpPrecosDia.Controls.Add(this.txtPrecoQuinta);
            this.grpPrecosDia.Controls.Add(this.label11);
            this.grpPrecosDia.Controls.Add(this.txtPrecoQuarta);
            this.grpPrecosDia.Controls.Add(this.label10);
            this.grpPrecosDia.Controls.Add(this.txtPrecoTerca);
            this.grpPrecosDia.Controls.Add(this.label7);
            this.grpPrecosDia.Controls.Add(this.txtPrecoSegunda);
            this.grpPrecosDia.Controls.Add(this.chkDom);
            this.grpPrecosDia.Controls.Add(this.chkSex);
            this.grpPrecosDia.Controls.Add(this.chkQui);
            this.grpPrecosDia.Controls.Add(this.chkSab);
            this.grpPrecosDia.Controls.Add(this.chkQua);
            this.grpPrecosDia.Controls.Add(this.chkTer);
            this.grpPrecosDia.Controls.Add(this.chkSeg);
            this.grpPrecosDia.Location = new System.Drawing.Point(3, 140);
            this.grpPrecosDia.Name = "grpPrecosDia";
            this.grpPrecosDia.Size = new System.Drawing.Size(594, 89);
            this.grpPrecosDia.TabIndex = 34;
            this.grpPrecosDia.TabStop = false;
            this.grpPrecosDia.Text = "Preço Diferente por Dia";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(480, 41);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 46;
            this.label15.Text = "Preço";
            // 
            // txtPrecoDomingo
            // 
            this.txtPrecoDomingo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPrecoDomingo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoDomingo.Location = new System.Drawing.Point(483, 57);
            this.txtPrecoDomingo.Name = "txtPrecoDomingo";
            this.txtPrecoDomingo.Size = new System.Drawing.Size(69, 26);
            this.txtPrecoDomingo.TabIndex = 45;
            this.txtPrecoDomingo.Tag = "Sunday";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(396, 41);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 13);
            this.label14.TabIndex = 44;
            this.label14.Text = "Preço";
            // 
            // txtPrecoSabado
            // 
            this.txtPrecoSabado.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPrecoSabado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoSabado.Location = new System.Drawing.Point(399, 57);
            this.txtPrecoSabado.Name = "txtPrecoSabado";
            this.txtPrecoSabado.Size = new System.Drawing.Size(69, 26);
            this.txtPrecoSabado.TabIndex = 43;
            this.txtPrecoSabado.Tag = "Saturday";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(319, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 42;
            this.label13.Text = "Preço";
            // 
            // txtPrecoSexta
            // 
            this.txtPrecoSexta.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPrecoSexta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoSexta.Location = new System.Drawing.Point(322, 57);
            this.txtPrecoSexta.Name = "txtPrecoSexta";
            this.txtPrecoSexta.Size = new System.Drawing.Size(69, 26);
            this.txtPrecoSexta.TabIndex = 41;
            this.txtPrecoSexta.Tag = "Friday";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(236, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 40;
            this.label12.Text = "Preço";
            // 
            // txtPrecoQuinta
            // 
            this.txtPrecoQuinta.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPrecoQuinta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoQuinta.Location = new System.Drawing.Point(239, 57);
            this.txtPrecoQuinta.Name = "txtPrecoQuinta";
            this.txtPrecoQuinta.Size = new System.Drawing.Size(69, 26);
            this.txtPrecoQuinta.TabIndex = 39;
            this.txtPrecoQuinta.Tag = "Thursday";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(160, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 38;
            this.label11.Text = "Preço";
            // 
            // txtPrecoQuarta
            // 
            this.txtPrecoQuarta.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPrecoQuarta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoQuarta.Location = new System.Drawing.Point(163, 57);
            this.txtPrecoQuarta.Name = "txtPrecoQuarta";
            this.txtPrecoQuarta.Size = new System.Drawing.Size(69, 26);
            this.txtPrecoQuarta.TabIndex = 37;
            this.txtPrecoQuarta.Tag = "Wednesday";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(82, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "Preço";
            // 
            // txtPrecoTerca
            // 
            this.txtPrecoTerca.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPrecoTerca.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoTerca.Location = new System.Drawing.Point(85, 57);
            this.txtPrecoTerca.Name = "txtPrecoTerca";
            this.txtPrecoTerca.Size = new System.Drawing.Size(69, 26);
            this.txtPrecoTerca.TabIndex = 35;
            this.txtPrecoTerca.Tag = "Tuesday";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Preço ";
            // 
            // txtPrecoSegunda
            // 
            this.txtPrecoSegunda.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPrecoSegunda.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoSegunda.Location = new System.Drawing.Point(10, 57);
            this.txtPrecoSegunda.Name = "txtPrecoSegunda";
            this.txtPrecoSegunda.Size = new System.Drawing.Size(69, 26);
            this.txtPrecoSegunda.TabIndex = 33;
            this.txtPrecoSegunda.Tag = "Monday";
            // 
            // chkDom
            // 
            this.chkDom.AutoSize = true;
            this.chkDom.Location = new System.Drawing.Point(483, 18);
            this.chkDom.Name = "chkDom";
            this.chkDom.Size = new System.Drawing.Size(68, 17);
            this.chkDom.TabIndex = 32;
            this.chkDom.Tag = "Sunday";
            this.chkDom.Text = "Domingo";
            this.chkDom.UseVisualStyleBackColor = true;
            // 
            // chkSex
            // 
            this.chkSex.AutoSize = true;
            this.chkSex.Location = new System.Drawing.Point(322, 19);
            this.chkSex.Name = "chkSex";
            this.chkSex.Size = new System.Drawing.Size(53, 17);
            this.chkSex.TabIndex = 31;
            this.chkSex.Tag = "Friday";
            this.chkSex.Text = "Sexta";
            this.chkSex.UseVisualStyleBackColor = true;
            // 
            // chkQui
            // 
            this.chkQui.AutoSize = true;
            this.chkQui.Location = new System.Drawing.Point(239, 18);
            this.chkQui.Name = "chkQui";
            this.chkQui.Size = new System.Drawing.Size(57, 17);
            this.chkQui.TabIndex = 30;
            this.chkQui.Tag = "Thursday";
            this.chkQui.Text = "Quinta";
            this.chkQui.UseVisualStyleBackColor = true;
            // 
            // chkSab
            // 
            this.chkSab.AutoSize = true;
            this.chkSab.Location = new System.Drawing.Point(405, 18);
            this.chkSab.Name = "chkSab";
            this.chkSab.Size = new System.Drawing.Size(63, 17);
            this.chkSab.TabIndex = 26;
            this.chkSab.Tag = "Saturday";
            this.chkSab.Text = "Sabado";
            this.chkSab.UseVisualStyleBackColor = true;
            // 
            // chkQua
            // 
            this.chkQua.AutoSize = true;
            this.chkQua.Location = new System.Drawing.Point(163, 18);
            this.chkQua.Name = "chkQua";
            this.chkQua.Size = new System.Drawing.Size(58, 17);
            this.chkQua.TabIndex = 29;
            this.chkQua.Tag = "Wednesday";
            this.chkQua.Text = "Quarta";
            this.chkQua.UseVisualStyleBackColor = true;
            // 
            // chkTer
            // 
            this.chkTer.AutoSize = true;
            this.chkTer.Location = new System.Drawing.Point(85, 18);
            this.chkTer.Name = "chkTer";
            this.chkTer.Size = new System.Drawing.Size(54, 17);
            this.chkTer.TabIndex = 28;
            this.chkTer.Tag = "Tuesday";
            this.chkTer.Text = "Terça";
            this.chkTer.UseVisualStyleBackColor = true;
            // 
            // chkSeg
            // 
            this.chkSeg.AutoSize = true;
            this.chkSeg.Location = new System.Drawing.Point(10, 19);
            this.chkSeg.Name = "chkSeg";
            this.chkSeg.Size = new System.Drawing.Size(69, 17);
            this.chkSeg.TabIndex = 27;
            this.chkSeg.Tag = "Monday";
            this.chkSeg.Text = "Segunda";
            this.chkSeg.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.horaInicio);
            this.groupBox2.Controls.Add(this.HoraFim);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(3, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 72);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Horario que o produto estará disponivel";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(95, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Fim";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Inicio";
            // 
            // horaInicio
            // 
            this.horaInicio.Checked = false;
            this.horaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.horaInicio.Location = new System.Drawing.Point(10, 43);
            this.horaInicio.Name = "horaInicio";
            this.horaInicio.Size = new System.Drawing.Size(82, 20);
            this.horaInicio.TabIndex = 11;
            // 
            // HoraFim
            // 
            this.HoraFim.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.HoraFim.Location = new System.Drawing.Point(98, 43);
            this.HoraFim.Name = "HoraFim";
            this.HoraFim.Size = new System.Drawing.Size(86, 20);
            this.HoraFim.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.checkBox5);
            this.groupBox1.Controls.Add(this.checkBox6);
            this.groupBox1.Controls.Add(this.checkBox7);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(594, 50);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dias que o produto estará disponivel";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(483, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(68, 17);
            this.checkBox1.TabIndex = 32;
            this.checkBox1.Text = "Domingo";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(322, 19);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(53, 17);
            this.checkBox2.TabIndex = 31;
            this.checkBox2.Text = "Sexta";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(239, 18);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(57, 17);
            this.checkBox3.TabIndex = 30;
            this.checkBox3.Text = "Quinta";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(405, 18);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(63, 17);
            this.checkBox4.TabIndex = 26;
            this.checkBox4.Text = "Sabado";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(163, 18);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(58, 17);
            this.checkBox5.TabIndex = 29;
            this.checkBox5.Text = "Quarta";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(85, 18);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(54, 17);
            this.checkBox6.TabIndex = 28;
            this.checkBox6.Text = "Terça";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(10, 19);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(69, 17);
            this.checkBox7.TabIndex = 27;
            this.checkBox7.Text = "Segunda";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // tbInsumo
            // 
            this.tbInsumo.Controls.Add(this.label18);
            this.tbInsumo.Controls.Add(this.txtQtd);
            this.tbInsumo.Controls.Add(this.panel2);
            this.tbInsumo.Controls.Add(this.btnEditarIns);
            this.tbInsumo.Controls.Add(this.btnAdicionar);
            this.tbInsumo.Controls.Add(this.cbxInsumo);
            this.tbInsumo.Controls.Add(this.label20);
            this.tbInsumo.Location = new System.Drawing.Point(4, 22);
            this.tbInsumo.Name = "tbInsumo";
            this.tbInsumo.Padding = new System.Windows.Forms.Padding(3);
            this.tbInsumo.Size = new System.Drawing.Size(673, 274);
            this.tbInsumo.TabIndex = 4;
            this.tbInsumo.Text = "5 - Composição/Insumo";
            this.tbInsumo.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(383, 13);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(27, 13);
            this.label18.TabIndex = 41;
            this.label18.Text = "Qtd.";
            // 
            // txtQtd
            // 
            this.txtQtd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtQtd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQtd.Location = new System.Drawing.Point(386, 27);
            this.txtQtd.Name = "txtQtd";
            this.txtQtd.Size = new System.Drawing.Size(52, 26);
            this.txtQtd.TabIndex = 40;
            this.toolTip1.SetToolTip(this.txtQtd, "Informe a quantidade usada para produzir o produto");
            this.txtQtd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQtd_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridInsumo);
            this.panel2.Location = new System.Drawing.Point(3, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(667, 208);
            this.panel2.TabIndex = 39;
            // 
            // gridInsumo
            // 
            this.gridInsumo.AllowUserToAddRows = false;
            this.gridInsumo.AllowUserToDeleteRows = false;
            this.gridInsumo.AllowUserToOrderColumns = true;
            this.gridInsumo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridInsumo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gridInsumo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridInsumo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridInsumo.Location = new System.Drawing.Point(0, 0);
            this.gridInsumo.MultiSelect = false;
            this.gridInsumo.Name = "gridInsumo";
            this.gridInsumo.ReadOnly = true;
            this.gridInsumo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridInsumo.Size = new System.Drawing.Size(667, 208);
            this.gridInsumo.TabIndex = 2;
            this.gridInsumo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuAuxiliar);
            this.gridInsumo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.EditarRegistro);
            // 
            // btnEditarIns
            // 
            this.btnEditarIns.Image = ((System.Drawing.Image)(resources.GetObject("btnEditarIns.Image")));
            this.btnEditarIns.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditarIns.Location = new System.Drawing.Point(532, 27);
            this.btnEditarIns.Name = "btnEditarIns";
            this.btnEditarIns.Size = new System.Drawing.Size(63, 26);
            this.btnEditarIns.TabIndex = 38;
            this.btnEditarIns.Text = "Editar";
            this.btnEditarIns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditarIns.UseVisualStyleBackColor = true;
            this.btnEditarIns.Click += new System.EventHandler(this.btnEditarIns_Click);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Image = ((System.Drawing.Image)(resources.GetObject("btnAdicionar.Image")));
            this.btnAdicionar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionar.Location = new System.Drawing.Point(451, 27);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(75, 26);
            this.btnAdicionar.TabIndex = 37;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.AdicionarInsumoProduto);
            // 
            // cbxInsumo
            // 
            this.cbxInsumo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxInsumo.FormattingEnabled = true;
            this.cbxInsumo.Location = new System.Drawing.Point(11, 29);
            this.cbxInsumo.Name = "cbxInsumo";
            this.cbxInsumo.Size = new System.Drawing.Size(359, 21);
            this.cbxInsumo.TabIndex = 34;
            this.cbxInsumo.DropDown += new System.EventHandler(this.ListaInsumos);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 13);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(38, 13);
            this.label20.TabIndex = 33;
            this.label20.Text = "Nome ";
            // 
            // frmCadastrarProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 349);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnDoProduto);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmCadastrarProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[XDelivery] Cadastrar Produto ";
            this.Load += new System.EventHandler(this.frmCadastrarProduto_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCadastrarProduto_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dBExpertDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.produtoBindingSource)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgProduto)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AdicionaisGridView)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.grpEstoque.ResumeLayout(false);
            this.grpEstoque.PerformLayout();
            this.grpDesconto.ResumeLayout(false);
            this.grpDesconto.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpPrecosDia.ResumeLayout(false);
            this.grpPrecosDia.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tbInsumo.ResumeLayout(false);
            this.tbInsumo.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridInsumo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DBExpertDataSet dBExpertDataSet;
        private System.Windows.Forms.BindingSource produtoBindingSource;
        private DBExpertDataSetTableAdapters.ProdutoTableAdapter produtoTableAdapter;
        private DBExpertDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnDoProduto;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox imgProduto;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnAdicionarOpcao;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrecoOpcao;
        private System.Windows.Forms.ComboBox cbxOpcao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView AdicionaisGridView;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtMaxAdicionais;
        private System.Windows.Forms.TextBox nomeProdutoTextBox;
        private System.Windows.Forms.TextBox descricaoProdutoTextBox;
        private System.Windows.Forms.TextBox precoProdutoTextBox;
        private System.Windows.Forms.CheckBox chkOnline;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.GroupBox grpDesconto;
        private System.Windows.Forms.CheckBox chkDomingo;
        private System.Windows.Forms.TextBox txtPrecoDesconto;
        private System.Windows.Forms.CheckBox ChkSexta;
        private System.Windows.Forms.CheckBox chkQuinta;
        private System.Windows.Forms.CheckBox ChkSabado;
        private System.Windows.Forms.CheckBox ChkQuarta;
        private System.Windows.Forms.CheckBox chkTerca;
        private System.Windows.Forms.CheckBox chkSegunda;
        private System.Windows.Forms.ComboBox cbxGrupoProduto;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnImg;
        private System.Windows.Forms.TextBox txtcaminhoImage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker horaInicio;
        private System.Windows.Forms.DateTimePicker HoraFim;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPrecoDomingo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPrecoSabado;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPrecoSexta;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPrecoQuinta;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPrecoQuarta;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPrecoTerca;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPrecoSegunda;
        private System.Windows.Forms.CheckBox chkDom;
        private System.Windows.Forms.CheckBox chkSex;
        private System.Windows.Forms.CheckBox chkQui;
        private System.Windows.Forms.CheckBox chkSab;
        private System.Windows.Forms.CheckBox chkQua;
        private System.Windows.Forms.CheckBox chkTer;
        private System.Windows.Forms.CheckBox chkSeg;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbxTipoOpcao;
        private System.Windows.Forms.TextBox txtCodInterno;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox grpPrecosDia;
        private System.Windows.Forms.TabPage tbInsumo;
        private System.Windows.Forms.Button btnEditarIns;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.ComboBox cbxInsumo;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView gridInsumo;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtQtd;
        private System.Windows.Forms.TextBox txtMarkup;
        private System.Windows.Forms.TextBox txtPrecoCusto;
        private System.Windows.Forms.TextBox txtPrecoSugerido;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtPontosFidelidade;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtPontosTroca;
        private System.Windows.Forms.GroupBox grpEstoque;
        private System.Windows.Forms.CheckBox chkControleEstoque;
        private System.Windows.Forms.TextBox txtEstoqueAtual;
        private System.Windows.Forms.TextBox txtEstMinimo;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtPalavrasChave;
    }
}