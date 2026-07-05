using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Abstractions
{
    public interface IAggregate<T>: IAggregate, IEntity<T>
    {

    }
    public interface IAggregate
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
    }
}
