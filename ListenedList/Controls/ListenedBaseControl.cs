using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Infrastructure.Logging;
using Data.DomainObjects;
using Core.DomainObjects;
using Core.Helpers.Script;
using Core.Membership;
using Core.Infrastructure;

namespace ListenedList.Controls
{
    public class ListenedBaseControl : System.Web.UI.UserControl
    {
        protected LogWriter _Log = new LogWriter();
        protected IDomainObjectFactory _DomainObjectFactory = Ioc.GetInstance<IDomainObjectFactory>();
        
        public Guid GetUserId() {
            return Base.GetUserId( Page.User.Identity.Name );
        }

        public void ValidateSuccess( bool success, string successMessage, string error ) {
            Page.RegisterStartupScript("validateSuccess", Base.ValidateSuccess(success, successMessage, error));
        }
    }
}