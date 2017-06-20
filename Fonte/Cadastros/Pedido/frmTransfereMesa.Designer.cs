namespace DexComanda.Cadastros.Pedido
{
    partial class frmTransfereMesa
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
            this.cbxListaMesasO = new System.Windows.Forms.ComboBox();
            this.gridOrigem = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxListaMesasD = new System.Windows.Forms.ComboBox();
            this.gridDestino = new System.Windows.Forms.DataGridView();
            this.btnTransferir = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrigem)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDestino)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxListaMesasO);
            this.groupBox1.Controls.Add(this.gridOrigem);
            this.groupBox1.Location = new System.Drawing.Point(3, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(529, 208);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Origem";
            // 
            // cbxListaMesasO
            // 
            this.cbxListaMesasO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxListaMesasO.FormattingEnabled = true;
            this.cbxListaMesasO.Location = new System.Drawing.Point(9, 19);
            this.cbxListaMesasO.Name = "cbxListaMesasO";
            this.cbxListaMesasO.Size = new System.Drawing.Size(65, 21);
            this.cbxListaMesasO.TabIndex = 2;
            this.cbxListaMesasO.SelectionChangeCommitted += new System.EventHandler(this.cbxListaMesasO_SelectionChangeCommitted);
            // 
            // gridOrigem
            // 
            this.gridOrigem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOrigem.Location = new System.Drawing.Point(6, 46);
            this.gridOrigem.Name = "gridOrigem";
            this.gridOrigem.Size = new System.Drawing.Size(504, 147);
            this.gridOrigem.TabIndex = 1;
            this.gridOrigem.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuAuxiliar);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxListaMesasD);
            this.groupBox2.Controls.Add(this.gridDestino);
            this.groupBox2.Location = new System.Drawing.Point(3, 282);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(529, 188);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destino";
            // 
            // cbxListaMesasD
            // 
            this.cbxListaMesasD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxListaMesasD.FormattingEnabled = true;
            this.cbxListaMesasD.Location = new System.Drawing.Point(6, 19);
            this.cbxListaMesasD.Name = "cbxListaMesasD";
            this.cbxListaMesasD.Size = new System.Drawing.Size(65, 21);
            this.cbxListaMesasD.TabIndex = 2;
            this.cbxListaMesasD.SelectionChangeCommitted += new System.EventHandler(this.cbxListaMesasD_SelectionChangeCommitted);
            // 
            // gridDestino
            // 
            this.gridDestino.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDestino.Location = new System.Drawing.Point(6, 46);
            this.gridDestino.Name = "gridDestino";
            this.gridDestino.Size = new System.Drawing.Size(504, 124);
            this.gridDestino.TabIndex = 1;
            // 
            // btnTransferir
            // 
            this.btnTransferir.Location = new System.Drawing.Point(218, 222);
            this.btnTransferir.Name = "btnTransferir";
            this.btnTransferir.Size = new System.Drawing.Size(156, 54);
            this.btnTransferir.TabIndex = 4;
            this.btnTransferir.Text = "Transferir Mesa Completa";
            this.toolTip1.SetToolTip(this.btnTransferir, "Transfere todos itens da mesa Origem para mesa destino");
            this.btnTransferir.UseVisualStyleBackColor = true;
            this.btnTransferir.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmTransfereMesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 475);
            this.Controls.Add(this.btnTransferir);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmTransfereMesa";
            this.Text = "[xSistemas] Transferencia Mesa";
            this.Load += new System.EventHandler(this.frmTransfereMesa_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOrigem)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDestino)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gridOrigem;
        private System.Windows.Forms.ComboBox cbxListaMesasO;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbxListaMesasD;
        private System.Windows.Forms.DataGridView gridDestino;
        private System.Windows.Forms.Button btnTransferir;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}