using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using Core.Extensions;
using ListenedList.Code;
using ListenedList.Controls.Templates;
using System.Text;
using System.IO;

namespace ListenedList
{
    public partial class Test : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            if ( !_RoleProvider.IsUserInRole( User.Identity.Name, Core.Membership.Roles.ADMINISTRATOR ) ) 
                Response.Redirect( LinkBuilder.DefaultMainLink() );
        }

        protected void btnTestForgot_Click( object sender, EventArgs e ) {
            var emailManager = new EmailManager( this.Page );
            
             try {
                emailManager.SendForgotEmail( "coredweller", "empires", "daniel.perillo@comcast.net" );
            }
            catch ( Exception ex ) {
                lblOutput.Text = ex.ToString();
            }
        }

        protected void btnTestWelcome_Click( object sender, EventArgs e ) {
            var emailManager = new EmailManager( this.Page );

             try {
                emailManager.SendWelcomeEmail( "coredweller", "empires", "daniel.perillo@comcast.net" );
            }
            catch ( Exception ex ) {
                lblOutput.Text = ex.ToString();
            }
            
            //SendEmail( null );
        }

        protected void btnForgotEmail_Click( object sender, EventArgs e ) {
            ForgotEmail ctrl = (ForgotEmail)this.Page.LoadControl( "/Controls/Templates/ForgotEmail.ascx" );
            ctrl.UserName = "coredweller";
            ctrl.Password = "fakePassword";
            ctrl.SetProperties();
            
            phControl.Controls.Add( ctrl );
        }

        protected void btnWelcomeEmail_Click( object sender, EventArgs e ) {
            WelcomeEmail ctrl = (WelcomeEmail)this.Page.LoadControl( "/Controls/Templates/WelcomeEmail.ascx" );
            ctrl.UserName = "coredweller";
            ctrl.Password = "fakePassword";
            ctrl.SetProperties();

            phControl.Controls.Add( ctrl );
        }
    }
}