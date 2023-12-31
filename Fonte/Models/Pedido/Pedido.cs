﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public class Pedido
    {
        public int Codigo { get; set; }
        public int CodPessoa { get; set; }
        public decimal TotalPedido { get; set; }
        public decimal TrocoPara { get; set; }
        public string FormaPagamento { get; set; }
        public DateTime RealizadoEm { get; set; }
        public string Tipo { get; set; }
        public string NumeroMesa { get; set; }
        public string Status { get; set; }
        public string PedidoOrigem { get; set; }
        public int CodigoMesa { get; set; }
        public decimal DescontoValor { get; set; }
        public int CodigoPedidoWS { get; set; }
        public int CodUsuario { get; set; }
        public string HorarioEntrega { get; set; }
        public string Observacao { get; set; }
        public int CodEndereco { get; set; }
        public string Senha { get; set; }
        public Boolean PagoFidelidade { get; set; }
        public string Cupom { get; set; }

        public string idiFood { get; set; }

    }
}
