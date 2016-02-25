using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string GrupoProduto { get; set; }
        public int CodGrupo { get; set; }
        public string DiaSemana { get; set; }
        public decimal PrecoDesconto { get; set; }
        public bool AtivoSN { get; set; }
        public bool OnlineSN { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int MaximoAdicionais { get; set; }
        public string UrlImagem { get; set; }
        public DateTime DataInicioPromocao { get; set; }
        public DateTime DataFimPromocao { get; set; }
    }
}
