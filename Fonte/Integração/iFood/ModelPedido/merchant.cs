using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Integração.iFood.Pedido
{
    public class merchant
    {
        public adress address { get; set; }
        public string currency { get; set; }
        public string externalCode { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public List<string> phones { get; set; }
    }
}
