using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class ProdutoOpcao_Tipo
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int Tipo { get; set; }
        public int MaximoOpcionais { get; set; }
        public int MinimoOpcionais { get; set; }
        public int OrdenExibicao { get; set; }
        public DateTime DataAlteracao { get; set; }
      //  public DateTime DataSincronismo { get; set; }
       // public int OrdenExibicao { get; set; }

    }
}
