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
        public string Password { get; set; }

        protected void Page_Load( object sender, EventArgs e ) {
            
        }

        public void SetProperties() {
            if ( string.IsNullOrEmpty( UserName ) || string.IsNullOrEmpty(Password) ) return;

            lblUserName.Text = UserName;
            lblPassword.Text = Password;
        }
    }
}