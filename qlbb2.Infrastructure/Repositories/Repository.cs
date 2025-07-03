using Microsoft.EntityFrameworkCore;
using qlbb2.Infrastructure.Repositories.Interface;

namespace qlbb2.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "AppDbContext cannot be null");
        }
        public Task AddAsync(T entity)
        {
            //_context.Users.Add(user);
            _context.Set<T>().Add(entity);
            return _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            if (result == null)
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");

            return result;
        }

        public async Task<List<T>> SearchAsync(string searchText)
        {
            var result = await _context.Set<T>()
                .Where(e => EF.Functions.Like(e.ToString(), $"%{searchText}%"))
                .ToListAsync();
            return result;
        }

        public async Task UpdateAsync(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
