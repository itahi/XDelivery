namespace DexComanda
{
    partial class frmCadastrarEntregador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastrarEntregador));
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtComissao = new System.Windows.Forms.TextBox();
            this.EntregGridView = new System.Windows.Forms.DataGridView();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.EntregGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(7, 43);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(194, 20);
            this.txtNome.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(207, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Comissão %";
            // 
            // txtComissao
            // 
            this.txtComissao.Enabled = false;
            this.txtComissao.Location = new System.Drawing.Point(207, 43);
            this.txtComissao.Name = "txtComissao";
            this.txtComissao.Size = new System.Drawing.Size(75, 20);
            this.txtComissao.TabIndex = 3;
            // 
            // EntregGridView
            // 
            this.EntregGridView.AllowUserToAddRows = false;
            this.EntregGridView.AllowUserToDeleteRows = false;
            this.EntregGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.EntregGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.EntregGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EntregGridView.Location = new System.Drawing.Point(7, 123);
            this.EntregGridView.MultiSelect = false;
            this.EntregGridView.Name = "EntregGridView";
            this.EntregGridView.ReadOnly = true;
            this.EntregGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.EntregGridView.Size = new System.Drawing.Size(275, 184);
            this.EntregGridView.TabIndex = 5;
            this.EntregGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EntregGridView_CellClick);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(169, 69);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(113, 48);
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(7, 69);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(108, 48);
            this.btnAdicionar.TabIndex = 1;
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.AdicionarEntregador);
            // 
            // frmCadastrarEntregador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 319);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.EntregGridView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtComissao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmCadastrarEntregador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[XDelivery] Cadastro Entregador";
            this.Load += new System.EventHandler(this.frmCadastrarEntregador_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCadastrarEntregador_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.EntregGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtComissao;
        private System.Windows.Forms.DataGridView EntregGridView;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnAdicionar;
    }
}