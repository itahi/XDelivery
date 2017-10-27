namespace DexComanda.Operações
{
    partial class frmAtivaIntegracao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAtivaIntegracao));
            this.btnAtivo = new System.Windows.Forms.Button();
            this.btnDesativo = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnAtivo
            // 
            this.btnAtivo.Location = new System.Drawing.Point(12, 13);
            this.btnAtivo.Name = "btnAtivo";
            this.btnAtivo.Size = new System.Drawing.Size(154, 60);
            this.btnAtivo.TabIndex = 0;
            this.btnAtivo.Text = "Ativar";
            this.btnAtivo.UseVisualStyleBackColor = true;
            this.btnAtivo.Click += new System.EventHandler(this.btnAtivo_Click);
            // 
            // btnDesativo
            // 
            this.btnDesativo.Location = new System.Drawing.Point(183, 13);
            this.btnDesativo.Name = "btnDesativo";
            this.btnDesativo.Size = new System.Drawing.Size(150, 60);
            this.btnDesativo.TabIndex = 1;
            this.btnDesativo.Text = "Desativar";
            this.btnDesativo.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(19, 87);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(303, 173);
            this.listBox1.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.ExecutaTimer);
            // 
            // frmAtivaIntegracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 277);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnDesativo);
            this.Controls.Add(this.btnAtivo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAtivaIntegracao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[xSistemas] Integração";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAtivo;
        private System.Windows.Forms.Button btnDesativo;
        private System.Windows.Forms.ListBox listBox1;
        public System.Windows.Forms.Timer timer1;
    }
}