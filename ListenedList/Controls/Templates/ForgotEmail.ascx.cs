using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListenedList.Controls.Templates
{
    public partial class ForgotEmail : ListenedBaseControl
    {
        public string UserName { get; set; }

        protected void Page_Load( object sender, EventArgs e ) {
            if ( string.IsNullOrEmpty( UserName ) ) return;

            var user = _MembershipProvider.GetUser( UserName );
            lblUserName.Text = user.UserName;
            lblPassword.Text = user.GetPassword();
        }
    }
}