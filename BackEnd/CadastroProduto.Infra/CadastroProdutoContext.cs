using CadastroProduto.Domain;
using Microsoft.EntityFrameworkCore;

namespace CadastroProduto.Infra
{
    public class CadastroProdutoContext : DbContext, IUnitOfWork
    {
        public CadastroProdutoContext(DbContextOptions<CadastroProdutoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
