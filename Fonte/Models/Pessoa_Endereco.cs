using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class Pessoa_Endereco
    {
        public int Codigo { get; set; }
        public int CodPessoa { get; set; }
        public string NomeEndereco { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string PontoReferencia { get; set; }
        public string Complemento { get; set; }
        public int CodRegiao { get; set; }
    }
}
