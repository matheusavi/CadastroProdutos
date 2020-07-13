using System;

namespace CadastroProduto.CQS
{
    public class ProdutoExcluido : IDomainEvent
    {
        public ProdutoExcluido(Guid guid)
        {
            Guid = guid;
        }

        public Guid Guid { get; }

    }
}
