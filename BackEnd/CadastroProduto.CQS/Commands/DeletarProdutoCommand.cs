using MediatR;

namespace CadastroProduto.CQS
{
    public class DeletarProdutoCommand : IRequest<ProdutoDto>
    {
        public DeletarProdutoCommand(long id)
        {
            Id = id;
        }
        public long Id { get; }
    }
}
