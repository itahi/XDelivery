using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class HistoricoPessoa
    {
        public int Codigo { get; set; }
        public int CodPessoa { get; set; }
        public char Tipo { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public string Historico { get; set; }
        public int CodUsuario { get; set; }
    }
}
