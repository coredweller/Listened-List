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

            var appConfigManager = Ioc.GetInstance<IAppConfigManager>();
            var from = appConfigManager.AppSettings["FromEmail"];
            var to = emailAddress;
            var subject = appConfigManager.AppSettings["ForgotSubject"];
            var body = CreateBody( user.UserName, user.GetPassword() );

            MailMessage mailObj = new MailMessage( from, to, subject, body );
            mailObj.BodyEncoding = Encoding.ASCII;
            mailObj.IsBodyHtml = true;

            SmtpClient SMTPServer = new SmtpClient();

            bool success = false;
            try {
                SMTPServer.Send( mailObj );
                success = true;
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "There was an error sending an email on Forgot.aspx. Email that was requested was: " + to );
            }

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

        private void ShowError( string errorMessage ) {
            var prompt = new PromptHelper( errorMessage );
            var errorScript = prompt.GetErrorScript();
            Page.RegisterStartupScript( "validateSuccess", errorScript );
        }
    }
}