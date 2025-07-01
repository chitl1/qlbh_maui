using qlbb2.AppService.Services.Interface;
using qlbb2.Data.Entities;
using qlbb2.Infrastructure.Repositories.Interface;

namespace qlbb2.AppService.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public Task AddSupplierAsync(TblSupplier supplier)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSupplierAsync(int supplierId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TblSupplier>> GetAllSuppliersAsync()
        {
            return _supplierRepository.GetAllSuppliersAsync();
        }

        public Task<TblSupplier> GetSupplierByIdAsync(int supplierId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TblSupplier>> SearchAsync(string searchText)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSupplierAsync(TblSupplier supplier)
        {
            throw new NotImplementedException();
        }
    }
}
