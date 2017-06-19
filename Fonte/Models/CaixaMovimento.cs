using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class CaixaMovimento
    {
       public int Codigo { get; set; }
     //  public int CodCaixa { get; set; }
       public int CodUser { get; set; }
       public DateTime Data { get; set; }
       public string Historico { get; set; }
       public int CodFormaPagamento { get; set; }
       public decimal Valor { get; set; }
       public char Tipo { get; set; }
       public string NumeroDocumento { get; set; }
       public string Turno { get; set; }
    }
}
