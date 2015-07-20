using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class Pedido
    {
        public int Codigo { get; set; }
        public int CodPessoa { get; set; }
        public decimal TotalPedido { get; set; }
        public string TrocoPara { get; set; }
        public string FormaPagamento { get; set; }
        public DateTime RealizadoEm { get; set; }
        public string Tipo { get; set; }
        public int NumeroMesa { get; set; }
        public string Status { get; set; }
        public string PedidoOrigem { get; set; }
        public int CodigoMesa { get; set; }
        public decimal DescontoValor { get; set; }
    }
}
