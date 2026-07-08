using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.DTOs
{
    public record AddressDTO(string FirstName, string LastName, string EmailAddress, string AddressLine, string Country, string State, string ZipCode);
}
