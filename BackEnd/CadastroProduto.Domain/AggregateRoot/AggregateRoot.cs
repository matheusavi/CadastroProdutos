using CadastroProduto.CQS;
using System.Collections.Generic;

namespace CadastroProduto.Domain
{
    public class AggregateRoot : IAggregrateRoot
    {
        public List<IDomainEvent> _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (_domainEvents == null)
                _domainEvents = new List<IDomainEvent>();

            _domainEvents.Add(domainEvent);
        }

    }
}
