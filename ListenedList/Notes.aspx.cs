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

namespace ListenedList
{
    public partial class Notes : ListenedBasePage
    {
        public string ShowTitle { get; set; }
        private const string statusSelected = "statusSelected";

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

            //Check to see if the show date or show id is passed in then get the IShow for that show
            if ( DateTime.TryParse( Request.QueryString["showDate"], out showDate ) ) {
                show = showService.GetShow( showDate );
            }
            else if ( Guid.TryParse( Request.QueryString["showId"], out showId ) ) {
                show = showService.GetShow( showId );
            }

            //If it is not a valid show then get out of here
            if ( show == null ) return;

            hdnShowId.Value = show.Id.ToString();

            //Get the user Id and Bind the notes and tags
            BindNotes( show );
            BindTags( show.Id );
            BindListenedShow( show );
        }

        private void BindListenedShow( IShow show ) {
            //var list = GetListenedStatusForDropDown();

            //ddlStatus.Items.AddRange( list );

            var listenedShowService = Ioc.GetInstance<IListenedShowService>(); ;

            var listened = listenedShowService.GetByUserAndShowId( GetUserId(), show.Id );

            if ( listened == null ) return;

            if ( listened.Attended ) {
                btnAttended.Text = "Attended";
                btnAttended.CssClass.Remove( 0 );
                btnAttended.CssClass = "notesAttended";
                hdnAttended.Value = "true";
            }

            lblCreatedDate.Text = string.Format( "First Created: {0}", listened.CreatedDate.ToString() );
            lblUpdatedDate.Text = string.Format( "Last Updated: {0}", listened.UpdatedDate.HasValue ? listened.UpdatedDate.Value.ToString() : "" );

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


            //var item = ddlStatus.Items.FindByValue( listened.Status.ToString() );
            //item.Selected = true;
        }

        public void btnAttended_Click( object sender, EventArgs e ) {
            if ( string.IsNullOrWhiteSpace( hdnAttended.Value ) ) return;

            bool attended;
            var success = bool.TryParse( hdnAttended.Value, out attended );

            if ( !success ) return;

            if ( string.IsNullOrWhiteSpace( hdnListenedId.Value ) ) return;

            Guid listenedId;
            if ( !Guid.TryParse( hdnListenedId.Value, out listenedId ) ) return;

            var listenedShowService = Ioc.GetInstance<IListenedShowService>();

            var listened = listenedShowService.GetById( listenedId );

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

            var listenedShowService = Ioc.GetInstance<IListenedShowService>();

            var userId = GetUserId();
            var listenedShow = listenedShowService.GetByUserAndShowId( userId, show.Id );

            if ( listenedShow == null ) {

                bool success = false;

                listenedShow = _DomainObjectFactory.CreateListenedShow( show.Id, userId, show.ShowDate.Value, (int)ListenedStatus.None, string.Empty );
                listenedShowService.SaveCommit( listenedShow, out success );

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
            IShowTagService showTagService = Ioc.GetInstance<IShowTagService>();

            var userId = GetUserId();

            //Bind the tag drop down list
            var allTags = tagService.GetTags( userId );
            var showTags = showTagService.GetTagsByShow( showId );

            var tags = ( from aT in allTags
                         join sT in showTags on aT.Id equals sT.TagId into temp
                         from t in temp.DefaultIfEmpty()
                         select new { Tag = aT, ShowTag = t } );

            var results = tags.Where( x => x.ShowTag == null ).Select( y => y.Tag ).OrderBy(z => z.Name).ToList();

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
            if (string.IsNullOrEmpty( txtNotes.Text ) || string.IsNullOrEmpty( hdnListenedId.Value ) ) return;

            var success = false;

            try {

                var listenedId = new Guid( hdnListenedId.Value );

                var listenedShowService = Ioc.GetInstance<IListenedShowService>();

                using ( IUnitOfWork uow = UnitOfWork.Begin() ) {

                    var listenedShow = listenedShowService.GetById( listenedId );
                    listenedShow.Notes = txtNotes.Text;
                    listenedShow.UpdatedDate = DateTime.UtcNow;

                    //if ( ddlStatus.SelectedValue != "-1" ) {
                    //    listenedShow.Status = int.Parse( ddlStatus.SelectedValue );
                    //}

                    uow.Commit();
                    success = true;
                }
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "THERE WAS AN EXCEPTION SAVING NOTES ON NOTES.ASPX with message: " + ex.Message );
                success = false;
            }

            ValidateSuccess( success, "You have saved your notes for this show.", "There was an error saving your notes for this show." );

        }

