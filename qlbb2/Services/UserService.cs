using qlbb2.Entities;
using qlbb2.Repositories;

namespace qlbb2.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task AddPersonAsync(User user)
        {
           return _userRepository.AddAsync(user);
        }

        public async Task DeletePersonAsync(int personId)
        {
            var user = await _userRepository.GetByIdAsync(personId);
            if (user == null)
            {
                await _userRepository.DeleteAsync(personId);
                return;
            }
        }

        public Task<List<User>> GetDisplayUserAsync()
        {
            var users = _userRepository.GetAllAsync();
            return users;
        }

        public Task<List<User>> SearchUsersAsync(string searchText)
        {
            return _userRepository.SearchAsync(searchText);
        }
    }
}
