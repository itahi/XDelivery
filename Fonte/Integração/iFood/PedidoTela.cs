using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Integração.iFood
{
   public class PedidoTela
    {
        public string reference { get; set; }
        public string name { get; set; }
        public DateTime DataPedido { get; set; }
        public string situacao { get; set; }
        public double total { get; set; }
    }
}
