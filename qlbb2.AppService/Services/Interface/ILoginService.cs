using qlbb2.Data.Views;

namespace qlbb2.AppService.Services.Interface
{
    public interface ILoginService
    {
        Task<UserInfo> LoginAsync(string username, string password);
        bool IsLoggedIn();
        void Logout();
    }
}
