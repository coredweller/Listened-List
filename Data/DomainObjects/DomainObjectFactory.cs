using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainObjects;

namespace Data.DomainObjects
{
    public class DomainObjectFactory
    {
        public IListenedShow CreateListenedShow(Guid showId, Guid userId, int status, string notes) {
            ListenedShow show = new ListenedShow {
                CreatedDate = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Notes = notes,
                ShowId = showId,
                Status = status,
                UserId = userId
            };

            return show;
        }
    }
}
