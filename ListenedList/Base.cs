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
        private string _USER_CACHE_KEY = "userId";

        public Guid GetUserId( string userName ) {
            var userObject = HttpRuntime.Cache.Get( _USER_CACHE_KEY );

            if ( userObject != null ) {
                Guid userId;
                if ( Guid.TryParse( userObject.ToString(), out userId ) ) return userId;
            }

            return CacheUserId( userName );
        }

        public void ClearCachedUserId() {
            HttpRuntime.Cache.Remove( _USER_CACHE_KEY );
        }

        private Guid CacheUserId( string userName ) {
            var userId = _MembershipProvider.GetUser( userName ).ProviderUserKey.ToString();
            if ( string.IsNullOrEmpty( userId ) ) return Guid.Empty;

            HttpRuntime.Cache.Insert( _USER_CACHE_KEY, userId, null, DateTime.Now.AddMinutes( 60 ), System.Web.Caching.Cache.NoSlidingExpiration );
            return new Guid( userId );
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