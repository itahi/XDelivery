namespace DexComanda.Relatorios
{
    partial class frmReportPedidosPorPeriodo
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
            this.vwObterPedidosFinalizadosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsRelatorio = new DexComanda.Relatorios.dsRelatorio();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTelefoneCliente = new System.Windows.Forms.TextBox();
            this.rpvPedidoPorPessoa = new Microsoft.Reporting.WinForms.ReportViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.cbxFormaPagamento = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkCancelados = new System.Windows.Forms.CheckBox();
            this.vwObterPedidosFinalizadosTableAdapter = new DexComanda.Relatorios.dsRelatorioTableAdapters.vwObterPedidosFinalizadosTableAdapter();
            this.label2 = new System.Windows.Forms.Label();
            this.dataInicio = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dataFim = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.vwObterPedidosFinalizadosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRelatorio)).BeginInit();
            this.SuspendLayout();
            // 
            // vwObterPedidosFinalizadosBindingSource
            // 
            this.vwObterPedidosFinalizadosBindingSource.DataMember = "vwObterPedidosFinalizados";
            this.vwObterPedidosFinalizadosBindingSource.DataSource = this.dsRelatorio;
            // 
            // dsRelatorio
            // 
            this.dsRelatorio.DataSetName = "dsRelatorio";
            this.dsRelatorio.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tel. Cliente:";
            // 
            // txtTelefoneCliente
            // 
            this.txtTelefoneCliente.Location = new System.Drawing.Point(13, 17);
            this.txtTelefoneCliente.Name = "txtTelefoneCliente";
            this.txtTelefoneCliente.Size = new System.Drawing.Size(85, 20);
            this.txtTelefoneCliente.TabIndex = 1;
            // 
            // rpvPedidoPorPessoa
            // 
            this.rpvPedidoPorPessoa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rpvPedidoPorPessoa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            reportDataSource1.Name = "dsPedido";
            reportDataSource1.Value = this.vwObterPedidosFinalizadosBindingSource;
            this.rpvPedidoPorPessoa.LocalReport.DataSources.Add(reportDataSource1);
            this.rpvPedidoPorPessoa.LocalReport.ReportEmbeddedResource = "DexComanda.Relatorios.Fechamentos.RelFechamentoDetalhado.rdlc";
            this.rpvPedidoPorPessoa.Location = new System.Drawing.Point(13, 48);
            this.rpvPedidoPorPessoa.Name = "rpvPedidoPorPessoa";
            this.rpvPedidoPorPessoa.Size = new System.Drawing.Size(723, 451);
            this.rpvPedidoPorPessoa.TabIndex = 3;
            this.rpvPedidoPorPessoa.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(587, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Consultar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Consultar);
            // 
            // cbxFormaPagamento
            // 
            this.cbxFormaPagamento.FormattingEnabled = true;
            this.cbxFormaPagamento.Location = new System.Drawing.Point(459, 16);
            this.cbxFormaPagamento.Name = "cbxFormaPagamento";
            this.cbxFormaPagamento.Size = new System.Drawing.Size(121, 21);
            this.cbxFormaPagamento.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(456, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Formas Pagamento";
            // 
            // chkCancelados
            // 
            this.chkCancelados.AutoSize = true;
            this.chkCancelados.Location = new System.Drawing.Point(364, 18);
            this.chkCancelados.Name = "chkCancelados";
            this.chkCancelados.Size = new System.Drawing.Size(82, 17);
            this.chkCancelados.TabIndex = 11;
            this.chkCancelados.Text = "Cancelados";
            this.chkCancelados.UseVisualStyleBackColor = true;
            // 
            // vwObterPedidosFinalizadosTableAdapter
            // 
            this.vwObterPedidosFinalizadosTableAdapter.ClearBeforeFill = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Período:";
            // 
            // dataInicio
            // 
            this.dataInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dataInicio.Location = new System.Drawing.Point(126, 17);
            this.dataInicio.Name = "dataInicio";
            this.dataInicio.Size = new System.Drawing.Size(101, 20);
            this.dataInicio.TabIndex = 5;
            this.dataInicio.Value = new System.DateTime(2014, 5, 15, 0, 0, 0, 0);
            this.dataInicio.ValueChanged += new System.EventHandler(this.dataFim_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "até";
            // 
            // dataFim
            // 
            this.dataFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dataFim.Location = new System.Drawing.Point(261, 17);
            this.dataFim.Name = "dataFim";
            this.dataFim.Size = new System.Drawing.Size(97, 20);
            this.dataFim.TabIndex = 7;
            this.dataFim.Value = new System.DateTime(2014, 5, 15, 0, 0, 0, 0);
            this.dataFim.ValueChanged += new System.EventHandler(this.dataFim_ValueChanged);
            // 
            // frmReportPedidosPorPeriodo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 497);
            this.Controls.Add(this.chkCancelados);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxFormaPagamento);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataFim);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataInicio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rpvPedidoPorPessoa);
            this.Controls.Add(this.txtTelefoneCliente);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(753, 536);
            this.MinimumSize = new System.Drawing.Size(753, 536);
            this.Name = "frmReportPedidosPorPeriodo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatórios - Pedidos por Dia";
            this.Load += new System.EventHandler(this.frmSelecionarCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vwObterPedidosFinalizadosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRelatorio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTelefoneCliente;
        private Microsoft.Reporting.WinForms.ReportViewer rpvPedidoPorPessoa;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbxFormaPagamento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkCancelados;
        private System.Windows.Forms.BindingSource vwObterPedidosFinalizadosBindingSource;
        private dsRelatorio dsRelatorio;
        private dsRelatorioTableAdapters.vwObterPedidosFinalizadosTableAdapter vwObterPedidosFinalizadosTableAdapter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dataInicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dataFim;
    }
}