using Microsoft.EntityFrameworkCore;
using qlbb2.Data;
using qlbb2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qlbb2.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext _context;
        public SupplierRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "AppDbContext cannot be null");
        }
        public Task AddSupplierAsync(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSupplierAsync(int supplierId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Supplier>> GetAllSuppliersAsync()
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
