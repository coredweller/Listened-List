
using System;
using System.Linq;
namespace Core.Repository
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> FindAll();
        TEntity FindById( Guid id );

        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
