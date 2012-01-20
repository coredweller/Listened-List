using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Infrastructure.Logging;
using System.Web.UI.WebControls;

namespace ListenedList
{
    public class ListenedBasePage : System.Web.UI.Page
    {
        protected readonly string BaseRoleType = "Registered";
        protected readonly string DefaultShowImageLocation = "~/images/Shows/";
        protected readonly string DefaultTitle = "The Listened List";

        protected readonly Guid EmptyGuid = new Guid("00000000-0000-0000-0000-000000000000");

        protected LogWriter log = new LogWriter();

        //var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

        public ListItem[] GetDropDownFromEnum(Type type, int startingEnumIndex, string firstItemMessage)
        {
            var jamTypeNames = Enum.GetNames(type);
            var jamTypeValue = Enum.GetValues(type);

            int numItems = jamTypeValue.Length;

            ListItem[] items = new ListItem[numItems];

            for (int i = startingEnumIndex; i < numItems; i++)
            {
                var jam = jamTypeNames[i];
                var jamValue = (int)jamTypeValue.GetValue(i);

                items[i] = new ListItem(jam, jamValue.ToString());
            }

            int val = (int)Enum.GetValues(type).GetValue(0);

            ListItem item = new ListItem(firstItemMessage, val.ToString());

            items[0] = item;

            item.Selected = true;

            return items;
        }

        public string FormatDate(DateTime? date)
        {
            return date != null ? date.Value.ToString("MM/dd/yyyy") : string.Empty;
        }
                
        public string ShortDescription(string notes, int desiredLength)
        {
            if (string.IsNullOrEmpty(notes)) return string.Empty;

            int lastIndex = notes.Length <= desiredLength ? notes.Length : desiredLength;
            return notes.Substring(0, lastIndex);
        }

        protected override void OnLoad(EventArgs e)
        {
            //Set it to the Default initially but any 
            // inheriting page can call SetPageTitle as well
            SetPageTitle(DefaultTitle);

            base.OnLoad(e);
        }

        protected virtual void SetPageTitle(string title)
        {
            Page.Title = title;
        }

        protected bool EmptyNullUndefined(string brih)
        {
            if (string.IsNullOrEmpty(brih) || brih == "undefined")
                return true;

            return false;
        }

        /// <summary>
        /// This function prevent the page being retrieved from browser cache
        /// </summary>
        protected void ExpirePageCache()
        {
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetExpires(DateTime.Now - new TimeSpan(1, 0, 0));
            Response.Cache.SetLastModified(DateTime.Now);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
        }
    }
}