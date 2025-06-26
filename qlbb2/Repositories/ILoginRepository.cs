

using qlbb2.Model;

namespace qlbb2.Repositories
{
    public interface ILoginRepository
    {
        Task<UserInfo> LoginAsync(string username, string password);
        bool IsLoggedIn();
        void Logout();
    }
}
