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
            this.chkRegiaoEntrega = new System.Windows.Forms.CheckBox();
            this.chkFPagamento = new System.Windows.Forms.CheckBox();
            this.chkProdutos = new System.Windows.Forms.CheckBox();
            this.btnSincronizar = new System.Windows.Forms.Button();
            this.lblSincronismo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPercentualDesconto = new System.Windows.Forms.TextBox();
            this.chkDesconto = new System.Windows.Forms.CheckBox();
            this.lblReturn = new System.Windows.Forms.Label();
            this.chkDinheiro = new System.Windows.Forms.CheckBox();
            this.chkCartao = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.prgBarRegiao);
            this.groupBox1.Controls.Add(this.prgBarpagamento);
            this.groupBox1.Controls.Add(this.prgBarProduto);
            this.groupBox1.Controls.Add(this.chkRegiaoEntrega);
            this.groupBox1.Controls.Add(this.chkFPagamento);
            this.groupBox1.Controls.Add(this.chkProdutos);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 157);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cadastros a Sincronizar";
            // 
            // prgBarRegiao
            // 
            this.prgBarRegiao.Location = new System.Drawing.Point(129, 64);
            this.prgBarRegiao.Name = "prgBarRegiao";
            this.prgBarRegiao.Size = new System.Drawing.Size(125, 18);
            this.prgBarRegiao.TabIndex = 7;
            // 
            // prgBarpagamento
            // 
            this.prgBarpagamento.Location = new System.Drawing.Point(129, 42);
            this.prgBarpagamento.Name = "prgBarpagamento";
            this.prgBarpagamento.Size = new System.Drawing.Size(125, 16);
            this.prgBarpagamento.TabIndex = 6;
            // 
            // prgBarProduto
            // 
            this.prgBarProduto.Location = new System.Drawing.Point(129, 20);
            this.prgBarProduto.Name = "prgBarProduto";
            this.prgBarProduto.Size = new System.Drawing.Size(125, 16);
            this.prgBarProduto.TabIndex = 5;
            // 
            // chkRegiaoEntrega
            // 
            this.chkRegiaoEntrega.AutoSize = true;
            this.chkRegiaoEntrega.Location = new System.Drawing.Point(6, 65);
            this.chkRegiaoEntrega.Name = "chkRegiaoEntrega";
            this.chkRegiaoEntrega.Size = new System.Drawing.Size(105, 17);
            this.chkRegiaoEntrega.TabIndex = 3;
            this.chkRegiaoEntrega.Text = "Regioes Entrega";
            this.chkRegiaoEntrega.UseVisualStyleBackColor = true;
            // 
            // chkFPagamento
            // 
            this.chkFPagamento.AutoSize = true;
            this.chkFPagamento.Enabled = false;
            this.chkFPagamento.Location = new System.Drawing.Point(6, 42);
            this.chkFPagamento.Name = "chkFPagamento";
            this.chkFPagamento.Size = new System.Drawing.Size(117, 17);
            this.chkFPagamento.TabIndex = 2;
            this.chkFPagamento.Text = "Forma Pagamentos";
            this.chkFPagamento.UseVisualStyleBackColor = true;
            // 
            // chkProdutos
            // 
            this.chkProdutos.AutoSize = true;
            this.chkProdutos.Location = new System.Drawing.Point(6, 19);
            this.chkProdutos.Name = "chkProdutos";
            this.chkProdutos.Size = new System.Drawing.Size(68, 17);
            this.chkProdutos.TabIndex = 1;
            this.chkProdutos.Text = "Produtos";
            this.chkProdutos.UseVisualStyleBackColor = true;
            // 
            // btnSincronizar
            // 
            this.btnSincronizar.Location = new System.Drawing.Point(90, 179);
            this.btnSincronizar.Name = "btnSincronizar";
            this.btnSincronizar.Size = new System.Drawing.Size(75, 23);
            this.btnSincronizar.TabIndex = 1;
            this.btnSincronizar.Text = "Sincronizar";
            this.btnSincronizar.UseVisualStyleBackColor = true;
            this.btnSincronizar.Click += new System.EventHandler(this.Sincroniza);
            // 
            // lblSincronismo
            // 
            this.lblSincronismo.AutoSize = true;
            this.lblSincronismo.Location = new System.Drawing.Point(51, 163);
            this.lblSincronismo.Name = "lblSincronismo";
            this.lblSincronismo.Size = new System.Drawing.Size(23, 13);
            this.lblSincronismo.TabIndex = 2;
            this.lblSincronismo.Text = "labl";
            this.lblSincronismo.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkCartao);
            this.groupBox2.Controls.Add(this.chkDinheiro);
            this.groupBox2.Controls.Add(this.lblReturn);
            this.groupBox2.Controls.Add(this.chkDesconto);
            this.groupBox2.Controls.Add(this.txtPercentualDesconto);
            this.groupBox2.Location = new System.Drawing.Point(6, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 67);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parametros Desconto";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // txtPercentualDesconto
            // 
            this.txtPercentualDesconto.Location = new System.Drawing.Point(123, 17);
            this.txtPercentualDesconto.Name = "txtPercentualDesconto";
            this.txtPercentualDesconto.Size = new System.Drawing.Size(45, 20);
            this.txtPercentualDesconto.TabIndex = 9;
            // 
            // chkDesconto
            // 
            this.chkDesconto.AutoSize = true;
            this.chkDesconto.Location = new System.Drawing.Point(6, 19);
            this.chkDesconto.Name = "chkDesconto";
            this.chkDesconto.Size = new System.Drawing.Size(105, 17);
            this.chkDesconto.TabIndex = 11;
            this.chkDesconto.Text = "Ativa Desconto?";
            this.chkDesconto.UseVisualStyleBackColor = true;
            // 
            // lblReturn
            // 
            this.lblReturn.AutoSize = true;
            this.lblReturn.Location = new System.Drawing.Point(6, 51);
            this.lblReturn.Name = "lblReturn";
            this.lblReturn.Size = new System.Drawing.Size(17, 13);
            this.lblReturn.TabIndex = 12;
            this.lblReturn.Text = "lbl";
            this.lblReturn.Visible = false;
            // 
            // chkDinheiro
            // 
            this.chkDinheiro.AutoSize = true;
            this.chkDinheiro.Location = new System.Drawing.Point(174, 20);
            this.chkDinheiro.Name = "chkDinheiro";
            this.chkDinheiro.Size = new System.Drawing.Size(51, 17);
            this.chkDinheiro.TabIndex = 13;
            this.chkDinheiro.Text = "Dinh.";
            this.chkDinheiro.UseVisualStyleBackColor = true;
            // 
            // chkCartao
            // 
            this.chkCartao.AutoSize = true;
            this.chkCartao.Location = new System.Drawing.Point(174, 47);
            this.chkCartao.Name = "chkCartao";
            this.chkCartao.Size = new System.Drawing.Size(62, 17);
            this.chkCartao.TabIndex = 14;
            this.chkCartao.Text = "Cartões";
            this.chkCartao.UseVisualStyleBackColor = true;
            // 
            // frmSincronizacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 204);
            this.Controls.Add(this.lblSincronismo);
            this.Controls.Add(this.btnSincronizar);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmSincronizacao";
            this.Text = "[XSistemas] Sincronizacao";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRegiaoEntrega;
        private System.Windows.Forms.CheckBox chkFPagamento;
        private System.Windows.Forms.CheckBox chkProdutos;
        private System.Windows.Forms.ProgressBar prgBarRegiao;
        private System.Windows.Forms.ProgressBar prgBarpagamento;
        private System.Windows.Forms.ProgressBar prgBarProduto;
        private System.Windows.Forms.Button btnSincronizar;
        private System.Windows.Forms.Label lblSincronismo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkDesconto;
        private System.Windows.Forms.TextBox txtPercentualDesconto;
        private System.Windows.Forms.Label lblReturn;
        private System.Windows.Forms.CheckBox chkCartao;
        private System.Windows.Forms.CheckBox chkDinheiro;
    }
}