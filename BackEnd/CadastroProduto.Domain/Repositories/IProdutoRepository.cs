using System.Threading.Tasks;

namespace CadastroProduto.Domain
{
    public interface IProdutoRepository
    {
        Produto Add(Produto produto);
        IUnitOfWork UnitOfWork { get; }
        Task<Produto> FindAsync(long id);
        Task<Produto> DeleteAsync(long id);
    }
}
