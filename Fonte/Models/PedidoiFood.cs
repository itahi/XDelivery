using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class PedidoiFood
    {
        public string idPedido { get; set; }
        public string status { get; set; }
        public DateTime Data { get; set; }
        public string Cliente { get; set; }
        public double Total { get; set; }

    }

}
