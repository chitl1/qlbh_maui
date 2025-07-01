using qlbb2.Data.Entities;
using qlbb2.Data.Views;
using qlbb2.Infrastructure.Repositories.Interface;

namespace qlbb2.Infrastructure.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IUserRepository _userRepository;
        private User _currentUser; // Lưu user đang đăng nhập
        public LoginRepository( IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool IsLoggedIn()
        {
            return _currentUser != null;
        }
        public async Task<UserInfo> LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var users = await _userRepository.GetAllAsync();
            var userlogin = users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (userlogin != null)
            {
                _currentUser = userlogin;
                // Chuyển đổi User sang UserInfo
                var userInfo = new UserInfo
                {
                    UserName = userlogin.UserName,
                    Role = userlogin.Role,
                };
                return userInfo;
            }
            return null;
        }

        public void Logout()
        {
            _currentUser = null;
        }

    }
}
