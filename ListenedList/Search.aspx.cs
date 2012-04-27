using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Services;
using System.Web.Profile;
using Core.Membership;
using Core.Infrastructure;
using Core.Services.Interfaces;
using System.Web.Security;
using Core.Helpers;

namespace ListenedList
{
    public partial class Search : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }
        }

        public void btnReset_Click( object sender, EventArgs e ) {
            lblResultsType.Text = "15 Most Recently Changed Guides";
            Bind();
        }

        private void Bind() {
            ResetPanels();

            //Gets all the public UserProfiles except for the person logged in
            var profiles = ProfileService.GetPublicProfiles( User.Identity.Name );
            //Gets all MembershipUsers
            var users = _MembershipProvider.GetAllUsers();

            //Gets all the public MembershipUsers
            var publicUsers = ( from p in profiles
                                join MembershipUser u in users on p.UserName equals u.UserName into temp
                                from t in temp.DefaultIfEmpty()
                                select t ).ToList();

            var listenedShowService = Ioc.GetInstance<IListenedShowService>();

            var userIds = publicUsers.Select( x => new Guid( x.ProviderUserKey.ToString() ) ).ToList();

            //Gets the 15 latest updated ListenedShows by a list of user Ids
            var latestlistened = listenedShowService.GetByUserIds( userIds ).ToList().Take( 15 ).ToList();
            var showService = Ioc.GetInstance<IShowService>();

            var latestProfiles = ( from l in latestlistened
                                   from u in publicUsers
                                   //from p in profiles
                                   //from s in showService.GetAllShows()
                                   where l.UserId == new Guid( u.ProviderUserKey.ToString() ) //&& u.UserName == p.UserName //&& l.ShowId == s.Id
                                   select new LatestProfile( l, null, null ) ).ToList();

            rptResults.DataSource = latestProfiles;
            rptResults.DataBind();
        }

        public void btnSearchUser_Click( object sender, EventArgs e ) {
            lblResultsType.Text = "User Name Search";
            phReset.Visible = true;

            var list = new List<LatestProfile>();
            var profiles = ProfileService.GetProfilesLikeUserName( txtUserName.Text );

            var listenedShowService = Ioc.GetInstance<IListenedShowService>();
            foreach ( var profile in profiles ) {
                var latest = listenedShowService.GetLatestByUserId( GetUserId( profile.UserName ) );

                if ( latest != null ) {
                    var showService = Ioc.GetInstance<IShowService>();
                    var show = showService.GetShow( latest.ShowId );

                    list.Add( new LatestProfile( latest, show, profile ) );
                }
            }

            rptResults.DataSource = profiles;
            rptResults.DataBind();
        }

        private void ResetPanels() {
            phReset.Visible = false;
        }
    }
}