﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Relatorios.Clientes
{
    public partial class frmDebitos : Form
    {
        public frmDebitos()
        {
            InitializeComponent();
        }

        private void Filtrar(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception erro)
            {
                MessageBox.Show(Bibliotecas.cException + erro.Message);
            }
        }
    }
}
