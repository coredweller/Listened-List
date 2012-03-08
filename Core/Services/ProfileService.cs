using Core.Membership;
using Core.Services.Interfaces;

namespace Core.Services
{
    public class ProfileService : IProfileService
    {
        private UserProfile _Profile;

        public ProfileService( string userName ) {
            _Profile = UserProfile.GetUserProfile( userName );
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
        /// Use this to save just the email
        /// </summary>
        /// <param name="email"></param>
        public void SaveEmail( string email ) {
            SetEmail( email );
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
            SetEmail( email );
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
        /// Worker method that is the only method to know how to set the email
        /// </summary>
        /// <param name="email"></param>
        private void SetEmail( string email ) {
            _Profile.SetPropertyValue( "Email", email );
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
