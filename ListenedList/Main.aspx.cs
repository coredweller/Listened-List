using System;
using System.Linq;
using Core.Services;
using Core.Infrastructure;
using System.Collections.Generic;
using Core.Helpers;
using Core.Services.Interfaces;
using Microsoft.AspNet.FriendlyUrls;
using ListenedList.Controls;
using System.Web.UI;

namespace ListenedList
{
    public partial class Main : ListenedBasePage
    {
        protected float ButtonSize { get; set; }
        protected float FontSize { get; set; }
        protected string UserName { get; set; }

        private const string YearBoxBaseId = "yearBox";

        protected void Page_Load( object sender, EventArgs e ) {

            if ( string.IsNullOrEmpty( hdnUserId.Value ) ) {
                hdnUserId.Value = GetUserId().ToString();
            }

            if ( !IsPostBack ) {
                Bind();
            }
        }

        private void Bind() {
            ResetPanels();

            Guid userId;

            UserName = User.Identity.Name;
            userId = GetUserId();
            var urlSegments = Request.GetFriendlyUrlSegments();

            var segment = urlSegments.FirstOrDefault();

            if ( segment != null && segment != "Year" ) {
                var user = _MembershipProvider.GetUser( segment );

                if ( user == null ) return;

                var otherUserProfileService = new ProfileService( user.UserName );
                var userProfile = otherUserProfileService.GetUserProfile();

                if ( !userProfile.Public.HasValue || ( userProfile.Public.HasValue && userProfile.Public.Value == false ) ) {
                    phPrivate.Visible = true;
                    return;
                }

                userId = new Guid( user.ProviderUserKey.ToString() );
            }
            else if ( segment != null && segment == "Year" ) {
                int year = 0;
                var segmentCount = urlSegments.Count();

                //Parse Year out
                if ( segmentCount > 1 && int.TryParse( urlSegments[1], out year ) ) {

                    //If "only" is in the url then turn all yearboxes off
                    if ( segmentCount > 2 ) {

                        var loopYear = LOWEST_YEAR;
                        while ( loopYear <= DateTime.Now.Year ) {
                            var yearStr = GetLastTwoDigits(loopYear);
                            var yearBoxId = YearBoxBaseId + yearStr;
                            var foundBox = FindControlRecursive( divAllYearBoxes, yearBoxId );

                            if ( foundBox != null ) {
                                YearBoxes boxToTurnOff = (YearBoxes)foundBox;
                                boxToTurnOff.Off = true;
                            }

                            loopYear++;
                        }

                        phYears.Visible = true;
                    }
                }

                var lastTwoDigits = GetLastTwoDigits( year );
                var yearBoxIdToFind = YearBoxBaseId + lastTwoDigits;

                var box = FindControlRecursive( divAllYearBoxes, yearBoxIdToFind );

                //Turns the chosen year box on and into month mode
                if ( box != null ) {
                    YearBoxes chosenBox = (YearBoxes)box;
                    chosenBox.Off = false;
                    chosenBox.MonthMode = true;
                }

                var spanToMove = "#spanYear" + lastTwoDigits;
                //This script moves the year box up to right under the year list.
                //  This is so they are all even on the page after postback
                Page.RegisterStartupScript( "MoveScript", "<script type=\"text/javascript\">  $('#divUnderYearList').append($('" + spanToMove + "') ); </script>" );
            }

            var profileService = new ProfileService( User.Identity.Name );
            var profile = profileService.GetUserProfile();
            ButtonSize = profile.ButtonSize.HasValue ? profile.ButtonSize.Value : DefaultButtonSize;
            FontSize = profile.FontSize.HasValue ? profile.FontSize.Value : DefaultFontSize;

            var listenedShowService = Ioc.GetInstance<IListenedShowService>();
            var listenedShows = listenedShowService.GetByUser( userId );

            List<ShowStatus> shows = new List<ShowStatus>();
            foreach ( var show in listenedShows.ToList() ) {
                shows.Add( new ShowStatus( show.ShowId, show.Status, show.Attended ) );
            }

            yearBox00.Shows = shows;
            yearBox03.Shows = shows;
            yearBox04.Shows = shows;
            yearBox09.Shows = shows;
            yearBox10.Shows = shows;
            yearBox11.Shows = shows;
            yearBox12.Shows = shows;
            yearBox13.Shows = shows;
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

        private string GetLastTwoDigits( int number ) {
            return ( number % 100 ).ToString().PadLeft( 2, '0' );
        }

        private void ResetPanels() {
            phPrivate.Visible = false;
        }

        protected override void OnInit( EventArgs e ) {
            base.OnInit( e );
        }

    }
}
