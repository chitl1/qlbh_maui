

using qlbb2.Entities;

namespace qlbb2.Services
{
    public interface IUserService 
    {
        Task<List<User>> GetDisplayUserAsync();
        Task AddPersonAsync(User user);
        Task DeletePersonAsync(int personId);
        Task<List<User>> SearchUsersAsync(string searchText);
    }
}
