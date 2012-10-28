using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Services;
using Core.Services.Interfaces;
using Core.Membership;

namespace ListenedList
{
    public partial class Settings : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }
        }

        private void Bind() {
            IProfileService profileService = new ProfileService( User.Identity.Name );

            var user = _MembershipProvider.GetUser( User.Identity.Name );
            var userProfile = profileService.GetUserProfile();

            txtName.Text = userProfile.Name;
            txtEmail.Text = user.Email;
            lblUserName.Text = userProfile.UserName;

            chkPublic.Checked = userProfile.Public.HasValue ? userProfile.Public.Value : false;
        }

        public void btnSaveProfile_Click( object sender, EventArgs e ) {
            IProfileService profileService = new ProfileService( User.Identity.Name );

            bool success = false;
            try {

                var userProfile = profileService.GetUserProfile();
                var user = _MembershipProvider.GetUser( User.Identity.Name );

                if ( txtName.Text != userProfile.Name ) profileService.SaveName( txtName.Text.Trim() );

                if ( txtEmail.Text != user.Email ) {
                    user.Email = txtEmail.Text.Trim();
                    _MembershipProvider.UpdateUser( user );
                }

                profileService.SavePublic( chkPublic.Checked );

                success = true;
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "THERE WAS AN ERROR SAVING PROFILE ON SETTINGS.ASPX with message: " + ex.Message );
                success = false;
            }

            ValidateSuccess( success, "You have successfully saved your profile.", "There was an error saving your profile." );
            Bind();
        }

    }
}