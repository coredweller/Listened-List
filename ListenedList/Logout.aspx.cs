using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;

namespace ListenedList
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e ) {
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect( FriendlyUrl.Href( "~/Login" ) );
        }
    }
}