using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using Core.Infrastructure;
using Core.Configuration;
using System.Web.Security;
using System.Text;
using ListenedList.Controls.Templates;
using System.IO;
using Core.Helpers.Script;
using ListenedList.Code;

namespace ListenedList
{
    public partial class Forgot : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {

        }

        protected void btnEmail_Click( object sender, EventArgs e ) {
            if ( string.IsNullOrEmpty( txtEmail.Text ) ) {
                ShowError( "Please enter an email address." );
                return;
            }

            var emailAddress = txtEmail.Text.Trim();

            var user = _MembershipProvider.GetUserByEmail( emailAddress );
            if ( user == null ) {
                ShowError( "This email is not in our system. Please create a user." );
                return;
            }

            var emailManager = new EmailManager( this.Page );
            var success = emailManager.SendForgotEmail( user.UserName, user.GetPassword(), user.Email );

            ValidateSuccess( success, "An email has been sent with your requested information.", "There was an error processing your request. Please try again later." );
        }

        private string CreateBody( string userName, string password ) {
            StringBuilder sb = new StringBuilder();

            // Load the control
            ForgotEmail ctrl = (ForgotEmail)LoadControl( "~/Controls/Templates/ForgotEmail.ascx" );

            ctrl.UserName = userName;
            ctrl.Password = password;
            ctrl.SetProperties();

            // Render the control into the stringbuilder
            StringWriter sw = new StringWriter( sb );
            Html32TextWriter htw = new Html32TextWriter( sw );
            ctrl.RenderControl( htw );

            // Get full body text
            return sb.ToString();
        }
    }
}