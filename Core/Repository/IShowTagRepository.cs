using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainObjects;

namespace Core.Repository
{
    public interface IShowTagRepository
    {
        void Add(IShowTag entity);
        void Remove(IShowTag entity);

        IShowTag FindById(Guid id);
        IQueryable<IShowTag> FindAll();
    }
}
