using System.Threading;
using System.Threading.Tasks;

namespace CadastroProduto.Domain
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
