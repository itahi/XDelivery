namespace DexComanda.Operações
{
    partial class frmLancaEstoque
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLancaEstoque));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridMovimento = new System.Windows.Forms.DataGridView();
            this.CodProduto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeProduto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoMovimento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLancar = new System.Windows.Forms.Button();
            this.rbSaida = new System.Windows.Forms.RadioButton();
            this.rbEntrada = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.cbxProdutosGrid = new System.Windows.Forms.ComboBox();
            this.txbProduto = new System.Windows.Forms.Label();
            this.lblGrupo = new System.Windows.Forms.Label();
            this.cbxTipoProduto = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMovimento)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.gridMovimento);
            this.groupBox1.Location = new System.Drawing.Point(5, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 287);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista Produtos";
            // 
            // gridMovimento
            // 
            this.gridMovimento.AllowUserToOrderColumns = true;
            this.gridMovimento.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gridMovimento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMovimento.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodProduto,
            this.NomeProduto,
            this.Quantidade,
            this.TipoMovimento});
            this.gridMovimento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMovimento.Location = new System.Drawing.Point(3, 16);
            this.gridMovimento.Name = "gridMovimento";
            this.gridMovimento.Size = new System.Drawing.Size(459, 268);
            this.gridMovimento.TabIndex = 0;
            // 
            // CodProduto
            // 
            this.CodProduto.HeaderText = "CodProduto";
            this.CodProduto.Name = "CodProduto";
            // 
            // NomeProduto
            // 
            this.NomeProduto.HeaderText = "Nome Produto";
            this.NomeProduto.Name = "NomeProduto";
            // 
            // Quantidade
            // 
            this.Quantidade.HeaderText = "Quantidade";
            this.Quantidade.Name = "Quantidade";
            // 
            // TipoMovimento
            // 
            this.TipoMovimento.HeaderText = "Entrada/Saida";
            this.TipoMovimento.Name = "TipoMovimento";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLancar);
            this.groupBox2.Controls.Add(this.rbSaida);
            this.groupBox2.Controls.Add(this.rbEntrada);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtQuantidade);
            this.groupBox2.Controls.Add(this.cbxProdutosGrid);
            this.groupBox2.Controls.Add(this.txbProduto);
            this.groupBox2.Controls.Add(this.lblGrupo);
            this.groupBox2.Controls.Add(this.cbxTipoProduto);
            this.groupBox2.Location = new System.Drawing.Point(5, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 91);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtros";
            // 
            // btnLancar
            // 
            this.btnLancar.Location = new System.Drawing.Point(368, 59);
            this.btnLancar.Name = "btnLancar";
            this.btnLancar.Size = new System.Drawing.Size(75, 23);
            this.btnLancar.TabIndex = 51;
            this.btnLancar.Text = "Lançar";
            this.btnLancar.UseVisualStyleBackColor = true;
            this.btnLancar.Click += new System.EventHandler(this.LancaMovimento);
            // 
            // rbSaida
            // 
            this.rbSaida.AutoSize = true;
            this.rbSaida.Location = new System.Drawing.Point(377, 19);
            this.rbSaida.Name = "rbSaida";
            this.rbSaida.Size = new System.Drawing.Size(54, 17);
            this.rbSaida.TabIndex = 50;
            this.rbSaida.Text = "Saída";
            this.rbSaida.UseVisualStyleBackColor = true;
            // 
            // rbEntrada
            // 
            this.rbEntrada.AutoSize = true;
            this.rbEntrada.Checked = true;
            this.rbEntrada.Location = new System.Drawing.Point(289, 19);
            this.rbEntrada.Name = "rbEntrada";
            this.rbEntrada.Size = new System.Drawing.Size(62, 17);
            this.rbEntrada.TabIndex = 49;
            this.rbEntrada.TabStop = true;
            this.rbEntrada.Text = "Entrada";
            this.rbEntrada.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Qtd.:";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidade.Location = new System.Drawing.Point(304, 59);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(58, 26);
            this.txtQuantidade.TabIndex = 47;
            this.txtQuantidade.Text = "1";
            // 
            // cbxProdutosGrid
            // 
            this.cbxProdutosGrid.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxProdutosGrid.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxProdutosGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxProdutosGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxProdutosGrid.FormattingEnabled = true;
            this.cbxProdutosGrid.Location = new System.Drawing.Point(60, 59);
            this.cbxProdutosGrid.Name = "cbxProdutosGrid";
            this.cbxProdutosGrid.Size = new System.Drawing.Size(206, 26);
            this.cbxProdutosGrid.TabIndex = 45;
            // 
            // txbProduto
            // 
            this.txbProduto.AutoSize = true;
            this.txbProduto.Location = new System.Drawing.Point(7, 66);
            this.txbProduto.Name = "txbProduto";
            this.txbProduto.Size = new System.Drawing.Size(47, 13);
            this.txbProduto.TabIndex = 46;
            this.txbProduto.Text = "Produto:";
            // 
            // lblGrupo
            // 
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.Location = new System.Drawing.Point(15, 25);
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(39, 13);
            this.lblGrupo.TabIndex = 34;
            this.lblGrupo.Text = "Grupo:";
            // 
            // cbxTipoProduto
            // 
            this.cbxTipoProduto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxTipoProduto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoProduto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxTipoProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipoProduto.FormattingEnabled = true;
            this.cbxTipoProduto.Location = new System.Drawing.Point(60, 19);
            this.cbxTipoProduto.Name = "cbxTipoProduto";
            this.cbxTipoProduto.Size = new System.Drawing.Size(206, 26);
            this.cbxTipoProduto.TabIndex = 33;
            this.cbxTipoProduto.SelectedIndexChanged += new System.EventHandler(this.ListaProdutosGrupo);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(84, 387);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 38);
            this.button1.TabIndex = 52;
            this.button1.Text = "Confirma Movimento";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.Location = new System.Drawing.Point(239, 387);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 38);
            this.button2.TabIndex = 53;
            this.button2.Text = "Cancela Movimento";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // frmLancaEstoque
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(482, 435);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLancaEstoque";
            this.Text = "[xDelivery]Movimentação Produto";
            this.Load += new System.EventHandler(this.frmLancaEstoque_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMovimento)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblGrupo;
        private System.Windows.Forms.ComboBox cbxTipoProduto;
        private System.Windows.Forms.ComboBox cbxProdutosGrid;
        private System.Windows.Forms.Label txbProduto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.RadioButton rbSaida;
        private System.Windows.Forms.RadioButton rbEntrada;
        private System.Windows.Forms.Button btnLancar;
        private System.Windows.Forms.DataGridView gridMovimento;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodProduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeProduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoMovimento;
    }
}