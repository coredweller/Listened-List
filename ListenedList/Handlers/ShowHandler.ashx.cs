using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;
using Core.Infrastructure.Logging;
using Core.Helpers.JSON;
using System.Text;
using Core.Services.Interfaces;

namespace ListenedList.Handlers
{
    /// <summary>
    /// Summary description for ShowHandler
    /// </summary>
    public class ShowHandler : BaseHandler
    {
        LogWriter writer = new LogWriter();
        public override void ProcessRequest( HttpContextBase context ) {

            HttpRequestBase request = context.Request;
            var sDate = request.QueryString["s"];
            var uId = request.QueryString["u"];
            var st = request.QueryString["st"];
            HttpResponseBase response = context.Response;

            Guid userId;
            DateTime showDate;
            int status = 0;
            bool success = false;
            string final;

            //Get the show date, user id, and status from the request and parse them into concrete types
            if ( !( DateTime.TryParse( sDate, out showDate ) && Guid.TryParse( uId, out userId ) && int.TryParse( st, out status ) ) ) {
                return;
            }

            var listenedShowService = Ioc.GetInstance<IListenedShowService>();;
            var showService = new ShowService( Ioc.GetInstance<IShowRepository>() );

            var show = showService.GetShow( showDate );

            //see if the user has an entry for this show yet
            var listenedShow = listenedShowService.GetByUserAndShowId( userId, show.Id );
            //Setup the JSON Builder
            var jsonifier = new BasicJSONifier( "records", "Question", "Answer" );

            //If status is EditNotes don't do anything and just get out of here
            if ( status == (int)ListenedStatus.EditNotes ) return;

            try {
                
                //If the user has one then update it
                if ( listenedShow != null ) {

                    //if ( status != (int)ListenedStatus.EditNotes ) {
                        using ( IUnitOfWork uow = UnitOfWork.Begin() ) {
                            var prevStatus = listenedShow.Status;

                            if ( status == (int)ListenedStatus.EditNotes ) status = prevStatus;

                            listenedShow.Status = status;

                            writer.Write( string.Format( "Updating listenedShow with Id:{0}, from the status: {1}, to the status: {2}", listenedShow.Id, prevStatus, status ) );
                            uow.Commit();
                            success = true;
                            writer.Write( "Successfully updated listenedShow id: " + listenedShow.Id );
                        }
                    //}
                }
                else {
                    //If the user does not have one then create it
                    var objectFactory = new Data.DomainObjects.DomainObjectFactory();

                    //If status is EditNotes then set it to 0 because that is the base.
                    //if ( status == (int)ListenedStatus.EditNotes ) status = 0;

                    var newListenedShow = objectFactory.CreateListenedShow( show.Id, userId, show.ShowDate.Value, status, string.Empty );

                    writer.Write( string.Format( "Saving a new listenedShow with Id:{0} with a status of {1}", newListenedShow.Id, status ) );

                    listenedShowService.SaveCommit( newListenedShow, out success );

                    if ( success ) writer.Write( "Successfully saved the new listenedShow with id: " + newListenedShow.Id );
                }

            }
            catch ( Exception ex ) {
                writer.WriteFatal( "There was an error saving a listenedShow. The exception is: " + ex.Message );
                success = false;
            }

            if ( success ) {
                jsonifier.Add( "success", "true" );
            }
            else {
                jsonifier.Add( "success", "false" );
            }

            final = jsonifier.GetFinalizedJSON();

            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.Write(final);
            response.End();
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}