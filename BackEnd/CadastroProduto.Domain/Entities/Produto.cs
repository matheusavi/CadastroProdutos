using System;

namespace CadastroProduto.Domain
{
    public class Produto
    {
        public Produto()
        {

        }
        public Produto(string nome, decimal preco, long estoque)
        {
            Nome = nome;
            Preco = preco;
            Estoque = estoque;
        }

        public long Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public long Estoque { get; private set; }

        public void UpdateInfo(string nome, decimal preco, long estoque)
        {
            Nome = nome;
            Preco = preco;
            Estoque = estoque;
        }
    }
}
