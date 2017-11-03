namespace xConnect_iFood
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEntregue = new System.Windows.Forms.Button();
            this.btnNaEntrega = new System.Windows.Forms.Button();
            this.btnConfimar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridPedido = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.grpConfirmados = new System.Windows.Forms.GroupBox();
            this.gridPedidoConfirmado = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblStatusLoja = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPedido)).BeginInit();
            this.grpConfirmados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPedidoConfirmado)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEntregue);
            this.groupBox1.Controls.Add(this.btnNaEntrega);
            this.groupBox1.Controls.Add(this.btnConfimar);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ações";
            // 
            // btnEntregue
            // 
            this.btnEntregue.Image = global::xConnect_iFood.Properties.Resources.home_icon;
            this.btnEntregue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEntregue.Location = new System.Drawing.Point(263, 19);
            this.btnEntregue.Name = "btnEntregue";
            this.btnEntregue.Size = new System.Drawing.Size(93, 49);
            this.btnEntregue.TabIndex = 2;
            this.btnEntregue.Text = "Entregue";
            this.btnEntregue.UseVisualStyleBackColor = true;
            this.btnEntregue.Click += new System.EventHandler(this.btnEntregue_Click);
            // 
            // btnNaEntrega
            // 
            this.btnNaEntrega.Image = global::xConnect_iFood.Properties.Resources.motorcycle;
            this.btnNaEntrega.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNaEntrega.Location = new System.Drawing.Point(134, 19);
            this.btnNaEntrega.Name = "btnNaEntrega";
            this.btnNaEntrega.Size = new System.Drawing.Size(93, 49);
            this.btnNaEntrega.TabIndex = 1;
            this.btnNaEntrega.Text = "Saiu para\r\nEntrega";
            this.btnNaEntrega.UseVisualStyleBackColor = true;
            this.btnNaEntrega.Click += new System.EventHandler(this.btnNaEntrega_Click);
            // 
            // btnConfimar
            // 
            this.btnConfimar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnConfimar.Image = global::xConnect_iFood.Properties.Resources.confirm1;
            this.btnConfimar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfimar.Location = new System.Drawing.Point(6, 19);
            this.btnConfimar.Name = "btnConfimar";
            this.btnConfimar.Size = new System.Drawing.Size(93, 49);
            this.btnConfimar.TabIndex = 0;
            this.btnConfimar.Text = "Confirmar";
            this.btnConfimar.UseVisualStyleBackColor = true;
            this.btnConfimar.Click += new System.EventHandler(this.btnConfimar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.gridPedido);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(366, 386);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pedidos Pendentes";
            // 
            // gridPedido
            // 
            this.gridPedido.AllowUserToAddRows = false;
            this.gridPedido.AllowUserToDeleteRows = false;
            this.gridPedido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPedido.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gridPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPedido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPedido.Location = new System.Drawing.Point(3, 17);
            this.gridPedido.Name = "gridPedido";
            this.gridPedido.ReadOnly = true;
            this.gridPedido.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.gridPedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPedido.Size = new System.Drawing.Size(360, 366);
            this.gridPedido.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.ExecutaBusca);
            // 
            // grpConfirmados
            // 
            this.grpConfirmados.BackColor = System.Drawing.Color.White;
            this.grpConfirmados.Controls.Add(this.gridPedidoConfirmado);
            this.grpConfirmados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpConfirmados.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpConfirmados.Location = new System.Drawing.Point(384, 116);
            this.grpConfirmados.Name = "grpConfirmados";
            this.grpConfirmados.Size = new System.Drawing.Size(366, 386);
            this.grpConfirmados.TabIndex = 2;
            this.grpConfirmados.TabStop = false;
            this.grpConfirmados.Text = "Pedidos Confirmados";
            // 
            // gridPedidoConfirmado
            // 
            this.gridPedidoConfirmado.AllowUserToAddRows = false;
            this.gridPedidoConfirmado.AllowUserToDeleteRows = false;
            this.gridPedidoConfirmado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPedidoConfirmado.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gridPedidoConfirmado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridPedidoConfirmado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPedidoConfirmado.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.gridPedidoConfirmado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPedidoConfirmado.Location = new System.Drawing.Point(3, 17);
            this.gridPedidoConfirmado.Name = "gridPedidoConfirmado";
            this.gridPedidoConfirmado.ReadOnly = true;
            this.gridPedidoConfirmado.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.gridPedidoConfirmado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPedidoConfirmado.Size = new System.Drawing.Size(360, 366);
            this.gridPedidoConfirmado.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Cliente";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Data";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Valor";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // lblStatusLoja
            // 
            this.lblStatusLoja.AutoSize = true;
            this.lblStatusLoja.Location = new System.Drawing.Point(414, 66);
            this.lblStatusLoja.Name = "lblStatusLoja";
            this.lblStatusLoja.Size = new System.Drawing.Size(0, 13);
            this.lblStatusLoja.TabIndex = 3;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(754, 531);
            this.Controls.Add(this.lblStatusLoja);
            this.Controls.Add(this.grpConfirmados);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[xSistemas] Integração com iFood";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.Resize += new System.EventHandler(this.frmPrincipal_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPedido)).EndInit();
            this.grpConfirmados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPedidoConfirmado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnEntregue;
        private System.Windows.Forms.Button btnNaEntrega;
        private System.Windows.Forms.Button btnConfimar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView gridPedido;
        private System.Windows.Forms.GroupBox grpConfirmados;
        private System.Windows.Forms.DataGridView gridPedidoConfirmado;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Label lblStatusLoja;
    }
}

