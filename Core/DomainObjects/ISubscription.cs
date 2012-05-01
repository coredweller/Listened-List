using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DomainObjects
{
    public interface ISubscription : IEntity
    {
        Guid Id { get; }
        Guid UserId { get; }
        Guid SubscribedUserId { get; }
    }
}
