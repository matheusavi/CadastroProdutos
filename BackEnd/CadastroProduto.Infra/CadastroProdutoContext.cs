using CadastroProduto.Domain;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<int> SaveAggregateEntitiesAsync(CancellationToken cancellationToken = default) 
            => PublishDomainEvents(base.SaveChangesAsync(cancellationToken));

        private async Task<int> PublishDomainEvents(Task<int> task)
        {
            List<object> entities = ChangeTracker
               .Entries<AggregateRoot>()
               .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
               .SelectMany(x => x.Entity.DomainEvents)
               .Select(x => (object)x)
               .ToList();

            var retorno = await task;

            foreach (var item in entities)
            {
                var type = item.GetType();
                await _publishEndpoint.Publish(item);
            }

            return retorno;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }



    }
}
