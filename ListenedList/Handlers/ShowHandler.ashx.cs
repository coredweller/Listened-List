using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListenedList.Handlers
{
    /// <summary>
    /// Summary description for ShowHandler
    /// </summary>
    public class ShowHandler : BaseHandler
    {

        public override void ProcessRequest( HttpContextBase context ) {

            HttpRequestBase request = context.Request;
            var showId = request.QueryString["s"];
            HttpResponseBase response = context.Response;
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}