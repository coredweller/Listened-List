﻿using System;
using System.Linq;
using System.Web.UI;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;
using Core.DomainObjects;
using Core.Services.Interfaces;
using Core.Helpers.Script;
using System.Web.UI.WebControls;
using Core.Extensions;
using Microsoft.AspNet.FriendlyUrls;
using Core.Configuration;
using System.Net;
using System.Collections;
using System.IO;
using Procurios.Public;
using System.Collections.Generic;
using ListenedList.Code;

namespace ListenedList
{
    public partial class Notes : ListenedBasePage
    {
        public string ShowTitle { get; set; }
        private const string statusSelected = "statusSelected";
        private const int DefaultPageSize = 10;
        IListenedShowService _ListenedShowService = Ioc.GetInstance<IListenedShowService>();
        IShowTagService _ShowTagService = Ioc.GetInstance<IShowTagService>();
        protected const int DEFAULT_MAX_TAG_NAME = 30;
        private TimeZone _LocalZone = TimeZone.CurrentTimeZone;
        public static double CurrentRating { get; set; }
        
        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }

            //Has to be after bind so the first time the user comes to the page
            // The show title will be set in Bind()
            ShowTitle = hdnShowTitle.Value;
        }

        protected override void OnInit( EventArgs e ) {


            base.OnInit( e );
        }

        #region Data Binding

        private void Bind() {

            DateTime showDate;
            Guid showId;

            var showService = new ShowService( Ioc.GetInstance<IShowRepository>() );

            IShow show = null;

            var segment = Request.GetFriendlyUrlSegments().FirstOrDefault();

            //Check to see if the show date or show id is passed in then get the IShow for that show
            if ( DateTime.TryParse( segment, out showDate ) ) {
                show = showService.GetShow( showDate );
            }
            else if ( Guid.TryParse( segment, out showId ) ) {
                show = showService.GetShow( showId );
            }

            //If it is not a valid show then get out of here
            if ( show == null ) Response.Redirect( LinkBuilder.DefaultMainLink() );

            hdnShowId.Value = show.Id.ToString();

            lnkPhishShows.NavigateUrl = GetPhishowsLink( showDate );
            lnkPhishTracks.NavigateUrl = GetPhishTracksLink( showDate );

            //Get the user Id and Bind the notes and tags
            BindNotes( show );
            BindTags( show.Id );
            BindListenedShow( show );
            BindSetlist( show.ShowDate.Value );
        }

        private void BindSetlist( DateTime showDate ) {

            string setlist = string.Empty;
            try {
                var appConfigManager = Ioc.GetInstance<IAppConfigManager>();
                var apiKey = appConfigManager.AppSettings["PhishNetApiKey"];

                if ( !string.IsNullOrEmpty( apiKey ) ) {
                    var url = GetPhishNetLink(showDate, apiKey);

                    var request = (HttpWebRequest)WebRequest.Create( url );

                    var response = (HttpWebResponse)request.GetResponse();

                    var reader = new StreamReader( response.GetResponseStream() );

                    var resp = reader.ReadToEnd();

                    setlist = ParseJson( resp, showDate );
                }
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "There was an exception getting the setlist from Phish.net.  exception info here: " + ex.Message );
                setlist = string.Empty;
            }

            if ( string.IsNullOrEmpty( setlist ) ) {
                phSetlist.Visible = false;
            }
            else {
                lblSetlist.InnerHtml = setlist;
                phSetlist.Visible = true;
            }
        }

        private string ParseJson( string response, DateTime showDate ) {
            var success = false;
            ArrayList jsonHash = null;
            try {
                jsonHash = (ArrayList)JSON.JsonDecode( response, ref success );
            }
            catch ( Exception ) {
                return string.Empty;
                _Log.WriteFatal( "There was an error getting a setlist for the show date of: " + showDate.ToShortDateString() );
            }

            if ( !success || jsonHash == null ) return string.Empty;

            var showData = (Hashtable)jsonHash[0];

            if ( showData["setlistdata"] == null ) return string.Empty;

            return showData["setlistdata"].ToString();
        }

        private void BindListenedShow( IShow show ) {
            var listened = _ListenedShowService.GetByUserAndShowId( GetUserId(), show.Id );

            if ( listened == null ) return;

            if ( listened.Attended ) {
                btnAttended.Text = "Attended";
                btnAttended.CssClass.Remove( 0 );
                btnAttended.CssClass = "notesAttended";
                hdnAttended.Value = "true";
            }

            if ( listened.Stars != null && listened.Stars.HasValue ) {
                CurrentRating = listened.Stars.Value;
            }
            else {
                CurrentRating = 0;
            }

            lblCreatedDate.Text = _LocalZone.ToLocalTime( listened.CreatedDate ).ToString();
            lblUpdatedDate.Text = listened.UpdatedDate.HasValue ? _LocalZone.ToLocalTime( listened.UpdatedDate.Value ).ToString() : "";

            Button button;
            switch ( listened.Status ) {
                case (int)ListenedStatus.InProgress:
                    button = btnInProgress;
                    break;
                case (int)ListenedStatus.Finished:
                    button = btnFinished;
                    break;
                case (int)ListenedStatus.NeedToListen:
                    button = btnNeedToListen;
                    break;
                case (int)ListenedStatus.None:
                default:
                    button = btnNeverHeard;
                    break;
            }

            SetListenedStatusButton( button );
        }

        public void btnAttended_Click( object sender, EventArgs e ) {
            if ( string.IsNullOrWhiteSpace( hdnAttended.Value ) ) return;

            bool attended;
            var success = bool.TryParse( hdnAttended.Value, out attended );

            if ( !success ) return;

            if ( string.IsNullOrWhiteSpace( hdnListenedId.Value ) ) return;

            Guid listenedId;
            if ( !Guid.TryParse( hdnListenedId.Value, out listenedId ) ) return;


            var listened = _ListenedShowService.GetById( listenedId );

            if ( listened == null ) return;

            using ( IUnitOfWork uow = UnitOfWork.Begin() ) {
                listened.Attended = !attended;

                uow.Commit();
            }

            if ( listened.Attended ) {
                btnAttended.Text = "Attended";
                btnAttended.CssClass.Remove( 0 );
                btnAttended.CssClass = "notesAttended";
                hdnAttended.Value = "true";
            }
            else {
                btnAttended.Text = "Did Not Attend";
                btnAttended.CssClass.Remove( 0 );
                btnAttended.CssClass = "notesDidNotAttend";
                hdnAttended.Value = "false";
            }
        }

        private void BindNotes( IShow show ) {

            hdnShowTitle.Value = show.GetShowName();

            var userId = GetUserId();
            var listenedShow = _ListenedShowService.GetByUserAndShowId( userId, show.Id );

            if ( listenedShow == null ) {

                bool success = false;

                listenedShow = _DomainObjectFactory.CreateListenedShow( show.Id, userId, show.ShowDate.Value, (int)ListenedStatus.None, string.Empty );
                _ListenedShowService.SaveCommit( listenedShow, out success );

                if ( !success ) {
                    _Log.Write( "Saving a listened show for user: {0} and show: {1} failed", userId, show.ShowDate.Value );
                    return;
                }
            }

            hdnListenedId.Value = listenedShow.Id.ToString();
            txtNotes.Text = listenedShow.Notes;

        }

        private void BindTags( Guid showId ) {

            ITagService tagService = Ioc.GetInstance<ITagService>();

            var userId = GetUserId();

            //Bind the tag drop down list
            var allTags = tagService.GetTags( userId );
            var showTags = _ShowTagService.GetTagsByShowAndUser( showId, userId );

            var tags = ( from aT in allTags
                         join sT in showTags on aT.Id equals sT.TagId into temp
                         from t in temp.DefaultIfEmpty()
                         select new { Tag = aT, ShowTag = t } );

            var results = tags.Where( x => x.ShowTag == null ).Select( y => y.Tag ).OrderBy( z => z.Name ).ToList();

            ddlTags.Items.Clear();
            foreach ( var r in results ) {
                ddlTags.Items.Add( new ListItem( r.Name, r.Id.ToString() ) );
            }

            var item = new ListItem( "Please select a tag", "-1" );
            ddlTags.Items.Insert( 0, item );

            //Bind the tags repeater
            rptTags.DataSource = showTags;
            rptTags.DataBind();
        }

        #endregion


        #region Events

        public void btnSubmit_Click( object sender, EventArgs e ) {
            //if ( (string.IsNullOrEmpty( txtNotes.Text ) && ddlStatus.SelectedValue == "-1" ) || string.IsNullOrEmpty( hdnListenedId.Value ) ) return;
            if ( string.IsNullOrEmpty( txtNotes.Text ) || string.IsNullOrEmpty( hdnListenedId.Value ) ) return;

            var success = false;
            DateTime newDate = DateTime.MinValue;
            try {

                var listenedId = new Guid( hdnListenedId.Value );

                using ( IUnitOfWork uow = UnitOfWork.Begin() ) {

                    var listenedShow = _ListenedShowService.GetById( listenedId );
                    listenedShow.Notes = txtNotes.Text;

                    newDate = DateTime.UtcNow;
                    CurrentRating = listenedShow.Stars.HasValue ? listenedShow.Stars.Value : 0;
                    listenedShow.UpdatedDate = newDate;

                    uow.Commit();
                    success = true;
                }
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "THERE WAS AN EXCEPTION SAVING NOTES ON NOTES.ASPX with message: " + ex.Message );
                success = false;
            }

            var div = "divFail";
            if ( success )
                div = "divSuccess";

            Page.RegisterStartupScript( "SuccessScript", "<script type=\"text/javascript\"> $('#" + div + "').fadeIn().delay(5000).fadeOut(); </script>" );

            if ( success && newDate != DateTime.MinValue ) {
                var localDate = _LocalZone.ToLocalTime( newDate );
                lblUpdatedDate.Text = localDate.ToString();
            }

        }

        public void btnListenStatus_Click( object sender, EventArgs e ) {
            if ( string.IsNullOrEmpty( hdnListenedId.Value ) ) return;

            var clickedButton = (Button)sender;
            var listenedId = new Guid( hdnListenedId.Value );

            using ( IUnitOfWork uow = UnitOfWork.Begin() ) {

                var listenedShow = _ListenedShowService.GetById( listenedId );

                switch ( clickedButton.ID ) {
                    case "btnNeverHeard":
                        listenedShow.Status = (int)ListenedStatus.None;
                        break;
                    case "btnInProgress":
                        listenedShow.Status = (int)ListenedStatus.InProgress;
                        break;
                    case "btnFinished":
                        listenedShow.Status = (int)ListenedStatus.Finished;
                        break;
                    case "btnNeedToListen":
                        listenedShow.Status = (int)ListenedStatus.NeedToListen;
                        break;
                }

                uow.Commit();

                SetListenedStatusButton( clickedButton );
            }
        }

        private void SetListenedStatusButton( Button button ) {
            btnNeverHeard.CssClass = btnNeverHeard.CssClass.Replace( statusSelected, "" );
            btnInProgress.CssClass = btnInProgress.CssClass.Replace( statusSelected, "" );
            btnFinished.CssClass = btnFinished.CssClass.Replace( statusSelected, "" );
            btnNeedToListen.CssClass = btnNeedToListen.CssClass.Replace( statusSelected, "" );

            button.CssClass += " " + statusSelected;
            //button.BorderColor = System.Drawing.Color.Blue;
            //button.BorderStyle = BorderStyle.Solid;
            //button.BorderWidth = Unit.Pixel(4);
        }

        public void btnSearch_Click( object sender, EventArgs e ) {
            if ( string.IsNullOrEmpty( txtSearch.Text ) ) return;

            var listenedShows = _ListenedShowService.GetByUser( GetUserId() ).ToList();

            if ( listenedShows == null || listenedShows.Count <= 0 ) return;

            var searchTerm = txtSearch.Text.ToLower();
            var filtered = listenedShows.Where( x => x.Notes.ToLower().Contains( searchTerm ) );
            var total = filtered.Count();
            var paged = filtered.Take( DefaultPageSize ).ToList();
            hdnSearchTerm.Value = searchTerm;

            rptNotes.DataSource = paged;
            rptNotes.DataBind();

            IList<int> list = new List<int>();

            var pages = total / DefaultPageSize;
            for ( int i = 1 ; i <= pages ; i++ ) {
                list.Add( i );
            }

            rptPager.DataSource = list;
            rptPager.DataBind();
        }

        public void btnCreateTag_Click( object sender, EventArgs e ) {
            if ( string.IsNullOrEmpty( txtTagName.Text ) || string.IsNullOrEmpty( hdnShowId.Value ) ) return;

            var userId = GetUserId();
            Guid showId = new Guid( hdnShowId.Value );

            ITagService tagService = Ioc.GetInstance<ITagService>();
            var tag = tagService.GetTag( txtTagName.Text.Trim(), userId );

            PromptHelper prompt;
            if ( tag != null ) {
                prompt = new PromptHelper( "You already have a tag with the same name. Please choose it from your list or give it a new name." );
                Page.RegisterStartupScript( prompt.ScriptName, prompt.GetErrorScript() );
                return;
            }

            var success = false;

            try {
                var tagName = txtTagName.Text;
                var newTag = _DomainObjectFactory.CreateTag( tagName.Length > DEFAULT_MAX_TAG_NAME ? tagName.Substring( 0, DEFAULT_MAX_TAG_NAME ) : tagName, userId );
                var showTag = _DomainObjectFactory.CreateShowTag( showId, newTag.Id, GetUserId() );

                using ( IUnitOfWork uow = UnitOfWork.Begin() ) {

                    tagService.Save( newTag, out success );

                    bool showTagSuccess = false;
                    _ShowTagService.Save( showTag, out showTagSuccess );

                    success = success && showTagSuccess;

                    if ( success ) uow.Commit();
                }
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "THERE WAS AN EXCEPTION SAVING A NEW TAG ON NOTES.ASPX with message: " + ex.Message );
                success = false;
            }

            ValidateTags( success, "There was an error saving your tag.", showId );
        }

        public void btnApplyTag_Click( object sender, EventArgs e ) {
            PromptHelper prompt;

            if ( string.IsNullOrEmpty( hdnShowId.Value ) ) return;

            if ( ddlTags.SelectedValue == "-1" ) {
                prompt = new PromptHelper( "Please choose a valid tag." );
                Page.RegisterStartupScript( prompt.ScriptName, prompt.GetErrorScript() );
            }

            var showId = new Guid( hdnShowId.Value );

            var success = false;

            try {

                var tagId = new Guid( ddlTags.SelectedValue );

                var showTag = _DomainObjectFactory.CreateShowTag( showId, tagId, GetUserId() );

                _ShowTagService.SaveCommit( showTag, out success );
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "THERE WAS AN ERROR APPLYING A TAG ON NOTES.ASPX with message: " + ex.Message );
                success = false;
            }

            ValidateTags( success, "There was an error applying the tag.", showId );
        }

        public void rptTags_ItemCommand( object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e ) {

            switch ( e.CommandName.ToLower() ) {
                case "delete":
                    DeleteTag( new Guid( e.CommandArgument.ToString() ) );
                    break;
            }
        }

        private void DeleteTag( Guid tagId ) {

            var tag = _ShowTagService.GetTag( tagId );

            if ( tag == null ) return;

            _ShowTagService.Delete( tag );
            BindTags( tag.ShowId );

        }

        #endregion

        #region Utilities

        public string GetUrl( Guid id ) {
            return LinkBuilder.DefaultNotesLink( id.ToString() );
        }

        private void ValidateTags( bool success, string error, Guid showId ) {
            PromptHelper prompt;

            if ( success ) {
                BindTags( showId );
            }
            else {
                prompt = new PromptHelper( error );
                Page.RegisterStartupScript( prompt.ScriptName, prompt.GetErrorScript() );
            }
        }

        protected void rptPager_ItemCommand( object source, RepeaterCommandEventArgs e ) {
            int pageNumber = 0;
            if ( !int.TryParse( e.CommandArgument.ToString(), out pageNumber ) || string.IsNullOrEmpty( hdnSearchTerm.Value ) ) return;

            var skipAmount = ( pageNumber - 1 ) * DefaultPageSize;
            var notes = _ListenedShowService.GetByUser( GetUserId() )
                                .Where( x => x.Notes.ToLower().Contains( hdnSearchTerm.Value.ToLower() ) )
                                .Skip( skipAmount )
                                .Take( DefaultPageSize );

            rptNotes.DataSource = notes;
            rptNotes.DataBind();
        }

        #endregion
    }
}