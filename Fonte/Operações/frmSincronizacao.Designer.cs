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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblMinimo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVlrMinimo = new System.Windows.Forms.TextBox();
            this.lblSinc = new System.Windows.Forms.Label();
            this.prgBarPrevisao = new System.Windows.Forms.ProgressBar();
            this.chkPrevisao = new System.Windows.Forms.CheckBox();
            this.btnSincronizar = new System.Windows.Forms.Button();
            this.prgBarRegiao = new System.Windows.Forms.ProgressBar();
            this.prgBarpagamento = new System.Windows.Forms.ProgressBar();
            this.prgBarProduto = new System.Windows.Forms.ProgressBar();
            this.chkRegiaoEntrega = new System.Windows.Forms.CheckBox();
            this.chkFPagamento = new System.Windows.Forms.CheckBox();
            this.chkProdutos = new System.Windows.Forms.CheckBox();
            this.lblSincronismo = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.grpBanner = new System.Windows.Forms.GroupBox();
            this.lbRetornoImage = new System.Windows.Forms.Label();
            this.btnImg = new System.Windows.Forms.Button();
            this.txtcaminhoImage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRemover = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbTotal = new System.Windows.Forms.RadioButton();
            this.rbSub = new System.Windows.Forms.RadioButton();
            this.chkCartao = new System.Windows.Forms.CheckBox();
            this.chkDinheiro = new System.Windows.Forms.CheckBox();
            this.lblReturn = new System.Windows.Forms.Label();
            this.chkDesconto = new System.Windows.Forms.CheckBox();
            this.txtPercentualDesconto = new System.Windows.Forms.TextBox();
            this.prgBarMobile = new System.Windows.Forms.ProgressBar();
            this.chkMobile = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.grpBanner.SuspendLayout();
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
            this.tabControl1.Location = new System.Drawing.Point(5, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(278, 266);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.prgBarMobile);
            this.tabPage1.Controls.Add(this.chkMobile);
            this.tabPage1.Controls.Add(this.lblMinimo);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtVlrMinimo);
            this.tabPage1.Controls.Add(this.lblSinc);
            this.tabPage1.Controls.Add(this.prgBarPrevisao);
            this.tabPage1.Controls.Add(this.chkPrevisao);
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
            this.tabPage1.Size = new System.Drawing.Size(270, 240);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cadastros";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblMinimo
            // 
            this.lblMinimo.AutoSize = true;
            this.lblMinimo.Location = new System.Drawing.Point(93, 159);
            this.lblMinimo.Name = "lblMinimo";
            this.lblMinimo.Size = new System.Drawing.Size(74, 13);
            this.lblMinimo.TabIndex = 20;
            this.lblMinimo.Text = "Sincronizando";
            this.lblMinimo.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Pedido Minimo R$";
            this.label2.Visible = false;
            // 
            // txtVlrMinimo
            // 
            this.txtVlrMinimo.Location = new System.Drawing.Point(8, 156);
            this.txtVlrMinimo.Name = "txtVlrMinimo";
            this.txtVlrMinimo.Size = new System.Drawing.Size(75, 20);
            this.txtVlrMinimo.TabIndex = 18;
            this.txtVlrMinimo.Text = "0";
            this.txtVlrMinimo.Visible = false;
            this.txtVlrMinimo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVlrMinimo_KeyPress);
            // 
            // lblSinc
            // 
            this.lblSinc.AutoSize = true;
            this.lblSinc.Location = new System.Drawing.Point(23, 179);
            this.lblSinc.Name = "lblSinc";
            this.lblSinc.Size = new System.Drawing.Size(74, 13);
            this.lblSinc.TabIndex = 17;
            this.lblSinc.Text = "Sincronizando";
            this.lblSinc.Visible = false;
            // 
            // prgBarPrevisao
            // 
            this.prgBarPrevisao.Location = new System.Drawing.Point(128, 78);
            this.prgBarPrevisao.Name = "prgBarPrevisao";
            this.prgBarPrevisao.Size = new System.Drawing.Size(114, 18);
            this.prgBarPrevisao.TabIndex = 16;
            // 
            // chkPrevisao
            // 
            this.chkPrevisao.AutoSize = true;
            this.chkPrevisao.Location = new System.Drawing.Point(5, 79);
            this.chkPrevisao.Name = "chkPrevisao";
            this.chkPrevisao.Size = new System.Drawing.Size(88, 17);
            this.chkPrevisao.TabIndex = 15;
            this.chkPrevisao.Text = "Prev Entrega";
            this.chkPrevisao.UseVisualStyleBackColor = true;
            // 
            // btnSincronizar
            // 
            this.btnSincronizar.Location = new System.Drawing.Point(89, 198);
            this.btnSincronizar.Name = "btnSincronizar";
            this.btnSincronizar.Size = new System.Drawing.Size(75, 23);
            this.btnSincronizar.TabIndex = 14;
            this.btnSincronizar.Text = "Sincronizar";
            this.btnSincronizar.UseVisualStyleBackColor = true;
            this.btnSincronizar.Click += new System.EventHandler(this.btnSincronizar_Click);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.grpBanner);
            this.tabPage2.Controls.Add(this.chkRemover);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(270, 240);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Descontos - Promo";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(84, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "Sinc. Adicionais";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SincAdicionais);
            // 
            // grpBanner
            // 
            this.grpBanner.Controls.Add(this.lbRetornoImage);
            this.grpBanner.Controls.Add(this.btnImg);
            this.grpBanner.Controls.Add(this.txtcaminhoImage);
            this.grpBanner.Controls.Add(this.label1);
            this.grpBanner.Location = new System.Drawing.Point(6, 136);
            this.grpBanner.Name = "grpBanner";
            this.grpBanner.Size = new System.Drawing.Size(258, 71);
            this.grpBanner.TabIndex = 24;
            this.grpBanner.TabStop = false;
            this.grpBanner.Text = "Banner Promocional ";
            // 
            // lbRetornoImage
            // 
            this.lbRetornoImage.AutoSize = true;
            this.lbRetornoImage.Location = new System.Drawing.Point(6, 42);
            this.lbRetornoImage.Name = "lbRetornoImage";
            this.lbRetornoImage.Size = new System.Drawing.Size(23, 13);
            this.lbRetornoImage.TabIndex = 31;
            this.lbRetornoImage.Text = "labl";
            this.lbRetornoImage.Visible = false;
            // 
            // btnImg
            // 
            this.btnImg.Location = new System.Drawing.Point(213, 15);
            this.btnImg.Name = "btnImg";
            this.btnImg.Size = new System.Drawing.Size(39, 23);
            this.btnImg.TabIndex = 29;
            this.btnImg.Text = "Abrir";
            this.btnImg.UseVisualStyleBackColor = true;
            this.btnImg.Click += new System.EventHandler(this.SelecionarImage);
            // 
            // txtcaminhoImage
            // 
            this.txtcaminhoImage.Location = new System.Drawing.Point(5, 17);
            this.txtcaminhoImage.Name = "txtcaminhoImage";
            this.txtcaminhoImage.Size = new System.Drawing.Size(202, 20);
            this.txtcaminhoImage.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-24, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 26;
            // 
            // chkRemover
            // 
            this.chkRemover.AutoSize = true;
            this.chkRemover.Location = new System.Drawing.Point(154, 118);
            this.chkRemover.Name = "chkRemover";
            this.chkRemover.Size = new System.Drawing.Size(106, 17);
            this.chkRemover.TabIndex = 17;
            this.chkRemover.Text = "Remover Banner";
            this.chkRemover.UseVisualStyleBackColor = true;
            this.chkRemover.CheckedChanged += new System.EventHandler(this.chkRemover_CheckedChanged);
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
            // prgBarMobile
            // 
            this.prgBarMobile.Location = new System.Drawing.Point(128, 104);
            this.prgBarMobile.Name = "prgBarMobile";
            this.prgBarMobile.Size = new System.Drawing.Size(114, 18);
            this.prgBarMobile.TabIndex = 22;
            // 
            // chkMobile
            // 
            this.chkMobile.AutoSize = true;
            this.chkMobile.Location = new System.Drawing.Point(5, 105);
            this.chkMobile.Name = "chkMobile";
            this.chkMobile.Size = new System.Drawing.Size(81, 17);
            this.chkMobile.TabIndex = 21;
            this.chkMobile.Text = "APP Mobile";
            this.chkMobile.UseVisualStyleBackColor = true;
            // 
            // frmSincronizacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 268);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmSincronizacao";
            this.Text = "[XSistemas] Sincronizacao";
            this.Load += new System.EventHandler(this.frmSincronizacao_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.grpBanner.ResumeLayout(false);
            this.grpBanner.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbTotal;
        private System.Windows.Forms.RadioButton rbSub;
        private System.Windows.Forms.CheckBox chkCartao;
        private System.Windows.Forms.CheckBox chkDinheiro;
        private System.Windows.Forms.Label lblReturn;
        private System.Windows.Forms.CheckBox chkDesconto;
        private System.Windows.Forms.TextBox txtPercentualDesconto;
        private System.Windows.Forms.GroupBox grpBanner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkRemover;
        private System.Windows.Forms.Label lbRetornoImage;
        private System.Windows.Forms.Button btnImg;
        private System.Windows.Forms.TextBox txtcaminhoImage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar prgBarPrevisao;
        private System.Windows.Forms.CheckBox chkPrevisao;
        private System.Windows.Forms.Label lblSinc;
        private System.Windows.Forms.Label lblMinimo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVlrMinimo;
        private System.Windows.Forms.ProgressBar prgBarMobile;
        private System.Windows.Forms.CheckBox chkMobile;
    }
}