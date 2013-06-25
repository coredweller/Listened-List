using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListenedList.Code;

namespace ListenedList.Controls
{
    public partial class Login : System.Web.UI.UserControl
    {
        public string CreateUserLink { get { return LinkBuilder.DefaultCreateUserLink(); } }
        public string ForgotPasswordLink { get { return LinkBuilder.DefaultForgotLink(); } }

        protected void Page_Load( object sender, EventArgs e ) {
            
        }

        protected void loginControl_LoggedIn( object sender, EventArgs e ) {
            Response.Redirect( LinkBuilder.DefaultMainLink() );
        }

    }
}