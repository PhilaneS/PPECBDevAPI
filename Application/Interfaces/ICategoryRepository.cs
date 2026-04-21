
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id, int userId);
        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> CodeExistsAsync(string categoryCode, int excludeCategoryId);

    }
}
