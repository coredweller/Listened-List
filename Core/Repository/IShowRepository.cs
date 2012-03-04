using System;
using System.Linq;
using Core.DomainObjects;

namespace Core.Repository
{
    public interface IShowRepository
    {
        void Add(IShow entity);
        void Remove(IShow entity);

        IShow FindByShowDate(DateTime date);
        IShow FindById(Guid id);
        IQueryable<IShow> FindAll();
    }
}