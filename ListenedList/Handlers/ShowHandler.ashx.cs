using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Services;
using Core.Infrastructure;
using Core.Repository;

namespace ListenedList.Handlers
{
    /// <summary>
    /// Summary description for ShowHandler
    /// </summary>
    public class ShowHandler : BaseHandler
    {

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

            if ( !( DateTime.TryParse( sDate, out showDate ) && Guid.TryParse( uId, out userId ) && int.TryParse( st, out status ) ) ) {
                return;
            }

            var listenedShowService = new ListenedShowService( Ioc.GetInstance<IListenedShowRepository>() );
            var showService = new ShowService( Ioc.GetInstance<IShowRepository>() );

            var show = showService.GetShow( showDate );

            var listenedShow = listenedShowService.GetByUserAndShowId( userId, show.Id );

            if ( listenedShow != null ) {
                using ( IUnitOfWork uow = UnitOfWork.Begin() ) {
                    listenedShow.Status = status;

                    listenedShowService.Save( listenedShow, out success );

                    uow.Commit();
                }
            }
            else {
                var objectFactory = new Data.DomainObjects.DomainObjectFactory();
                var newListenedShow = objectFactory.CreateListenedShow( show.Id, userId, status, string.Empty );

                listenedShowService.SaveCommit( newListenedShow, out success );
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}