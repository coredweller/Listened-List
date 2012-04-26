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

            var profiles = ProfileService.GetPublicProfiles( User.Identity.Name );
            var users = _MembershipProvider.GetAllUsers();

            var publicUsers = ( from p in profiles
                                join MembershipUser u in users on p.UserName equals u.UserName into temp
                                from t in temp.DefaultIfEmpty()
                                select new Guid(t.ProviderUserKey.ToString()) ).ToList();

            var listenedShowService = Ioc.GetInstance<IListenedShowService>();
            listenedShowService.GetByUserIds( publicUsers );
            
            rptResults.DataSource = profiles;
            rptResults.DataBind();
        }

        public void btnSearchUser_Click( object sender, EventArgs e ) {
            lblResultsType.Text = "User Name Search";

            var profiles = ProfileService.GetProfilesLikeUserName( txtUserName.Text );


            rptResults.DataSource = profiles;
            rptResults.DataBind();
        }
    }
}