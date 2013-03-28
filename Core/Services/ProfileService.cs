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
        /// Gets all profiles infos
        /// </summary>
        /// <returns></returns>
        private static ProfileInfoCollection GetAllProfileInfos() {
            return ProfileManager.GetAllProfiles( ProfileAuthenticationOption.All );
        }

        /// <summary>
        /// Gets all profiles
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<UserProfile> GetProfiles() {
            return GetAllProfileInfos().Cast<ProfileInfo>().Select( x => UserProfile.GetUserProfile( x.UserName ) );
        }

        /// <summary>
        /// Gets all public profiles
        /// </summary>
        /// <returns></returns>
        public static IList<UserProfile> GetPublicProfiles() {
            return GetPublicProfiles( string.Empty );
        }

        /// <summary>
        /// Gets all profiles for the public
        /// </summary>
        /// <returns></returns>
        public static IList<UserProfile> GetAllProfiles() {
            return GetProfiles().ToList();
        }

        /// <summary>
        /// Gets public profiles except you
        /// </summary>
        /// <returns></returns>
        public static IList<UserProfile> GetPublicProfiles( string you ) {
            if ( string.IsNullOrWhiteSpace( you ) ) return null;
            return GetProfiles().Where( y => y.Public.HasValue && y.Public.Value && y.UserName != you ).ToList();
        }

        public static IList<UserProfile> GetProfilesLikeUserName( string userName ) {
            if ( string.IsNullOrWhiteSpace( userName ) ) return null;
            return GetProfiles().Where( x => x.UserName.Contains( userName ) ).ToList();
        }

        /// <summary>
        /// Use this to save just the button size
        /// </summary>
        /// <param name="buttonSize"></param>
        public void SaveButtonSize( float buttonSize ) {
            SetButtonSize( buttonSize );
            _Profile.Save();
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

        /// <summary>
        /// Worker method that is the only method to know how to set ButtonSize
        /// </summary>
        /// <param name="buttonSize"></param>
        private void SetButtonSize( float buttonSize ) {
            _Profile.SetPropertyValue( "ButtonSize", buttonSize );
        }
    }
}
