using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ListenedList.Code;

namespace ListenedList.Masters
{
    public partial class Genius : System.Web.UI.MasterPage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            
        }

        public void HeadLoginStatus_LoggingOut( object sender, LoginCancelEventArgs e ) {
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect( LinkBuilder.DefaultLoginLink() );
        }
    }
}