using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Abstractions
{
    public interface IDomainEvent: INotification
    {
        Guid EventId => Guid.NewGuid();
        public DateTime OccouredOn => DateTime.Now;
    }
}
