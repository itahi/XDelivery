﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ItemPedido
    {
        public int Codigo { get; set; }
        public int CodPedido { get; set; }
        public int CodProduto { get; set;}
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; set; }
        public string Item { get; set; }
    }
}
