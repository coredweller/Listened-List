using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;
using Core.DomainObjects;
using Data.DomainObjects;

namespace ListenedList
{
    public partial class Notes : ListenedBasePage
    {
        public string ShowTitle { get; set; }

        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }

            //Has to be after bind so the first time the user comes to the page
            // The show title will be set in Bind()
            ShowTitle = hdnShowTitle.Value;
        }

        private void Bind() {

            DateTime showDate;
            Guid showId;
            
            var showService = new ShowService( Ioc.GetInstance<IShowRepository>() );

            IShow show = null;

            if ( DateTime.TryParse( Request.QueryString["showDate"], out showDate ) ) {
                show = showService.GetShow( showDate );
            }
            else if ( Guid.TryParse( Request.QueryString["showId"], out showId ) ) {
                show = showService.GetShow( showId );
            }

            if ( show == null ) return;

            hdnShowTitle.Value = ( (Show)show ).GetShowName();

            var listenedShowService = new ListenedShowService( Ioc.GetInstance<IListenedShowRepository>() );

            var userId = new Guid( membershipProvider.GetUser( User.Identity.Name ).ProviderUserKey.ToString() );
            var listenedShow = listenedShowService.GetByUserAndShowId( userId, show.Id );

            if ( listenedShow == null ) return;

            hdnListenedId.Value = listenedShow.Id.ToString();
            txtNotes.Text = listenedShow.Notes;
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

        public void btnSearch_Click( object sender, EventArgs e ) {
            if ( string.IsNullOrEmpty( txtSearch.Text ) ) return;

            var listenedShowService = new ListenedShowService( Ioc.GetInstance<IListenedShowRepository>() );
            var userId = new Guid( membershipProvider.GetUser( User.Identity.Name ).ProviderUserKey.ToString() );

            var listenedShows = listenedShowService.GetByUser( userId ).ToList();

            if ( listenedShows == null || listenedShows.Count <= 0 ) return;

            var filtered = listenedShows.Where( x => x.Notes.Contains( txtSearch.Text ) );

            rptNotes.DataSource = filtered;
            rptNotes.DataBind();
        }

        public string GetUrl( Guid id ) {
            return "Notes.aspx?showId=" + id.ToString();
        }
    }
}