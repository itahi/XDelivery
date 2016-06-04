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
            this.btnAddGrupo = new System.Windows.Forms.Button();
            grupoProdutoLabel = new System.Windows.Forms.Label();
            precoProdutoLabel = new System.Windows.Forms.Label();
            descricaoProdutoLabel = new System.Windows.Forms.Label();
            nomeProdutoLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dBExpertDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.produtoBindingSource)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgProduto)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AdicionaisGridView)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.grpDesconto.SuspendLayout();
            this.tabControl1.SuspendLayout();
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
            precoProdutoLabel.Location = new System.Drawing.Point(123, 69);
            precoProdutoLabel.Name = "precoProdutoLabel";
            precoProdutoLabel.Size = new System.Drawing.Size(78, 13);
            precoProdutoLabel.TabIndex = 30;
            precoProdutoLabel.Text = "Preco Produto:";
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
            label1.Location = new System.Drawing.Point(152, 15);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(74, 13);
            label1.TabIndex = 24;
            label1.Text = " C/ Desconto:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 71);
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
            this.btnSair.Location = new System.Drawing.Point(456, 303);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(150, 43);
            this.btnSair.TabIndex = 35;
            this.btnSair.Text = "Sair [ESC]";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click_1);
            // 
            // btnDoProduto
            // 
            this.btnDoProduto.Location = new System.Drawing.Point(288, 303);
            this.btnDoProduto.Name = "btnDoProduto";
            this.btnDoProduto.Size = new System.Drawing.Size(150, 43);
            this.btnDoProduto.TabIndex = 34;
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
            this.tabPage3.Size = new System.Drawing.Size(602, 274);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Imagem";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(547, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Limpar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnImg
            // 
            this.btnImg.Location = new System.Drawing.Point(486, 13);
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
            this.txtcaminhoImage.Size = new System.Drawing.Size(472, 20);
            this.txtcaminhoImage.TabIndex = 14;
            this.txtcaminhoImage.TextChanged += new System.EventHandler(this.txtcaminhoImage_TextChanged);
            // 
            // imgProduto
            // 
            this.imgProduto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imgProduto.Location = new System.Drawing.Point(8, 42);
            this.imgProduto.Name = "imgProduto";
            this.imgProduto.Size = new System.Drawing.Size(588, 226);
            this.imgProduto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgProduto.TabIndex = 0;
            this.imgProduto.TabStop = false;
            // 
            // tabPage2
            // 
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
            this.tabPage2.Size = new System.Drawing.Size(602, 274);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Adicionais/Opcionais";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(533, 19);
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
            this.btnAdicionarOpcao.Location = new System.Drawing.Point(440, 19);
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
            this.label4.Location = new System.Drawing.Point(339, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Preço";
            // 
            // txtPrecoOpcao
            // 
            this.txtPrecoOpcao.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtPrecoOpcao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoOpcao.Location = new System.Drawing.Point(342, 18);
            this.txtPrecoOpcao.Name = "txtPrecoOpcao";
            this.txtPrecoOpcao.Size = new System.Drawing.Size(92, 26);
            this.txtPrecoOpcao.TabIndex = 27;
            // 
            // cbxOpcao
            // 
            this.cbxOpcao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOpcao.FormattingEnabled = true;
            this.cbxOpcao.Location = new System.Drawing.Point(11, 24);
            this.cbxOpcao.Name = "cbxOpcao";
            this.cbxOpcao.Size = new System.Drawing.Size(300, 21);
            this.cbxOpcao.TabIndex = 6;
            this.cbxOpcao.Click += new System.EventHandler(this.ListaOpcao);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Adicionais do Produto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nome ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AdicionaisGridView);
            this.panel1.Location = new System.Drawing.Point(3, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(593, 178);
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
            this.AdicionaisGridView.Size = new System.Drawing.Size(593, 178);
            this.AdicionaisGridView.TabIndex = 2;
            this.AdicionaisGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AdicionaisGridView_CellClick);
            this.AdicionaisGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AdicionaisGridView_CellContentClick);
            this.AdicionaisGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AdicionaisGridView_MouseClick);
            this.AdicionaisGridView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.EditarLinha);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnAddGrupo);
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
            this.tabPage1.Size = new System.Drawing.Size(602, 274);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Produto";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtMaxAdicionais
            // 
            this.txtMaxAdicionais.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.txtMaxAdicionais.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxAdicionais.Location = new System.Drawing.Point(9, 88);
            this.txtMaxAdicionais.Name = "txtMaxAdicionais";
            this.txtMaxAdicionais.Size = new System.Drawing.Size(92, 26);
            this.txtMaxAdicionais.TabIndex = 36;
            // 
            // nomeProdutoTextBox
            // 
            this.nomeProdutoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "NomeProduto", true));
            this.nomeProdutoTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nomeProdutoTextBox.Location = new System.Drawing.Point(11, 25);
            this.nomeProdutoTextBox.Name = "nomeProdutoTextBox";
            this.nomeProdutoTextBox.Size = new System.Drawing.Size(585, 26);
            this.nomeProdutoTextBox.TabIndex = 23;
            // 
            // descricaoProdutoTextBox
            // 
            this.descricaoProdutoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "DescricaoProduto", true));
            this.descricaoProdutoTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descricaoProdutoTextBox.Location = new System.Drawing.Point(6, 186);
            this.descricaoProdutoTextBox.Multiline = true;
            this.descricaoProdutoTextBox.Name = "descricaoProdutoTextBox";
            this.descricaoProdutoTextBox.Size = new System.Drawing.Size(590, 82);
            this.descricaoProdutoTextBox.TabIndex = 27;
            // 
            // precoProdutoTextBox
            // 
            this.precoProdutoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.produtoBindingSource, "PrecoProduto", true));
            this.precoProdutoTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.precoProdutoTextBox.Location = new System.Drawing.Point(126, 86);
            this.precoProdutoTextBox.Name = "precoProdutoTextBox";
            this.precoProdutoTextBox.Size = new System.Drawing.Size(92, 26);
            this.precoProdutoTextBox.TabIndex = 26;
            // 
            // chkOnline
            // 
            this.chkOnline.AutoSize = true;
            this.chkOnline.Location = new System.Drawing.Point(422, 3);
            this.chkOnline.Name = "chkOnline";
            this.chkOnline.Size = new System.Drawing.Size(96, 17);
            this.chkOnline.TabIndex = 35;
            this.chkOnline.Text = "Venda Online?";
            this.chkOnline.UseVisualStyleBackColor = true;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Checked = true;
            this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivo.Location = new System.Drawing.Point(357, 2);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(59, 17);
            this.chkAtivo.TabIndex = 34;
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
            this.grpDesconto.Location = new System.Drawing.Point(364, 57);
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
            this.txtPrecoDesconto.Size = new System.Drawing.Size(75, 26);
            this.txtPrecoDesconto.TabIndex = 23;
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
            this.cbxGrupoProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGrupoProduto.FormattingEnabled = true;
            this.cbxGrupoProduto.Location = new System.Drawing.Point(8, 141);
            this.cbxGrupoProduto.Name = "cbxGrupoProduto";
            this.cbxGrupoProduto.Size = new System.Drawing.Size(253, 26);
            this.cbxGrupoProduto.TabIndex = 24;
            this.cbxGrupoProduto.Click += new System.EventHandler(this.cbxGrupoProduto_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(610, 300);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // btnAddGrupo
            // 
            this.btnAddGrupo.Image = ((System.Drawing.Image)(resources.GetObject("btnAddGrupo.Image")));
            this.btnAddGrupo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddGrupo.Location = new System.Drawing.Point(267, 138);
            this.btnAddGrupo.Name = "btnAddGrupo";
            this.btnAddGrupo.Size = new System.Drawing.Size(74, 33);
            this.btnAddGrupo.TabIndex = 36;
            this.btnAddGrupo.Text = "Grupo";
            this.btnAddGrupo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddGrupo.UseVisualStyleBackColor = true;
            this.btnAddGrupo.Click += new System.EventHandler(this.AddGrupo);
            // 
            // frmCadastrarProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 349);
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
            this.grpDesconto.ResumeLayout(false);
            this.grpDesconto.PerformLayout();
            this.tabControl1.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnAddGrupo;
    }
}