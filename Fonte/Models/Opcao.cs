using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class Opcao
    {
       public int Codigo { get; set; }
       public string Nome { get; set; }
       public string Tipo { get; set; }
       public DateTime DataAlteracao { get; set; }
       public DateTime DataSincronismo { get; set; }
       public Boolean OnlineSN { get; set; }

    }
}
