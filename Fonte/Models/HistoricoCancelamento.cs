using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class HistoricoCancelamento
    {
        public int Codigo { get; set; }
        public int CodPessoa { get; set; }
        public string Motivo { get; set; }
        public DateTime Data { get; set; }
        public int CodMotivo { get; set; }
    }
}
