using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DomainObjects
{
    public interface IDomainObjectFactory
    {
        IListenedShow CreateListenedShow( Guid showId, Guid userId, DateTime showDate, int status, string notes );
        IShow CreateShow( string venue, string city, string state, string country, string notes, DateTime showDate );
    }
}
