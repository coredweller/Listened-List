using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainObjects;

namespace Core.Repository
{
    public interface IListenedShowRepository
    {
        IQueryable<IListenedShow> FindAll();
        IListenedShow FindByShowId( Guid id );
        void Add( IListenedShow entity );
        void Remove( IListenedShow entity );
    }
}
