using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
       // Task<bool> IsEmailExistsAsync(string email);
        Task CreateAsync(UserDto registerDto);
        //Task<int?> GetUserIdByEmailAsync(string email);
        Task<User?> LoginAsync(string email);
    }
}
