using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CadastroProdutos.Api
{
    public class Produto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid Guid { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public long Estoque { get; set; }

        public void UpdateInfo(string nome, decimal preco, long estoque)
        {
            Nome = nome;
            Preco = preco;
            Estoque = estoque;
        }
    }
}
