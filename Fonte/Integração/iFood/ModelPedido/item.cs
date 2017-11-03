using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Integração.iFood.Pedido
{
    public class item
    {
        public string category { get; set; }
        public int discount { get; set; }
        public string externalCode { get; set; }
        public string name { get; set; }
        public string observations { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public List<object> subItems { get; set; }
        public int subItemsPrice { get; set; }
        public int totalPrice { get; set; }
    }
}
