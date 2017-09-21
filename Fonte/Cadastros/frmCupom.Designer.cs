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
            this.CuponGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.txtCodCupom = new System.Windows.Forms.TextBox();
            this.CodCupom = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.QtdCupom = new System.Windows.Forms.Label();
            this.txtQdtCupom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtQtdPessoa = new System.Windows.Forms.TextBox();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.CuponGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            this.CuponGridView.Location = new System.Drawing.Point(6, 176);
            this.CuponGridView.MultiSelect = false;
            this.CuponGridView.Name = "CuponGridView";
            this.CuponGridView.ReadOnly = true;
            this.CuponGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CuponGridView.Size = new System.Drawing.Size(308, 247);
            this.CuponGridView.TabIndex = 2;
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
            this.groupBox1.Location = new System.Drawing.Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 119);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cadastro";
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(6, 19);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(65, 17);
            this.chkAtivo.TabIndex = 0;
            this.chkAtivo.Text = "AtivoSN";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // txtCodCupom
            // 
            this.txtCodCupom.Location = new System.Drawing.Point(6, 86);
            this.txtCodCupom.Name = "txtCodCupom";
            this.txtCodCupom.Size = new System.Drawing.Size(65, 20);
            this.txtCodCupom.TabIndex = 3;
            this.toolTip1.SetToolTip(this.txtCodCupom, "Informe o código que validará o cupom");
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
            this.toolTip1.SetToolTip(this.txtQtdPessoa, "Quantidade máxima de cupons que o mesmo cliente poderá usar\r\nPreencha 0 caso não " +
        "deseje ter máximo\r\n");
            this.txtQtdPessoa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQtdPessoa_KeyPress);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(183, 136);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(131, 35);
            this.btnEditar.TabIndex = 8;
            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(6, 136);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(115, 35);
            this.btnAdicionar.TabIndex = 7;
            this.btnAdicionar.Text = "Adicionar [F12]";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.AdicionarRegistro);
            // 
            // frmCupom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 425);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CuponGridView);
            this.Name = "frmCupom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[xSistemas] Cupom Fidelidade";
            this.Load += new System.EventHandler(this.frmCupom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CuponGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView CuponGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label CodCupom;
        private System.Windows.Forms.TextBox txtCodCupom;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtQtdPessoa;
        private System.Windows.Forms.Label QtdCupom;
        private System.Windows.Forms.TextBox txtQdtCupom;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnAdicionar;
    }
}