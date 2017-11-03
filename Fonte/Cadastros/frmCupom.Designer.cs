namespace DexComanda.Cadastros
{
    partial class frmCupom
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtQtdPessoa = new System.Windows.Forms.TextBox();
            this.QtdCupom = new System.Windows.Forms.Label();
            this.txtQdtCupom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.CodCupom = new System.Windows.Forms.Label();
            this.txtCodCupom = new System.Windows.Forms.TextBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.CuponGridView = new System.Windows.Forms.DataGridView();
            this.gridViewCupons = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CuponGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCupons)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(326, 412);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnEditar);
            this.tabPage1.Controls.Add(this.btnAdicionar);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.CuponGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(318, 386);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cadastro";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridViewCupons);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(318, 386);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Historico";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(181, 135);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(131, 35);
            this.btnEditar.TabIndex = 12;
            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(4, 135);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(115, 35);
            this.btnAdicionar.TabIndex = 11;
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.AdicionarRegistro);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtQtdPessoa);
            this.groupBox1.Controls.Add(this.QtdCupom);
            this.groupBox1.Controls.Add(this.txtQdtCupom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDesc);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.CodCupom);
            this.groupBox1.Controls.Add(this.txtCodCupom);
            this.groupBox1.Controls.Add(this.chkAtivo);
            this.groupBox1.Location = new System.Drawing.Point(6, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 119);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cadastro";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "QtdPorPessoa";
            // 
            // txtQtdPessoa
            // 
            this.txtQtdPessoa.Location = new System.Drawing.Point(229, 86);
            this.txtQtdPessoa.Name = "txtQtdPessoa";
            this.txtQtdPessoa.Size = new System.Drawing.Size(60, 20);
            this.txtQtdPessoa.TabIndex = 6;
            this.txtQtdPessoa.Text = "1";
            this.toolTip1.SetToolTip(this.txtQtdPessoa, "Quantidade máxima de cupons que o mesmo cliente poderá usar\r\nPreencha 0 caso não " +
        "deseje ter máximo\r\n");
            this.txtQtdPessoa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQtdPessoa_KeyPress);
            // 
            // QtdCupom
            // 
            this.QtdCupom.AutoSize = true;
            this.QtdCupom.Location = new System.Drawing.Point(147, 70);
            this.QtdCupom.Name = "QtdCupom";
            this.QtdCupom.Size = new System.Drawing.Size(57, 13);
            this.QtdCupom.TabIndex = 9;
            this.QtdCupom.Text = "QtdCupom";
            // 
            // txtQdtCupom
            // 
            this.txtQdtCupom.Location = new System.Drawing.Point(150, 86);
            this.txtQdtCupom.Name = "txtQdtCupom";
            this.txtQdtCupom.Size = new System.Drawing.Size(60, 20);
            this.txtQdtCupom.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtQdtCupom, "Quantidade de cupons");
            this.txtQdtCupom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQdtCupom_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "%Desc";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(77, 86);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(60, 20);
            this.txtDesc.TabIndex = 4;
            this.toolTip1.SetToolTip(this.txtDesc, "Porcentagem de desconto que o cupom vai gerar");
            this.txtDesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDesc_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtFim);
            this.groupBox2.Controls.Add(this.dtInicio);
            this.groupBox2.Location = new System.Drawing.Point(103, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(199, 46);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Validade do Cupom";
            this.toolTip1.SetToolTip(this.groupBox2, "Periodo validade do cupom");
            // 
            // dtFim
            // 
            this.dtFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFim.Location = new System.Drawing.Point(105, 19);
            this.dtFim.Name = "dtFim";
            this.dtFim.Size = new System.Drawing.Size(81, 20);
            this.dtFim.TabIndex = 2;
            // 
            // dtInicio
            // 
            this.dtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtInicio.Location = new System.Drawing.Point(6, 22);
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Size = new System.Drawing.Size(81, 20);
            this.dtInicio.TabIndex = 1;
            // 
            // CodCupom
            // 
            this.CodCupom.AutoSize = true;
            this.CodCupom.Location = new System.Drawing.Point(6, 70);
            this.CodCupom.Name = "CodCupom";
            this.CodCupom.Size = new System.Drawing.Size(59, 13);
            this.CodCupom.TabIndex = 2;
            this.CodCupom.Text = "CodCupom";
            // 
            // txtCodCupom
            // 
            this.txtCodCupom.Location = new System.Drawing.Point(6, 86);
            this.txtCodCupom.Name = "txtCodCupom";
            this.txtCodCupom.Size = new System.Drawing.Size(65, 20);
            this.txtCodCupom.TabIndex = 3;
            this.toolTip1.SetToolTip(this.txtCodCupom, "Informe o código que validará o cupom");
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(9, 28);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(65, 17);
            this.chkAtivo.TabIndex = 0;
            this.chkAtivo.Text = "AtivoSN";
            this.toolTip1.SetToolTip(this.chkAtivo, "Defina se o cupom esta ativo ou não");
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // CuponGridView
            // 
            this.CuponGridView.AllowUserToAddRows = false;
            this.CuponGridView.AllowUserToDeleteRows = false;
            this.CuponGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CuponGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CuponGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CuponGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CuponGridView.Location = new System.Drawing.Point(3, 176);
            this.CuponGridView.MultiSelect = false;
            this.CuponGridView.Name = "CuponGridView";
            this.CuponGridView.ReadOnly = true;
            this.CuponGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CuponGridView.Size = new System.Drawing.Size(305, 207);
            this.CuponGridView.TabIndex = 9;
            this.CuponGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.CuponGridView_CellMouseDoubleClick);
            // 
            // gridViewCupons
            // 
            this.gridViewCupons.AllowUserToAddRows = false;
            this.gridViewCupons.AllowUserToDeleteRows = false;
            this.gridViewCupons.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gridViewCupons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewCupons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridViewCupons.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gridViewCupons.Location = new System.Drawing.Point(3, 3);
            this.gridViewCupons.Name = "gridViewCupons";
            this.gridViewCupons.ReadOnly = true;
            this.gridViewCupons.Size = new System.Drawing.Size(312, 380);
            this.gridViewCupons.TabIndex = 0;
            // 
            // frmCupom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 417);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmCupom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[xSistemas] Cupom Fidelidade";
            this.Load += new System.EventHandler(this.frmCupom_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CuponGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCupons)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtQtdPessoa;
        private System.Windows.Forms.Label QtdCupom;
        private System.Windows.Forms.TextBox txtQdtCupom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.Label CodCupom;
        private System.Windows.Forms.TextBox txtCodCupom;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.DataGridView CuponGridView;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView gridViewCupons;
    }
}