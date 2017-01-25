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
            this.btnEnviar = new System.Windows.Forms.Button();
            this.grpPeriodo = new System.Windows.Forms.GroupBox();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbxGrupo = new System.Windows.Forms.ComboBox();
            this.rbProduto = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxRegiao = new System.Windows.Forms.ComboBox();
            this.rbRegiao = new System.Windows.Forms.RadioButton();
            this.rbSumido = new System.Windows.Forms.RadioButton();
            this.rbAniversario = new System.Windows.Forms.RadioButton();
            this.grpGrid = new System.Windows.Forms.GroupBox();
            this.gridResultado = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTextoMsg = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.grpPeriodo.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridResultado)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEnviar);
            this.groupBox1.Controls.Add(this.grpPeriodo);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.rbSumido);
            this.groupBox1.Controls.Add(this.rbAniversario);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo Filtros";
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(263, 116);
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
            this.grpPeriodo.Location = new System.Drawing.Point(110, 37);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.cbxGrupo);
            this.panel2.Controls.Add(this.rbProduto);
            this.panel2.Location = new System.Drawing.Point(363, 89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(144, 57);
            this.panel2.TabIndex = 7;
            // 
            // cbxGrupo
            // 
            this.cbxGrupo.Enabled = false;
            this.cbxGrupo.FormattingEnabled = true;
            this.cbxGrupo.Location = new System.Drawing.Point(11, 29);
            this.cbxGrupo.Name = "cbxGrupo";
            this.cbxGrupo.Size = new System.Drawing.Size(121, 21);
            this.cbxGrupo.TabIndex = 8;
            // 
            // rbProduto
            // 
            this.rbProduto.AutoSize = true;
            this.rbProduto.Enabled = false;
            this.rbProduto.Location = new System.Drawing.Point(13, 6);
            this.rbProduto.Name = "rbProduto";
            this.rbProduto.Size = new System.Drawing.Size(107, 17);
            this.rbProduto.TabIndex = 4;
            this.rbProduto.Text = "Comprou Produto";
            this.toolTip1.SetToolTip(this.rbProduto, "Filtra clientes que compra o produto selecionado no periodo informado");
            this.rbProduto.UseVisualStyleBackColor = true;
            this.rbProduto.CheckedChanged += new System.EventHandler(this.rbProduto_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxRegiao);
            this.panel1.Controls.Add(this.rbRegiao);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(363, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 128);
            this.panel1.TabIndex = 6;
            // 
            // cbxRegiao
            // 
            this.cbxRegiao.FormattingEnabled = true;
            this.cbxRegiao.Location = new System.Drawing.Point(11, 30);
            this.cbxRegiao.Name = "cbxRegiao";
            this.cbxRegiao.Size = new System.Drawing.Size(121, 21);
            this.cbxRegiao.TabIndex = 7;
            // 
            // rbRegiao
            // 
            this.rbRegiao.AutoSize = true;
            this.rbRegiao.Location = new System.Drawing.Point(13, 5);
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
            this.rbAniversario.Location = new System.Drawing.Point(11, 37);
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
            this.grpGrid.Location = new System.Drawing.Point(12, 163);
            this.grpGrid.Name = "grpGrid";
            this.grpGrid.Size = new System.Drawing.Size(257, 250);
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
            this.gridResultado.Size = new System.Drawing.Size(251, 231);
            this.gridResultado.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTextoMsg);
            this.groupBox2.Location = new System.Drawing.Point(275, 166);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 247);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Texto Mensagem";
            // 
            // txtTextoMsg
            // 
            this.txtTextoMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTextoMsg.Location = new System.Drawing.Point(3, 16);
            this.txtTextoMsg.Name = "txtTextoMsg";
            this.txtTextoMsg.Size = new System.Drawing.Size(305, 228);
            this.txtTextoMsg.TabIndex = 0;
            this.txtTextoMsg.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 430);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(512, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Envio de mensagens \"Push\" para clientes que efetuaram Pedidos online \r\nem sua pla" +
    "taforma seja Site ou APP.";
            // 
            // frmEnvioPush
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 506);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpGrid);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmEnvioPush";
            this.Text = "[xSistemas] Envio de Notificações Push";
            this.Load += new System.EventHandler(this.frmEnvioPush_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpPeriodo.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridResultado)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbProduto;
        private System.Windows.Forms.RadioButton rbSumido;
        private System.Windows.Forms.RadioButton rbAniversario;
        private System.Windows.Forms.GroupBox grpGrid;
        private System.Windows.Forms.DataGridView gridResultado;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxRegiao;
        private System.Windows.Forms.RadioButton rbRegiao;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbxGrupo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox grpPeriodo;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox txtTextoMsg;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Label label1;
    }
}