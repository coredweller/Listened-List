using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Infrastructure;
using Core.Services.Interfaces;
using Core.Helpers.Script;
using Microsoft.AspNet.FriendlyUrls;

namespace ListenedList.Admin
{
    public partial class CreateShow : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            UserInRoleRedirect( User.Identity.Name, Core.Membership.Roles.ADMINISTRATOR );
        }

        public void btnSubmit_Click( object sender, EventArgs e ) {
            PromptHelper prompt;

            if ( string.IsNullOrEmpty( ddlCountry.SelectedValue ) ) {
                prompt = new PromptHelper( "Please choose a valid country." );
                Page.RegisterStartupScript( prompt.ScriptName, prompt.GetErrorScript() );
                return;
            }

            bool success = false;

            try {
                
                DateTime showDate;
                var parsed = DateTime.TryParse( txtShowDate.Text, out showDate );

                if ( !parsed ) {
                    prompt = new PromptHelper( "Please enter a valid date for the show." );
                    Page.RegisterStartupScript( prompt.ScriptName, prompt.GetErrorScript() );
                    return;
                }

                var show = _DomainObjectFactory.CreateShow( txtVenue.Text, txtCity.Text, ddlState.SelectedValue, ddlCountry.SelectedValue, txtNotes.Text, showDate );

                var showService = Ioc.GetInstance<IShowService>();
                
                showService.SaveCommit( show, out success );
            }
            catch ( Exception ex) {
                _Log.WriteFatal( "THERE WAS AN ERROR CREATING A SHOW IN CREATESHOW with message: " + ex.Message );
                success = false;
            }

            ValidateSuccess(success, "You have successfully saved the show.", "There was an error saving the show." );
        }
    }
}