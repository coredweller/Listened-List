using System;
using System.Web;
using Core.Infrastructure.Logging;
using System.Web.UI.WebControls;
using Core.Membership;
using Core.DomainObjects;
using Data.DomainObjects;
using Core.Helpers.Script;
using Core.Services;
using Core.Infrastructure;

namespace ListenedList
{
    public class ListenedBasePage : System.Web.UI.Page
    {
        protected readonly string DefaultShowImageLocation = "~/images/Shows/";
        protected readonly string DefaultTitle = "Phisherman's Guide";

        protected IRoleProvider _RoleProvider = new ListenedRoleProvider();
        protected IMembershipProvider _MembershipProvider = new ListenedMembershipProvider();
        protected IDomainObjectFactory _DomainObjectFactory = Ioc.GetInstance<IDomainObjectFactory>();
        protected LogWriter _Log = new LogWriter();

        protected readonly Guid EmptyGuid = new Guid( "00000000-0000-0000-0000-000000000000" );

        public ListItem[] GetDropDownFromEnum( Type type, int startingEnumIndex, string firstItemMessage ) {
            var jamTypeNames = Enum.GetNames( type );
            var jamTypeValue = Enum.GetValues( type );

            int numItems = jamTypeValue.Length;

            ListItem[] items = new ListItem[numItems];

            for ( int i = startingEnumIndex ; i < numItems ; i++ ) {
                var jam = jamTypeNames[i];
                var jamValue = (int)jamTypeValue.GetValue( i );

                items[i] = new ListItem( jam, jamValue.ToString() );
            }

            int val = (int)Enum.GetValues( type ).GetValue( 0 );

            ListItem item = new ListItem( firstItemMessage, val.ToString() );

            items[0] = item;

            item.Selected = true;

            return items;
        }

        public ListItem[] GetListenedStatusForDropDown() {
            return new ListItem[] {
                new ListItem( "Please choose status", "-1"),
                new ListItem( "Never Heard", ((int)ListenedStatus.None).ToString() ),
                new ListItem( "In Progress", ( (int)ListenedStatus.InProgress ).ToString() ),
                new ListItem( "Finished", ((int)ListenedStatus.Finished).ToString() ),
                new ListItem( "Need To Listen", ((int)ListenedStatus.NeedToListen).ToString())
            };
        }

        public string FormatDate( DateTime? date ) {
            return date != null ? date.Value.ToString( "MM/dd/yyyy" ) : string.Empty;
        }

        public string ShortDescription( string notes, int desiredLength ) {
            if ( string.IsNullOrEmpty( notes ) ) return string.Empty;

            int lastIndex = notes.Length <= desiredLength ? notes.Length : desiredLength;
            return notes.Substring( 0, lastIndex );
        }

        public Guid GetUserId() {
            return Base.GetUserId( Page.User.Identity.Name );
        }

        public Guid GetUserId( string userName ) {
            return Base.GetUserId( userName );
        }

        protected override void OnLoad( EventArgs e ) {
            //Set it to the Default initially but any 
            // inheriting page can call SetPageTitle as well
            SetPageTitle( DefaultTitle );

            base.OnLoad( e );
        }

        protected void ValidateSuccess( bool success, string successMessage, string error ) {
            Page.RegisterStartupScript( "validateSuccess", Base.ValidateSuccess( success, successMessage, error ) );
        }

        protected void ShowError( string errorMessage ) {
            var prompt = new PromptHelper( errorMessage );
            var errorScript = prompt.GetErrorScript();
            Page.RegisterStartupScript( "validateSuccess", errorScript );
        }

        protected virtual void SetPageTitle( string title ) {
            Page.Title = title;
        }

        protected bool EmptyNullUndefined( string brih ) {
            if ( string.IsNullOrEmpty( brih ) || brih == "undefined" )
                return true;

            return false;
        }

        /// <summary>
        /// This function prevent the page being retrieved from browser cache
        /// </summary>
        protected void ExpirePageCache() {
            Response.Cache.SetCacheability( HttpCacheability.ServerAndNoCache );
            Response.Cache.SetExpires( DateTime.Now - new TimeSpan( 1, 0, 0 ) );
            Response.Cache.SetLastModified( DateTime.Now );
            Response.Cache.SetAllowResponseInBrowserHistory( false );
        }
    }
}