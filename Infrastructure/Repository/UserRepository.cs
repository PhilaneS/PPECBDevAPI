using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(UserDto register)
        {
            var user = new User
            {
                Email = register.Email,
                Password = register.Password              
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> LoginAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<int?> GetUserIdByEmailAsync(string email)
        {
            return Task.FromResult<int?>(_context.Users.Where(u => u.Email == email).Select(u => (int?)u.Id).FirstOrDefault());
        }

        public Task<bool> IsEmailExistsAsync(string email)
        {
            return Task.FromResult(_context.Users.Any(u => u.Email == email));
        }
    }
}
