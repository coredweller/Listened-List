using System;
using System.Collections.Generic;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;
using Core.Helpers;
using System.Drawing;
using Core.Services.Interfaces;
using System.Linq;
using Core.Extensions;

namespace ListenedList.Controls
{
    public partial class YearBoxes : System.Web.UI.UserControl
    {
        //The year that the control will display
        public int Year { get; set; }

        //puts the control into Tutorial mode. It limits the amount of shows to ShowsToDisplay.
        public bool Tutorial { get; set; }
        public int ShowsToDisplay { get; set; }

        //puts the control into Month mode. Shows the shows vertical by month.
        public bool MonthMode { get; set; }

        //Can turn it off
        public bool Off { get; set; }

        public List<ShowStatus> Shows { get; set; }

        private const int _DEFAULT_SHOWS_TO_DISPLAY = 5;

        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }

            if ( MonthMode ) {
                phMonthMode.Visible = true;
                phYearMode.Visible = false;
            }
            else {
                phYearMode.Visible = true;
                phMonthMode.Visible = false;
            }
        }

        protected override void OnInit( EventArgs e ) {
            base.OnInit( e );
            //Bind();
        }

        private void Bind() {
            if ( Off ) return;

            var showService = Ioc.GetInstance<IShowService>();
            List<ShowStatus> shows = showService.GetShowStatusByYear( Year );

            //Shows is populated in Main.aspx with all the ShowStatus's for the ListenedShows
            if ( Shows != null && Shows.Count > 0 ) {

                foreach ( var s in Shows ) {
                    //If the user has a listened status set for this show then its a match
                    var match = shows.Find( x => x.ShowId == s.ShowId );
                    if ( match == null ) continue;

                    //The new ShowStatus to use for binding
                    var copy = new ShowStatus( match.ShowId, s.Status, match.ShowDate, match.ShowName, s.Attended );

                    //Put the new ShowStatus where the old one was because order is important
                    var index = shows.IndexOf( match );
                    //Remove the old one
                    shows.Remove( match );
                    //Insert the new one where the old one was
                    shows.Insert( index, copy );
                }
            }

            if ( MonthMode && ( shows != null && shows.Count >= 0 ) ) {
                SetupMonthMode( shows );
            }

            if ( Tutorial && ( shows != null && shows.Count >= 0 ) ) {
                var displayCount = ShowsToDisplay > 0 ? ShowsToDisplay : _DEFAULT_SHOWS_TO_DISPLAY;
                shows = shows.GetRange( 0, displayCount );
            }

            //Set the one year box
            yearBox.Shows = shows;
            yearBox.Month = Year.ToString();
            yearBox.Tutorial = Tutorial;
        }

        public void SetupMonthMode( IList<ShowStatus> shows ) {
            var janShows = shows.GetShowsByMonth( Month.JANUARY );
            phJan.Visible = ( janShows == null || janShows.Count <= 0 ) ? false : true;
            janBox.Shows = janShows;
            janBox.Month = Month.JANUARY.StringValue();

            var febShows = shows.GetShowsByMonth( Month.FEBRUARY );
            phFeb.Visible = ( febShows == null || febShows.Count <= 0 ) ? false : true;
            febBox.Shows = febShows;
            febBox.Month = Month.FEBRUARY.StringValue();

            var marchShows = shows.GetShowsByMonth( Month.MARCH );
            phMarch.Visible = ( marchShows == null || marchShows.Count <= 0 ) ? false : true;
            marchBox.Shows = marchShows;
            marchBox.Month = Month.MARCH.StringValue();

            var aprilShows = shows.GetShowsByMonth( Month.APRIL );
            phApril.Visible = ( aprilShows == null || aprilShows.Count <= 0 ) ? false : true;
            aprilBox.Shows = aprilShows;
            aprilBox.Month = Month.APRIL.StringValue();

            var mayShows = shows.GetShowsByMonth( Month.MAY );
            phMay.Visible = ( mayShows == null || mayShows.Count <= 0 ) ? false : true;
            mayBox.Shows = mayShows;
            mayBox.Month = Month.MAY.StringValue();

            var juneShows = shows.GetShowsByMonth( Month.JUNE );
            phJune.Visible = ( juneShows == null || juneShows.Count <= 0 ) ? false : true;
            juneBox.Shows = juneShows;
            juneBox.Month = Month.JUNE.StringValue();

            var julyShows = shows.GetShowsByMonth( Month.JULY );
            phJuly.Visible = ( julyShows == null || julyShows.Count <= 0 ) ? false : true;
            julyBox.Shows = julyShows;
            julyBox.Month = Month.JULY.StringValue();

            var augustShows = shows.GetShowsByMonth( Month.AUGUST );
            phAug.Visible = ( augustShows == null || augustShows.Count <= 0 ) ? false : true;
            augBox.Shows = augustShows;
            augBox.Month = Month.AUGUST.StringValue();

            var septShows = shows.GetShowsByMonth( Month.SEPTEMBER );
            phSept.Visible = ( septShows == null || septShows.Count <= 0 ) ? false : true;
            septBox.Shows = septShows;
            septBox.Month = Month.SEPTEMBER.StringValue();

            var octShows = shows.GetShowsByMonth( Month.OCTOBER );
            phOct.Visible = ( octShows == null || octShows.Count <= 0 ) ? false : true;
            octBox.Shows = octShows;
            octBox.Month = Month.OCTOBER.StringValue();

            var novShows = shows.GetShowsByMonth( Month.NOVEMBER );
            phNov.Visible = ( novShows == null || novShows.Count <= 0 ) ? false : true;
            novBox.Shows = novShows;
            novBox.Month = Month.NOVEMBER.StringValue();

            var decShows = shows.GetShowsByMonth( Month.DECEMBER );
            phDec.Visible = ( decShows == null || decShows.Count <= 0 ) ? false : true;
            decBox.Shows = decShows;
            decBox.Month = Month.DECEMBER.StringValue();
        }

        public Color GetStatus( int status ) {
            switch ( status ) {
                case (int)ListenedStatus.InProgress:
                    return Color.Yellow;
                case (int)ListenedStatus.Finished:
                    return Color.Orange;
                case (int)ListenedStatus.NeedToListen:
                    return Color.GreenYellow;
            }

            return Color.White;
        }
    }
}