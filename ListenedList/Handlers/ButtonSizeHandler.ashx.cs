using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Infrastructure.Logging;
using Core.Services.Interfaces;
using Core.Services;

namespace ListenedList.Handlers
{
    /// <summary>
    /// On the Main page when the Plus or Minus button is pressed
    ///   it alters the size of the show buttons and this handler
    ///     saves the new button sizes.
    /// </summary>
    public class ButtonSizeHandler : BaseHandler
    {
        LogWriter writer = new LogWriter();
        public override void ProcessRequest( HttpContextBase context ) {
            HttpRequestBase request = context.Request;
            var w = request.QueryString["width"];
            var f = request.QueryString["fontSize"];
            var userName = request.QueryString["uName"].ToString();
            HttpResponseBase response = context.Response;

            float width, fontSize;
            if ( !( float.TryParse( w, out width ) && float.TryParse( f, out fontSize ) && !string.IsNullOrEmpty(userName) ) ) return;

            var profileService = new ProfileService( userName );
            var profile = profileService.GetUserProfile();

            profile.ButtonSize = width;
            profile.FontSize = fontSize;
            profile.Save();
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}