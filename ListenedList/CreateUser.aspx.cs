using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Membership;
using Core.Services;

namespace ListenedList
{
    public partial class CreateUser : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            SetPageTitle( "Create Phisherman's Guide User" );
        }

        public void createControl_ContinueButtonClick( object sender, EventArgs e ) {
            Response.Redirect( "~/Logout.aspx" );
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

            //If they successfully log create a new user then take them to the default page
            //  Otherwise it will show them an error or in a fatal case it will take them to Login.
            if ( success && cont != null ) {
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage( cont.UserName, true );
                Response.Redirect( "~/" );
            }
        }
    }
}