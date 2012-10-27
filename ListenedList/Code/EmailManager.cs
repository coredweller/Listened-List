using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Infrastructure;
using Core.Configuration;
using System.Net.Mail;
using Core.Infrastructure.Logging;
using ListenedList.Controls.Templates;
using System.Web.UI;
using System.IO;

namespace ListenedList.Code
{
    public class EmailManager
    {
        private LogWriter _Log = new LogWriter();
        private Page _Page { get; set; }

        IAppConfigManager _AppConfigManager = Ioc.GetInstance<IAppConfigManager>();
        private string _FromEmail;

        public EmailManager(System.Web.UI.Page page){
            _Page = page;
            _FromEmail = _AppConfigManager.AppSettings["FromEmail"];
        }

        public void SendWelcomeEmail( string userName, string password, string toEmail ) {
            var subject = _AppConfigManager.AppSettings["WelcomeSubject"];

            WelcomeEmail ctrl = (WelcomeEmail)_Page.LoadControl( "/Controls/Templates/WelcomeEmail.ascx" );
            ctrl.UserName = userName;
            ctrl.Password = password;
            ctrl.SetProperties();

            var body = RenderControl(ctrl);

            MailMessage mailObj = new MailMessage( _FromEmail, toEmail, subject, body );
            
            SendEmail(mailObj, true);
        }

        public bool SendForgotEmail( string userName, string password, string toEmail ) {
            var subject = _AppConfigManager.AppSettings["ForgotSubject"];

            ForgotEmail ctrl = (ForgotEmail)_Page.LoadControl( "/Controls/Templates/ForgotEmail.ascx" );
            ctrl.UserName = userName;
            ctrl.Password = password;
            ctrl.SetProperties();

            var body = RenderControl(ctrl);

            MailMessage mailObj = new MailMessage( _FromEmail, toEmail, subject, body );
            
            return SendEmail( mailObj, true );
        }


        /***************
            HELPER FUNCTIONS
         * **********/

        private bool SendEmail( MailMessage message, bool html ) {
            SmtpClient SMTPServer = new SmtpClient();

            if ( html ) {
                message.BodyEncoding = Encoding.ASCII;
                message.IsBodyHtml = true;
            }

            bool success = false;
            try {
                SMTPServer.Send( message );
                success = true;
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "There was an error sending an email in ListenedList/Code/EmailManager. Email that was requested was: " + message.To );
            }

            return success;
        }

        private string RenderControl(UserControl control) {
            StringBuilder sb = new StringBuilder();

            // Render the control into the stringbuilder
            StringWriter sw = new StringWriter( sb );
            Html32TextWriter htw = new Html32TextWriter( sw );
            control.RenderControl( htw );

            // Get full body text
            return sb.ToString();
        }
    }
}
