using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qlbb2.Services
{
    public interface ISupplierService
    {
        Task<List<Entities.Supplier>> GetAllSuppliersAsync();
        Task<Entities.Supplier> GetSupplierByIdAsync(int supplierId);
        Task AddSupplierAsync(Entities.Supplier supplier);
        Task UpdateSupplierAsync(Entities.Supplier supplier);
        Task DeleteSupplierAsync(int supplierId);
        Task<List<Entities.Supplier>> SearchAsync(string searchText);
    }
}
