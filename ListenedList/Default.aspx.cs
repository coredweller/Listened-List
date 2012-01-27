using System;
using System.Linq;
using System.Web.UI.WebControls;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;
using ListenedList.Controls;
using Core.DomainObjects;
using System.Collections.Generic;
using Core.Helpers;

namespace ListenedList
{
    public partial class _Default : ListenedBasePage
    {
        

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

            yearBox00.Shows = shows;
            yearBox03.Shows = shows;
            yearBox04.Shows = shows;
            yearBox09.Shows = shows;
            yearBox10.Shows = shows;
            yearBox11.Shows = shows;
            yearBox83.Shows = shows;
            yearBox84.Shows = shows;
            yearBox85.Shows = shows;
            yearBox86.Shows = shows;
            yearBox87.Shows = shows;
            yearBox88.Shows = shows;
            yearBox89.Shows = shows;
            yearBox90.Shows = shows;
            yearBox91.Shows = shows;
            yearBox92.Shows = shows;
            yearBox93.Shows = shows;
            yearBox94.Shows = shows;
            yearBox95.Shows = shows;
            yearBox96.Shows = shows;
            yearBox97.Shows = shows;
            yearBox98.Shows = shows;
            yearBox99.Shows = shows;
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
