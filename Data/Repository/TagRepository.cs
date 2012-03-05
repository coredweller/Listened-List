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
    public class TagRepository : BaseRepository<ITag, Tag>,  ITagRepository
    {
        LogWriter writer = new LogWriter();
        public TagRepository(IDatabase database) : base(database) { }

        public TagRepository(IDatabaseFactory factory) : base(factory) { }

        private IQueryable<ITag> GetAll()
        {
            return Database.TagDataSource.Where(x => x.Deleted == false);
        }

        public override IQueryable<ITag> FindAll()
        {
            return GetAll();
        }

        public override ITag FindById(Guid id)
        {
            return GetAll().SingleOrDefault(Tag => Tag.Id == id);
        }

        public override void Add(ITag entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(Tag => Tag.Id == entity.Id))
            {
                writer.WriteLine("A Tag with an id={0}".FormatWith(entity.Id));
                throw new AlreadyExistsException("A Tag with an id={0}".FormatWith(entity.Id));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(ITag entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
