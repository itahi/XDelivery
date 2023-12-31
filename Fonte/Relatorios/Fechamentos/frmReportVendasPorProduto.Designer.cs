﻿namespace DexComanda.Relatorios
{
    partial class frmReportVendasPorProduto
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
            this.vwObterItemsVendidosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsRelatorio = new DexComanda.Relatorios.dsRelatorio();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtdtInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdtFim = new System.Windows.Forms.DateTimePicker();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.vwObterItemsVendidosTableAdapter = new DexComanda.Relatorios.dsRelatorioTableAdapters.vwObterItemsVendidosTableAdapter();
            this.rptProdutosVenda = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.vwObterItemsVendidosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRelatorio)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vwObterItemsVendidosBindingSource
            // 
            this.vwObterItemsVendidosBindingSource.DataMember = "vwObterItemsVendidos";
            this.vwObterItemsVendidosBindingSource.DataSource = this.dsRelatorio;
            // 
            // dsRelatorio
            // 
            this.dsRelatorio.DataSetName = "dsRelatorio";
            this.dsRelatorio.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtdtInicio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtdtFim);
            this.groupBox1.Controls.Add(this.btnConsultar);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(809, 57);
            this.groupBox1.TabIndex = 0;
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
            this.btnConsultar.Location = new System.Drawing.Point(247, 16);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 14;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.GeraReport);
            // 
            // vwObterItemsVendidosTableAdapter
            // 
            this.vwObterItemsVendidosTableAdapter.ClearBeforeFill = true;
            // 
            // rptProdutosVenda
            // 
            this.rptProdutosVenda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "dsItems";
            reportDataSource1.Value = this.vwObterItemsVendidosBindingSource;
            this.rptProdutosVenda.LocalReport.DataSources.Add(reportDataSource1);
            this.rptProdutosVenda.LocalReport.ReportEmbeddedResource = "DexComanda.Relatorios.Fechamentos.RelItemsVendidos.rdlc";
            this.rptProdutosVenda.Location = new System.Drawing.Point(7, 66);
            this.rptProdutosVenda.Name = "rptProdutosVenda";
            this.rptProdutosVenda.Size = new System.Drawing.Size(821, 378);
            this.rptProdutosVenda.TabIndex = 1;
            // 
            // frmReportVendasPorProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 446);
            this.Controls.Add(this.rptProdutosVenda);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmReportVendasPorProduto";
            this.Text = "Relatório de Produtos (Vendas)";
            this.Load += new System.EventHandler(this.frmReportVendas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vwObterItemsVendidosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRelatorio)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker txtdtFim;
        private System.Windows.Forms.DateTimePicker txtdtInicio;
        private System.Windows.Forms.BindingSource vwObterItemsVendidosBindingSource;
        private dsRelatorio dsRelatorio;
        private dsRelatorioTableAdapters.vwObterItemsVendidosTableAdapter vwObterItemsVendidosTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer rptProdutosVenda;
    }
}