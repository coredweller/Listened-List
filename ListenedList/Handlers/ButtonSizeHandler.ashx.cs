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
    /// Summary description for ButtonSizeHandler
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