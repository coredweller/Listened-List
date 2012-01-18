using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;

namespace ListenedList.Controls
{
    public partial class YearBoxes : System.Web.UI.UserControl
    {
        public int Year { get; set; }

        //public YearBoxes( int year ) {
        //    Year = year;
        //}

        //public YearBoxes() {

        //}

        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }
        }

        protected override void OnInit( EventArgs e ) {
            base.OnInit( e );
            //Bind();
        }

        private void Bind() {
            var showService = new ShowService( Ioc.GetInstance<IShowRepository>() );
            var shows = showService.GetShowsByYear( Year );

            rptShows.DataSource = shows;
            rptShows.DataBind();
        }
       
    }
}