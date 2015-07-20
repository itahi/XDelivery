﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string GrupoProduto { get; set; }
        public string DiaSemana { get; set; }
        public decimal PrecoDesconto { get; set; }
        public bool AtivoSN { get; set; }
    }
}
