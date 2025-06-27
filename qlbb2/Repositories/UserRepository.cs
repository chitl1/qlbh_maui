using Microsoft.EntityFrameworkCore;
using qlbb2.Data;
using qlbb2.Entities;
using System;

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

        public Task DeleteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            _context.Users.Remove(user);
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
