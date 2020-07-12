using System;

namespace CadastroProduto.CQS
{
    public interface ProdutoCriado
    {
        string Nome { get; }
        decimal Preco { get; }
        long Estoque { get; }
        Guid Guid { get; }
    }
}
