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
            SetPageTitle( "Create Listened List User" );
        }

        public void createControl_ContinueButtonClick( object sender, EventArgs e ) {
            Response.Redirect( "~/Logout.aspx" );
        }

        public void createControl_CreatedUser( object sender, EventArgs e ) {
            var cont = (CreateUserWizard)sender;

            var provider = new ListenedRoleProvider();
            provider.AddUsersToRoles( new string[1] { cont.UserName }, new string[1] { base.BaseRoleType } );

            var profileService = new ProfileService( cont.UserName );
            profileService.SavePublic( false );
        }
    }
}