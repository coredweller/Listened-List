using System;
using System.Linq;
using System.Web.UI;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;
using Core.DomainObjects;
using Core.Services.Interfaces;
using Core.Helpers.Script;
using System.Web.UI.WebControls;

namespace ListenedList
{
    public partial class Notes : ListenedBasePage
    {
        public string ShowTitle { get; set; }

        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }

            //Has to be after bind so the first time the user comes to the page
            // The show title will be set in Bind()
            ShowTitle = hdnShowTitle.Value;
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

            var results = tags.Where( x => x.ShowTag == null ).Select( y => y.Tag ).ToList();

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
            if ( string.IsNullOrEmpty( txtNotes.Text ) || string.IsNullOrEmpty( hdnListenedId.Value ) ) return;

            var success = false;

            try {

                var listenedId = new Guid( hdnListenedId.Value );

                var listenedShowService = Ioc.GetInstance<IListenedShowService>();

                using ( IUnitOfWork uow = UnitOfWork.Begin() ) {

                    var listenedShow = listenedShowService.GetById( listenedId );
                    listenedShow.Notes = txtNotes.Text;

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

        public void btnSearch_Click( object sender, EventArgs e ) {
            if ( string.IsNullOrEmpty( txtSearch.Text ) ) return;

            var listenedShowService = Ioc.GetInstance<IListenedShowService>();

            var listenedShows = listenedShowService.GetByUser( GetUserId() ).ToList();

            if ( listenedShows == null || listenedShows.Count <= 0 ) return;

            var filtered = listenedShows.Where( x => x.Notes.Contains( txtSearch.Text ) );

            rptNotes.DataSource = filtered;
            rptNotes.DataBind();
        }

        public void btnCreateTag_Click( object sender, EventArgs e ) {
            if ( string.IsNullOrEmpty( txtTagName.Text ) || string.IsNullOrEmpty( hdnShowId.Value ) ) return;

            var userId = GetUserId();
            Guid showId = new Guid(hdnShowId.Value);

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
                var showTag = _DomainObjectFactory.CreateShowTag( showId, newTag.Id );

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

            if(string.IsNullOrEmpty(hdnShowId.Value)) return;

            if ( ddlTags.SelectedValue == "-1" ) {
                prompt = new PromptHelper( "Please choose a valid tag." );
                Page.RegisterStartupScript( prompt.ScriptName, prompt.GetErrorScript() );
            }

            var showId = new Guid(hdnShowId.Value);

            var success = false;

            try {
                var showTagService = Ioc.GetInstance<IShowTagService>();

                var tagId = new Guid(ddlTags.SelectedValue);
                
                var showTag = _DomainObjectFactory.CreateShowTag( showId, tagId);

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