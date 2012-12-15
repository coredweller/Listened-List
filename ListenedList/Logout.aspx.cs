using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;

namespace ListenedList
{
    public partial class Logout : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            ClearCachedUserId();

            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect( FriendlyUrl.Href( "~/Login" ) );
        }
    }
}