﻿namespace DexComanda
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
            this.chkTodosClientes = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxPorta = new System.Windows.Forms.ComboBox();
            this.grpTotalSelect = new System.Windows.Forms.GroupBox();
            this.lbl = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rbTotalPedidos = new System.Windows.Forms.RadioButton();
            this.txtDataFinal2 = new System.Windows.Forms.MaskedTextBox();
            this.txtDataInicial2 = new System.Windows.Forms.MaskedTextBox();
            this.rbSemPedidos = new System.Windows.Forms.RadioButton();
            this.txtDataFinal = new System.Windows.Forms.MaskedTextBox();
            this.txtDataInicial = new System.Windows.Forms.MaskedTextBox();
            this.rbAniversariantes = new System.Windows.Forms.RadioButton();
            this.tbSelecao.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpTotalSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSelecao
            // 
            this.tbSelecao.Controls.Add(this.tabPage1);
            this.tbSelecao.Location = new System.Drawing.Point(0, 1);
            this.tbSelecao.Name = "tbSelecao";
            this.tbSelecao.SelectedIndex = 0;
            this.tbSelecao.Size = new System.Drawing.Size(507, 395);
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
            this.tabPage1.Size = new System.Drawing.Size(499, 369);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Seleção";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(302, 243);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "\"@Cliente\"";
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(300, 226);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "na mensagem inclua a chave";
            this.label7.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(300, 210);
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
            this.label5.Location = new System.Drawing.Point(263, 340);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "caracteres";
            // 
            // lblRestante
            // 
            this.lblRestante.AutoSize = true;
            this.lblRestante.Location = new System.Drawing.Point(232, 340);
            this.lblRestante.Name = "lblRestante";
            this.lblRestante.Size = new System.Drawing.Size(25, 13);
            this.lblRestante.TabIndex = 11;
            this.lblRestante.Text = "155";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 340);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Ainda restam:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mensagem ao Cliente";
            // 
            // btnEnviarSms
            // 
            this.btnEnviarSms.Location = new System.Drawing.Point(305, 259);
            this.btnEnviarSms.Name = "btnEnviarSms";
            this.btnEnviarSms.Size = new System.Drawing.Size(122, 46);
            this.btnEnviarSms.TabIndex = 2;
            this.btnEnviarSms.Text = "Selecionar / Enviar";
            this.btnEnviarSms.UseVisualStyleBackColor = true;
            this.btnEnviarSms.Click += new System.EventHandler(this.EnviarSMS);
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(6, 210);
            this.txtMensagem.MaxLength = 145;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(288, 127);
            this.txtMensagem.TabIndex = 1;
            this.txtMensagem.Text = "";
            this.txtMensagem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMensagem_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkTodosClientes);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxPorta);
            this.groupBox1.Controls.Add(this.grpTotalSelect);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.rbTotalPedidos);
            this.groupBox1.Controls.Add(this.txtDataFinal2);
            this.groupBox1.Controls.Add(this.txtDataInicial2);
            this.groupBox1.Controls.Add(this.rbSemPedidos);
            this.groupBox1.Controls.Add(this.txtDataFinal);
            this.groupBox1.Controls.Add(this.txtDataInicial);
            this.groupBox1.Controls.Add(this.rbAniversariantes);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 169);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // chkTodosClientes
            // 
            this.chkTodosClientes.AutoSize = true;
            this.chkTodosClientes.Location = new System.Drawing.Point(170, 144);
            this.chkTodosClientes.Name = "chkTodosClientes";
            this.chkTodosClientes.Size = new System.Drawing.Size(96, 17);
            this.chkTodosClientes.TabIndex = 13;
            this.chkTodosClientes.Text = "Todos Clientes";
            this.chkTodosClientes.UseMnemonic = false;
            this.chkTodosClientes.UseVisualStyleBackColor = true;
            this.chkTodosClientes.CheckedChanged += new System.EventHandler(this.chkTodosClientes_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(272, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Porta Moden";
            // 
            // cbxPorta
            // 
            this.cbxPorta.Enabled = false;
            this.cbxPorta.FormattingEnabled = true;
            this.cbxPorta.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12"});
            this.cbxPorta.Location = new System.Drawing.Point(346, 142);
            this.cbxPorta.Name = "cbxPorta";
            this.cbxPorta.Size = new System.Drawing.Size(75, 21);
            this.cbxPorta.TabIndex = 11;
            // 
            // grpTotalSelect
            // 
            this.grpTotalSelect.Controls.Add(this.lbl);
            this.grpTotalSelect.Location = new System.Drawing.Point(307, 16);
            this.grpTotalSelect.Name = "grpTotalSelect";
            this.grpTotalSelect.Size = new System.Drawing.Size(119, 100);
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 134);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(85, 20);
            this.textBox1.TabIndex = 8;
            // 
            // rbTotalPedidos
            // 
            this.rbTotalPedidos.AutoSize = true;
            this.rbTotalPedidos.Location = new System.Drawing.Point(7, 111);
            this.rbTotalPedidos.Name = "rbTotalPedidos";
            this.rbTotalPedidos.Size = new System.Drawing.Size(93, 17);
            this.rbTotalPedidos.TabIndex = 7;
            this.rbTotalPedidos.TabStop = true;
            this.rbTotalPedidos.Text = "Total Pedidos ";
            this.rbTotalPedidos.UseVisualStyleBackColor = true;
            // 
            // txtDataFinal2
            // 
            this.txtDataFinal2.Location = new System.Drawing.Point(82, 85);
            this.txtDataFinal2.Mask = "00/00/0000";
            this.txtDataFinal2.Name = "txtDataFinal2";
            this.txtDataFinal2.Size = new System.Drawing.Size(64, 20);
            this.txtDataFinal2.TabIndex = 5;
            // 
            // txtDataInicial2
            // 
            this.txtDataInicial2.Location = new System.Drawing.Point(7, 85);
            this.txtDataInicial2.Mask = "00/00/0000";
            this.txtDataInicial2.Name = "txtDataInicial2";
            this.txtDataInicial2.Size = new System.Drawing.Size(69, 20);
            this.txtDataInicial2.TabIndex = 4;
            // 
            // rbSemPedidos
            // 
            this.rbSemPedidos.AutoSize = true;
            this.rbSemPedidos.Location = new System.Drawing.Point(7, 65);
            this.rbSemPedidos.Name = "rbSemPedidos";
            this.rbSemPedidos.Size = new System.Drawing.Size(139, 17);
            this.rbSemPedidos.TabIndex = 3;
            this.rbSemPedidos.TabStop = true;
            this.rbSemPedidos.Text = "Sem pedidos no periodo";
            this.rbSemPedidos.UseVisualStyleBackColor = true;
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.Location = new System.Drawing.Point(55, 39);
            this.txtDataFinal.Mask = "00/00";
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Size = new System.Drawing.Size(36, 20);
            this.txtDataFinal.TabIndex = 2;
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.Location = new System.Drawing.Point(7, 39);
            this.txtDataInicial.Mask = "00/00";
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Size = new System.Drawing.Size(42, 20);
            this.txtDataInicial.TabIndex = 1;
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
            this.rbAniversariantes.UseVisualStyleBackColor = true;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbSelecao;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox txtDataFinal2;
        private System.Windows.Forms.MaskedTextBox txtDataInicial2;
        private System.Windows.Forms.RadioButton rbSemPedidos;
        private System.Windows.Forms.MaskedTextBox txtDataFinal;
        private System.Windows.Forms.MaskedTextBox txtDataInicial;
        private System.Windows.Forms.RadioButton rbAniversariantes;
        private System.Windows.Forms.Button btnEnviarSms;
        private System.Windows.Forms.RichTextBox txtMensagem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton rbTotalPedidos;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxPorta;
        private System.Windows.Forms.CheckBox chkTodosClientes;
    }
}