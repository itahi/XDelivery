namespace DexComanda
{
    partial class frmAdicionarGrupo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdicionarGrupo));
            this.gruposGridView = new System.Windows.Forms.DataGridView();
            this.btnAdicionarGrupo = new System.Windows.Forms.Button();
            this.txbNomeGrupo = new System.Windows.Forms.TextBox();
            this.btnEditarGrupo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkOnline = new System.Windows.Forms.CheckBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.pnlImpressora = new System.Windows.Forms.Panel();
            this.btnLista = new System.Windows.Forms.Button();
            this.cbxNomeImpressora = new System.Windows.Forms.ComboBox();
            this.chkImprimeCozinha = new System.Windows.Forms.CheckBox();
            this.cbxFamilia = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEditarFamilia = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gruposGridView)).BeginInit();
            this.pnlImpressora.SuspendLayout();
            this.SuspendLayout();
            // 
            // gruposGridView
            // 
            this.gruposGridView.AllowUserToAddRows = false;
            this.gruposGridView.AllowUserToDeleteRows = false;
            this.gruposGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gruposGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gruposGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gruposGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gruposGridView.Location = new System.Drawing.Point(4, 243);
            this.gruposGridView.MultiSelect = false;
            this.gruposGridView.Name = "gruposGridView";
            this.gruposGridView.ReadOnly = true;
            this.gruposGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gruposGridView.Size = new System.Drawing.Size(302, 235);
            this.gruposGridView.TabIndex = 1;
            this.gruposGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gruposGridView_CellClick);
            this.gruposGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gruposGridView_MouseClick_1);
            // 
            // btnAdicionarGrupo
            // 
            this.btnAdicionarGrupo.Location = new System.Drawing.Point(4, 202);
            this.btnAdicionarGrupo.Name = "btnAdicionarGrupo";
            this.btnAdicionarGrupo.Size = new System.Drawing.Size(115, 35);
            this.btnAdicionarGrupo.TabIndex = 2;
            this.btnAdicionarGrupo.Text = "Adicionar [F12]";
            this.btnAdicionarGrupo.UseVisualStyleBackColor = true;
            this.btnAdicionarGrupo.Click += new System.EventHandler(this.AdicionarGrupo);
            // 
            // txbNomeGrupo
            // 
            this.txbNomeGrupo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbNomeGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbNomeGrupo.Location = new System.Drawing.Point(4, 170);
            this.txbNomeGrupo.Name = "txbNomeGrupo";
            this.txbNomeGrupo.Size = new System.Drawing.Size(300, 26);
            this.txbNomeGrupo.TabIndex = 1;
            this.txbNomeGrupo.TextChanged += new System.EventHandler(this.txbNomeGrupo_TextChanged);
            // 
            // btnEditarGrupo
            // 
            this.btnEditarGrupo.Location = new System.Drawing.Point(172, 202);
            this.btnEditarGrupo.Name = "btnEditarGrupo";
            this.btnEditarGrupo.Size = new System.Drawing.Size(131, 35);
            this.btnEditarGrupo.TabIndex = 3;
            this.btnEditarGrupo.Text = "Editar [F11]";
            this.btnEditarGrupo.UseVisualStyleBackColor = true;
            this.btnEditarGrupo.Click += new System.EventHandler(this.EditarGrupo);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nome do Grupo:";
            // 
            // chkOnline
            // 
            this.chkOnline.AutoSize = true;
            this.chkOnline.Location = new System.Drawing.Point(92, 150);
            this.chkOnline.Name = "chkOnline";
            this.chkOnline.Size = new System.Drawing.Size(62, 17);
            this.chkOnline.TabIndex = 11;
            this.chkOnline.Text = "Online?";
            this.chkOnline.UseVisualStyleBackColor = true;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(172, 150);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(56, 17);
            this.chkAtivo.TabIndex = 12;
            this.chkAtivo.Text = "Ativo?";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // pnlImpressora
            // 
            this.pnlImpressora.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlImpressora.Controls.Add(this.btnLista);
            this.pnlImpressora.Controls.Add(this.cbxNomeImpressora);
            this.pnlImpressora.Location = new System.Drawing.Point(4, 36);
            this.pnlImpressora.Name = "pnlImpressora";
            this.pnlImpressora.Size = new System.Drawing.Size(299, 48);
            this.pnlImpressora.TabIndex = 15;
            // 
            // btnLista
            // 
            this.btnLista.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLista.Location = new System.Drawing.Point(206, 13);
            this.btnLista.Name = "btnLista";
            this.btnLista.Size = new System.Drawing.Size(78, 23);
            this.btnLista.TabIndex = 17;
            this.btnLista.Text = "Lista Impre.";
            this.btnLista.UseVisualStyleBackColor = true;
            // 
            // cbxNomeImpressora
            // 
            this.cbxNomeImpressora.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxNomeImpressora.FormattingEnabled = true;
            this.cbxNomeImpressora.Location = new System.Drawing.Point(3, 13);
            this.cbxNomeImpressora.Name = "cbxNomeImpressora";
            this.cbxNomeImpressora.Size = new System.Drawing.Size(197, 21);
            this.cbxNomeImpressora.TabIndex = 16;
            // 
            // chkImprimeCozinha
            // 
            this.chkImprimeCozinha.AutoSize = true;
            this.chkImprimeCozinha.Location = new System.Drawing.Point(4, 13);
            this.chkImprimeCozinha.Name = "chkImprimeCozinha";
            this.chkImprimeCozinha.Size = new System.Drawing.Size(109, 17);
            this.chkImprimeCozinha.TabIndex = 15;
            this.chkImprimeCozinha.Text = "Imprime Cozinha?";
            this.chkImprimeCozinha.UseVisualStyleBackColor = true;
            this.chkImprimeCozinha.CheckedChanged += new System.EventHandler(this.chkImprimeCozinha_CheckedChanged_1);
            // 
            // cbxFamilia
            // 
            this.cbxFamilia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxFamilia.FormattingEnabled = true;
            this.cbxFamilia.Location = new System.Drawing.Point(9, 114);
            this.cbxFamilia.Name = "cbxFamilia";
            this.cbxFamilia.Size = new System.Drawing.Size(197, 21);
            this.cbxFamilia.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Familia";
            // 
            // btnEditarFamilia
            // 
            this.btnEditarFamilia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditarFamilia.Location = new System.Drawing.Point(212, 112);
            this.btnEditarFamilia.Name = "btnEditarFamilia";
            this.btnEditarFamilia.Size = new System.Drawing.Size(91, 23);
            this.btnEditarFamilia.TabIndex = 18;
            this.btnEditarFamilia.Text = "Editar Familia";
            this.btnEditarFamilia.UseVisualStyleBackColor = true;
            this.btnEditarFamilia.Click += new System.EventHandler(this.EditarFamilia);
            // 
            // frmAdicionarGrupo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 493);
            this.Controls.Add(this.btnEditarFamilia);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxFamilia);
            this.Controls.Add(this.pnlImpressora);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.chkImprimeCozinha);
            this.Controls.Add(this.chkOnline);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEditarGrupo);
            this.Controls.Add(this.txbNomeGrupo);
            this.Controls.Add(this.btnAdicionarGrupo);
            this.Controls.Add(this.gruposGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmAdicionarGrupo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[XDelivery] Cadastro Grupos ";
            this.Load += new System.EventHandler(this.Main_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAdicionarGrupo_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gruposGridView)).EndInit();
            this.pnlImpressora.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gruposGridView;
        private System.Windows.Forms.Button btnAdicionarGrupo;
        private System.Windows.Forms.TextBox txbNomeGrupo;
        private System.Windows.Forms.Button btnEditarGrupo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkOnline;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Panel pnlImpressora;
        private System.Windows.Forms.Button btnLista;
        private System.Windows.Forms.ComboBox cbxNomeImpressora;
        private System.Windows.Forms.CheckBox chkImprimeCozinha;
        private System.Windows.Forms.ComboBox cbxFamilia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEditarFamilia;
    }
}