using AutoMapper;
using CadastroProduto.CQS;
using CadastroProduto.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroProduto.Application
{
    public class DeletarProdutoCommandHandler : IRequestHandler<DeletarProdutoCommand, ProdutoDto>
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;

        public DeletarProdutoCommandHandler(IProdutoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ProdutoDto> Handle(DeletarProdutoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.DeleteAsync(request.Id);
            entity.Excluir();
            await _repository.UnitOfWork.SaveAggregateEntitiesAsync();
            return _mapper.Map<ProdutoDto>(entity);
        }
    }
}
