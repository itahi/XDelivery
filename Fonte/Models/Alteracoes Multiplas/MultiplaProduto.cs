using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models.Alteracoes_Multiplas
{
   public class MultiplaProduto
    {
        public int Codigo { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataInicioPromocao { get; set; }
        public DateTime DataFimPromocao { get; set; }
        public decimal PrecoDesconto { get; set; }
        public string DiasDesconto { get; set; }
        //  public int Codigo { get; set; }
    }
}
