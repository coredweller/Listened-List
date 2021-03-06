﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainObjects;

namespace Core.Services.Interfaces
{
    public interface IShowTagService : IBase<IShowTag>
    {
        IQueryable<IShowTag> GetAllTags();

        IShowTag GetTag( Guid id );
        IList<IShowTag> GetTagsByShow( Guid showId );
        IList<IShowTag> GetTagsByShowAndUser( Guid showId, Guid userId );
        IQueryable<IShowTag> GetTagsByTagAndUser( Guid tagId, Guid userId );
        IList<IShowTag> GetShowTagsByTagId( Guid tagId );

        void Delete( Guid tagId );
    }
}
