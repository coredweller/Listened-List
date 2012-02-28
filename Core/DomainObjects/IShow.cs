using System;

namespace Core.DomainObjects
{
    public interface IShow : IEntity
    {
        Guid Id { get; }
        string VenueName { get; }
        string City { get; }
        string State { get; }
        string Country { get; }
        string Notes { get; }
        DateTime? ShowDate { get; }
        string VenueNotes { get; }
        string PhishNetUrl { get; }

        string GetShowName();
    }
}
