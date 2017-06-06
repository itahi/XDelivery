using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class CepUtil
    {
        //public int ID { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public static implicit operator Task<object>(CepUtil v)
        {
            throw new NotImplementedException();
        }
    }
}
