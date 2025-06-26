

using qlbb2.Model;

namespace qlbb2.Services
{
    public interface ILoginService
    {
        Task<UserInfo> LoginAsync(string username, string password);
        bool IsLoggedIn();
        void Logout();
    }
}
