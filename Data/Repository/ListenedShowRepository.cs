using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainObjects;
using Data.DomainObjects;
using Core.Infrastructure.Logging;
using Core.Helpers;
using Core.Exceptions;
using Core.Repository;

namespace Data.Repository
{
    public class ListenedShowRepository : BaseRepository<IListenedShow, ListenedShow>,  IListenedShowRepository
    {
        LogWriter writer = new LogWriter();
        public ListenedShowRepository(IDatabase database) : base(database) { }

        public ListenedShowRepository(IDatabaseFactory factory) : base(factory) { }

        private IQueryable<IListenedShow> GetAll()
        {
            return Database.ListenedShowDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<IListenedShow> FindAll()
        {
            return GetAll();
        }

        public IListenedShow FindByShowId(Guid id)
        {
            return GetAll().SingleOrDefault(show => show.ShowId == id);
        }

        public IListenedShow FindById( Guid id ) {
            return GetAll().SingleOrDefault( show => show.Id == id );
        }
        
        public override void Add(IListenedShow entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(show => show.Id == entity.Id))
            {
                writer.WriteLine("A ListenedShow with an id={0}".FormatWith(entity.Id));
                throw new AlreadyExistsException("A ListenedShow with an id={0}".FormatWith(entity.Id));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IListenedShow entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
