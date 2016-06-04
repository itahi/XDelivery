using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class OpcaoPedido
    {
        public int CodPedido { get; set; }
        public int CodProduto { get; set; }
        public int CodOpcao { get; set; }
        public decimal Quantidade { get; set; }
        public string Observacao { get; set; }
    }
}
