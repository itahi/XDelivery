namespace DexComanda.Operações
{
    partial class frmNotificacao
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
            this.msg = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnEnvio = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grpAgendamento = new System.Windows.Forms.GroupBox();
            this.horaEnvio = new System.Windows.Forms.DateTimePicker();
            this.dtEnvio = new System.Windows.Forms.DateTimePicker();
            this.chkAgendarEnvio = new System.Windows.Forms.CheckBox();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbInstantaneo = new System.Windows.Forms.RadioButton();
            this.rbControlado = new System.Windows.Forms.RadioButton();
            this.tpRadio = new System.Windows.Forms.ToolTip(this.components);
            this.grpAgendamento.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msg
            // 
            this.msg.Location = new System.Drawing.Point(12, 177);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(311, 157);
            this.msg.TabIndex = 0;
            this.msg.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnEnvio
            // 
            this.btnEnvio.Location = new System.Drawing.Point(108, 340);
            this.btnEnvio.Name = "btnEnvio";
            this.btnEnvio.Size = new System.Drawing.Size(102, 23);
            this.btnEnvio.TabIndex = 2;
            this.btnEnvio.Text = "Enviar";
            this.btnEnvio.UseVisualStyleBackColor = true;
            this.btnEnvio.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Conteudo da Mensagem";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grpAgendamento
            // 
            this.grpAgendamento.Controls.Add(this.horaEnvio);
            this.grpAgendamento.Controls.Add(this.dtEnvio);
            this.grpAgendamento.Enabled = false;
            this.grpAgendamento.Location = new System.Drawing.Point(12, 29);
            this.grpAgendamento.Name = "grpAgendamento";
            this.grpAgendamento.Size = new System.Drawing.Size(167, 72);
            this.grpAgendamento.TabIndex = 6;
            this.grpAgendamento.TabStop = false;
            this.grpAgendamento.Text = "Agende o Envio";
            // 
            // horaEnvio
            // 
            this.horaEnvio.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.horaEnvio.Location = new System.Drawing.Point(92, 19);
            this.horaEnvio.Name = "horaEnvio";
            this.horaEnvio.ShowUpDown = true;
            this.horaEnvio.Size = new System.Drawing.Size(68, 20);
            this.horaEnvio.TabIndex = 6;
            // 
            // dtEnvio
            // 
            this.dtEnvio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnvio.Location = new System.Drawing.Point(6, 19);
            this.dtEnvio.Name = "dtEnvio";
            this.dtEnvio.ShowUpDown = true;
            this.dtEnvio.Size = new System.Drawing.Size(80, 20);
            this.dtEnvio.TabIndex = 5;
            // 
            // chkAgendarEnvio
            // 
            this.chkAgendarEnvio.AutoSize = true;
            this.chkAgendarEnvio.Location = new System.Drawing.Point(12, 6);
            this.chkAgendarEnvio.Name = "chkAgendarEnvio";
            this.chkAgendarEnvio.Size = new System.Drawing.Size(102, 17);
            this.chkAgendarEnvio.TabIndex = 7;
            this.chkAgendarEnvio.Text = "Agendar Envio?";
            this.chkAgendarEnvio.UseVisualStyleBackColor = true;
            this.chkAgendarEnvio.CheckStateChanged += new System.EventHandler(this.chkAgendarEnvio_CheckStateChanged);
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(12, 125);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(310, 20);
            this.txtTitulo.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Titulo da Mensagem";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbControlado);
            this.groupBox1.Controls.Add(this.rbInstantaneo);
            this.groupBox1.Location = new System.Drawing.Point(185, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 72);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo Envio";
            // 
            // rbInstantaneo
            // 
            this.rbInstantaneo.AutoSize = true;
            this.rbInstantaneo.Location = new System.Drawing.Point(13, 21);
            this.rbInstantaneo.Name = "rbInstantaneo";
            this.rbInstantaneo.Size = new System.Drawing.Size(81, 17);
            this.rbInstantaneo.TabIndex = 0;
            this.rbInstantaneo.Text = "Instantaneo";
            this.tpRadio.SetToolTip(this.rbInstantaneo, "Entrega a todos ao mesmo tempo");
            this.rbInstantaneo.UseVisualStyleBackColor = true;
            // 
            // rbControlado
            // 
            this.rbControlado.AutoSize = true;
            this.rbControlado.Checked = true;
            this.rbControlado.Location = new System.Drawing.Point(13, 44);
            this.rbControlado.Name = "rbControlado";
            this.rbControlado.Size = new System.Drawing.Size(76, 17);
            this.rbControlado.TabIndex = 1;
            this.rbControlado.TabStop = true;
            this.rbControlado.Text = "Controlado";
            this.tpRadio.SetToolTip(this.rbControlado, "Entrega no horario que o cliente costuma acessar o site/app");
            this.rbControlado.UseVisualStyleBackColor = true;
            // 
            // frmNotificacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 375);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTitulo);
            this.Controls.Add(this.chkAgendarEnvio);
            this.Controls.Add(this.grpAgendamento);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEnvio);
            this.Controls.Add(this.msg);
            this.Name = "frmNotificacao";
            this.Text = "[xSistemas] Push/Notificação";
            this.grpAgendamento.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox msg;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnEnvio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpAgendamento;
        private System.Windows.Forms.DateTimePicker horaEnvio;
        private System.Windows.Forms.DateTimePicker dtEnvio;
        private System.Windows.Forms.CheckBox chkAgendarEnvio;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbControlado;
        private System.Windows.Forms.RadioButton rbInstantaneo;
        private System.Windows.Forms.ToolTip tpRadio;
    }
}