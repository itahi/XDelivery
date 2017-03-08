namespace DexComanda
{
    partial class frmEnvioSms
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
            this.tbSelecao = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.grpGrid = new System.Windows.Forms.GroupBox();
            this.gridResultado = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMensagem = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOndeConheceu = new System.Windows.Forms.RadioButton();
            this.cbxOndeConheceu = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.btnEnviarSms = new System.Windows.Forms.Button();
            this.rbSemPedidos = new System.Windows.Forms.RadioButton();
            this.rbAniversariantes = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRestante = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbSelecao.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridResultado)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSelecao
            // 
            this.tbSelecao.Controls.Add(this.tabPage1);
            this.tbSelecao.Location = new System.Drawing.Point(0, 1);
            this.tbSelecao.Name = "tbSelecao";
            this.tbSelecao.SelectedIndex = 0;
            this.tbSelecao.Size = new System.Drawing.Size(578, 359);
            this.tbSelecao.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnEnviar);
            this.tabPage1.Controls.Add(this.grpGrid);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtMensagem);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(570, 333);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Seleção";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(458, 290);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(96, 28);
            this.btnEnviar.TabIndex = 19;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.DisparaSMS);
            // 
            // grpGrid
            // 
            this.grpGrid.Controls.Add(this.gridResultado);
            this.grpGrid.Location = new System.Drawing.Point(8, 133);
            this.grpGrid.Name = "grpGrid";
            this.grpGrid.Size = new System.Drawing.Size(220, 198);
            this.grpGrid.TabIndex = 14;
            this.grpGrid.TabStop = false;
            this.grpGrid.Text = "Resultado Filtro";
            // 
            // gridResultado
            // 
            this.gridResultado.AllowUserToAddRows = false;
            this.gridResultado.AllowUserToDeleteRows = false;
            this.gridResultado.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gridResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridResultado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridResultado.Location = new System.Drawing.Point(3, 16);
            this.gridResultado.Name = "gridResultado";
            this.gridResultado.ReadOnly = true;
            this.gridResultado.Size = new System.Drawing.Size(214, 179);
            this.gridResultado.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(300, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(245, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mensagem ao Cliente";
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(238, 146);
            this.txtMensagem.MaxLength = 145;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(200, 182);
            this.txtMensagem.TabIndex = 1;
            this.txtMensagem.Text = "";
            this.txtMensagem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMensagem_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbOndeConheceu);
            this.groupBox1.Controls.Add(this.cbxOndeConheceu);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnEnviarSms);
            this.groupBox1.Controls.Add(this.rbSemPedidos);
            this.groupBox1.Controls.Add(this.rbAniversariantes);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 121);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // rbOndeConheceu
            // 
            this.rbOndeConheceu.AutoSize = true;
            this.rbOndeConheceu.Location = new System.Drawing.Point(388, 16);
            this.rbOndeConheceu.Name = "rbOndeConheceu";
            this.rbOndeConheceu.Size = new System.Drawing.Size(108, 17);
            this.rbOndeConheceu.TabIndex = 18;
            this.rbOndeConheceu.TabStop = true;
            this.rbOndeConheceu.Text = "Onde conheceu?";
            this.toolTip1.SetToolTip(this.rbOndeConheceu, "Clientes por origem de onde conheceram ");
            this.rbOndeConheceu.UseVisualStyleBackColor = true;
            this.rbOndeConheceu.CheckedChanged += new System.EventHandler(this.rbOndeConheceu_CheckedChanged);
            // 
            // cbxOndeConheceu
            // 
            this.cbxOndeConheceu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOndeConheceu.FormattingEnabled = true;
            this.cbxOndeConheceu.Location = new System.Drawing.Point(388, 42);
            this.cbxOndeConheceu.Name = "cbxOndeConheceu";
            this.cbxOndeConheceu.Size = new System.Drawing.Size(160, 21);
            this.cbxOndeConheceu.TabIndex = 17;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtFim);
            this.groupBox2.Controls.Add(this.dtInicio);
            this.groupBox2.Location = new System.Drawing.Point(11, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 55);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Periodo";
            // 
            // dtFim
            // 
            this.dtFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFim.Location = new System.Drawing.Point(95, 19);
            this.dtFim.Name = "dtFim";
            this.dtFim.Size = new System.Drawing.Size(79, 20);
            this.dtFim.TabIndex = 17;
            // 
            // dtInicio
            // 
            this.dtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtInicio.Location = new System.Drawing.Point(6, 19);
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Size = new System.Drawing.Size(79, 20);
            this.dtInicio.TabIndex = 16;
            // 
            // btnEnviarSms
            // 
            this.btnEnviarSms.Location = new System.Drawing.Point(452, 69);
            this.btnEnviarSms.Name = "btnEnviarSms";
            this.btnEnviarSms.Size = new System.Drawing.Size(96, 28);
            this.btnEnviarSms.TabIndex = 2;
            this.btnEnviarSms.Text = "Selecionar";
            this.btnEnviarSms.UseVisualStyleBackColor = true;
            this.btnEnviarSms.Click += new System.EventHandler(this.EnviarSMS);
            // 
            // rbSemPedidos
            // 
            this.rbSemPedidos.AutoSize = true;
            this.rbSemPedidos.Location = new System.Drawing.Point(199, 19);
            this.rbSemPedidos.Name = "rbSemPedidos";
            this.rbSemPedidos.Size = new System.Drawing.Size(139, 17);
            this.rbSemPedidos.TabIndex = 3;
            this.rbSemPedidos.TabStop = true;
            this.rbSemPedidos.Text = "Sem pedidos no periodo";
            this.toolTip1.SetToolTip(this.rbSemPedidos, "Clientes que não fizeram pedido no periodo");
            this.rbSemPedidos.UseVisualStyleBackColor = true;
            // 
            // rbAniversariantes
            // 
            this.rbAniversariantes.AutoSize = true;
            this.rbAniversariantes.Location = new System.Drawing.Point(7, 16);
            this.rbAniversariantes.Name = "rbAniversariantes";
            this.rbAniversariantes.Size = new System.Drawing.Size(150, 17);
            this.rbAniversariantes.TabIndex = 0;
            this.rbAniversariantes.TabStop = true;
            this.rbAniversariantes.Text = "Aniversáriantes no periodo";
            this.toolTip1.SetToolTip(this.rbAniversariantes, "Busca clientes que fazem ainversário no periodo, considera apenas dia e mês");
            this.rbAniversariantes.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(380, 364);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "caracteres";
            // 
            // lblRestante
            // 
            this.lblRestante.AutoSize = true;
            this.lblRestante.Location = new System.Drawing.Point(349, 364);
            this.lblRestante.Name = "lblRestante";
            this.lblRestante.Size = new System.Drawing.Size(25, 13);
            this.lblRestante.TabIndex = 11;
            this.lblRestante.Text = "155";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 364);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Ainda restam:";
            // 
            // frmEnvioSms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 382);
            this.Controls.Add(this.tbSelecao);
            this.Controls.Add(this.lblRestante);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmEnvioSms";
            this.Text = "[xSistemas] Envio de SMS ao Cliente";
            this.Load += new System.EventHandler(this.frmEnvioSms_Load);
            this.tbSelecao.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.grpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridResultado)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbSelecao;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSemPedidos;
        private System.Windows.Forms.RadioButton rbAniversariantes;
        private System.Windows.Forms.Button btnEnviarSms;
        private System.Windows.Forms.RichTextBox txtMensagem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRestante;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox grpGrid;
        private System.Windows.Forms.DataGridView gridResultado;
        private System.Windows.Forms.ComboBox cbxOndeConheceu;
        private System.Windows.Forms.RadioButton rbOndeConheceu;
        private System.Windows.Forms.Button btnEnviar;
    }
}