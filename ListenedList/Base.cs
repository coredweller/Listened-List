using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Membership;
using Core.Helpers.Script;

namespace ListenedList
{
    public class Base
    {
        protected static IMembershipProvider _MembershipProvider = new ListenedMembershipProvider();

        public static Guid GetUserId( string userName ) {

            var user = _MembershipProvider.GetUser( userName ).ProviderUserKey.ToString();
            if ( user != null ) {
                return new Guid( user );
            }

            return Guid.Empty;
        }

        public static string ValidateSuccess( bool success, string successMessage, string error ) {
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