using CadastroProduto.CQS;
using System;

namespace CadastroProduto.Domain
{
    public class Produto : AggregateRoot
    {
        private Produto() { }
        public Produto(string nome, decimal preco, long estoque)
        {
            Nome = nome;
            Preco = preco;
            Estoque = estoque;
            Guid = Guid.NewGuid();
            AddDomainEvent(new ProdutoCriado(nome, preco, estoque, Guid));
        }

        public long Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public long Estoque { get; private set; }
        public Guid Guid { get; private set; }

        public void UpdateInfo(string nome, decimal preco, long estoque)
        {
            Nome = nome;
            Preco = preco;
            Estoque = estoque;
            AddDomainEvent(new ProdutoAlterado(nome, preco, estoque, Guid));
        }

        public void Excluir()
        {
            AddDomainEvent(new ProdutoExcluido(Guid));
        }
    }
}
