using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models.Alteracoes_Multiplas
{
    public class AlteracaoMultiplaOpcao
    {
        public int CodOpcao { get; set; }
        public decimal Preco { get; set; }
       // public DateTime DataAlteracao { get; set; }
        public Boolean OnlineSN { get; set; }
    }
}
