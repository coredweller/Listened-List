using System;
using System.Linq;
using System.Web.UI;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;
using Core.DomainObjects;
using Core.Services.Interfaces;
using Core.Helpers.Script;

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

            //Get the user Id and Bind the notes and tags
            BindNotes( show );
            BindTags( show );
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

        private void BindTags( IShow show ) {
            ITagService tagService = Ioc.GetInstance<ITagService>();

            var tags = tagService.GetTags( show.Id, GetUserId() );

            rptTags.DataSource = tags;
            rptTags.DataBind();
        }

        private void BindTags( Guid showId ) {

            var showService = Ioc.GetInstance<IShowService>();

            var show = showService.GetShow( showId );

            BindTags( show );
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

            ValidateSuccess(success, "You have saved your notes for this show.", "There was an error saving your notes for this show." );
                
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
            if ( string.IsNullOrEmpty( txtTagName.Text ) || string.IsNullOrEmpty( hdnListenedId.Value ) ) return;
            
            var userId = GetUserId();
            Guid showId = Guid.Empty;

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
                var listenedShowService = Ioc.GetInstance<IListenedShowService>();

                var listenedId = new Guid( hdnListenedId.Value );
                var listenedShow = listenedShowService.GetById( listenedId );
                showId = listenedShow.ShowId;

                var newTag = _DomainObjectFactory.CreateTag( txtTagName.Text, listenedShow.ShowId, userId );

                tagService.SaveCommit( newTag, out success );
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "THERE WAS AN EXCEPTION SAVING A NEW TAG ON NOTES.ASPX with message: " + ex.Message );
                success = false;
            }

            ValidateTags( success, "You have saved your tag successfully.", "There was an error saving your tag.", showId );
        }

        

        public void btnApplyTag_Click( object sender, EventArgs e ) {
            throw new NotImplementedException();
        }

        public void rptTags_ItemCommand( object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e ) {

            switch ( e.CommandName.ToLower() ) {
                case "delete":
                    DeleteTag( new Guid( e.CommandArgument.ToString() ) );
                    break;
            }
        }

        private void DeleteTag( Guid tagId ) {
            ITagService tagService = Ioc.GetInstance<ITagService>();

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