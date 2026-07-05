using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Exceptions
{
    public class DomainException: Exception
    {
        public DomainExceptions(string message): base($"Domain Exception: {message}")
        {
            
        }
    }
}
