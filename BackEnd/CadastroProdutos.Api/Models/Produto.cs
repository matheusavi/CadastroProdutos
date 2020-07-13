using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CadastroProdutos.Api
{
    public class Produto
    {
        public Guid Guid { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public long Estoque { get; set; }
    }
}
