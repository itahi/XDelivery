namespace DexComanda.Cadastros.Pedido
{
    partial class frmFinalizaDelivery
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
            this.gridFormasPagamento = new System.Windows.Forms.DataGridView();
            this.lblTotalPedido = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTrocoPara = new System.Windows.Forms.TextBox();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNumeroPedido = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridFormasPagamento)).BeginInit();
            this.SuspendLayout();
            // 
            // gridFormasPagamento
            // 
            this.gridFormasPagamento.AllowUserToAddRows = false;
            this.gridFormasPagamento.AllowUserToDeleteRows = false;
            this.gridFormasPagamento.AllowUserToOrderColumns = true;
            this.gridFormasPagamento.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridFormasPagamento.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gridFormasPagamento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFormasPagamento.Location = new System.Drawing.Point(2, 46);
            this.gridFormasPagamento.MultiSelect = false;
            this.gridFormasPagamento.Name = "gridFormasPagamento";
            this.gridFormasPagamento.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridFormasPagamento.Size = new System.Drawing.Size(406, 223);
            this.gridFormasPagamento.TabIndex = 10;
            this.gridFormasPagamento.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.ValidaTexto);
            // 
            // lblTotalPedido
            // 
            this.lblTotalPedido.AutoSize = true;
            this.lblTotalPedido.BackColor = System.Drawing.Color.White;
            this.lblTotalPedido.Font = new System.Drawing.Font("Showcard Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotalPedido.Location = new System.Drawing.Point(122, 273);
            this.lblTotalPedido.Name = "lblTotalPedido";
            this.lblTotalPedido.Size = new System.Drawing.Size(52, 23);
            this.lblTotalPedido.TabIndex = 9;
            this.lblTotalPedido.Text = "0,00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Pedido R$:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Troco P/:";
            // 
            // txtTrocoPara
            // 
            this.txtTrocoPara.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtTrocoPara.Location = new System.Drawing.Point(123, 308);
            this.txtTrocoPara.Name = "txtTrocoPara";
            this.txtTrocoPara.Size = new System.Drawing.Size(57, 26);
            this.txtTrocoPara.TabIndex = 12;
            this.txtTrocoPara.Text = "0,00";
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(287, 283);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(121, 48);
            this.btnFinalizar.TabIndex = 13;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Click += new System.EventHandler(this.FinalizarPedido);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Sitka Display", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(74, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 28);
            this.label3.TabIndex = 14;
            this.label3.Text = "Finalizar Pedido";
            // 
            // lblNumeroPedido
            // 
            this.lblNumeroPedido.AutoSize = true;
            this.lblNumeroPedido.Font = new System.Drawing.Font("Sitka Display", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroPedido.Location = new System.Drawing.Point(232, 4);
            this.lblNumeroPedido.Name = "lblNumeroPedido";
            this.lblNumeroPedido.Size = new System.Drawing.Size(48, 39);
            this.lblNumeroPedido.TabIndex = 15;
            this.lblNumeroPedido.Text = "txt";
            this.lblNumeroPedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmFinalizaDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 351);
            this.Controls.Add(this.lblNumeroPedido);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.txtTrocoPara);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gridFormasPagamento);
            this.Controls.Add(this.lblTotalPedido);
            this.Controls.Add(this.label1);
            this.Name = "frmFinalizaDelivery";
            this.Text = "[xSistemas] Finalizar Pedido";
            this.Load += new System.EventHandler(this.frmFinalizaDelivery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridFormasPagamento)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridFormasPagamento;
        private System.Windows.Forms.Label lblTotalPedido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTrocoPara;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNumeroPedido;
    }
}