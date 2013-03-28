using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using Core.Membership;

namespace ListenedList.Admin
{
    public partial class AssignAdmin : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            CheckPermissions();

            if ( IsPostBack ) return;

            Bind();
        }

        private void Bind() {
            rptAdmins.DataSource = GetUsers();
            rptAdmins.DataBind();
        }

        private string[] GetUsers() {
            return _RoleProvider.GetUsersInRole( Core.Membership.Roles.ADMINISTRATOR );
        }

        protected void rptAdmins_ItemCommand( object source, RepeaterCommandEventArgs e ) {
            CheckPermissions();

            switch ( e.CommandName.ToLower() ) {
                case "remove":
                    var user = _MembershipProvider.GetUser( e.CommandArgument.ToString() );
                    if ( user == null ) return;

                    _RoleProvider.RemoveUsersFromRoles( new string[1] { user.UserName }, new string[1] { Core.Membership.Roles.ADMINISTRATOR } );
                    break;
            }

            Bind();
        }

        protected void btnMakeAdmin_Click( object sender, EventArgs e ) {
            CheckPermissions();

            if ( string.IsNullOrEmpty( txtUserName.Text ) ) return;

            var success = false;
            try {
                var user = _MembershipProvider.GetUser( txtUserName.Text );

                if ( user == null ) ShowError( "Please enter a valid user name" );

                _RoleProvider.AddUsersToRoles( new string[1] { txtUserName.Text }, new string[1] { Core.Membership.Roles.ADMINISTRATOR } );
                success = true;
            }
            catch ( Exception ) {
                _Log.WriteFatal( "There was an error assigning the admin role to the user: " + txtUserName.Text );
            }

            ValidateSuccess( success, "You have successfully made " + txtUserName.Text + " an admin!", "There was an error adminizing " + txtUserName.Text );

            Bind();
        }

        protected void CheckPermissions() {
            UserInRoleRedirect( User.Identity.Name, Core.Membership.Roles.ADMINISTRATOR );
        }
    }
}