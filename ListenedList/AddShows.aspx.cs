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
        private const string finished = "chkFinished";
        private const string inProgress = "chkInProgress";
        private const string needToListen = "chkNeedToListen";

        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }
        }

        private void Bind() {
            var years = showService.GetYears();

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

            var listenedShows = listenedShowService.GetShowsByYear( year, GetUserId() );

            rptAdder.DataSource = listenedShows;
            rptAdder.DataBind();
        }

        public void rptAdder_ItemCreated( object sender, RepeaterItemEventArgs e ) {
            RepeaterItem item = (RepeaterItem)e.Item;

            if ( item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem ) {
                CheckBox finishedChk = item.FindControl( finished ) as CheckBox;
                CheckBox inProgressChk = item.FindControl( inProgress ) as CheckBox;
                CheckBox needToListenChk = item.FindControl( needToListen ) as CheckBox;

                finishedChk.CheckedChanged += new EventHandler( CheckedChanged );
                inProgressChk.CheckedChanged += new EventHandler( CheckedChanged );
                needToListenChk.CheckedChanged += new EventHandler( CheckedChanged );
            }
        }

        private void CheckedChanged( object sender, EventArgs e ) {
            CheckBox cb = (CheckBox)sender;
            var showDate = DateTime.Parse( cb.ToolTip );
            int status = 0;

            switch ( cb.ID ) {
                case finished:
                    status = (int)ListenedStatus.Finished;
                    break;
                case inProgress:
                    status = (int)ListenedStatus.InProgress;
                    break;
                case needToListen:
                    status = (int)ListenedStatus.NeedToListen;
                    break;
            }

            if ( cb.Checked ) {
                var show = showService.GetShow( showDate );

                var userId = GetUserId();
                var listenedShow = listenedShowService.GetByUserAndShowId( userId, show.Id );

                if ( listenedShow == null ) {
                    var l = _DomainObjectFactory.CreateListenedShow( show.Id, userId, showDate, status, string.Empty );

                    bool success = false;
                    listenedShowService.SaveCommit( l, out success );
                }
                else {
                    using ( IUnitOfWork uow = UnitOfWork.Begin() ) {

                        listenedShow.Status = status;
                        uow.Commit();
                    }
                }
            }
        }
    }
}