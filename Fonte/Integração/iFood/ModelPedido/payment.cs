using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Integração.iFood.Pedido
{
    public class payment
    {
        public int changeFor { get; set; }
        public string code { get; set; }
        public string collector { get; set; }
        public string externalCode { get; set; }
        public string issuer { get; set; }
        public string name { get; set; }
        public bool prepaid { get; set; }
        public int value { get; set; }
    }
}
