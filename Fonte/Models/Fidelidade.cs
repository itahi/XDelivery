using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class Fidelidade
    {
        public Boolean AtivoSN { get; set; }
        public string Tipo { get; set; }
        public int Multiplicador { get; set; }
        public string Dias { get; set;}
    }
}
