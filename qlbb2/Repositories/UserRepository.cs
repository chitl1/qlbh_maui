
using Microsoft.EntityFrameworkCore;
using qlbb2.Data;
using qlbb2.Entities;

namespace qlbb2.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) {
            _context = context;
        }
        public Task AddAsync(User user)
        {
            _context.Users.Add(user);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            _context.Users.Remove(new User { UserId = id });
            return _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public Task<List<User>> SearchAsync(string searchText)
        {
            var result = _context.Users
                .Where(u => u.UserName.Contains(searchText) || u.Role.Contains(searchText))
                .ToListAsync();
            return result;
        }

        public Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return _context.SaveChangesAsync();
        }
    }
}
