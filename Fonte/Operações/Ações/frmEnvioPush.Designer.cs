namespace DexComanda.Operações.Ações
{
    partial class frmEnvioPush
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
            this.grpTipoEnvio = new System.Windows.Forms.GroupBox();
            this.rbAgora = new System.Windows.Forms.RadioButton();
            this.rbControlado = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.grpPeriodo = new System.Windows.Forms.GroupBox();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxGrupo = new System.Windows.Forms.ComboBox();
            this.rbProduto = new System.Windows.Forms.RadioButton();
            this.cbxRegiao = new System.Windows.Forms.ComboBox();
            this.rbRegiao = new System.Windows.Forms.RadioButton();
            this.rbSumido = new System.Windows.Forms.RadioButton();
            this.rbAniversario = new System.Windows.Forms.RadioButton();
            this.grpGrid = new System.Windows.Forms.GroupBox();
            this.gridResultado = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTextoMsg = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNumero = new System.Windows.Forms.Label();
            this.cbxOrigemCadastro = new System.Windows.Forms.ComboBox();
            this.rbOrigemCadastro = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.grpTipoEnvio.SuspendLayout();
            this.grpPeriodo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridResultado)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grpTipoEnvio);
            this.groupBox1.Controls.Add(this.rbTodos);
            this.groupBox1.Controls.Add(this.btnEnviar);
            this.groupBox1.Controls.Add(this.grpPeriodo);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.rbSumido);
            this.groupBox1.Controls.Add(this.rbAniversario);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 172);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo Filtros";
            // 
            // grpTipoEnvio
            // 
            this.grpTipoEnvio.Controls.Add(this.rbAgora);
            this.grpTipoEnvio.Controls.Add(this.rbControlado);
            this.grpTipoEnvio.Location = new System.Drawing.Point(114, 7);
            this.grpTipoEnvio.Name = "grpTipoEnvio";
            this.grpTipoEnvio.Size = new System.Drawing.Size(221, 50);
            this.grpTipoEnvio.TabIndex = 13;
            this.grpTipoEnvio.TabStop = false;
            this.grpTipoEnvio.Text = "Tipo Envio";
            // 
            // rbAgora
            // 
            this.rbAgora.AutoSize = true;
            this.rbAgora.Location = new System.Drawing.Point(109, 19);
            this.rbAgora.Name = "rbAgora";
            this.rbAgora.Size = new System.Drawing.Size(81, 17);
            this.rbAgora.TabIndex = 14;
            this.rbAgora.Text = "Instantanêo";
            this.toolTip1.SetToolTip(this.rbAgora, "Envia exatamente nesse momento para todos,\r\nporém quem estiver sem internet não r" +
        "eceberá!");
            this.rbAgora.UseVisualStyleBackColor = true;
            // 
            // rbControlado
            // 
            this.rbControlado.AutoSize = true;
            this.rbControlado.Checked = true;
            this.rbControlado.Location = new System.Drawing.Point(11, 19);
            this.rbControlado.Name = "rbControlado";
            this.rbControlado.Size = new System.Drawing.Size(76, 17);
            this.rbControlado.TabIndex = 13;
            this.rbControlado.TabStop = true;
            this.rbControlado.Text = "Controlado";
            this.toolTip1.SetToolTip(this.rbControlado, "Baseia-se no horário que o cliente usa o App para enviar ");
            this.rbControlado.UseVisualStyleBackColor = true;
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Checked = true;
            this.rbTodos.Location = new System.Drawing.Point(11, 22);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(99, 17);
            this.rbTodos.TabIndex = 12;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos Usuários";
            this.toolTip1.SetToolTip(this.rbTodos, "Envia o push para todos usuarios que tem o app ou acessaram o site");
            this.rbTodos.UseVisualStyleBackColor = true;
            this.rbTodos.CheckedChanged += new System.EventHandler(this.rbTodos_CheckedChanged);
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(260, 123);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(75, 23);
            this.btnEnviar.TabIndex = 11;
            this.btnEnviar.Text = "Enviar ";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.EnviarPush);
            // 
            // grpPeriodo
            // 
            this.grpPeriodo.Controls.Add(this.dtFim);
            this.grpPeriodo.Controls.Add(this.dtInicio);
            this.grpPeriodo.Location = new System.Drawing.Point(114, 59);
            this.grpPeriodo.Name = "grpPeriodo";
            this.grpPeriodo.Size = new System.Drawing.Size(221, 60);
            this.grpPeriodo.TabIndex = 10;
            this.grpPeriodo.TabStop = false;
            this.grpPeriodo.Text = "Periodo Filtro";
            // 
            // dtFim
            // 
            this.dtFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFim.Location = new System.Drawing.Point(109, 26);
            this.dtFim.Name = "dtFim";
            this.dtFim.Size = new System.Drawing.Size(78, 20);
            this.dtFim.TabIndex = 1;
            // 
            // dtInicio
            // 
            this.dtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtInicio.Location = new System.Drawing.Point(11, 26);
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Size = new System.Drawing.Size(78, 20);
            this.dtInicio.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(11, 123);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 9;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.BuscarFiltro);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxOrigemCadastro);
            this.panel1.Controls.Add(this.rbOrigemCadastro);
            this.panel1.Controls.Add(this.cbxGrupo);
            this.panel1.Controls.Add(this.rbProduto);
            this.panel1.Controls.Add(this.cbxRegiao);
            this.panel1.Controls.Add(this.rbRegiao);
            this.panel1.Location = new System.Drawing.Point(363, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 159);
            this.panel1.TabIndex = 6;
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
            this.rbProduto.CheckedChanged += new System.EventHandler(this.rbProduto_CheckedChanged_1);
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
            // rbSumido
            // 
            this.rbSumido.AutoSize = true;
            this.rbSumido.Location = new System.Drawing.Point(11, 84);
            this.rbSumido.Name = "rbSumido";
            this.rbSumido.Size = new System.Drawing.Size(93, 17);
            this.rbSumido.TabIndex = 1;
            this.rbSumido.Text = "Cliente sumido";
            this.toolTip1.SetToolTip(this.rbSumido, "Filtra clientes que não compraram no periodo informado");
            this.rbSumido.UseVisualStyleBackColor = true;
            // 
            // rbAniversario
            // 
            this.rbAniversario.AutoSize = true;
            this.rbAniversario.Location = new System.Drawing.Point(11, 52);
            this.rbAniversario.Name = "rbAniversario";
            this.rbAniversario.Size = new System.Drawing.Size(97, 17);
            this.rbAniversario.TabIndex = 0;
            this.rbAniversario.Text = "Aniversáriantes";
            this.toolTip1.SetToolTip(this.rbAniversario, "Filtra aniversáriantes no periodo informado");
            this.rbAniversario.UseVisualStyleBackColor = true;
            // 
            // grpGrid
            // 
            this.grpGrid.Controls.Add(this.gridResultado);
            this.grpGrid.Location = new System.Drawing.Point(12, 183);
            this.grpGrid.Name = "grpGrid";
            this.grpGrid.Size = new System.Drawing.Size(257, 230);
            this.grpGrid.TabIndex = 3;
            this.grpGrid.TabStop = false;
            this.grpGrid.Text = "Resultado Filtro";
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
            this.gridResultado.Size = new System.Drawing.Size(251, 211);
            this.gridResultado.TabIndex = 0;
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(3, 37);
            this.txtTitulo.MaxLength = 30;
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(302, 20);
            this.txtTitulo.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtTitulo, "Preencha o titulo da mensagem, seja breve e chamativo");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtTitulo);
            this.groupBox2.Controls.Add(this.txtTextoMsg);
            this.groupBox2.Location = new System.Drawing.Point(275, 183);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 230);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mensagem a ser enviada";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Titulo";
            // 
            // txtTextoMsg
            // 
            this.txtTextoMsg.Location = new System.Drawing.Point(3, 62);
            this.txtTextoMsg.Name = "txtTextoMsg";
            this.txtTextoMsg.Size = new System.Drawing.Size(305, 182);
            this.txtTextoMsg.TabIndex = 2;
            this.txtTextoMsg.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 450);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(512, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Envio de mensagens \"Push\" para clientes que efetuaram Pedidos online \r\nem sua pla" +
    "taforma seja Site ou APP.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 418);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nr.:";
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(48, 418);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(0, 13);
            this.lblNumero.TabIndex = 7;
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
            this.rbOrigemCadastro.CheckedChanged += new System.EventHandler(this.rbOrigemCadastro_CheckedChanged);
            // 
            // frmEnvioPush
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 506);
            this.Controls.Add(this.lblNumero);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpGrid);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmEnvioPush";
            this.Text = "[xSistemas] Envio de Notificações Push";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpTipoEnvio.ResumeLayout(false);
            this.grpTipoEnvio.PerformLayout();
            this.grpPeriodo.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridResultado)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSumido;
        private System.Windows.Forms.RadioButton rbAniversario;
        private System.Windows.Forms.GroupBox grpGrid;
        private System.Windows.Forms.DataGridView gridResultado;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxRegiao;
        private System.Windows.Forms.RadioButton rbRegiao;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox grpPeriodo;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox txtTextoMsg;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.GroupBox grpTipoEnvio;
        private System.Windows.Forms.RadioButton rbAgora;
        private System.Windows.Forms.RadioButton rbControlado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.ComboBox cbxGrupo;
        private System.Windows.Forms.RadioButton rbProduto;
        private System.Windows.Forms.ComboBox cbxOrigemCadastro;
        private System.Windows.Forms.RadioButton rbOrigemCadastro;
    }
}