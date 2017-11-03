using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Integração.iFood.Pedido
{
    public class root
    {
        public int change { get; set; }
        public DateTime createdAt { get; set; }
        public customer customer { get; set; }
        public deliveryAddress deliveryAddress { get; set; }
        public DateTime deliveryDateTime { get; set; }
        public int deliveryFee { get; set; }
        public string id { get; set; }
        public List<item> items { get; set; }
        public merchant merchant { get; set; }
        public string observations { get; set; }
        public List<payment> payments { get; set; }
        public string reference { get; set; }
        public int subTotal { get; set; }
        public int totalPrice { get; set; }
        public string type { get; set; }
    }
}
