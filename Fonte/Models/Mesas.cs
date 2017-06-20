using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class Mesas
    {
        public int Codigo { get; set; }
        public string NumeroMesa { get; set; }
        public decimal StatusMesa { get; set; }
        public bool AtivoSN { get; set; }
        public bool OnlineSN { get; set; }

    }
}
