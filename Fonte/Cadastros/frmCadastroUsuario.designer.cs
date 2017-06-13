namespace DexComanda
{
    partial class frmCadastroUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroUsuario));
            this.txtNomeUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.usuariosGridView = new System.Windows.Forms.DataGridView();
            this.chkCancelaPedidos = new System.Windows.Forms.CheckBox();
            this.chkAcessaRelat = new System.Windows.Forms.CheckBox();
            this.chkAlteraProdutos = new System.Windows.Forms.CheckBox();
            this.chkAdministrador = new System.Windows.Forms.CheckBox();
            this.chkFechaPedido = new System.Windows.Forms.CheckBox();
            this.chkDescSN = new System.Windows.Forms.CheckBox();
            this.txtDesconto = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkAbreCliente = new System.Windows.Forms.CheckBox();
            this.chkAbreCaixa = new System.Windows.Forms.CheckBox();
            this.chkEditaCliente = new System.Windows.Forms.CheckBox();
            this.chkAlteraPedido = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.usuariosGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNomeUsuario
            // 
            this.txtNomeUsuario.Location = new System.Drawing.Point(15, 26);
            this.txtNomeUsuario.Name = "txtNomeUsuario";
            this.txtNomeUsuario.Size = new System.Drawing.Size(168, 20);
            this.txtNomeUsuario.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome:";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(15, 105);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 2;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.AdicionarSalvarUsuario);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Senha:";
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(15, 79);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = 'X';
            this.txtSenha.Size = new System.Drawing.Size(168, 20);
            this.txtSenha.TabIndex = 4;
            this.txtSenha.UseSystemPasswordChar = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(108, 105);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Editar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.Editar);
            // 
            // usuariosGridView
            // 
            this.usuariosGridView.AllowUserToAddRows = false;
            this.usuariosGridView.AllowUserToDeleteRows = false;
            this.usuariosGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usuariosGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.usuariosGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.usuariosGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usuariosGridView.Location = new System.Drawing.Point(5, 163);
            this.usuariosGridView.MultiSelect = false;
            this.usuariosGridView.Name = "usuariosGridView";
            this.usuariosGridView.ReadOnly = true;
            this.usuariosGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.usuariosGridView.Size = new System.Drawing.Size(399, 133);
            this.usuariosGridView.TabIndex = 7;
            this.usuariosGridView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.usuariosGridView_MouseDoubleClick);
            // 
            // chkCancelaPedidos
            // 
            this.chkCancelaPedidos.AutoSize = true;
            this.chkCancelaPedidos.Location = new System.Drawing.Point(287, 3);
            this.chkCancelaPedidos.Name = "chkCancelaPedidos";
            this.chkCancelaPedidos.Size = new System.Drawing.Size(106, 17);
            this.chkCancelaPedidos.TabIndex = 8;
            this.chkCancelaPedidos.Text = "Cancela Pedidos";
            this.toolTip1.SetToolTip(this.chkCancelaPedidos, "Permite que o usuário \"CANCELE\" pedidos");
            this.chkCancelaPedidos.UseVisualStyleBackColor = true;
            // 
            // chkAcessaRelat
            // 
            this.chkAcessaRelat.AutoSize = true;
            this.chkAcessaRelat.Location = new System.Drawing.Point(189, 26);
            this.chkAcessaRelat.Name = "chkAcessaRelat";
            this.chkAcessaRelat.Size = new System.Drawing.Size(92, 17);
            this.chkAcessaRelat.TabIndex = 9;
            this.chkAcessaRelat.Text = "Ac. Relatórios";
            this.toolTip1.SetToolTip(this.chkAcessaRelat, "Permite visualização e impressão de relatórios ( caixa, estoque, vendas etc)");
            this.chkAcessaRelat.UseVisualStyleBackColor = true;
            // 
            // chkAlteraProdutos
            // 
            this.chkAlteraProdutos.AutoSize = true;
            this.chkAlteraProdutos.Location = new System.Drawing.Point(287, 28);
            this.chkAlteraProdutos.Name = "chkAlteraProdutos";
            this.chkAlteraProdutos.Size = new System.Drawing.Size(86, 17);
            this.chkAlteraProdutos.TabIndex = 10;
            this.chkAlteraProdutos.Text = "Alt. Produtos";
            this.chkAlteraProdutos.UseVisualStyleBackColor = true;
            // 
            // chkAdministrador
            // 
            this.chkAdministrador.AutoSize = true;
            this.chkAdministrador.Location = new System.Drawing.Point(187, 89);
            this.chkAdministrador.Name = "chkAdministrador";
            this.chkAdministrador.Size = new System.Drawing.Size(89, 17);
            this.chkAdministrador.TabIndex = 11;
            this.chkAdministrador.Text = "Administrador";
            this.chkAdministrador.UseVisualStyleBackColor = true;
            // 
            // chkFechaPedido
            // 
            this.chkFechaPedido.AutoSize = true;
            this.chkFechaPedido.Location = new System.Drawing.Point(189, 3);
            this.chkFechaPedido.Name = "chkFechaPedido";
            this.chkFechaPedido.Size = new System.Drawing.Size(92, 17);
            this.chkFechaPedido.TabIndex = 13;
            this.chkFechaPedido.Text = "Fecha Pedido";
            this.toolTip1.SetToolTip(this.chkFechaPedido, "Permite que o usuário finalize pedidos");
            this.chkFechaPedido.UseVisualStyleBackColor = true;
            // 
            // chkDescSN
            // 
            this.chkDescSN.AutoSize = true;
            this.chkDescSN.Location = new System.Drawing.Point(5, 9);
            this.chkDescSN.Name = "chkDescSN";
            this.chkDescSN.Size = new System.Drawing.Size(96, 17);
            this.chkDescSN.TabIndex = 16;
            this.chkDescSN.Text = "Desc. Pedido?";
            this.chkDescSN.UseVisualStyleBackColor = true;
            this.chkDescSN.CheckedChanged += new System.EventHandler(this.chkDescSN_CheckedChanged);
            // 
            // txtDesconto
            // 
            this.txtDesconto.Location = new System.Drawing.Point(151, 7);
            this.txtDesconto.Name = "txtDesconto";
            this.txtDesconto.Size = new System.Drawing.Size(56, 20);
            this.txtDesconto.TabIndex = 17;
            this.txtDesconto.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Max %";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtDesconto);
            this.panel1.Controls.Add(this.chkDescSN);
            this.panel1.Location = new System.Drawing.Point(185, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(218, 35);
            this.panel1.TabIndex = 16;
            this.toolTip1.SetToolTip(this.panel1, "Permite que o usuário insira descontos no pedido.\r\nLimitando-o a máximo de porcet" +
        "agem definidio\r\n");
            // 
            // chkAbreCliente
            // 
            this.chkAbreCliente.AutoSize = true;
            this.chkAbreCliente.Location = new System.Drawing.Point(187, 112);
            this.chkAbreCliente.Name = "chkAbreCliente";
            this.chkAbreCliente.Size = new System.Drawing.Size(83, 17);
            this.chkAbreCliente.TabIndex = 20;
            this.chkAbreCliente.Text = "Abre Cliente";
            this.toolTip1.SetToolTip(this.chkAbreCliente, "Permite visualizar o cadastro de clientes");
            this.chkAbreCliente.UseVisualStyleBackColor = true;
            // 
            // chkAbreCaixa
            // 
            this.chkAbreCaixa.AutoSize = true;
            this.chkAbreCaixa.Location = new System.Drawing.Point(287, 112);
            this.chkAbreCaixa.Name = "chkAbreCaixa";
            this.chkAbreCaixa.Size = new System.Drawing.Size(112, 17);
            this.chkAbreCaixa.TabIndex = 19;
            this.chkAbreCaixa.Text = "Abre/Fecha Caixa";
            this.toolTip1.SetToolTip(this.chkAbreCaixa, "Permite o fechamento e abertura de caixa");
            this.chkAbreCaixa.UseVisualStyleBackColor = true;
            // 
            // chkEditaCliente
            // 
            this.chkEditaCliente.AutoSize = true;
            this.chkEditaCliente.Location = new System.Drawing.Point(187, 135);
            this.chkEditaCliente.Name = "chkEditaCliente";
            this.chkEditaCliente.Size = new System.Drawing.Size(85, 17);
            this.chkEditaCliente.TabIndex = 18;
            this.chkEditaCliente.Text = "Edita Cliente";
            this.toolTip1.SetToolTip(this.chkEditaCliente, "Permite a edição de clientes cadastrados");
            this.chkEditaCliente.UseVisualStyleBackColor = true;
            // 
            // chkAlteraPedido
            // 
            this.chkAlteraPedido.AutoSize = true;
            this.chkAlteraPedido.Location = new System.Drawing.Point(287, 89);
            this.chkAlteraPedido.Name = "chkAlteraPedido";
            this.chkAlteraPedido.Size = new System.Drawing.Size(89, 17);
            this.chkAlteraPedido.TabIndex = 17;
            this.chkAlteraPedido.Text = "Altera Pedido";
            this.toolTip1.SetToolTip(this.chkAlteraPedido, "Permite que o usuário altere pedidos lançados");
            this.chkAlteraPedido.UseVisualStyleBackColor = true;
            // 
            // frmCadastroUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 303);
            this.Controls.Add(this.chkAbreCliente);
            this.Controls.Add(this.chkAbreCaixa);
            this.Controls.Add(this.chkEditaCliente);
            this.Controls.Add(this.chkAlteraPedido);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkFechaPedido);
            this.Controls.Add(this.chkAdministrador);
            this.Controls.Add(this.chkAlteraProdutos);
            this.Controls.Add(this.chkAcessaRelat);
            this.Controls.Add(this.chkCancelaPedidos);
            this.Controls.Add(this.usuariosGridView);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNomeUsuario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCadastroUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[XDelivery] Cadastro de Usuario";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCadastroUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.usuariosGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNomeUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridView usuariosGridView;
        private System.Windows.Forms.CheckBox chkCancelaPedidos;
        private System.Windows.Forms.CheckBox chkAcessaRelat;
        private System.Windows.Forms.CheckBox chkAlteraProdutos;
        private System.Windows.Forms.CheckBox chkAdministrador;
        private System.Windows.Forms.CheckBox chkFechaPedido;
        private System.Windows.Forms.CheckBox chkDescSN;
        private System.Windows.Forms.TextBox txtDesconto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAbreCliente;
        private System.Windows.Forms.CheckBox chkAbreCaixa;
        private System.Windows.Forms.CheckBox chkEditaCliente;
        private System.Windows.Forms.CheckBox chkAlteraPedido;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}