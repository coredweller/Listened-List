using System.Web.Profile;

namespace Core.Membership
{
    public class UserProfile : ProfileBase
    {
        public static UserProfile GetUserProfile( string userName ) {
            return Create( userName ) as UserProfile;
        }

        [SettingsAllowAnonymous( false )]
        public bool? Public { get { return base["Public"] as bool?; } set { base["Public"] = value; } }

        [SettingsAllowAnonymous( false )]
        public string Name { get { return base["Name"] as string; } set { base["Name"] = value; } }

        [SettingsAllowAnonymous( false )]
        public float? ButtonSize { get { return base["ButtonSize"] as float?; } set { base["ButtonSize"] = value; } }

        [SettingsAllowAnonymous( false )]
        public float? FontSize { get { return base["FontSize"] as float?; } set { base["FontSize"] = value; } }
    }
}
