namespace DexComanda.Cadastros.Produto
{
    partial class frmCadOpcao
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
            this.OpcaoGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.cbxTipo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAtivoSN = new System.Windows.Forms.CheckBox();
            this.chkOnlineSN = new System.Windows.Forms.CheckBox();
            this.grpDiasDisponivel = new System.Windows.Forms.GroupBox();
            this.chkDomingo = new System.Windows.Forms.CheckBox();
            this.ChkSexta = new System.Windows.Forms.CheckBox();
            this.chkQuinta = new System.Windows.Forms.CheckBox();
            this.ChkSabado = new System.Windows.Forms.CheckBox();
            this.ChkQuarta = new System.Windows.Forms.CheckBox();
            this.chkTerca = new System.Windows.Forms.CheckBox();
            this.chkSegunda = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.OpcaoGridView)).BeginInit();
            this.grpDiasDisponivel.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpcaoGridView
            // 
            this.OpcaoGridView.AllowUserToAddRows = false;
            this.OpcaoGridView.AllowUserToDeleteRows = false;
            this.OpcaoGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OpcaoGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.OpcaoGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.OpcaoGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OpcaoGridView.Location = new System.Drawing.Point(4, 221);
            this.OpcaoGridView.MultiSelect = false;
            this.OpcaoGridView.Name = "OpcaoGridView";
            this.OpcaoGridView.ReadOnly = true;
            this.OpcaoGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OpcaoGridView.Size = new System.Drawing.Size(288, 252);
            this.OpcaoGridView.TabIndex = 2;
            this.OpcaoGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OpcaoGridView_CellClick);
            this.OpcaoGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OpcaoGridView_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Nome da Opção";
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(158, 179);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(130, 35);
            this.btnEditar.TabIndex = 2;
            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.EditarOpcao);
            // 
            // txtNome
            // 
            this.txtNome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNome.Location = new System.Drawing.Point(4, 26);
            this.txtNome.MaxLength = 30;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(284, 26);
            this.txtNome.TabIndex = 0;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(4, 181);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(114, 35);
            this.btnAdicionar.TabIndex = 1;
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.CadastraOpcao);
            // 
            // cbxTipo
            // 
            this.cbxTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipo.FormattingEnabled = true;
            this.cbxTipo.Items.AddRange(new object[] {
            "Selecao unica",
            "Multipla Selecao",
            "Texto livre"});
            this.cbxTipo.Location = new System.Drawing.Point(4, 151);
            this.cbxTipo.Name = "cbxTipo";
            this.cbxTipo.Size = new System.Drawing.Size(186, 21);
            this.cbxTipo.TabIndex = 17;
            this.cbxTipo.DropDown += new System.EventHandler(this.cbxTipo_DropDown);
            this.cbxTipo.SelectionChangeCommitted += new System.EventHandler(this.cbxTipo_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Tipo";
            // 
            // chkAtivoSN
            // 
            this.chkAtivoSN.AutoSize = true;
            this.chkAtivoSN.Checked = true;
            this.chkAtivoSN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivoSN.Location = new System.Drawing.Point(139, 2);
            this.chkAtivoSN.Name = "chkAtivoSN";
            this.chkAtivoSN.Size = new System.Drawing.Size(65, 17);
            this.chkAtivoSN.TabIndex = 67;
            this.chkAtivoSN.Text = "AtivoSN";
            this.chkAtivoSN.UseVisualStyleBackColor = true;
            // 
            // chkOnlineSN
            // 
            this.chkOnlineSN.AutoSize = true;
            this.chkOnlineSN.Checked = true;
            this.chkOnlineSN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnlineSN.Location = new System.Drawing.Point(210, 3);
            this.chkOnlineSN.Name = "chkOnlineSN";
            this.chkOnlineSN.Size = new System.Drawing.Size(71, 17);
            this.chkOnlineSN.TabIndex = 66;
            this.chkOnlineSN.Text = "OnlineSN";
            this.chkOnlineSN.UseVisualStyleBackColor = true;
            // 
            // grpDiasDisponivel
            // 
            this.grpDiasDisponivel.Controls.Add(this.chkDomingo);
            this.grpDiasDisponivel.Controls.Add(this.ChkSexta);
            this.grpDiasDisponivel.Controls.Add(this.chkQuinta);
            this.grpDiasDisponivel.Controls.Add(this.ChkSabado);
            this.grpDiasDisponivel.Controls.Add(this.ChkQuarta);
            this.grpDiasDisponivel.Controls.Add(this.chkTerca);
            this.grpDiasDisponivel.Controls.Add(this.chkSegunda);
            this.grpDiasDisponivel.Location = new System.Drawing.Point(4, 73);
            this.grpDiasDisponivel.Name = "grpDiasDisponivel";
            this.grpDiasDisponivel.Size = new System.Drawing.Size(284, 60);
            this.grpDiasDisponivel.TabIndex = 70;
            this.grpDiasDisponivel.TabStop = false;
            this.grpDiasDisponivel.Text = "Disponibilidade";
            // 
            // chkDomingo
            // 
            this.chkDomingo.AutoSize = true;
            this.chkDomingo.Checked = true;
            this.chkDomingo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDomingo.Location = new System.Drawing.Point(111, 38);
            this.chkDomingo.Name = "chkDomingo";
            this.chkDomingo.Size = new System.Drawing.Size(51, 17);
            this.chkDomingo.TabIndex = 25;
            this.chkDomingo.Tag = "Sunday";
            this.chkDomingo.Text = "Dom.";
            this.chkDomingo.UseVisualStyleBackColor = true;
            // 
            // ChkSexta
            // 
            this.ChkSexta.AutoSize = true;
            this.ChkSexta.Checked = true;
            this.ChkSexta.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSexta.Location = new System.Drawing.Point(6, 38);
            this.ChkSexta.Name = "ChkSexta";
            this.ChkSexta.Size = new System.Drawing.Size(44, 17);
            this.ChkSexta.TabIndex = 22;
            this.ChkSexta.Tag = "Friday";
            this.ChkSexta.Text = "Sex";
            this.ChkSexta.UseVisualStyleBackColor = true;
            // 
            // chkQuinta
            // 
            this.chkQuinta.AutoSize = true;
            this.chkQuinta.Checked = true;
            this.chkQuinta.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQuinta.Location = new System.Drawing.Point(170, 13);
            this.chkQuinta.Name = "chkQuinta";
            this.chkQuinta.Size = new System.Drawing.Size(51, 17);
            this.chkQuinta.TabIndex = 21;
            this.chkQuinta.Tag = "Thursday";
            this.chkQuinta.Text = "Quin.";
            this.chkQuinta.UseVisualStyleBackColor = true;
            // 
            // ChkSabado
            // 
            this.ChkSabado.AutoSize = true;
            this.ChkSabado.Checked = true;
            this.ChkSabado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSabado.Location = new System.Drawing.Point(60, 38);
            this.ChkSabado.Name = "ChkSabado";
            this.ChkSabado.Size = new System.Drawing.Size(48, 17);
            this.ChkSabado.TabIndex = 18;
            this.ChkSabado.Tag = "Saturday";
            this.ChkSabado.Text = "Sab.";
            this.ChkSabado.UseVisualStyleBackColor = true;
            // 
            // ChkQuarta
            // 
            this.ChkQuarta.AutoSize = true;
            this.ChkQuarta.Checked = true;
            this.ChkQuarta.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkQuarta.Location = new System.Drawing.Point(111, 13);
            this.ChkQuarta.Name = "ChkQuarta";
            this.ChkQuarta.Size = new System.Drawing.Size(52, 17);
            this.ChkQuarta.TabIndex = 20;
            this.ChkQuarta.Tag = "Wednesday";
            this.ChkQuarta.Text = "Quar.";
            this.ChkQuarta.UseVisualStyleBackColor = true;
            // 
            // chkTerca
            // 
            this.chkTerca.AutoSize = true;
            this.chkTerca.Checked = true;
            this.chkTerca.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTerca.Location = new System.Drawing.Point(60, 13);
            this.chkTerca.Name = "chkTerca";
            this.chkTerca.Size = new System.Drawing.Size(45, 17);
            this.chkTerca.TabIndex = 19;
            this.chkTerca.Tag = "Tuesday";
            this.chkTerca.Text = "Ter.";
            this.chkTerca.UseVisualStyleBackColor = true;
            // 
            // chkSegunda
            // 
            this.chkSegunda.AutoSize = true;
            this.chkSegunda.Checked = true;
            this.chkSegunda.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSegunda.Location = new System.Drawing.Point(6, 15);
            this.chkSegunda.Name = "chkSegunda";
            this.chkSegunda.Size = new System.Drawing.Size(48, 17);
            this.chkSegunda.TabIndex = 18;
            this.chkSegunda.Tag = "Monday";
            this.chkSegunda.Text = "Seg.";
            this.chkSegunda.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(90, 58);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(131, 17);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Tag = "Thursday";
            this.checkBox1.Text = "Marca todos dias SN?";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // frmCadOpcao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 478);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.grpDiasDisponivel);
            this.Controls.Add(this.chkAtivoSN);
            this.Controls.Add(this.chkOnlineSN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxTipo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.OpcaoGridView);
            this.KeyPreview = true;
            this.Name = "frmCadOpcao";
            this.Text = "[xSistemas] Cadastros de Opção";
            this.Load += new System.EventHandler(this.frmCadOpcao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OpcaoGridView)).EndInit();
            this.grpDiasDisponivel.ResumeLayout(false);
            this.grpDiasDisponivel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView OpcaoGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.ComboBox cbxTipo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAtivoSN;
        private System.Windows.Forms.CheckBox chkOnlineSN;
        private System.Windows.Forms.GroupBox grpDiasDisponivel;
        private System.Windows.Forms.CheckBox chkDomingo;
        private System.Windows.Forms.CheckBox ChkSexta;
        private System.Windows.Forms.CheckBox chkQuinta;
        private System.Windows.Forms.CheckBox ChkSabado;
        private System.Windows.Forms.CheckBox ChkQuarta;
        private System.Windows.Forms.CheckBox chkTerca;
        private System.Windows.Forms.CheckBox chkSegunda;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}