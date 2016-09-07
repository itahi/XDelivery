using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class HorarioEntrega
    {
        public int Codigo { get; set; }
        public string Limite_horario_pedido { get; set; }
        public string Horario_entrega { get; set; }
        public Boolean OnlineSN { get; set; }

    }
}
