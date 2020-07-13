using CadastroProduto.CQS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroProduto.CQS
{
    public interface IProdutoQueries
    {
        Task<ProdutoDto> FindAsync(long id);
        Task<List<ProdutoDto>> GetAllAsync();
    }
}
