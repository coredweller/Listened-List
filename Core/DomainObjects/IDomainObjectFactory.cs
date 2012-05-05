using System;

namespace Core.DomainObjects
{
    public interface IDomainObjectFactory
    {
        IListenedShow CreateListenedShow( Guid showId, Guid userId, DateTime showDate, int status, string notes );
        IListenedShow CreateListenedShow( Guid showId, Guid userId, DateTime showDate, int status, string notes, bool attended );
        IShow CreateShow( string venue, string city, string state, string country, string notes, DateTime showDate );
        ITag CreateTag( string name, Guid userId ); 
        IShowTag CreateShowTag( Guid showId, Guid tagId, Guid userId );
        ISubscription CreateSubscription( Guid userId, Guid subscribedUserId );
    }
}
