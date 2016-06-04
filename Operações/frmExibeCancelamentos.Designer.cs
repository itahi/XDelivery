namespace DexComanda.Operações
{
    partial class frmExibeCancelamentos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExibeCancelamentos));
            this.CancGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.CancGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CancGridView
            // 
            this.CancGridView.AllowUserToAddRows = false;
            this.CancGridView.AllowUserToDeleteRows = false;
            this.CancGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CancGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CancGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CancGridView.Location = new System.Drawing.Point(1, 12);
            this.CancGridView.MultiSelect = false;
            this.CancGridView.Name = "CancGridView";
            this.CancGridView.ReadOnly = true;
            this.CancGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CancGridView.Size = new System.Drawing.Size(372, 106);
            this.CancGridView.TabIndex = 5;
            this.CancGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MostraObservacoes);
            // 
            // frmExibeCancelamentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 119);
            this.Controls.Add(this.CancGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExibeCancelamentos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " [XSistemas] Cancelamentos ";
            this.Load += new System.EventHandler(this.frmExibeCancelamentos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CancGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView CancGridView;
    }
}