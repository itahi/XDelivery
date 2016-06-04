using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class FormasPagamento
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public Boolean DescontoSN { get; set; }
        public Boolean GeraFinanceiro { get; set; }
        public Boolean OnlineSN { get; set; }
        public Boolean AtivoSN { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string CaminhoImagem { get; set; }
    }
}
