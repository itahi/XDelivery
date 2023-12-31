﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    
    public class RegioesEntrega
    {
        public int Codigo { get; set; }
        public string NomeRegiao { get; set; }
        public decimal TaxaServico { get; set; }
        public DateTime DataAlteracao { get; set; }
        public Boolean OnlineSN { get; set; }
        public Boolean AtivoSN { get; set; }
        public Double valorMinimoFreteGratis { get; set; }
        public string PrevisaoEntrega { get; set; }
        public decimal TaxaEntregador { get; set; }
    }
}
