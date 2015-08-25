namespace DexComanda.Relatorios.Fechamentos
{
    partial class frmReportEntregasPorMotoboy
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.panel1 = new System.Windows.Forms.Panel();
            this.reportViewEntregas = new Microsoft.Reporting.WinForms.ReportViewer();
            this.grpFiltro = new System.Windows.Forms.GroupBox();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxEntregador = new System.Windows.Forms.ComboBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.dataFim = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dataInicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dsRelatorio = new DexComanda.Relatorios.dsRelatorio();
            this.spEntregasPorBoyDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.spEntregasPorBoyDataTableAdapter = new DexComanda.Relatorios.dsRelatorioTableAdapters.spEntregasPorBoyDataTableAdapter();
            this.panel1.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsRelatorio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spEntregasPorBoyDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.reportViewEntregas);
            this.panel1.Location = new System.Drawing.Point(4, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 420);
            this.panel1.TabIndex = 0;
            // 
            // reportViewEntregas
            // 
            this.reportViewEntregas.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource3.Name = "dsEntregasPorEntregador";
            reportDataSource3.Value = this.spEntregasPorBoyDataBindingSource;
            this.reportViewEntregas.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewEntregas.LocalReport.ReportEmbeddedResource = "DexComanda.Relatorios.Fechamentos.RelEntregasPorMotoboy.rdlc";
            this.reportViewEntregas.Location = new System.Drawing.Point(0, 0);
            this.reportViewEntregas.Name = "reportViewEntregas";
            this.reportViewEntregas.Size = new System.Drawing.Size(575, 420);
            this.reportViewEntregas.TabIndex = 0;
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.chkTodos);
            this.grpFiltro.Controls.Add(this.label4);
            this.grpFiltro.Controls.Add(this.cbxEntregador);
            this.grpFiltro.Controls.Add(this.btnConsultar);
            this.grpFiltro.Controls.Add(this.dataFim);
            this.grpFiltro.Controls.Add(this.label3);
            this.grpFiltro.Controls.Add(this.dataInicio);
            this.grpFiltro.Controls.Add(this.label2);
            this.grpFiltro.Location = new System.Drawing.Point(4, -1);
            this.grpFiltro.Name = "grpFiltro";
            this.grpFiltro.Size = new System.Drawing.Size(575, 70);
            this.grpFiltro.TabIndex = 1;
            this.grpFiltro.TabStop = false;
            this.grpFiltro.Text = "Filtros";
            // 
            // chkTodos
            // 
            this.chkTodos.AutoSize = true;
            this.chkTodos.Location = new System.Drawing.Point(463, 47);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(56, 17);
            this.chkTodos.TabIndex = 21;
            this.chkTodos.Text = "Todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Entregador";
            // 
            // cbxEntregador
            // 
            this.cbxEntregador.FormattingEnabled = true;
            this.cbxEntregador.Location = new System.Drawing.Point(293, 31);
            this.cbxEntregador.Name = "cbxEntregador";
            this.cbxEntregador.Size = new System.Drawing.Size(164, 21);
            this.cbxEntregador.TabIndex = 19;
            this.cbxEntregador.SelectedIndexChanged += new System.EventHandler(this.cbxEntregador_SelectedIndexChanged);
            this.cbxEntregador.Click += new System.EventHandler(this.cbxEntregador_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(463, 19);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(65, 23);
            this.btnConsultar.TabIndex = 18;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.Consultar);
            // 
            // dataFim
            // 
            this.dataFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dataFim.Location = new System.Drawing.Point(168, 35);
            this.dataFim.Name = "dataFim";
            this.dataFim.Size = new System.Drawing.Size(97, 20);
            this.dataFim.TabIndex = 17;
            this.dataFim.Value = new System.DateTime(2014, 5, 15, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(134, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "até";
            // 
            // dataInicio
            // 
            this.dataInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dataInicio.Location = new System.Drawing.Point(24, 35);
            this.dataInicio.Name = "dataInicio";
            this.dataInicio.Size = new System.Drawing.Size(101, 20);
            this.dataInicio.TabIndex = 15;
            this.dataInicio.Value = new System.DateTime(2014, 5, 15, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(118, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "Período:";
            // 
            // dsRelatorio
            // 
            this.dsRelatorio.DataSetName = "dsRelatorio";
            this.dsRelatorio.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // spEntregasPorBoyDataBindingSource
            // 
            this.spEntregasPorBoyDataBindingSource.DataMember = "spEntregasPorBoyData";
            this.spEntregasPorBoyDataBindingSource.DataSource = this.dsRelatorio;
            // 
            // spEntregasPorBoyDataTableAdapter
            // 
            this.spEntregasPorBoyDataTableAdapter.ClearBeforeFill = true;
            // 
            // frmReportEntregasPorMotoboy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 497);
            this.Controls.Add(this.grpFiltro);
            this.Controls.Add(this.panel1);
            this.Name = "frmReportEntregasPorMotoboy";
            this.Text = "[XSistemas] Entregas por Motoboy";
            this.Load += new System.EventHandler(this.frmReportEntregasPorMotoboy_Load);
            this.panel1.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.grpFiltro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsRelatorio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spEntregasPorBoyDataBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewEntregas;
        private System.Windows.Forms.GroupBox grpFiltro;
        private System.Windows.Forms.CheckBox chkTodos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxEntregador;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DateTimePicker dataFim;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dataInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource spEntregasPorBoyDataBindingSource;
        private dsRelatorio dsRelatorio;
        private dsRelatorioTableAdapters.spEntregasPorBoyDataTableAdapter spEntregasPorBoyDataTableAdapter;

    }
}