namespace DexComanda
{
    partial class frmCadastroRegioes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroRegioes));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEditar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEntrega = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCEP = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RegioesGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RegioesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEditar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtEntrega);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBairro);
            this.groupBox1.Controls.Add(this.btnSalvar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCEP);
            this.groupBox1.Location = new System.Drawing.Point(1, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cadastro ";
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(308, 84);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(86, 40);
            this.btnEditar.TabIndex = 4;
            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.Editar);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Taxa Entrega";
            // 
            // txtEntrega
            // 
            this.txtEntrega.Location = new System.Drawing.Point(201, 41);
            this.txtEntrega.Name = "txtEntrega";
            this.txtEntrega.Size = new System.Drawing.Size(93, 20);
            this.txtEntrega.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Bairro";
            // 
            // txtBairro
            // 
            this.txtBairro.Location = new System.Drawing.Point(14, 84);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(161, 20);
            this.txtBairro.TabIndex = 2;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(308, 25);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(86, 37);
            this.btnSalvar.TabIndex = 3;
            this.btnSalvar.Text = "Adicionar [F12]";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.AdicionarRegiao);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome Região";
            // 
            // txtCEP
            // 
            this.txtCEP.Location = new System.Drawing.Point(11, 41);
            this.txtCEP.Name = "txtCEP";
            this.txtCEP.Size = new System.Drawing.Size(164, 20);
            this.txtCEP.TabIndex = 0;
            this.txtCEP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConsultarCEP);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RegioesGridView);
            this.groupBox2.Location = new System.Drawing.Point(1, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 174);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista de Regiões";
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
            this.RegioesGridView.Size = new System.Drawing.Size(394, 155);
            this.RegioesGridView.TabIndex = 3;
            this.RegioesGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RegioesGridView_CellClick);
            // 
            // frmCadastroRegioes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 334);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmCadastroRegioes";
            this.Text = "[XDelivery] Cadastro de Regiões";
            this.Load += new System.EventHandler(this.frmCadastroRegioes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCadastroRegioes_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RegioesGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEntrega;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCEP;
        private System.Windows.Forms.DataGridView RegioesGridView;
        private System.Windows.Forms.Button btnEditar;
    }
}