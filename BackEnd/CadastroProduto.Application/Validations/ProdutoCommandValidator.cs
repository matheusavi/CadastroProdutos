using CadastroProduto.CQS;
using FluentValidation;

namespace CadastroProduto.Application
{
    public class ProdutoCommandValidator : AbstractValidator<IProdutoCommand>
    {
        public ProdutoCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome do produto é obrigatório");

            RuleFor(x => x.Preco)
                .GreaterThan(0)
                .WithMessage("Preço deve ser maior que zero");
        }
    }
}
