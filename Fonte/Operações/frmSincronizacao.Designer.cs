﻿namespace DexComanda.Operações
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblSincronismo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbTotal = new System.Windows.Forms.RadioButton();
            this.rbSub = new System.Windows.Forms.RadioButton();
            this.chkCartao = new System.Windows.Forms.CheckBox();
            this.chkDinheiro = new System.Windows.Forms.CheckBox();
            this.lblReturn = new System.Windows.Forms.Label();
            this.chkDesconto = new System.Windows.Forms.CheckBox();
            this.txtPercentualDesconto = new System.Windows.Forms.TextBox();
            this.lbRetornoImage = new System.Windows.Forms.Label();
            this.btnImg = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtcaminhoImage = new System.Windows.Forms.TextBox();
            this.prgBarRegiao = new System.Windows.Forms.ProgressBar();
            this.prgBarpagamento = new System.Windows.Forms.ProgressBar();
            this.prgBarProduto = new System.Windows.Forms.ProgressBar();
            this.chkRegiaoEntrega = new System.Windows.Forms.CheckBox();
            this.chkFPagamento = new System.Windows.Forms.CheckBox();
            this.chkProdutos = new System.Windows.Forms.CheckBox();
            this.btnSincronizar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(278, 249);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSincronizar);
            this.tabPage1.Controls.Add(this.prgBarRegiao);
            this.tabPage1.Controls.Add(this.prgBarpagamento);
            this.tabPage1.Controls.Add(this.prgBarProduto);
            this.tabPage1.Controls.Add(this.chkRegiaoEntrega);
            this.tabPage1.Controls.Add(this.chkFPagamento);
            this.tabPage1.Controls.Add(this.chkProdutos);
            this.tabPage1.Controls.Add(this.lblSincronismo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(270, 223);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cadastros";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.lbRetornoImage);
            this.tabPage2.Controls.Add(this.btnImg);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtcaminhoImage);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(270, 223);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Adicionais";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblSincronismo
            // 
            this.lblSincronismo.AutoSize = true;
            this.lblSincronismo.Location = new System.Drawing.Point(42, 288);
            this.lblSincronismo.Name = "lblSincronismo";
            this.lblSincronismo.Size = new System.Drawing.Size(23, 13);
            this.lblSincronismo.TabIndex = 4;
            this.lblSincronismo.Text = "labl";
            this.lblSincronismo.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbTotal);
            this.groupBox2.Controls.Add(this.rbSub);
            this.groupBox2.Controls.Add(this.chkCartao);
            this.groupBox2.Controls.Add(this.chkDinheiro);
            this.groupBox2.Controls.Add(this.lblReturn);
            this.groupBox2.Controls.Add(this.chkDesconto);
            this.groupBox2.Controls.Add(this.txtPercentualDesconto);
            this.groupBox2.Location = new System.Drawing.Point(6, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 113);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parametros Desconto";
            // 
            // rbTotal
            // 
            this.rbTotal.AutoSize = true;
            this.rbTotal.Location = new System.Drawing.Point(123, 83);
            this.rbTotal.Name = "rbTotal";
            this.rbTotal.Size = new System.Drawing.Size(49, 17);
            this.rbTotal.TabIndex = 16;
            this.rbTotal.TabStop = true;
            this.rbTotal.Text = "Total";
            this.rbTotal.UseVisualStyleBackColor = true;
            // 
            // rbSub
            // 
            this.rbSub.AutoSize = true;
            this.rbSub.Checked = true;
            this.rbSub.Location = new System.Drawing.Point(123, 61);
            this.rbSub.Name = "rbSub";
            this.rbSub.Size = new System.Drawing.Size(71, 17);
            this.rbSub.TabIndex = 15;
            this.rbSub.TabStop = true;
            this.rbSub.Text = "Sub Total";
            this.rbSub.UseVisualStyleBackColor = true;
            // 
            // chkCartao
            // 
            this.chkCartao.AutoSize = true;
            this.chkCartao.Location = new System.Drawing.Point(6, 86);
            this.chkCartao.Name = "chkCartao";
            this.chkCartao.Size = new System.Drawing.Size(62, 17);
            this.chkCartao.TabIndex = 14;
            this.chkCartao.Text = "Cartões";
            this.chkCartao.UseVisualStyleBackColor = true;
            // 
            // chkDinheiro
            // 
            this.chkDinheiro.AutoSize = true;
            this.chkDinheiro.Location = new System.Drawing.Point(6, 63);
            this.chkDinheiro.Name = "chkDinheiro";
            this.chkDinheiro.Size = new System.Drawing.Size(65, 17);
            this.chkDinheiro.TabIndex = 13;
            this.chkDinheiro.Text = "Dinheiro";
            this.chkDinheiro.UseVisualStyleBackColor = true;
            // 
            // lblReturn
            // 
            this.lblReturn.AutoSize = true;
            this.lblReturn.Location = new System.Drawing.Point(6, 39);
            this.lblReturn.Name = "lblReturn";
            this.lblReturn.Size = new System.Drawing.Size(17, 13);
            this.lblReturn.TabIndex = 12;
            this.lblReturn.Text = "lbl";
            this.lblReturn.Visible = false;
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
            // txtPercentualDesconto
            // 
            this.txtPercentualDesconto.Location = new System.Drawing.Point(123, 17);
            this.txtPercentualDesconto.Name = "txtPercentualDesconto";
            this.txtPercentualDesconto.Size = new System.Drawing.Size(45, 20);
            this.txtPercentualDesconto.TabIndex = 9;
            // 
            // lbRetornoImage
            // 
            this.lbRetornoImage.AutoSize = true;
            this.lbRetornoImage.Location = new System.Drawing.Point(110, 173);
            this.lbRetornoImage.Name = "lbRetornoImage";
            this.lbRetornoImage.Size = new System.Drawing.Size(23, 13);
            this.lbRetornoImage.TabIndex = 22;
            this.lbRetornoImage.Text = "labl";
            this.lbRetornoImage.Visible = false;
            // 
            // btnImg
            // 
            this.btnImg.Location = new System.Drawing.Point(11, 168);
            this.btnImg.Name = "btnImg";
            this.btnImg.Size = new System.Drawing.Size(75, 23);
            this.btnImg.TabIndex = 19;
            this.btnImg.Text = "Abrir";
            this.btnImg.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Banner Promocional ";
            this.label1.Visible = false;
            // 
            // txtcaminhoImage
            // 
            this.txtcaminhoImage.Location = new System.Drawing.Point(11, 143);
            this.txtcaminhoImage.Name = "txtcaminhoImage";
            this.txtcaminhoImage.Size = new System.Drawing.Size(248, 20);
            this.txtcaminhoImage.TabIndex = 20;
            // 
            // prgBarRegiao
            // 
            this.prgBarRegiao.Location = new System.Drawing.Point(128, 51);
            this.prgBarRegiao.Name = "prgBarRegiao";
            this.prgBarRegiao.Size = new System.Drawing.Size(114, 18);
            this.prgBarRegiao.TabIndex = 13;
            // 
            // prgBarpagamento
            // 
            this.prgBarpagamento.Location = new System.Drawing.Point(128, 29);
            this.prgBarpagamento.Name = "prgBarpagamento";
            this.prgBarpagamento.Size = new System.Drawing.Size(114, 16);
            this.prgBarpagamento.TabIndex = 12;
            // 
            // prgBarProduto
            // 
            this.prgBarProduto.Location = new System.Drawing.Point(128, 7);
            this.prgBarProduto.Name = "prgBarProduto";
            this.prgBarProduto.Size = new System.Drawing.Size(114, 16);
            this.prgBarProduto.TabIndex = 11;
            // 
            // chkRegiaoEntrega
            // 
            this.chkRegiaoEntrega.AutoSize = true;
            this.chkRegiaoEntrega.Location = new System.Drawing.Point(5, 52);
            this.chkRegiaoEntrega.Name = "chkRegiaoEntrega";
            this.chkRegiaoEntrega.Size = new System.Drawing.Size(105, 17);
            this.chkRegiaoEntrega.TabIndex = 10;
            this.chkRegiaoEntrega.Text = "Regioes Entrega";
            this.chkRegiaoEntrega.UseVisualStyleBackColor = true;
            // 
            // chkFPagamento
            // 
            this.chkFPagamento.AutoSize = true;
            this.chkFPagamento.Enabled = false;
            this.chkFPagamento.Location = new System.Drawing.Point(5, 29);
            this.chkFPagamento.Name = "chkFPagamento";
            this.chkFPagamento.Size = new System.Drawing.Size(117, 17);
            this.chkFPagamento.TabIndex = 9;
            this.chkFPagamento.Text = "Forma Pagamentos";
            this.chkFPagamento.UseVisualStyleBackColor = true;
            // 
            // chkProdutos
            // 
            this.chkProdutos.AutoSize = true;
            this.chkProdutos.Location = new System.Drawing.Point(6, 6);
            this.chkProdutos.Name = "chkProdutos";
            this.chkProdutos.Size = new System.Drawing.Size(68, 17);
            this.chkProdutos.TabIndex = 8;
            this.chkProdutos.Text = "Produtos";
            this.chkProdutos.UseVisualStyleBackColor = true;
            // 
            // btnSincronizar
            // 
            this.btnSincronizar.Location = new System.Drawing.Point(90, 194);
            this.btnSincronizar.Name = "btnSincronizar";
            this.btnSincronizar.Size = new System.Drawing.Size(75, 23);
            this.btnSincronizar.TabIndex = 14;
            this.btnSincronizar.Text = "Sincronizar";
            this.btnSincronizar.UseVisualStyleBackColor = true;
            this.btnSincronizar.Click += new System.EventHandler(this.btnSincronizar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(88, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "Sinc. Adicionais";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SincAdicionais);
            // 
            // frmSincronizacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 271);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmSincronizacao";
            this.Text = "[XSistemas] Sincronizacao";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnSincronizar;
        private System.Windows.Forms.ProgressBar prgBarRegiao;
        private System.Windows.Forms.ProgressBar prgBarpagamento;
        private System.Windows.Forms.ProgressBar prgBarProduto;
        private System.Windows.Forms.CheckBox chkRegiaoEntrega;
        private System.Windows.Forms.CheckBox chkFPagamento;
        private System.Windows.Forms.CheckBox chkProdutos;
        private System.Windows.Forms.Label lblSincronismo;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbRetornoImage;
        private System.Windows.Forms.Button btnImg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtcaminhoImage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbTotal;
        private System.Windows.Forms.RadioButton rbSub;
        private System.Windows.Forms.CheckBox chkCartao;
        private System.Windows.Forms.CheckBox chkDinheiro;
        private System.Windows.Forms.Label lblReturn;
        private System.Windows.Forms.CheckBox chkDesconto;
        private System.Windows.Forms.TextBox txtPercentualDesconto;
    }
}