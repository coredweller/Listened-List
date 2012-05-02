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
using Core.Helpers.Script;

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

            //Set search mode off if they are looking at the default or reset to default
            hdnSearchMode.Value = "false";

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
            var subscriptionService = Ioc.GetInstance<ISubscriptionService>();

            //Matches membershipUsers by user id to listened show user ids, 
            // user profile user names to membershipUsers user names, 
            //  and listened show show ids to all show ids 
            var latestProfiles = ( from l in latestlistened
                                   from u in publicUsers
                                   from p in profiles
                                   from s in showService.GetAllShows()
                                   join sub in subscriptionService.GetSubscriptionsByUser( GetUserId() ) on l.UserId equals sub.SubscribedUserId into temp
                                   from t in temp.DefaultIfEmpty()
                                   where l.UserId == new Guid( u.ProviderUserKey.ToString() ) && u.UserName == p.UserName && l.ShowId == s.Id
                                   select new LatestProfile( l, s, p, t ) ).ToList();

            rptResults.DataSource = latestProfiles;
            rptResults.DataBind();
        }

        public void rptResults_ItemCommand( object source, RepeaterCommandEventArgs e ) {
            switch ( e.CommandName.ToLower() ) {
                case "subscribe":
                    Subscribe( e.CommandArgument.ToString() );
                    break;
            }
        }

        private void Subscribe( string userName ) {
            var user = _MembershipProvider.GetUser( userName );

            if ( user == null ) return;

            var subscribedUserId = new Guid( user.ProviderUserKey.ToString() );
            var userId = GetUserId();

            var subscription = _DomainObjectFactory.CreateSubscription( userId, subscribedUserId );

            var subscriptionService = Ioc.GetInstance<ISubscriptionService>();

            var success = false;
            subscriptionService.SaveCommit( subscription, out success );

            PromptHelper prompt;
            if ( success ) {
                if ( hdnSearchMode.Value == "true" ) {
                    SearchUser( true );
                }
                else {
                    Bind();
                }

                prompt = new PromptHelper( "You have successfully subscribed to that user's guide" );
                Page.RegisterStartupScript( prompt.ScriptName, prompt.GetSuccessScript() );
            }
            else {
                prompt = new PromptHelper( "There was an error subscribing to that user.  Please try again later." );
                Page.RegisterStartupScript( prompt.ScriptName, prompt.GetErrorScript() );
            }
        }

        public void btnSearchUser_Click( object sender, EventArgs e ) {
            SearchUser( false );
        }

        private void SearchUser( bool fromSubscribe ) {
            var list = new List<LatestProfile>();

            //If there is no text then do not continue
            if ( string.IsNullOrEmpty( txtUserName.Text ) ) {

                //If it came from Subscribe then just reset to the default
                if ( fromSubscribe ) {
                    Bind();
                }
                //If it did NOT come from Subscribe then tell them to enter text to search on.
                else {
                    var prompt = new PromptHelper( "Please enter the user name or part of the user name in order to search." );
                    Page.RegisterStartupScript( prompt.ScriptName, prompt.GetErrorScript() );
                }

                return;
            }

            lblResultsType.Text = "User Name Search";
            phReset.Visible = true;

            //Set search mode to true so that you know the user was searching on their last view
            hdnSearchMode.Value = "true";

            var profiles = ProfileService.GetProfilesLikeUserName( txtUserName.Text );

            var listenedShowService = Ioc.GetInstance<IListenedShowService>();
            var subscriptionService = Ioc.GetInstance<ISubscriptionService>();

            var subscriptions = subscriptionService.GetSubscriptionsByUser( GetUserId() );
            foreach ( var profile in profiles ) {
                var userId = GetUserId( profile.UserName );
                var latest = listenedShowService.GetLatestByUserId( userId );

                if ( latest != null ) {
                    var showService = Ioc.GetInstance<IShowService>();
                    var show = showService.GetShow( latest.ShowId );
                    var sub = subscriptions.SingleOrDefault( x => x.SubscribedUserId == latest.UserId );

                    list.Add( new LatestProfile( latest, show, profile, sub ) );
                }
            }

            rptResults.DataSource = list;
            rptResults.DataBind();
        }

        private void ResetPanels() {
            phReset.Visible = false;
        }
    }
}