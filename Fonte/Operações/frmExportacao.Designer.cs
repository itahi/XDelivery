namespace DexComanda
{
    partial class frmExportacao
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbExportar = new System.Windows.Forms.TabPage();
            this.btnExport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkConfig = new System.Windows.Forms.CheckBox();
            this.chkPedi = new System.Windows.Forms.CheckBox();
            this.chkGarc = new System.Windows.Forms.CheckBox();
            this.chkClie = new System.Windows.Forms.CheckBox();
            this.chkProd = new System.Windows.Forms.CheckBox();
            this.tbImportar = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tbExportar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbExportar);
            this.tabControl1.Controls.Add(this.tbImportar);
            this.tabControl1.Location = new System.Drawing.Point(2, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(425, 301);
            this.tabControl1.TabIndex = 0;
            // 
            // tbExportar
            // 
            this.tbExportar.Controls.Add(this.btnExport);
            this.tbExportar.Controls.Add(this.panel1);
            this.tbExportar.Location = new System.Drawing.Point(4, 22);
            this.tbExportar.Name = "tbExportar";
            this.tbExportar.Padding = new System.Windows.Forms.Padding(3);
            this.tbExportar.Size = new System.Drawing.Size(417, 275);
            this.tbExportar.TabIndex = 0;
            this.tbExportar.Text = "Exportar Dados";
            this.tbExportar.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(6, 132);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Exportar";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.ExportaArquivos);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkConfig);
            this.panel1.Controls.Add(this.chkPedi);
            this.panel1.Controls.Add(this.chkGarc);
            this.panel1.Controls.Add(this.chkClie);
            this.panel1.Controls.Add(this.chkProd);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 100);
            this.panel1.TabIndex = 0;
            // 
            // chkConfig
            // 
            this.chkConfig.AutoSize = true;
            this.chkConfig.Location = new System.Drawing.Point(108, 35);
            this.chkConfig.Name = "chkConfig";
            this.chkConfig.Size = new System.Drawing.Size(89, 17);
            this.chkConfig.TabIndex = 4;
            this.chkConfig.Text = "Configuracao";
            this.chkConfig.UseVisualStyleBackColor = true;
            // 
            // chkPedi
            // 
            this.chkPedi.AutoSize = true;
            this.chkPedi.Location = new System.Drawing.Point(108, 7);
            this.chkPedi.Name = "chkPedi";
            this.chkPedi.Size = new System.Drawing.Size(64, 17);
            this.chkPedi.TabIndex = 3;
            this.chkPedi.Text = "Pedidos";
            this.chkPedi.UseVisualStyleBackColor = true;
            // 
            // chkGarc
            // 
            this.chkGarc.AutoSize = true;
            this.chkGarc.Location = new System.Drawing.Point(8, 66);
            this.chkGarc.Name = "chkGarc";
            this.chkGarc.Size = new System.Drawing.Size(66, 17);
            this.chkGarc.TabIndex = 2;
            this.chkGarc.Text = "Garçons";
            this.chkGarc.UseVisualStyleBackColor = true;
            // 
            // chkClie
            // 
            this.chkClie.AutoSize = true;
            this.chkClie.Location = new System.Drawing.Point(8, 35);
            this.chkClie.Name = "chkClie";
            this.chkClie.Size = new System.Drawing.Size(63, 17);
            this.chkClie.TabIndex = 1;
            this.chkClie.Text = "Clientes";
            this.chkClie.UseVisualStyleBackColor = true;
            // 
            // chkProd
            // 
            this.chkProd.AutoSize = true;
            this.chkProd.Location = new System.Drawing.Point(8, 7);
            this.chkProd.Name = "chkProd";
            this.chkProd.Size = new System.Drawing.Size(68, 17);
            this.chkProd.TabIndex = 0;
            this.chkProd.Text = "Produtos";
            this.chkProd.UseVisualStyleBackColor = true;
            // 
            // tbImportar
            // 
            this.tbImportar.Location = new System.Drawing.Point(4, 22);
            this.tbImportar.Name = "tbImportar";
            this.tbImportar.Padding = new System.Windows.Forms.Padding(3);
            this.tbImportar.Size = new System.Drawing.Size(417, 275);
            this.tbImportar.TabIndex = 1;
            this.tbImportar.Text = "Importar";
            this.tbImportar.UseVisualStyleBackColor = true;
            // 
            // frmExportacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 306);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmExportacao";
            this.Text = "DEX [Geração de Arquivos Mobile]";
            this.tabControl1.ResumeLayout(false);
            this.tbExportar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbExportar;
        private System.Windows.Forms.TabPage tbImportar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkConfig;
        private System.Windows.Forms.CheckBox chkPedi;
        private System.Windows.Forms.CheckBox chkGarc;
        private System.Windows.Forms.CheckBox chkClie;
        private System.Windows.Forms.CheckBox chkProd;
        private System.Windows.Forms.Button btnExport;
    }
}