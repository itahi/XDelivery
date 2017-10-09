namespace DexComanda.Operações.Ações
{
    partial class frmMapaClientes
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
            this.grpTipoFiltro = new System.Windows.Forms.GroupBox();
            this.rbComprandoAgora = new System.Windows.Forms.RadioButton();
            this.rbSumido = new System.Windows.Forms.RadioButton();
            this.grpMapa = new System.Windows.Forms.GroupBox();
            this.panel = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblmsg = new System.Windows.Forms.Label();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.grpTipoFiltro.SuspendLayout();
            this.grpMapa.SuspendLayout();
            this.panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTipoFiltro
            // 
            this.grpTipoFiltro.Controls.Add(this.rbComprandoAgora);
            this.grpTipoFiltro.Controls.Add(this.rbSumido);
            this.grpTipoFiltro.Location = new System.Drawing.Point(12, 4);
            this.grpTipoFiltro.Name = "grpTipoFiltro";
            this.grpTipoFiltro.Size = new System.Drawing.Size(175, 93);
            this.grpTipoFiltro.TabIndex = 0;
            this.grpTipoFiltro.TabStop = false;
            this.grpTipoFiltro.Text = "Filtros";
            // 
            // rbComprandoAgora
            // 
            this.rbComprandoAgora.AutoSize = true;
            this.rbComprandoAgora.Location = new System.Drawing.Point(6, 53);
            this.rbComprandoAgora.Name = "rbComprandoAgora";
            this.rbComprandoAgora.Size = new System.Drawing.Size(109, 17);
            this.rbComprandoAgora.TabIndex = 13;
            this.rbComprandoAgora.Text = "Comprando agora";
            this.rbComprandoAgora.UseVisualStyleBackColor = true;
            // 
            // rbSumido
            // 
            this.rbSumido.AutoSize = true;
            this.rbSumido.Checked = true;
            this.rbSumido.Location = new System.Drawing.Point(6, 19);
            this.rbSumido.Name = "rbSumido";
            this.rbSumido.Size = new System.Drawing.Size(99, 17);
            this.rbSumido.TabIndex = 4;
            this.rbSumido.TabStop = true;
            this.rbSumido.Text = "Cliente \'Sumido\'";
            this.rbSumido.UseVisualStyleBackColor = true;
            // 
            // grpMapa
            // 
            this.grpMapa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMapa.Controls.Add(this.panel);
            this.grpMapa.Controls.Add(this.gmap);
            this.grpMapa.Location = new System.Drawing.Point(12, 98);
            this.grpMapa.Name = "grpMapa";
            this.grpMapa.Size = new System.Drawing.Size(496, 369);
            this.grpMapa.TabIndex = 1;
            this.grpMapa.TabStop = false;
            this.grpMapa.Text = "Mapa";
            // 
            // panel
            // 
            this.panel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel.Controls.Add(this.progressBar1);
            this.panel.Controls.Add(this.lblmsg);
            this.panel.Location = new System.Drawing.Point(115, 99);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(260, 62);
            this.panel.TabIndex = 62;
            this.panel.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(54, 36);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(173, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // lblmsg
            // 
            this.lblmsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblmsg.AutoSize = true;
            this.lblmsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.Location = new System.Drawing.Point(51, 10);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(0, 16);
            this.lblmsg.TabIndex = 0;
            this.lblmsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gmap
            // 
            this.gmap.AllowDrop = true;
            this.gmap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gmap.AutoScroll = true;
            this.gmap.AutoSize = true;
            this.gmap.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.ImeMode = System.Windows.Forms.ImeMode.On;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(6, 19);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 18;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(484, 344);
            this.gmap.TabIndex = 1;
            this.gmap.Zoom = 16D;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(433, 35);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(75, 39);
            this.btnFiltrar.TabIndex = 0;
            this.btnFiltrar.Text = "Exibir";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtFim);
            this.groupBox1.Controls.Add(this.dtInicio);
            this.groupBox1.Location = new System.Drawing.Point(193, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 92);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Periodo Filtro";
            // 
            // dtFim
            // 
            this.dtFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFim.Location = new System.Drawing.Point(103, 37);
            this.dtFim.Name = "dtFim";
            this.dtFim.Size = new System.Drawing.Size(81, 20);
            this.dtFim.TabIndex = 1;
            // 
            // dtInicio
            // 
            this.dtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtInicio.Location = new System.Drawing.Point(6, 37);
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Size = new System.Drawing.Size(81, 20);
            this.dtInicio.TabIndex = 0;
            // 
            // frmMapaClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 471);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpMapa);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.grpTipoFiltro);
            this.Name = "frmMapaClientes";
            this.Text = "[xSistemas] Mapa de Clientes";
            this.Load += new System.EventHandler(this.frmMapaClientes_Load);
            this.grpTipoFiltro.ResumeLayout(false);
            this.grpTipoFiltro.PerformLayout();
            this.grpMapa.ResumeLayout(false);
            this.grpMapa.PerformLayout();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTipoFiltro;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.GroupBox grpMapa;
        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.RadioButton rbSumido;
        private System.Windows.Forms.RadioButton rbComprandoAgora;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}