using System.Web.Security;

namespace Core.Membership
{
    public class ListenedSqlRoleProvider : SqlRoleProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
        }

        #region IRoleProvider Members


        public bool DeleteRole(string roleName)
        {
            return base.DeleteRole(roleName, true);
        }

        #endregion
    }
}
