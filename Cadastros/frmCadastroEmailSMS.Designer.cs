namespace DexComanda
{
    partial class frmCadastroEmailSMS
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMsgAniversariante = new System.Windows.Forms.RichTextBox();
            this.btnSalvarniver = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSalvarSemPedido = new System.Windows.Forms.Button();
            this.txtMsgClienteSemPedido = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.brnSalvarMelhorCliente = new System.Windows.Forms.Button();
            this.txtMsgMelhorCliente = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(567, 314);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(559, 288);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Textos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSalvarniver);
            this.groupBox1.Controls.Add(this.txtMsgAniversariante);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Aniversáriantes";
            // 
            // txtMsgAniversariante
            // 
            this.txtMsgAniversariante.Location = new System.Drawing.Point(12, 17);
            this.txtMsgAniversariante.Name = "txtMsgAniversariante";
            this.txtMsgAniversariante.Size = new System.Drawing.Size(238, 80);
            this.txtMsgAniversariante.TabIndex = 0;
            this.txtMsgAniversariante.Text = "";
            // 
            // btnSalvarniver
            // 
            this.btnSalvarniver.Location = new System.Drawing.Point(89, 103);
            this.btnSalvarniver.Name = "btnSalvarniver";
            this.btnSalvarniver.Size = new System.Drawing.Size(85, 23);
            this.btnSalvarniver.TabIndex = 1;
            this.btnSalvarniver.Text = "Salvar/Editar";
            this.btnSalvarniver.UseVisualStyleBackColor = true;
            this.btnSalvarniver.Click += new System.EventHandler(this.SalvarMsgNiver);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSalvarSemPedido);
            this.groupBox2.Controls.Add(this.txtMsgClienteSemPedido);
            this.groupBox2.Location = new System.Drawing.Point(283, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(256, 132);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Clientes sem pedido";
            // 
            // btnSalvarSemPedido
            // 
            this.btnSalvarSemPedido.Location = new System.Drawing.Point(89, 103);
            this.btnSalvarSemPedido.Name = "btnSalvarSemPedido";
            this.btnSalvarSemPedido.Size = new System.Drawing.Size(85, 23);
            this.btnSalvarSemPedido.TabIndex = 1;
            this.btnSalvarSemPedido.Text = "Salvar/Editar";
            this.btnSalvarSemPedido.UseVisualStyleBackColor = true;
            this.btnSalvarSemPedido.Click += new System.EventHandler(this.SalvarSemPedido);
            // 
            // txtMsgClienteSemPedido
            // 
            this.txtMsgClienteSemPedido.Location = new System.Drawing.Point(12, 17);
            this.txtMsgClienteSemPedido.Name = "txtMsgClienteSemPedido";
            this.txtMsgClienteSemPedido.Size = new System.Drawing.Size(238, 80);
            this.txtMsgClienteSemPedido.TabIndex = 0;
            this.txtMsgClienteSemPedido.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.brnSalvarMelhorCliente);
            this.groupBox3.Controls.Add(this.txtMsgMelhorCliente);
            this.groupBox3.Location = new System.Drawing.Point(12, 150);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 132);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Melhores clientes";
            // 
            // brnSalvarMelhorCliente
            // 
            this.brnSalvarMelhorCliente.Location = new System.Drawing.Point(89, 103);
            this.brnSalvarMelhorCliente.Name = "brnSalvarMelhorCliente";
            this.brnSalvarMelhorCliente.Size = new System.Drawing.Size(85, 23);
            this.brnSalvarMelhorCliente.TabIndex = 1;
            this.brnSalvarMelhorCliente.Text = "Salvar/Editar";
            this.brnSalvarMelhorCliente.UseVisualStyleBackColor = true;
            this.brnSalvarMelhorCliente.Click += new System.EventHandler(this.brnSalvarMelhorCliente_Click);
            // 
            // txtMsgMelhorCliente
            // 
            this.txtMsgMelhorCliente.Location = new System.Drawing.Point(12, 17);
            this.txtMsgMelhorCliente.Name = "txtMsgMelhorCliente";
            this.txtMsgMelhorCliente.Size = new System.Drawing.Size(238, 80);
            this.txtMsgMelhorCliente.TabIndex = 0;
            this.txtMsgMelhorCliente.Text = "";
            // 
            // frmCadastroEmailSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 314);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmCadastroEmailSMS";
            this.Text = "Cadastro SMS - Email";
            this.Load += new System.EventHandler(this.frmCadastroEmailSMS_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button brnSalvarMelhorCliente;
        private System.Windows.Forms.RichTextBox txtMsgMelhorCliente;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSalvarSemPedido;
        private System.Windows.Forms.RichTextBox txtMsgClienteSemPedido;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSalvarniver;
        private System.Windows.Forms.RichTextBox txtMsgAniversariante;
    }
}