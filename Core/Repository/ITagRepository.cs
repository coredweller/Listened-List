using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainObjects;

namespace Core.Repository
{
    public interface ITagRepository
    {
        void Add(ITag entity);
        void Remove(ITag entity);

        ITag FindById(Guid id);
        IQueryable<ITag> FindAll();
    }
}
