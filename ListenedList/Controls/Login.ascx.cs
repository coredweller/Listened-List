using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;

namespace ListenedList.Controls
{
    public partial class Login : System.Web.UI.UserControl
    {
        protected void Page_Load( object sender, EventArgs e ) {
            
        }

        protected void loginControl_LoggedIn( object sender, EventArgs e ) {
            Response.Redirect( FriendlyUrl.Href( "~/Default" ) );
        }

    }
}