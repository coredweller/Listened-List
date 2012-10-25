using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using Core.Extensions;

namespace ListenedList
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e ) {

        }

        protected void btnTestSmtpConfig_Click( object sender, EventArgs e ) {
            SendEmail( null );
        }

        protected void btnTestSmtpLocalhost_Click( object sender, EventArgs e ) {
            SendEmail( "localhost" );
        }

        protected void SendEmail( string host ) {
            var from = "friend@phishermansguide.com";
            var to = "dperillo1785@gmail.com";
            var subject = "Test Phisherman's Guide Email";
            var body = "Please let this go through";

            MailMessage mailObj = new MailMessage( from, to, subject, body );

            SmtpClient SMTPServer;
            if(host == null)
                SMTPServer = new SmtpClient();
            else
                SMTPServer = new SmtpClient( "localhost" );

            try {
                SMTPServer.Send( mailObj );
            }
            catch ( Exception ex ) {
                lblOutput.Text = ex.ToString();
            }
        }
    }
}