using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;

namespace ListenedList.Masters
{
    public partial class Wooden : System.Web.UI.MasterPage
    {
        protected void Page_Load( object sender, EventArgs e ) {

        }

        public void HeadLoginStatus_LoggingOut( object sender, LoginCancelEventArgs e ) {
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect( FriendlyUrl.Href( "~/Login" ) );
        }
    }
}