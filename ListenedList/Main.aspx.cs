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
using Core.Services.Interfaces;
using Microsoft.AspNet.FriendlyUrls;
using System.Web.Services;

namespace ListenedList
{
    public partial class Main : ListenedBasePage
    {
        protected float ButtonSize { get; set; }
        protected float FontSize { get; set; }
        protected string UserName { get; set; }

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

                if ( segmentCount > 1 && int.TryParse( urlSegments[1], out year ) ) {

                    if ( segmentCount > 2 ) {
                        //turn off all
                        yearBox12.Off = true;
                        yearBox11.Off = true;
                        yearBox10.Off = true;
                        yearBox09.Off = true;
                        yearBox04.Off = true;
                        yearBox03.Off = true;
                        yearBox00.Off = true;
                        yearBox99.Off = true;
                        yearBox98.Off = true;
                        yearBox97.Off = true;
                        yearBox96.Off = true;
                        yearBox95.Off = true;
                        yearBox94.Off = true;
                        yearBox93.Off = true;
                        yearBox92.Off = true;
                        yearBox91.Off = true;
                        yearBox90.Off = true;
                        yearBox89.Off = true;
                        yearBox88.Off = true;
                        yearBox87.Off = true;
                        phYears.Visible = true;
                    }
                }

                switch ( year ) {
                    //case 2014:
                    //    yearbox14.MonthMode = true;
                    //    break;
                    //case 2015:
                    //    yearbox15.MonthMode = true;
                    //    break;
                    //case 2013:
                    //    yearbox13.MonthMode = true;
                    //    break;
                    case 2012:
                        yearBox12.Off = false;
                        yearBox12.MonthMode = true;
                        break;
                    case 2011:
                        yearBox11.Off = false;
                        yearBox11.MonthMode = true;
                        break;
                    case 2010:
                        yearBox10.Off = false;
                        yearBox10.MonthMode = true;
                        break;
                    case 2009:
                        yearBox09.Off = false;
                        yearBox09.MonthMode = true;
                        break;
                    case 2004:
                        yearBox04.Off = false;
                        yearBox04.MonthMode = true;
                        break;
                    case 2003:
                        yearBox03.Off = false;
                        yearBox03.MonthMode = true;
                        break;
                    case 2000:
                        yearBox00.Off = false;
                        yearBox00.MonthMode = true;
                        break;
                    case 1999:
                        yearBox99.Off = false;
                        yearBox99.MonthMode = true;
                        break;
                    case 1998:
                        yearBox98.Off = false;
                        yearBox98.MonthMode = true;
                        break;
                    case 1997:
                        yearBox97.Off = false;
                        yearBox97.MonthMode = true;
                        break;
                    case 1996:
                        yearBox96.Off = false;
                        yearBox96.MonthMode = true;
                        break;
                    case 1995:
                        yearBox95.Off = false;
                        yearBox95.MonthMode = true;
                        break;
                    case 1994:
                        yearBox94.Off = false;
                        yearBox94.MonthMode = true;
                        break;
                    case 1993:
                        yearBox93.Off = false;
                        yearBox93.MonthMode = true;
                        break;
                    case 1992:
                        yearBox92.Off = false;
                        yearBox92.MonthMode = true;
                        break;
                    case 1991:
                        yearBox91.Off = false;
                        yearBox91.MonthMode = true;
                        break;
                    case 1990:
                        yearBox90.Off = false;
                        yearBox90.MonthMode = true;
                        break;
                    case 1989:
                        yearBox89.Off = false;
                        yearBox89.MonthMode = true;
                        break;
                    case 1988:
                        yearBox88.Off = false;
                        yearBox88.MonthMode = true;
                        break;
                    case 1987:
                        yearBox87.Off = false;
                        yearBox87.MonthMode = true;
                        break;
                }
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

        private void ResetPanels() {
            phPrivate.Visible = false;
        }

        protected override void OnInit( EventArgs e ) {
            base.OnInit( e );
        }

    }
}
