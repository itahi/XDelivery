namespace DexComanda.Operações
{
    partial class frmLancaEstoqueProd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLancaEstoqueProd));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridMovimento = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtprecocompra = new System.Windows.Forms.TextBox();
            this.btnLancar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.cbxProdutosGrid = new System.Windows.Forms.ComboBox();
            this.txbProduto = new System.Windows.Forms.Label();
            this.lblGrupo = new System.Windows.Forms.Label();
            this.cbxGrupo = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.CodProduto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeProduto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Preco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.groupBox1.Location = new System.Drawing.Point(5, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(595, 299);
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
            this.Preco,
            this.Quantidade});
            this.gridMovimento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMovimento.Location = new System.Drawing.Point(3, 16);
            this.gridMovimento.Name = "gridMovimento";
            this.gridMovimento.ReadOnly = true;
            this.gridMovimento.Size = new System.Drawing.Size(589, 280);
            this.gridMovimento.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtprecocompra);
            this.groupBox2.Controls.Add(this.btnLancar);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtQuantidade);
            this.groupBox2.Controls.Add(this.cbxProdutosGrid);
            this.groupBox2.Controls.Add(this.txbProduto);
            this.groupBox2.Controls.Add(this.lblGrupo);
            this.groupBox2.Controls.Add(this.cbxGrupo);
            this.groupBox2.Location = new System.Drawing.Point(5, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(595, 106);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtros";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(351, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Pr. Compra";
            // 
            // txtprecocompra
            // 
            this.txtprecocompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtprecocompra.Location = new System.Drawing.Point(354, 72);
            this.txtprecocompra.Name = "txtprecocompra";
            this.txtprecocompra.Size = new System.Drawing.Size(58, 26);
            this.txtprecocompra.TabIndex = 52;
            this.txtprecocompra.Text = "1,00";
            this.toolTip1.SetToolTip(this.txtprecocompra, "Preço de compra do  produto");
            this.txtprecocompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtprecocompra_KeyPress);
            // 
            // btnLancar
            // 
            this.btnLancar.Location = new System.Drawing.Point(482, 72);
            this.btnLancar.Name = "btnLancar";
            this.btnLancar.Size = new System.Drawing.Size(75, 23);
            this.btnLancar.TabIndex = 51;
            this.btnLancar.Text = "Lançar";
            this.btnLancar.UseVisualStyleBackColor = true;
            this.btnLancar.Click += new System.EventHandler(this.LancaMovimento);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(415, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Qtd.:";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidade.Location = new System.Drawing.Point(418, 72);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(58, 26);
            this.txtQuantidade.TabIndex = 47;
            this.txtQuantidade.Text = "1";
            this.toolTip1.SetToolTip(this.txtQuantidade, "Quantidade comprada do item");
            // 
            // cbxProdutosGrid
            // 
            this.cbxProdutosGrid.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxProdutosGrid.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxProdutosGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxProdutosGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxProdutosGrid.FormattingEnabled = true;
            this.cbxProdutosGrid.Location = new System.Drawing.Point(60, 73);
            this.cbxProdutosGrid.Name = "cbxProdutosGrid";
            this.cbxProdutosGrid.Size = new System.Drawing.Size(288, 26);
            this.cbxProdutosGrid.TabIndex = 45;
            // 
            // txbProduto
            // 
            this.txbProduto.AutoSize = true;
            this.txbProduto.Location = new System.Drawing.Point(7, 80);
            this.txbProduto.Name = "txbProduto";
            this.txbProduto.Size = new System.Drawing.Size(47, 13);
            this.txbProduto.TabIndex = 46;
            this.txbProduto.Text = "Produto:";
            // 
            // lblGrupo
            // 
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.Location = new System.Drawing.Point(15, 38);
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(39, 13);
            this.lblGrupo.TabIndex = 34;
            this.lblGrupo.Text = "Grupo:";
            // 
            // cbxGrupo
            // 
            this.cbxGrupo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxGrupo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxGrupo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGrupo.FormattingEnabled = true;
            this.cbxGrupo.Location = new System.Drawing.Point(60, 31);
            this.cbxGrupo.Name = "cbxGrupo";
            this.cbxGrupo.Size = new System.Drawing.Size(206, 26);
            this.cbxGrupo.TabIndex = 33;
            this.cbxGrupo.SelectedIndexChanged += new System.EventHandler(this.ListaProdutosGrupo);
            this.cbxGrupo.SelectionChangeCommitted += new System.EventHandler(this.cbxTipoProduto_SelectionChangeCommitted);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(149, 414);
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
            this.button2.Location = new System.Drawing.Point(304, 414);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 38);
            this.button2.TabIndex = 53;
            this.button2.Text = "Cancela Movimento";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // CodProduto
            // 
            this.CodProduto.HeaderText = "CodProduto";
            this.CodProduto.Name = "CodProduto";
            this.CodProduto.ReadOnly = true;
            this.CodProduto.Width = 70;
            // 
            // NomeProduto
            // 
            this.NomeProduto.HeaderText = "Nome Produto";
            this.NomeProduto.Name = "NomeProduto";
            this.NomeProduto.ReadOnly = true;
            this.NomeProduto.Width = 280;
            // 
            // Preco
            // 
            this.Preco.HeaderText = "Preço compra";
            this.Preco.Name = "Preco";
            this.Preco.ReadOnly = true;
            // 
            // Quantidade
            // 
            this.Quantidade.HeaderText = "Quantidade";
            this.Quantidade.Name = "Quantidade";
            this.Quantidade.ReadOnly = true;
            this.Quantidade.Width = 80;
            // 
            // frmLancaEstoqueProd
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(612, 462);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLancaEstoqueProd";
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
        private System.Windows.Forms.ComboBox cbxGrupo;
        private System.Windows.Forms.ComboBox cbxProdutosGrid;
        private System.Windows.Forms.Label txbProduto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.Button btnLancar;
        private System.Windows.Forms.DataGridView gridMovimento;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtprecocompra;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodProduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeProduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Preco;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantidade;
    }
}