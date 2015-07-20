using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class UsuarioPermissao
    {
        public int CodUsuario { get; set; }
        public bool CancelaPedidoSN { get; set; }
        public bool RelatorioSN { get; set; }
        public bool AlteraPedidoSN { get; set; }
        public bool  AlteraProdSN { get; set; }
        public bool AlteraClieSN { get; set; }
        public bool PermiteDescSN { get; set; }
        public bool AdministradorSN { get; set; }
        public DateTime DataAtualizacao { get; set; } 
    }
}
