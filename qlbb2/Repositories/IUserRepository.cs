using qlbb2.Entities;

namespace qlbb2.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<List<User>> SearchAsync(string searchText);
    }
}
