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
    public class ShowTagRepository: BaseRepository<IShowTag, ShowTag>,  IShowTagRepository
    {
        LogWriter writer = new LogWriter();
        public ShowTagRepository(IDatabase database) : base(database) { }

        public ShowTagRepository(IDatabaseFactory factory) : base(factory) { }

        private IQueryable<IShowTag> GetAll()
        {
            return Database.ShowTagDataSource;
        }

        public override IQueryable<IShowTag> FindAll()
        {
            return GetAll();
        }

        public override IShowTag FindById(Guid id)
        {
            return GetAll().SingleOrDefault(ShowTag => ShowTag.Id == id);
        }

        public override void Add(IShowTag entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            if (GetAll().Any(ShowTag => ShowTag.Id == entity.Id))
            {
                writer.WriteLine("A ShowTag with an id={0}".FormatWith(entity.Id));
                throw new AlreadyExistsException("A ShowTag with an id={0}".FormatWith(entity.Id));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IShowTag entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
