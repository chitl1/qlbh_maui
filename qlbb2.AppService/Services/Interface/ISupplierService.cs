using qlbb2.Data.Entities;

namespace qlbb2.AppService.Services.Interface
{
    public interface ISupplierService
    {
        Task<List<TblSupplier>> GetAllAsync();
        Task AddAsync(TblSupplier supplier);
        Task UpdateAsync(TblSupplier supplier);
        Task DeleteAsync(int supplierId);
        Task<List<TblSupplier>> SearchAsync(string searchText);
    }
}
