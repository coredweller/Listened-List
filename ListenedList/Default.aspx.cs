using System;
using Microsoft.AspNet.FriendlyUrls;
using ListenedList.Code;

namespace ListenedList
{
    public partial class _Default : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            Response.Redirect( LinkBuilder.DefaultMainLink() );
        }
    }
}
