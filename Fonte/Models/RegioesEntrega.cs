using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    
    public class RegioesEntrega
    {
        public int Codigo { get; set; }
        public string NomeRegiao { get; set; }
        public string Bairro { get; set; }
        public decimal TaxaServico { get; set; }
    }
}
