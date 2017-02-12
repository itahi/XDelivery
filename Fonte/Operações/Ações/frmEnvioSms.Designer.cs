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
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRestante = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEnviarSms = new System.Windows.Forms.Button();
            this.txtMensagem = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpTotalSelect = new System.Windows.Forms.GroupBox();
            this.lbl = new System.Windows.Forms.Label();
            this.rbSemPedidos = new System.Windows.Forms.RadioButton();
            this.rbAniversariantes = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbSelecao.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpTotalSelect.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSelecao
            // 
            this.tbSelecao.Controls.Add(this.tabPage1);
            this.tbSelecao.Location = new System.Drawing.Point(0, 1);
            this.tbSelecao.Name = "tbSelecao";
            this.tbSelecao.SelectedIndex = 0;
            this.tbSelecao.Size = new System.Drawing.Size(451, 395);
            this.tbSelecao.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.lblRestante);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnEnviarSms);
            this.tabPage1.Controls.Add(this.txtMensagem);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(443, 369);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Seleção";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(289, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "\"@Cliente\"";
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(289, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "na mensagem inclua a chave";
            this.label7.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(289, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Para incluir o nome do cliente ";
            this.label6.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(300, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 328);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "caracteres";
            // 
            // lblRestante
            // 
            this.lblRestante.AutoSize = true;
            this.lblRestante.Location = new System.Drawing.Point(84, 328);
            this.lblRestante.Name = "lblRestante";
            this.lblRestante.Size = new System.Drawing.Size(25, 13);
            this.lblRestante.TabIndex = 11;
            this.lblRestante.Text = "155";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 328);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Ainda restam:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mensagem ao Cliente";
            // 
            // btnEnviarSms
            // 
            this.btnEnviarSms.Location = new System.Drawing.Point(295, 213);
            this.btnEnviarSms.Name = "btnEnviarSms";
            this.btnEnviarSms.Size = new System.Drawing.Size(122, 46);
            this.btnEnviarSms.TabIndex = 2;
            this.btnEnviarSms.Text = "Selecionar / Enviar";
            this.btnEnviarSms.UseVisualStyleBackColor = true;
            this.btnEnviarSms.Click += new System.EventHandler(this.EnviarSMS);
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(1, 152);
            this.txtMensagem.MaxLength = 145;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(288, 173);
            this.txtMensagem.TabIndex = 1;
            this.txtMensagem.Text = "";
            this.txtMensagem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMensagem_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.grpTotalSelect);
            this.groupBox1.Controls.Add(this.rbSemPedidos);
            this.groupBox1.Controls.Add(this.rbAniversariantes);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 121);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // grpTotalSelect
            // 
            this.grpTotalSelect.Controls.Add(this.lbl);
            this.grpTotalSelect.Location = new System.Drawing.Point(307, 16);
            this.grpTotalSelect.Name = "grpTotalSelect";
            this.grpTotalSelect.Size = new System.Drawing.Size(119, 82);
            this.grpTotalSelect.TabIndex = 10;
            this.grpTotalSelect.TabStop = false;
            this.grpTotalSelect.Text = "Total Selecionados";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.BackColor = System.Drawing.SystemColors.ControlText;
            this.lbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.ForeColor = System.Drawing.Color.Crimson;
            this.lbl.Location = new System.Drawing.Point(39, 37);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(34, 19);
            this.lbl.TabIndex = 10;
            this.lbl.Text = "000";
            // 
            // rbSemPedidos
            // 
            this.rbSemPedidos.AutoSize = true;
            this.rbSemPedidos.Location = new System.Drawing.Point(155, 19);
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
            this.rbAniversariantes.Size = new System.Drawing.Size(102, 17);
            this.rbAniversariantes.TabIndex = 0;
            this.rbAniversariantes.TabStop = true;
            this.rbAniversariantes.Text = "Aniv. no periodo";
            this.toolTip1.SetToolTip(this.rbAniversariantes, "Busca clientes que fazem ainversário no periodo, considera apenas dia e mês");
            this.rbAniversariantes.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtFim);
            this.groupBox2.Controls.Add(this.dtInicio);
            this.groupBox2.Location = new System.Drawing.Point(48, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(187, 56);
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
            // frmEnvioSms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 397);
            this.Controls.Add(this.tbSelecao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmEnvioSms";
            this.Text = "[XSistemas] Envio de SMS ao Cliente";
            this.Load += new System.EventHandler(this.frmEnvioSms_Load);
            this.tbSelecao.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpTotalSelect.ResumeLayout(false);
            this.grpTotalSelect.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.GroupBox grpTotalSelect;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRestante;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}