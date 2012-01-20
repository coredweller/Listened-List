using System;
using System.Linq;
using Core.DomainObjects;
using Data.DomainObjects;
using Core.Infrastructure.Logging;
using Core.Helpers;
using Core.Exceptions;
using Core.Repository;

namespace Data.Repository
{
    public class ShowRepository : BaseRepository<IShow, Show>,  IShowRepository
    {
        LogWriter writer = new LogWriter();
        public ShowRepository(IDatabase database) : base(database) { }

        public ShowRepository(IDatabaseFactory factory) : base(factory) { }

        private IQueryable<IShow> GetAll()
        {
            return Database.ShowDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<IShow> FindAll()
        {
            return GetAll().OrderBy(s => s.ShowDate);
        }

        public IShow FindByShowId(Guid id)
        {
            return GetAll().SingleOrDefault(show => show.Id == id);
        }

        public IShow FindByShowDate(DateTime date)
        {
            return GetAll().SingleOrDefault(show => show.ShowDate == date);
        }

        public override void Add(IShow entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(show => show.Id == entity.Id))
            {
                writer.WriteLine("A Show with an id={0}".FormatWith(entity.Id));
                throw new AlreadyExistsException("A Show with an id={0}".FormatWith(entity.Id));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IShow entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
