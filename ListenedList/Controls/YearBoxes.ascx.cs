using System;
using System.Collections.Generic;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;
using Core.Helpers;
using System.Drawing;

namespace ListenedList.Controls
{
    public partial class YearBoxes : System.Web.UI.UserControl
    {
        public int Year { get; set; }

        public List<ShowStatus> Shows { get; set; }

        //public YearBoxes( int year ) {
        //    Year = year;
        //}

        //public YearBoxes() {

        //}

        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }
        }

        protected override void OnInit( EventArgs e ) {
            base.OnInit( e );
            //Bind();
        }

        private void Bind() {
            var showService = new ShowService( Ioc.GetInstance<IShowRepository>() );
            var shows = showService.GetShowStatusByYear( Year );

            if ( Shows != null && Shows.Count > 0) {
                foreach ( var s in Shows ) {
                    var match = shows.Find( x => x.ShowId == s.ShowId );
                    if ( match == null ) continue;

                    var copy = new ShowStatus(match.ShowId, s.Status, match.ShowDate);

                    var index = shows.IndexOf(match);
                    shows.Remove( match );
                    shows.Insert( index, copy );
                    
                }
            }

            rptShows.DataSource = shows;
            rptShows.DataBind();
        }

        public Color GetStatus( int status ) {
            switch ( status ) {
                case (int)ListenedStatus.InProgress:
                    return Color.Yellow;
                case (int)ListenedStatus.Finished:
                    return Color.Blue;
            }

            return Color.White;
        }
    }
}