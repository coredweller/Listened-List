using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DomainObjects
{
    public interface ITag : IEntity
    {
        Guid Id { get; }
        Guid UserId { get; }

        string Name { get; set; }
        string Color { get; set; }
    }
}
