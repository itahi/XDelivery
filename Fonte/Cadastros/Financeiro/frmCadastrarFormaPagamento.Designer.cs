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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastrarFormaPagamento));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkAtivoSN = new System.Windows.Forms.CheckBox();
            this.chkOnline = new System.Windows.Forms.CheckBox();
            this.chkFinanceiro = new System.Windows.Forms.CheckBox();
            this.chkDesconto2 = new System.Windows.Forms.CheckBox();
            this.txtNomeFP = new System.Windows.Forms.TextBox();
            this.btnEditarFP = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.FPGridView = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.btnImg = new System.Windows.Forms.Button();
            this.txtcaminhoImage = new System.Windows.Forms.TextBox();
            this.img = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPGridView)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(276, 389);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkAtivoSN);
            this.tabPage1.Controls.Add(this.chkOnline);
            this.tabPage1.Controls.Add(this.chkFinanceiro);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.chkDesconto2);
            this.tabPage1.Controls.Add(this.txtNomeFP);
            this.tabPage1.Controls.Add(this.btnEditarFP);
            this.tabPage1.Controls.Add(this.btnAdicionar);
            this.tabPage1.Controls.Add(this.FPGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(268, 363);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cadastro";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkAtivoSN
            // 
            this.chkAtivoSN.AutoSize = true;
            this.chkAtivoSN.Location = new System.Drawing.Point(128, 41);
            this.chkAtivoSN.Name = "chkAtivoSN";
            this.chkAtivoSN.Size = new System.Drawing.Size(71, 17);
            this.chkAtivoSN.TabIndex = 19;
            this.chkAtivoSN.Text = "AtivoSN?";
            this.chkAtivoSN.UseVisualStyleBackColor = true;
            // 
            // chkOnline
            // 
            this.chkOnline.AutoSize = true;
            this.chkOnline.Location = new System.Drawing.Point(8, 41);
            this.chkOnline.Name = "chkOnline";
            this.chkOnline.Size = new System.Drawing.Size(62, 17);
            this.chkOnline.TabIndex = 18;
            this.chkOnline.Text = "Online?";
            this.chkOnline.UseVisualStyleBackColor = true;
            // 
            // chkFinanceiro
            // 
            this.chkFinanceiro.AutoSize = true;
            this.chkFinanceiro.Location = new System.Drawing.Point(128, 7);
            this.chkFinanceiro.Name = "chkFinanceiro";
            this.chkFinanceiro.Size = new System.Drawing.Size(107, 17);
            this.chkFinanceiro.TabIndex = 17;
            this.chkFinanceiro.Text = "Gera Financeiro?";
            this.toolTip1.SetToolTip(this.chkFinanceiro, "Marcando essa opção , vendas feitas nessa forma de pagamento geram débitos para o" +
        " cliente");
            this.chkFinanceiro.UseVisualStyleBackColor = true;
            // 
            // chkDesconto2
            // 
            this.chkDesconto2.AutoSize = true;
            this.chkDesconto2.Location = new System.Drawing.Point(8, 7);
            this.chkDesconto2.Name = "chkDesconto2";
            this.chkDesconto2.Size = new System.Drawing.Size(116, 17);
            this.chkDesconto2.TabIndex = 15;
            this.chkDesconto2.Text = "Permite Desconto?";
            this.chkDesconto2.UseVisualStyleBackColor = true;
            // 
            // txtNomeFP
            // 
            this.txtNomeFP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNomeFP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeFP.Location = new System.Drawing.Point(6, 92);
            this.txtNomeFP.Name = "txtNomeFP";
            this.txtNomeFP.Size = new System.Drawing.Size(252, 26);
            this.txtNomeFP.TabIndex = 14;
            // 
            // btnEditarFP
            // 
            this.btnEditarFP.Location = new System.Drawing.Point(136, 124);
            this.btnEditarFP.Name = "btnEditarFP";
            this.btnEditarFP.Size = new System.Drawing.Size(124, 48);
            this.btnEditarFP.TabIndex = 13;
            this.btnEditarFP.Text = "Editar [F11]";
            this.btnEditarFP.UseVisualStyleBackColor = true;
            this.btnEditarFP.Click += new System.EventHandler(this.btnEditarFP_Click);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(3, 124);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(108, 48);
            this.btnAdicionar.TabIndex = 12;
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // FPGridView
            // 
            this.FPGridView.AllowUserToAddRows = false;
            this.FPGridView.AllowUserToDeleteRows = false;
            this.FPGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FPGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FPGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FPGridView.Location = new System.Drawing.Point(5, 176);
            this.FPGridView.MultiSelect = false;
            this.FPGridView.Name = "FPGridView";
            this.FPGridView.ReadOnly = true;
            this.FPGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FPGridView.Size = new System.Drawing.Size(257, 181);
            this.FPGridView.TabIndex = 11;
            this.FPGridView.DoubleClick += new System.EventHandler(this.btnEditarFP_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.btnImg);
            this.tabPage2.Controls.Add(this.txtcaminhoImage);
            this.tabPage2.Controls.Add(this.img);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(268, 341);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Imagen";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(136, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Limpar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnImg
            // 
            this.btnImg.Location = new System.Drawing.Point(75, 53);
            this.btnImg.Name = "btnImg";
            this.btnImg.Size = new System.Drawing.Size(55, 23);
            this.btnImg.TabIndex = 18;
            this.btnImg.Text = "Buscar";
            this.btnImg.UseVisualStyleBackColor = true;
            this.btnImg.Click += new System.EventHandler(this.SelecionarImagem);
            // 
            // txtcaminhoImage
            // 
            this.txtcaminhoImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcaminhoImage.Location = new System.Drawing.Point(10, 21);
            this.txtcaminhoImage.Name = "txtcaminhoImage";
            this.txtcaminhoImage.Size = new System.Drawing.Size(242, 20);
            this.txtcaminhoImage.TabIndex = 1;
            this.txtcaminhoImage.TextChanged += new System.EventHandler(this.txtcaminhoImage_TextChanged);
            // 
            // img
            // 
            this.img.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.img.Location = new System.Drawing.Point(6, 82);
            this.img.Name = "img";
            this.img.Size = new System.Drawing.Size(259, 178);
            this.img.TabIndex = 0;
            this.img.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Nome:";
            // 
            // frmCadastrarFormaPagamento
            // 
            this.ClientSize = new System.Drawing.Size(283, 400);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCadastrarFormaPagamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[XDelivery]  Formas De Pagamento";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCadastrarFormaPagamento_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPGridView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Button btnAdicionarFP;
        private System.Windows.Forms.DataGridView formaPgtGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkDesconto;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox chkOnline;
        private System.Windows.Forms.CheckBox chkFinanceiro;
        private System.Windows.Forms.CheckBox chkDesconto2;
        private System.Windows.Forms.TextBox txtNomeFP;
        private System.Windows.Forms.Button btnEditarFP;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.DataGridView FPGridView;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtcaminhoImage;
        private System.Windows.Forms.PictureBox img;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnImg;
        private System.Windows.Forms.CheckBox chkAtivoSN;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
    }
}