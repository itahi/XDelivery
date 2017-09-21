namespace DexComanda.Cadastros.Pedido
{
    partial class frmProdutosTrocaFidelidade
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
            this.produtoGridView = new System.Windows.Forms.DataGridView();
            this.Selecionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConfirma = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblSaldo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.produtoGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // produtoGridView
            // 
            this.produtoGridView.AllowUserToAddRows = false;
            this.produtoGridView.AllowUserToDeleteRows = false;
            this.produtoGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.produtoGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.produtoGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.produtoGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selecionar});
            this.produtoGridView.Location = new System.Drawing.Point(2, 32);
            this.produtoGridView.MultiSelect = false;
            this.produtoGridView.Name = "produtoGridView";
            this.produtoGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.produtoGridView.Size = new System.Drawing.Size(335, 193);
            this.produtoGridView.TabIndex = 2;
            // 
            // Selecionar
            // 
            this.Selecionar.HeaderText = "Selecionar";
            this.Selecionar.IndeterminateValue = "False";
            this.Selecionar.Name = "Selecionar";
            this.Selecionar.ToolTipText = "Marque os produtos que deseja trocar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(329, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lista de produtos disponiveis para troca";
            // 
            // btnConfirma
            // 
            this.btnConfirma.Location = new System.Drawing.Point(151, 238);
            this.btnConfirma.Name = "btnConfirma";
            this.btnConfirma.Size = new System.Drawing.Size(90, 33);
            this.btnConfirma.TabIndex = 4;
            this.btnConfirma.Text = "Confirmar";
            this.btnConfirma.UseVisualStyleBackColor = true;
            this.btnConfirma.Click += new System.EventHandler(this.btnConfirma_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnCancelar.Location = new System.Drawing.Point(247, 238);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 33);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldo.ForeColor = System.Drawing.Color.Crimson;
            this.lblSaldo.Location = new System.Drawing.Point(1, 245);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(101, 16);
            this.lblSaldo.TabIndex = 6;
            this.lblSaldo.Text = "Saldo Pontos";
            // 
            // frmProdutosTrocaFidelidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 282);
            this.Controls.Add(this.lblSaldo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirma);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.produtoGridView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProdutosTrocaFidelidade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[xSistemas] Produtos para troca";
            this.Load += new System.EventHandler(this.frmProdutosTrocaFidelidade_Load);
            ((System.ComponentModel.ISupportInitialize)(this.produtoGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView produtoGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConfirma;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selecionar;
        private System.Windows.Forms.Label lblSaldo;
    }
}