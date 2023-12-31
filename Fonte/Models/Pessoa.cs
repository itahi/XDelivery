﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DexComanda.Models
{
    public class Pessoa
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string PontoReferencia { get; set; }
        public string Observacao { get; set; }
        public string Telefone { get; set; }
        public string Telefone2 { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public int TicketFidelidade { get; set; }
        public int CodRegiao { get; set; }
        public string user_id { get; set; }
        public string DDD { get; set; }
        public string Sexo { get; set; }
        public char PFPJ { get; set; }
        public int CodOrigemCadastro { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string email { get; set; }
    }
}
