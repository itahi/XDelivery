using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class Caixa
    {
        public int Codigo { get; set; }
        public DateTime Data { get; set; }
        public int CodUsuario { get; set; }
        public string Historico { get; set; }
        public string Numero { get; set; }
        public decimal ValorAbertura { get; set; }
        public decimal ValorFechamento { get; set; }
        public bool Estado { get; set; }
        public string Turno { get; set; }
        public DateTime HorarioFechamento { get; set;}
    }
}
