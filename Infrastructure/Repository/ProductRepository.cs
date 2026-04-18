using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetByUserIdAsync(int userId)
        {
           return await _context.Products.Where(p => p.UserId == userId).ToListAsync();
        }
        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }       

        public async Task<Product> GetByIdAsync(int id, int userId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with Id {id} and UserId {userId} not found.");
            }
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public Task DeleteAsync(int id, int userId)
        {
           var deletedProduct =  _context.Products.Where(p => p.Id == id && p.UserId == userId);
            if (deletedProduct == null)
            {
                throw new InvalidOperationException($"Product with Id {id} and UserId {userId} not found.");
            }
            _context.Products.RemoveRange(deletedProduct);
            return _context.SaveChangesAsync();
        }
        public async Task<(List<Product>, int)> GetPagedAsync(
           int userId,
           int pageNumber,
           int pageSize)
        {
            var query = _context.Products
                .Where(p => p.UserId == userId)
                .Include(p => p.Category);

            var total = await query.CountAsync();

            var data = await query
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, total);
        }
    }
}
