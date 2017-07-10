namespace DexComanda.Cadastros.Pessoa
{
    partial class frmCadastroOrigem
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
            this.txtNome = new System.Windows.Forms.TextBox();
            this.chkAtivoSN = new System.Windows.Forms.CheckBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.OrigemGridView = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.OrigemGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(12, 43);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(279, 20);
            this.txtNome.TabIndex = 0;
            this.toolTip1.SetToolTip(this.txtNome, "Preencha o nome que deseja exibir ");
            // 
            // chkAtivoSN
            // 
            this.chkAtivoSN.AutoSize = true;
            this.chkAtivoSN.Location = new System.Drawing.Point(202, 17);
            this.chkAtivoSN.Name = "chkAtivoSN";
            this.chkAtivoSN.Size = new System.Drawing.Size(71, 17);
            this.chkAtivoSN.TabIndex = 1;
            this.chkAtivoSN.Text = "AtivoSN?";
            this.chkAtivoSN.UseVisualStyleBackColor = true;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(9, 21);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(38, 13);
            this.lblNome.TabIndex = 2;
            this.lblNome.Text = "Nome:";
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(160, 73);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(131, 35);
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(12, 73);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(115, 35);
            this.btnAdicionar.TabIndex = 2;
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.AdicionarRegistro);
            // 
            // OrigemGridView
            // 
            this.OrigemGridView.AllowUserToAddRows = false;
            this.OrigemGridView.AllowUserToDeleteRows = false;
            this.OrigemGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrigemGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.OrigemGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.OrigemGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OrigemGridView.Location = new System.Drawing.Point(7, 114);
            this.OrigemGridView.MultiSelect = false;
            this.OrigemGridView.Name = "OrigemGridView";
            this.OrigemGridView.ReadOnly = true;
            this.OrigemGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OrigemGridView.Size = new System.Drawing.Size(284, 227);
            this.OrigemGridView.TabIndex = 4;
            this.OrigemGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuAuxiliar);
            // 
            // frmCadastroOrigem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 353);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.OrigemGridView);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.chkAtivoSN);
            this.Controls.Add(this.txtNome);
            this.KeyPreview = true;
            this.Name = "frmCadastroOrigem";
            this.Text = "[xSistemas] Origem do Cliente";
            this.Load += new System.EventHandler(this.frmCadastroOrigem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCadastroOrigem_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.OrigemGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.CheckBox chkAtivoSN;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.DataGridView OrigemGridView;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}