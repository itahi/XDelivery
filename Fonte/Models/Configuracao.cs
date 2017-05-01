using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class Configuracao
    {

        public int cod { get; set; }
        public string ImpViaCozinha { get; set; }
        public bool UsaDataNascimento { get; set; }
        public bool UsaLoginSenha { get; set; }
        public int QtdCaracteresImp { get; set; }
        public bool ControlaEntregador { get; set; }
        public string ProdutoPorCodigo { get; set; }
        public bool Usa2Telefones { get; set; }
        public bool UsaControleMesa { get; set; }
        public string ImprimeViaEntrega { get; set; }
        public string ImprimeViaBalcao { get; set; }
        public string ControlaFidelidade { get; set; }
        public bool DescontoDiaSemana { get; set; }
        public bool CobraTaxaGarcon { get; set; }
        public bool EnviaSMS { get; set; }
        public bool RepeteUltimoPedido { get; set; }
        public bool RegistraCancelamentos { get; set; }
        public string DadosApp { get; set; }
        public string CidadesAtendidas { get; set; }
        public bool ExigeVendedorSN { get; set; }
        public bool CobrancaProporcionalSN { get; set; }
        public string DadosPush { get; set; }
        public string Impressoras { get; set; }
    }
}
