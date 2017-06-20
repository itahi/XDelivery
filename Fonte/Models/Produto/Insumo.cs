using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models.Produto
{
    public class Insumo
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string UnidadeMedida { get; set; }
        public bool AtivoSN { get; set; }
    }
}
