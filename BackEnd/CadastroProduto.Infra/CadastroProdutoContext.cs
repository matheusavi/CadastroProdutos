using CadastroProduto.CQS;
using CadastroProduto.Domain;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroProduto.Infra
{
    public class CadastroProdutoContext : DbContext, IUnitOfWork
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public CadastroProdutoContext(DbContextOptions<CadastroProdutoContext> options, IPublishEndpoint publishEndpoint)
            : base(options)
        {
            _publishEndpoint = publishEndpoint;
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            try
            {
                //await _publishEndpoint.Publish<ProdutoCriado>(new ProdutoCriado("nome", 1, 1, Guid.NewGuid()));
            }
            catch (Exception ex)
            {

            }
            return await base.SaveChangesAsync(cancellationToken);

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }



    }
}
