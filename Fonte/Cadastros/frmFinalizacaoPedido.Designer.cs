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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.b = new System.Windows.Forms.Label();
            this.lblFalta = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.gridFormasPagamento = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridFormasPagamento)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pedido R$:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 310);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Falta R$:";
            // 
            // b
            // 
            this.b.AutoSize = true;
            this.b.BackColor = System.Drawing.Color.Red;
            this.b.Font = new System.Drawing.Font("Showcard Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.b.Location = new System.Drawing.Point(125, 278);
            this.b.Name = "b";
            this.b.Size = new System.Drawing.Size(52, 23);
            this.b.TabIndex = 4;
            this.b.Text = "0,00";
            // 
            // lblFalta
            // 
            this.lblFalta.AutoSize = true;
            this.lblFalta.BackColor = System.Drawing.Color.Red;
            this.lblFalta.Font = new System.Drawing.Font("Showcard Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFalta.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFalta.Location = new System.Drawing.Point(125, 310);
            this.lblFalta.Name = "lblFalta";
            this.lblFalta.Size = new System.Drawing.Size(52, 23);
            this.lblFalta.TabIndex = 5;
            this.lblFalta.Text = "0,00";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(255, 293);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 37);
            this.button1.TabIndex = 6;
            this.button1.Text = "Finalizar [F12]";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // gridFormasPagamento
            // 
            this.gridFormasPagamento.AllowUserToAddRows = false;
            this.gridFormasPagamento.AllowUserToDeleteRows = false;
            this.gridFormasPagamento.AllowUserToOrderColumns = true;
            this.gridFormasPagamento.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridFormasPagamento.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gridFormasPagamento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFormasPagamento.Location = new System.Drawing.Point(1, 12);
            this.gridFormasPagamento.MultiSelect = false;
            this.gridFormasPagamento.Name = "gridFormasPagamento";
            this.gridFormasPagamento.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridFormasPagamento.Size = new System.Drawing.Size(372, 263);
            this.gridFormasPagamento.TabIndex = 7;
            // 
            // frmFinalizacaoPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 339);
            this.Controls.Add(this.gridFormasPagamento);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblFalta);
            this.Controls.Add(this.b);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFinalizacaoPedido";
            this.Text = "[xSistemas] Finalizacao do Pedido";
            this.Load += new System.EventHandler(this.frmFinalizacaoPedido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridFormasPagamento)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label b;
        private System.Windows.Forms.Label lblFalta;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView gridFormasPagamento;
    }
}