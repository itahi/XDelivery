namespace DexComanda.Relatorios.Clientes
{
    partial class frmReportClientePorOrigem
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.grmReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.Size = new System.Drawing.Size(362, 395);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Size = new System.Drawing.Size(372, 79);
            this.groupBox1.Controls.SetChildIndex(this.dtInicio, 0);
            this.groupBox1.Controls.SetChildIndex(this.dtFim, 0);
            this.groupBox1.Controls.SetChildIndex(this.label2, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnImprimir, 0);
            this.groupBox1.Controls.SetChildIndex(this.label1, 0);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(269, 35);
            this.btnImprimir.Click += new System.EventHandler(this.GerarReport);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(126, 44);
            this.label2.Visible = false;
            // 
            // dtFim
            // 
            this.dtFim.Location = new System.Drawing.Point(177, 38);
            this.dtFim.Visible = false;
            // 
            // dtInicio
            // 
            this.dtInicio.Location = new System.Drawing.Point(23, 38);
            this.dtInicio.Visible = false;
            this.dtInicio.ValueChanged += new System.EventHandler(this.dtInicio_ValueChanged);
            // 
            // grmReport
            // 
            this.grmReport.Location = new System.Drawing.Point(2, 87);
            this.grmReport.Size = new System.Drawing.Size(368, 414);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Saiba de onde seus clientes estão lhe conhecendo";
            // 
            // frmReportClientePorOrigem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(377, 513);
            this.Name = "frmReportClientePorOrigem";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grmReport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
    }
}
