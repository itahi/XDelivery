using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models.Alteracoes_Multiplas
{
    public class AlteracaoCliente
    {
        public int Codigo { get; set; }
        public int CodRegiao { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Telefone { get; set; }

    }
}
