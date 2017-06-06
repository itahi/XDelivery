namespace DexComanda.Operações.Financeiro
{
    partial class frmAberturaCaixa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAberturaCaixa));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.horafechamento = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxTurno = new System.Windows.Forms.ComboBox();
            this.cbxCaixas = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtAbertura = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbxFuncionario = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.horafechamento);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cbxTurno);
            this.panel1.Controls.Add(this.cbxCaixas);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtAbertura);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(5, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 104);
            this.panel1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "Fecham.";
            // 
            // horafechamento
            // 
            this.horafechamento.CustomFormat = "";
            this.horafechamento.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.horafechamento.Location = new System.Drawing.Point(94, 79);
            this.horafechamento.Name = "horafechamento";
            this.horafechamento.Size = new System.Drawing.Size(89, 20);
            this.horafechamento.TabIndex = 10;
            this.toolTip1.SetToolTip(this.horafechamento, "Informe a hora que o caixa deve fechar");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(152, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Turno";
            // 
            // cbxTurno
            // 
            this.cbxTurno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTurno.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxTurno.FormattingEnabled = true;
            this.cbxTurno.Items.AddRange(new object[] {
            "Dia",
            "Noite"});
            this.cbxTurno.Location = new System.Drawing.Point(212, 45);
            this.cbxTurno.Name = "cbxTurno";
            this.cbxTurno.Size = new System.Drawing.Size(64, 21);
            this.cbxTurno.TabIndex = 8;
            // 
            // cbxCaixas
            // 
            this.cbxCaixas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCaixas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxCaixas.FormattingEnabled = true;
            this.cbxCaixas.Location = new System.Drawing.Point(94, 45);
            this.cbxCaixas.Name = "cbxCaixas";
            this.cbxCaixas.Size = new System.Drawing.Size(42, 21);
            this.cbxCaixas.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Caixa N°.";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // dtAbertura
            // 
            this.dtAbertura.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtAbertura.Location = new System.Drawing.Point(94, 15);
            this.dtAbertura.Name = "dtAbertura";
            this.dtAbertura.Size = new System.Drawing.Size(89, 20);
            this.dtAbertura.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbxFuncionario);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(5, 116);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(283, 49);
            this.panel2.TabIndex = 1;
            // 
            // cbxFuncionario
            // 
            this.cbxFuncionario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFuncionario.Enabled = false;
            this.cbxFuncionario.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxFuncionario.FormattingEnabled = true;
            this.cbxFuncionario.Location = new System.Drawing.Point(84, 14);
            this.cbxFuncionario.Name = "cbxFuncionario";
            this.cbxFuncionario.Size = new System.Drawing.Size(142, 21);
            this.cbxFuncionario.TabIndex = 2;
            this.cbxFuncionario.SelectedIndexChanged += new System.EventHandler(this.cbxFuncionario_SelectedIndexChanged);
            this.cbxFuncionario.SelectionChangeCommitted += new System.EventHandler(this.cbxFuncionario_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Func.";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtValor);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(5, 169);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(283, 49);
            this.panel3.TabIndex = 2;
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(85, 16);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(142, 20);
            this.txtValor.TabIndex = 3;
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValor_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Vlr. R$";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtSenha);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(5, 223);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(283, 44);
            this.panel4.TabIndex = 3;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(84, 10);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(142, 20);
            this.txtSenha.TabIndex = 4;
            this.txtSenha.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Senha";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(66, 271);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(76, 32);
            this.btnSalvar.TabIndex = 4;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.Salvar);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(148, 271);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(86, 32);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // frmAberturaCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 307);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAberturaCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[XDelivery] Abertuda Caixa";
            this.Load += new System.EventHandler(this.frmAberturaCaixa_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtAbertura;
        private System.Windows.Forms.ComboBox cbxFuncionario;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxCaixas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxTurno;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker horafechamento;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}