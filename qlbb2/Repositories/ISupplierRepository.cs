

using qlbb2.Entities;

namespace qlbb2.Repositories
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetAllSuppliersAsync();
        Task<Supplier> GetSupplierByIdAsync(int supplierId);
        Task AddSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(int supplierId);
        Task<List<Supplier>> SearchAsync(string searchText);
    }
}
