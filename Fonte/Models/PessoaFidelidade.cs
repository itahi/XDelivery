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
        public int CodProduto { get; set; }
        public int CodUsuario { get; set; }
        public int Pontos { get; set; }
    }
}
