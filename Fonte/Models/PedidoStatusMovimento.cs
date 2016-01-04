using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XIntegrador.Classe.Local
{
   public class PedidoStatusMovimento
    {
        public int CodPedido { get; set; }
        public int CodStatus { get; set; }
        public int CodUsuario { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
