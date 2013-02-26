using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Infrastructure.Logging;
using Core.Infrastructure;
using Core.Services.Interfaces;
using Core.Helpers.JSON;
using System.Text;

namespace ListenedList.Handlers
{
    /// <summary>
    /// On the Notes page when the Star rating is changed by the user
    ///   the star count must be saved for that listened show.
    /// </summary>
    public class RatingHandler : BaseHandler
    {
        LogWriter writer = new LogWriter();
        public override void ProcessRequest( HttpContextBase context ) {
            HttpRequestBase request = context.Request;
            var l = request.QueryString["l"];
            var r = request.QueryString["r"];
            HttpResponseBase response = context.Response;

            Guid listenedId;
            double rating;

            if ( !( Guid.TryParse( l, out listenedId ) && double.TryParse( r, out rating ) ) ) return;

            var success = false;

            try {

                var listenedService = Ioc.GetInstance<IListenedShowService>();

                using ( IUnitOfWork uow = UnitOfWork.Begin() ) {
                    var listenedShow = listenedService.GetById( listenedId );

                    //If there is no listened show then there is no way
                    // that a rating can be taking place. Notes creates a new
                    //  listened show for you if one does not already exist.
                    if ( listenedShow == null ) return;

                    listenedShow.Stars = rating;
                    listenedShow.UpdatedDate = DateTime.UtcNow;

                    uow.Commit();
                    success = true;
                }

            }
            catch ( Exception ex ) {
                writer.WriteFatal( " THERE WAS AN EXCEPTION SAVING THE RATING ON NOTES.ASPX" + ex.Message );
            }

            var jsonifier = new BasicJSONifier( "records", "Question", "Answer" );
            jsonifier.Add( "success", success.ToString() );
            var final = jsonifier.GetFinalizedJSON();

            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.Write( final );
            response.End();
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}