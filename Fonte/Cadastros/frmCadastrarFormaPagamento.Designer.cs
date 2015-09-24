namespace DexComanda
{
    partial class frmCadastrarFormaPagamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastrarFormaPagamento));
            this.FPGridView = new System.Windows.Forms.DataGridView();
            this.btnEditarFP = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.txtNomeFP = new System.Windows.Forms.TextBox();
            this.chkDesconto2 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkFinanceiro = new System.Windows.Forms.CheckBox();
            this.chkOnline = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FPGridView
            // 
            this.FPGridView.AllowUserToAddRows = false;
            this.FPGridView.AllowUserToDeleteRows = false;
            this.FPGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FPGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FPGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FPGridView.Location = new System.Drawing.Point(3, 173);
            this.FPGridView.MultiSelect = false;
            this.FPGridView.Name = "FPGridView";
            this.FPGridView.ReadOnly = true;
            this.FPGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FPGridView.Size = new System.Drawing.Size(260, 184);
            this.FPGridView.TabIndex = 2;
            this.FPGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Excluir);
            // 
            // btnEditarFP
            // 
            this.btnEditarFP.Location = new System.Drawing.Point(132, 119);
            this.btnEditarFP.Name = "btnEditarFP";
            this.btnEditarFP.Size = new System.Drawing.Size(124, 48);
            this.btnEditarFP.TabIndex = 5;
            this.btnEditarFP.Text = "Editar [F11]";
            this.btnEditarFP.UseVisualStyleBackColor = true;
            this.btnEditarFP.Click += new System.EventHandler(this.Editar);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(1, 119);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(108, 48);
            this.btnAdicionar.TabIndex = 4;
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.Adicionar);
            // 
            // txtNomeFP
            // 
            this.txtNomeFP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeFP.Location = new System.Drawing.Point(1, 87);
            this.txtNomeFP.Name = "txtNomeFP";
            this.txtNomeFP.Size = new System.Drawing.Size(235, 26);
            this.txtNomeFP.TabIndex = 6;
            // 
            // chkDesconto2
            // 
            this.chkDesconto2.AutoSize = true;
            this.chkDesconto2.Location = new System.Drawing.Point(3, 39);
            this.chkDesconto2.Name = "chkDesconto2";
            this.chkDesconto2.Size = new System.Drawing.Size(116, 17);
            this.chkDesconto2.TabIndex = 7;
            this.chkDesconto2.Text = "Permite Desconto?";
            this.chkDesconto2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Formas de Pagamento";
            // 
            // chkFinanceiro
            // 
            this.chkFinanceiro.AutoSize = true;
            this.chkFinanceiro.Location = new System.Drawing.Point(129, 39);
            this.chkFinanceiro.Name = "chkFinanceiro";
            this.chkFinanceiro.Size = new System.Drawing.Size(107, 17);
            this.chkFinanceiro.TabIndex = 9;
            this.chkFinanceiro.Text = "Gera Financeiro?";
            this.chkFinanceiro.UseVisualStyleBackColor = true;
            // 
            // chkOnline
            // 
            this.chkOnline.AutoSize = true;
            this.chkOnline.Location = new System.Drawing.Point(3, 62);
            this.chkOnline.Name = "chkOnline";
            this.chkOnline.Size = new System.Drawing.Size(62, 17);
            this.chkOnline.TabIndex = 10;
            this.chkOnline.Text = "Online?";
            this.chkOnline.UseVisualStyleBackColor = true;
            // 
            // frmCadastrarFormaPagamento
            // 
            this.ClientSize = new System.Drawing.Size(272, 369);
            this.Controls.Add(this.chkOnline);
            this.Controls.Add(this.chkFinanceiro);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkDesconto2);
            this.Controls.Add(this.txtNomeFP);
            this.Controls.Add(this.btnEditarFP);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.FPGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCadastrarFormaPagamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[XDelivery]  Formas De Pagamento";
            this.Load += new System.EventHandler(this.frmCadastrarFormaPagamento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Button btnAdicionarFP;
        private System.Windows.Forms.DataGridView formaPgtGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkDesconto;
        private System.Windows.Forms.DataGridView FPGridView;
        private System.Windows.Forms.Button btnEditarFP;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.TextBox txtNomeFP;
        private System.Windows.Forms.CheckBox chkDesconto2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkFinanceiro;
        private System.Windows.Forms.CheckBox chkOnline;
    }
}