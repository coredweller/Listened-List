using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace ListenedList.Handlers
{
    public abstract class BaseHandler : IHttpHandler
    {
        public virtual bool IsReusable { get { return false; } }

        protected const string ShowImagesFolder = "/images/Shows/";
        protected const string TicketStubImagesFolder = "/images/TicketStubs/";

        public void ProcessRequest(HttpContext context)
        {
            ProcessRequest(new HttpContextWrapper(context));
        }

        public abstract void ProcessRequest(HttpContextBase context);

        public static bool HandleIfNotModified(HttpContextBase context, string etag)
        {
            bool notModified = false;

            if (!string.IsNullOrEmpty(etag))
            {
                string ifNoneMatch = context.Request.Headers["If-None-Match"];

                if (!string.IsNullOrEmpty(ifNoneMatch) && (string.CompareOrdinal(ifNoneMatch, etag) == 0))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotModified;
                    notModified = true;
                }
            }

            return notModified;
        }

    }
}