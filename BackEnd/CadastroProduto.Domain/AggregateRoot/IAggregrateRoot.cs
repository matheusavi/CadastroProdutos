using CadastroProduto.CQS;
using System.Collections.Generic;

namespace CadastroProduto.Domain
{
    public interface IAggregrateRoot
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void AddDomainEvent(IDomainEvent domainEvent);
    }
}
