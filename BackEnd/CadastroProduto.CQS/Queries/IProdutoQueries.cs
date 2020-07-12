using CadastroProduto.CQS;
using System.Threading.Tasks;

namespace CadastroProduto.CQS
{
    public interface IProdutoQueries
    {
        Task<ProdutoDto> FindAsync(long id);
    }
}
