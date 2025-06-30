

using qlbb2.Entities;
using qlbb2.Repositories;

namespace qlbb2.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public Task AddSupplierAsync(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSupplierAsync(int supplierId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Supplier>> GetAllSuppliersAsync()
        {
            return _supplierRepository.GetAllSuppliersAsync();
        }

        public Task<Supplier> GetSupplierByIdAsync(int supplierId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Supplier>> SearchAsync(string searchText)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSupplierAsync(Supplier supplier)
        {
            throw new NotImplementedException();
        }
    }
}
