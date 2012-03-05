using System;
using System.Linq;
using Core.DomainObjects;
using System.Collections.Generic;

namespace Core.Services.Interfaces
{
    public interface ITagService : IBase<ITag>
    {
        IQueryable<ITag> GetAllTags();

        ITag GetTag( Guid id );
        ITag GetTag( string name, Guid userId );

        IList<ITag> GetTags( Guid userId );
    }
}
