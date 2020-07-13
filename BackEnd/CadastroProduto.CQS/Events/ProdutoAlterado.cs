using System;

namespace CadastroProduto.CQS
{
    public class ProdutoAlterado : IDomainEvent
    {
        public ProdutoAlterado(string nome, decimal preco, long estoque, Guid guid)
        {
            Nome = nome;
            Preco = preco;
            Estoque = estoque;
            Guid = guid;
        }

        public string Nome { get; }
        public decimal Preco { get; }
        public long Estoque { get; }
        public Guid Guid { get; }
    }
}
