using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class Empresa
    {
       // public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Telefone2 { get; set; }
        public string Contato { get; set; }
        public int Cep { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string UF { get; set; }
        public string PontoReferencia { get; set; }
        public string Servidor { get; set; }
        public string Banco { get; set; }
        public string VersaoBanco { get; set; }
        public DateTime DataInicio { get; set; }
        
        
    }
}
