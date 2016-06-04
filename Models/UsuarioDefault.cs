using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class UsuarioDefault
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string senha { get; set; }
        public bool AdministradorSN { get; set; }
    }
}
