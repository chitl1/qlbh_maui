using qlbb2.Data.Entities;
using qlbb2.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace qlbb2.Infrastructure.Repositories
{
    public class SupplierRepository : Repository<TblSupplier>, ISupplierRepository
    {
        private readonly AppDbContext _context;
        public SupplierRepository(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "AppDbContext cannot be null");
        }
        public override Task<List<TblSupplier>> SearchAsync(string searchText)
        {
            try
            {
                var result = _context.Suppliers
                    .Where(s => EF.Functions.Like(s.SupplierName, $"%{searchText}%") ||
                                EF.Functions.Like(s.Phone, $"%{searchText}%") ||
                                EF.Functions.Like(s.Email, $"%{searchText}%") ||
                                EF.Functions.Like(s.Address, $"%{searchText}%"))
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"An error occurred while searching for suppliers: {ex.Message}", ex);
            }
        }
    }
}
