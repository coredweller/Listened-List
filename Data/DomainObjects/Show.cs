using Core.DomainObjects;

namespace Data.DomainObjects
{
    public partial class Show : IShow
    {
        public string GetShowName() {
            return string.Format( "{0} - {1} - {2}, {3}", ShowDate.Value.ToString( "MM/dd/yyyy" ), VenueName, City, State );
        }
    }
}