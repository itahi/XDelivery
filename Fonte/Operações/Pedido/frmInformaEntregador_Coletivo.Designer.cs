namespace DexComanda.Operações.Pedido
{
    partial class frmInformaEntregador_Coletivo
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
            this.grpPedidos = new System.Windows.Forms.GroupBox();
            this.PedidosGridView = new System.Windows.Forms.DataGridView();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodEntregador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Entregador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbxEntregador = new System.Windows.Forms.ComboBox();
            this.txtCodPedido = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirma = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpPedidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PedidosGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPedidos
            // 
            this.grpPedidos.Controls.Add(this.PedidosGridView);
            this.grpPedidos.Location = new System.Drawing.Point(12, 49);
            this.grpPedidos.Name = "grpPedidos";
            this.grpPedidos.Size = new System.Drawing.Size(262, 195);
            this.grpPedidos.TabIndex = 0;
            this.grpPedidos.TabStop = false;
            this.grpPedidos.Text = "Pedidos";
            // 
            // PedidosGridView
            // 
            this.PedidosGridView.AllowUserToAddRows = false;
            this.PedidosGridView.AllowUserToDeleteRows = false;
            this.PedidosGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PedidosGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PedidosGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.CodEntregador,
            this.Entregador});
            this.PedidosGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PedidosGridView.Location = new System.Drawing.Point(3, 16);
            this.PedidosGridView.Name = "PedidosGridView";
            this.PedidosGridView.ReadOnly = true;
            this.PedidosGridView.Size = new System.Drawing.Size(256, 176);
            this.PedidosGridView.TabIndex = 0;
            // 
            // Codigo
            // 
            this.Codigo.HeaderText = "CodPedido";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            // 
            // CodEntregador
            // 
            this.CodEntregador.HeaderText = "CodEntregador";
            this.CodEntregador.Name = "CodEntregador";
            this.CodEntregador.ReadOnly = true;
            this.CodEntregador.Visible = false;
            // 
            // Entregador
            // 
            this.Entregador.HeaderText = "Entregador";
            this.Entregador.Name = "Entregador";
            this.Entregador.ReadOnly = true;
            // 
            // cbxEntregador
            // 
            this.cbxEntregador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEntregador.FormattingEnabled = true;
            this.cbxEntregador.Location = new System.Drawing.Point(69, 22);
            this.cbxEntregador.Name = "cbxEntregador";
            this.cbxEntregador.Size = new System.Drawing.Size(154, 21);
            this.cbxEntregador.TabIndex = 1;
            this.toolTip1.SetToolTip(this.cbxEntregador, "Selecione o entregador");
            this.cbxEntregador.DropDown += new System.EventHandler(this.cbxEntregador_DropDown);
            // 
            // txtCodPedido
            // 
            this.txtCodPedido.Location = new System.Drawing.Point(11, 22);
            this.txtCodPedido.Name = "txtCodPedido";
            this.txtCodPedido.Size = new System.Drawing.Size(52, 20);
            this.txtCodPedido.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtCodPedido, "Informe o código do pedido");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Codigo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Entregador";
            // 
            // btnConfirma
            // 
            this.btnConfirma.Location = new System.Drawing.Point(199, 250);
            this.btnConfirma.Name = "btnConfirma";
            this.btnConfirma.Size = new System.Drawing.Size(75, 23);
            this.btnConfirma.TabIndex = 5;
            this.btnConfirma.Text = "Confirmar";
            this.toolTip1.SetToolTip(this.btnConfirma, "Confirme todas alteracoes?");
            this.btnConfirma.UseVisualStyleBackColor = true;
            this.btnConfirma.Click += new System.EventHandler(this.btnConfirma_Click);
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(229, 19);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(45, 23);
            this.Add.TabIndex = 6;
            this.Add.Text = "ADD";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.AdicionaPedido);
            // 
            // frmInformaEntregador_Coletivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 279);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.btnConfirma);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCodPedido);
            this.Controls.Add(this.cbxEntregador);
            this.Controls.Add(this.grpPedidos);
            this.MaximizeBox = false;
            this.Name = "frmInformaEntregador_Coletivo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[xSistemas] Inf. Entregador";
            this.grpPedidos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PedidosGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPedidos;
        private System.Windows.Forms.ComboBox cbxEntregador;
        private System.Windows.Forms.TextBox txtCodPedido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView PedidosGridView;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnConfirma;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodEntregador;
        private System.Windows.Forms.DataGridViewTextBoxColumn Entregador;
    }
}