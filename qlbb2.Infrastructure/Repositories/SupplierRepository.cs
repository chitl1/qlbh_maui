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
    }
}
