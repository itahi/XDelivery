using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class Usuario
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public bool CancelaPedidosSN { get; set; }
        public bool AlteraProdutosSN { get; set; }
        public bool AdministradorSN { get; set; }
        public bool AcessaRelatoriosSN { get; set; }
        public bool DescontoPedidoSN { get; set; }
        public bool FinalizaPedidoSN { get; set; }
        public double DescontoMax { get; set; }
        public int CaixaLogado { get; set; }
        public bool EditaPedidoSN { get; set; }
        public bool VisualizaDadosClienteSN { get; set; }
        public bool AbreFechaCaixaSN { get; set; }

    }
}
