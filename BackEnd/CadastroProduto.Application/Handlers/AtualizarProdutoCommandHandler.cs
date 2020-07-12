using CadastroProduto.CQS;
using CadastroProduto.Domain;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroProduto.Application
{
    public class AtualizarProdutoCommandHandler : IRequestHandler<AtualizarProdutoCommand, bool>
    {
        private readonly IProdutoRepository _repository;
        private readonly ProdutoCommandValidator _validator;

        public AtualizarProdutoCommandHandler(IProdutoRepository repository, ProdutoCommandValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<bool> Handle(AtualizarProdutoCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = await _repository.FindAsync(request.Id);
            if (entity == null)
                return false;
            entity.UpdateInfo(request.Nome, request.Preco, request.Estoque);
            await _repository.UnitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
