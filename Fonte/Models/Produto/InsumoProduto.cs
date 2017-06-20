using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models.Produto
{
   public class InsumoProduto
    {
        public int Codigo { get; set; }
        public int CodProduto { get; set; }
        public int CodInsumo { get; set; }
        public decimal Quantidade { get; set; }
    }
}
