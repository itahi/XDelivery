namespace DexComanda.Operações.Pedido
{
    partial class frmAlterarStatusPedido
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
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFiltro = new System.Windows.Forms.Button();
            this.gridViewPedidos = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExecutar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.chkSomenteOnline = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSomenteOnline);
            this.groupBox1.Controls.Add(this.btnFiltro);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtFim);
            this.groupBox1.Controls.Add(this.dtInicio);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(651, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // dtInicio
            // 
            this.dtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtInicio.Location = new System.Drawing.Point(99, 34);
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Size = new System.Drawing.Size(97, 20);
            this.dtInicio.TabIndex = 0;
            // 
            // dtFim
            // 
            this.dtFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFim.Location = new System.Drawing.Point(237, 34);
            this.dtFim.Name = "dtFim";
            this.dtFim.Size = new System.Drawing.Size(97, 20);
            this.dtFim.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(192, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Periodo Filtrado";
            // 
            // btnFiltro
            // 
            this.btnFiltro.Location = new System.Drawing.Point(501, 30);
            this.btnFiltro.Name = "btnFiltro";
            this.btnFiltro.Size = new System.Drawing.Size(75, 23);
            this.btnFiltro.TabIndex = 3;
            this.btnFiltro.Text = "Filtrar";
            this.btnFiltro.UseVisualStyleBackColor = true;
            this.btnFiltro.Click += new System.EventHandler(this.FiltrarRegistros);
            // 
            // gridViewPedidos
            // 
            this.gridViewPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridViewPedidos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gridViewPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewPedidos.Location = new System.Drawing.Point(7, 104);
            this.gridViewPedidos.Name = "gridViewPedidos";
            this.gridViewPedidos.Size = new System.Drawing.Size(651, 249);
            this.gridViewPedidos.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(480, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Caso algum dos pedidos abaixo não faça parte da lista selecionada remova-o antes";
            // 
            // btnExecutar
            // 
            this.btnExecutar.Location = new System.Drawing.Point(257, 367);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(75, 23);
            this.btnExecutar.TabIndex = 4;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.UseVisualStyleBackColor = true;
            this.btnExecutar.Click += new System.EventHandler(this.ExecutarAlteracoes);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(362, 367);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // chkSomenteOnline
            // 
            this.chkSomenteOnline.AutoSize = true;
            this.chkSomenteOnline.Checked = true;
            this.chkSomenteOnline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSomenteOnline.Location = new System.Drawing.Point(343, 34);
            this.chkSomenteOnline.Name = "chkSomenteOnline";
            this.chkSomenteOnline.Size = new System.Drawing.Size(142, 17);
            this.chkSomenteOnline.TabIndex = 4;
            this.chkSomenteOnline.Text = "Somente Pedidos Online";
            this.chkSomenteOnline.UseVisualStyleBackColor = true;
            // 
            // frmAlterarStatusPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 402);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnExecutar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gridViewPedidos);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmAlterarStatusPedido";
            this.Text = "[xDelivery] Controle e Alteração de Status do Pedido";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPedidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFiltro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.DataGridView gridViewPedidos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExecutar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.CheckBox chkSomenteOnline;
    }
}