using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models.Configuracoes
{
   public class ImpressaoDelivery
    {
        public bool ImprimeSN { get; set; }
        public string TipoAgrupamento { get; set; }
        public string ViaDelivery { get; set; }
    }
}
