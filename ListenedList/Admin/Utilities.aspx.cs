using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using Core.Services;

namespace ListenedList.Admin
{
    public partial class Utilities : ListenedBasePage
    {
        protected void Page_Load( object sender, EventArgs e ) {
            UserInRoleRedirect( User.Identity.Name, Core.Membership.Roles.ADMINISTRATOR );
        }

        protected void btnResetFontSize_Click( object sender, EventArgs e ) {
            var profiles = ProfileService.GetAllProfiles();
        }
    }
}