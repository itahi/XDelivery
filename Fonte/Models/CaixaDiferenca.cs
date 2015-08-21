using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class CaixaDiferenca
    {
       //public int Codigo { get; set; }
       public string NumeroCaixa { get; set; }
       public DateTime Data { get; set; }
       public decimal ValorSomado { get; set; }
       public decimal ValorInformado { get; set; }
       public decimal ValorDiferenca { get; set; }
       public int CodUsuario { get; set; }
       public char Tipo { get; set; }
    }
}
