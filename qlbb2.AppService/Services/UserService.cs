using qlbb2.AppService.Services.Interface;
using qlbb2.Data.Entities;
using qlbb2.Infrastructure.Repositories.Interface;


namespace qlbb2.AppService.Services
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
            try
            {
                var user = await _userRepository.GetByIdAsync(personId);
                if (user != null)
                {
                    await _userRepository.DeleteAsync(user);
                }
                else
                {
                    throw new InvalidOperationException($"User with ID {personId} not found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new InvalidOperationException($"Error deleting user with ID {personId}: {ex.Message}", ex);
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

        public async Task UpdatePersonAsync(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User cannot be null");

                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error updating user with ID {user?.UserId}: {ex.Message}", ex);
            }
        }
    }
}
