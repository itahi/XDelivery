namespace DexComanda.Cadastros
{
    partial class frmFinalizacaoPedido
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalPedido = new System.Windows.Forms.Label();
            this.lblFalta = new System.Windows.Forms.Label();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.gridFormasPagamento = new System.Windows.Forms.DataGridView();
            this.lblFinalizacao = new System.Windows.Forms.Label();
            this.lblNumeroMesa = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridFormasPagamento)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pedido R$:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Falta R$:";
            // 
            // lblTotalPedido
            // 
            this.lblTotalPedido.AutoSize = true;
            this.lblTotalPedido.BackColor = System.Drawing.Color.White;
            this.lblTotalPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotalPedido.Location = new System.Drawing.Point(119, 291);
            this.lblTotalPedido.Name = "lblTotalPedido";
            this.lblTotalPedido.Size = new System.Drawing.Size(49, 24);
            this.lblTotalPedido.TabIndex = 4;
            this.lblTotalPedido.Text = "0,00";
            this.toolTip1.SetToolTip(this.lblTotalPedido, "Valor total da Mesa");
            // 
            // lblFalta
            // 
            this.lblFalta.AutoSize = true;
            this.lblFalta.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblFalta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFalta.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFalta.Location = new System.Drawing.Point(119, 323);
            this.lblFalta.Name = "lblFalta";
            this.lblFalta.Size = new System.Drawing.Size(49, 24);
            this.lblFalta.TabIndex = 5;
            this.lblFalta.Text = "0,00";
            this.toolTip1.SetToolTip(this.lblFalta, "Valor que falta para fechar a conta");
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(322, 309);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(85, 37);
            this.btnFinalizar.TabIndex = 6;
            this.btnFinalizar.Text = "Finalizar [F12]";
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Click += new System.EventHandler(this.Finaliza);
            // 
            // gridFormasPagamento
            // 
            this.gridFormasPagamento.AllowUserToAddRows = false;
            this.gridFormasPagamento.AllowUserToDeleteRows = false;
            this.gridFormasPagamento.AllowUserToOrderColumns = true;
            this.gridFormasPagamento.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridFormasPagamento.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gridFormasPagamento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFormasPagamento.Location = new System.Drawing.Point(1, 52);
            this.gridFormasPagamento.MultiSelect = false;
            this.gridFormasPagamento.Name = "gridFormasPagamento";
            this.gridFormasPagamento.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridFormasPagamento.Size = new System.Drawing.Size(406, 223);
            this.gridFormasPagamento.TabIndex = 7;
            this.toolTip1.SetToolTip(this.gridFormasPagamento, "Preencha a coluna valor , com o total pago em cada forma de pagamento");
            this.gridFormasPagamento.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridFormasPagamento_CellValueChanged);
            this.gridFormasPagamento.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Valida);
            // 
            // lblFinalizacao
            // 
            this.lblFinalizacao.AutoSize = true;
            this.lblFinalizacao.Font = new System.Drawing.Font("Sitka Banner", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinalizacao.Location = new System.Drawing.Point(50, 21);
            this.lblFinalizacao.Name = "lblFinalizacao";
            this.lblFinalizacao.Size = new System.Drawing.Size(127, 23);
            this.lblFinalizacao.TabIndex = 8;
            this.lblFinalizacao.Text = "Fechamento Mesa";
            // 
            // lblNumeroMesa
            // 
            this.lblNumeroMesa.AutoSize = true;
            this.lblNumeroMesa.Font = new System.Drawing.Font("Sitka Banner", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroMesa.Location = new System.Drawing.Point(203, 2);
            this.lblNumeroMesa.Name = "lblNumeroMesa";
            this.lblNumeroMesa.Size = new System.Drawing.Size(55, 47);
            this.lblNumeroMesa.TabIndex = 9;
            this.lblNumeroMesa.Text = "txt";
            // 
            // txtNumero
            // 
            this.txtNumero.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumero.Location = new System.Drawing.Point(245, 317);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(71, 29);
            this.txtNumero.TabIndex = 35;
            this.txtNumero.Text = "1";
            this.toolTip1.SetToolTip(this.txtNumero, "Numero de pessoas na mesa para dividir a conta");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(219, 291);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 20);
            this.label4.TabIndex = 36;
            this.label4.Text = "Nº Pessoas";
            // 
            // frmFinalizacaoPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 354);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.lblNumeroMesa);
            this.Controls.Add(this.lblFinalizacao);
            this.Controls.Add(this.gridFormasPagamento);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.lblFalta);
            this.Controls.Add(this.lblTotalPedido);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFinalizacaoPedido";
            this.Text = "[xSistemas] Finalizacao do Pedido";
            this.Load += new System.EventHandler(this.frmFinalizacaoPedido_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFinalizacaoPedido_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gridFormasPagamento)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalPedido;
        private System.Windows.Forms.Label lblFalta;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.DataGridView gridFormasPagamento;
        private System.Windows.Forms.Label lblFinalizacao;
        private System.Windows.Forms.Label lblNumeroMesa;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label4;
    }
}