using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Infrastructure.Logging;
using Core.DomainObjects;
using Data.DomainObjects;
using Core.Helpers;
using Core.Exceptions;
using Core.Repository;

namespace Data.Repository
{
    public class SubscriptionRepository : BaseRepository<ISubscription, Subscription>, ISubscriptionRepository
    {
        LogWriter writer = new LogWriter();
        public SubscriptionRepository(IDatabase database) : base(database) { }

        public SubscriptionRepository(IDatabaseFactory factory) : base(factory) { }

        private IQueryable<ISubscription> GetAll()
        {
            return Database.SubscriptionDataSource.Where(x => x.Deleted == false);
        }

        public override IQueryable<ISubscription> FindAll()
        {
            return GetAll();
        }

        public override ISubscription FindById(Guid id)
        {
            return GetAll().SingleOrDefault(Subscription => Subscription.Id == id);
        }

        public override void Add(ISubscription entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            if (GetAll().Any(Subscription => Subscription.Id == entity.Id))
            {
                writer.WriteLine("A Subscription with an id={0}".FormatWith(entity.Id));
                throw new AlreadyExistsException("A Subscription with an id={0}".FormatWith(entity.Id));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(ISubscription entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
