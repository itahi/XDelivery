﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class CancelarPedido
    {
        public int Codigo { get; set; }
        public string status { get; set; }
        public DateTime RealizadoEm { get; set; }
        //public int CodUsuarioIncluiCancelou { get; set; }
        //public DateTime DataCancelado { get; set; }
    }
}
