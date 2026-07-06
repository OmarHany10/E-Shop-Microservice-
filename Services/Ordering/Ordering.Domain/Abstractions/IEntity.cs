using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Abstractions
{
    public interface IEntity<T>: IEntity
    {
        public T Id { get; set; }
    }
    public interface IEntity
    {
        public string? CreateidBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
