using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repository
{
    using System;
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);

        void Remove(TEntity entity);
    }
}
