using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class AtualizaEstoque
    {
        public int CodProduto { get; set; }
        public decimal Quantidade { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
