using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainObjects;

namespace Core.Services.Interfaces
{
    public interface IListenedShowService : IBase<IListenedShow>
    {
        IQueryable<IListenedShow> GetAllShows();
        IListenedShow GetById( Guid id );
        IListenedShow GetByShowId( Guid id );
        IListenedShow GetByUserAndShowId( Guid userId, Guid showId );
        IQueryable<IListenedShow> GetByUser( Guid userId );
        IQueryable<IListenedShow> GetShowsByYear( int year, Guid userId );
        IDictionary<IShow, IListenedShow> GetBothShowsByYear( int year, Guid userId );
        IList<IListenedShow> GetByUserIds( IList<Guid> userIds );
        IListenedShow GetLatestByUserId( Guid userId );
        IList<IListenedShow> GetByAttendedForUser( Guid userId );
    }
}
