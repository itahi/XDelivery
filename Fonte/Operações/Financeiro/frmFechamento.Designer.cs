namespace DexComanda.Operações.Financeiro
{
    partial class frmCaixaFechamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCaixaFechamento));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbxCaixas = new System.Windows.Forms.ComboBox();
            this.txtDtAbertura = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDtFechamento = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUAbertura = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUFechamento = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtVlrAbertura = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtVlrFechamento = new System.Windows.Forms.TextBox();
            this.FechamentosGrid = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FechamentosGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FechamentosGrid);
            this.groupBox1.Location = new System.Drawing.Point(1, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 134);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Valores Somados";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtVlrFechamento);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtVlrAbertura);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtUFechamento);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtUAbertura);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtDtFechamento);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtDtAbertura);
            this.groupBox3.Controls.Add(this.cbxCaixas);
            this.groupBox3.Location = new System.Drawing.Point(1, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(448, 109);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filtro Caixa";
            // 
            // cbxCaixas
            // 
            this.cbxCaixas.FormattingEnabled = true;
            this.cbxCaixas.Location = new System.Drawing.Point(10, 26);
            this.cbxCaixas.Name = "cbxCaixas";
            this.cbxCaixas.Size = new System.Drawing.Size(66, 21);
            this.cbxCaixas.TabIndex = 0;
            this.cbxCaixas.SelectionChangeCommitted += new System.EventHandler(this.FiltraCaixa);
            // 
            // txtDtAbertura
            // 
            this.txtDtAbertura.Location = new System.Drawing.Point(211, 26);
            this.txtDtAbertura.Name = "txtDtAbertura";
            this.txtDtAbertura.Size = new System.Drawing.Size(86, 20);
            this.txtDtAbertura.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(214, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Dt. Fechamento";
            // 
            // txtDtFechamento
            // 
            this.txtDtFechamento.Location = new System.Drawing.Point(210, 68);
            this.txtDtFechamento.Name = "txtDtFechamento";
            this.txtDtFechamento.Size = new System.Drawing.Size(86, 20);
            this.txtDtFechamento.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(91, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "U. Abertura";
            // 
            // txtUAbertura
            // 
            this.txtUAbertura.Location = new System.Drawing.Point(94, 27);
            this.txtUAbertura.Name = "txtUAbertura";
            this.txtUAbertura.Size = new System.Drawing.Size(86, 20);
            this.txtUAbertura.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(91, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "U. Fechamento";
            // 
            // txtUFechamento
            // 
            this.txtUFechamento.Location = new System.Drawing.Point(94, 68);
            this.txtUFechamento.Name = "txtUFechamento";
            this.txtUFechamento.Size = new System.Drawing.Size(86, 20);
            this.txtUFechamento.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(208, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Dt. Abertura";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(330, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Vlr. Abertura";
            // 
            // txtVlrAbertura
            // 
            this.txtVlrAbertura.Location = new System.Drawing.Point(333, 26);
            this.txtVlrAbertura.Name = "txtVlrAbertura";
            this.txtVlrAbertura.Size = new System.Drawing.Size(86, 20);
            this.txtVlrAbertura.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(330, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Vlr. Fechamento";
            // 
            // txtVlrFechamento
            // 
            this.txtVlrFechamento.Location = new System.Drawing.Point(333, 68);
            this.txtVlrFechamento.Name = "txtVlrFechamento";
            this.txtVlrFechamento.Size = new System.Drawing.Size(86, 20);
            this.txtVlrFechamento.TabIndex = 21;
            // 
            // FechamentosGrid
            // 
            this.FechamentosGrid.AllowUserToAddRows = false;
            this.FechamentosGrid.AllowUserToDeleteRows = false;
            this.FechamentosGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FechamentosGrid.BackgroundColor = System.Drawing.Color.White;
            this.FechamentosGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FechamentosGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FechamentosGrid.Location = new System.Drawing.Point(3, 16);
            this.FechamentosGrid.Name = "FechamentosGrid";
            this.FechamentosGrid.ReadOnly = true;
            this.FechamentosGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FechamentosGrid.Size = new System.Drawing.Size(442, 115);
            this.FechamentosGrid.TabIndex = 9;
            // 
            // frmCaixaFechamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 361);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCaixaFechamento";
            this.Text = "[XDelivery] Fechamento Caixa";
            this.Load += new System.EventHandler(this.frmCaixaFechamento_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FechamentosGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbxCaixas;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtVlrFechamento;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtVlrAbertura;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUFechamento;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUAbertura;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDtFechamento;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDtAbertura;
        public System.Windows.Forms.DataGridView FechamentosGrid;
    }
}