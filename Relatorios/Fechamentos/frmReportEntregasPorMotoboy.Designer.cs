﻿namespace DexComanda.Relatorios.Fechamentos
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
            this.spEntregasPorBoyDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsRelatorio = new DexComanda.Relatorios.dsRelatorio();
            this.panel1 = new System.Windows.Forms.Panel();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.grpFiltro = new System.Windows.Forms.GroupBox();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxEntregador = new System.Windows.Forms.ComboBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.dataFim = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dataInicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.spEntregasPorBoyDataTableAdapter = new DexComanda.Relatorios.dsRelatorioTableAdapters.spEntregasPorBoyDataTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.spEntregasPorBoyDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRelatorio)).BeginInit();
            this.panel1.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // spEntregasPorBoyDataBindingSource
            // 
            this.spEntregasPorBoyDataBindingSource.DataMember = "spEntregasPorBoyData";
            this.spEntregasPorBoyDataBindingSource.DataSource = this.dsRelatorio;
            // 
            // dsRelatorio
            // 
            this.dsRelatorio.DataSetName = "dsRelatorio";
            this.dsRelatorio.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.crystalReportViewer1);
            this.panel1.Location = new System.Drawing.Point(4, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 420);
            this.panel1.TabIndex = 0;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = 0;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.DisplayStatusBar = false;
            this.crystalReportViewer1.DisplayToolbar = false;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = "E:\\#Projetos\\XDelivery\\Fonte\\Relatorios\\Fechamentos\\RelEntregasPorMotoboy.rpt";
            this.crystalReportViewer1.Size = new System.Drawing.Size(575, 420);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
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
            this.chkTodos.Checked = true;
            this.chkTodos.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.MaximizeBox = false;
            this.Name = "frmReportEntregasPorMotoboy";
            this.Text = "s";
            this.Load += new System.EventHandler(this.frmReportEntregasPorMotoboy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spEntregasPorBoyDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRelatorio)).EndInit();
            this.panel1.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.grpFiltro.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
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
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;

    }
}