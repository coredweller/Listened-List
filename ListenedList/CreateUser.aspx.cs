﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Membership;
using Core.Services;
using Core.Configuration;
using Core.Infrastructure;
using System.Text;
using ListenedList.Controls.Templates;
using System.IO;
using System.Net.Mail;
using ListenedList.Code;

namespace ListenedList
{
    public partial class CreateUser : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            SetPageTitle( "Create Phisherman's Guide User" );
        }

        public void createControl_ContinueButtonClick( object sender, EventArgs e ) {
            Response.Redirect( LinkBuilder.DefaultLogoutLink() );
        }

        public void createControl_CreatedUser( object sender, EventArgs e ) {
            bool success = false;
            CreateUserWizard cont = null;

            try {
                cont = (CreateUserWizard)sender;

                _RoleProvider.AddUsersToRoles( new string[1] { cont.UserName }, new string[1] { Core.Membership.Roles.REGISTERED } );

                var profileService = new ProfileService( cont.UserName );
                var profile = profileService.GetUserProfile();
                profile.Public = false;
                profile.ButtonSize = DefaultButtonSize;
                profile.FontSize = DefaultFontSize;
                profile.Save();

                success = true;
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "There was a major error creating a new user with a message of: " + ex.Message );
            }

            //If they successfully create a new user then send a welcome email and take them to the main page
            //  Otherwise it will show them an error or in a fatal case it will take them to Login.
            if ( success && cont != null ) {
                var emailManager = new EmailManager( this.Page );
                emailManager.SendWelcomeEmail( cont.UserName, cont.Password, cont.Email );

                System.Web.Security.FormsAuthentication.RedirectFromLoginPage( cont.UserName, true );
                Response.Redirect( LinkBuilder.DefaultMainLink() );
            }
        }
    }
}