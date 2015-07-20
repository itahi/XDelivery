using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class DescontoDiaSemana
    {
        //public int Codigo { get; set; }
        public string DiaSemana { get; set; }
        public int CodProduto { get; set; }
        public decimal PrecoComDesconto { get; set; }
    }
}
