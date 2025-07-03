using Microsoft.EntityFrameworkCore;
using qlbb2.Data.Entities;
using qlbb2.Infrastructure;
using qlbb2.Infrastructure.Repositories.Interface;


namespace qlbb2.Infrastructure.Repositories
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
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            return user;
        }

        public Task<List<User>> SearchAsync(string searchText)
        {
            var result = _context.Users
                .Where(u => u.UserName.Contains(searchText) || u.Role.Contains(searchText))
                .ToListAsync();
            return result;
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null)
                throw new InvalidOperationException($"User with ID {user.UserId} not found.");

            // Update properties
            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.Role = user.Role;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }
    }
}