        public void btnListenStatus_Click( object sender, EventArgs e ) {
            if ( string.IsNullOrEmpty( hdnListenedId.Value ) ) return;

            var clickedButton = (Button)sender;
            var listenedId = new Guid( hdnListenedId.Value );

            var listenedShowService = Ioc.GetInstance<IListenedShowService>();

            using ( IUnitOfWork uow = UnitOfWork.Begin() ) {

                var listenedShow = listenedShowService.GetById( listenedId );
                
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

            var listenedShowService = Ioc.GetInstance<IListenedShowService>();

            var listenedShows = listenedShowService.GetByUser( GetUserId() ).ToList();

            if ( listenedShows == null || listenedShows.Count <= 0 ) return;

            var filtered = listenedShows.Where( x => x.Notes.ToLower().Contains( txtSearch.Text.ToLower() ) );

            rptNotes.DataSource = filtered;
            rptNotes.DataBind();
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

                var newTag = _DomainObjectFactory.CreateTag( txtTagName.Text, userId );
                var showTag = _DomainObjectFactory.CreateShowTag( showId, newTag.Id, GetUserId() );

                using ( IUnitOfWork uow = UnitOfWork.Begin() ) {

                    tagService.Save( newTag, out success );

                    bool showTagSuccess = false;
                    IShowTagService showTagService = Ioc.GetInstance<IShowTagService>();
                    showTagService.Save( showTag, out showTagSuccess );

                    success = success && showTagSuccess;

                    if ( success ) uow.Commit();
                }
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "THERE WAS AN EXCEPTION SAVING A NEW TAG ON NOTES.ASPX with message: " + ex.Message );
                success = false;
            }

            ValidateTags( success, "You have saved your tag successfully.", "There was an error saving your tag.", showId );
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
                var showTagService = Ioc.GetInstance<IShowTagService>();

                var tagId = new Guid( ddlTags.SelectedValue );

                var showTag = _DomainObjectFactory.CreateShowTag( showId, tagId, GetUserId() );

                showTagService.SaveCommit( showTag, out success );
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "THERE WAS AN ERROR APPLYING A TAG ON NOTES.ASPX with message: " + ex.Message );
                success = false;
            }

            ValidateTags( success, "You have successfully applied the tag.", "There was an error applying the tag.", showId );
        }

        public void rptTags_ItemCommand( object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e ) {

            switch ( e.CommandName.ToLower() ) {
                case "delete":
                    DeleteTag( new Guid( e.CommandArgument.ToString() ) );
                    break;
            }
        }

        private void DeleteTag( Guid tagId ) {
            IShowTagService tagService = Ioc.GetInstance<IShowTagService>();

            var tag = tagService.GetTag( tagId );

            if ( tag == null ) return;

            tagService.Delete( tag );
            BindTags( tag.ShowId );

        }

        #endregion

        #region Utilities

        public string GetUrl( Guid id ) {
            return "Notes.aspx?showId=" + id.ToString();
        }

        private void ValidateTags( bool success, string successMessage, string error, Guid showId ) {
            PromptHelper prompt;

            if ( success ) {
                BindTags( showId );
                prompt = new PromptHelper( successMessage );
                Page.RegisterStartupScript( prompt.ScriptName, prompt.GetSuccessScript() );
            }
            else {
                prompt = new PromptHelper( error );
                Page.RegisterStartupScript( prompt.ScriptName, prompt.GetErrorScript() );
            }
        }

        #endregion
    }
}