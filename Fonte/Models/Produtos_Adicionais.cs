using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class Produtos_Adicionais
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int CodProduto { get; set; }
        public decimal Valor { get; set; }
    }
}
