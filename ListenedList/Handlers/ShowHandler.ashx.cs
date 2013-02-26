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
using Core.Helpers;
using Core.DomainObjects;

namespace ListenedList.Handlers
{
    /// <summary>
    /// On the Main page when a show button is clicked.
    ///   the new status must be recorded for that show
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
            bool displayAttended = false;
            int displayStatus = 0;

            //Get the show date, user id, and status from the request and parse them into concrete types
            if ( !( DateTime.TryParse( sDate, out showDate ) && Guid.TryParse( uId, out userId ) && int.TryParse( st, out status ) ) ) {
                return;
            }

            var listenedShowService = Ioc.GetInstance<IListenedShowService>(); ;
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

                        if ( status != (int)ListenedStatus.Attended ) {
                            var prevStatus = listenedShow.Status;

                            if ( status == (int)ListenedStatus.EditNotes ) status = prevStatus;

                            listenedShow.Status = status;
                            listenedShow.UpdatedDate = Constants.Now();

                            //For display purposes on Main. 
                            displayAttended = listenedShow.Attended;
                            //This needs to be here because if edit notes is the status we need to make sure we take the previous status
                            displayStatus = status;

                            writer.Write( string.Format( "Updating listenedShow with Id:{0}, from the status: {1}, to the status: {2}", listenedShow.Id, prevStatus, status ) );

                        }
                        else {
                            //What it was before, make it the opposite
                            listenedShow.Attended = !listenedShow.Attended;
                            listenedShow.UpdatedDate = Constants.Now();

                            //For display purposes on Main. 
                            displayAttended = listenedShow.Attended;
                            //This needs to be here b/c we don't know what old status was otherwise
                            displayStatus = listenedShow.Status;

                            writer.Write( "Updating listenedShow with Id:" + listenedShow.Id + "with Attended = " + !listenedShow.Attended );
                        }

                        uow.Commit();
                        success = true;
                        writer.Write( "Successfully updated listenedShow id: " + listenedShow.Id );
                    }
                    //}
                }
                else {
                    //If the user does not have one then create it
                    var objectFactory = Ioc.GetInstance<IDomainObjectFactory>();

                    bool attended;
                    if ( status == (int)ListenedStatus.Attended ) {
                        attended = true;
                    }
                    else {
                        attended = false;
                    }

                    var newListenedShow = objectFactory.CreateListenedShow( show.Id, userId, show.ShowDate.Value, status, string.Empty, attended );
                    displayAttended = attended;
                    displayStatus = status;

                    writer.Write( string.Format( "Saving a new listenedShow with Id:{0} with a status of {1}, and attended is {2}", newListenedShow.Id, status, attended ) );

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
                jsonifier.Add( "attended", displayAttended.ToString() );
                jsonifier.Add( "status", displayStatus.ToString() );
            }
            else {
                jsonifier.Add( "success", "false" );
            }

            final = jsonifier.GetFinalizedJSON();

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