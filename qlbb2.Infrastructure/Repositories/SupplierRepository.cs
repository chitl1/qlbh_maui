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
        public async Task AddAsync(TblSupplier supplier)
        {
            try
            {
                await _context.Suppliers.AddAsync(supplier);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new InvalidOperationException("An error occurred while adding the supplier.", ex);
            }
        }

        public Task DeleteAsync(TblSupplier supplier)
        {
            if(supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier), "Supplier cannot be null");
            }
            _context.Suppliers.Remove(supplier);
            return _context.SaveChangesAsync();
        }

        public async Task<List<TblSupplier>> GetAllAsync()
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

        public async Task<TblSupplier> GetByIdAsync(int supplierId)
        {
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            if (supplier == null)
            {
                throw new KeyNotFoundException($"Supplier with ID {supplierId} not found.");
            }
            return supplier;
        }

        public Task<List<TblSupplier>> SearchAsync(string searchText)
        {
            var result = _context.Suppliers
                .Where(s => s.SupplierName.Contains(searchText) || s.Phone.Contains(searchText) || s.Email.Contains(searchText))
                .ToListAsync();
            return result;
        }

        public Task UpdateAsync(TblSupplier supplier)
        {
            if(supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier), "Supplier cannot be null");
            }
            var existingSupplier = _context.Suppliers.Find(supplier.SupplierId);
            if (existingSupplier == null)
            {
                throw new InvalidOperationException($"Supplier with ID {supplier.SupplierId} not found.");
            }

            // Update properties
            existingSupplier.SupplierName = supplier.SupplierName;
            existingSupplier.Phone = supplier.Phone;
            existingSupplier.Email = supplier.Email;
            existingSupplier.Address = supplier.Address;
            _context.Suppliers.Update(existingSupplier);
            return _context.SaveChangesAsync();
        }
    }
}
