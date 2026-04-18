using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetByUserIdAsync(int userId);
        Task<Product> GetByIdAsync(int id,int userId);
        Task CreateAsync(Product productDto);
        Task<Product> UpdateAsync(Product productDto);
        Task DeleteAsync(int id, int userId);

        Task<(List<Product>, int)> GetPagedAsync(int userId, int pageNumber, int pageSize);
    }
}
