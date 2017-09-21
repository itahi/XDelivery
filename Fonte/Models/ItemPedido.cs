using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DexComanda.Models
{
    public class ItemPedido
    {
        public int Codigo { get; set; }
        public int CodPedido { get; set; }
        public int CodProduto { get; set;}
        public string NomeProduto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; set; }
        public bool ImpressoSN { get; set; }
        public string Item { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public bool FidelidadeSN { get; set; }
        public decimal DescontoPorcetagem { get; set; }
    }
}
