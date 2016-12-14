using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models.Operacoes
{
    public class NovoTotalPedido
    {
        public int Codigo { get; set; }
        public decimal TotalPedido { get; set; }
        public string Tipo { get; set; }
        public string NumeroMesa { get; set; }
        public int CodUsuario { get; set; }
        public string HorarioEntrega { get; set; }
        public decimal DescontoValor { get; set; }
        public string Observacao { get; set; }

    }
}
