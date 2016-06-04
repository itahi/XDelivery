namespace DexComanda.Relatorios
{
    partial class frmReportProdutos
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.produtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dBExpertDataSet = new DexComanda.DBExpertDataSet();
            this.produtoTableAdapter = new DexComanda.DBExpertDataSetTableAdapters.ProdutoTableAdapter();
            this.reportProdutos = new Microsoft.Reporting.WinForms.ReportViewer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxGrupos = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.produtoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBExpertDataSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // produtoBindingSource
            // 
            this.produtoBindingSource.DataMember = "Produto";
            this.produtoBindingSource.DataSource = this.dBExpertDataSet;
            // 
            // dBExpertDataSet
            // 
            this.dBExpertDataSet.DataSetName = "DBExpertDataSet";
            this.dBExpertDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // produtoTableAdapter
            // 
            this.produtoTableAdapter.ClearBeforeFill = true;
            // 
            // reportProdutos
            // 
            this.reportProdutos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "Produtos";
            reportDataSource1.Value = this.produtoBindingSource;
            this.reportProdutos.LocalReport.DataSources.Add(reportDataSource1);
            this.reportProdutos.LocalReport.ReportEmbeddedResource = "DexComanda.Relatorios.RelatorioDeProdutos.rdlc";
            this.reportProdutos.Location = new System.Drawing.Point(0, 62);
            this.reportProdutos.Name = "reportProdutos";
            this.reportProdutos.Size = new System.Drawing.Size(694, 401);
            this.reportProdutos.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConsultar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxGrupos);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(670, 54);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros:";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(265, 19);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(90, 23);
            this.btnConsultar.TabIndex = 3;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.Consultar);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Grupo:";
            // 
            // cbxGrupos
            // 
            this.cbxGrupos.FormattingEnabled = true;
            this.cbxGrupos.Location = new System.Drawing.Point(67, 19);
            this.cbxGrupos.Name = "cbxGrupos";
            this.cbxGrupos.Size = new System.Drawing.Size(175, 21);
            this.cbxGrupos.TabIndex = 0;
            // 
            // frmReportProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 461);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.reportProdutos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmReportProdutos";
            this.Text = "Relatório de Produtos";
            this.Load += new System.EventHandler(this.frmReportProdutos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.produtoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBExpertDataSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DBExpertDataSet dBExpertDataSet;
        private System.Windows.Forms.BindingSource produtoBindingSource;
        private DBExpertDataSetTableAdapters.ProdutoTableAdapter produtoTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportProdutos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxGrupos;
        private System.Windows.Forms.Button btnConsultar;
    }
}