namespace DexComanda.Operações
{
    partial class frmSincronizacao
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.prgBarRegiao = new System.Windows.Forms.ProgressBar();
            this.prgBarpagamento = new System.Windows.Forms.ProgressBar();
            this.prgBarProduto = new System.Windows.Forms.ProgressBar();
            this.prgBarCategoria = new System.Windows.Forms.ProgressBar();
            this.chkRegiaoEntrega = new System.Windows.Forms.CheckBox();
            this.chkFPagamento = new System.Windows.Forms.CheckBox();
            this.chkProdutos = new System.Windows.Forms.CheckBox();
            this.chkCategorias = new System.Windows.Forms.CheckBox();
            this.btnSincronizar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.prgBarRegiao);
            this.groupBox1.Controls.Add(this.prgBarpagamento);
            this.groupBox1.Controls.Add(this.prgBarProduto);
            this.groupBox1.Controls.Add(this.prgBarCategoria);
            this.groupBox1.Controls.Add(this.chkRegiaoEntrega);
            this.groupBox1.Controls.Add(this.chkFPagamento);
            this.groupBox1.Controls.Add(this.chkProdutos);
            this.groupBox1.Controls.Add(this.chkCategorias);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 129);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cadastros a Sincronizar";
            // 
            // prgBarRegiao
            // 
            this.prgBarRegiao.Location = new System.Drawing.Point(129, 90);
            this.prgBarRegiao.Name = "prgBarRegiao";
            this.prgBarRegiao.Size = new System.Drawing.Size(100, 16);
            this.prgBarRegiao.TabIndex = 7;
            // 
            // prgBarpagamento
            // 
            this.prgBarpagamento.Location = new System.Drawing.Point(129, 68);
            this.prgBarpagamento.Name = "prgBarpagamento";
            this.prgBarpagamento.Size = new System.Drawing.Size(100, 16);
            this.prgBarpagamento.TabIndex = 6;
            // 
            // prgBarProduto
            // 
            this.prgBarProduto.Location = new System.Drawing.Point(129, 46);
            this.prgBarProduto.Name = "prgBarProduto";
            this.prgBarProduto.Size = new System.Drawing.Size(100, 16);
            this.prgBarProduto.TabIndex = 5;
            // 
            // prgBarCategoria
            // 
            this.prgBarCategoria.Location = new System.Drawing.Point(129, 19);
            this.prgBarCategoria.Name = "prgBarCategoria";
            this.prgBarCategoria.Size = new System.Drawing.Size(100, 16);
            this.prgBarCategoria.TabIndex = 4;
            // 
            // chkRegiaoEntrega
            // 
            this.chkRegiaoEntrega.AutoSize = true;
            this.chkRegiaoEntrega.Location = new System.Drawing.Point(6, 91);
            this.chkRegiaoEntrega.Name = "chkRegiaoEntrega";
            this.chkRegiaoEntrega.Size = new System.Drawing.Size(105, 17);
            this.chkRegiaoEntrega.TabIndex = 3;
            this.chkRegiaoEntrega.Text = "Regioes Entrega";
            this.chkRegiaoEntrega.UseVisualStyleBackColor = true;
            // 
            // chkFPagamento
            // 
            this.chkFPagamento.AutoSize = true;
            this.chkFPagamento.Location = new System.Drawing.Point(6, 68);
            this.chkFPagamento.Name = "chkFPagamento";
            this.chkFPagamento.Size = new System.Drawing.Size(117, 17);
            this.chkFPagamento.TabIndex = 2;
            this.chkFPagamento.Text = "Forma Pagamentos";
            this.chkFPagamento.UseVisualStyleBackColor = true;
            // 
            // chkProdutos
            // 
            this.chkProdutos.AutoSize = true;
            this.chkProdutos.Location = new System.Drawing.Point(6, 45);
            this.chkProdutos.Name = "chkProdutos";
            this.chkProdutos.Size = new System.Drawing.Size(68, 17);
            this.chkProdutos.TabIndex = 1;
            this.chkProdutos.Text = "Produtos";
            this.chkProdutos.UseVisualStyleBackColor = true;
            // 
            // chkCategorias
            // 
            this.chkCategorias.AutoSize = true;
            this.chkCategorias.Location = new System.Drawing.Point(6, 19);
            this.chkCategorias.Name = "chkCategorias";
            this.chkCategorias.Size = new System.Drawing.Size(76, 17);
            this.chkCategorias.TabIndex = 0;
            this.chkCategorias.Text = "Categorias";
            this.chkCategorias.UseVisualStyleBackColor = true;
            // 
            // btnSincronizar
            // 
            this.btnSincronizar.Location = new System.Drawing.Point(75, 138);
            this.btnSincronizar.Name = "btnSincronizar";
            this.btnSincronizar.Size = new System.Drawing.Size(75, 23);
            this.btnSincronizar.TabIndex = 1;
            this.btnSincronizar.Text = "Sincronizar";
            this.btnSincronizar.UseVisualStyleBackColor = true;
            this.btnSincronizar.Click += new System.EventHandler(this.Sincroniza);
            // 
            // frmSincronizacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 173);
            this.Controls.Add(this.btnSincronizar);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmSincronizacao";
            this.Text = "[XSistemas] Sincronizacao";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRegiaoEntrega;
        private System.Windows.Forms.CheckBox chkFPagamento;
        private System.Windows.Forms.CheckBox chkProdutos;
        private System.Windows.Forms.CheckBox chkCategorias;
        private System.Windows.Forms.ProgressBar prgBarRegiao;
        private System.Windows.Forms.ProgressBar prgBarpagamento;
        private System.Windows.Forms.ProgressBar prgBarProduto;
        private System.Windows.Forms.ProgressBar prgBarCategoria;
        private System.Windows.Forms.Button btnSincronizar;

    }
}