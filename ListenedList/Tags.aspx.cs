using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Infrastructure;
using Core.Services.Interfaces;

namespace ListenedList
{
    public partial class Tags : ListenedBasePage
    {
        ITagService _tagService = Ioc.GetInstance<ITagService>();

        protected void Page_Load( object sender, EventArgs e ) {
            if ( !IsPostBack ) {
                Bind();
            }
        }

        private void Bind() {
            var tags = _tagService.GetTags( GetUserId() );

            rptTags.DataSource = tags;
            rptTags.DataBind();
        }

        public void btnSaveTagName_Click( object sender, EventArgs e ) {
            //1. Save the tag name
            throw new NotImplementedException();
        }

        public void rptTags_ItemCommand( object source, RepeaterCommandEventArgs e ) {

            var tagId = new Guid( e.CommandArgument.ToString() );

            switch ( e.CommandName.ToLower() ) {
                case "delete":
                    DeleteTag( tagId );
                    break;
                case "edit":
                    EditTag( tagId );
                    break;
            }
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
            //2. Edit tag
        }
    }
}