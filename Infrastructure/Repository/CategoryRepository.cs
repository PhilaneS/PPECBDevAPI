using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CodeExistsAsync(string categoryCode)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryCode == categoryCode);
        }

        public async Task CreateAsync(Category categoryDto)
        {
            _context.Categories.Add(categoryDto);
             await _context.SaveChangesAsync();
        }
        public async Task<Category?> GetByIdAsync(int id, int userId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c=> c.CategoryId == id && c.UserId == userId);
        }

        public async Task<IEnumerable<Category>> GetByUserIdAsync(int userId)
        {
            return await _context.Categories.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
