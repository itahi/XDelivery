using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DexComanda.Models
{
    public class Grupo
    {
        public int Codigo { get; set; }
        public string NomeGrupo { get; set; }
        public Boolean ImprimeCozinhaSN { get; set; }
        public Boolean OnlineSN { get; set; }
        public Boolean AtivoSN { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string NomeImpressora { get; set; }
        public int CodFamilia { get; set; }
        

    }
}
