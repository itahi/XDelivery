using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models.Operacoes
{
    public class PedidoStatus
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public Boolean EnviarSN { get; set; }
        public Boolean AlertarSN { get; set; }
        public int Status { get; set; }
       // public DateTime DataAlteracao { get; set; }
    }
}
