
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICategoryRepository
    {
            Task<IEnumerable<Category>> GetByUserIdAsync(int userId);
            Task<Category?> GetByIdAsync(int id,int userId);
            Task CreateAsync(Category categoryDto);
            Task<Category> UpdateAsync(Category category);           
            Task<bool> CodeExistsAsync(string categoryCode);

    }
}
