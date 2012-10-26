using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Infrastructure;
using Core.Configuration;

namespace ListenedList.Controls.Templates
{
    public partial class WelcomeEmail : ListenedBaseControl
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        protected void Page_Load( object sender, EventArgs e ) {
            if ( string.IsNullOrEmpty( UserName ) || string.IsNullOrEmpty(Password) ) return;

            var configManager = Ioc.GetInstance<IAppConfigManager>();

            lblUserName.Text = UserName;
            lblPassword.Text = Password;
            lblHelpEmail.Text = configManager.AppSettings["HelpEmail"];
        }
    }
}