namespace DexComanda.Cadastros.Pessoa
{
    partial class frmSelecionaEndereco
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
            this.btnConfirma = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxEnderecos = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnConfirma
            // 
            this.btnConfirma.Location = new System.Drawing.Point(154, 83);
            this.btnConfirma.Name = "btnConfirma";
            this.btnConfirma.Size = new System.Drawing.Size(75, 23);
            this.btnConfirma.TabIndex = 1;
            this.btnConfirma.Text = "Confirma";
            this.btnConfirma.UseVisualStyleBackColor = true;
            this.btnConfirma.Click += new System.EventHandler(this.ConfirmaSeleção);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(95, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selecione qual endereço deseja receber";
            // 
            // cbxEnderecos
            // 
            this.cbxEnderecos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEnderecos.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxEnderecos.FormattingEnabled = true;
            this.cbxEnderecos.Location = new System.Drawing.Point(12, 47);
            this.cbxEnderecos.Name = "cbxEnderecos";
            this.cbxEnderecos.Size = new System.Drawing.Size(404, 21);
            this.cbxEnderecos.TabIndex = 3;
            // 
            // frmSelecionaEndereco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 125);
            this.Controls.Add(this.cbxEnderecos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConfirma);
            this.Name = "frmSelecionaEndereco";
            this.Text = "[xSistemas] Selecione o Endereço";
            this.Load += new System.EventHandler(this.frmSelecionaEndereco_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConfirma;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxEnderecos;
    }
}