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

        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }
        }

        private void Bind() {
            var years = showService.GetYears().Where( x => x > 1984 ); ;

            foreach ( var year in years ) {
                var yearStr = year.ToString();
                ddlYears.Items.Add( new ListItem( yearStr, yearStr ) );
            }

            var item = new ListItem( "Choose a Year", "0" );
            ddlYears.Items.Insert( 0, item );
        }

        public void SaveAll( object sender, EventArgs e ) {

            if ( rptAdder.Items == null || rptAdder.Items.Count <= 0 ) return;

            var success = false;

            try {

                var item = rptAdder.Items[0];

                RadioButton btn = item.FindControl( finished ) as RadioButton;

                var showDate = DateTime.Parse( btn.ToolTip );
                var userId = GetUserId();
                var listenedShows = listenedShowService.GetShowsByYear( showDate.Year, userId );

                using ( IUnitOfWork uow = UnitOfWork.Begin() ) {
                    foreach ( RepeaterItem i in rptAdder.Items ) {
                        if ( item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem ) {
                            RadioButton finishedChk = i.FindControl( finished ) as RadioButton;
                            RadioButton inProgressChk = i.FindControl( inProgress ) as RadioButton;
                            RadioButton needToListenChk = i.FindControl( needToListen ) as RadioButton;
                            RadioButton neverHeardChk = i.FindControl( neverHeard ) as RadioButton;

                            var date = DateTime.Parse( finishedChk.ToolTip );

                            var newStatus = ListenedStatus.None;
                            if ( finishedChk.Checked )
                                newStatus = ListenedStatus.Finished;
                            else if ( inProgressChk.Checked )
                                newStatus = ListenedStatus.InProgress;
                            else if ( needToListenChk.Checked )
                                newStatus = ListenedStatus.NeedToListen;

                            var listened = listenedShows.SingleOrDefault( x => x.ShowDate == date );

                            if ( listened == null && newStatus == ListenedStatus.None ) continue;
                            else if(listened == null) {
                                var show = showService.GetShow(date);
                                var newListened = _DomainObjectFactory.CreateListenedShow( show.Id, userId, date, (int)newStatus, string.Empty );
                                bool minorSuccess;
                                listenedShowService.Save( newListened, out minorSuccess );

                                if ( !minorSuccess ) _Log.Write( "There was an error saving a listenedShow status for userId: " + userId + " and showId: " + show.Id + " and listenedShowId: " + newListened != null ? newListened.Id.ToString() : "new listened show was null" );
                                continue;
                            }

                            if ( listened.Status == (int)newStatus ) continue;

                            listened.Status = (int)newStatus;
                        }
                    }
                    uow.Commit();
                    success = true;
                }
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "There was an error saving all shows on AddShows.aspx in the SaveAll method with message: " + ex.Message );
            }

            ValidateSuccess( success, "You have successfully saved your shows!", "There was an error saving your shows!" );
        }

        public void btnSubmit_Click( object sender, EventArgs e ) {

            if ( ddlYears.SelectedValue == "0" ) return;

            var year = int.Parse( ddlYears.SelectedValue );

            var listenedShows = listenedShowService.GetBothShowsByYear( year, GetUserId() );

            DisplaySaveButtons( true );

            rptAdder.DataSource = listenedShows;
            rptAdder.DataBind();
        }

        private void DisplaySaveButtons( bool display ) {
            phSaveButton1.Visible = display;
            phSaveButton2.Visible = display;
        }

        protected string GetClass(string currentClass, ListenedStatus buttonStatus, int userStatus ) {
            var newClass = currentClass;

            if ( userStatus >= 0 && (int)buttonStatus == userStatus ) newClass += " statusSelected";

            return newClass;
        }

        //public void rptAdder_ItemCreated( object sender, RepeaterItemEventArgs e ) {
        //    RepeaterItem item = (RepeaterItem)e.Item;

        //    if ( item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem ) {
        //        RadioButton finishedChk = item.FindControl( finished ) as RadioButton;
        //        RadioButton inProgressChk = item.FindControl( inProgress ) as RadioButton;
        //        RadioButton needToListenChk = item.FindControl( needToListen ) as RadioButton;
        //        RadioButton neverHeardChk = item.FindControl( neverHeard ) as RadioButton;

        //        finishedChk.CheckedChanged += new EventHandler( CheckedChanged );
        //        inProgressChk.CheckedChanged += new EventHandler( CheckedChanged );
        //        needToListenChk.CheckedChanged += new EventHandler( CheckedChanged );
        //        neverHeardChk.CheckedChanged += new EventHandler( CheckedChanged );
        //    }
        //}

        //private void CheckedChanged( object sender, EventArgs e ) {
        //    RadioButton rb = (RadioButton)sender;
        //    var showDate = DateTime.Parse( rb.ToolTip );
        //    int status = 0;

        //    switch ( rb.ID ) {
        //        case finished:
        //            status = (int)ListenedStatus.Finished;
        //            break;
        //        case inProgress:
        //            status = (int)ListenedStatus.InProgress;
        //            break;
        //        case needToListen:
        //            status = (int)ListenedStatus.NeedToListen;
        //            break;
        //        case neverHeard:
        //            status = (int)ListenedStatus.None;
        //            break;
        //    }

        //    if ( rb.Checked ) {
        //        var show = showService.GetShow( showDate );

        //        var userId = GetUserId();
        //        var listenedShow = listenedShowService.GetByUserAndShowId( userId, show.Id );

        //        if ( listenedShow == null ) {
        //            var l = _DomainObjectFactory.CreateListenedShow( show.Id, userId, showDate, status, string.Empty );

        //            bool success = false;
        //            listenedShowService.SaveCommit( l, out success );
        //        }
        //        else {
        //            using ( IUnitOfWork uow = UnitOfWork.Begin() ) {

        //                listenedShow.Status = status;
        //                uow.Commit();
        //            }
        //        }
        //    }
        //}
    }
}