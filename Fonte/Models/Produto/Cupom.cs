using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models.Produto
{
   public class Cupom
    {
        public int Codigo { get; set; }
        public string CodCupom  { get; set; }
        public decimal Desconto { get; set; }
        //public DateTime DataCadastro { get; set; }
        public DateTime DataValidade_Inicio { get; set; }
        public DateTime DataValidade_Fim { get; set; }
        public int Quantidade { get; set; }
        public Boolean AtivoSN { get; set; }
        public int QuantidadePessoa { get; set; }
    }
}
