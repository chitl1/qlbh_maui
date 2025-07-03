

namespace qlbb2.AppService.Services.Interface
{
    public interface IGenericService<T> where T: class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<List<T>> SearchAsync(string searchText);
    }
}
