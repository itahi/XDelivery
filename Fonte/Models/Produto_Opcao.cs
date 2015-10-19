using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class Produto_Opcao
    {
        public int CodProduto { get; set; }
        public int CodOpcao { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataAlteracao { get; set; }

    }
}
