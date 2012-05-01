using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainObjects;

namespace Core.Services.Interfaces
{
    public interface ISubscriptionService : IBase<ISubscription>
    {
        IQueryable<ISubscription> GetAllSubscriptions();
        ISubscription GetSubscription( Guid id );
        IList<ISubscription> GetSubscriptionsByUser( Guid userId );
    }
}
