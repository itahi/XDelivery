namespace DexComanda.Relatorios.Fechamentos.Novos
{
    partial class frmReportEntregasMotoboy
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.horaFim = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtinicio = new System.Windows.Forms.DateTimePicker();
            this.horaInicio = new System.Windows.Forms.DateTimePicker();
            this.rbResumido = new System.Windows.Forms.RadioButton();
            this.rbDetalhado = new System.Windows.Forms.RadioButton();
            this.btnFiltro = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.crystalReportViewer1);
            this.groupBox2.Location = new System.Drawing.Point(5, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(513, 386);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.EnableRefresh = false;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 16);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.PrintMode = CrystalDecisions.Windows.Forms.PrintMode.PrintOutputController;
            this.crystalReportViewer1.ShowCloseButton = false;
            this.crystalReportViewer1.ShowCopyButton = false;
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.ShowLogo = false;
            this.crystalReportViewer1.ShowParameterPanelButton = false;
            this.crystalReportViewer1.ShowRefreshButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(507, 367);
            this.crystalReportViewer1.TabIndex = 2;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.rbResumido);
            this.groupBox1.Controls.Add(this.rbDetalhado);
            this.groupBox1.Controls.Add(this.btnFiltro);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 85);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtFim);
            this.groupBox3.Controls.Add(this.horaFim);
            this.groupBox3.Location = new System.Drawing.Point(264, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(162, 46);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fim";
            // 
            // dtFim
            // 
            this.dtFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFim.Location = new System.Drawing.Point(6, 19);
            this.dtFim.Name = "dtFim";
            this.dtFim.Size = new System.Drawing.Size(78, 20);
            this.dtFim.TabIndex = 0;
            // 
            // horaFim
            // 
            this.horaFim.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.horaFim.Location = new System.Drawing.Point(90, 19);
            this.horaFim.Name = "horaFim";
            this.horaFim.Size = new System.Drawing.Size(64, 20);
            this.horaFim.TabIndex = 6;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtinicio);
            this.groupBox4.Controls.Add(this.horaInicio);
            this.groupBox4.Location = new System.Drawing.Point(95, 23);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(162, 46);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Inicio";
            // 
            // dtinicio
            // 
            this.dtinicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtinicio.Location = new System.Drawing.Point(6, 19);
            this.dtinicio.Name = "dtinicio";
            this.dtinicio.Size = new System.Drawing.Size(78, 20);
            this.dtinicio.TabIndex = 0;
            // 
            // horaInicio
            // 
            this.horaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.horaInicio.Location = new System.Drawing.Point(90, 19);
            this.horaInicio.Name = "horaInicio";
            this.horaInicio.Size = new System.Drawing.Size(64, 20);
            this.horaInicio.TabIndex = 6;
            // 
            // rbResumido
            // 
            this.rbResumido.AutoSize = true;
            this.rbResumido.Location = new System.Drawing.Point(7, 52);
            this.rbResumido.Name = "rbResumido";
            this.rbResumido.Size = new System.Drawing.Size(72, 17);
            this.rbResumido.TabIndex = 11;
            this.rbResumido.TabStop = true;
            this.rbResumido.Text = "Resumido";
            this.rbResumido.UseVisualStyleBackColor = true;
            // 
            // rbDetalhado
            // 
            this.rbDetalhado.AutoSize = true;
            this.rbDetalhado.Checked = true;
            this.rbDetalhado.Location = new System.Drawing.Point(7, 29);
            this.rbDetalhado.Name = "rbDetalhado";
            this.rbDetalhado.Size = new System.Drawing.Size(74, 17);
            this.rbDetalhado.TabIndex = 10;
            this.rbDetalhado.TabStop = true;
            this.rbDetalhado.Text = "Detalhado";
            this.rbDetalhado.UseVisualStyleBackColor = true;
            // 
            // btnFiltro
            // 
            this.btnFiltro.Location = new System.Drawing.Point(434, 30);
            this.btnFiltro.Name = "btnFiltro";
            this.btnFiltro.Size = new System.Drawing.Size(75, 34);
            this.btnFiltro.TabIndex = 9;
            this.btnFiltro.Text = "Filtrar";
            this.btnFiltro.UseVisualStyleBackColor = true;
            this.btnFiltro.Click += new System.EventHandler(this.Filtrar);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Periodo ";
            // 
            // frmReportEntregasMotoboy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 475);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmReportEntregasMotoboy";
            this.Text = "[xSistemas] Entregas por Motoboy";
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbResumido;
        private System.Windows.Forms.RadioButton rbDetalhado;
        private System.Windows.Forms.Button btnFiltro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker horaFim;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtinicio;
        private System.Windows.Forms.DateTimePicker horaInicio;
    }
}