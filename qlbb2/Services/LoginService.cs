using qlbb2.Model;
using qlbb2.Repositories;

namespace qlbb2.Services
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
