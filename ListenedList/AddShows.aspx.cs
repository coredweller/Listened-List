﻿using System;
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
                RadioButton finishedChk = item.FindControl( finished ) as RadioButton;
                RadioButton inProgressChk = item.FindControl( inProgress ) as RadioButton;
                RadioButton needToListenChk = item.FindControl( needToListen ) as RadioButton;
                RadioButton neverHeardChk = item.FindControl( neverHeard ) as RadioButton;

                finishedChk.CheckedChanged += new EventHandler( CheckedChanged );
                inProgressChk.CheckedChanged += new EventHandler( CheckedChanged );
                needToListenChk.CheckedChanged += new EventHandler( CheckedChanged );
                neverHeardChk.CheckedChanged += new EventHandler( CheckedChanged );
            }
        }
        
        private void CheckedChanged( object sender, EventArgs e ) {
            RadioButton rb = (RadioButton)sender;
            var showDate = DateTime.Parse( rb.ToolTip );
            int status = 0;

            switch ( rb.ID ) {
                case finished:
                    status = (int)ListenedStatus.Finished;
                    break;
                case inProgress:
                    status = (int)ListenedStatus.InProgress;
                    break;
                case needToListen:
                    status = (int)ListenedStatus.NeedToListen;
                    break;
                case neverHeard:
                    status = (int)ListenedStatus.None;
                    break;
            }

            if ( rb.Checked ) {
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