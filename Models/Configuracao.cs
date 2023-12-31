﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class Configuracao
    {

        public int cod { get; set; }
        public bool ImpViaCozinha { get; set; }
        public bool UsaDataNascimento { get; set; }
        public bool UsaLoginSenha { get; set; }
        public int QtdCaracteresImp { get; set; }
        public bool ControlaEntregador { get; set; }
        public bool ProdutoPorCodigo { get; set; }
        public bool Usa2Telefones { get; set; }
        public bool UsaControleMesa { get; set; }
        public bool ImprimeViaEntrega { get; set; }
        public bool ControlaFidelidade { get; set; }
        public int  PedidosParaFidelidade { get; set; }
        public bool DescontoDiaSemana { get; set; }
        public string PrevisaoEntrega { get; set; }
        public bool PrevisaoEntregaSN { get; set; }
        public bool CobraTaxaGarcon { get; set; }
        public string TamanhoFont { get; set; }
        public string PortaLPT { get; set; }
        public bool ImpLPT { get; set; }
        public bool EnviaSMS { get; set; }
        public string ViasEntrega { get; set; }
        public string ViasCozinha    { get; set; }
        public string ViasBalcao { get; set; }
        public bool RepeteUltimoPedido { get; set; }
        public bool RegistraCancelamentos { get; set; }
        //public string ImpressoraEntrega { get; set; }
        //public string ImpressoraCopaBalcao { get; set; }
    }
}
