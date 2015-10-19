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
            this.chkImprimeCozinha = new System.Windows.Forms.CheckBox();
            this.chkOnline = new System.Windows.Forms.CheckBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gruposGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // gruposGridView
            // 
            this.gruposGridView.AllowUserToAddRows = false;
            this.gruposGridView.AllowUserToDeleteRows = false;
            this.gruposGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gruposGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gruposGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gruposGridView.Location = new System.Drawing.Point(4, 118);
            this.gruposGridView.MultiSelect = false;
            this.gruposGridView.Name = "gruposGridView";
            this.gruposGridView.ReadOnly = true;
            this.gruposGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gruposGridView.Size = new System.Drawing.Size(300, 196);
            this.gruposGridView.TabIndex = 1;
            this.gruposGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gruposGridView_CellClick);
            this.gruposGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gruposGridView_MouseClick_1);
            // 
            // btnAdicionarGrupo
            // 
            this.btnAdicionarGrupo.Location = new System.Drawing.Point(4, 80);
            this.btnAdicionarGrupo.Name = "btnAdicionarGrupo";
            this.btnAdicionarGrupo.Size = new System.Drawing.Size(114, 35);
            this.btnAdicionarGrupo.TabIndex = 2;
            this.btnAdicionarGrupo.Text = "Adicionar [F12]";
            this.btnAdicionarGrupo.UseVisualStyleBackColor = true;
            this.btnAdicionarGrupo.Click += new System.EventHandler(this.AdicionarGrupo);
            // 
            // txbNomeGrupo
            // 
            this.txbNomeGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbNomeGrupo.Location = new System.Drawing.Point(4, 48);
            this.txbNomeGrupo.Name = "txbNomeGrupo";
            this.txbNomeGrupo.Size = new System.Drawing.Size(300, 26);
            this.txbNomeGrupo.TabIndex = 1;
            this.txbNomeGrupo.TextChanged += new System.EventHandler(this.txbNomeGrupo_TextChanged);
            // 
            // btnEditarGrupo
            // 
            this.btnEditarGrupo.Location = new System.Drawing.Point(174, 80);
            this.btnEditarGrupo.Name = "btnEditarGrupo";
            this.btnEditarGrupo.Size = new System.Drawing.Size(130, 35);
            this.btnEditarGrupo.TabIndex = 3;
            this.btnEditarGrupo.Text = "Editar [F11]";
            this.btnEditarGrupo.UseVisualStyleBackColor = true;
            this.btnEditarGrupo.Click += new System.EventHandler(this.EditarGrupo);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nome do Grupo:";
            // 
            // chkImprimeCozinha
            // 
            this.chkImprimeCozinha.AutoSize = true;
            this.chkImprimeCozinha.Location = new System.Drawing.Point(97, 3);
            this.chkImprimeCozinha.Name = "chkImprimeCozinha";
            this.chkImprimeCozinha.Size = new System.Drawing.Size(109, 17);
            this.chkImprimeCozinha.TabIndex = 6;
            this.chkImprimeCozinha.Text = "Imprime Cozinha?";
            this.chkImprimeCozinha.UseVisualStyleBackColor = true;
            // 
            // chkOnline
            // 
            this.chkOnline.AutoSize = true;
            this.chkOnline.Location = new System.Drawing.Point(212, 3);
            this.chkOnline.Name = "chkOnline";
            this.chkOnline.Size = new System.Drawing.Size(62, 17);
            this.chkOnline.TabIndex = 11;
            this.chkOnline.Text = "Online?";
            this.chkOnline.UseVisualStyleBackColor = true;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(212, 27);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(56, 17);
            this.chkAtivo.TabIndex = 12;
            this.chkAtivo.Text = "Ativo?";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // frmAdicionarGrupo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 319);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.chkOnline);
            this.Controls.Add(this.chkImprimeCozinha);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEditarGrupo);
            this.Controls.Add(this.txbNomeGrupo);
            this.Controls.Add(this.btnAdicionarGrupo);
            this.Controls.Add(this.gruposGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmAdicionarGrupo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[XDelivery] Cadastro Grupos ";
            this.Load += new System.EventHandler(this.Main_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAdicionarGrupo_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gruposGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gruposGridView;
        private System.Windows.Forms.Button btnAdicionarGrupo;
        private System.Windows.Forms.TextBox txbNomeGrupo;
        private System.Windows.Forms.Button btnEditarGrupo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkImprimeCozinha;
        private System.Windows.Forms.CheckBox chkOnline;
        private System.Windows.Forms.CheckBox chkAtivo;
    }
}