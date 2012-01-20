using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DomainObjects
{
    public interface IListenedShow : IEntity
    {
        Guid Id { get; }
        Guid UserId { get; }
        Guid ShowId { get; }
        string Notes { get; }
        double? Stars { get; }
        int Status { get; }
    }
}
