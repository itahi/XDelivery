using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class PessoaFidelidade
    {
        public int CodPessoa { get; set; }
        public int CodPedido { get; set; }
        public int Ponto { get; set; }
        public char Tipo { get; set; }
    }
}
