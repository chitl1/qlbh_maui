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
        public Task AddAsync(TblSupplier supplier)
        {
            return _supplierRepository.AddAsync(supplier);
        }

        public async Task DeleteAsync(int supplierId)
        {
            try
            {
                var supplier = await _supplierRepository.GetByIdAsync(supplierId);
                if(supplier != null)
                {
                    await _supplierRepository.DeleteAsync(supplier);
                }
                else
                {
                    throw new InvalidOperationException($"Supplier with ID {supplierId} not found.");
                }
            }
            catch (Exception ex) {
                  // Log the exception or handle it appropriately
                throw new InvalidOperationException($"Error deleting supplier with ID {supplierId}: {ex.Message}", ex);
            }
        }

        public Task<List<TblSupplier>> GetAllAsync()
        {
            return _supplierRepository.GetAllAsync();
        }

        public Task<List<TblSupplier>> SearchAsync(string searchText)
        {
            return _supplierRepository.SearchAsync(searchText);
        }

        public Task UpdateAsync(TblSupplier supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier), "Supplier cannot be null");
            }
            return _supplierRepository.UpdateAsync(supplier);
        }
    }
}
