using Application.Common.Exceptions;
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

        public async Task<bool> CodeExistsAsync(string categoryCode, int excludeCategoryId)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryCode == categoryCode && c.CategoryId != excludeCategoryId);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<Category> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> GetByIdAsync(int id, int userId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id && c.UserId == userId);
            if (category == null)
                throw new NotFoundException("Category is not found");
            return category;
        }

        public async Task<IEnumerable<Category>> GetByUserIdAsync(int userId)
        {
            return await _context.Categories.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
