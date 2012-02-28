using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainObjects;
using Core.Helpers;

namespace Core.Services.Interfaces
{
    public interface IShowService : IBase<IShow>
    {
        IQueryable<IShow> GetAllShows();
        IShow GetShow( Guid id );
        IShow GetShow( DateTime date );
        IList<IShow> GetShows( IList<DateTime> dates );
        IList<IShow> GetShows( IList<Guid> showIds );
        IQueryable<IShow> GetShowsByYear( int year );
        List<ShowStatus> GetShowStatusByYear( int year );
        IList<int> GetYears();
    }
}
