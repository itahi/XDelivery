namespace DexComanda.Cadastros
{
    partial class frmBairrosRegiao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBairrosRegiao));
            this.cbxRegiao = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCEP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnConsultaCEp = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.RegioesGridView = new System.Windows.Forms.DataGridView();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.chkOnlineSN = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2.SuspendLayout();
            this.pnConsultaCEp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RegioesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxRegiao
            // 
            this.cbxRegiao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRegiao.FormattingEnabled = true;
            this.cbxRegiao.Location = new System.Drawing.Point(4, 25);
            this.cbxRegiao.Name = "cbxRegiao";
            this.cbxRegiao.Size = new System.Drawing.Size(221, 21);
            this.cbxRegiao.TabIndex = 0;
            this.cbxRegiao.SelectionChangeCommitted += new System.EventHandler(this.ListaBairrosRegiao);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Região De Entrega";
            // 
            // txtCEP
            // 
            this.txtCEP.Location = new System.Drawing.Point(4, 65);
            this.txtCEP.MaxLength = 9;
            this.txtCEP.Name = "txtCEP";
            this.txtCEP.Size = new System.Drawing.Size(80, 20);
            this.txtCEP.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtCEP, "Preencha o CEP para buscar o bairro");
            this.txtCEP.TextChanged += new System.EventHandler(this.txtCEP_TextChanged);
            this.txtCEP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCEP_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "CEP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Bairro";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pnConsultaCEp);
            this.groupBox2.Controls.Add(this.RegioesGridView);
            this.groupBox2.Location = new System.Drawing.Point(1, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 198);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bairros";
            // 
            // pnConsultaCEp
            // 
            this.pnConsultaCEp.Controls.Add(this.label26);
            this.pnConsultaCEp.Location = new System.Drawing.Point(3, 16);
            this.pnConsultaCEp.Name = "pnConsultaCEp";
            this.pnConsultaCEp.Size = new System.Drawing.Size(283, 65);
            this.pnConsultaCEp.TabIndex = 62;
            this.pnConsultaCEp.Visible = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(49, 16);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(189, 32);
            this.label26.TabIndex = 0;
            this.label26.Text = "Aguarde consultando CEP\r\n na Base dos correios ...";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RegioesGridView
            // 
            this.RegioesGridView.AllowUserToAddRows = false;
            this.RegioesGridView.AllowUserToDeleteRows = false;
            this.RegioesGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RegioesGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RegioesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RegioesGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RegioesGridView.Location = new System.Drawing.Point(3, 16);
            this.RegioesGridView.MultiSelect = false;
            this.RegioesGridView.Name = "RegioesGridView";
            this.RegioesGridView.ReadOnly = true;
            this.RegioesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RegioesGridView.Size = new System.Drawing.Size(285, 179);
            this.RegioesGridView.TabIndex = 3;
            this.RegioesGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RegioesGridView_CellClick);
            this.RegioesGridView.DoubleClick += new System.EventHandler(this.RegioesGridView_DoubleClick);
            this.RegioesGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuAuxiliar);
            // 
            // btnEditar
            // 
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(107, 130);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(74, 24);
            this.btnEditar.TabIndex = 32;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.Editar);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Image = ((System.Drawing.Image)(resources.GetObject("btnAdicionar.Image")));
            this.btnAdicionar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionar.Location = new System.Drawing.Point(4, 130);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(74, 24);
            this.btnAdicionar.TabIndex = 31;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.Adicionar);
            // 
            // txtBairro
            // 
            this.txtBairro.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtBairro.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtBairro.Location = new System.Drawing.Point(4, 104);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(177, 20);
            this.txtBairro.TabIndex = 35;
            this.toolTip1.SetToolTip(this.txtBairro, "d");
            this.txtBairro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RetornaCEP);
            this.txtBairro.Leave += new System.EventHandler(this.txtBairro_Leave);
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(193, 61);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(71, 17);
            this.chkAtivo.TabIndex = 36;
            this.chkAtivo.Text = "AtivoSN?";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // chkOnlineSN
            // 
            this.chkOnlineSN.AutoSize = true;
            this.chkOnlineSN.Location = new System.Drawing.Point(193, 84);
            this.chkOnlineSN.Name = "chkOnlineSN";
            this.chkOnlineSN.Size = new System.Drawing.Size(77, 17);
            this.chkOnlineSN.TabIndex = 37;
            this.chkOnlineSN.Text = "OnlineSN?";
            this.chkOnlineSN.UseVisualStyleBackColor = true;
            // 
            // frmBairrosRegiao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 363);
            this.Controls.Add(this.chkOnlineSN);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.txtBairro);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCEP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxRegiao);
            this.Name = "frmBairrosRegiao";
            this.Text = "[xDelivery] Cadastro de Bairros ";
            this.Load += new System.EventHandler(this.frmBairrosRegiao_Load);
            this.groupBox2.ResumeLayout(false);
            this.pnConsultaCEp.ResumeLayout(false);
            this.pnConsultaCEp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RegioesGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxRegiao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCEP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView RegioesGridView;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.CheckBox chkOnlineSN;
        private System.Windows.Forms.Panel pnConsultaCEp;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}