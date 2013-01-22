using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Membership;
using Core.Helpers.Script;
using Core.Infrastructure;

namespace ListenedList
{
    public class Base
    {
        protected IMembershipProvider _MembershipProvider = Ioc.GetInstance<IMembershipProvider>();

        public Guid GetUserId( string userName ) {
            var user = _MembershipProvider.GetUser( userName ).ProviderUserKey.ToString();

            if ( user == null ) return Guid.Empty;

            return new Guid( user );
        }

        public string ValidateSuccess( bool success, string successMessage, string error ) {
            PromptHelper prompt;

            if ( success ) {
                prompt = new PromptHelper( successMessage );
                return prompt.GetSuccessScript();
            }
            else {
                prompt = new PromptHelper( error );
                return prompt.GetErrorScript();
            }
        }
    }
}