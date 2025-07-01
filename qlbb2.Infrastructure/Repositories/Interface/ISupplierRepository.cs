using qlbb2.Data.Entities;

namespace qlbb2.Infrastructure.Repositories.Interface
{
    public interface ISupplierRepository
    {
        Task<List<TblSupplier>> GetAllSuppliersAsync();
        Task<TblSupplier> GetSupplierByIdAsync(int supplierId);
        Task AddSupplierAsync(TblSupplier supplier);
        Task UpdateSupplierAsync(TblSupplier supplier);
        Task DeleteSupplierAsync(int supplierId);
        Task<List<TblSupplier>> SearchAsync(string searchText);
    }
}
