using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Helpers
{
    public class ShowStatus
    {
        public Guid ShowId { get; set; }
        public int Status { get; set; }
        public DateTime ShowDate { get; set; }
        public string ShowName { get; set; }
        public bool Attended { get; set; }

        //public ShowStatus( Guid showId, int status ) {
        //    ShowId = showId;
        //    Status = status;
        //    Attended = false;
        //}

        //public ShowStatus( Guid showId, int status, DateTime showDate ) {
        //    ShowId = showId;
        //    Status = status;
        //    ShowDate = showDate;
        //    Attended = false;
        //}

        public ShowStatus( Guid showId, int status, bool attended ) {
            ShowId = showId;
            Status = status;
            Attended = attended;
        }

        public ShowStatus( Guid showId, int status, DateTime showDate, string showName ) {
            ShowId = showId;
            Status = status;
            ShowDate = showDate;
            ShowName = showName;
            Attended = false;
        }

        public ShowStatus( Guid showId, int status, DateTime showDate, string showName, bool attended ) {
            ShowId = showId;
            Status = status;
            ShowDate = showDate;
            ShowName = showName;
            Attended = attended;
        }
    }

    public static class ShowStatusExtension
    {
        public static IList<ShowStatus> GetShowsByMonth( this IEnumerable<ShowStatus> shows, Month month ) {
            return shows.Where( x => x.ShowDate.Month == (int)month ).ToList();
        }
    }
}