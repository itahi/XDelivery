﻿namespace DexComanda.Cadastros.Pedido
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxListaMesasO = new System.Windows.Forms.ComboBox();
            this.gridOrigem = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxListaMesasD = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrigem)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxListaMesasO);
            this.groupBox1.Controls.Add(this.gridOrigem);
            this.groupBox1.Location = new System.Drawing.Point(3, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 211);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Origem";
            // 
            // cbxListaMesasO
            // 
            this.cbxListaMesasO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxListaMesasO.FormattingEnabled = true;
            this.cbxListaMesasO.Location = new System.Drawing.Point(21, 26);
            this.cbxListaMesasO.Name = "cbxListaMesasO";
            this.cbxListaMesasO.Size = new System.Drawing.Size(65, 21);
            this.cbxListaMesasO.TabIndex = 2;
            this.cbxListaMesasO.SelectedIndexChanged += new System.EventHandler(this.cbxListaMesasO_SelectedIndexChanged);
            this.cbxListaMesasO.SelectionChangeCommitted += new System.EventHandler(this.cbxListaMesasO_SelectionChangeCommitted);
            // 
            // gridOrigem
            // 
            this.gridOrigem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOrigem.Location = new System.Drawing.Point(6, 64);
            this.gridOrigem.Name = "gridOrigem";
            this.gridOrigem.Size = new System.Drawing.Size(771, 138);
            this.gridOrigem.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxListaMesasD);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(3, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(784, 211);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destino";
            // 
            // cbxListaMesasD
            // 
            this.cbxListaMesasD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxListaMesasD.FormattingEnabled = true;
            this.cbxListaMesasD.Location = new System.Drawing.Point(21, 26);
            this.cbxListaMesasD.Name = "cbxListaMesasD";
            this.cbxListaMesasD.Size = new System.Drawing.Size(65, 21);
            this.cbxListaMesasD.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 64);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(771, 138);
            this.dataGridView1.TabIndex = 1;
            // 
            // frmTransfereMesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 465);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmTransfereMesa";
            this.Text = "[xSistemas] Transferencia Mesa";
            this.Load += new System.EventHandler(this.frmTransfereMesa_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOrigem)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gridOrigem;
        private System.Windows.Forms.ComboBox cbxListaMesasO;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbxListaMesasD;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}