using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DomainObjects
{
    public interface IShowTag
    {
        Guid Id { get; }
        Guid ShowId { get; }
        Guid TagId { get; }
        Guid UserId { get; }
    }
}
