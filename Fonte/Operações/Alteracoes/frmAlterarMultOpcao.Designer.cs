namespace DexComanda.Operações.Alteracoes
{
    partial class frmAlterarMultOpcao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAlterarMultOpcao));
            this.grpDiasDisponivel = new System.Windows.Forms.GroupBox();
            this.chkDomingo = new System.Windows.Forms.CheckBox();
            this.ChkSexta = new System.Windows.Forms.CheckBox();
            this.chkQuinta = new System.Windows.Forms.CheckBox();
            this.ChkSabado = new System.Windows.Forms.CheckBox();
            this.ChkQuarta = new System.Windows.Forms.CheckBox();
            this.chkTerca = new System.Windows.Forms.CheckBox();
            this.chkSegunda = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxTipo = new System.Windows.Forms.ComboBox();
            this.cbxOpcao = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAtivoSN = new System.Windows.Forms.CheckBox();
            this.chkOnlineSN = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.GridView = new System.Windows.Forms.DataGridView();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdicionarOpcao = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnExecutar = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.grpDiasDisponivel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
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
            this.grpDiasDisponivel.Location = new System.Drawing.Point(10, 75);
            this.grpDiasDisponivel.Name = "grpDiasDisponivel";
            this.grpDiasDisponivel.Size = new System.Drawing.Size(224, 66);
            this.grpDiasDisponivel.TabIndex = 73;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 72;
            this.label2.Text = "Tipo";
            // 
            // cbxTipo
            // 
            this.cbxTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipo.FormattingEnabled = true;
            this.cbxTipo.Items.AddRange(new object[] {
            "Selecao unica",
            "Multipla Selecao",
            "Texto livre"});
            this.cbxTipo.Location = new System.Drawing.Point(10, 25);
            this.cbxTipo.Name = "cbxTipo";
            this.cbxTipo.Size = new System.Drawing.Size(163, 21);
            this.cbxTipo.TabIndex = 71;
            this.cbxTipo.SelectionChangeCommitted += new System.EventHandler(this.cbxTipo_SelectionChangeCommitted);
            // 
            // cbxOpcao
            // 
            this.cbxOpcao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOpcao.FormattingEnabled = true;
            this.cbxOpcao.Location = new System.Drawing.Point(180, 25);
            this.cbxOpcao.Name = "cbxOpcao";
            this.cbxOpcao.Size = new System.Drawing.Size(172, 21);
            this.cbxOpcao.TabIndex = 74;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(177, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Opção";
            // 
            // chkAtivoSN
            // 
            this.chkAtivoSN.AutoSize = true;
            this.chkAtivoSN.Checked = true;
            this.chkAtivoSN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivoSN.Location = new System.Drawing.Point(240, 88);
            this.chkAtivoSN.Name = "chkAtivoSN";
            this.chkAtivoSN.Size = new System.Drawing.Size(65, 17);
            this.chkAtivoSN.TabIndex = 77;
            this.chkAtivoSN.Text = "AtivoSN";
            this.chkAtivoSN.UseVisualStyleBackColor = true;
            // 
            // chkOnlineSN
            // 
            this.chkOnlineSN.AutoSize = true;
            this.chkOnlineSN.Checked = true;
            this.chkOnlineSN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnlineSN.Location = new System.Drawing.Point(240, 121);
            this.chkOnlineSN.Name = "chkOnlineSN";
            this.chkOnlineSN.Size = new System.Drawing.Size(71, 17);
            this.chkOnlineSN.TabIndex = 76;
            this.chkOnlineSN.Text = "OnlineSN";
            this.chkOnlineSN.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.GridView);
            this.groupBox2.Location = new System.Drawing.Point(10, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(403, 159);
            this.groupBox2.TabIndex = 78;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resultado - Filtro";
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.AllowUserToDeleteRows = false;
            this.GridView.AllowUserToOrderColumns = true;
            this.GridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Nome});
            this.GridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridView.Location = new System.Drawing.Point(3, 16);
            this.GridView.MultiSelect = false;
            this.GridView.Name = "GridView";
            this.GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridView.Size = new System.Drawing.Size(397, 140);
            this.GridView.TabIndex = 2;
            // 
            // Codigo
            // 
            this.Codigo.FillWeight = 50.76142F;
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.Name = "Codigo";
            // 
            // Nome
            // 
            this.Nome.FillWeight = 149.2386F;
            this.Nome.HeaderText = "Nome";
            this.Nome.Name = "Nome";
            // 
            // btnAdicionarOpcao
            // 
            this.btnAdicionarOpcao.Image = ((System.Drawing.Image)(resources.GetObject("btnAdicionarOpcao.Image")));
            this.btnAdicionarOpcao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionarOpcao.Location = new System.Drawing.Point(358, 21);
            this.btnAdicionarOpcao.Name = "btnAdicionarOpcao";
            this.btnAdicionarOpcao.Size = new System.Drawing.Size(35, 26);
            this.btnAdicionarOpcao.TabIndex = 79;
            this.btnAdicionarOpcao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdicionarOpcao.UseVisualStyleBackColor = true;
            this.btnAdicionarOpcao.Click += new System.EventHandler(this.AdicionaItemGrid);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelar.Location = new System.Drawing.Point(199, 306);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 82;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnExecutar
            // 
            this.btnExecutar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExecutar.Location = new System.Drawing.Point(129, 306);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(64, 23);
            this.btnExecutar.TabIndex = 81;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecutar.UseVisualStyleBackColor = true;
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(10, 53);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(131, 17);
            this.checkBox1.TabIndex = 83;
            this.checkBox1.Tag = "Thursday";
            this.checkBox1.Text = "Marca todos dias SN?";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // frmAlterarMultOpcao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 339);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnExecutar);
            this.Controls.Add(this.btnAdicionarOpcao);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chkAtivoSN);
            this.Controls.Add(this.chkOnlineSN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxOpcao);
            this.Controls.Add(this.grpDiasDisponivel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxTipo);
            this.Name = "frmAlterarMultOpcao";
            this.Text = "[xSistemas] Montar Cardapio";
            this.Load += new System.EventHandler(this.frmAlterarMultOpcao_Load);
            this.grpDiasDisponivel.ResumeLayout(false);
            this.grpDiasDisponivel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDiasDisponivel;
        private System.Windows.Forms.CheckBox chkDomingo;
        private System.Windows.Forms.CheckBox ChkSexta;
        private System.Windows.Forms.CheckBox chkQuinta;
        private System.Windows.Forms.CheckBox ChkSabado;
        private System.Windows.Forms.CheckBox ChkQuarta;
        private System.Windows.Forms.CheckBox chkTerca;
        private System.Windows.Forms.CheckBox chkSegunda;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxTipo;
        private System.Windows.Forms.ComboBox cbxOpcao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAtivoSN;
        private System.Windows.Forms.CheckBox chkOnlineSN;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView GridView;
        private System.Windows.Forms.Button btnAdicionarOpcao;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnExecutar;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
    }
}