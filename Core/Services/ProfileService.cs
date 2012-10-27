using Core.Membership;
using Core.Services.Interfaces;
using System.Collections.Generic;
using System.Web.Profile;
using System;
using System.Linq;
using Core.Extensions;

namespace Core.Services
{
    public class ProfileService : IProfileService
    {
        private UserProfile _Profile;

        public ProfileService( string userName ) {
            _Profile = UserProfile.GetUserProfile( userName );
        }

        /// <summary>
        /// Gets all profiles
        /// </summary>
        /// <returns></returns>
        private static ProfileInfoCollection GetAllProfiles() {
            return ProfileManager.GetAllProfiles( ProfileAuthenticationOption.All );
        }

        /// <summary>
        /// Gets all public profiles
        /// </summary>
        /// <returns></returns>
        public static IList<UserProfile> GetPublicProfiles() {
            return GetPublicProfiles(string.Empty);
        }

        /// <summary>
        /// Gets public profiles except you
        /// </summary>
        /// <returns></returns>
        public static IList<UserProfile> GetPublicProfiles( string you ) {
            var publicProfiles = new List<UserProfile>();

            foreach ( ProfileInfo profile in GetAllProfiles() ) {
                UserProfile p = UserProfile.GetUserProfile( profile.UserName );

                if ( p.Public.HasValue && p.Public.Value && p.UserName != you ) publicProfiles.Add( p );
            }

            return publicProfiles;
        }

        public static IList<UserProfile> GetProfilesLikeUserName( string userName ) {

            if ( string.IsNullOrEmpty( userName ) || string.IsNullOrWhiteSpace( userName ) ) return null;

            var profiles = new List<UserProfile>();

            foreach ( ProfileInfo profile in GetAllProfiles() ) {
                if ( profile.UserName == userName || profile.UserName.Contains( userName ) ) {
                    UserProfile p = UserProfile.GetUserProfile( profile.UserName );
                    profiles.Add( p );
                }
            }

            return profiles;
        }

        /// <summary>
        /// Use this to save just the name
        /// </summary>
        /// <param name="name"></param>
        public void SaveName( string name ) {
            SetName( name );
            _Profile.Save();
        }

        /// <summary>
        /// Use this to save just Public
        /// </summary>
        /// <param name="isPublic"></param>
        public void SavePublic( bool isPublic ) {
            SetPublic( isPublic );
            _Profile.Save();
        }

        public UserProfile GetUserProfile() {
            return _Profile;
        }

        /// <summary>
        /// Use this to save all the profile items at once
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="isPublic"></param>
        public void SaveProfile( string name, string email, bool isPublic ) {
            SetName( name );
            SetPublic( isPublic );
            _Profile.Save();
        }


        /// <summary>
        /// Worker method that is the only method to know how to set name
        /// </summary>
        /// <param name="name"></param>
        private void SetName( string name ) {
            _Profile.SetPropertyValue( "Name", name );
        }

        /// <summary>
        /// Worker method that is the only method to know how to set Public
        /// </summary>
        /// <param name="isPublic"></param>
        private void SetPublic( bool isPublic ) {
            _Profile.SetPropertyValue( "Public", isPublic );
        }
    }
}
