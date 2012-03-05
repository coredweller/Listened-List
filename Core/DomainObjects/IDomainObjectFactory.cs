﻿using System;

namespace Core.DomainObjects
{
    public interface IDomainObjectFactory
    {
        IListenedShow CreateListenedShow( Guid showId, Guid userId, DateTime showDate, int status, string notes );
        IShow CreateShow( string venue, string city, string state, string country, string notes, DateTime showDate );
        ITag CreateTag( string name, Guid showId, Guid userId );
    }
}
