namespace DexComanda
{
    partial class frmEnvioSms
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
            this.tbSelecao = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNumero = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.grpGrid = new System.Windows.Forms.GroupBox();
            this.gridResultado = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMensagem = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxOrigemCadastro = new System.Windows.Forms.ComboBox();
            this.rbOrigemCadastro = new System.Windows.Forms.RadioButton();
            this.cbxGrupo = new System.Windows.Forms.ComboBox();
            this.rbProduto = new System.Windows.Forms.RadioButton();
            this.cbxRegiao = new System.Windows.Forms.ComboBox();
            this.rbRegiao = new System.Windows.Forms.RadioButton();
            this.grpPeriodo = new System.Windows.Forms.GroupBox();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.btnEnviarSms = new System.Windows.Forms.Button();
            this.rbSemPedidos = new System.Windows.Forms.RadioButton();
            this.rbAniversariantes = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRestante = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtDDD = new System.Windows.Forms.TextBox();
            this.tbSelecao.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridResultado)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpPeriodo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSelecao
            // 
            this.tbSelecao.Controls.Add(this.tabPage1);
            this.tbSelecao.Location = new System.Drawing.Point(0, 1);
            this.tbSelecao.Name = "tbSelecao";
            this.tbSelecao.SelectedIndex = 0;
            this.tbSelecao.Size = new System.Drawing.Size(578, 428);
            this.tbSelecao.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtDDD);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lblNumero);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.btnEnviar);
            this.tabPage1.Controls.Add(this.grpGrid);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtMensagem);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(570, 402);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Seleção";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(457, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 39);
            this.label2.TabIndex = 23;
            this.label2.Text = "Insira <cliente> \r\nno texto caso queira\r\npersonalizar a msg";
            this.toolTip1.SetToolTip(this.label2, "Personalize sua mensagem com o nome do cliente \r\ninclua a tag <nome> na parte do " +
        "texto onde deseja inserir\r\nque o sistema ira personalizar cada msg.");
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(44, 383);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(0, 13);
            this.lblNumero.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 383);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Nr,:";
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(458, 347);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(96, 28);
            this.btnEnviar.TabIndex = 19;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.DisparaSMS);
            // 
            // grpGrid
            // 
            this.grpGrid.Controls.Add(this.gridResultado);
            this.grpGrid.Location = new System.Drawing.Point(8, 199);
            this.grpGrid.Name = "grpGrid";
            this.grpGrid.Size = new System.Drawing.Size(220, 180);
            this.grpGrid.TabIndex = 14;
            this.grpGrid.TabStop = false;
            this.grpGrid.Text = "Resultado Filtro";
            this.toolTip1.SetToolTip(this.grpGrid, "Lista dos clientes que estão dentro do filtro selecionado");
            // 
            // gridResultado
            // 
            this.gridResultado.AllowUserToAddRows = false;
            this.gridResultado.AllowUserToDeleteRows = false;
            this.gridResultado.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gridResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridResultado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridResultado.Location = new System.Drawing.Point(3, 16);
            this.gridResultado.Name = "gridResultado";
            this.gridResultado.ReadOnly = true;
            this.gridResultado.Size = new System.Drawing.Size(214, 161);
            this.gridResultado.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(300, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(245, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mensagem ao Cliente";
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(238, 212);
            this.txtMensagem.MaxLength = 145;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(214, 163);
            this.txtMensagem.TabIndex = 1;
            this.txtMensagem.Text = "";
            this.toolTip1.SetToolTip(this.txtMensagem, "Escreva aqui o texto que será enviado ao cliente");
            this.txtMensagem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMensagem_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.grpPeriodo);
            this.groupBox1.Controls.Add(this.btnEnviarSms);
            this.groupBox1.Controls.Add(this.rbSemPedidos);
            this.groupBox1.Controls.Add(this.rbAniversariantes);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 187);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxOrigemCadastro);
            this.panel1.Controls.Add(this.rbOrigemCadastro);
            this.panel1.Controls.Add(this.cbxGrupo);
            this.panel1.Controls.Add(this.rbProduto);
            this.panel1.Controls.Add(this.cbxRegiao);
            this.panel1.Controls.Add(this.rbRegiao);
            this.panel1.Location = new System.Drawing.Point(356, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 159);
            this.panel1.TabIndex = 17;
            // 
            // cbxOrigemCadastro
            // 
            this.cbxOrigemCadastro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOrigemCadastro.FormattingEnabled = true;
            this.cbxOrigemCadastro.Location = new System.Drawing.Point(11, 132);
            this.cbxOrigemCadastro.Name = "cbxOrigemCadastro";
            this.cbxOrigemCadastro.Size = new System.Drawing.Size(169, 21);
            this.cbxOrigemCadastro.TabIndex = 12;
            // 
            // rbOrigemCadastro
            // 
            this.rbOrigemCadastro.AutoSize = true;
            this.rbOrigemCadastro.Location = new System.Drawing.Point(11, 109);
            this.rbOrigemCadastro.Name = "rbOrigemCadastro";
            this.rbOrigemCadastro.Size = new System.Drawing.Size(103, 17);
            this.rbOrigemCadastro.TabIndex = 11;
            this.rbOrigemCadastro.Text = "Origem Cadastro";
            this.toolTip1.SetToolTip(this.rbOrigemCadastro, "Filtra clientes de acordo com a origem do cadastro");
            this.rbOrigemCadastro.UseVisualStyleBackColor = true;
            // 
            // cbxGrupo
            // 
            this.cbxGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGrupo.FormattingEnabled = true;
            this.cbxGrupo.Location = new System.Drawing.Point(11, 80);
            this.cbxGrupo.Name = "cbxGrupo";
            this.cbxGrupo.Size = new System.Drawing.Size(169, 21);
            this.cbxGrupo.TabIndex = 10;
            // 
            // rbProduto
            // 
            this.rbProduto.AutoSize = true;
            this.rbProduto.Location = new System.Drawing.Point(11, 57);
            this.rbProduto.Name = "rbProduto";
            this.rbProduto.Size = new System.Drawing.Size(107, 17);
            this.rbProduto.TabIndex = 9;
            this.rbProduto.Text = "Comprou Produto";
            this.toolTip1.SetToolTip(this.rbProduto, "Filtra clientes que compraram determinado grupo de produto");
            this.rbProduto.UseVisualStyleBackColor = true;
            // 
            // cbxRegiao
            // 
            this.cbxRegiao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRegiao.FormattingEnabled = true;
            this.cbxRegiao.Location = new System.Drawing.Point(11, 30);
            this.cbxRegiao.Name = "cbxRegiao";
            this.cbxRegiao.Size = new System.Drawing.Size(169, 21);
            this.cbxRegiao.TabIndex = 7;
            // 
            // rbRegiao
            // 
            this.rbRegiao.AutoSize = true;
            this.rbRegiao.Location = new System.Drawing.Point(13, 7);
            this.rbRegiao.Name = "rbRegiao";
            this.rbRegiao.Size = new System.Drawing.Size(119, 17);
            this.rbRegiao.TabIndex = 6;
            this.rbRegiao.Text = "Região selecionada";
            this.toolTip1.SetToolTip(this.rbRegiao, "Clientes da região selecionada");
            this.rbRegiao.UseVisualStyleBackColor = true;
            this.rbRegiao.CheckedChanged += new System.EventHandler(this.rbRegiao_CheckedChanged);
            // 
            // grpPeriodo
            // 
            this.grpPeriodo.Controls.Add(this.dtFim);
            this.grpPeriodo.Controls.Add(this.dtInicio);
            this.grpPeriodo.Location = new System.Drawing.Point(80, 45);
            this.grpPeriodo.Name = "grpPeriodo";
            this.grpPeriodo.Size = new System.Drawing.Size(184, 55);
            this.grpPeriodo.TabIndex = 16;
            this.grpPeriodo.TabStop = false;
            this.grpPeriodo.Text = "Periodo";
            // 
            // dtFim
            // 
            this.dtFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFim.Location = new System.Drawing.Point(95, 19);
            this.dtFim.Name = "dtFim";
            this.dtFim.Size = new System.Drawing.Size(79, 20);
            this.dtFim.TabIndex = 17;
            // 
            // dtInicio
            // 
            this.dtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtInicio.Location = new System.Drawing.Point(6, 19);
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Size = new System.Drawing.Size(79, 20);
            this.dtInicio.TabIndex = 16;
            // 
            // btnEnviarSms
            // 
            this.btnEnviarSms.Location = new System.Drawing.Point(123, 124);
            this.btnEnviarSms.Name = "btnEnviarSms";
            this.btnEnviarSms.Size = new System.Drawing.Size(96, 28);
            this.btnEnviarSms.TabIndex = 2;
            this.btnEnviarSms.Text = "Selecionar";
            this.btnEnviarSms.UseVisualStyleBackColor = true;
            this.btnEnviarSms.Click += new System.EventHandler(this.EnviarSMS);
            // 
            // rbSemPedidos
            // 
            this.rbSemPedidos.AutoSize = true;
            this.rbSemPedidos.Location = new System.Drawing.Point(199, 19);
            this.rbSemPedidos.Name = "rbSemPedidos";
            this.rbSemPedidos.Size = new System.Drawing.Size(139, 17);
            this.rbSemPedidos.TabIndex = 3;
            this.rbSemPedidos.TabStop = true;
            this.rbSemPedidos.Text = "Sem pedidos no periodo";
            this.toolTip1.SetToolTip(this.rbSemPedidos, "Clientes que não fizeram pedido no periodo");
            this.rbSemPedidos.UseVisualStyleBackColor = true;
            // 
            // rbAniversariantes
            // 
            this.rbAniversariantes.AutoSize = true;
            this.rbAniversariantes.Location = new System.Drawing.Point(7, 16);
            this.rbAniversariantes.Name = "rbAniversariantes";
            this.rbAniversariantes.Size = new System.Drawing.Size(150, 17);
            this.rbAniversariantes.TabIndex = 0;
            this.rbAniversariantes.TabStop = true;
            this.rbAniversariantes.Text = "Aniversáriantes no periodo";
            this.toolTip1.SetToolTip(this.rbAniversariantes, "Busca clientes que fazem ainversário no periodo, considera apenas dia e mês");
            this.rbAniversariantes.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(376, 432);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "caracteres";
            // 
            // lblRestante
            // 
            this.lblRestante.AutoSize = true;
            this.lblRestante.Location = new System.Drawing.Point(345, 432);
            this.lblRestante.Name = "lblRestante";
            this.lblRestante.Size = new System.Drawing.Size(25, 13);
            this.lblRestante.TabIndex = 11;
            this.lblRestante.Text = "155";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 432);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Ainda restam:";
            // 
            // txtDDD
            // 
            this.txtDDD.Location = new System.Drawing.Point(458, 321);
            this.txtDDD.MaxLength = 2;
            this.txtDDD.Name = "txtDDD";
            this.txtDDD.Size = new System.Drawing.Size(53, 20);
            this.txtDDD.TabIndex = 24;
            this.toolTip1.SetToolTip(this.txtDDD, "Informe o DDD padrão da sua area");
            this.txtDDD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDDD_KeyPress);
            // 
            // frmEnvioSms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 454);
            this.Controls.Add(this.tbSelecao);
            this.Controls.Add(this.lblRestante);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmEnvioSms";
            this.Text = "[xSistemas] Envio de SMS ao Cliente";
            this.Load += new System.EventHandler(this.frmEnvioSms_Load);
            this.tbSelecao.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.grpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridResultado)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpPeriodo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbSelecao;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSemPedidos;
        private System.Windows.Forms.RadioButton rbAniversariantes;
        private System.Windows.Forms.Button btnEnviarSms;
        private System.Windows.Forms.RichTextBox txtMensagem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRestante;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpPeriodo;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox grpGrid;
        private System.Windows.Forms.DataGridView gridResultado;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxOrigemCadastro;
        private System.Windows.Forms.RadioButton rbOrigemCadastro;
        private System.Windows.Forms.ComboBox cbxGrupo;
        private System.Windows.Forms.RadioButton rbProduto;
        private System.Windows.Forms.ComboBox cbxRegiao;
        private System.Windows.Forms.RadioButton rbRegiao;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDDD;
    }
}