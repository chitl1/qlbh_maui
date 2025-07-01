using qlbb2.Data.Views;

namespace qlbb2.Infrastructure.Repositories.Interface
{
    public interface ILoginRepository
    {
        Task<UserInfo> LoginAsync(string username, string password);
        bool IsLoggedIn();
        void Logout();
    }
}
