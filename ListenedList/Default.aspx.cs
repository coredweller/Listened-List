using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;
using ListenedList.Controls;
using Core.Membership;

namespace ListenedList
{
    public partial class _Default : ListenedBasePage
    {
        IMembershipProvider membershipProvider = new ListenedMembershipProvider();

        protected void Page_Load( object sender, EventArgs e ) {

            if ( string.IsNullOrEmpty( hdnUserId.Value ) ) {
                var userId = new Guid( membershipProvider.GetUser( User.Identity.Name ).ProviderUserKey.ToString() );
                hdnUserId.Value = userId.ToString();
            }
        }

        protected override void OnInit( EventArgs e ) {
            
            base.OnInit( e );
            

        }

        private void LoadShows() {
            YearBoxes yearBox = new YearBoxes( );
            yearBox.Year = 1997;

            TextBox tb = new TextBox();
            tb.BackColor = System.Drawing.Color.White;
            tb.Width = new Unit( 65, UnitType.Pixel );

            //phMain.Controls.Add( yearBox );
        }
    }
}
