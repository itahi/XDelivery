namespace DexComanda.Cadastros.Empresa
{
    partial class frmHorariosEntrega
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridViewHorarios = new System.Windows.Forms.DataGridView();
            this.dtLimite = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtEntregaFim = new System.Windows.Forms.DateTimePicker();
            this.dtEntregaInicio = new System.Windows.Forms.DateTimePicker();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.chkOnlineSN = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHorarios)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.gridViewHorarios);
            this.groupBox1.Location = new System.Drawing.Point(10, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 270);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Horarios ";
            // 
            // gridViewHorarios
            // 
            this.gridViewHorarios.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gridViewHorarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewHorarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridViewHorarios.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.gridViewHorarios.Location = new System.Drawing.Point(3, 16);
            this.gridViewHorarios.Name = "gridViewHorarios";
            this.gridViewHorarios.Size = new System.Drawing.Size(349, 251);
            this.gridViewHorarios.TabIndex = 0;
            this.gridViewHorarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BuscaRowIndex);
            // 
            // dtLimite
            // 
            this.dtLimite.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtLimite.Location = new System.Drawing.Point(12, 32);
            this.dtLimite.Name = "dtLimite";
            this.dtLimite.Size = new System.Drawing.Size(66, 20);
            this.dtLimite.TabIndex = 1;
            this.toolTip1.SetToolTip(this.dtLimite, "Horario Limite que o pedido será aceito para esse grupo de horas");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Limite Pedido";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dtEntregaFim);
            this.groupBox2.Controls.Add(this.dtEntregaInicio);
            this.groupBox2.Location = new System.Drawing.Point(118, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 45);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Horario Entrega";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Às";
            // 
            // dtEntregaFim
            // 
            this.dtEntregaFim.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtEntregaFim.Location = new System.Drawing.Point(115, 17);
            this.dtEntregaFim.Name = "dtEntregaFim";
            this.dtEntregaFim.Size = new System.Drawing.Size(66, 20);
            this.dtEntregaFim.TabIndex = 5;
            this.toolTip1.SetToolTip(this.dtEntregaFim, "Horario fim da entrega a ser exibida");
            // 
            // dtEntregaInicio
            // 
            this.dtEntregaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtEntregaInicio.Location = new System.Drawing.Point(14, 18);
            this.dtEntregaInicio.Name = "dtEntregaInicio";
            this.dtEntregaInicio.Size = new System.Drawing.Size(66, 20);
            this.dtEntregaInicio.TabIndex = 4;
            this.toolTip1.SetToolTip(this.dtEntregaInicio, "Horario inicio da entrega a ser exibida");
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(12, 99);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(110, 36);
            this.btnAdicionar.TabIndex = 6;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(229, 99);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(133, 36);
            this.btnEditar.TabIndex = 7;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.EditarRegistro);
            // 
            // chkOnlineSN
            // 
            this.chkOnlineSN.AutoSize = true;
            this.chkOnlineSN.Location = new System.Drawing.Point(12, 67);
            this.chkOnlineSN.Name = "chkOnlineSN";
            this.chkOnlineSN.Size = new System.Drawing.Size(65, 17);
            this.chkOnlineSN.TabIndex = 8;
            this.chkOnlineSN.Text = "AtivoSN";
            this.chkOnlineSN.UseVisualStyleBackColor = true;
            // 
            // frmHorariosEntrega
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 410);
            this.Controls.Add(this.chkOnlineSN);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtLimite);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmHorariosEntrega";
            this.Text = "[xSistemas] Horarios Entrega";
            this.Load += new System.EventHandler(this.frmHorariosEntrega_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHorarios)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gridViewHorarios;
        private System.Windows.Forms.DateTimePicker dtLimite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtEntregaFim;
        private System.Windows.Forms.DateTimePicker dtEntregaInicio;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkOnlineSN;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}