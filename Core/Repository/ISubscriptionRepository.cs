using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainObjects;

namespace Core.Repository
{
    public interface ISubscriptionRepository
    {
        void Add(ISubscription entity);
        void Remove(ISubscription entity);

        ISubscription FindById(Guid id);
        IQueryable<ISubscription> FindAll();
    }
}
