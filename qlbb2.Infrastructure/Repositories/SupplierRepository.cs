using qlbb2.Data.Entities;
using qlbb2.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace qlbb2.Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext _context;
        public SupplierRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "AppDbContext cannot be null");
        }
        public Task AddSupplierAsync(TblSupplier supplier)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSupplierAsync(int supplierId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TblSupplier>> GetAllSuppliersAsync()
        {
            try
            {
                return await _context.Suppliers.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new InvalidOperationException("An error occurred while retrieving suppliers.", ex);
                    }
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
