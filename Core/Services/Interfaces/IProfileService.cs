using Core.Membership;

namespace Core.Services.Interfaces
{
    public interface IProfileService
    {
        void SaveName( string name ) ;
        void SavePublic( bool isPublic );
        void SaveProfile( string name, string email, bool isPublic );

        UserProfile GetUserProfile();
    }
}
