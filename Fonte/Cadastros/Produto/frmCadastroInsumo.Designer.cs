namespace DexComanda.Cadastros.Produto
{
    partial class frmCadastroInsumo
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
            this.txtNome = new System.Windows.Forms.TextBox();
            this.chkAtivoSN = new System.Windows.Forms.CheckBox();
            this.cbxUndMedida = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.registrosGridView = new System.Windows.Forms.DataGridView();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.registrosGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(12, 39);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(293, 20);
            this.txtNome.TabIndex = 0;
            this.toolTip1.SetToolTip(this.txtNome, "Nome do insumo ");
            // 
            // chkAtivoSN
            // 
            this.chkAtivoSN.AutoSize = true;
            this.chkAtivoSN.Location = new System.Drawing.Point(159, 12);
            this.chkAtivoSN.Name = "chkAtivoSN";
            this.chkAtivoSN.Size = new System.Drawing.Size(65, 17);
            this.chkAtivoSN.TabIndex = 1;
            this.chkAtivoSN.Text = "AtivoSN";
            this.chkAtivoSN.UseVisualStyleBackColor = true;
            // 
            // cbxUndMedida
            // 
            this.cbxUndMedida.FormattingEnabled = true;
            this.cbxUndMedida.Items.AddRange(new object[] {
            "UND",
            "KG",
            "LT",
            "PC",
            "CX"});
            this.cbxUndMedida.Location = new System.Drawing.Point(12, 87);
            this.cbxUndMedida.Name = "cbxUndMedida";
            this.cbxUndMedida.Size = new System.Drawing.Size(77, 21);
            this.cbxUndMedida.TabIndex = 2;
            this.toolTip1.SetToolTip(this.cbxUndMedida, "Tipo de medida que compra o insumo");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nome";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Unidade medida";
            // 
            // registrosGridView
            // 
            this.registrosGridView.AllowUserToAddRows = false;
            this.registrosGridView.AllowUserToDeleteRows = false;
            this.registrosGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.registrosGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.registrosGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.registrosGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.registrosGridView.Location = new System.Drawing.Point(12, 169);
            this.registrosGridView.MultiSelect = false;
            this.registrosGridView.Name = "registrosGridView";
            this.registrosGridView.ReadOnly = true;
            this.registrosGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.registrosGridView.Size = new System.Drawing.Size(295, 201);
            this.registrosGridView.TabIndex = 10;
            this.registrosGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuAuxiliar);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(180, 128);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(127, 35);
            this.btnEditar.TabIndex = 4;
            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(12, 128);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(115, 35);
            this.btnAdicionar.TabIndex = 3;
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.AdicionarRegistro);
            // 
            // frmCadastroInsumo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 382);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.registrosGridView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxUndMedida);
            this.Controls.Add(this.chkAtivoSN);
            this.Controls.Add(this.txtNome);
            this.KeyPreview = true;
            this.Name = "frmCadastroInsumo";
            this.Text = "[xSistemas] Cadastro Insumo";
            this.Load += new System.EventHandler(this.frmCadastroInsumo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCadastroInsumo_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.registrosGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.CheckBox chkAtivoSN;
        private System.Windows.Forms.ComboBox cbxUndMedida;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView registrosGridView;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}