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
using Core.DomainObjects;

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

            phNoSaveMessage.Visible = true;

            rptAdder.DataSource = listenedShows;
            rptAdder.DataBind();
        }

        protected void rptAdder_ItemCommand( object source, RepeaterCommandEventArgs e ) {
            var showId = new Guid( e.CommandName.ToString() );
            var newStatus = (ListenedStatus)int.Parse( e.CommandArgument.ToString() );
            var button = (Button)e.CommandSource;
            var repeaterItem = (RepeaterItem)button.NamingContainer;
            var showDate = DateTime.Parse( button.ToolTip );
            var userId = GetUserId();
            var currentlyAttended = button.CssClass == "notesAttended" ? true : false;
            //reset means that the status is the same so we are setting it back to Never Heard
            bool reset = false;

            var success = false;

            using ( IUnitOfWork uow = UnitOfWork.Begin() ) {
                var listened = listenedShowService.GetByUserAndShow( userId, showDate );

                if ( listened == null ) {

                    IListenedShow newListened;
                    if ( button.ID != "btnAttended" )
                        newListened = _DomainObjectFactory.CreateListenedShow( showId, userId, showDate, (int)newStatus, string.Empty );
                    else
                        newListened = _DomainObjectFactory.CreateListenedShow( showId, userId, showDate, (int)ListenedStatus.None, string.Empty, !currentlyAttended );

                    bool minorSuccess;
                    listenedShowService.Save( newListened, out minorSuccess );

                    if ( !minorSuccess ) _Log.Write( "There was an error saving a listenedShow status for userId: " + userId + " and showId: " + showId + " and listenedShowId: " + newListened != null ? newListened.Id.ToString() : "new listened show was null" );
                }
                else {
                    if ( button.ID != "btnAttended" ) {
                        //If the current status is the same as the new status then unselect it and set it back to Never Heard
                        if ( listened.Status == (int)newStatus ) {
                            listened.Status = (int)ListenedStatus.None;
                            reset = true;
                        }
                        else
                            listened.Status = (int)newStatus;
                    }
                    else {
                        listened.Attended = !currentlyAttended;
                    }
                }

                uow.Commit();
                success = true;
            }

            if ( !success ) return;

            if ( button.ID != "btnAttended" ) {
                //Remove the statusSelected class from all and then add it to the newly selected button
                RemoveClass( repeaterItem, "btnNeverHeard" );
                RemoveClass( repeaterItem, "btnInProgress" );
                RemoveClass( repeaterItem, "btnFinished" );
                RemoveClass( repeaterItem, "btnNeedToListen" );

                if ( !reset ) {
                    button.CssClass += " " + statusSelected;
                }
                else {
                    var neverHeardButton = ( (Button)repeaterItem.FindControl( "btnNeverHeard" ) );
                    neverHeardButton.CssClass += " " + statusSelected;
                }
            }
            else {
                //If the button status was clicked then switch the class to the other one
                button.CssClass = currentlyAttended ? "notesDidNotAttend" : "notesAttended";
                button.Text = currentlyAttended ? "Did Not Attend" : "Attended";
            }
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