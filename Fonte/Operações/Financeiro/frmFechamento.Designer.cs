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
            this.FechamentosGrid = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxTurno = new System.Windows.Forms.ComboBox();
            this.dtFechamento = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.txtVlrFechamento = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtVlrAbertura = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUFechamento = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDtAbertura = new System.Windows.Forms.TextBox();
            this.cbxCaixas = new System.Windows.Forms.ComboBox();
            this.btnExecutar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FechamentosGrid)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FechamentosGrid);
            this.groupBox1.Location = new System.Drawing.Point(1, 134);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Valores Somados";
            // 
            // FechamentosGrid
            // 
            this.FechamentosGrid.AllowUserToAddRows = false;
            this.FechamentosGrid.AllowUserToDeleteRows = false;
            this.FechamentosGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FechamentosGrid.BackgroundColor = System.Drawing.Color.White;
            this.FechamentosGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FechamentosGrid.Location = new System.Drawing.Point(3, 16);
            this.FechamentosGrid.Name = "FechamentosGrid";
            this.FechamentosGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FechamentosGrid.Size = new System.Drawing.Size(442, 132);
            this.FechamentosGrid.TabIndex = 9;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cbxTurno);
            this.groupBox3.Controls.Add(this.dtFechamento);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtVlrFechamento);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtVlrAbertura);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtUFechamento);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtDtAbertura);
            this.groupBox3.Controls.Add(this.cbxCaixas);
            this.groupBox3.Location = new System.Drawing.Point(1, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(448, 97);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filtro Caixa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Turno";
            // 
            // cbxTurno
            // 
            this.cbxTurno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTurno.FormattingEnabled = true;
            this.cbxTurno.Items.AddRange(new object[] {
            "Dia",
            "Noite"});
            this.cbxTurno.Location = new System.Drawing.Point(10, 67);
            this.cbxTurno.Name = "cbxTurno";
            this.cbxTurno.Size = new System.Drawing.Size(66, 21);
            this.cbxTurno.TabIndex = 24;
            // 
            // dtFechamento
            // 
            this.dtFechamento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechamento.Location = new System.Drawing.Point(211, 68);
            this.dtFechamento.Name = "dtFechamento";
            this.dtFechamento.Size = new System.Drawing.Size(86, 20);
            this.dtFechamento.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(330, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Vlr. Fechamento";
            this.label11.Visible = false;
            // 
            // txtVlrFechamento
            // 
            this.txtVlrFechamento.Location = new System.Drawing.Point(333, 68);
            this.txtVlrFechamento.Name = "txtVlrFechamento";
            this.txtVlrFechamento.Size = new System.Drawing.Size(86, 20);
            this.txtVlrFechamento.TabIndex = 21;
            this.txtVlrFechamento.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(214, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Vlr. Abertura";
            // 
            // txtVlrAbertura
            // 
            this.txtVlrAbertura.Enabled = false;
            this.txtVlrAbertura.Location = new System.Drawing.Point(217, 27);
            this.txtVlrAbertura.Name = "txtVlrAbertura";
            this.txtVlrAbertura.Size = new System.Drawing.Size(86, 20);
            this.txtVlrAbertura.TabIndex = 19;
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
            this.txtUFechamento.Enabled = false;
            this.txtUFechamento.Location = new System.Drawing.Point(94, 68);
            this.txtUFechamento.Name = "txtUFechamento";
            this.txtUFechamento.Size = new System.Drawing.Size(86, 20);
            this.txtUFechamento.TabIndex = 17;
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(91, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Dt. Abertura";
            // 
            // txtDtAbertura
            // 
            this.txtDtAbertura.Enabled = false;
            this.txtDtAbertura.Location = new System.Drawing.Point(94, 26);
            this.txtDtAbertura.Name = "txtDtAbertura";
            this.txtDtAbertura.Size = new System.Drawing.Size(86, 20);
            this.txtDtAbertura.TabIndex = 11;
            // 
            // cbxCaixas
            // 
            this.cbxCaixas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCaixas.FormattingEnabled = true;
            this.cbxCaixas.Location = new System.Drawing.Point(10, 26);
            this.cbxCaixas.Name = "cbxCaixas";
            this.cbxCaixas.Size = new System.Drawing.Size(66, 21);
            this.cbxCaixas.TabIndex = 0;
            this.cbxCaixas.SelectionChangeCommitted += new System.EventHandler(this.FiltraCaixa);
            this.cbxCaixas.Click += new System.EventHandler(this.cbxCaixas_Click);
            // 
            // btnExecutar
            // 
            this.btnExecutar.Enabled = false;
            this.btnExecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnExecutar.Image")));
            this.btnExecutar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecutar.Location = new System.Drawing.Point(94, 274);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(121, 39);
            this.btnExecutar.TabIndex = 13;
            this.btnExecutar.Text = "Fechar";
            this.btnExecutar.UseVisualStyleBackColor = true;
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(232, 275);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(121, 39);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 31);
            this.button1.TabIndex = 15;
            this.button1.Text = "Consultar [F12]";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmCaixaFechamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 315);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnExecutar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmCaixaFechamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[XDelivery] Fechamento Caixa";
            this.Load += new System.EventHandler(this.frmCaixaFechamento_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FechamentosGrid)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDtAbertura;
        public System.Windows.Forms.DataGridView FechamentosGrid;
        private System.Windows.Forms.DateTimePicker dtFechamento;
        private System.Windows.Forms.Button btnExecutar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxTurno;
        private System.Windows.Forms.Button button1;
    }
}