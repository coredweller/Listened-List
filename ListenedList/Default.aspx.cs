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

namespace ListenedList
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e ) {
            LoadShows();
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
