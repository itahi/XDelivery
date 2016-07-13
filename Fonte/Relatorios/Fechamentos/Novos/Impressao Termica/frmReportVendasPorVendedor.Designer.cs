namespace DexComanda.Relatorios.Impressao_Termica
{
    partial class frmReportVendasPorVendedor
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
            this.txtdtInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdtFim = new System.Windows.Forms.DateTimePicker();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.crReportVendasPorVendedor = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.txtdtInicio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtdtFim);
            this.groupBox1.Controls.Add(this.btnConsultar);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 57);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // txtdtInicio
            // 
            this.txtdtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtdtInicio.Location = new System.Drawing.Point(15, 20);
            this.txtdtInicio.Name = "txtdtInicio";
            this.txtdtInicio.Size = new System.Drawing.Size(91, 20);
            this.txtdtInicio.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Entre";
            // 
            // txtdtFim
            // 
            this.txtdtFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtdtFim.Location = new System.Drawing.Point(150, 20);
            this.txtdtFim.Name = "txtdtFim";
            this.txtdtFim.Size = new System.Drawing.Size(91, 20);
            this.txtdtFim.TabIndex = 16;
            this.txtdtFim.Value = new System.DateTime(2014, 11, 19, 0, 0, 0, 0);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(256, 17);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 14;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.GerarRelatório);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.crReportVendasPorVendedor);
            this.groupBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox2.Location = new System.Drawing.Point(5, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(715, 388);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Relatório";
            // 
            // crReportVendasPorVendedor
            // 
            this.crReportVendasPorVendedor.ActiveViewIndex = -1;
            this.crReportVendasPorVendedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crReportVendasPorVendedor.Cursor = System.Windows.Forms.Cursors.Default;
            this.crReportVendasPorVendedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crReportVendasPorVendedor.Location = new System.Drawing.Point(3, 16);
            this.crReportVendasPorVendedor.Name = "crReportVendasPorVendedor";
            this.crReportVendasPorVendedor.Size = new System.Drawing.Size(709, 369);
            this.crReportVendasPorVendedor.TabIndex = 0;
            // 
            // frmReportVendasPorVendedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 466);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmReportVendasPorVendedor";
            this.Text = "[xSistemas] Rel. Vendedor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker txtdtInicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker txtdtFim;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.GroupBox groupBox2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crReportVendasPorVendedor;
    }
}