using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class Produto_Estoque
    {
        public int CodProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoCompra { get; set; }
        public DateTime DataAtualizacao { get; set; }

    }
}
