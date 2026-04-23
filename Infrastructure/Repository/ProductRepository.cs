using Application.Common.Exceptions;
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
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new NotFoundException($"Product with Id {id} not found.");
            }

            return product;
        }


        public async Task<IEnumerable<Product>> GetByUserIdAsync(int userId)
        {
           return await _context.Products.AsNoTracking().Where(p => p.UserId == userId).ToListAsync();
        }
        public async Task<Product> CreateAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }       

        public async Task<Product> GetByIdAndUserIdAsync(int id, int userId)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            if (product == null)
            {
                throw new NotFoundException($"Product with Id {id} and UserId {userId} not found.");
            }
            return product;
        }

        public async Task<Product> UpdateAsync(Product product, byte[] rowVersion)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (rowVersion == null) throw new ArgumentNullException(nameof(rowVersion));

            _context.Products.Attach(product);
            _context.Entry(product).Property(p => p.RowVersion).OriginalValue = rowVersion;

            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return product;
        }
        public async Task DeleteAsync(int id, int userId)
        {
           var product =  _context.Products.Where(p => p.Id == id && p.UserId == userId);
            if (product == null)
            {
                throw new NotFoundException($"Product with Id {id} and UserId {userId} not found.");
            }
            _context.Products.RemoveRange(product);
            var deleted = await _context.SaveChangesAsync();
        }
        public async Task<(List<Product>, int)> GetPagedAsync(
           int userId,
           int pageNumber,
           int pageSize)
        {
            if(pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _context.Products.AsNoTracking()
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
