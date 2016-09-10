namespace DexComanda.Cadastros.Produto
{
    partial class frmTipoOpcao
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
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpTipo = new System.Windows.Forms.GroupBox();
            this.rbTexto = new System.Windows.Forms.RadioButton();
            this.rbMultipla = new System.Windows.Forms.RadioButton();
            this.rbUnica = new System.Windows.Forms.RadioButton();
            this.grpMaxMin = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.txtMinimo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TipoOpcaoGrid = new System.Windows.Forms.DataGridView();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.cbxOrdem = new System.Windows.Forms.ComboBox();
            this.chkOnlineSN = new System.Windows.Forms.CheckBox();
            this.chkAtivoSN = new System.Windows.Forms.CheckBox();
            this.grpTipo.SuspendLayout();
            this.grpMaxMin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TipoOpcaoGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(12, 42);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(216, 20);
            this.txtNome.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome Tipo";
            // 
            // grpTipo
            // 
            this.grpTipo.Controls.Add(this.rbTexto);
            this.grpTipo.Controls.Add(this.rbMultipla);
            this.grpTipo.Controls.Add(this.rbUnica);
            this.grpTipo.Location = new System.Drawing.Point(15, 68);
            this.grpTipo.Name = "grpTipo";
            this.grpTipo.Size = new System.Drawing.Size(216, 67);
            this.grpTipo.TabIndex = 2;
            this.grpTipo.TabStop = false;
            this.grpTipo.Text = "Tipo";
            // 
            // rbTexto
            // 
            this.rbTexto.AutoSize = true;
            this.rbTexto.Location = new System.Drawing.Point(6, 44);
            this.rbTexto.Name = "rbTexto";
            this.rbTexto.Size = new System.Drawing.Size(52, 17);
            this.rbTexto.TabIndex = 2;
            this.rbTexto.TabStop = true;
            this.rbTexto.Tag = "3";
            this.rbTexto.Text = "Texto";
            this.rbTexto.UseVisualStyleBackColor = true;
            // 
            // rbMultipla
            // 
            this.rbMultipla.AutoSize = true;
            this.rbMultipla.Location = new System.Drawing.Point(107, 19);
            this.rbMultipla.Name = "rbMultipla";
            this.rbMultipla.Size = new System.Drawing.Size(61, 17);
            this.rbMultipla.TabIndex = 1;
            this.rbMultipla.TabStop = true;
            this.rbMultipla.Tag = "2";
            this.rbMultipla.Text = "Multipla";
            this.rbMultipla.UseVisualStyleBackColor = true;
            this.rbMultipla.CheckedChanged += new System.EventHandler(this.rbMultipla_CheckedChanged);
            // 
            // rbUnica
            // 
            this.rbUnica.AutoSize = true;
            this.rbUnica.Location = new System.Drawing.Point(6, 19);
            this.rbUnica.Name = "rbUnica";
            this.rbUnica.Size = new System.Drawing.Size(53, 17);
            this.rbUnica.TabIndex = 0;
            this.rbUnica.TabStop = true;
            this.rbUnica.Tag = "1";
            this.rbUnica.Text = "Unica";
            this.rbUnica.UseVisualStyleBackColor = true;
            // 
            // grpMaxMin
            // 
            this.grpMaxMin.Controls.Add(this.label3);
            this.grpMaxMin.Controls.Add(this.label2);
            this.grpMaxMin.Controls.Add(this.txtMax);
            this.grpMaxMin.Controls.Add(this.txtMinimo);
            this.grpMaxMin.Location = new System.Drawing.Point(237, 68);
            this.grpMaxMin.Name = "grpMaxMin";
            this.grpMaxMin.Size = new System.Drawing.Size(160, 67);
            this.grpMaxMin.TabIndex = 3;
            this.grpMaxMin.TabStop = false;
            this.grpMaxMin.Text = "Definições";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Maximo Opções";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Minimo Opções";
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(91, 41);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(36, 20);
            this.txtMax.TabIndex = 1;
            // 
            // txtMinimo
            // 
            this.txtMinimo.Location = new System.Drawing.Point(92, 16);
            this.txtMinimo.Name = "txtMinimo";
            this.txtMinimo.Size = new System.Drawing.Size(35, 20);
            this.txtMinimo.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(243, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Ordem Exibicao";
            // 
            // TipoOpcaoGrid
            // 
            this.TipoOpcaoGrid.AllowUserToAddRows = false;
            this.TipoOpcaoGrid.AllowUserToDeleteRows = false;
            this.TipoOpcaoGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TipoOpcaoGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TipoOpcaoGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TipoOpcaoGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TipoOpcaoGrid.Location = new System.Drawing.Point(15, 188);
            this.TipoOpcaoGrid.MultiSelect = false;
            this.TipoOpcaoGrid.Name = "TipoOpcaoGrid";
            this.TipoOpcaoGrid.ReadOnly = true;
            this.TipoOpcaoGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TipoOpcaoGrid.Size = new System.Drawing.Size(382, 155);
            this.TipoOpcaoGrid.TabIndex = 5;
            this.TipoOpcaoGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TipoOpcaoGrid_CellClick);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(149, 143);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(130, 35);
            this.btnEditar.TabIndex = 16;
            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(15, 142);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(114, 35);
            this.btnAdicionar.TabIndex = 15;
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // cbxOrdem
            // 
            this.cbxOrdem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxOrdem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxOrdem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOrdem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxOrdem.FormattingEnabled = true;
            this.cbxOrdem.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cbxOrdem.Location = new System.Drawing.Point(246, 39);
            this.cbxOrdem.Name = "cbxOrdem";
            this.cbxOrdem.Size = new System.Drawing.Size(42, 26);
            this.cbxOrdem.TabIndex = 63;
            // 
            // chkOnlineSN
            // 
            this.chkOnlineSN.AutoSize = true;
            this.chkOnlineSN.Checked = true;
            this.chkOnlineSN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnlineSN.Location = new System.Drawing.Point(140, 22);
            this.chkOnlineSN.Name = "chkOnlineSN";
            this.chkOnlineSN.Size = new System.Drawing.Size(71, 17);
            this.chkOnlineSN.TabIndex = 64;
            this.chkOnlineSN.Text = "OnlineSN";
            this.chkOnlineSN.UseVisualStyleBackColor = true;
            // 
            // chkAtivoSN
            // 
            this.chkAtivoSN.AutoSize = true;
            this.chkAtivoSN.Checked = true;
            this.chkAtivoSN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivoSN.Location = new System.Drawing.Point(69, 21);
            this.chkAtivoSN.Name = "chkAtivoSN";
            this.chkAtivoSN.Size = new System.Drawing.Size(65, 17);
            this.chkAtivoSN.TabIndex = 65;
            this.chkAtivoSN.Text = "AtivoSN";
            this.chkAtivoSN.UseVisualStyleBackColor = true;
            // 
            // frmTipoOpcao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 355);
            this.Controls.Add(this.chkAtivoSN);
            this.Controls.Add(this.chkOnlineSN);
            this.Controls.Add(this.cbxOrdem);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.TipoOpcaoGrid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grpMaxMin);
            this.Controls.Add(this.grpTipo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmTipoOpcao";
            this.Text = "[xDelivery] Tipo Opção";
            this.Load += new System.EventHandler(this.frmTipoOpcao_Load);
            this.grpTipo.ResumeLayout(false);
            this.grpTipo.PerformLayout();
            this.grpMaxMin.ResumeLayout(false);
            this.grpMaxMin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TipoOpcaoGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpTipo;
        private System.Windows.Forms.RadioButton rbTexto;
        private System.Windows.Forms.RadioButton rbMultipla;
        private System.Windows.Forms.RadioButton rbUnica;
        private System.Windows.Forms.GroupBox grpMaxMin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.TextBox txtMinimo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView TipoOpcaoGrid;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.ComboBox cbxOrdem;
        private System.Windows.Forms.CheckBox chkOnlineSN;
        private System.Windows.Forms.CheckBox chkAtivoSN;
    }
}