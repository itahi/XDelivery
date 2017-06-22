namespace DexComanda.Operações
{
    partial class frmConectaBanco
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label12 = new System.Windows.Forms.Label();
            this.btnConectarAoBanco = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.cbxServidor = new System.Windows.Forms.ComboBox();
            this.cbxListaBanco = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxListaBanco);
            this.groupBox1.Controls.Add(this.cbxServidor);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.btnConectarAoBanco);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Location = new System.Drawing.Point(10, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 191);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conexão Banco de Dados";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(135, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "Servidor:";
            // 
            // btnConectarAoBanco
            // 
            this.btnConectarAoBanco.Location = new System.Drawing.Point(113, 137);
            this.btnConectarAoBanco.Name = "btnConectarAoBanco";
            this.btnConectarAoBanco.Size = new System.Drawing.Size(83, 48);
            this.btnConectarAoBanco.TabIndex = 24;
            this.btnConectarAoBanco.Text = "Conectar";
            this.btnConectarAoBanco.UseVisualStyleBackColor = true;
            this.btnConectarAoBanco.Click += new System.EventHandler(this.ConectaBancoDeDados);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(135, 83);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Banco:";
            // 
            // cbxServidor
            // 
            this.cbxServidor.FormattingEnabled = true;
            this.cbxServidor.Location = new System.Drawing.Point(43, 51);
            this.cbxServidor.Name = "cbxServidor";
            this.cbxServidor.Size = new System.Drawing.Size(235, 21);
            this.cbxServidor.TabIndex = 25;
            this.cbxServidor.DropDown += new System.EventHandler(this.ListaServidores);
            // 
            // cbxListaBanco
            // 
            this.cbxListaBanco.FormattingEnabled = true;
            this.cbxListaBanco.Location = new System.Drawing.Point(43, 109);
            this.cbxListaBanco.Name = "cbxListaBanco";
            this.cbxListaBanco.Size = new System.Drawing.Size(235, 21);
            this.cbxListaBanco.TabIndex = 26;
            this.cbxListaBanco.DropDown += new System.EventHandler(this.cbxListaBanco_DropDown);
            // 
            // frmConectaBanco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 211);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmConectaBanco";
            this.Text = "[xSistemas]  Banco de Dados";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox cbxServidor;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnConectarAoBanco;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbxListaBanco;
    }
}