using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;

namespace ListenedList
{
    public partial class Notes : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }
        }

        private void Bind() {

            DateTime showDate;
            if ( DateTime.TryParse( Request.QueryString["showDate"], out showDate ) ) {
                var showService = new ShowService( Ioc.GetInstance<IShowRepository>() );

                var show = showService.GetShow( showDate );

                if ( show == null ) return;

                var userId = new Guid( membershipProvider.GetUser( User.Identity.Name ).ProviderUserKey.ToString() );

                var listenedShowService = new ListenedShowService( Ioc.GetInstance<IListenedShowRepository>() );
                var listenedShow = listenedShowService.GetByUserAndShowId( userId, show.Id );

                if ( listenedShow == null ) return;

                hdnListenedId.Value = listenedShow.Id.ToString();
                txtNotes.Text = listenedShow.Notes;
            }
        }

        public void btnSubmit_Click( object sender, EventArgs e ) {
            if ( string.IsNullOrEmpty( txtNotes.Text ) || string.IsNullOrEmpty( hdnListenedId.Value ) ) return;

            var success = false;

            var listenedId = new Guid( hdnListenedId.Value );

            var listenedShowService = new ListenedShowService( Ioc.GetInstance<IListenedShowRepository>() );

            using ( IUnitOfWork uow = UnitOfWork.Begin() ) {

                var listenedShow = listenedShowService.GetById( listenedId );
                listenedShow.Notes = txtNotes.Text;

                uow.Commit();
                success = true;
            }

            if ( success ) {
                Page.RegisterStartupScript( "success", "<script type=\"text/javascript\"> $.prompt('You have saved your notes for this show.'); </script>" );
            }
            else {
                Page.RegisterStartupScript( "failure", "<script type=\"text/javascript\"> $.prompt('There was an error saving your notes for this show.'); </script>" );
            }
        }
    }
}