using CadastroProduto.Domain;
using System.Threading.Tasks;

namespace CadastroProduto.Infra
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CadastroProdutoContext _dbContext;

        public ProdutoRepository(CadastroProdutoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public Produto Add(Produto produto)
            => _dbContext.Add(produto).Entity;

        public async Task<Produto> DeleteAsync(long id)
        {
            var entity = await FindAsync(id);
            if (entity == null)
                return null;
            return _dbContext.Set<Produto>().Remove(entity).Entity;
        }

        public Task<Produto> FindAsync(long id)
            => _dbContext.Set<Produto>().FindAsync(id).AsTask();
    }
}
