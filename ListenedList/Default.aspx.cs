using System;
using Microsoft.AspNet.FriendlyUrls;

namespace ListenedList
{
    public partial class _Default : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            Response.Redirect( FriendlyUrl.Href( "~/Main" ) );
        }
    }
}
