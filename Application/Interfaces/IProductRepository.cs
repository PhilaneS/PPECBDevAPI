using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetByUserIdAsync(int userId);
        Task<Product> GetByIdAndUserIdAsync(int id,int userId);
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(int id, int userId);

        Task<(List<Product>, int)> GetPagedAsync(int userId, int pageNumber, int pageSize);
    }
}
