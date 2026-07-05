using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Abstractions
{
    public abstract class Aggregate<T> : IAggregate<T>
    {
        private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => domainEvents;

        public T Id { get; set; }
        public string CreateidBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }

        public IDomainEvent[] ClearDomainEvents()
        {
            var result = domainEvents.ToArray();

            domainEvents.Clear();
            return result;
        }
    }
}
