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
            this.dtEnvio = new System.Windows.Forms.DateTimePicker();
            this.horaEnvio = new System.Windows.Forms.DateTimePicker();
            this.chkAgendarEnvio = new System.Windows.Forms.CheckBox();
            this.grpAgendamento.SuspendLayout();
            this.SuspendLayout();
            // 
            // msg
            // 
            this.msg.Location = new System.Drawing.Point(12, 113);
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
            this.btnEnvio.Location = new System.Drawing.Point(12, 276);
            this.btnEnvio.Name = "btnEnvio";
            this.btnEnvio.Size = new System.Drawing.Size(75, 23);
            this.btnEnvio.TabIndex = 2;
            this.btnEnvio.Text = "Enviar";
            this.btnEnvio.UseVisualStyleBackColor = true;
            this.btnEnvio.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Escreva aqui o conteudo da mensagem ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grpAgendamento
            // 
            this.grpAgendamento.Controls.Add(this.horaEnvio);
            this.grpAgendamento.Controls.Add(this.dtEnvio);
            this.grpAgendamento.Enabled = false;
            this.grpAgendamento.Location = new System.Drawing.Point(12, 28);
            this.grpAgendamento.Name = "grpAgendamento";
            this.grpAgendamento.Size = new System.Drawing.Size(198, 56);
            this.grpAgendamento.TabIndex = 6;
            this.grpAgendamento.TabStop = false;
            this.grpAgendamento.Text = "Agende o Envio";
            // 
            // dtEnvio
            // 
            this.dtEnvio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnvio.Location = new System.Drawing.Point(6, 19);
            this.dtEnvio.Name = "dtEnvio";
            this.dtEnvio.ShowUpDown = true;
            this.dtEnvio.Size = new System.Drawing.Size(96, 20);
            this.dtEnvio.TabIndex = 5;
            // 
            // horaEnvio
            // 
            this.horaEnvio.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.horaEnvio.Location = new System.Drawing.Point(112, 19);
            this.horaEnvio.Name = "horaEnvio";
            this.horaEnvio.ShowUpDown = true;
            this.horaEnvio.Size = new System.Drawing.Size(80, 20);
            this.horaEnvio.TabIndex = 6;
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
            // frmNotificacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 306);
            this.Controls.Add(this.chkAgendarEnvio);
            this.Controls.Add(this.grpAgendamento);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEnvio);
            this.Controls.Add(this.msg);
            this.Name = "frmNotificacao";
            this.Text = "[xSistemas] Push/Notificação";
            this.grpAgendamento.ResumeLayout(false);
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
    }
}