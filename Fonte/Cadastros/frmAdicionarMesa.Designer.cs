namespace DexComanda.Cadastros
{
    partial class frmAdicionarMesa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdicionarMesa));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxStatusMesa = new System.Windows.Forms.ComboBox();
            this.btnEditar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumeroMesa = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MesasGridView = new System.Windows.Forms.DataGridView();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.chkOnline = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MesasGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAtivo);
            this.groupBox1.Controls.Add(this.chkOnline);
            this.groupBox1.Controls.Add(this.cbxStatusMesa);
            this.groupBox1.Controls.Add(this.btnEditar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnSalvar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNumeroMesa);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 116);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cadastro ";
            // 
            // cbxStatusMesa
            // 
            this.cbxStatusMesa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStatusMesa.FormattingEnabled = true;
            this.cbxStatusMesa.Items.AddRange(new object[] {
            "Aberta",
            "Ocupada",
            "Reservada"});
            this.cbxStatusMesa.Location = new System.Drawing.Point(80, 39);
            this.cbxStatusMesa.Name = "cbxStatusMesa";
            this.cbxStatusMesa.Size = new System.Drawing.Size(161, 21);
            this.cbxStatusMesa.TabIndex = 1;
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(144, 71);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(86, 39);
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Status";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(27, 72);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(86, 37);
            this.btnSalvar.TabIndex = 2;
            this.btnSalvar.Text = "Adicionar [F12]";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.AdicionarMesa);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mesa";
            // 
            // txtNumeroMesa
            // 
            this.txtNumeroMesa.Location = new System.Drawing.Point(27, 40);
            this.txtNumeroMesa.Name = "txtNumeroMesa";
            this.txtNumeroMesa.Size = new System.Drawing.Size(47, 20);
            this.txtNumeroMesa.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.MesasGridView);
            this.groupBox2.Location = new System.Drawing.Point(5, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(306, 152);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista Mesas";
            // 
            // MesasGridView
            // 
            this.MesasGridView.AllowUserToAddRows = false;
            this.MesasGridView.AllowUserToDeleteRows = false;
            this.MesasGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MesasGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.MesasGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MesasGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MesasGridView.Location = new System.Drawing.Point(3, 16);
            this.MesasGridView.MultiSelect = false;
            this.MesasGridView.Name = "MesasGridView";
            this.MesasGridView.ReadOnly = true;
            this.MesasGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MesasGridView.Size = new System.Drawing.Size(300, 133);
            this.MesasGridView.TabIndex = 3;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(224, 16);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(56, 17);
            this.chkAtivo.TabIndex = 14;
            this.chkAtivo.Text = "Ativo?";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // chkOnline
            // 
            this.chkOnline.AutoSize = true;
            this.chkOnline.Location = new System.Drawing.Point(144, 16);
            this.chkOnline.Name = "chkOnline";
            this.chkOnline.Size = new System.Drawing.Size(62, 17);
            this.chkOnline.TabIndex = 13;
            this.chkOnline.Text = "Online?";
            this.chkOnline.UseVisualStyleBackColor = true;
            // 
            // frmAdicionarMesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 289);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAdicionarMesa";
            this.Text = "[XDelivery] Cadastro Mesas";
            this.Load += new System.EventHandler(this.frmAdicionarMesa_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MesasGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxStatusMesa;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumeroMesa;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView MesasGridView;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.CheckBox chkOnline;
    }
}