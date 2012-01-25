using System;

namespace Core.Helpers
{
    public class ShowStatus
    {
        public Guid ShowId { get; set; }
        public int Status { get; set; }
        public DateTime ShowDate { get; set; }

        public ShowStatus( Guid showId, int status ) {
            ShowId = showId;
            Status = status;
        }

        public ShowStatus( Guid showId, int status, DateTime showDate ) {
            ShowId = showId;
            Status = status;
            ShowDate = showDate;
        }
    }
}