using System;
using System.Web.UI.WebControls;
using ListenedList.Code;

namespace ListenedList.Masters
{
    public partial class Wooden : System.Web.UI.MasterPage
    {
        protected void Page_Load( object sender, EventArgs e ) {

        }

        public void HeadLoginStatus_LoggingOut( object sender, LoginCancelEventArgs e ) {
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect( LinkBuilder.DefaultLoginLink() );
        }
    }
}