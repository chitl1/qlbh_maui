using qlbb2.AppService.Services.Interface;
using qlbb2.Data.Views;
using qlbb2.Infrastructure.Repositories.Interface;

namespace qlbb2.AppService.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public bool IsLoggedIn()
        {
            return _loginRepository.IsLoggedIn();
        }

        public async Task<UserInfo> LoginAsync(string username, string password)
        {
            return await _loginRepository.LoginAsync(username, password);
        }

        public void Logout()
        {
            _loginRepository.Logout();
        }
    }
}
