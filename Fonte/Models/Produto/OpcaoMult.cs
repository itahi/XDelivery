using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models.Produto
{
   public class OpcaoMult
    {
        public int Codigo { get; set; }
        public string DiasDisponivel { get; set; }
        public Boolean OnlineSN { get; set; }
        public Boolean AtivoSN { get; set; }
    }
}
