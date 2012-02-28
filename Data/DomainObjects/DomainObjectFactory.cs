using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainObjects;

namespace Data.DomainObjects
{
    public class DomainObjectFactory : IDomainObjectFactory
    {
        public IListenedShow CreateListenedShow(Guid showId, Guid userId, DateTime showDate, int status, string notes) {
            ListenedShow show = new ListenedShow {
                CreatedDate = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Notes = notes,
                ShowId = showId,
                Status = status,
                UserId = userId,
                ShowDate = showDate
            };

            return show;
        }
    }
}
