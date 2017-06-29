namespace DexComanda.Operações.Estoque
{
    partial class frmConsultaInsumo
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
            this.rbDetalhado = new System.Windows.Forms.RadioButton();
            this.rbResumido = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.grmReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.DisplayStatusBar = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(339, 380);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbResumido);
            this.groupBox1.Controls.Add(this.rbDetalhado);
            this.groupBox1.Size = new System.Drawing.Size(342, 94);
            this.groupBox1.Controls.SetChildIndex(this.dtInicio, 0);
            this.groupBox1.Controls.SetChildIndex(this.dtFim, 0);
            this.groupBox1.Controls.SetChildIndex(this.label2, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnImprimir, 0);
            this.groupBox1.Controls.SetChildIndex(this.rbDetalhado, 0);
            this.groupBox1.Controls.SetChildIndex(this.rbResumido, 0);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(119, 65);
            this.btnImprimir.Click += new System.EventHandler(this.Report);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(99, 33);
            // 
            // dtFim
            // 
            this.dtFim.Location = new System.Drawing.Point(137, 28);
            // 
            // dtInicio
            // 
            this.dtInicio.Location = new System.Drawing.Point(10, 27);
            // 
            // grmReport
            // 
            this.grmReport.Location = new System.Drawing.Point(2, 102);
            this.grmReport.Size = new System.Drawing.Size(345, 399);
            // 
            // rbDetalhado
            // 
            this.rbDetalhado.AutoSize = true;
            this.rbDetalhado.Location = new System.Drawing.Point(244, 27);
            this.rbDetalhado.Name = "rbDetalhado";
            this.rbDetalhado.Size = new System.Drawing.Size(74, 17);
            this.rbDetalhado.TabIndex = 6;
            this.rbDetalhado.Text = "Detalhado";
            this.toolTip1.SetToolTip(this.rbDetalhado, "Monstra todas movimentações de entrada e saida");
            this.rbDetalhado.UseVisualStyleBackColor = true;
            // 
            // rbResumido
            // 
            this.rbResumido.AutoSize = true;
            this.rbResumido.Checked = true;
            this.rbResumido.Location = new System.Drawing.Point(246, 50);
            this.rbResumido.Name = "rbResumido";
            this.rbResumido.Size = new System.Drawing.Size(72, 17);
            this.rbResumido.TabIndex = 7;
            this.rbResumido.TabStop = true;
            this.rbResumido.Text = "Resumido";
            this.toolTip1.SetToolTip(this.rbResumido, "Agrupa as movimentações e mostra posição atual do estoque");
            this.rbResumido.UseVisualStyleBackColor = true;
            // 
            // frmConsultaInsumo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(352, 513);
            this.Name = "frmConsultaInsumo";
            this.Text = "[xSistemas] Relatório de Insumos";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grmReport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbResumido;
        private System.Windows.Forms.RadioButton rbDetalhado;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
