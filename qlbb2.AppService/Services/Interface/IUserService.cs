using qlbb2.Data.Entities;

namespace qlbb2.AppService.Services.Interface
{
    public interface IUserService
    {
        Task<List<User>> GetDisplayUserAsync();
        Task AddPersonAsync(User user);
        Task DeletePersonAsync(int personId);
        Task UpdatePersonAsync(User user);
        Task<List<User>> SearchUsersAsync(string searchText);
    }
}
