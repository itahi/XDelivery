using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class RegiaoEntrega_Bairros
    {
        public int CodRegiao { get; set; }
        public string Nome { get; set; }
        public string CEP { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataSincronismo { get; set; }

    }
}
