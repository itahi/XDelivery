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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.prgBarCategoria = new System.Windows.Forms.ProgressBar();
            this.prgBarProduto = new System.Windows.Forms.ProgressBar();
            this.prgBarpagamento = new System.Windows.Forms.ProgressBar();
            this.prgBarRegiao = new System.Windows.Forms.ProgressBar();
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
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 129);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cadastros a Sincronizar";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(76, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Categorias";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 45);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(68, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Produtos";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(6, 68);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(117, 17);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "Forma Pagamentos";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(6, 91);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(105, 17);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "Regioes Entrega";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // prgBarCategoria
            // 
            this.prgBarCategoria.Location = new System.Drawing.Point(129, 19);
            this.prgBarCategoria.Name = "prgBarCategoria";
            this.prgBarCategoria.Size = new System.Drawing.Size(100, 16);
            this.prgBarCategoria.TabIndex = 4;
            // 
            // prgBarProduto
            // 
            this.prgBarProduto.Location = new System.Drawing.Point(129, 46);
            this.prgBarProduto.Name = "prgBarProduto";
            this.prgBarProduto.Size = new System.Drawing.Size(100, 16);
            this.prgBarProduto.TabIndex = 5;
            // 
            // prgBarpagamento
            // 
            this.prgBarpagamento.Location = new System.Drawing.Point(129, 68);
            this.prgBarpagamento.Name = "prgBarpagamento";
            this.prgBarpagamento.Size = new System.Drawing.Size(100, 16);
            this.prgBarpagamento.TabIndex = 6;
            // 
            // prgBarRegiao
            // 
            this.prgBarRegiao.Location = new System.Drawing.Point(129, 90);
            this.prgBarRegiao.Name = "prgBarRegiao";
            this.prgBarRegiao.Size = new System.Drawing.Size(100, 16);
            this.prgBarRegiao.TabIndex = 7;
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
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ProgressBar prgBarRegiao;
        private System.Windows.Forms.ProgressBar prgBarpagamento;
        private System.Windows.Forms.ProgressBar prgBarProduto;
        private System.Windows.Forms.ProgressBar prgBarCategoria;
        private System.Windows.Forms.Button btnSincronizar;

    }
}