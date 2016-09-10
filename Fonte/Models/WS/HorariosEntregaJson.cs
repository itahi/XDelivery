using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models.WS
{
   public class HorariosEntregaJson
    {
        public string limite_horario_pedido { get; set; }
        public string horario_entrega { get; set; }
        public int ativo { get; set; }
        public int referencia_id { get; set; }

    }
}
