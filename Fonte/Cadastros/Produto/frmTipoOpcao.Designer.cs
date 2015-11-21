namespace DexComanda.Cadastros.Produto
{
    partial class frmTipoOpcao
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.txbNomeGrupo = new System.Windows.Forms.TextBox();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.GridView = new System.Windows.Forms.DataGridView();
            this.chkAtivoSN = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Nome";
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(173, 57);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(130, 35);
            this.btnEditar.TabIndex = 9;
            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.UseVisualStyleBackColor = true;
            // 
            // txbNomeGrupo
            // 
            this.txbNomeGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbNomeGrupo.Location = new System.Drawing.Point(3, 25);
            this.txbNomeGrupo.Name = "txbNomeGrupo";
            this.txbNomeGrupo.Size = new System.Drawing.Size(300, 26);
            this.txbNomeGrupo.TabIndex = 6;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(3, 57);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(114, 35);
            this.btnAdicionar.TabIndex = 8;
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.AllowUserToDeleteRows = false;
            this.GridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView.Location = new System.Drawing.Point(3, 95);
            this.GridView.MultiSelect = false;
            this.GridView.Name = "GridView";
            this.GridView.ReadOnly = true;
            this.GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridView.Size = new System.Drawing.Size(294, 276);
            this.GridView.TabIndex = 7;
            // 
            // chkAtivoSN
            // 
            this.chkAtivoSN.AutoSize = true;
            this.chkAtivoSN.Location = new System.Drawing.Point(178, 2);
            this.chkAtivoSN.Name = "chkAtivoSN";
            this.chkAtivoSN.Size = new System.Drawing.Size(56, 17);
            this.chkAtivoSN.TabIndex = 11;
            this.chkAtivoSN.Text = "Ativo?";
            this.chkAtivoSN.UseVisualStyleBackColor = true;
            // 
            // frmTipoOpcao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 383);
            this.Controls.Add(this.chkAtivoSN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.txbNomeGrupo);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.GridView);
            this.Name = "frmTipoOpcao";
            this.Text = "[xDelivery] Tipo Opção";
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.TextBox txbNomeGrupo;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.CheckBox chkAtivoSN;
        private System.Windows.Forms.DataGridView GridView;
    }
}