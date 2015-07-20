namespace DexComanda
{
    partial class frmAlteraSenha
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
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtSenhaAntiga = new System.Windows.Forms.TextBox();
            this.txtSenhanova = new System.Windows.Forms.TextBox();
            this.btnAlteraSenha = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLogin
            // 
            this.txtLogin.Enabled = false;
            this.txtLogin.Location = new System.Drawing.Point(60, 28);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(176, 20);
            this.txtLogin.TabIndex = 0;
            // 
            // txtSenhaAntiga
            // 
            this.txtSenhaAntiga.Location = new System.Drawing.Point(60, 71);
            this.txtSenhaAntiga.Name = "txtSenhaAntiga";
            this.txtSenhaAntiga.PasswordChar = 'X';
            this.txtSenhaAntiga.Size = new System.Drawing.Size(176, 20);
            this.txtSenhaAntiga.TabIndex = 1;
            this.txtSenhaAntiga.UseSystemPasswordChar = true;
            // 
            // txtSenhanova
            // 
            this.txtSenhanova.Location = new System.Drawing.Point(60, 108);
            this.txtSenhanova.Name = "txtSenhanova";
            this.txtSenhanova.PasswordChar = 'X';
            this.txtSenhanova.Size = new System.Drawing.Size(176, 20);
            this.txtSenhanova.TabIndex = 2;
            this.txtSenhanova.UseSystemPasswordChar = true;
            // 
            // btnAlteraSenha
            // 
            this.btnAlteraSenha.Location = new System.Drawing.Point(108, 134);
            this.btnAlteraSenha.Name = "btnAlteraSenha";
            this.btnAlteraSenha.Size = new System.Drawing.Size(75, 23);
            this.btnAlteraSenha.TabIndex = 3;
            this.btnAlteraSenha.Text = "Alterar";
            this.btnAlteraSenha.UseVisualStyleBackColor = true;
            this.btnAlteraSenha.Click += new System.EventHandler(this.btnAlteraSenha_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Senha Antiga";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nova Senha";
            // 
            // frmAlteraSenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 167);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAlteraSenha);
            this.Controls.Add(this.txtSenhanova);
            this.Controls.Add(this.txtSenhaAntiga);
            this.Controls.Add(this.txtLogin);
            this.Name = "frmAlteraSenha";
            this.Text = "[DEX] Altera senha";
            this.Load += new System.EventHandler(this.frmAlteraSenha_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtSenhaAntiga;
        private System.Windows.Forms.TextBox txtSenhanova;
        private System.Windows.Forms.Button btnAlteraSenha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}