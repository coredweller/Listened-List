using System;
using Core.DomainObjects;
using Core.Helpers;

namespace Data.DomainObjects
{
    public class DomainObjectFactory : IDomainObjectFactory
    {
        public IListenedShow CreateListenedShow(Guid showId, Guid userId, DateTime showDate, int status, string notes) {
            ListenedShow show = new ListenedShow {
                CreatedDate = Constants.Now(),
                Id = Guid.NewGuid(),
                Notes = notes,
                ShowId = showId,
                Status = status,
                UserId = userId,
                ShowDate = showDate
            };

            return show;
        }

        public IShow CreateShow( string venue, string city, string state, string country, string notes, DateTime showDate ) {
            Show show = new Show {
                CreatedDate = Constants.Now(),
                Id = Guid.NewGuid(),
                Notes = notes,
                City = city,
                Country = country,
                ShowDate = showDate,
                State = state,
                VenueName = venue
            };

            return show;
        }

        public ITag CreateTag( string name, Guid userId ) {
            Tag tag = new Tag {
                CreatedDate = Constants.Now(),
                Id = Guid.NewGuid(),
                Name = name,
                UserId = userId,
                Color = TagColors.Blue.CssClass
            };

            return tag;
        }

        public IShowTag CreateShowTag( Guid showId, Guid tagId ) {
            ShowTag tag = new ShowTag() {
                Id = Guid.NewGuid(),
                ShowId = showId,
                TagId = tagId
            };

            return tag;
        }
    }
}
