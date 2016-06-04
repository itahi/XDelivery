using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class FinalizaPedido
    {
        public int CodPedido { get; set; }
        public int CodPagamento { get; set; }
        public decimal ValorPagamento { get; set; }
    }
}
