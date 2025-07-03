using qlbb2.Data.Entities;

namespace qlbb2.Infrastructure.Repositories.Interface
{
    public interface ISupplierRepository
    {
        Task<List<TblSupplier>> GetAllAsync();
        Task<TblSupplier> GetByIdAsync(int supplierId);
        Task AddAsync(TblSupplier supplier);
        Task UpdateAsync(TblSupplier supplier);
        Task DeleteAsync(TblSupplier supplier);
        Task<List<TblSupplier>> SearchAsync(string searchText);
    }
}
