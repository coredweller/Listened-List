using Core.DomainObjects;
using System.Text;
using Core.Extensions;

namespace Data.DomainObjects
{
    public partial class Show : IShow
    {
        private const string SEPARATOR = " - ";

        public string GetShowName() {
            var showBuilder = new StringBuilder();

            //Always append the showdate because it always exists
            showBuilder.Append( ShowDate.Value.ToString( "MM/dd/yyyy" ) );

            //If there is a venue then append it
            if ( !VenueName.IsNullEmptyOrWhitespace() ) showBuilder.Append( SEPARATOR + VenueName );

            //If there is a city
            if ( !City.IsNullEmptyOrWhitespace() ) {
                showBuilder.Append( SEPARATOR );
                //If there is a state then append both city and state
                if ( !State.IsNullEmptyOrWhitespace() ) {
                    showBuilder.Append( City + ", " + State );
                }
                //Otherwise just append the city
                else {
                    showBuilder.Append( City );
                }
            }
            //If there is NO city
            else {
                //If there is a state though it could be a country
                if ( !State.IsNullEmptyOrWhitespace() ) {
                    showBuilder.Append( SEPARATOR + State );
                }
            }

            //If there is a country besides USA then show it
            if ( !Country.IsNullEmptyOrWhitespace() && Country != "USA" ) showBuilder.Append( SEPARATOR + Country );

            return showBuilder.ToString();
        }
    }
}