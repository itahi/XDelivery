namespace DexComanda.Cadastros
{
    partial class frmCadastroStatus
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
            this.StatusGridView = new System.Windows.Forms.DataGridView();
            this.chkAlertar = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.txbNome = new System.Windows.Forms.TextBox();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.cbxOrder = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.StatusGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusGridView
            // 
            this.StatusGridView.AllowUserToAddRows = false;
            this.StatusGridView.AllowUserToDeleteRows = false;
            this.StatusGridView.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.StatusGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.StatusGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.StatusGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StatusGridView.Location = new System.Drawing.Point(5, 149);
            this.StatusGridView.MultiSelect = false;
            this.StatusGridView.Name = "StatusGridView";
            this.StatusGridView.ReadOnly = true;
            this.StatusGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.StatusGridView.Size = new System.Drawing.Size(304, 196);
            this.StatusGridView.TabIndex = 2;
            this.StatusGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StatusGridView_CellClick);
            // 
            // chkAlertar
            // 
            this.chkAlertar.AutoSize = true;
            this.chkAlertar.Location = new System.Drawing.Point(169, 47);
            this.chkAlertar.Name = "chkAlertar";
            this.chkAlertar.Size = new System.Drawing.Size(121, 17);
            this.chkAlertar.TabIndex = 2;
            this.chkAlertar.Text = "Enviar Alerta Cliente";
            this.chkAlertar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Nome";
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(179, 108);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(130, 35);
            this.btnEditar.TabIndex = 4;
            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditarGrupo_Click);
            // 
            // txbNome
            // 
            this.txbNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbNome.Location = new System.Drawing.Point(8, 76);
            this.txbNome.Name = "txbNome";
            this.txbNome.Size = new System.Drawing.Size(301, 26);
            this.txbNome.TabIndex = 0;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(5, 108);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(149, 35);
            this.btnAdicionar.TabIndex = 3;
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionarGrupo_Click);
            // 
            // cbxOrder
            // 
            this.cbxOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOrder.FormattingEnabled = true;
            this.cbxOrder.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbxOrder.Location = new System.Drawing.Point(56, 43);
            this.cbxOrder.Name = "cbxOrder";
            this.cbxOrder.Size = new System.Drawing.Size(42, 21);
            this.cbxOrder.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(53, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Ordem Exibição";
            // 
            // frmCadastroStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 377);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxOrder);
            this.Controls.Add(this.chkAlertar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.txbNome);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.StatusGridView);
            this.KeyPreview = true;
            this.Name = "frmCadastroStatus";
            this.Text = "[xDelivery] Cadastro Status";
            this.Load += new System.EventHandler(this.frmCadastroStatus_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCadastroStatus_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.StatusGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView StatusGridView;
        private System.Windows.Forms.CheckBox chkAlertar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.TextBox txbNome;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.ComboBox cbxOrder;
        private System.Windows.Forms.Label label2;
    }
}