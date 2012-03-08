﻿using System.Web.Profile;

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
        public string Email { get { return base["Email"] as string; } set { base["Email"] = value; } }
    }
}
