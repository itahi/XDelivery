using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models.Zenvia
{
  public  class sendSmsMultiRequest
    {
        public string from { get; set; }
        public string to { get; set; }
        public string msg { get; set; }
        public string callbackOption { get; set; }
        public string id { get; set; }
    }
}
