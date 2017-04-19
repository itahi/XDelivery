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
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.produtoGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // produtoGridView
            // 
            this.produtoGridView.AllowUserToAddRows = false;
            this.produtoGridView.AllowUserToDeleteRows = false;
            this.produtoGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.produtoGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.produtoGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.produtoGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.produtoGridView.Location = new System.Drawing.Point(2, 69);
            this.produtoGridView.MultiSelect = false;
            this.produtoGridView.Name = "produtoGridView";
            this.produtoGridView.ReadOnly = true;
            this.produtoGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.produtoGridView.Size = new System.Drawing.Size(335, 235);
            this.produtoGridView.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(329, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lista de produtos disponiveis para troca";
            // 
            // frmProdutosTrocaFidelidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 308);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.produtoGridView);
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
    }
}