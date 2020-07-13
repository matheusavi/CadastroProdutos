using AutoMapper;
using CadastroProduto.CQS;
using CadastroProduto.Domain;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroProduto.Application
{
    public class CriarProdutoCommandHandler : IRequestHandler<CriarProdutoCommand, ProdutoDto>
    {
        private readonly IProdutoRepository _repository;
        private readonly ProdutoCommandValidator _validator;
        private readonly IMapper _mapper;

        public CriarProdutoCommandHandler(
            IProdutoRepository repository,
            ProdutoCommandValidator validator,
            IMapper mapper
            )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<ProdutoDto> Handle(CriarProdutoCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = _repository.Add(new Produto(request.Nome, request.Preco, request.Estoque));
            await _repository.UnitOfWork.SaveAggregateEntitiesAsync();
            return _mapper.Map<ProdutoDto>(entity);
        }
    }
}
