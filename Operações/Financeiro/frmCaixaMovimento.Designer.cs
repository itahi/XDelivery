namespace DexComanda.Operações
{
    partial class frmCaixaMovimento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCaixaMovimento));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxFPagamento = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxNumCaixa = new System.Windows.Forms.ComboBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.chkFPagamento = new System.Windows.Forms.CheckBox();
            this.rbSaida = new System.Windows.Forms.RadioButton();
            this.rbEntrada = new System.Windows.Forms.RadioButton();
            this.rbEntradaSaida = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MovimentosGridView = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblEntradas = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblLiquido = new System.Windows.Forms.Label();
            this.lblSaidas = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovimentosGridView)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbxFPagamento);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbxNumCaixa);
            this.groupBox1.Controls.Add(this.btnFiltrar);
            this.groupBox1.Controls.Add(this.chkFPagamento);
            this.groupBox1.Controls.Add(this.rbSaida);
            this.groupBox1.Controls.Add(this.rbEntrada);
            this.groupBox1.Controls.Add(this.rbEntradaSaida);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtFim);
            this.groupBox1.Controls.Add(this.dtInicio);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(690, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "F. Pagamento";
            // 
            // cbxFPagamento
            // 
            this.cbxFPagamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFPagamento.FormattingEnabled = true;
            this.cbxFPagamento.Location = new System.Drawing.Point(88, 52);
            this.cbxFPagamento.Name = "cbxFPagamento";
            this.cbxFPagamento.Size = new System.Drawing.Size(166, 21);
            this.cbxFPagamento.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(489, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Caixa";
            // 
            // cbxNumCaixa
            // 
            this.cbxNumCaixa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxNumCaixa.FormattingEnabled = true;
            this.cbxNumCaixa.Location = new System.Drawing.Point(528, 45);
            this.cbxNumCaixa.Name = "cbxNumCaixa";
            this.cbxNumCaixa.Size = new System.Drawing.Size(48, 21);
            this.cbxNumCaixa.TabIndex = 9;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.ImageKey = "(none)";
            this.btnFiltrar.Location = new System.Drawing.Point(582, 19);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(83, 50);
            this.btnFiltrar.TabIndex = 8;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.ExecutaFiltro);
            // 
            // chkFPagamento
            // 
            this.chkFPagamento.AutoSize = true;
            this.chkFPagamento.Location = new System.Drawing.Point(270, 52);
            this.chkFPagamento.Name = "chkFPagamento";
            this.chkFPagamento.Size = new System.Drawing.Size(172, 17);
            this.chkFPagamento.TabIndex = 7;
            this.chkFPagamento.Text = "Agrupar Formas de Pagamento";
            this.chkFPagamento.UseVisualStyleBackColor = true;
            this.chkFPagamento.CheckedChanged += new System.EventHandler(this.chkFPagamento_CheckedChanged);
            // 
            // rbSaida
            // 
            this.rbSaida.AutoSize = true;
            this.rbSaida.Location = new System.Drawing.Point(503, 26);
            this.rbSaida.Name = "rbSaida";
            this.rbSaida.Size = new System.Drawing.Size(73, 17);
            this.rbSaida.TabIndex = 6;
            this.rbSaida.Text = "Só Saidas";
            this.rbSaida.UseVisualStyleBackColor = true;
            // 
            // rbEntrada
            // 
            this.rbEntrada.AutoSize = true;
            this.rbEntrada.Location = new System.Drawing.Point(401, 26);
            this.rbEntrada.Name = "rbEntrada";
            this.rbEntrada.Size = new System.Drawing.Size(83, 17);
            this.rbEntrada.TabIndex = 5;
            this.rbEntrada.Text = "Só Entradas";
            this.rbEntrada.UseVisualStyleBackColor = true;
            // 
            // rbEntradaSaida
            // 
            this.rbEntradaSaida.AutoSize = true;
            this.rbEntradaSaida.Checked = true;
            this.rbEntradaSaida.Location = new System.Drawing.Point(270, 26);
            this.rbEntradaSaida.Name = "rbEntradaSaida";
            this.rbEntradaSaida.Size = new System.Drawing.Size(111, 17);
            this.rbEntradaSaida.TabIndex = 4;
            this.rbEntradaSaida.TabStop = true;
            this.rbEntradaSaida.Text = "Entradas e Saidas";
            this.rbEntradaSaida.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Periodo:";
            // 
            // dtFim
            // 
            this.dtFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFim.Location = new System.Drawing.Point(169, 22);
            this.dtFim.Name = "dtFim";
            this.dtFim.Size = new System.Drawing.Size(85, 20);
            this.dtFim.TabIndex = 1;
            // 
            // dtInicio
            // 
            this.dtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtInicio.Location = new System.Drawing.Point(60, 22);
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Size = new System.Drawing.Size(85, 20);
            this.dtInicio.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.MovimentosGridView);
            this.panel1.Location = new System.Drawing.Point(0, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(691, 315);
            this.panel1.TabIndex = 1;
            // 
            // MovimentosGridView
            // 
            this.MovimentosGridView.AllowUserToAddRows = false;
            this.MovimentosGridView.AllowUserToDeleteRows = false;
            this.MovimentosGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MovimentosGridView.BackgroundColor = System.Drawing.Color.White;
            this.MovimentosGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MovimentosGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MovimentosGridView.Location = new System.Drawing.Point(0, 0);
            this.MovimentosGridView.Name = "MovimentosGridView";
            this.MovimentosGridView.ReadOnly = true;
            this.MovimentosGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MovimentosGridView.Size = new System.Drawing.Size(691, 315);
            this.MovimentosGridView.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblEntradas);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.lblLiquido);
            this.panel2.Controls.Add(this.lblSaidas);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(25, 415);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(633, 25);
            this.panel2.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(432, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Liquido R$:";
            // 
            // lblEntradas
            // 
            this.lblEntradas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntradas.AutoSize = true;
            this.lblEntradas.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblEntradas.Location = new System.Drawing.Point(160, 3);
            this.lblEntradas.Name = "lblEntradas";
            this.lblEntradas.Size = new System.Drawing.Size(28, 13);
            this.lblEntradas.TabIndex = 12;
            this.lblEntradas.Text = "0,00";
            this.lblEntradas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(56, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Total Entradas $:";
            // 
            // lblLiquido
            // 
            this.lblLiquido.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLiquido.AutoSize = true;
            this.lblLiquido.ForeColor = System.Drawing.Color.Red;
            this.lblLiquido.Location = new System.Drawing.Point(509, 3);
            this.lblLiquido.Name = "lblLiquido";
            this.lblLiquido.Size = new System.Drawing.Size(28, 13);
            this.lblLiquido.TabIndex = 10;
            this.lblLiquido.Text = "0,00";
            // 
            // lblSaidas
            // 
            this.lblSaidas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSaidas.AutoSize = true;
            this.lblSaidas.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblSaidas.Location = new System.Drawing.Point(324, 3);
            this.lblSaidas.Name = "lblSaidas";
            this.lblSaidas.Size = new System.Drawing.Size(28, 13);
            this.lblSaidas.TabIndex = 9;
            this.lblSaidas.Text = "0,00";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(223, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Total Saidas $:";
            // 
            // frmCaixaMovimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 451);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCaixaMovimento";
            this.Text = "XDelivery [ Controle de Caixa]";
            this.Load += new System.EventHandler(this.frmCaixaMovimento_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MovimentosGridView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.RadioButton rbSaida;
        private System.Windows.Forms.RadioButton rbEntrada;
        private System.Windows.Forms.RadioButton rbEntradaSaida;
        private System.Windows.Forms.CheckBox chkFPagamento;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.DataGridView MovimentosGridView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxNumCaixa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxFPagamento;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblEntradas;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblLiquido;
        protected System.Windows.Forms.Label lblSaidas;
        private System.Windows.Forms.Label label3;
    }
}