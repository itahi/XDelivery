namespace DexComanda.Cadastros
{
    partial class frmLancamentoCaixa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLancamentoCaixa));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSolicitante = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxTurno = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxCaixas = new System.Windows.Forms.ComboBox();
            this.cbxFormaPagamento = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbSaida = new System.Windows.Forms.RadioButton();
            this.rbEntrada = new System.Windows.Forms.RadioButton();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtMovimento = new System.Windows.Forms.DateTimePicker();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSolicitante);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cbxTurno);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbxCaixas);
            this.groupBox1.Controls.Add(this.cbxFormaPagamento);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtValor);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rbSaida);
            this.groupBox1.Controls.Add(this.rbEntrada);
            this.groupBox1.Controls.Add(this.txtDescricao);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDocumento);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtMovimento);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 180);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtSolicitante
            // 
            this.txtSolicitante.Location = new System.Drawing.Point(142, 154);
            this.txtSolicitante.Name = "txtSolicitante";
            this.txtSolicitante.Size = new System.Drawing.Size(100, 20);
            this.txtSolicitante.TabIndex = 8;
            this.toolTip1.SetToolTip(this.txtSolicitante, "Nome da pessoa que solicita o valor ");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(139, 138);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Solicitante";
            // 
            // cbxTurno
            // 
            this.cbxTurno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTurno.FormattingEnabled = true;
            this.cbxTurno.Items.AddRange(new object[] {
            "Dia",
            "Noite"});
            this.cbxTurno.Location = new System.Drawing.Point(265, 29);
            this.cbxTurno.Name = "cbxTurno";
            this.cbxTurno.Size = new System.Drawing.Size(46, 21);
            this.cbxTurno.TabIndex = 3;
            this.cbxTurno.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(264, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Turno";
            // 
            // cbxCaixas
            // 
            this.cbxCaixas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCaixas.FormattingEnabled = true;
            this.cbxCaixas.Location = new System.Drawing.Point(214, 29);
            this.cbxCaixas.Name = "cbxCaixas";
            this.cbxCaixas.Size = new System.Drawing.Size(44, 21);
            this.cbxCaixas.TabIndex = 2;
            // 
            // cbxFormaPagamento
            // 
            this.cbxFormaPagamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFormaPagamento.FormattingEnabled = true;
            this.cbxFormaPagamento.Location = new System.Drawing.Point(12, 73);
            this.cbxFormaPagamento.Name = "cbxFormaPagamento";
            this.cbxFormaPagamento.Size = new System.Drawing.Size(171, 21);
            this.cbxFormaPagamento.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Forma Pagamento";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(211, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "N. Caixa";
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(9, 154);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(100, 20);
            this.txtValor.TabIndex = 7;
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValor_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Valor R$";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(327, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Tipo Lançamento";
            // 
            // rbSaida
            // 
            this.rbSaida.AutoSize = true;
            this.rbSaida.Location = new System.Drawing.Point(398, 34);
            this.rbSaida.Name = "rbSaida";
            this.rbSaida.Size = new System.Drawing.Size(52, 17);
            this.rbSaida.TabIndex = 5;
            this.rbSaida.Text = "Saida";
            this.toolTip1.SetToolTip(this.rbSaida, "Use para solicitações de vale, compras de itens , pagamentos de freelancer");
            this.rbSaida.UseVisualStyleBackColor = true;
            this.rbSaida.CheckedChanged += new System.EventHandler(this.rbSaida_CheckedChanged);
            // 
            // rbEntrada
            // 
            this.rbEntrada.AutoSize = true;
            this.rbEntrada.Checked = true;
            this.rbEntrada.Location = new System.Drawing.Point(330, 33);
            this.rbEntrada.Name = "rbEntrada";
            this.rbEntrada.Size = new System.Drawing.Size(62, 17);
            this.rbEntrada.TabIndex = 4;
            this.rbEntrada.TabStop = true;
            this.rbEntrada.Text = "Entrada";
            this.toolTip1.SetToolTip(this.rbEntrada, "Geralmente é usado quando se precisa colocar mais dinheiro para trocos no caixa");
            this.rbEntrada.UseVisualStyleBackColor = true;
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(9, 113);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(423, 20);
            this.txtDescricao.TabIndex = 6;
            this.toolTip1.SetToolTip(this.txtDescricao, "Preencha com a descrição do lançamento assim ficará fácil identifica-lo no caixa");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Descrição Lançamento";
            // 
            // txtDocumento
            // 
            this.txtDocumento.Location = new System.Drawing.Point(110, 30);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(100, 20);
            this.txtDocumento.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "N. Documento";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data Lançamento";
            // 
            // dtMovimento
            // 
            this.dtMovimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtMovimento.Location = new System.Drawing.Point(12, 30);
            this.dtMovimento.Name = "dtMovimento";
            this.dtMovimento.Size = new System.Drawing.Size(83, 20);
            this.dtMovimento.TabIndex = 0;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(110, 186);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(87, 23);
            this.btnSalvar.TabIndex = 9;
            this.btnSalvar.Text = "Salvar[F12]";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.Salvar);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(216, 186);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(92, 23);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Canc[ESC]";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // frmLancamentoCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 213);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmLancamentoCaixa";
            this.Text = "[XDelivery] Lançamento Caixa";
            this.Load += new System.EventHandler(this.frmLancamentoCaixa_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLancamentoCaixa_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSaida;
        private System.Windows.Forms.RadioButton rbEntrada;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtMovimento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxFormaPagamento;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox cbxCaixas;
        private System.Windows.Forms.ComboBox cbxTurno;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSolicitante;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}