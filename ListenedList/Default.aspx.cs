using System;
using System.Linq;
using System.Web.UI.WebControls;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;
using ListenedList.Controls;
using Core.Membership;
using Core.DomainObjects;
using System.Collections.Generic;
using Core.Helpers;

namespace ListenedList
{
    public partial class _Default : ListenedBasePage
    {
        IMembershipProvider membershipProvider = new ListenedMembershipProvider();

        protected void Page_Load( object sender, EventArgs e ) {

            if ( string.IsNullOrEmpty( hdnUserId.Value ) ) {
                var userId = new Guid( membershipProvider.GetUser( User.Identity.Name ).ProviderUserKey.ToString() );
                hdnUserId.Value = userId.ToString();
            }

            if ( !IsPostBack ) {
                Bind(hdnUserId.Value);
            }
        }

        private void Bind(string userIdStr) {
            var userId = new Guid( userIdStr );

            var listenedShowService = new ListenedShowService( Ioc.GetInstance<IListenedShowRepository>() );
            var listenedShows = listenedShowService.GetByUser( userId );

            List<ShowStatus> shows = new List<ShowStatus>();
            foreach ( var show in listenedShows.ToList() ) {
                shows.Add( new ShowStatus( show.ShowId, show.Status ) );
            }

            yearBox84.Shows = shows;
        }

        protected override void OnInit( EventArgs e ) {
            
            base.OnInit( e );
            

        }

        private void LoadShows() {
            YearBoxes yearBox = new YearBoxes( );
            yearBox.Year = 1997;

            TextBox tb = new TextBox();
            tb.BackColor = System.Drawing.Color.White;
            tb.Width = new Unit( 65, UnitType.Pixel );

            //phMain.Controls.Add( yearBox );
        }
    }
}
