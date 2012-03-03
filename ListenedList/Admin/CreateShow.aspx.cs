using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Infrastructure;
using Core.Services.Interfaces;

namespace ListenedList.Admin
{
    public partial class CreateShow : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
        }

        public void btnSubmit_Click( object sender, EventArgs e ) {
            if ( string.IsNullOrEmpty( ddlState.SelectedValue ) || string.IsNullOrEmpty( ddlCountry.SelectedValue ) ) {
                Page.RegisterStartupScript( "failure", "<script type=\"text/javascript\"> $.prompt('Please choose a valid state and country.'); </script>" );
                return;
            }

            DateTime showDate;
            var parsed = DateTime.TryParse( txtShowDate.Text, out showDate );

            if ( !parsed ) {
                Page.RegisterStartupScript( "failure", "<script type=\"text/javascript\"> $.prompt('Please enter a valid date for the show.'); </script>" );
                return;
            }

            var show = _DomainObjectFactory.CreateShow( txtVenue.Text, txtCity.Text, ddlState.SelectedValue, ddlCountry.SelectedValue, txtNotes.Text, showDate );

            var showService = Ioc.GetInstance<IShowService>();

            bool success = false;
            showService.SaveCommit( show, out success );

            if ( success ) {
                Page.RegisterStartupScript( "success", "<script type=\"text/javascript\"> $.prompt('You have successfully saved the show.'); </script>" );
            }
            else {
                Page.RegisterStartupScript( "failure", "<script type=\"text/javascript\"> $.prompt('There was an error saving the show.'); </script>" );
            }
        }
    }
}