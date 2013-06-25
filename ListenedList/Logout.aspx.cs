using System;
using ListenedList.Code;

namespace ListenedList
{
    public partial class Logout : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect( LinkBuilder.DefaultLoginLink() );
        }
    }
}