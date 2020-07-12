using CadastroProduto.Api.Principal;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace FluentValidation
{
    internal static class ValidationFailureExtensions
    {
        public static IEnumerable<ErrorModel> Parse(this IEnumerable<ValidationFailure> validationFailures)
            => validationFailures.Select(x => new ErrorModel(x.PropertyName, x.ErrorMessage));

    }
}
