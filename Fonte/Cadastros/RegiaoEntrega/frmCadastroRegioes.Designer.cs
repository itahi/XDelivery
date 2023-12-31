﻿namespace DexComanda
{
    partial class frmCadastroRegioes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroRegioes));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrevisao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTaxaGratis = new System.Windows.Forms.TextBox();
            this.chkOnline = new System.Windows.Forms.CheckBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.btnEditar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEntrega = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRegiao = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RegioesGridView = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.txtTaxaEntregador = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RegioesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtTaxaEntregador);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPrevisao);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTaxaGratis);
            this.groupBox1.Controls.Add(this.chkOnline);
            this.groupBox1.Controls.Add(this.chkAtivo);
            this.groupBox1.Controls.Add(this.btnEditar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtEntrega);
            this.groupBox1.Controls.Add(this.btnSalvar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtRegiao);
            this.groupBox1.Location = new System.Drawing.Point(1, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 147);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cadastro ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(289, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Prev. Entrega";
            // 
            // txtPrevisao
            // 
            this.txtPrevisao.Location = new System.Drawing.Point(292, 71);
            this.txtPrevisao.MaxLength = 8;
            this.txtPrevisao.Name = "txtPrevisao";
            this.txtPrevisao.Size = new System.Drawing.Size(60, 20);
            this.txtPrevisao.TabIndex = 6;
            this.toolTip1.SetToolTip(this.txtPrevisao, "Previsão de entrega para essa determinada região em Minutos");
            this.txtPrevisao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrevisao_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Entrega Grátis?";
            // 
            // txtTaxaGratis
            // 
            this.txtTaxaGratis.Location = new System.Drawing.Point(206, 71);
            this.txtTaxaGratis.Name = "txtTaxaGratis";
            this.txtTaxaGratis.Size = new System.Drawing.Size(60, 20);
            this.txtTaxaGratis.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtTaxaGratis, "Informe aqui o valor minimo que dará a entrega grátis");
            // 
            // chkOnline
            // 
            this.chkOnline.AutoSize = true;
            this.chkOnline.Location = new System.Drawing.Point(281, 32);
            this.chkOnline.Name = "chkOnline";
            this.chkOnline.Size = new System.Drawing.Size(77, 17);
            this.chkOnline.TabIndex = 2;
            this.chkOnline.Text = "OnlineSN?";
            this.chkOnline.UseVisualStyleBackColor = true;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(204, 33);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(71, 17);
            this.chkAtivo.TabIndex = 1;
            this.chkAtivo.Text = "AtivoSN?";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(204, 104);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(154, 37);
            this.btnEditar.TabIndex = 8;
            this.btnEditar.Text = "Editar [F11]";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.Editar);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Taxa Entrega";
            // 
            // txtEntrega
            // 
            this.txtEntrega.Location = new System.Drawing.Point(11, 71);
            this.txtEntrega.Name = "txtEntrega";
            this.txtEntrega.Size = new System.Drawing.Size(93, 20);
            this.txtEntrega.TabIndex = 4;
            this.toolTip1.SetToolTip(this.txtEntrega, "Valor da entrega para região");
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(11, 104);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(161, 37);
            this.btnSalvar.TabIndex = 7;
            this.btnSalvar.Text = "Adicionar [F12]";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.AdicionarRegiao);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome Região";
            // 
            // txtRegiao
            // 
            this.txtRegiao.Location = new System.Drawing.Point(11, 33);
            this.txtRegiao.Name = "txtRegiao";
            this.txtRegiao.Size = new System.Drawing.Size(164, 20);
            this.txtRegiao.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.RegioesGridView);
            this.groupBox2.Location = new System.Drawing.Point(1, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 192);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista de Regiões";
            // 
            // RegioesGridView
            // 
            this.RegioesGridView.AllowUserToAddRows = false;
            this.RegioesGridView.AllowUserToDeleteRows = false;
            this.RegioesGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RegioesGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RegioesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RegioesGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RegioesGridView.Location = new System.Drawing.Point(3, 16);
            this.RegioesGridView.MultiSelect = false;
            this.RegioesGridView.Name = "RegioesGridView";
            this.RegioesGridView.ReadOnly = true;
            this.RegioesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RegioesGridView.Size = new System.Drawing.Size(372, 173);
            this.RegioesGridView.TabIndex = 3;
            this.RegioesGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RegioesGridView_CellClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(112, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Tx. Entregador";
            // 
            // txtTaxaEntregador
            // 
            this.txtTaxaEntregador.Location = new System.Drawing.Point(115, 71);
            this.txtTaxaEntregador.Name = "txtTaxaEntregador";
            this.txtTaxaEntregador.Size = new System.Drawing.Size(60, 20);
            this.txtTaxaEntregador.TabIndex = 42;
            this.toolTip1.SetToolTip(this.txtTaxaEntregador, "Caso pague o motoboy por entrega,\r\ndefina aqui quanto ganhará por entregas dessa " +
        "região.");
            // 
            // frmCadastroRegioes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 361);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmCadastroRegioes";
            this.Text = "[XDelivery] Cadastro de Regiões";
            this.Load += new System.EventHandler(this.frmCadastroRegioes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCadastroRegioes_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RegioesGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRegiao;
        private System.Windows.Forms.DataGridView RegioesGridView;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.CheckBox chkOnline;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTaxaGratis;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEntrega;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrevisao;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTaxaEntregador;
    }
}