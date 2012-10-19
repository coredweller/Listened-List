using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Infrastructure;
using Core.Services.Interfaces;
using System.Web.Security;
using Core.Services;

namespace ListenedList
{
    public partial class AddShows : ListenedBasePage
    {
        IShowService showService = Ioc.GetInstance<IShowService>();
        IListenedShowService listenedShowService = Ioc.GetInstance<IListenedShowService>();
        private const string finished = "rdoFinished";
        private const string inProgress = "rdoInProgress";
        private const string needToListen = "rdoNeedToListen";
        private const string neverHeard = "rdoNeverHeard";
        private const string statusSelected = "statusSelected";

        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }
        }

        private void Bind() {
            var years = showService.GetYears().Where( x => x > 1986 ); ;

            foreach ( var year in years ) {
                var yearStr = year.ToString();
                ddlYears.Items.Add( new ListItem( yearStr, yearStr ) );
            }

            var item = new ListItem( "Choose a Year", "0" );
            ddlYears.Items.Insert( 0, item );
        }

        public void btnSubmit_Click( object sender, EventArgs e ) {

            if ( ddlYears.SelectedValue == "0" ) return;

            var year = int.Parse( ddlYears.SelectedValue );

            var listenedShows = listenedShowService.GetBothShowsByYear( year, GetUserId() );

            rptAdder.DataSource = listenedShows;
            rptAdder.DataBind();
        }

        protected void rptAdder_ItemCommand( object source, RepeaterCommandEventArgs e ) {
            var showId = new Guid( e.CommandName.ToString() );
            var newStatus = (ListenedStatus)int.Parse( e.CommandArgument.ToString() );
            var button = (Button)e.CommandSource;
            var showDate = DateTime.Parse( button.ToolTip );
            var userId = GetUserId();

            var success = false;

            using ( IUnitOfWork uow = UnitOfWork.Begin() ) {
                var listened = listenedShowService.GetByUserAndShow( userId, showDate );

                if ( listened == null ) {
                    var newListened = _DomainObjectFactory.CreateListenedShow( showId, userId, showDate, (int)newStatus, string.Empty );
                    bool minorSuccess;
                    listenedShowService.Save( newListened, out minorSuccess );

                    if ( !minorSuccess ) _Log.Write( "There was an error saving a listenedShow status for userId: " + userId + " and showId: " + showId + " and listenedShowId: " + newListened != null ? newListened.Id.ToString() : "new listened show was null" );
                }
                else {
                    //If the current status is the same as the new status then do nothing
                    if ( listened.Status == (int)newStatus ) return;

                    listened.Status = (int)newStatus;
                }

                uow.Commit();
                success = true;
            }

            if ( !success ) return;

            //Remove the statusSelected class from all and then add it to the newly selected button
            var repeaterItem = (RepeaterItem)button.NamingContainer;
            RemoveClass( repeaterItem, "btnNeverHeard" );
            RemoveClass( repeaterItem, "btnInProgress" );
            RemoveClass( repeaterItem, "btnFinished" );
            RemoveClass( repeaterItem, "btnNeedToListen" );

            button.CssClass += " " + statusSelected;
        }

        private void RemoveClass( RepeaterItem repeaterItem, string buttonName ) {
            var button = ( (Button)repeaterItem.FindControl( buttonName ) );
            button.CssClass = button.CssClass.Replace( statusSelected, "" );
        }

        protected string GetClass( string currentClass, ListenedStatus buttonStatus, int userStatus ) {
            var newClass = currentClass;

            if ( userStatus >= 0 && (int)buttonStatus == userStatus ) newClass += " " + statusSelected;

            return newClass;
        }
    }
}