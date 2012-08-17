using System;
using System.Collections.Generic;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;
using Core.Helpers;
using System.Drawing;
using Core.Helpers;
using Core.Services.Interfaces;

namespace ListenedList.Controls
{
    public partial class YearBoxes : System.Web.UI.UserControl
    {
        public int Year { get; set; }
        public bool Tutorial { get; set; }
        public List<ShowStatus> Shows { get; set; }

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
            var showService = Ioc.GetInstance<IShowService>();
            var shows = showService.GetShowStatusByYear( Year );

            //Shows is populated in Default.aspx.cs with all the ShowStatus's for the ListenedShows
            if ( Shows != null && Shows.Count > 0) {
                
                foreach ( var s in Shows ) {
                    //If the user has a listened status set for this show then its a match
                    var match = shows.Find( x => x.ShowId == s.ShowId );
                    if ( match == null ) continue;

                    //The new ShowStatus to use for binding
                    var copy = new ShowStatus(match.ShowId, s.Status, match.ShowDate, match.ShowName, s.Attended);
                    
                    //Put the new ShowStatus where the old one was because order is important
                    var index = shows.IndexOf(match);
                    //Remove the old one
                    shows.Remove( match );
                    //Insert the new one where the old one was
                    shows.Insert( index, copy );
                }
            }

            if ( Tutorial && ( shows != null && shows.Count >= 0 ) ) shows = shows.GetRange( 0, 5 );

            rptShows.DataSource = shows;
            rptShows.DataBind();
        }

        public string GetCssClass( int status, bool attended ) {
            string cssClass = string.Empty;

            switch ( status ) {
                case (int)ListenedStatus.InProgress:
                    cssClass = "defaultButtonYellow";
                    break;
                case (int)ListenedStatus.Finished:
                    cssClass = "defaultButtonOrange";
                    break;
                case (int)ListenedStatus.NeedToListen:
                    cssClass = "defaultButtonGreen";
                    break;
                default:
                    cssClass = "defaultButtonWhite";
                        break;
            }

            if ( attended ) {
                cssClass += " attendedButton";
            }

            return cssClass;
        }

        public Color GetStatus( int status ) {
            switch ( status ) {
                case (int)ListenedStatus.InProgress:
                    return Color.Yellow;
                case (int)ListenedStatus.Finished:
                    return Color.Orange;
                case (int)ListenedStatus.NeedToListen:
                    return Color.GreenYellow;
            }

            return Color.White;
        }
    }
}