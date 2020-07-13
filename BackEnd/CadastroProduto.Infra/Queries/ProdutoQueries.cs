using AutoMapper;
using AutoMapper.QueryableExtensions;
using CadastroProduto.CQS;
using CadastroProduto.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroProduto.Infra
{
    public class ProdutoQueries : IProdutoQueries
    {
        private readonly CadastroProdutoContext _dbContext;
        private readonly IMapper _mapper;

        public ProdutoQueries(CadastroProdutoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ProdutoDto> FindAsync(long id)
        {
            var entity = await _dbContext.Set<Produto>().FindAsync(id);
            return _mapper.Map<ProdutoDto>(entity);
        }

        public Task<List<ProdutoDto>> GetAllAsync()
            => _dbContext
                .Set<Produto>()
                .ProjectTo<ProdutoDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
