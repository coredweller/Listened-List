using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Infrastructure;
using Core.Services.Interfaces;
using Core.Helpers.Script;
using Core.Helpers;
using Core.DomainObjects;
using System.Linq;


namespace ListenedList.Controls
{
    public partial class TagControl : ListenedBaseControl
    {
        ITagService _tagService = Ioc.GetInstance<ITagService>();
        public bool Tutorial { get; set; }

        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }

            Page.LoadComplete += new EventHandler( Tags_LoadComplete );
        }

        protected void Page_PreRender( object sender, EventArgs e ) {
            if ( Tutorial ) {
                phPart1.Visible = true;
                phPart2.Visible = true;
                //phPart3.Visible = true;
            }
        }

        public void lnkSeeAll_Click( object sender, EventArgs e ) {
            HideShowList();
            BindTags(Guid.Empty);
        }

        private void HideShowList() {
            phShowList.Visible = false;
        }

        //This is used to set the color of each individual list item in ddlColor
        protected void Tags_LoadComplete( object sender, EventArgs e ) {
            foreach ( ListItem item in ddlColor.Items ) {

                var addColor = "background-color:" + item.Value;
                item.Attributes.Add( "style", addColor );
            }
        }

        private void Bind() {
            //Bind the colors drop down
            var colors = TagColors.GetAllColors();
            ddlColor.Items.Clear();

            var colorList = new List<ListItem>();
            foreach ( var color in colors ) {
                var item = new ListItem( color.ColorName, color.Hex );
                colorList.Add( item );
                ddlColor.Items.Add( item );
            }

            BindTags(Guid.Empty);
        }

        private void BindTags(Guid tagId) {

            IList<ITag> tags = new List<ITag>();
            if ( tagId == Guid.Empty ) {

                tags = _tagService.GetTags( GetUserId() );
            }
            else {
                tags = new List<ITag>{_tagService.GetTag( tagId ) };
            }

            rptTags.DataSource = tags.OrderBy( x => x.Name );
            rptTags.DataBind();
        }

        public void btnSaveTagName_Click( object sender, EventArgs e ) {
            if ( Tutorial ) {
                phPart3.Visible = true;
            }

            if ( string.IsNullOrEmpty( txtTagName.Text ) ) {
                PromptHelper prompt = new PromptHelper( "Please choose a tag to edit" );
                Page.RegisterStartupScript( prompt.ScriptName, prompt.GetErrorScript() );
            }

            string previousColor = string.Empty;
            var success = false;
            try {
                var tagId = (Guid)ViewState["TagId"];

                using ( IUnitOfWork uow = UnitOfWork.Begin() ) {
                    var tag = _tagService.GetTag( tagId );
                    tag.Name = txtTagName.Text;
                    tag.Color = TagColors.GetColorByHex( ddlColor.SelectedValue ).CssClass;
                    previousColor = ddlColor.SelectedValue;

                    uow.Commit();
                    success = true;
                }

            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "THERE WAS AN ERROR SAVING A TAG NAME ON TAGS.ASPX with message: " + ex.Message );
                success = false;
            }

            Bind();
            if ( !string.IsNullOrEmpty( previousColor ) ) ddlColor.SelectedValue = previousColor;

            ValidateSuccess( success, "You have successfully saved the new tag name.", "There was an error saving the new tag name." );
        }

        public void rptTags_ItemCommand( object source, RepeaterCommandEventArgs e ) {

            var tagId = new Guid( e.CommandArgument.ToString() );

            switch ( e.CommandName.ToLower() ) {
                case "click":
                    ClickTag( tagId );
                    break;
                case "delete":
                    DeleteTag( tagId );
                    break;
                case "edit":
                    EditTag( tagId );
                    break;
            }

        }

        private void ClickTag( Guid tagId ) {

            var tag = _tagService.GetTag( tagId );

            if(tag == null) return;

            BindTags( tag.Id );

            var showTagService = Ioc.GetInstance<IShowTagService>();
            var showTags = showTagService.GetTagsByTagAndUser( tag.Id, tag.UserId );
            var showIds = showTags.Select( x => x.ShowId ).ToList();

            var showService = Ioc.GetInstance<IShowService>();

            var shows = showService.GetShows( showIds );

            phShowList.Visible = true;
            rptShows.DataSource = shows;
            rptShows.DataBind();
        }

        public void btnCreateTag_Click( object sender, EventArgs e ) {
            if ( string.IsNullOrEmpty( txtNewTagName.Text ) ) return;

            HideShowList();
            var userId = GetUserId();

            var tag = _tagService.GetTag( txtNewTagName.Text.Trim(), userId );

            PromptHelper prompt;
            if ( tag != null ) {
                prompt = new PromptHelper( "You already have a tag with the same name. Please choose it from your list or give it a new name." );
                Page.RegisterStartupScript( prompt.ScriptName, prompt.GetErrorScript() );
                return;
            }

            var success = false;

            try {

                var newTag = _DomainObjectFactory.CreateTag( txtNewTagName.Text, userId );
                _tagService.SaveCommit( newTag, out success );

            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "THERE WAS AN ERROR SAVING A NEW TAG ON TAGS.ASPX with message: " + ex.Message );
                success = false;
            }

            Bind();
            ValidateSuccess( success, "You have saved your tag successfully.", "There was an error saving your tag." );
        }

        private void DeleteTag( Guid tagId ) {

            var success = false;

            try {
                var tag = _tagService.GetTag( tagId );

                if ( tag == null ) return;

                _tagService.Delete( tag );
                success = true;
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "THERE WAS AN ERROR DELETING A TAG ON TAGS.ASPX with message: " + ex.Message );
                success = false;
            }

            Bind();
            ValidateSuccess( success, "You have successfully deleted the tag.", "There was an error deleting the tag." );
        }

        private void EditTag( Guid tagId ) {

            var tagService = Ioc.GetInstance<ITagService>();

            var tag = tagService.GetTag( tagId );

            if ( tag == null ) return;

            phEditTag.Visible = true;

            txtTagName.Text = tag.Name;
            var color = TagColors.GetColorByCssClass( tag.Color );

            ddlColor.SelectedIndex = -1;

            var item = ddlColor.Items.FindByValue( color.Hex );
            item.Selected = true;

            ViewState["TagId"] = tag.Id;
        }
    }
}