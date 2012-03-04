﻿using System;
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
        IDictionary<IShow, IListenedShow> GetShowsByYear( int year, Guid userId );
    }
}