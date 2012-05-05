using System;
using Core.DomainObjects;
using Core.Helpers;

namespace Data.DomainObjects
{
    public class DomainObjectFactory : IDomainObjectFactory
    {
        public IListenedShow CreateListenedShow( Guid showId, Guid userId, DateTime showDate, int status, string notes ) {
            return CreateListenedShow( showId, userId, showDate, status, notes, false );
        }

        public IListenedShow CreateListenedShow( Guid showId, Guid userId, DateTime showDate, int status, string notes, bool attended ) {
            ListenedShow show = new ListenedShow {
                CreatedDate = Constants.Now(),
                Id = Guid.NewGuid(),
                Notes = notes,
                ShowId = showId,
                Status = status,
                UserId = userId,
                ShowDate = showDate,
                Attended = attended,
                UpdatedDate = Constants.Now()
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

        public IShowTag CreateShowTag( Guid showId, Guid tagId, Guid userId ) {
            ShowTag tag = new ShowTag() {
                Id = Guid.NewGuid(),
                ShowId = showId,
                TagId = tagId,
                UserId = userId
            };

            return tag;
        }

        public ISubscription CreateSubscription( Guid userId, Guid subscribedUserId ) {
            Subscription subscription = new Subscription() {
                CreatedDate = Constants.Now(),
                Id = Guid.NewGuid(),
                SubscribedUserId = subscribedUserId,
                UserId = userId
            };

            return subscription;
        }
    }
}
