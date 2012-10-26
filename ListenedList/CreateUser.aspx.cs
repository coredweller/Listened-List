using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Membership;
using Core.Services;
using Microsoft.AspNet.FriendlyUrls;
using Core.Configuration;
using Core.Infrastructure;
using System.Text;
using ListenedList.Controls.Templates;
using System.IO;
using System.Net.Mail;

namespace ListenedList
{
    public partial class CreateUser : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            SetPageTitle( "Create Phisherman's Guide User" );
        }

        public void createControl_ContinueButtonClick( object sender, EventArgs e ) {
            Response.Redirect( FriendlyUrl.Href( "~/Logout" ) );
        }

        public void createControl_CreatedUser( object sender, EventArgs e ) {
            bool success = false;
            CreateUserWizard cont = null;

            try {
                cont = (CreateUserWizard)sender;

                var provider = new ListenedRoleProvider();
                provider.AddUsersToRoles( new string[1] { cont.UserName }, new string[1] { base.BaseRoleType } );

                var profileService = new ProfileService( cont.UserName );
                profileService.SavePublic( false );

                success = true;
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "There was a major error creating a new user with a message of: " + ex.Message );
            }

            //If they successfully log create a new user then take them to the main page
            //  Otherwise it will show them an error or in a fatal case it will take them to Login.
            if ( success && cont != null ) {
                SendEmail( cont.UserName, cont.Password, cont.Email );

                System.Web.Security.FormsAuthentication.RedirectFromLoginPage( cont.UserName, true );
                Response.Redirect( FriendlyUrl.Href( "~/Main" ) );
            }
        }

        private void SendEmail( string userName, string password, string email ) {

            var appConfigManager = Ioc.GetInstance<IAppConfigManager>();
            var from = appConfigManager.AppSettings["FromEmail"];
            var to = email;
            var subject = appConfigManager.AppSettings["WelcomeSubject"];
            var body = CreateBody( userName, password );

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
        }

        private string CreateBody( string userName, string password ) {
            StringBuilder sb = new StringBuilder();

            // Load the control
            WelcomeEmail ctrl = (WelcomeEmail)LoadControl( "~/Controls/Templates/WelcomeEmail.ascx" );

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