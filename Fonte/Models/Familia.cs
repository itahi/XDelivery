using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class Familia
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public Boolean OnlineSN { get; set; }
        public Boolean AtivoSN { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
